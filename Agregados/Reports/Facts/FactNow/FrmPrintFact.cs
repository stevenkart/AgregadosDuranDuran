using Agregados.Reports.Facts.FactNow;
using CrystalDecisions.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Reports
{
    public partial class FrmPrintFact : Form
    {
        int Consecutivo;

        public FrmPrintFact(int id)
        {
            InitializeComponent();

            Consecutivo = id;
        }

        private void FrmPrintFact_Load(object sender, EventArgs e)
        {
            RptFactCreated rptFactCreated = new RptFactCreated();
            rptFactCreated.SetParameterValue("@Consecutivo", Consecutivo);

            /*
            rptFactCreated.SetParameterValue("@Cantidad", null);
            rptFactCreated.SetParameterValue("@Precio", null);
            rptFactCreated.SetParameterValue("@Subtotal", null);
            rptFactCreated.SetParameterValue("@IVA", null);
            rptFactCreated.SetParameterValue("@Total", null);
            rptFactCreated.SetParameterValue("@NombreMaterial", null);
            rptFactCreated.SetParameterValue("@IdMaterial", null);
            */

            crystalReportViewer1.ReportSource = rptFactCreated;

        }


    }
}
