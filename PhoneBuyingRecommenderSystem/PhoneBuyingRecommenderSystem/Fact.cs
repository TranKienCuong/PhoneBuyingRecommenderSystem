using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBuyingRecommenderSystem
{
    /// <summary>
    /// A fact in rule-based system
    /// </summary>
    class Fact
    {
        public string Name { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }

        public Fact() { }

        public Fact(string name, string op, string value)
        {
            Name = name;
            Operator = op;
            Value = value;
        }

        public Fact(string factString)
        {

        }
    }
}
