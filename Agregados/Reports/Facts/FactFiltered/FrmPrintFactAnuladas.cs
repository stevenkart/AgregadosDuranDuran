using Agregados.Reports.Caja;
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
    public partial class FrmPrintFactAnuladas : Form
    {
        string fechaInicio;
        string fechaFin;
        public FrmPrintFactAnuladas(string inicio, string fin)
        {
            InitializeComponent();
            fechaInicio = inicio;
            fechaFin = fin;
        }

        private void FrmPrintFactAnuladas_Load(object sender, EventArgs e)
        {
            RptFactsAnuladas rptFactsAnuladas = new RptFactsAnuladas();
            string reportPath = Path.GetFullPath(System.Configuration.ConfigurationManager.AppSettings["RptFactAnuladas"]);
            rptFactsAnuladas.Load(reportPath);
 
            rptFactsAnuladas.Refresh();
            rptFactsAnuladas.SetParameterValue("@fechaInicio", fechaInicio);
            rptFactsAnuladas.SetParameterValue("@fechaFin", fechaFin);

            crystalReportViewer1.ReportSource = rptFactsAnuladas;
        }
    }
}
