using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Forms.Help
{
    public partial class FrmInfo : Form
    {
        public FrmInfo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Globals.AcercaDe = false;
            this.Hide();

        }

        private void FrmInfo_Load(object sender, EventArgs e)
        {

        }

        private void FrmInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            Globals.AcercaDe = false;
            this.Hide();
        }
    }
}
