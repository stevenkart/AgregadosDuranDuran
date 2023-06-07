using Agregados.Reports.Facts.FactNow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Reports.Facts.FactFiltered
{
    public partial class FrmPrintFactFilter : Form
    {

        string fechaInicio;
        string fechaFin;


        public FrmPrintFactFilter(string inicio, string fin)
        {
            InitializeComponent();
            fechaInicio = inicio;
            fechaFin = fin;



        }

        private void FrmPrintFactFilter_Load(object sender, EventArgs e)
        {
            RptFactFiltered rptFactFiltered = new RptFactFiltered();
            rptFactFiltered.SetParameterValue("@FechaInicio", fechaInicio);
            rptFactFiltered.SetParameterValue("@FechaFin", fechaFin);

            crystalReportViewer1.ReportSource = rptFactFiltered;
        }
    }
}
