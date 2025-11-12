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

            // Agencia: inicialmente deshabilitada y vacía
            AgenciaActualCombo.DisplayMember = "Nombre";
            AgenciaActualCombo.Enabled = false;
            AgenciaActualCombo.Items.Clear();
        }

        private void CDActualCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cdSeleccionado = (CentroDeDistribucionEntidad)CDActualCombo.SelectedItem;
            CentroDeDistribucionAlmacen.CentroDeDistribucionActual = cdSeleccionado;

            AgenciaActualCombo.BeginUpdate();
            try
            {
                // Refrescar items según CD elegido
                AgenciaActualCombo.Items.Clear();
                if (cdSeleccionado != null)
                {
                    var agenciasFiltradas = AgenciaAlmacen.Agencias
                        .Where(a => string.Equals(a.CDAsignado, cdSeleccionado.IdCD, StringComparison.OrdinalIgnoreCase))
                        .OrderBy(a => a.Nombre)
                        .ToArray();

                    AgenciaActualCombo.Items.AddRange(agenciasFiltradas);
                }

                // Siempre resetear la selección de agencia
                AgenciaActualCombo.SelectedIndex = -1;
                AgenciaAlmacen.AgenciaActual = null;

                // Habilitar solo si hay un CD seleccionado (independiente de si hay agencias o no)
                AgenciaActualCombo.Enabled = cdSeleccionado != null;
            }
            finally
            {
                AgenciaActualCombo.EndUpdate();
            }
        }

        private void AgenciaActualCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!AgenciaActualCombo.Enabled || AgenciaActualCombo.SelectedIndex < 0)
            {
                AgenciaAlmacen.AgenciaActual = null;
                return;
            }

            AgenciaAlmacen.AgenciaActual = (AgenciaEntidad)AgenciaActualCombo.SelectedItem;
        }
    }
}