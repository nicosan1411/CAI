using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1; // Ensure this using directive is present if FormCuentaCorriente is in the same namespace

namespace WindowsFormsApp1
{
    public partial class FormCuentaCorriente : Form
    {
        public FormCuentaCorriente()
        {
            InitializeComponent();
        }

        // Add this method to the FormCuentaCorriente class to fix CS1061
        private void button2_Click(object sender, EventArgs e)
        {
            // TODO: Implement export logic here
            MessageBox.Show("Exportar button clicked.");
        }

        private void btnVolverAlMenuPrincipal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
