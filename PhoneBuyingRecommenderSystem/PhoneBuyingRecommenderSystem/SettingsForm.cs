using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PhoneBuyingRecommenderSystem
{
    public partial class SettingsForm : Form
    {
        StreamReader reader;
        StreamWriter writer;
        bool adding = true;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            reader = new StreamReader("Rules.txt");
            while (!reader.EndOfStream)
            {
                string s = reader.ReadLine();
                ruleListBox.Items.Add(s);
            }
            reader.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            adding = true;
            changePanel.Visible = true;
            changeLabel.Text = "Thêm luật:";
            ruleTextBox.Text = "";
        }

        private void modifyButton_Click(object sender, EventArgs e)
        {
            if (ruleListBox.SelectedIndices.Count == 0)
            {
                noticeLabel.Text = "Xin hãy chọn trước luật cần sửa!";
                return;
            }
            adding = false;
            changePanel.Visible = true;
            changeLabel.Text = "Chỉnh sửa luật:";
            ruleTextBox.Text = (string)ruleListBox.SelectedItem;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (ruleListBox.SelectedIndices.Count == 0)
            {
                noticeLabel.Text = "Xin hãy chọn trước luật cần xóa!";
                return;
            }
            ruleListBox.Items.RemoveAt(ruleListBox.SelectedIndex);
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            changePanel.Visible = false;
            if (adding)
            {
                ruleListBox.Items.Add(ruleTextBox.Text);
            }
            else
            {
                ruleListBox.Items[ruleListBox.SelectedIndex] = ruleTextBox.Text;
            }
            noticeLabel.Text = "";
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            writer = new StreamWriter("Rules.txt");
            foreach (string s in ruleListBox.Items)
                writer.WriteLine(s);
            writer.Close();

            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void ruleListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (adding || ruleListBox.SelectedIndices.Count == 0)
            {
                ruleTextBox.Text = "";
                return;
            }
            ruleTextBox.Text = (string)ruleListBox.SelectedItem;
        }
    }
}
