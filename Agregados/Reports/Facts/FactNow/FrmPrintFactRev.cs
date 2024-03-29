﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Reports.Facts.FactNow
{
    public partial class FrmPrintFactRev : Form
    {
        int Consecutivo;

        public FrmPrintFactRev(int id)
        {
            InitializeComponent();

            Consecutivo = id;
        }

        private void FrmPrintFactRev_Load(object sender, EventArgs e)
        {
            RptFactRev rptFactRev = new RptFactRev();
            string reportPath = Path.GetFullPath(System.Configuration.ConfigurationManager.AppSettings["RptFactRev"]);
            rptFactRev.Load(reportPath);

            rptFactRev.Refresh();
            rptFactRev.SetParameterValue("@Consecutivo", Consecutivo);

            crystalReportViewer1.ReportSource = rptFactRev;
        }
    }
}
