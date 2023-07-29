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
            RptFactFilteredSinDetalle rptFactFiltered = new RptFactFilteredSinDetalle();
            rptFactFiltered.Refresh();
            rptFactFiltered.SetParameterValue("@fechaInicio", fechaInicio);
            rptFactFiltered.SetParameterValue("@fechaFin", fechaFin);

            crystalReportViewer1.ReportSource = rptFactFiltered;
        }

    }
}
