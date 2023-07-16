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
    public partial class FrmPrintFactFilterAll : Form
    {
        string fechaInicio;
        string fechaFin;
        public FrmPrintFactFilterAll(string inicio, string fin)
        {
            InitializeComponent();

            fechaInicio = inicio;
            fechaFin = fin;
        }

        private void FrmPrintFactFilterAll_Load(object sender, EventArgs e)
        {
            RptPrintFactFilterAll rptFactFiltered = new RptPrintFactFilterAll();
            rptFactFiltered.Refresh();
            rptFactFiltered.SetParameterValue("@fechaInicio", fechaInicio);
            rptFactFiltered.SetParameterValue("@fechaFin", fechaFin);

            crystalReportViewer1.ReportSource = rptFactFiltered;
        }



    }
}
