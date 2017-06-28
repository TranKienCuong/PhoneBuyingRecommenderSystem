using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace PhoneBuyingRecommenderSystem
{
    public partial class MainForm : Form
    {
        PhoneModel phone;
        FilterOptions filterOptions = new FilterOptions();
        ConsultOptions consultOptions = new ConsultOptions();
        bool ignoreUpdate = false;
        bool ignoreCheckEvent = false;
        Dictionary<string, float> ModelScorePercentage = new Dictionary<string, float>();

        public MainForm()
        {
            InitializeComponent();
        }

        void ShowPhones(IEnumerable<KeyValuePair<string, string>> models)
        {
            phoneListView.Clear();
            foreach (var model in models)
            {
                ListViewItem item = phoneListView.Items.Add(model.Value);
                item.Tag = model.Key;
                if (model.Key.StartsWith("iPhone"))
                    item.ImageKey = model.Key.Split('-')[0] + ".jpg";
                else
                    item.ImageKey = model.Key + ".jpg";
            }
            if (models.Count() != 0)
            {
                noPhoneLabel.Visible = false;
                phonePanel.Visible = true;
                ChoosePhone(phoneListView.Items[0]);
            }
            else
            {
                noPhoneLabel.Visible = true;
                phonePanel.Visible = false;
            }
        }

        Color GetHighlightColor(Color color, float percentage)
        {
            int R, G, B;
            if (percentage == 0)
                R = G = B = 255;
            else
            {
                float scale = 1 / percentage;

                R = (int)(color.R * scale);
                if (R > 255) R = 255;
                G = (int)(color.G * scale);
                if (G > 255) G = 255;
                B = (int)(color.B * scale);
                if (B > 255) B = 255;
            }
            return Color.FromArgb(R, G, B);
        }

        void HighlightPhones(Color maxColor)
        {
            foreach (ListViewItem item in phoneListView.Items)
            {
                float scorePercentage = ModelScorePercentage[item.Tag.ToString()];
                item.BackColor = GetHighlightColor(maxColor, scorePercentage);
            }
        }

        void UpdatePhones()
        {
            if (ignoreUpdate)
                return;

            Dictionary<string, string> filterModels = SearchEngine.SearchProperties(filterOptions);
            List<KeyValuePair<string, int>> consultModels = InferenceEngine.DoConsult(consultOptions, filterModels);

            List<KeyValuePair<string, string>> finalModels = new List<KeyValuePair<string, string>>();
            foreach (var model in consultModels)
            {
                string modelKey = model.Key;
                string modelName = filterModels[modelKey];
                KeyValuePair<string, string> finalModel = new KeyValuePair<string, string>(modelKey, modelName);
                finalModels.Add(finalModel);

                int score = model.Value;
                int maxScore = InferenceEngine.MaxScore;
                if (maxScore == 0)
                    ModelScorePercentage[modelKey] = 0;
                else
                    ModelScorePercentage[modelKey] = (float)score / maxScore;
            }
            ShowPhones(finalModels);
            HighlightPhones(Color.GreenYellow);
        }

        void ResetAllPhones()
        {
            ShowPhones(PhoneModel.GetAllModels());

            ignoreUpdate = true;

            manufacComboBox.SelectedIndex = 0;
            priceComboBox.SelectedIndex = 0;
            materialComboBox.SelectedIndex = 0;
            colorComboBox.SelectedIndex = 0;
            OSComboBox.SelectedIndex = 0;
            screenSizeComboBox.SelectedIndex = 0;
            frontCamComboBox.SelectedIndex = 0;
            rearCamComboBox.SelectedIndex = 0;
            batteryComboBox.SelectedIndex = 0;
            storageComboBox.SelectedIndex = 0;
            RAMComboBox.SelectedIndex = 0;
            otherFeaturesComboBox.SelectedIndex = 0;

            genderComboBox.SelectedIndex = 0;
            ageComboBox.SelectedIndex = 0;
            foreach (int i in hobbyCheckedListBox.CheckedIndices)
                hobbyCheckedListBox.SetItemChecked(i, false);
            foreach (int i in majorCheckedListBox.CheckedIndices)
                majorCheckedListBox.SetItemChecked(i, false);
            foreach (int i in demandCheckedListBox.CheckedIndices)
                demandCheckedListBox.SetItemChecked(i, false);

            ignoreUpdate = false;

            suggestedLabel.Visible = false;
            suitableButton.Visible = false;
        }

        void ChoosePhone(ListViewItem item)
        {
            string modelKey = item.Tag.ToString();
            string modelImage = modelKey;
            if (modelKey.StartsWith("iPhone"))
                modelImage = modelKey.Split('-')[0];
            phonePictureBox.Image = (Image)Properties.Resources.ResourceManager.GetObject(modelImage);

            phone = new PhoneModel(modelKey);
            phoneNameLabel.Text = phone.Name;
            priceLabel.Text = "Giá: " + phone.Price;
            batteryLabel.Text = "Dung lượng pin: " + phone.BatteryCapacity;
            screenSizeLabel.Text = "Kích thước màn hình: " + phone.ScreenSize;
            resolutionLabel.Text = "Độ phân giải: " + phone.Resolution;
            colorLabel.Text = "Màu sắc: " + phone.Color;
            OSLabel.Text = "HĐH: " + phone.OS;
            CPULabel.Text = "CPU: " + phone.CPU;
            RAMLabel.Text = "RAM: " + phone.RAMCapacity;
            storageLabel.Text = "Bộ nhớ trong: " + phone.StorageCapacity;
            frontCamLabel.Text = "Camera trước: " + phone.FrontCamera;
            rearCamLabel.Text = "Camera sau: " + phone.RearCamera;
            materialLabel.Text = "Chất liệu: " + phone.Material;
            otherfeaturesLabel.Text = "Các tính năng khác: " + phone.OtherFeatures;

            if (consultOptions.IsConsulting() && ModelScorePercentage.ContainsKey(modelKey))
            {
                float scorePercentage = ModelScorePercentage[modelKey];
                Color color = GetHighlightColor(Color.GreenYellow, scorePercentage);
                if (scorePercentage >= 0.5)
                {
                    suggestedLabel.Visible = true;
                    suggestedLabel.BackColor = color;
                }
                else
                    suggestedLabel.Visible = false;

                suitableButton.Visible = true;
                suitableButton.BackColor = color;
                suitableButton.Text = "Mức độ phù hợp:\r\n" + ((int)(scorePercentage * 100)).ToString() + " %";
            }
            else
            {
                suggestedLabel.Visible = false;
                suitableButton.Visible = false;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SPARQL.Start();
            InferenceEngine.LoadRules();

            manufacComboBox.Items.AddRange(FilterOptions.Manufacturers);
            priceComboBox.Items.AddRange(FilterOptions.Prices);
            materialComboBox.Items.AddRange(FilterOptions.Materials);
            colorComboBox.Items.AddRange(FilterOptions.Colors);
            OSComboBox.Items.AddRange(FilterOptions.OSes);
            screenSizeComboBox.Items.AddRange(FilterOptions.ScreenSizes);
            frontCamComboBox.Items.AddRange(FilterOptions.FrontCams);
            rearCamComboBox.Items.AddRange(FilterOptions.RearCams);
            batteryComboBox.Items.AddRange(FilterOptions.BatteryCapacities);
            storageComboBox.Items.AddRange(FilterOptions.Storages);
            RAMComboBox.Items.AddRange(FilterOptions.RAMCapacities);
            otherFeaturesComboBox.Items.AddRange(FilterOptions.OtherFeatures);

            genderComboBox.Items.AddRange(ConsultOptions.GenderValues);
            ageComboBox.Items.AddRange(ConsultOptions.AgeValues);
            hobbyCheckedListBox.Items.AddRange(ConsultOptions.HobbyValues);
            majorCheckedListBox.Items.AddRange(ConsultOptions.MajorValues);
            demandCheckedListBox.Items.AddRange(ConsultOptions.DemandValues);

            ResetAllPhones();

            ////test
            //SparqlResultSet results = SPARQL.DoQuery(@"
            //    PREFIX ont: <http://www.co-ode.org/ontologies/ont.owl#>
            //    SELECT ?model WHERE 
            //    { 
            //        ?s a ont:PhoneModel. BIND (STRAFTER(STR(?s), STR(ont:)) AS ?model).
            //        ?s ont:hasColor ?color.
            //        FILTER regex(?color, 'Black|Gold|Pink').
            //    }");

            //foreach (var result in results)
            //{
            //    string modelKey = result.Value("model").ToString();
            //    Console.WriteLine(modelKey);
            //}
        }

        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchButton.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            ShowPhones(SearchEngine.SearchName(searchTextBox.Text));
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetAllPhones();
        }

        private void phoneListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (phoneListView.SelectedItems.Count != 0)
                ChoosePhone(phoneListView.SelectedItems[0]);
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(phone.Link);
        }

        private void manufacComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterOptions.ManufacturerIndex = manufacComboBox.SelectedIndex;
            UpdatePhones();
        }

        private void priceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterOptions.PriceIndex = priceComboBox.SelectedIndex;
            UpdatePhones();
        }

        private void screenSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterOptions.ScreenSizeIndex = screenSizeComboBox.SelectedIndex;
            UpdatePhones();
        }

        private void materialComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterOptions.MaterialIndex = materialComboBox.SelectedIndex;
            UpdatePhones();
        }

        private void colorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterOptions.ColorIndex = colorComboBox.SelectedIndex;
            UpdatePhones();
        }

        private void OSComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterOptions.OSIndex = OSComboBox.SelectedIndex;
            UpdatePhones();
        }

        private void frontCamComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterOptions.FrontCamIndex = frontCamComboBox.SelectedIndex;
            UpdatePhones();
        }

        private void rearCamComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterOptions.RearCamIndex = rearCamComboBox.SelectedIndex;
            UpdatePhones();
        }

        private void batteryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterOptions.BatteryCapacityIndex = batteryComboBox.SelectedIndex;
            UpdatePhones();
        }

        private void storageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterOptions.StorageIndex = storageComboBox.SelectedIndex;
            UpdatePhones();
        }

        private void RAMComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterOptions.RAMCapacityIndex = RAMComboBox.SelectedIndex;
            UpdatePhones();
        }

        private void otherFeaturesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterOptions.OtherFeatureIndex = otherFeaturesComboBox.SelectedIndex;
            UpdatePhones();
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            new SettingsForm().ShowDialog();
        }

        private void consultLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AdvancedConsultForm form = new AdvancedConsultForm(consultOptions);
            if (form.ShowDialog() == DialogResult.OK)
            {
                consultOptions = form.consultOptions;

                ignoreCheckEvent = true;
                foreach (int i in hobbyCheckedListBox.CheckedIndices)
                    hobbyCheckedListBox.SetItemChecked(i, false);
                foreach (int i in majorCheckedListBox.CheckedIndices)
                    majorCheckedListBox.SetItemChecked(i, false);
                foreach (int i in demandCheckedListBox.CheckedIndices)
                    demandCheckedListBox.SetItemChecked(i, false);

                genderComboBox.SelectedIndex = consultOptions.GenderIndex;
                ageComboBox.SelectedIndex = consultOptions.AgeIndex;
                foreach (int i in consultOptions.HobbyIndices)
                    hobbyCheckedListBox.SetItemChecked(i, true);
                foreach (int i in consultOptions.MajorIndices)
                    majorCheckedListBox.SetItemChecked(i, true);
                foreach (int i in consultOptions.DemandIndices)
                    demandCheckedListBox.SetItemChecked(i, true);
                ignoreCheckEvent = false;

                UpdatePhones();
            }
        }

        private void genderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreCheckEvent)
                return;
            consultOptions.GenderIndex = genderComboBox.SelectedIndex;
            consultOptions.GenderScore = 1;
            UpdatePhones();
        }

        private void ageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreCheckEvent)
                return;
            consultOptions.AgeIndex = ageComboBox.SelectedIndex;
            consultOptions.AgeScore = 1;
            UpdatePhones();
        }

        private void majorCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (ignoreCheckEvent)
                return;
            if (e.NewValue == CheckState.Checked)
            {
                consultOptions.MajorIndices.Add(e.Index);
                consultOptions.MajorScores[e.Index] = 1;
            }
            else
            {
                consultOptions.MajorIndices.Remove(e.Index);
                consultOptions.MajorScores.Remove(e.Index);
            }
            UpdatePhones();
        }

        private void hobbyCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (ignoreCheckEvent)
                return;
            if (e.NewValue == CheckState.Checked)
            {
                consultOptions.HobbyIndices.Add(e.Index);
                consultOptions.HobbyScores[e.Index] = 1;
            }
            else
            {
                consultOptions.HobbyIndices.Remove(e.Index);
                consultOptions.HobbyScores.Remove(e.Index);
            }
            UpdatePhones();
        }

        private void demandCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (ignoreCheckEvent)
                return;
            if (e.NewValue == CheckState.Checked)
            {
                consultOptions.DemandIndices.Add(e.Index);
                consultOptions.DemandScores[e.Index] = 1;
            }
            else
            {
                consultOptions.DemandIndices.Remove(e.Index);
                consultOptions.DemandScores.Remove(e.Index);
            }
            UpdatePhones();
        }

        private void suitableButton_Click(object sender, EventArgs e)
        {
            new SuitableFactsForm(phone.ModelKey).ShowDialog();
        }
    }
}
