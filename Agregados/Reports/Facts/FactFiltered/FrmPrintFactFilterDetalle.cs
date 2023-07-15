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
            RptFactFilteredDetalle rptFactFiltered = new RptFactFilteredDetalle();
            rptFactFiltered.SetParameterValue("@fechaInicio", fechaInicio);
            rptFactFiltered.SetParameterValue("@fechaFin", fechaFin);

            crystalReportViewer1.ReportSource = rptFactFiltered;
        }
    }
}
