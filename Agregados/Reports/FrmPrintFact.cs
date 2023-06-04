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
        Facturas fact;

       
        private decimal Subtotal;
        private decimal IVA;
        private decimal Total;
        private string NombreCliente;
        private int MaterialCod;
        private string Material;
        private string TipoFact;
        private string Metodo;
        private string Empleado;
        private string Estado;



        int IdFact;

        public FrmPrintFact(int id)
        {
            InitializeComponent();

            IdFact = id;
        }

        private void FrmPrintFact_Load(object sender, EventArgs e)
        {
            RptFactCreated rptFactCreated = new RptFactCreated();
            rptFactCreated.SetParameterValue("@IdFactura", IdFact);
 
            crystalReportViewer1.ReportSource = rptFactCreated;

        }


    }
}
