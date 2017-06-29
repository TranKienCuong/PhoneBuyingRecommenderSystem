using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBuyingRecommenderSystem
{
    class Fuzzy
    {
        public Fuzzy() { }

        public void findMax(List<float> l, ref int t)
        {
            float max = -1;
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i] > max)
                {
                    max = l[i];
                    t = i;
                }
            }
        }
        public Dictionary<string,float> DoTuoi(float x)
        {
            Dictionary<string, float> temper = new Dictionary<string, float>();
            int _temp = -1;
            float ThieuNhi = -1, ThieuNien = -1, ThanhNien = -1, TrungNien = -1, NguoiGia = -1;
            //Do tuoi Thieu nhi
            if (x <= 10)
                ThieuNhi = 1;
            else if (10 < x && x < 14)
                ThieuNhi = (float)((x - 14.0) / 4.0);
            else if (x >= 14)
                ThieuNhi = 0;
            temper["ThieuNhi"] = ThieuNhi;
            //Do tuoi thieu nien
            if (x <= 9)
                ThieuNien = 0;
            else if (9 < x && x < 13)
                ThieuNien = (float)((x - 9.0) / 4.0);
            else if (13 <= x && x <= 18)
                ThieuNien = 1;
            else if (18 <= x && x <= 22)
                ThieuNien = (float)((x - 22.0) / 4.0);
            else if (22 <= x)
                ThieuNien = 0;
            temper["ThieuNien"] = ThieuNien;
            //Do tuoi Thanh nien
            if (x <= 17)
                ThanhNien = 0;
            else if (17 < x && x < 20)
                ThanhNien = (float)((x - 17.0) / 3.0);
            else if (20 <= x && x <= 29)
                ThanhNien = 1;
            else if (29 < x && x < 35)
                ThanhNien = (float)((35.0 - x) / 6.0);
            else if (35 <= x)
                ThanhNien = 0;
            temper["ThanhNien"] = ThanhNien;
            //Do tuoi Trung nien
            if (x <= 30)
                TrungNien = 0;
            else if (30 < x && x < 37)
                TrungNien = (float)((x - 30.0) / 7.0);
            else if (37 <= x && x <= 53)
                TrungNien = 1;
            else if (53 < x && x < 65)
                TrungNien = (float)((65.0 - x) / 12.0);
            else if (65 <= x)
                TrungNien = 0;
            temper["TrungNien"] = TrungNien;
            //Do tuoi Nguoi gia
            if (x <= 60)
                NguoiGia = 0;
            else if (60 < x && x < 71)
                NguoiGia = (float)-1/5 + (float)(x / 12.0);
            else if (71 <= x)
                NguoiGia = 1;
            temper["NguoiGia"] = NguoiGia;

            var mylist = temper.ToList();
            mylist.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            

            Dictionary<string, float> FuzzyAge = new Dictionary<string, float>();
            FuzzyAge[mylist[4].Key] = mylist[4].Value;
            FuzzyAge[mylist[3].Key] = mylist[3].Value;

            return FuzzyAge;
            //MessageBox.Show(mylist[4].Key.ToString() + " va " + mylist[3].Key.ToString());
        }

        public void ScreenSize(float x, ref string output)
        {
            List<float> temper = new List<float>();
            int _temp = -1;
            float Mini = -1, Nho = -1, TrungBinh = -1, Lon = -1;
            //Screen size Mini
            if (x <= 2.8)
                Mini = 1;
            else if (2.8 < x && x < 5)
                Mini = 25 / 11 - (5 / 11) * x;
            else if (5 <= x)
                Mini = 0;
            temper.Add(Mini);
            //Screen size Nho
            if (x <= 2.3)
                Nho = 0;
            else if (2.3 < x && x < 4)
                Nho = (float)0.5882352941 * x - (float)1.352941176;
            else if (4 <= x && x <= 4.5)
                Nho = 1;
            else if (4.5 < x && x < 5)
                Nho = 10 - 2 * x;
            else if (x >= 5)
                Nho = 0;
            temper.Add(Nho);
            //Screen size Trung binh
            if (x <= 4)
                TrungBinh = 0;
            else if (4 < x && x < 4.5)
                TrungBinh = -8 + 2 * x;
            else if (4.5 <= x && x <= 5.2)
                TrungBinh = 1;
            else if (5.3 < x && x < 5.5)
                TrungBinh = 55 / 3 - (10 / 3) * x;
            else if (x >= 5.5)
                TrungBinh = 0;
            temper.Add(TrungBinh);
            //Screen size Lon
            if (x <= 5)
                Lon = 0;
            else if (5 < x && x < 5.7)
                Lon = - (float)7.142857145 + (float)1.428571429 * x;
            else if (5.7 <= x)
                Lon = 1;
            temper.Add(Lon);

            findMax(temper, ref _temp);
            switch (_temp)
            {
                case 0: output = "Mini"; break;
                case 1: output = "Nho"; break;
                case 2: output = "Trung binh"; break;
                case 3: output = "Lon"; break;
            }
        }

    }
}
