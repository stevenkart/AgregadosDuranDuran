using Agregados.Reports;
using Agregados.Reports.Facts.FactFiltered;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Forms.Reports
{
    public partial class FrmFactsReports : Form
    {
        public FrmFactsReports()
        {
            InitializeComponent();
        }

        private void btnFiltrarFechas_Click(object sender, EventArgs e)
        {

            string fechaInicio = "2023-06-04";
            string fechaFin = "2023-06-06";

            using (FrmPrintFactFilter frm = new FrmPrintFactFilter(fechaInicio, fechaFin))
            {
                frm.ShowDialog();
            };
        }

        private void FrmFactsReports_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
        }
    }
}
