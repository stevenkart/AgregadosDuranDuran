using Agregados.Reports.Facts.FactFiltered;
using Agregados.Reports.Facts.FactNow;
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

namespace Agregados.Reports.Facts.TicketFiltered
{
    public partial class FrmTicketAnuladas : Form
    {
        string fechaInicio;
        string fechaFin;
        public FrmTicketAnuladas(string inicio, string fin)
        {
            InitializeComponent();
            fechaInicio = inicio;
            fechaFin = fin;
        }

        private void FrmTicketAnuladas_Load(object sender, EventArgs e)
        {
            RptTicketAnuladasAll rptTicketAnuladasAll = new RptTicketAnuladasAll();
            string reportPath = Path.GetFullPath(System.Configuration.ConfigurationManager.AppSettings["RptTicketAnuladasAll"]);
            rptTicketAnuladasAll.Load(reportPath);
      
            rptTicketAnuladasAll.Refresh();
            rptTicketAnuladasAll.SetParameterValue("@fechaInicio", fechaInicio);
            rptTicketAnuladasAll.SetParameterValue("@fechaFin", fechaFin);

            crystalReportViewer1.ReportSource = rptTicketAnuladasAll;
        }
    }
}
