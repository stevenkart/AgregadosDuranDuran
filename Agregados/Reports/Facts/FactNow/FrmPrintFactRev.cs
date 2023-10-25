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

namespace Agregados.Reports.Facts.FactNow
{
    public partial class FrmPrintFactRev : Form
    {
        int Consecutivo;

        public FrmPrintFactRev(int id)
        {
            InitializeComponent();

            Consecutivo = id;
        }

        private void FrmPrintFactRev_Load(object sender, EventArgs e)
        {
            //RptFactRev rptFactRev = new RptFactRev();
            RptFactRevDup rptFactRevDup = new RptFactRevDup();
            string reportPath = Path.GetFullPath(System.Configuration.ConfigurationManager.AppSettings["RptFactRevDup"]);
            rptFactRevDup.Load(reportPath);

            rptFactRevDup.Refresh();
            //rptFactRevDup.SetParameterValue("@Consecutivo", Consecutivo);
            rptFactRevDup.SetParameterValue("@Consecutivo", Consecutivo, rptFactRevDup.Subreports[0].Name.ToString());
            rptFactRevDup.SetParameterValue("@Consecutivo", Consecutivo, rptFactRevDup.Subreports[1].Name.ToString());

            crystalReportViewer1.ReportSource = rptFactRevDup;
        }
    }
}
