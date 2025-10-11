using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormEmitirFactura : Form
    {
        public FormEmitirFactura()
        {
            InitializeComponent();
        }
        private void btnVolverMenuPrincipal_Click(object sender, EventArgs e)
        {
            FormUtils.VolverAlMenu(this);
        }
    }
}
