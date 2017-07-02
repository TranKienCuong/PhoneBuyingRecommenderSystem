namespace PhoneBuyingRecommenderSystem
{
    partial class AdvancedConsultForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedConsultForm));
            this.demandCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.demandLabel = new System.Windows.Forms.Label();
            this.ageComboBox = new System.Windows.Forms.ComboBox();
            this.majorCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.hobbyCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.genderComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.consultPanel = new System.Windows.Forms.Panel();
            this.scoreListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.scoreNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.consultPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scoreNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // demandCheckedListBox
            // 
            this.demandCheckedListBox.CheckOnClick = true;
            this.demandCheckedListBox.FormattingEnabled = true;
            this.demandCheckedListBox.Location = new System.Drawing.Point(474, 68);
            this.demandCheckedListBox.Name = "demandCheckedListBox";
            this.demandCheckedListBox.Size = new System.Drawing.Size(215, 180);
            this.demandCheckedListBox.TabIndex = 11;
            this.demandCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.demandCheckedListBox_ItemCheck);
            // 
            // demandLabel
            // 
            this.demandLabel.AutoSize = true;
            this.demandLabel.Location = new System.Drawing.Point(479, 44);
            this.demandLabel.Name = "demandLabel";
            this.demandLabel.Size = new System.Drawing.Size(75, 21);
            this.demandLabel.TabIndex = 10;
            this.demandLabel.Text = "Nhu cầu";
            // 
            // ageComboBox
            // 
            this.ageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ageComboBox.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ageComboBox.FormattingEnabled = true;
            this.ageComboBox.Location = new System.Drawing.Point(525, 8);
            this.ageComboBox.Name = "ageComboBox";
            this.ageComboBox.Size = new System.Drawing.Size(110, 27);
            this.ageComboBox.TabIndex = 9;
            this.ageComboBox.SelectedIndexChanged += new System.EventHandler(this.ageComboBox_SelectedIndexChanged);
            // 
            // majorCheckedListBox
            // 
            this.majorCheckedListBox.CheckOnClick = true;
            this.majorCheckedListBox.FormattingEnabled = true;
            this.majorCheckedListBox.Location = new System.Drawing.Point(9, 68);
            this.majorCheckedListBox.Name = "majorCheckedListBox";
            this.majorCheckedListBox.Size = new System.Drawing.Size(228, 180);
            this.majorCheckedListBox.TabIndex = 7;
            this.majorCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.majorCheckedListBox_ItemCheck);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(249, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Giới tính";
            // 
            // hobbyCheckedListBox
            // 
            this.hobbyCheckedListBox.CheckOnClick = true;
            this.hobbyCheckedListBox.FormattingEnabled = true;
            this.hobbyCheckedListBox.Location = new System.Drawing.Point(243, 68);
            this.hobbyCheckedListBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.hobbyCheckedListBox.Name = "hobbyCheckedListBox";
            this.hobbyCheckedListBox.Size = new System.Drawing.Size(225, 180);
            this.hobbyCheckedListBox.TabIndex = 0;
            this.hobbyCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.hobbyCheckedListBox_ItemCheck);
            // 
            // genderComboBox
            // 
            this.genderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.genderComboBox.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.genderComboBox.FormattingEnabled = true;
            this.genderComboBox.Location = new System.Drawing.Point(328, 7);
            this.genderComboBox.Name = "genderComboBox";
            this.genderComboBox.Size = new System.Drawing.Size(110, 27);
            this.genderComboBox.TabIndex = 5;
            this.genderComboBox.SelectedIndexChanged += new System.EventHandler(this.genderComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(249, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sở thích";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(180, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "Lĩnh vực/Nghề nghiệp";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(479, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "Tuổi";
            // 
            // consultPanel
            // 
            this.consultPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(240)))), ((int)(((byte)(230)))), ((int)(((byte)(140)))));
            this.consultPanel.Controls.Add(this.demandCheckedListBox);
            this.consultPanel.Controls.Add(this.demandLabel);
            this.consultPanel.Controls.Add(this.ageComboBox);
            this.consultPanel.Controls.Add(this.majorCheckedListBox);
            this.consultPanel.Controls.Add(this.label2);
            this.consultPanel.Controls.Add(this.hobbyCheckedListBox);
            this.consultPanel.Controls.Add(this.genderComboBox);
            this.consultPanel.Controls.Add(this.label1);
            this.consultPanel.Controls.Add(this.label4);
            this.consultPanel.Controls.Add(this.label3);
            this.consultPanel.Location = new System.Drawing.Point(12, 12);
            this.consultPanel.Name = "consultPanel";
            this.consultPanel.Size = new System.Drawing.Size(702, 262);
            this.consultPanel.TabIndex = 4;
            // 
            // scoreListView
            // 
            this.scoreListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.scoreListView.FullRowSelect = true;
            this.scoreListView.Location = new System.Drawing.Point(12, 280);
            this.scoreListView.Name = "scoreListView";
            this.scoreListView.Size = new System.Drawing.Size(401, 234);
            this.scoreListView.TabIndex = 5;
            this.scoreListView.UseCompatibleStateImageBehavior = false;
            this.scoreListView.View = System.Windows.Forms.View.Details;
            this.scoreListView.SelectedIndexChanged += new System.EventHandler(this.scoreListView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Các giá trị đã chọn";
            this.columnHeader1.Width = 232;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Mức độ ưu tiên";
            this.columnHeader2.Width = 128;
            // 
            // scoreNumericUpDown
            // 
            this.scoreNumericUpDown.Location = new System.Drawing.Point(423, 403);
            this.scoreNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.scoreNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.scoreNumericUpDown.Name = "scoreNumericUpDown";
            this.scoreNumericUpDown.Size = new System.Drawing.Size(120, 27);
            this.scoreNumericUpDown.TabIndex = 6;
            this.scoreNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.scoreNumericUpDown.ValueChanged += new System.EventHandler(this.scoreNumericUpDown_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(419, 379);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 21);
            this.label5.TabIndex = 7;
            this.label5.Text = "Điều chỉnh mức độ";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(419, 280);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(295, 72);
            this.label6.TabIndex = 8;
            this.label6.Text = "Mức độ ưu tiên thể hiện độ quan trọng của một yếu tố. Mức độ càng lớn thì độ quan" +
    " trọng càng cao";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(515, 472);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(88, 42);
            this.OKButton.TabIndex = 9;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(626, 472);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(88, 42);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // AdvancedConsultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 526);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.scoreNumericUpDown);
            this.Controls.Add(this.scoreListView);
            this.Controls.Add(this.consultPanel);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AdvancedConsultForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tư vấn nâng cao";
            this.Load += new System.EventHandler(this.AdvancedConsultForm_Load);
            this.consultPanel.ResumeLayout(false);
            this.consultPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scoreNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox demandCheckedListBox;
        private System.Windows.Forms.Label demandLabel;
        private System.Windows.Forms.ComboBox ageComboBox;
        private System.Windows.Forms.CheckedListBox majorCheckedListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox hobbyCheckedListBox;
        private System.Windows.Forms.ComboBox genderComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel consultPanel;
        private System.Windows.Forms.ListView scoreListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.NumericUpDown scoreNumericUpDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button cancelButton;
    }
}