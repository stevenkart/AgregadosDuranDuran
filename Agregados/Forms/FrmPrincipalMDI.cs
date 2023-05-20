using Agregados.Forms.Loading;
using Agregados.Forms.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Forms
{
    public partial class FrmPrincipalMDI : Form
    {
        public FrmPrincipalMDI()
        {
            InitializeComponent();
            
        }


        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Globals.MyGlobalUser = null;
            FrmLogin frmLogin = new Forms.Login.FrmLogin();
            frmLogin.Show();
            this.Hide();
        }

        private void FrmPrincipalMDI_Load(object sender, EventArgs e)
        {
            
        }

        private void notificacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Globals.MifrmNotifications = new Notifications.FrmNotifications();
            Globals.MifrmNotifications.Show();
        }

        private void acercaDeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Globals.MifrmInfo = new Help.FrmInfo();
            Globals.MifrmInfo.Show();
        }

        private void vehiculosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Globals.MifrmVehiclesManage = new Vehicles.FrmVehiclesManage();
        }

       

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Globals.MifrmUser = new Users.FrmUserManage();
            Globals.MifrmUser.Show();
        }












        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Globals.MyGlobalUser = null;
            FrmLogin frmLogin = new Forms.Login.FrmLogin();
            frmLogin.Show();
            this.Hide();
        }

        private void salirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //cierra completamente la app
            Application.Exit();
        }




    }
}
