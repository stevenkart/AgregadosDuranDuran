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
using System.Data.SqlClient;

namespace Agregados.Forms.Login
{
    public partial class FrmLogin : Form
    {

        AgregadosEntities context;
        Crypto encriptar;
        string data = "";

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
                            Cursor.Current = Cursors.WaitCursor;

                            frmLoading.ShowDialog(this);
                            string u = txtUser.Text.Trim();
                            string p = encriptar.EncriptarEnUnSentido(txtPassword.Text.Trim());
                            //verifica usuario contrasennia y que el usuario este con status activo
                            int IdLog = context.Usuarios.Where(x => x.NombreUsuario == u && x.Contrasennia == p && x.IdEstado == 1).Select(x => x.IdUsuario).FirstOrDefault();              
                            if (IdLog > 0)
                            {
                                Globals.MyGlobalUser = context.Usuarios.Find(IdLog);
                                if (Globals.MyGlobalUser.IdUsuario > 0)
                                {
                                    MessageBox.Show("Bienvenido " + $"{Globals.MyGlobalUser.NombreEmpleado}", "Éxito",
                                                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
                                    frmPrincipalMDI.Show();
                                    this.Hide();
                                }

                            }
                            else
                            {
                                MessageBox.Show("Usuario o contraseña incorrecta, o puede que su usuario esta inactivo", "Error validación",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (SqlException ex)
                        {
                            
                            MessageBox.Show(ex.Message, "Error validación",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }finally { 
                            Cursor.Current = Cursors.Default; 
                        }
                    }  
                }
                else
                {
                    MessageBox.Show("Contraseña es requerido, no puede estar vacio \n" +
                        "Contraseña debe ser entre 8 a 16 carácteres.", "Error validación",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Usuario es requerido, no puede estar vacio, siempre es en minusculas.", "Error validación",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnRecoverDB_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                
                dialog.InitialDirectory = "C:\\Program Files\\Microsoft SQL Server\\MSSQL15.SQLEXPRESS\\MSSQL\\Backup";
                dialog.Filter = "Backup Files (*.bak)|*.bak|All Files (*.*)|*.*";
                dialog.Title = "Restaurar Base de Datos";
                
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (var context = new AgregadosEntities())
                    {
                        //string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        //string relativePath = baseDirectory + "Agregados.mdf";
                        //string LocalDB = relativePath;
                        SqlConnection sqlConn = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;User Id=administrador; Password=administrador123;Connect Timeout=30");
                        //SqlConnection sqlConn = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;User Id=administrador; Password=administrador123;Connect Timeout=30");

                        if (sqlConn != null)
                        {
                            try
                            {
                                
                                var RestoreFilePath = dialog.FileName;

                                //var RestoreQuery1 = $"USE master \r\n IF exists (SELECT * FROM sysdatabases WHERE NAME='Agregados')\r\n\t\tDROP DATABASE [Agregados]\r\n";
                                var RestoreQuery1 = $"USE [master]";
                                var RestoreQuery2 = "RESTORE DATABASE [Agregados] FROM  DISK = N'" + RestoreFilePath + "' WITH  FILE = 1,  NOUNLOAD,  STATS = 5";

                                SqlCommand command1 = new SqlCommand(RestoreQuery1, sqlConn);
                                SqlCommand command2 = new SqlCommand(RestoreQuery2, sqlConn);

                                Cursor.Current = Cursors.WaitCursor;

                                sqlConn.Open();
                                command1.ExecuteNonQuery();
                                command2.ExecuteNonQuery();
                                MessageBox.Show("Restauración de Base de Datos realizada con éxito.", "Restauración Correcta",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                throw;
                            }
                            finally
                            {
                                sqlConn.Close();
                                Cursor.Current = Cursors.Default;
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se pudo establecer la conexión con Servidor.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Se cancelo proceso de restauración de datos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             
            }
        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            btnRecoverDB.Visible = false;
            data += e.KeyCode.ToString();
        }

        private void FrmLogin_KeyUp(object sender, KeyEventArgs e)
        {
     
            if (btnRecoverDB.Visible == false)
            {//Restore Data Base 'RDB' = makes visible the hidden button to select and restore the database,
             //but keep pressing the RD once the B is unpressed to show the hiide button
               if (data == "RDB")
                {
                    btnRecoverDB.Visible = true;
                }
                else
                {
                    data = "";
                    btnRecoverDB.Visible = false;
                }
            }
            else
            {
                data = "";
                btnRecoverDB.Visible = false;
            }
        }
    }
}
