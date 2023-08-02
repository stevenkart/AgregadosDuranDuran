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
            RptTicketCreated rptTicketCreated = new RptTicketCreated();
            string reportPath = Path.GetFullPath(System.Configuration.ConfigurationManager.AppSettings["RptTicketCreated"]);
            rptTicketCreated.Load(reportPath);
           
            rptTicketCreated.Refresh();
            rptTicketCreated.SetParameterValue("@Consecutivo", Consecutivo);

            crystalReportViewer1.ReportSource = rptTicketCreated;
        }
    }
}
