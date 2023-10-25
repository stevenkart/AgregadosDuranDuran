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
    public partial class FrmPrintTicket : Form
    {
        int Consecutivo;

        public FrmPrintTicket(int id)
        {
            InitializeComponent();
            Consecutivo = id;
        }
        private void FrmPrintTicket_Load(object sender, EventArgs e)
        {
            //RptTicketCreated rptTicketCreated = new RptTicketCreated();
            RptTicketCreatedDup rptTicketCreatedDup = new RptTicketCreatedDup();
            string reportPath = Path.GetFullPath(System.Configuration.ConfigurationManager.AppSettings["RptTicketCreatedDup"]);
            rptTicketCreatedDup.Load(reportPath);

            rptTicketCreatedDup.Refresh();
            //rptTicketCreated.SetParameterValue("@Consecutivo", Consecutivo);
            rptTicketCreatedDup.SetParameterValue("@Consecutivo", Consecutivo, rptTicketCreatedDup.Subreports[0].Name.ToString());
            rptTicketCreatedDup.SetParameterValue("@Consecutivo", Consecutivo, rptTicketCreatedDup.Subreports[1].Name.ToString());

            crystalReportViewer1.ReportSource = rptTicketCreatedDup;
        }
    }
}
