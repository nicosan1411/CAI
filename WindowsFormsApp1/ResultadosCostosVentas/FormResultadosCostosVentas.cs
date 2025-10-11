using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormResultadosCostosVentas : Form
    {
        public FormResultadosCostosVentas()
        {
            InitializeComponent();
        }

        private void btnVolverMenuPrincipal_Click_1(object sender, EventArgs e)
        {
            FormUtils.VolverAlMenu(this);
        }
    }
}
