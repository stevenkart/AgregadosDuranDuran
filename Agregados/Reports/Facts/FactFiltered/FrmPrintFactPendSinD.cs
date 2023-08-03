using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Reports.Facts.FactFiltered
{
    public partial class FrmPrintFactPendSinD : Form
    {
        public FrmPrintFactPendSinD()
        {
            InitializeComponent();
        }

        private void FrmPrintFactPendSinD_Load(object sender, EventArgs e)
        {
            RptFactPendSinD rptFactPendSinD = new RptFactPendSinD();
            string reportPath = Path.GetFullPath(System.Configuration.ConfigurationManager.AppSettings["RptFactPendSinD"]);
            rptFactPendSinD.Load(reportPath);
      
            rptFactPendSinD.Refresh();
            crystalReportViewer1.ReportSource = rptFactPendSinD;
        }
    }
}
