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
    public partial class SuitableFactsForm : Form
    {
        public string ModelKey;

        public SuitableFactsForm(string ModelKey)
        {
            InitializeComponent();
            this.ModelKey = ModelKey;
        }

        private void SuitableFactsForm_Load(object sender, EventArgs e)
        {
            if (InferenceEngine.ModelFacts.ContainsKey(ModelKey))
            {
                foreach (Fact f in InferenceEngine.ModelFacts[ModelKey])
                {
                    factListBox.Items.Add(f.ToString());
                }
            }
        }
    }
}
