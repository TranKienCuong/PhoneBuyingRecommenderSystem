using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBuyingRecommenderSystem
{
    /// <summary>
    /// Supports properties' values for UI. And one instance contains the selected properties for filtering the phones
    /// </summary>
    class FilterOptions
    {
        public static string[] Manufacturers = new string[] { "", "Apple", "Samsung", "Sony", "LG", "Nokia", "Microsoft", "Freetel", "Obi Worldphone", "Asus", "HTC", "Lenovo", "Xiaomi", "Philips", "Wiko", "Meizu" };
        public static string[] Prices = new string[] { "", "< 1 triệu", "1 - 3 triệu", "3 - 7 triệu", "7 - 10 triệu", "10 - 15 triệu", "> 15 triệu" };
        public static string[] Materials = new string[] { };
        public static string[] Colors = new string[] { };
        public static string[] OSes = new string[] { };
        public static string[] ScreenSizes = new string[] { };
        public static string[] FrontCams = new string[] { };
        public static string[] RearCams = new string[] { };
        public static string[] BatteryCapacities = new string[] { };
        public static string[] Storages = new string[] { };
        public static string[] RAMCapacities = new string[] { };

        public int ManufacturerIndex = 0;
        public int PriceIndex = 0;
        public int MaterialIndex = 0;
        public int ColorIndex = 0;
        public int OSIndex = 0;
        public int ScreenSizeIndex = 0;
        public int FrontCamIndex = 0;
        public int RearCamIndex = 0;
        public int BatteryCapacityIndex = 0;
        public int StorageIndex = 0;
        public int RAMCapacityIndex = 0;

        /// <summary>
        /// Returns the query pattern string to filter phone results. A single pattern has formed: "?s ont:has[Property] [Value]"
        /// </summary>
        /// <returns></returns>
        public string GetQueryPattern()
        {
            string pattern = "";

            if (ManufacturerIndex != 0)
            {
                string manufacturer = Manufacturers[ManufacturerIndex];
                if (ManufacturerIndex == 8)
                    manufacturer = "ObiWorldphone";
                pattern += ("?s ont:hasManufacturer ont:" + manufacturer + ".");
            }

            if (PriceIndex != 0)
            {
                pattern += "?s ont:hasPrice ?price. FILTER (?price ";
                switch (PriceIndex)
                {
                    case 1: pattern += "< 1000000"; break;
                    case 2: pattern += ">= 1000000 && ?price <= 3000000"; break;
                    case 3: pattern += ">= 3000000 && ?price <= 7000000"; break;
                    case 4: pattern += ">= 7000000 && ?price <= 10000000"; break;
                    case 5: pattern += ">= 10000000 && ?price <= 15000000"; break;
                    case 6: pattern += "> 15000000"; break;
                }
                pattern += ").";
            }

            // so on...

            return pattern;
        }
    }
}
