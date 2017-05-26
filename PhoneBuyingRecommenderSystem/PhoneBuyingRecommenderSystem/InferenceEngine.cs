using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBuyingRecommenderSystem
{
    /// <summary>
    /// The main class for consult feature, finds the phones suitable for customer
    /// </summary>
    static class InferenceEngine
    {
        static HashSet<Fact> Known = new HashSet<Fact>();
        static List<Rule> Rules = new List<Rule>();
        static HashSet<string> PhoneProperties = new HashSet<string>(new string[] { "RAM", /*...*/ });

        /// <summary>
        /// Loads rules from file
        /// </summary>
        public static void LoadRules()
        {

        }

        /// <summary>
        /// Returns sorted phone models suitable for customer information
        /// </summary>
        /// <param name="consultOptions">customer information</param>
        /// <param name="models">phone models (as [modelKey, modelName]) to consult</param>
        /// <returns> List with key is model, and value is count of suitable properties on model</returns>
        public static List<KeyValuePair<string, int>> DoConsult(ConsultOptions consultOptions, Dictionary<string, string> models)
        {
            List<KeyValuePair<string, int>> L = new List<KeyValuePair<string, int>>();

            foreach (var model in models)
                L.Add(new KeyValuePair<string, int>(model.Key, 0));

            ForwardChaining();

            SortModels(L);

            FilterKnown();

            return L;
        }

        static void ForwardChaining()
        {

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

        static void FilterKnown()
        {
            foreach (Fact k in Known)
                if (!PhoneProperties.Contains(k.Name))
                    Known.Remove(k);
        }
    }
}
