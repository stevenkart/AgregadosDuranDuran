using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Reports.Facts.TicketFiltered
{
    public partial class FrmTicketPendAll : Form
    {
        public FrmTicketPendAll()
        {
            InitializeComponent();
        }

        private void FrmTicketPendAll_Load(object sender, EventArgs e)
        {
            RptTicketPendAll rptTicketPendAll = new RptTicketPendAll();
            rptTicketPendAll.Refresh();
            crystalReportViewer1.ReportSource = rptTicketPendAll;
        }
    }
}
