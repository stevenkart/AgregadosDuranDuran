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
    public partial class FrmPrintTicket : Form
    {
        int IdFact;

        public FrmPrintTicket(int id)
        {
            InitializeComponent();
            IdFact = id;
        }
        private void FrmPrintTicket_Load(object sender, EventArgs e)
        {
            RptTicketCreated rptTicketCreated = new RptTicketCreated();
            rptTicketCreated.SetParameterValue("@IdTicket", IdFact);

            crystalReportViewer1.ReportSource = rptTicketCreated;
        }
    }
}
