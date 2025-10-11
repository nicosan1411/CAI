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
        private int sortColumn = -1;

        public FormCuentaCorriente()
        {
            InitializeComponent();

            // Wire UI events here (keep designer file untouched)
            btnBuscar.Click += btnBuscar_Click;
            lvCuentasCorrientes.ColumnClick += lvCuentasCorrientes_ColumnClick;

            // Reduce flicker
            SetDoubleBuffered(lvCuentasCorrientes, true);
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            btnBuscar.Enabled = false;
            btnExportar.Enabled = false;
            try
            {
                DateTime desde = dtpDesde.Value.Date;
                DateTime hasta = dtpHasta.Value.Date;
                object clienteKey = cbCliente.SelectedItem; // adapt to your model

                // Run query off the UI thread
                var entries = await Task.Run(() => QueryEntries(clienteKey, desde, hasta));

                PopulateListView(entries);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnBuscar.Enabled = true;
                btnExportar.Enabled = true;
            }
        }

        // Replace this stub with real data access (repository/service)
        private List<AccountEntry> QueryEntries(object clienteKey, DateTime desde, DateTime hasta)
        {
            var list = new List<AccountEntry>();
            // Example static data (remove when wiring real data)
            list.Add(new AccountEntry { Cliente = "ACME", Fecha = DateTime.Today, NroComprobante = "001", Concepto = "Venta", Monto = 100m, Pago = 0m, Saldo = 100m });
            return list;
        }

        private void PopulateListView(List<AccountEntry> entries)
        {
            lvCuentasCorrientes.BeginUpdate();
            lvCuentasCorrientes.Items.Clear();

            foreach (var e in entries)
            {
                var item = new ListViewItem(e.Cliente ?? string.Empty);
                item.SubItems.Add(e.Fecha.ToShortDateString());
                item.SubItems.Add(e.NroComprobante ?? string.Empty);
                item.SubItems.Add(e.Concepto ?? string.Empty);
                item.SubItems.Add(e.Monto.ToString("C", CultureInfo.CurrentCulture));
                item.SubItems.Add(e.Pago.ToString("C", CultureInfo.CurrentCulture));
                item.SubItems.Add(e.Saldo.ToString("C", CultureInfo.CurrentCulture));
                lvCuentasCorrientes.Items.Add(item);
            }

            // Auto-size columns to content
            foreach (ColumnHeader ch in lvCuentasCorrientes.Columns)
                ch.Width = -2;

            lvCuentasCorrientes.EndUpdate();
        }

        private void SetDoubleBuffered(Control c, bool enabled)
        {
            // ListView.DoubleBuffered is protected — toggle via reflection
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(c, enabled, null);
        }

        private void lvCuentasCorrientes_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != sortColumn)
            {
                sortColumn = e.Column;
                lvCuentasCorrientes.Sorting = SortOrder.Ascending;
            }
            else
            {
                lvCuentasCorrientes.Sorting = lvCuentasCorrientes.Sorting == SortOrder.Ascending
                    ? SortOrder.Descending
                    : SortOrder.Ascending;
            }

            lvCuentasCorrientes.ListViewItemSorter = new ListViewItemComparer(sortColumn, lvCuentasCorrientes.Sorting);
            lvCuentasCorrientes.Sort();
        }

        // Simple comparer that tries date and numeric/currency comparison before falling back to string
        private class ListViewItemComparer : System.Collections.IComparer
        {
            private readonly int column;
            private readonly SortOrder order;
            private readonly CultureInfo culture = CultureInfo.CurrentCulture;

            public ListViewItemComparer(int column, SortOrder order)
            {
                this.column = column;
                this.order = order;
            }

            public int Compare(object x, object y)
            {
                var a = ((ListViewItem)x).SubItems[column].Text;
                var b = ((ListViewItem)y).SubItems[column].Text;

                DateTime da, db;
                if (DateTime.TryParse(a, culture, DateTimeStyles.None, out da) &&
                    DateTime.TryParse(b, culture, DateTimeStyles.None, out db))
                {
                    return order == SortOrder.Ascending ? DateTime.Compare(da, db) : DateTime.Compare(db, da);
                }

                decimal ma, mb;
                if (decimal.TryParse(a, NumberStyles.Currency, culture, out ma) &&
                    decimal.TryParse(b, NumberStyles.Currency, culture, out mb))
                {
                    return order == SortOrder.Ascending ? ma.CompareTo(mb) : mb.CompareTo(ma);
                }

                return order == SortOrder.Ascending
                    ? string.Compare(a, b, StringComparison.CurrentCulture)
                    : string.Compare(b, a, StringComparison.CurrentCulture);
            }
        }

        // Minimal DTO for example; replace with your domain model
        private class AccountEntry
        {
            public string Cliente { get; set; }
            public DateTime Fecha { get; set; }
            public string NroComprobante { get; set; }
            public string Concepto { get; set; }
            public decimal Monto { get; set; }
            public decimal Pago { get; set; }
            public decimal Saldo { get; set; }
        }

        // Add this method to the FormCuentaCorriente class to fix CS1061
        private void button2_Click(object sender, EventArgs e)
        {
            // TODO: Implement export logic here
            MessageBox.Show("Exportar button clicked.");
        }

        private void btnVolverMenuPrincipal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
