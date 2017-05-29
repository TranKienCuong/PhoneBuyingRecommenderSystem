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
        public static int KnownCount { get { return Known.Count; } }

        static HashSet<Fact> Known = new HashSet<Fact>();
        static List<Rule> Rules = new List<Rule>();
        static HashSet<string> PhoneProperties = new HashSet<string>(new string[] { "Manufacturer", /*...*/ });

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

            if (consultOptions.GenderIndex != 0)
            {
                Fact fact = new Fact();
                fact.Name = "Gender";
                fact.Operator = "=";
                fact.Value = ConsultOptions.GenderKeys[consultOptions.GenderIndex];
                Known.Add(fact);
            }

            // so on...
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
                        Known = new HashSet<Fact>(Known.Union(r.Conclusions));
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
        }

        static void CountMatchingFacts(Dictionary<string, int> models)
        {
            foreach (Fact f in Known)
            {
                string prefix = "";
                if (f.Name == "Manufacturer" || f.Name == "OS" || f.Name == "CPU")
                    prefix = "ont:";

                SparqlResultSet results = SPARQL.DoQuery(@"
                PREFIX ont: <http://www.co-ode.org/ontologies/ont.owl#>
                SELECT ?model WHERE 
                { 
                    ?s a ont:PhoneModel. BIND (STRAFTER(STR(?s), STR(ont:)) AS ?model).
                    ?s ont:has" + f.Name + @" ?prop.
                    FILTER (?prop " + f.Operator + " " + prefix + f.Value + @").
                }");

                foreach (var result in results)
                {
                    string modelKey = result.Value("model").ToString();
                    if (models.ContainsKey(modelKey))
                        models[modelKey]++;
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
