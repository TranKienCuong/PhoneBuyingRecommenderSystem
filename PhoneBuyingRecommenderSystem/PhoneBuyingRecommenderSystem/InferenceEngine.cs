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

        static Dictionary<string, float> FuzzyAge = new Dictionary<string, float>();
        static List<string> keyList = new List<string>();

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

            ScoreMatchingFacts(DModels);

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
            if (i > 1)
            {
                i = int.Parse(consultOptions.AgeIndex.ToString()) + 8;

                Fuzzy f = new Fuzzy();
                FuzzyAge = f.DoTuoi(i);
                keyList = new List<string>(FuzzyAge.Keys);

                fact = new Fact("Age", "=", keyList[0]);
                FactScore[fact] = consultOptions.AgeScore;
                Known.Add(fact);
                if (FuzzyAge[keyList[1]] != 0)
                {
                    fact = new Fact("Age", "=", keyList[1]);
                    FactScore[fact] = consultOptions.AgeScore;
                    Known.Add(fact);
                }
            }
            else if (i == 1)
            {
                fact = new Fact("Age", "=", "ThieuNhi");
                FuzzyAge["ThieuNhi"] = 1;
                keyList = new List<string>(FuzzyAge.Keys);
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

        static void FuzzyInference(List<Rule> FuzzyRules)
        {
            int AgeScore = 1;
            float ScreenSizeMini = 0, ScreenSizeNho = 0, ScreenSizeTrungBinh = 0, ScreenSizeLon = 0;

            int i = 0;
            foreach (Rule r in FuzzyRules)
            {
                AgeScore = FactScore[r.Premises[0]];
                FactScore[r.Conclusions[0]] = AgeScore;
                //lấy r.Conclusions.value để tính mờ
                string[] Ages = r.Conclusions[0].Value.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                float value = FuzzyAge[keyList[i]];
                foreach (string x in Ages)
                {
                    if ("Mini".Contains(x) && ScreenSizeMini < value)
                    {
                        ScreenSizeMini = value;
                    }
                    else if ("Nho".Contains(x) && ScreenSizeNho < value)
                    {
                        ScreenSizeNho = value;
                    }
                    else if ("TrungBinh".Contains(x) && ScreenSizeTrungBinh < value)
                    {
                        ScreenSizeTrungBinh = value;
                    }
                    else if ("Lon".Contains(x) && ScreenSizeLon < value)
                    {
                        ScreenSizeLon = value;
                    }
                }
                i++;
            }

            float ScreenSize = 0;
            float Tu = 0, Mau = 0;
            if (ScreenSizeMini != 0)
            {
                ScreenSize = 5 - (float)(ScreenSizeMini * 2.2);
                Tu += ScreenSize * ScreenSizeMini;
                Mau += ScreenSizeMini;
            }
            if (ScreenSizeNho != 0)
            {
                ScreenSize = (float)((ScreenSizeNho + 1.352941176) / 0.5882352941);
                Tu += ScreenSize * ScreenSizeNho;
                Mau += ScreenSizeNho;
                ScreenSize = (float)((10 - ScreenSizeNho) / 2);
                Tu += ScreenSize * ScreenSizeNho;
                Mau += ScreenSizeNho;
            }
            if (ScreenSizeTrungBinh != 0)
            {
                ScreenSize = (float)((ScreenSizeTrungBinh + 8) / 2);
                Tu += ScreenSize * ScreenSizeTrungBinh;
                Mau += ScreenSizeTrungBinh;
                ScreenSize = (float)((55 - ScreenSizeTrungBinh * 3) / 10);
                Tu += ScreenSize * ScreenSizeTrungBinh;
                Mau += ScreenSizeTrungBinh;
            }
            if (ScreenSizeLon != 0)
            {
                ScreenSize = (float)((ScreenSizeLon + 7.142857145) / 1.428571429);
                Tu += ScreenSize * ScreenSizeLon;
                Mau += ScreenSizeLon;
            }
            if (ScreenSize != 0)
            {
                ScreenSize = Tu / Mau;
                ScreenSize = (float)Math.Round(ScreenSize, 1);
                if (ScreenSize != 0)
                {
                    Double[] size = { 1.77, 1.8, 2.4, 2.8, 4.0, 4.5, 4.7, 5.0, 5.2, 5.3, 5.5, 5.7, 5.8, 5.9, 6.0, 6.2, 6.4 };
                    double min = 10;
                    double value = 0;
                    foreach (double a in size)
                    {
                        if (min > Math.Abs(ScreenSize - a))
                        {
                            min = Math.Abs(ScreenSize - a);
                            value = a;
                        }
                    }
                    Fact fact = new Fact("ScreenSize", "=", value.ToString());
                    Known.Add(fact);
                    FactScore[fact] = AgeScore;
                }
            }
        }

        //Từ các sự kiện đã biết dò theo luật suy ra các sự kiện chưa biết
        static void ForwardChaining()
        {
            List<Rule> FuzzyRules = new List<Rule>();
            HashSet<Fact> Hold;
            do
            {
                Hold = new HashSet<Fact>(Known);
                foreach (Rule r in Rules)
                {
                    if (Known.IsSupersetOf(r.Premises) && r.Premises.Count != 0)
                    {
                        if(r.Premises[0].Name == "Age")
                        {
                            FuzzyRules.Add(r);
                        }
                        else
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
                }
            } while (!Hold.SetEquals(Known));
            FuzzyInference(FuzzyRules);

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

        static void ScoreMatchingFacts(Dictionary<string, int> models)
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
                else if (f.Name == "Material")
                    patterns = @"
                    ?s ont:hasMaterial ?prop.
                    FILTER regex(?prop, " + f.Value + ").";
                else if (f.Name == "OS")
                { 
                    switch (f.Value)
                    {
                        case "Android": patterns += ("?s ont:hasOS ?os. FILTER ( ?os > ont:" + f.Value + " && ?os < ont:B"); break;
                        case "iOS": patterns += ("?s ont:hasOS ?os. FILTER ( ?os > ont:" + f.Value + " && ?os < ont:j"); break;
                        case "Windows":
                            patterns += ("?s ont:hasOS ?os. FILTER ( ?os > ont:" + f.Value + " && ?os < ont:X"); break;
                    }
                    patterns += ").";
                }
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
                        models[modelKey] += FactScore[f];
                        if (!ModelFacts.ContainsKey(modelKey))
                            ModelFacts[modelKey] = new List<Fact>();
                        else 
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
