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

        void HighlightPhones(int from, int to, Color color)
        {
            for (int i = from; i <= to; i++)
                phoneListView.Items[i].BackColor = color;
        }

        void UpdatePhones()
        {
            if (ignoreUpdate)
                return;

            Dictionary<string, string> filterModels = SearchEngine.SearchProperties(filterOptions);
            List<KeyValuePair<string, int>> consultModels = InferenceEngine.DoConsult(consultOptions, filterModels);

            List<KeyValuePair<string, string>> finalModels = new List<KeyValuePair<string, string>>();
            int highlightCount = 0;
            foreach (var model in consultModels)
            {
                string modelKey = model.Key;
                string modelName = filterModels[modelKey];
                KeyValuePair<string, string> finalModel = new KeyValuePair<string, string>(modelKey, modelName);
                finalModels.Add(finalModel);

                if (model.Value != 0 && model.Value == InferenceEngine.KnownCount)
                    highlightCount++;
            }
            ShowPhones(finalModels);
            HighlightPhones(0, highlightCount - 1, Color.GreenYellow);
            if (highlightCount != 0)
                suggestedLabel.Visible = true;
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

            ignoreUpdate = false;
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

            if (item.BackColor == Color.GreenYellow)
                suggestedLabel.Visible = true;
            else
                suggestedLabel.Visible = false;
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

        private void genderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            consultOptions.GenderIndex = genderComboBox.SelectedIndex;
            UpdatePhones();
        }

        private void hobbyCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            consultOptions.HobbyIndices = hobbyCheckedListBox.CheckedIndices.Cast<int>().ToList();
            UpdatePhones();
        }

        private void majorCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            consultOptions.MajorIndices = majorCheckedListBox.CheckedIndices.Cast<int>().ToList();
            UpdatePhones();
        }
    }
}
