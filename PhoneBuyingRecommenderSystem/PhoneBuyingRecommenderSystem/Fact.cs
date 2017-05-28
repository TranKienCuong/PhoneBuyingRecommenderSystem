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

        /// <summary>
        /// Creates a new fact
        /// </summary>
        public Fact() { }

        /// <summary>
        /// Creates a new fact from string
        /// </summary>
        /// <param name="factString">fact as string</param>
        public Fact(string factString)
        {
            string[] strs = factString.Split(new string[] { ">=", "<=", ">", "<", "=" }, StringSplitOptions.RemoveEmptyEntries);
            Name = strs[0];
            Value = strs[1];
            Operator = factString.Replace(Name, "").Replace(Value, "");
        }

        public override bool Equals(object obj)
        {
            Fact f = (Fact)obj;
            return (Name == f.Name) && (Operator == f.Operator) && (Value == f.Value);
        }

        public override int GetHashCode()
        {
            return (Name + Operator + Value).GetHashCode();
        }
    }
}
