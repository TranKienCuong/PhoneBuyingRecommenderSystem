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
    public class ConsultOptions
    {
        public static string[] GenderKeys = new string[] { "", "Male", "Female" };
        public static string[] HobbyKeys = new string[] { "SingingOrDancing", "PlayingSports", "ReadingBooksOrComics", "Traveling", "Shopping", "WatchingTV" };
        public static string[] MajorKeys = new string[] { "Journalism", "InformationTechnology", "Student", "EconomicOrFinance", "Marketing", "Art", "Agriculture", "Law", "BiologyOrPhysiology", "Pedagogy", "Psychology", "Sport", "BusinessOrCommerce", "Literary", "Sociology" };
        public static string[] DemandKeys = new string[] { "Chat", "PlayingGames", "PhotographingOrSelfie", "LinteningToMusic", "WatchingFilms", "ReadingOrWatchingNews" };

        public static string[] GenderValues = new string[] { "", "Nam", "Nữ" };
        public static string[] HobbyValues = new string[] { "Ca hát/Nhảy múa", "Chơi thể thao", "Đọc sách/truyện", "Du lịch", "Mua sắm", "Xem TV" };
        public static string[] MajorValues = new string[] { "Báo chí", "Công nghệ thông tin", "Học sinh/Sinh viên", "Kinh tế/Tài chính", "Marketing", "Nghệ thuật", "Nông nghiệp", "Pháp luật", "Sinh học/Y học", "Sư phạm", "Tâm lý học", "Thể thao", "Kinh doanh/Thương mại", "Văn học", "Xã hội học" };
        public static string[] DemandValues = new string[] { "Chat", "Chơi game", "Chụp hình/quay phim", "Nghe nhạc", "Xem phim", "Xem tin tức/thời sự" };

        public static string[] AgeValues = new string[] { "", "< 12", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "> 70" };

        public int GenderIndex = 0;
        public int AgeIndex = 0;
        public List<int> HobbyIndices = new List<int>();
        public List<int> MajorIndices = new List<int>();
        public List<int> DemandIndices = new List<int>();

        public int GenderScore = 1;
        public int AgeScore = 1;
        public Dictionary<int, int> HobbyScores = new Dictionary<int, int>();
        public Dictionary<int, int> MajorScores = new Dictionary<int, int>();
        public Dictionary<int, int> DemandScores = new Dictionary<int, int>();

        public ConsultOptions() { }

        public ConsultOptions(ConsultOptions options)
        {
            this.GenderIndex = options.GenderIndex;
            this.AgeIndex = options.AgeIndex;
            this.HobbyIndices = new List<int>(options.HobbyIndices);
            this.MajorIndices = new List<int>(options.MajorIndices);
            this.DemandIndices = new List<int>(options.DemandIndices);

            this.GenderScore = options.GenderScore;
            this.AgeScore = options.AgeScore;
            this.HobbyScores = new Dictionary<int, int>(options.HobbyScores);
            this.MajorScores = new Dictionary<int, int>(options.MajorScores);
            this.DemandScores = new Dictionary<int, int>(options.DemandScores);
        }

        public bool IsConsulting()
        {
            return (GenderIndex != 0 || AgeIndex != 0 || HobbyIndices.Count != 0 || MajorIndices.Count != 0 || DemandIndices.Count != 0);
        }
    }
}
