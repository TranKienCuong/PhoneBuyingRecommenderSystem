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
using VDS.RDF.Query;

namespace PhoneBuyingRecommenderSystem
{
    public partial class MainForm : Form
    {
        PhoneModel phone;
        FilterOptions filterOptions = new FilterOptions();
        ConsultOptions consultOptions = new ConsultOptions();

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
                ChoosePhone(models.ElementAt(0).Key);
            }
            else
            {
                noPhoneLabel.Visible = true;
                phonePanel.Visible = false;
            }
        }

        void UpdatePhones()
        {
            Dictionary<string, string> filterModels = SearchEngine.SearchProperties(filterOptions);
            List<KeyValuePair<string, int>> consultModels = InferenceEngine.DoConsult(consultOptions, filterModels);

            List<KeyValuePair<string, string>> finalModels = new List<KeyValuePair<string, string>>();
            foreach (var model in consultModels)
            {
                string modelKey = model.Key;
                string modelName = filterModels[modelKey];
                KeyValuePair<string, string> finalModel = new KeyValuePair<string, string>(modelKey, modelName);
                finalModels.Add(finalModel);
            }
            ShowPhones(finalModels);
        }

        void ResetAllPhones()
        {
            ShowPhones(PhoneModel.GetAllModels());
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
        }

        void ChoosePhone(string modelKey)
        {
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
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SPARQL.Start();
            InferenceEngine.LoadRules();
            manufacComboBox.Items.AddRange(FilterOptions.Manufacturers);
            priceComboBox.Items.AddRange(FilterOptions.Prices);
            // so on...
            ResetAllPhones();

            ////test
            //SparqlResultSet results = SPARQL.DoQuery(@"
            //    PREFIX ont: <http://www.co-ode.org/ontologies/ont.owl#>
            //    PREFIX swrl: <http://www.w3.org/2003/11/swrl#>
            //    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
            //    SELECT * WHERE 
            //    { 
            //        ?rule a swrl:Imp.
                    
            //        ?rule swrl:body ?body.
            //        ?body rdf:first ?bfirst1.
            //        ?bfirst1 swrl:propertyPredicate ?bprop1.
            //        ?bfirst1 swrl:argument1 ?barg11.
            //        ?bfirst1 swrl:argument2 ?barg12.
            //    }");
            //foreach (SparqlResult result in results)
            //    foreach (string s in result.Variables)
            //        Console.WriteLine(s + ": " + result.Value(s));
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
                ChoosePhone(phoneListView.SelectedItems[0].Tag.ToString());
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
    }
}
