using Agregados.Forms.Loading;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Agregados.Forms.Login
{
    public partial class FrmLogin : Form
    {

        AgregadosEntities context;
        Crypto encriptar;

        public FrmLogin()
        {
            InitializeComponent();
            context = new AgregadosEntities();
            encriptar = new Crypto();
            
        }
        private void btnSeePass_Click(object sender, EventArgs e)
        {
            if (txtPassword.UseSystemPasswordChar == true)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            //cierra completamente la app
            System.Environment.Exit(0);
        }

        //tiempo login
        void Wait()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(5);
            }
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Limpiar Globales para buscar el nuevo login del usuario y refrescar info

            if (!string.IsNullOrEmpty(txtUser.Text.Trim()))
            {
                if (!string.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    using (FrmLoading frmLoading = new FrmLoading(Wait))
                    {
                        try
                        {
                            frmLoading.ShowDialog(this);
                            string u = txtUser.Text.Trim();
                            string p = encriptar.EncriptarEnUnSentido(txtPassword.Text.Trim());
                            int IdLog = context.Usuarios.Where(x => x.NombreUsuario == u && x.Contrasennia == p).Select(x => x.IdUsuario).FirstOrDefault();              
                            if (IdLog > 0)
                            {
                                Globals.MyGlobalUser = context.Usuarios.Find(IdLog);
                                if (Globals.MyGlobalUser.IdUsuario > 0)
                                {
                                    MessageBox.Show("Bienvenido " + $"{Globals.MyGlobalUser.NombreEmpleado}", "Éxito",
                                                                MessageBoxButtons.OK);
                                    FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
                                    frmPrincipalMDI.Show();
                                    this.Hide();
                                }

                            }
                            else
                            {
                                MessageBox.Show("Usuario o contraseña incorrecta", "Error validación",
                                    MessageBoxButtons.OK);
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }  
                }
                else
                {
                    MessageBox.Show("Contraseña es requerido, no puede estar vacio \n" +
                        "Contraseña debe ser entre 8 a 16 carácteres.", "Error validación",
                       MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Usuario es requerido, no puede estar vacio, siempre es en minusculas.", "Error validación",
                       MessageBoxButtons.OK);
            }
        }

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, false, true);
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmForgetPass frmForgetPass = new FrmForgetPass();
            frmForgetPass.Show();
            this.Hide();
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            //cierra completamente la app
            System.Environment.Exit(0);
        }
    }
}
