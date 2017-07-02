namespace PhoneBuyingRecommenderSystem
{
    partial class SuitableFactsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SuitableFactsForm));
            this.factListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // factListBox
            // 
            this.factListBox.FormattingEnabled = true;
            this.factListBox.ItemHeight = 20;
            this.factListBox.Location = new System.Drawing.Point(18, 19);
            this.factListBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.factListBox.Name = "factListBox";
            this.factListBox.Size = new System.Drawing.Size(440, 424);
            this.factListBox.TabIndex = 0;
            // 
            // SuitableFactsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 474);
            this.Controls.Add(this.factListBox);
            this.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SuitableFactsForm";
            this.Text = "Các yếu tố phù hợp";
            this.Load += new System.EventHandler(this.SuitableFactsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox factListBox;
    }
}