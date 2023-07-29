using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            RptTicketProviderRev rptTicketProviderRev = new RptTicketProviderRev();
            rptTicketProviderRev.Refresh();
            rptTicketProviderRev.SetParameterValue("@Consecutivo", Consecutivo);
            //rptTicketProviderRev.Refresh();



            crystalReportViewer1.ReportSource = rptTicketProviderRev;
        }
    }
}
