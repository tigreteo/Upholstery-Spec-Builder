using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Upholstery_Builder
{
    public partial class Procedure_Description : Form
    {
        public string TheValue
        { get { return richTextBox1.Text; } }

        public Procedure_Description()
        {
            InitializeComponent();
        }

        private void AddUpholProcedure_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


    }


}
