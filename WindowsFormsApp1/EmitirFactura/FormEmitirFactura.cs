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

        private void btnvolvermenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AdjustListViewColumns();
            // Keep columns adjusted if the ListView is resized (e.g., form resize)
            this.lvFacturasClientes.SizeChanged += (s, ev) => AdjustListViewColumns();
        }

        private void AdjustListViewColumns()
        {
            // Desired minimal widths (tweak these if you prefer other proportions)
            const int minNroGuia = 90;
            const int minMonto = 70;
            const int minConcepto = 80;

            int total = lvFacturasClientes.ClientSize.Width;

            // Reserve fixed widths for N° Guía and Monto, give remainder to Concepto
            int colNro = minNroGuia;
            int colMonto = minMonto;
            int colConcepto = total - colNro - colMonto;

            // If not enough space, reduce columns but respect minimal widths
            if (colConcepto < minConcepto)
            {
                colConcepto = minConcepto;
                int remaining = total - colConcepto;
                // Split remaining between the other two, but don't go below their minima
                int half = Math.Max(minNroGuia, remaining / 2);
                colNro = Math.Min(half, remaining - minMonto);
                if (colNro < minNroGuia) colNro = minNroGuia;
                colMonto = Math.Max(minMonto, remaining - colNro);
            }

            // Final safety checks
            if (colNro < minNroGuia) colNro = minNroGuia;
            if (colMonto < minMonto) colMonto = minMonto;
            if (colConcepto < minConcepto) colConcepto = minConcepto;

            // Apply widths
            columnNroGuia.Width = colNro;
            columnMonto.Width = colMonto;
            columnConcepto.Width = Math.Max(0, total - colNro - colMonto); // ensure no negative width
        }
    }
}
