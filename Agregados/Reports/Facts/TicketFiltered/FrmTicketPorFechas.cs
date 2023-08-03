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
    public partial class FrmTicketPorFechas : Form
    {
        string fechaInicio;
        string fechaFin;
        public FrmTicketPorFechas(string inicio, string fin)
        {
            InitializeComponent();
            fechaInicio = inicio;
            fechaFin = fin;
        }

        private void FrmTicketPorFechas_Load(object sender, EventArgs e)
        {
            RptTickesPorFechas rptTickesPorFechas = new RptTickesPorFechas();
            string reportPath = Path.GetFullPath(System.Configuration.ConfigurationManager.AppSettings["RptTickesPorFechas"]);
            rptTickesPorFechas.Load(reportPath);
    
            rptTickesPorFechas.Refresh();
            rptTickesPorFechas.SetParameterValue("@fechaInicio", fechaInicio);
            rptTickesPorFechas.SetParameterValue("@fechaFin", fechaFin);

            crystalReportViewer1.ReportSource = rptTickesPorFechas;
        }
    }
}
