using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Reports.Facts.FactNow
{
    public partial class FrmPrintTicketRev : Form
    {

        int Consecutivo;


        public FrmPrintTicketRev( int id)
        {
            InitializeComponent();
            Consecutivo = id;
        }

        private void FrmPrintFact_Load(object sender, EventArgs e)
        {
            RptTicketProviderRev rptTicketProviderRev = new RptTicketProviderRev();
            rptTicketProviderRev.SetParameterValue("@Consecutivo", Consecutivo);

            /*
            rptFactCreated.SetParameterValue("@Cantidad", null);
            rptFactCreated.SetParameterValue("@Precio", null);
            rptFactCreated.SetParameterValue("@Subtotal", null);
            rptFactCreated.SetParameterValue("@IVA", null);
            rptFactCreated.SetParameterValue("@Total", null);
            rptFactCreated.SetParameterValue("@NombreMaterial", null);
            rptFactCreated.SetParameterValue("@IdMaterial", null);
            */

            crystalReportViewer1.ReportSource = rptTicketProviderRev;

        }
    }
}
