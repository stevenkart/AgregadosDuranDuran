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
    public partial class FrmPrintFactFilterSinDetalle : Form
    {

        string fechaInicio;
        string fechaFin;

        public FrmPrintFactFilterSinDetalle(string inicio, string fin)
        {
            InitializeComponent();
            fechaInicio = inicio;
            fechaFin = fin;
        }

        private void FrmPrintFactFilterSinDetalle_Load(object sender, EventArgs e)
        {
            RptFactFilteredSinDetalle rptFactFilteredSinDetalle = new RptFactFilteredSinDetalle();
            string reportPath = Path.GetFullPath(System.Configuration.ConfigurationManager.AppSettings["RptFactFilteredSinDetalle"]);
            rptFactFilteredSinDetalle.Load(reportPath);
            rptFactFilteredSinDetalle.Refresh();
            rptFactFilteredSinDetalle.SetParameterValue("@fechaInicio", fechaInicio);
            rptFactFilteredSinDetalle.SetParameterValue("@fechaFin", fechaFin);

            crystalReportViewer1.ReportSource = rptFactFilteredSinDetalle;
        }

    }
}
