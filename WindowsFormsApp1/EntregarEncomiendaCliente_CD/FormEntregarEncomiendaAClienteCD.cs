using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.EntregaEnCD
{
    public partial class FormEntregarEncomiendaAClienteCD : Form
    {
        public FormEntregarEncomiendaAClienteCD()
        {
            InitializeComponent();
        }
        private void btnVolverMenuPrincipal_Click(object sender, EventArgs e)
        {
            FormUtils.VolverAlMenu(this);
        }
    }
}