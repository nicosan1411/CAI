using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormCuentaCorriente : Form
    {
        public FormCuentaCorriente()
        {
            InitializeComponent();
        }

        // Falta código por realizar

        private void btnVolverMenuPrincipal_Click(object sender, EventArgs e)
        {
            FormUtils.VolverAlMenu(this);
        }
    }
}
