using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Reports.Caja
{
    public partial class FrmCajaPorFecha : Form
    {
        string inicio;
        string fin;
        public FrmCajaPorFecha(string pInicio, string pFin)
        {
            InitializeComponent();
            inicio = pInicio;
            fin = pFin;
        }

        private void FrmCajaPorFecha_Load(object sender, EventArgs e)
        {
            RptCajaPorFecha rptCajaPorFecha = new RptCajaPorFecha();
            rptCajaPorFecha.Refresh();
            rptCajaPorFecha.SetParameterValue("@fechaInicio", inicio);
            rptCajaPorFecha.SetParameterValue("@fechaFin", fin);

            crystalReportViewer1.ReportSource = rptCajaPorFecha;
        }
    }
}
