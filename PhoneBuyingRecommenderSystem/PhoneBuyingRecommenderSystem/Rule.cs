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
        public List<Fact> Premises;
        public List<Fact> Conclusions;

        public Rule() { }
        
        public Rule(List<Fact> premises, List<Fact> conclusions)
        {
            Premises = premises;
            Conclusions = conclusions;
        }

        public Rule(string ruleString)
        {

        }
    }
}
