using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBuyingRecommenderSystem
{
    public partial class AdvancedConsultForm : Form
    {
        public ConsultOptions consultOptions;
        bool ignoreCheck = false;

        public AdvancedConsultForm(ConsultOptions consultOptions)
        {
            InitializeComponent();
            this.consultOptions = new ConsultOptions(consultOptions);

            genderComboBox.Items.AddRange(ConsultOptions.GenderValues);
            ageComboBox.Items.AddRange(ConsultOptions.AgeValues);
            hobbyCheckedListBox.Items.AddRange(ConsultOptions.HobbyValues);
            majorCheckedListBox.Items.AddRange(ConsultOptions.MajorValues);
            demandCheckedListBox.Items.AddRange(ConsultOptions.DemandValues);
        }

        private ListViewItem FindItemByTag(string tag)
        {
            foreach (ListViewItem item in scoreListView.Items)
            {
                if ((string)item.Tag == tag)
                {
                    return item;
                }
            }
            return null;
        }

        private int GetScore(ListViewItem item)
        {
            return int.Parse(item.SubItems[1].Text);
        }

        private void AdvancedConsultForm_Load(object sender, EventArgs e)
        {
            ignoreCheck = true;

            int i = consultOptions.GenderIndex;
            if (i != 0)
            {
                genderComboBox.SelectedIndex = i;
                ListViewItem item = new ListViewItem(new string[] { ConsultOptions.GenderValues[i], consultOptions.GenderScore.ToString() });
                item.Tag = ("Gender");
                scoreListView.Items.Add(item);
            }

            i = consultOptions.AgeIndex;
            if (i != 0)
            {
                ageComboBox.SelectedIndex = i;
                ListViewItem item = new ListViewItem(new string[] { ConsultOptions.AgeValues[i], consultOptions.AgeScore.ToString() });
                item.Tag = ("Age");
                scoreListView.Items.Add(item);
            }

            foreach (int j in consultOptions.MajorIndices)
            {
                majorCheckedListBox.SetItemChecked(j, true);
                ListViewItem item = new ListViewItem(new string[] { ConsultOptions.MajorValues[j], consultOptions.MajorScores[j].ToString() });
                item.Tag = ("Major_" + j.ToString());
                scoreListView.Items.Add(item);
            }

            foreach (int j in consultOptions.HobbyIndices)
            {
                hobbyCheckedListBox.SetItemChecked(j, true);
                ListViewItem item = new ListViewItem(new string[] { ConsultOptions.HobbyValues[j], consultOptions.HobbyScores[j].ToString() });
                item.Tag = ("Hobby_" + j.ToString());
                scoreListView.Items.Add(item);
            }

            foreach (int j in consultOptions.DemandIndices)
            {
                demandCheckedListBox.SetItemChecked(j, true);
                ListViewItem item = new ListViewItem(new string[] { ConsultOptions.DemandValues[j], consultOptions.DemandScores[j].ToString() });
                item.Tag = ("Demand_" + j.ToString());
                scoreListView.Items.Add(item);
            }

            ignoreCheck = false;
        }

        private void genderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreCheck)
                return;
            int i = consultOptions.GenderIndex = genderComboBox.SelectedIndex;
            ListViewItem item = FindItemByTag("Gender");
            if (i == 0)
            {
                scoreListView.Items.Remove(item);
                return;
            }
            if (item == null)
            {
                item = new ListViewItem(new string[] { ConsultOptions.GenderValues[i], consultOptions.GenderScore.ToString() });
                item.Tag = ("Gender");
                scoreListView.Items.Add(item);
            }
            else
            {
                item.Text = ConsultOptions.GenderValues[i];
            }
        }

        private void ageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreCheck)
                return;
            int i = consultOptions.AgeIndex = ageComboBox.SelectedIndex;
            ListViewItem item = FindItemByTag("Age");
            if (i == 0)
            {
                scoreListView.Items.Remove(item);
                return;
            }
            if (item == null)
            {
                item = new ListViewItem(new string[] { ConsultOptions.AgeValues[i], consultOptions.AgeScore.ToString() });
                item.Tag = ("Age");
                scoreListView.Items.Add(item);
            }
            else
            {
                item.Text = ConsultOptions.AgeValues[i];
            }
        }

        private void majorCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (ignoreCheck)
                return;
            if (e.NewValue == CheckState.Checked)
            {
                consultOptions.MajorIndices.Add(e.Index);
                consultOptions.MajorScores[e.Index] = 1;
                ListViewItem item = new ListViewItem(new string[] { ConsultOptions.MajorValues[e.Index], consultOptions.MajorScores[e.Index].ToString() });
                item.Tag = ("Major_" + e.Index.ToString());
                scoreListView.Items.Add(item);
            }
            else
            {
                consultOptions.MajorIndices.Remove(e.Index);
                consultOptions.MajorScores.Remove(e.Index);
                scoreListView.Items.Remove(FindItemByTag("Major_" + e.Index.ToString()));
            }
        }

        private void hobbyCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (ignoreCheck)
                return;
            if (e.NewValue == CheckState.Checked)
            {
                consultOptions.HobbyIndices.Add(e.Index);
                consultOptions.HobbyScores[e.Index] = 1;
                ListViewItem item = new ListViewItem(new string[] { ConsultOptions.HobbyValues[e.Index], consultOptions.HobbyScores[e.Index].ToString() });
                item.Tag = ("Hobby_" + e.Index.ToString());
                scoreListView.Items.Add(item);
            }
            else
            {
                consultOptions.HobbyIndices.Remove(e.Index);
                consultOptions.HobbyScores.Remove(e.Index);
                scoreListView.Items.Remove(FindItemByTag("Hobby_" + e.Index.ToString()));
            }
        }

        private void demandCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (ignoreCheck)
                return;
            if (e.NewValue == CheckState.Checked)
            {
                consultOptions.DemandIndices.Add(e.Index);
                consultOptions.DemandScores[e.Index] = 1;
                ListViewItem item = new ListViewItem(new string[] { ConsultOptions.DemandValues[e.Index], consultOptions.DemandScores[e.Index].ToString() });
                item.Tag = ("Demand_" + e.Index.ToString());
                scoreListView.Items.Add(item);
            }
            else
            {
                consultOptions.DemandIndices.Remove(e.Index);
                consultOptions.DemandScores.Remove(e.Index);
                scoreListView.Items.Remove(FindItemByTag("Demand_" + e.Index.ToString()));
            }
        }

        private void scoreNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (scoreListView.SelectedItems.Count == 0)
                return;
            int score = (int)scoreNumericUpDown.Value;
            scoreListView.SelectedItems[0].SubItems[1].Text = score.ToString();
            string[] strs = scoreListView.SelectedItems[0].Tag.ToString().Split('_');
            string kind = strs[0];
            int index = 0;
            if (kind != "Gender" && kind != "Age")
                index = int.Parse(strs[1]);
            switch (kind)
            {
                case "Gender":
                    consultOptions.GenderScore = score;
                    break;
                case "Age":
                    consultOptions.AgeScore = score;
                    break;
                case "Major":
                    consultOptions.MajorScores[index] = score;
                    break;
                case "Hobby":
                    consultOptions.HobbyScores[index] = score;
                    break;
                case "Demand":
                    consultOptions.DemandScores[index] = score;
                    break;
            }
        }

        private void scoreListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (scoreListView.SelectedItems.Count == 0)
                return;
            scoreNumericUpDown.Value = GetScore(scoreListView.SelectedItems[0]);
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
