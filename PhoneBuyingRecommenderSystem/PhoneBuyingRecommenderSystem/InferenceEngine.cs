using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using VDS.RDF.Query;

namespace PhoneBuyingRecommenderSystem
{
    /// <summary>
    /// The main class for consult feature, finds the phones suitable for customer
    /// </summary>
    static class InferenceEngine
    {
        public static int MaxScore { get; private set; }
        public static Dictionary<string, List<Fact>> ModelFacts { get; private set; }

        static HashSet<Fact> Known = new HashSet<Fact>();
        static List<Rule> Rules = new List<Rule>();
        static HashSet<string> PhoneProperties = new HashSet<string>(new string[] { "Manufacturer", "Price", "Material", "Color", "OS", "OSName", "ScreenSize", "HeightOfRes", "WidthOfRes", "FrontMegapixel", "RearMegapixel", "BatteryCapacity", "InternalStorageCapacity", "RAMCapacity", "SpecialFeature" });
        static Dictionary<Fact, int> FactScore = new Dictionary<Fact, int>();

        /// <summary>
        /// Loads rules from file
        /// </summary>
        public static void LoadRules()
        {
            StreamReader reader = new StreamReader("Rules.txt");
            while (!reader.EndOfStream)
            {
                string ruleString = reader.ReadLine();
                Rule rule = new Rule(ruleString);
                Rules.Add(rule);
            }
        }

        /// <summary>
        /// Returns sorted phone models suitable for customer information
        /// </summary>
        /// <param name="consultOptions">customer information</param>
        /// <param name="models">phone models (as [modelKey, modelName]) to consult</param>
        /// <returns> List with key is model, and value is count of suitable properties on model</returns>
        public static List<KeyValuePair<string, int>> DoConsult(ConsultOptions consultOptions, Dictionary<string, string> models)
        {
            ModelFacts = new Dictionary<string, List<Fact>>();

            Dictionary<string, int> DModels = new Dictionary<string, int>();
            foreach (var model in models)
                DModels[model.Key] = 0;

            InitKnown(consultOptions);

            ForwardChaining();

            CountMatchingFacts(DModels);

            List<KeyValuePair<string, int>> LModels = new List<KeyValuePair<string, int>>();
            foreach (var model in DModels)
                LModels.Add(model);

            SortModels(LModels);

            return LModels;
        }

        static void InitKnown(ConsultOptions consultOptions)
        {
            Known = new HashSet<Fact>();
            Fact fact;
            int i;

            i = consultOptions.GenderIndex;
            if (i != 0)
            {
                fact = new Fact("Gender", "=", ConsultOptions.GenderKeys[i]);
                FactScore[fact] = consultOptions.GenderScore;
                Known.Add(fact);
            }

            i = consultOptions.AgeIndex;
            if (i != 0)
            {
                fact = new Fact();
                fact.Name = "Age";
                if (i == 1)
                {
                    fact.Operator = "<";
                    fact.Value = "12";
                }
                else if (i == 61)
                {
                    fact.Operator = ">";
                    fact.Value = "70";
                }
                else
                {
                    fact.Operator = "=";
                    fact.Value = ConsultOptions.AgeValues[i];
                }
                FactScore[fact] = consultOptions.AgeScore;
                Known.Add(fact);
            }

            foreach (int j in consultOptions.HobbyIndices)
            {
                fact = new Fact("Hobby", "=", ConsultOptions.HobbyKeys[j]);
                FactScore[fact] = consultOptions.HobbyScores[j];
                Known.Add(fact);
            }

            foreach (int j in consultOptions.MajorIndices)
            {
                fact = new Fact("Major", "=", ConsultOptions.MajorKeys[j]);
                FactScore[fact] = consultOptions.MajorScores[j];
                Known.Add(fact);
            }

            foreach (int j in consultOptions.DemandIndices)
            {
                fact = new Fact("Demand", "=", ConsultOptions.DemandKeys[j]);
                FactScore[fact] = consultOptions.DemandScores[j];
                Known.Add(fact);
            }
        }

        static void ForwardChaining()
        {
            HashSet<Fact> Hold;
            do
            {
                Hold = new HashSet<Fact>(Known);
                foreach (Rule r in Rules)
                {
                    if (Known.IsSupersetOf(r.Premises))
                    {
                        int score = 1;
                        foreach (Fact f in r.Premises)
                            if (score < FactScore[f])
                                score = FactScore[f];

                        foreach (Fact f in r.Conclusions)
                            FactScore[f] = score;

                        Known = new HashSet<Fact>(Known.Union(r.Conclusions));
                    }
                }
            } while (!Hold.SetEquals(Known));

            FilterKnown();
        }

        static void FilterKnown()
        {
            HashSet<Fact> temp = new HashSet<Fact>(Known);
            foreach (Fact f in Known)
                if (!PhoneProperties.Contains(f.Name))
                    temp.Remove(f);
            Known = temp;

            MaxScore = 0;
            foreach (Fact f in Known)
                MaxScore += FactScore[f];
        }

        static void CountMatchingFacts(Dictionary<string, int> models)
        {
            foreach (Fact f in Known)
            {
                string prefix = "";
                if (f.Name == "Manufacturer" || f.Name == "OS" || f.Name == "CPU")
                    prefix = "ont:";

                string patterns = "";
                if (f.Name == "CPUCores")
                    patterns = @"
                    ?s ont:hasCPU ?prop.
                    ?prop ont:hasCores ?subprop.
                    FILTER (?subprop " + f.Operator + " " + f.Value + ").";
                else if (f.Name == "OSName")
                    patterns = @"
                    ?s ont:hasOS ?prop.
                    ?prop ont:hasName ?subprop.
                    FILTER (?subprop " + f.Operator + " " + f.Value + ").";
                else if (f.Name == "Color")
                    patterns = @"
                    ?s ont:hasColor ?prop.
                    FILTER regex(?prop, " + f.Value + ").";
                else
                    patterns = @"
                    ?s ont:has" + f.Name + @" ?prop.
                    FILTER (?prop " + f.Operator + " " + prefix + f.Value + ").";

                SparqlResultSet results = SPARQL.DoQuery(@"
                PREFIX ont: <http://www.co-ode.org/ontologies/ont.owl#>
                SELECT ?model WHERE 
                { 
                    ?s a ont:PhoneModel. BIND (STRAFTER(STR(?s), STR(ont:)) AS ?model).
                    " + patterns + @"
                }");

                foreach (var result in results)
                {
                    string modelKey = result.Value("model").ToString();
                    if (models.ContainsKey(modelKey))
                    {
                        models[modelKey]++;
                        if (!ModelFacts.ContainsKey(modelKey))
                            ModelFacts[modelKey] = new List<Fact>();
                        ModelFacts[modelKey].Add(f);
                    }
                }
            }
        }

        static void SortModels(List<KeyValuePair<string, int>> models)
        {
            models.Sort(delegate (KeyValuePair<string, int> model1, KeyValuePair<string, int> model2)
            {
                if (model1.Value == model2.Value)
                    return 0;
                else if (model1.Value < model2.Value)
                    return 1;
                return -1;
            });
        }
    }
}
