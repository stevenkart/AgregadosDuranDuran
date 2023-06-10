using Agregados.Forms.Cashiers;
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
        //variables del form
        AgregadosEntities DB;
        CierreApertCajas cierreApertCajas;

        CierreApertCajas apertura; // valor termporal apertura
        CierreApertCajas cierre; // valor termporal cierre

        public FrmPrincipalMDI()
        {
            InitializeComponent();
            DB = new AgregadosEntities();
            cierreApertCajas = new CierreApertCajas();
            apertura = new CierreApertCajas();
            cierre = new CierreApertCajas();

            abrirCajaToolStripMenuItem.Enabled = false;
            cerrarCajaToolStripMenuItem.Enabled = false;
            facturaciónToolStripMenuItem.Enabled = false;
            facturaciónComprasToolStripMenuItem.Enabled = false;

        }

        //busca si hay una apertura actualmente
        public CierreApertCajas BuscarAperturaActual()
        {
            apertura = null;
            try
            {
                cierreApertCajas = DB.CierreApertCajas.Where((x) => x.Accion == 1).FirstOrDefault();
                if (cierreApertCajas != null)
                {
                    int id = DB.CierreApertCajas.Where((x) => x.Accion == 1).Select((x) => x.IdCierreApert).Max();
                    apertura = DB.CierreApertCajas.Find(id);
                }
                cierreApertCajas = null;
                return apertura;
            }
            catch (Exception)
            {
                throw;
            }
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


        //carga el fomr
        private void FrmPrincipalMDI_Load(object sender, EventArgs e)
        {
            //identifica si hay una apertura de caja, y si pertenece al usuario logueado hbailita botones, sino no 
            if (BuscarAperturaActual() != null)
            {
                if (apertura.IdUsuario == Globals.MyGlobalUser.IdUsuario)
                {
                    facturaciónToolStripMenuItem.Enabled = true;
                    facturaciónComprasToolStripMenuItem.Enabled = true;
                    cerrarCajaToolStripMenuItem.Enabled = true;
                    abrirCajaToolStripMenuItem.Enabled = false;
                }
                else
                {
                    abrirCajaToolStripMenuItem.Enabled = false;
                    facturaciónComprasToolStripMenuItem.Enabled = false;
                    facturaciónToolStripMenuItem.Enabled = false;
                    cerrarCajaToolStripMenuItem.Enabled = false;
                }
            }
            else
            {
                abrirCajaToolStripMenuItem.Enabled = true;
            }



            tmrFechaHora.Enabled = true;
            lblUsuarioLogueado.Text = $"( {Globals.MyGlobalUser.NombreUsuario} )" + $" {Globals.MyGlobalUser.NombreEmpleado} ";

           
        }

        private void abrirCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form MifrmCashierOpen = new Cashiers.FrmCashierOpen();

            DialogResult resp = MifrmCashierOpen.ShowDialog();

            if (resp == DialogResult.OK)
            {
                //identifica si hay una apertura de caja se hizo correctamente, y si pertenece al usuario logueado habilita botones, sino no 
                if (BuscarAperturaActual() != null)
                {
                    if (apertura.IdUsuario == Globals.MyGlobalUser.IdUsuario)
                    {
                        facturaciónToolStripMenuItem.Enabled = true;
                        facturaciónComprasToolStripMenuItem.Enabled = true;
                        cerrarCajaToolStripMenuItem.Enabled = true;
                        abrirCajaToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        abrirCajaToolStripMenuItem.Enabled = false;
                        facturaciónComprasToolStripMenuItem.Enabled = false;
                        facturaciónToolStripMenuItem.Enabled = false;
                        cerrarCajaToolStripMenuItem.Enabled = false;
                    }
                }
                else
                {
                    abrirCajaToolStripMenuItem.Enabled = true;
                }
            }

        }

        private void cerrarCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form MifrmCashierClose = new Cashiers.FrmCashierClose();

            DialogResult resp = MifrmCashierClose.ShowDialog();

            if (resp == DialogResult.OK)
            {
                //identifica si hay una apertura de caja se hizo correctamente, y si pertenece al usuario logueado habilita botones, sino no 
                if (BuscarAperturaActual() != null)
                {
                    if (apertura.IdUsuario == Globals.MyGlobalUser.IdUsuario)
                    {
                        facturaciónToolStripMenuItem.Enabled = true;
                        facturaciónComprasToolStripMenuItem.Enabled = true;
                        cerrarCajaToolStripMenuItem.Enabled = true;
                        abrirCajaToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        abrirCajaToolStripMenuItem.Enabled = false;
                        facturaciónComprasToolStripMenuItem.Enabled = false;
                        facturaciónToolStripMenuItem.Enabled = false;
                        cerrarCajaToolStripMenuItem.Enabled = false;
                    }
                }
                else
                {
                    abrirCajaToolStripMenuItem.Enabled = true;
                    facturaciónComprasToolStripMenuItem.Enabled = false;
                    facturaciónToolStripMenuItem.Enabled = false;
                    cerrarCajaToolStripMenuItem.Enabled = false;
                }
            }
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

        private void facturaciónComprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Globals.MifrmBillProviderAdd = new Bills.FrmBillProviderAdd();
            Globals.MifrmBillProviderAdd.Show();
            this.Hide();
        }

       
    }
}
