using Agregados.Reports.Facts.FactFiltered;
using Agregados.Reports.Facts.FactNow;
using CrystalDecisions.Windows.Forms;
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
            //RptFactCreated rptFactCreated = new RptFactCreated();
            RptFactCreateDup RptFactCreateDup = new RptFactCreateDup();
            string reportPath = Path.GetFullPath(System.Configuration.ConfigurationManager.AppSettings["RptFactCreateDup"]);
            RptFactCreateDup.Load(reportPath);

            RptFactCreateDup.Refresh();
            RptFactCreateDup.SetParameterValue("@Consecutivo", Consecutivo, RptFactCreateDup.Subreports[0].Name.ToString());
            RptFactCreateDup.SetParameterValue("@Consecutivo", Consecutivo, RptFactCreateDup.Subreports[1].Name.ToString());
            crystalReportViewer1.ReportSource = RptFactCreateDup;

        }


    }
}
