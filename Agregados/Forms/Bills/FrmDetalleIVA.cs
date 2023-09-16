using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Forms.Bills
{
    public partial class FrmDetalleIVA : Form
    {

        //variables del form
        AgregadosEntities DB;

        public FrmDetalleIVA()
        {
            InitializeComponent();

            DB = new AgregadosEntities();

        }

        private void FrmDetalleIVA_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtDetalleNoCobroIVA = null;
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            Globals.MifrmBillAdd.Detalle = txtDetalleNoCobroIVA.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }
    }
}
