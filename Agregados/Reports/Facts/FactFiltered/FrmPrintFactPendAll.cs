﻿using System;
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
    public partial class FrmPrintFactPendAll : Form
    {
        public FrmPrintFactPendAll()
        {
            InitializeComponent();
        }

        private void FrmPrintFactPendAll_Load(object sender, EventArgs e)
        {
            RptFactPendAll rptFactPendAll = new RptFactPendAll();

            crystalReportViewer1.ReportSource = rptFactPendAll;
        }
    }
}
