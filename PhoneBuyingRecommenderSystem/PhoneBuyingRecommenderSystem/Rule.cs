using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBuyingRecommenderSystem
{
    /// <summary>
    /// A rule in rule-based system which has formed: premises -> conclusions
    /// </summary>
    class Rule
    {
        public List<Fact> Premises { get; set; }
        public List<Fact> Conclusions { get; set; }

        /// <summary>
        /// Creates a new rule
        /// </summary>
        public Rule()
        {
            Premises = new List<Fact>();
            Conclusions = new List<Fact>();
        }

        /// <summary>
        /// Creates a new rule from string
        /// </summary>
        /// <param name="ruleString">rule as string</param>
        public Rule(string ruleString)
        {
            Premises = new List<Fact>();
            Conclusions = new List<Fact>();

            ruleString = ruleString.Replace(" ", "");
            string[] factSets = ruleString.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
            List<string> premises = new List<string>();
            List<string> conclusions = new List<string>();
            if (factSets.Length == 2)
            {
                premises = new List<string>(factSets[0].Split(','));
                conclusions = new List<string>(factSets[1].Split(','));
            }

            foreach (string factString in premises)
            {
                Fact fact = new Fact(factString);
                Premises.Add(fact);
            }

            foreach (string factString in conclusions)
            {
                Fact fact = new Fact(factString);
                Conclusions.Add(fact);
            }
        }
    }
}
