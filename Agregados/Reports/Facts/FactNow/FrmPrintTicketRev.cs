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
    public partial class FrmPrintTicketRev : Form
    {

        int Consecutivo;


        public FrmPrintTicketRev( int id)
        {
            InitializeComponent();
            Consecutivo = id;
        }

        private void FrmPrintTicketRev_Load(object sender, EventArgs e)
        {
            //RptTicketProviderRev rptTicketProviderRev = new RptTicketProviderRev();
            RptTicketProviderRevDup rptTicketProviderRevDup = new RptTicketProviderRevDup();
            string reportPath = Path.GetFullPath(System.Configuration.ConfigurationManager.AppSettings["RptTicketProviderRevDup"]);
            rptTicketProviderRevDup.Load(reportPath);

            rptTicketProviderRevDup.Refresh();
            //rptTicketProviderRev.SetParameterValue("@Consecutivo", Consecutivo);
            rptTicketProviderRevDup.SetParameterValue("@Consecutivo", Consecutivo, rptTicketProviderRevDup.Subreports[0].Name.ToString());
            rptTicketProviderRevDup.SetParameterValue("@Consecutivo", Consecutivo, rptTicketProviderRevDup.Subreports[1].Name.ToString());




            crystalReportViewer1.ReportSource = rptTicketProviderRevDup;
        }
    }
}
