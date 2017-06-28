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
        public static string[] Manufacturers = new string[] { "", "Apple", "Samsung", "Sony", "LG", "Nokia", "Microsoft", "Freetel", "Obi Worldphone", "Oppo", "Asus", "HTC", "Lenovo", "Xiaomi", "Philips", "Wiko", "Meizu" };
        public static string[] Prices = new string[] { "", "< 1 triệu", "1 - 3 triệu", "3 - 7 triệu", "7 - 10 triệu", "10 - 15 triệu", "> 15 triệu" };
        public static string[] Materials = new string[] { "", "Nhựa", "Nhôm", "Hợp kim nhôm nguyên khối", "Kim loại", "Kim loại nguyên khối", "Nhựa và kim loại", "Kim loại và kính cường lực" };
        public static string[] Colors = new string[] { "", "Đen", "Bạc", "Vàng đồng", "Vàng hồng", "Trắng", "Đỏ", "Hồng", "Xanh dương", "Xanh lá", "Xám", "Cam" };
        public static string[] OSes = new string[] { "", "Android", "iOS", "Windows Phone" };
        public static string[] ScreenSizes = new string[] { "", "4.0 - 4.5 inch", "4.6 - 5.0 inch", "5.1 - 5.5 inch", "5.6 - 6.0 inch", "> 6.0 inch" };
        public static string[] FrontCams = new string[] { "", "< 2 MP", "2 - 5 MP", "5  - 8 MP", "8 - 12 MP", "12 - 16 MP", "> 16 MP" };
        public static string[] RearCams = new string[] { "", "< 2 MP", "2 - 5 MP", "5  - 8 MP", "8 - 12 MP", "12 - 16 MP", "> 16 MP" };
        public static string[] BatteryCapacities = new string[] { "", "< 1000 mAh", "1000 - 1500 mAh", "1500 - 2000 mAh", "2000 - 2500 mAh", "2500 - 3000 mAh", "> 3000 mAh" };
        public static string[] Storages = new string[] { "", "< 8 GB", "8 GB", "16 GB", "32 GB", "64 GB", "> 64 GB" };
        public static string[] RAMCapacities = new string[] { "", "< 1 GB", "1.0 GB", "1.5 GB", "2.0 GB", "3.0 GB", "4.0 GB", "> 4 GB" };
        public static string[] OtherFeatures = new string[] { "", "Hỗ trợ thẻ SD", "Camera kép", "Chống nước", "Bảo mật vân tay", "3D Touch" };

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
        public int OtherFeatureIndex = 0;

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

            if (MaterialIndex != 0)
            {
                string material = PhoneModel.ENGLISH[Materials[MaterialIndex]];
                pattern += ("?s ont:hasMaterial ?material. FILTER regex (?material , '" + material + "').");
            }

            if (ColorIndex != 0)
            {
                string color = PhoneModel.ENGLISH[Colors[ColorIndex]];
                pattern += ("?s ont:hasColor ?color. FILTER regex (?color , '" + color + "').");
            }

            if (OSIndex != 0)
            {
                string os = OSes[OSIndex];
                switch (OSIndex)
                {
                    case 1: pattern += ("?s ont:hasOS ?os. FILTER ( ?os > ont:" + os + " && ?os < ont:B"); break;
                    case 2: pattern += ("?s ont:hasOS ?os. FILTER ( ?os > ont:" + os + " && ?os < ont:j"); break;
                    case 3:
                        os = "Windows";
                        pattern += ("?s ont:hasOS ?os. FILTER ( ?os > ont:" + os + " && ?os < ont:X"); break;
                }
                pattern += ").";
            }

            if (ScreenSizeIndex != 0)
            {
                pattern += "?s ont:hasScreenSize ?screen. FILTER (?screen ";
                switch (ScreenSizeIndex)
                {
                    case 1: pattern += ">= 4.0 && ?screen <= 4.5"; break;
                    case 2: pattern += ">= 4.6 && ?screen <= 5.0"; break;
                    case 3: pattern += ">= 5.1 && ?screen <= 5.5"; break;
                    case 4: pattern += "> 5.5"; break;
                }
                pattern += ").";
            }

            if (FrontCamIndex != 0)
            {
                pattern += "?s ont:hasFrontMegapixel ?frontcam. FILTER (?frontcam";
                switch (FrontCamIndex)
                {
                    case 1: pattern += " < 2.0"; break;
                    case 2: pattern += ">= 2.0 && ?frontcam <= 5.0"; break;
                    case 3: pattern += ">= 5.0 && ?frontcam <= 8.0"; break;
                    case 4: pattern += ">= 8.0 && ?frontcam <= 12.0"; break;
                    case 5: pattern += ">= 12.0 && ?frontcam <= 16.0"; break;
                    case 6: pattern += "> 16.0"; break;
                }
                pattern += ").";
            }

            if (RearCamIndex != 0)
            {
                pattern += "?s ont:hasRearMegapixel ?rearcam. FILTER (?rearcam";
                switch (RearCamIndex)
                {
                    case 1: pattern += " < 2.0"; break;
                    case 2: pattern += ">= 2.0 && ?rearcam <= 5.0"; break;
                    case 3: pattern += ">= 5.0 && ?rearcam <= 8.0"; break;
                    case 4: pattern += ">= 8.0 && ?rearcam <= 12.0"; break;
                    case 5: pattern += ">= 12.0 && ?rearcam <= 16.0"; break;
                    case 6: pattern += "> 16.0"; break;
                }
                pattern += ").";
            }

            if (BatteryCapacityIndex != 0)
            {
                pattern += "?s ont:hasBatteryCapacity ?battery. FILTER (?battery";
                switch (BatteryCapacityIndex)
                {
                    case 1: pattern += " < 1000"; break;
                    case 2: pattern += ">= 1000 && ?battery <= 1500"; break;
                    case 3: pattern += ">= 1500 && ?battery <= 2000"; break;
                    case 4: pattern += ">= 2000 && ?battery <= 2500"; break;
                    case 5: pattern += ">= 2500 && ?battery <= 3000"; break;
                    case 6: pattern += "> 3000"; break;
                }
                pattern += ").";
            }

            if (StorageIndex != 0)
            {
                string storage = Storages[StorageIndex];
                if (StorageIndex == 1)
                    pattern += "?s ont:hasInternalStorageCapacity ?storage. FILTER (?storage < 8).";
                else if (StorageIndex == 6)
                    pattern += "?s ont:hasInternalStorageCapacity ?storage. FILTER (?storage > 64).";
                else if (StorageIndex == 2)
                    pattern += "?s ont:hasInternalStorageCapacity ?storage. FILTER (?storage = 8).";
                else
                    pattern += "?s ont:hasInternalStorageCapacity ?storage. FILTER (?storage = " + storage.Substring(0, 2) + ").";
            }

            if (RAMCapacityIndex != 0)
            {
                string ram = RAMCapacities[RAMCapacityIndex];
                if (RAMCapacityIndex == 1)
                    pattern += "?s ont:hasRAMCapacity ?ram. FILTER (?ram < 1.0).";
                else if (RAMCapacityIndex == RAMCapacities.Count() - 1)
                    pattern += "?s ont:hasRAMCapacity ?ram. FILTER (?ram > 4.0).";
                else
                    pattern += "?s ont:hasRAMCapacity ?ram. FILTER (?ram = " + ram.Substring(0, 3) + ").";
            }

            if (OtherFeatureIndex != 0)
            {
                string other = PhoneModel.ENGLISH[OtherFeatures[OtherFeatureIndex]];
                pattern += ("?s ont:hasOtherFeatures ?other. FILTER regex (?other, '" + other + "').");
            }

            return pattern;
        }
    }
}
