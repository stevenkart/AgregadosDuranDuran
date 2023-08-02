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

namespace Agregados.Reports.Facts.FactFiltered
{
    public partial class FrmPrintFactFilterDetalle : Form
    {

        string fechaInicio;
        string fechaFin;


        public FrmPrintFactFilterDetalle(string inicio, string fin)
        {
            InitializeComponent();
            fechaInicio = inicio;
            fechaFin = fin;



        }

        private void FrmPrintFactFilterDetalle_Load(object sender, EventArgs e)
        {
            RptFactFilteredDetalle rptFactFilteredDetalle = new RptFactFilteredDetalle();
            string reportPath = Path.GetFullPath(System.Configuration.ConfigurationManager.AppSettings["RptFactFilteredDetalle"]);
            rptFactFilteredDetalle.Load(reportPath);

            rptFactFilteredDetalle.Refresh();
            rptFactFilteredDetalle.SetParameterValue("@fechaInicio", fechaInicio);
            rptFactFilteredDetalle.SetParameterValue("@fechaFin", fechaFin);

            crystalReportViewer1.ReportSource = rptFactFilteredDetalle;
        }
    }
}
