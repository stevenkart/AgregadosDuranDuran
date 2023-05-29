using Agregados.Forms.Loading;
using Agregados.Forms.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Forms.Notifications
{
    public partial class FrmNotifications : Form
    {

        AgregadosEntities context;
        Crypto encriptar;
        CorreoNotificaciones email;

        public FrmNotifications()
        {
            InitializeComponent();
            context = new AgregadosEntities();
            encriptar = new Crypto();
            email = new CorreoNotificaciones();

        }

        //metodo que se encarga de que en el momento de abrir el form, cargue la informacion que corresponda
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            email = context.CorreoNotificaciones.FirstOrDefault();
            txtEmailActual.Text = email.Correo.ToString();

        }

        //para el loading 
        void Wait()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(5);
            }
        }

        //exit form
        private void btnExit_Click(object sender, EventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
        }



        //cambio de correo para notificaciones
        private void btnChange_Click(object sender, EventArgs e)
        {
            using (FrmLoading frmLoading = new FrmLoading(Wait))
            {
                try
                {
                    if (!string.IsNullOrEmpty(txtNewEmail.Text.Trim()) &&
                        !string.IsNullOrEmpty(txtConfirmEmail.Text.Trim()) &&
                        !string.IsNullOrEmpty(txtPass.Text.Trim()) &&
                        !string.IsNullOrEmpty(txtConfirmPass.Text.Trim())
                        )
                    {
                        if (txtNewEmail.Text.Trim() == txtConfirmEmail.Text.Trim())
                        {
                            if (txtPass.Text.Trim() == txtConfirmPass.Text.Trim())
                            {
                                if (Validaciones.IsValidEmail2(txtNewEmail.Text.Trim()))
                                {
                                    DialogResult Respuesta = MessageBox.Show("¿Seguro de actualizar el correo?", "Correo Notificaciones",
                                                                MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                                    if (Respuesta == DialogResult.Yes)
                                    {
                                        email.Correo = txtNewEmail.Text.Trim();
                                        email.Contrasennia = encriptar.EncriptarPassword(txtPass.Text.Trim());
                                        context.Entry(email).State = EntityState.Modified;
                                        if (context.SaveChanges() > 0)
                                        {
                                            MessageBox.Show("Cambio correcto de email para notificaciones",
                                                "Correo Notificaciones", MessageBoxButtons.OK);

                                            txtEmailActual.Text = email.Correo.ToString();
                                            Clean();
                                        }
                                    }                                
                                }
                                else
                                {
                                    MessageBox.Show("Correo no es valido, por favor verifique que cuente con '@', y con dominio correcto",
                                                                  "Correo Notificaciones (Error)", MessageBoxButtons.OK);

                                }
                            }
                            else
                            {
                                MessageBox.Show("La confirmación de contraseña no es igual al correo ingresado",
                                                                  "Correo Notificaciones (Error)", MessageBoxButtons.OK);

                            }
                        }
                        else
                        {
                            MessageBox.Show("La confirmación de correo no es igual al correo ingresado",
                                                                  "Correo Notificaciones (Error)", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {

                        MessageBox.Show("Los campos no pueden estar vacíos",
                                      "Correo Notificaciones (Error)", MessageBoxButtons.OK);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }


        //LIMPIA LOS ESPACIOS
        private void Clean()
        {
            txtNewEmail.Text = null;
            txtPass.Text = null;
            txtConfirmPass.Text = null;
            txtConfirmEmail.Text = null;
        }

        private void txtNewEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, false, true);
        }

        private void txtConfirmEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, false, true);
        }

        private void FrmNotifications_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
        }
    }
}
