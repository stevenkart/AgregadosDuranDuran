using NPOI.SS.Formula.Functions;
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
    public partial class FrmManualUser : Form
    {
        public FrmManualUser()
        {
            InitializeComponent();
        }

        private void FrmManualUser_Load(object sender, EventArgs e)
        {
            try
            {
                //pdfDocument.src = "C:\\Users\\steve\\source\\repos\\Agregados\\Agregados\\Forms\\Help\\report 1.pdf";
                string path = Application.StartupPath;
                path += @"\";
                string path2 = System.Configuration.ConfigurationManager.AppSettings["AppManual"];
                pdfDocument.src = path + path2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Al cargar documento",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
              
            }
           
        }

        private void btnPdfManualUserExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void FrmManualUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();

        }
    }
}
