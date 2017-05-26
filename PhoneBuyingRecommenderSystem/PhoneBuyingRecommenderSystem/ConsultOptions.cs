using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBuyingRecommenderSystem
{
    /// <summary>
    /// Supports customer's information values for UI. And one instance contains the selected information for consulting
    /// </summary>
    class ConsultOptions
    {
        public static string[] Gender = new string[] { "", "Nam", "Nữ" };
        public static string[] Age = new string[] { };
        public static string[] Hobbies = new string[] { };
        public static string[] Majors = new string[] { };

        public int GenderIndex = 0;
        public int AgeIndex = 0;
        public int HobbyIndex = 0;
        public int MajorIndex = 0;
    }
}
