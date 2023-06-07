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
            if (!Globals.MifrmInfo.IsDisposed)
            {
                Globals.MifrmInfo.Hide();
            }
            this.Hide();
        }

        private void FrmPrincipalMDI_Load(object sender, EventArgs e)
        {
            tmrFechaHora.Enabled = true;
            lblUsuarioLogueado.Text = $"( {Globals.MyGlobalUser.NombreUsuario} )" + $" {Globals.MyGlobalUser.NombreEmpleado} ";
        }

        private void notificacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Globals.MifrmNotifications = new Notifications.FrmNotifications();
            Globals.MifrmNotifications.Show();
            this.Hide();
        }

        private void acercaDeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Globals.MifrmInfo = new Help.FrmInfo();
            Globals.MifrmInfo.Show();
        }

        private void vehiculosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Globals.MifrmVehiclesManage = new Vehicles.FrmVehiclesManage();
            Globals.MifrmVehiclesManage.Show();
            this.Hide();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Globals.MifrmCustomers = new Customers.FrmCustomersManage();
            Globals.MifrmCustomers.Show();
            this.Hide();
        }


        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Globals.MifrmUser = new Users.FrmUserManage();
            Globals.MifrmUser.Show();
            this.Hide();
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Globals.MifrmProviders = new Providers.FrmProvidersManage();
            Globals.MifrmProviders.Show();
            this.Hide();
        }


        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Globals.MyGlobalUser = null;
            FrmLogin frmLogin = new Forms.Login.FrmLogin();
            frmLogin.Show();
            if (!Globals.MifrmInfo.IsDisposed)
            {
                Globals.MifrmInfo.Hide();
            }
            this.Hide();
        }

        private void salirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //cierra completamente la app
            System.Environment.Exit(0);
        }

        private void FrmPrincipalMDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Globals.MyGlobalUser = null;
            FrmLogin frmLogin = new Forms.Login.FrmLogin();
            frmLogin.Show();
            if (!Globals.MifrmInfo.IsDisposed)
            {
                Globals.MifrmInfo.Hide();
            }
            this.Hide();
        }

        private void materialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Globals.MifrmMaterials = new Materials.FrmMaterialsManage();
            Globals.MifrmMaterials.Show();
            this.Hide();
        }

        private void facturaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Globals.MifrmBillAdd = new Bills.FrmBillAdd();
            Globals.MifrmBillAdd.Show();
            this.Hide();
        }

        private void tmrFechaHora_Tick(object sender, EventArgs e)
        {
            //asignar al label de fecha y hora el valor en formato extendido de la fecha 
            // y ademas la hora

            string fecha = DateTime.Now.Date.ToLongDateString();
            string hora = DateTime.Now.ToLongTimeString();

            lblFechaHora.Text = fecha + " / " + hora;
        }

        private void reporteDeFacturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Globals.MifrmFactsReports = new Reports.FrmFactsReports();
            Globals.MifrmFactsReports.Show();
            this.Hide();
        }
    }
}
