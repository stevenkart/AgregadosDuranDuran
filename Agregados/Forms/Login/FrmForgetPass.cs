using Agregados.Forms.Loading;
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

namespace Agregados.Forms.Login
{
    public partial class FrmForgetPass : Form
    {
        AgregadosEntities context;
        Crypto encriptar;
        Usuarios usuario;
        CorreoNotificaciones email;


        public FrmForgetPass()
        {
            InitializeComponent();

            context = new AgregadosEntities();
            encriptar = new Crypto();
            usuario = new Usuarios();
            email = new CorreoNotificaciones();
        }
        //metodo que se encarga de que en el momento de abrir el form, cargue la informacion que corresponda
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            lblPin.Visible = false;
            txtPin.Visible = false;
            btnValidatePin.Visible = false;

            lblPass.Visible = false;
            lblConfirmPass.Visible = false;
            txtPass.Visible = false;
            txtConfirmPass.Visible = false;
            btnChange.Visible = false;
            btnSeePass.Visible = false;
        }
        //para el loading 
        void Wait()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(5);
            }
        }

        //metodo que se encarga de validar los datos ingresado de usuario y inmediatamente enviar un correo a su usuario
        private void btnSend_Click(object sender, EventArgs e)
        {
            using (FrmLoading frmLoading = new FrmLoading(Wait))
            {
                try
                {
                    frmLoading.ShowDialog(this);
                    if (!string.IsNullOrEmpty(txtUser.Text.Trim()))
                    {
                        int idUser = context.Usuarios.Where(x => x.NombreUsuario == txtUser.Text.Trim()).Select(x => x.IdUsuario).FirstOrDefault();
                        if (idUser > 0)
                        {
                            usuario = context.Usuarios.Find(idUser);
                            //validamos que si o si tenga un valor nuestro usuario
                            if (usuario.IdUsuario > 0)
                            {
                                //crear un valor aleatorio de 6 digitos
                                Random rand = new Random();
                                char[] numbers = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
                                string tempCode = null;
                                for (int i = 0; i < 6; i++)
                                {
                                    tempCode += numbers[rand.Next(0, 9)];
                                }
                                int pCode = Convert.ToInt32(tempCode);

                                //cuando se obtiene se pasa al usuario, para modificarlo en la DB
                                usuario.Pin = Convert.ToString(pCode.ToString());
                                context.Entry(usuario).State = EntityState.Modified;
                                //si sale bien, se procede a enviar correo
                                if (context.SaveChanges() > 0)
                                {
                                    email = context.CorreoNotificaciones.FirstOrDefault();
                                    if (email.SendEmail(usuario.Correo, "Prueba",
                                        "Este es el código PIN para que puedas cambiar la contraseña " +
                                        $"{usuario.Pin}")) //sentto quemado //TODO cambiar a que obtenga el correo del usuario
                                    {
                                        MessageBox.Show("Por favor ingrese el pin, que fue enviado a su correo para continuar.",
                                            "Recuperación de Contraseña", MessageBoxButtons.OK);

                                        txtUser.Enabled = false;

                                        lblPin.Visible = true;
                                        txtPin.Visible = true;
                                        btnValidatePin.Visible = true;

                                        btnSend.Text = "Re-Enviar";

                                    }
                                    else
                                    {
                                        MessageBox.Show("Correo no pudo ser enviado.",
                                            "Recuperación de Contraseña (Error)", MessageBoxButtons.OK);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No pudo guardarse la información del usuario.",
                                       "Recuperación de Contraseña (Error)", MessageBoxButtons.OK);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("No existe el usuario ingresado en la base de datos.",
                                    "Recuperación de Contraseña (Error)", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Usuario es requerido", "Error validación",
                                           MessageBoxButtons.OK);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
        }

        //metodo que valida que el caracter ingresado sea texto y minuscula
        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, false, true);
        }

        //metodo que valida que el caracter ingresado sea numero

        private void txtPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e,true);
        }

        //boton de salir, vuelve al formulario de login
        private void btnExit_Click(object sender, EventArgs e)
        {
            usuario = null;
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
            this.Hide();
        }

        //valida el pin ingresado
        private void btnValidatePin_Click(object sender, EventArgs e)
        {
            using (FrmLoading frmLoading = new FrmLoading(Wait))
            {
                try
                {
                    frmLoading.ShowDialog(this);
                    if (!string.IsNullOrEmpty(txtPin.Text.Trim()))
                    {
                        if (txtPin.Text.Trim() == usuario.Pin.ToString())
                        {
                            txtPin.Enabled = false;

                            lblPass.Visible = true;
                            lblConfirmPass.Visible = true;
                            txtPass.Visible = true;
                            txtConfirmPass.Visible = true;
                            btnChange.Visible = true;
                            btnSeePass.Visible = true;

                            btnValidatePin.Enabled = false;

                            btnSend.Enabled = false;

                        }
                        else
                        {
                            MessageBox.Show("Pin no es correcto!", "Error validación",
                                          MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Pin debe ser ingresado para continuar", "Error validación",
                                         MessageBoxButtons.OK);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
        }


        //valida la contrasennia y la cambia si todo esta bien
        private void btnChange_Click(object sender, EventArgs e)
        {
            using (FrmLoading frmLoading = new FrmLoading(Wait))
            {
                try
                {
                    frmLoading.ShowDialog(this);
                    if (!string.IsNullOrEmpty(txtPass.Text.Trim()) || !string.IsNullOrEmpty(txtConfirmPass.Text.Trim()))
                    {
                        if (Validaciones.IsValidPass(txtPass.Text.Trim()))
                        {
                            if (txtPass.Text.Trim() == txtConfirmPass.Text.Trim())
                            {
                                //cuando se pasan las validaciones y se sabe que contraseña cumple con lo solicitado
                                //se procede a guardar en la base de datos.
                                usuario.Contrasennia = encriptar.EncriptarEnUnSentido(txtPass.Text.Trim());
                                usuario.Pin = null;
                                context.Entry(usuario).State = EntityState.Modified;
                                if (context.SaveChanges() > 0)
                                {
                                    email = context.CorreoNotificaciones.FirstOrDefault();
                                    if (email.SendEmail(usuario.Correo, "Cambio de contraseña",
                                        "Haz Cambiado la contraseña correctamente. Por favor intenta ingresar. ")) //sentto quemado //TODO cambiar a que obtenga el correo del usuario
                                    {
                                        MessageBox.Show("Cambio de Contraseña correcto",
                                            "Recuperación de Contraseña", MessageBoxButtons.OK);

                                        usuario = null;
                                        FrmLogin frmLogin = new FrmLogin();
                                        frmLogin.Show();
                                        this.Hide();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Los espacios de contraseña y confirmar contraseña, No son igual.",
                            "Error validación", MessageBoxButtons.OK);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Los espacios de contraseña y confirmar contraseña no pueden estar vacios.",
                            "Error validación", MessageBoxButtons.OK);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }

        }

        //boton para observar contrasennia
        private void btnSeePass_Click(object sender, EventArgs e)
        {
            if (txtPass.UseSystemPasswordChar == true)
            {
                txtPass.UseSystemPasswordChar = false;
                txtConfirmPass.UseSystemPasswordChar= false;
            }
            else
            {
                txtPass.UseSystemPasswordChar = true;
                txtConfirmPass.UseSystemPasswordChar = true;
            }
        }
    }
}
