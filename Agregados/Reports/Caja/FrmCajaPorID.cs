using Agregados.Reports.Facts.FactNow;
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

namespace Agregados.Reports.Caja
{
    public partial class FrmCajaPorID : Form
    {
        int ID;
        public FrmCajaPorID(int Id)
        {
            InitializeComponent();
            ID = Id;
        }

        private void FrmCajaPorID_Load(object sender, EventArgs e)
        {
            RptCajaPorID rptCajaPorID = new RptCajaPorID();
            string reportPath = Path.GetFullPath(System.Configuration.ConfigurationManager.AppSettings["RptCajaPorID"]);
            rptCajaPorID.Load(reportPath);
           
            rptCajaPorID.Refresh();
            rptCajaPorID.SetParameterValue("@ID", ID);

            crystalReportViewer1.ReportSource = rptCajaPorID;
        }
    }
}
