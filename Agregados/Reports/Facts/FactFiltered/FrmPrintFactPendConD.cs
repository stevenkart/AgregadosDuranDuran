using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Reports.Facts.FactFiltered
{
    public partial class FrmPrintFactPendConD : Form
    {
        public FrmPrintFactPendConD()
        {
            InitializeComponent();
        }

        private void FrmPrintFactPendConD_Load(object sender, EventArgs e)
        {
            RptFactPendConD rptFactPendConD = new RptFactPendConD();

            crystalReportViewer1.ReportSource = rptFactPendConD;
        }
    }
}
