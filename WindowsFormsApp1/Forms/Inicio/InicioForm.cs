using CAI_Proyecto.Almacenes.Almacen;
using CAI_Proyecto.Almacenes.Entidad;
using CAI_Proyecto.Forms.Consultas.CuentaCorriente.Forms;
using CAI_Proyecto.Forms.Consultas.EstadoGuia.Forms;
using CAI_Proyecto.Forms.Consultas.ResultadosCostosVentas.Forms;
using CAI_Proyecto.Forms.Operacion.AdmitirEnCD.Forms;
using CAI_Proyecto.Forms.Operacion.EmitirFactura.Forms;
using CAI_Proyecto.Forms.Operacion.EntregarEncomiendaClienteCD.Forms;
using CAI_Proyecto.Forms.Operacion.ImponerCallCenter.Forms;
using CAI_Proyecto.Forms.Operacion.RecepcionarMicros.Forms;
using CAI_Proyecto.Forms.Operacion.RendirEncomienda.Forms;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CAI_Proyecto.Forms.Inicio
{
    public partial class InicioForm : Form
    {
        public InicioForm()
        {
            InitializeComponent();
        }

        private void OpenAsDialog(Form form)
        {
            form.StartPosition = FormStartPosition.CenterParent;

            this.Hide();

            try
            {
                form.ShowDialog(this);
            }
            finally
            {
                this.Show();
                this.Activate();
            }
        }

        public static void VolverAlMenu(Form formularioActual)
        {
            var result = MessageBox.Show(
                "¿Deseás volver al menú principal?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (result == DialogResult.Yes)
                formularioActual.Close();
        }

        private void btnImposicionCallCenter_Click(object sender, EventArgs e)
        {
            OpenAsDialog(new ImponerCallCenterForm());
        }

        private void btnAdmisionCD_Click(object sender, EventArgs e)
        {
            OpenAsDialog(new AdmitirEnCDForm());
        }

        private void btnRendicionPrimeraMilla_Click(object sender, EventArgs e)
        {
            OpenAsDialog(new RendirEncomiendasForm());
        }

        private void btnForm4_Click(object sender, EventArgs e)
        {
            OpenAsDialog(new RecepcionarMicrosForm());
        }

        private void btnEmitirFactura_Click(object sender, EventArgs e)
        {
            OpenAsDialog(new EmitirFacturaForm());
        }

        private void btnEstadoGuia_Click(object sender, EventArgs e)
        {
            OpenAsDialog(new EstadoGuiaForm());
        }

        private void btnResultadosCostosVentas_Click(object sender, EventArgs e)
        {
            OpenAsDialog(new ResultadosCostosVentasForm());
        }

        private void btnCuentaCorriente_Click(object sender, EventArgs e)
        {
            OpenAsDialog(new CuentaCorrienteForm());
        }

        private void btnEntregasEnCD_Click(object sender, EventArgs e)
        {
            OpenAsDialog(new EntregarEncomiendaAClienteCDForm());
        }

        public static void ConfigureListView(ListView lv, bool withCheckboxes)
        {
            lv.View = View.Details;
            lv.CheckBoxes = withCheckboxes;
            lv.FullRowSelect = true;
            lv.MultiSelect = false;
            lv.Items.Clear();
        }

        private void InicioForm_Load(object sender, EventArgs e)
        {
            CDActualCombo.DisplayMember = "Nombre";
            CDActualCombo.Items.AddRange(CentroDeDistribucionAlmacen.CentrosDeDistribucion.OrderBy(cd => cd.Nombre).ToArray());

            AgenciaActualCombo.DisplayMember = "Nombre";
            AgenciaActualCombo.Items.AddRange(AgenciaAlmacen.Agencias.OrderBy(a => a.Nombre).ToArray());
        }

        private void CDActualCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CentroDeDistribucionAlmacen.CentroDeDistribucionActual = (CentroDeDistribucionEntidad)CDActualCombo.SelectedItem;
        }

        private void AgenciaActualCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AgenciaAlmacen.AgenciaActual = (AgenciaEntidad)AgenciaActualCombo.SelectedItem;
        }
    }
}