using Agregados.Forms.Loading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Agregados.Forms.Users
{
    public partial class FrmUserManage : Form
    {
        //variables del form
        AgregadosEntities DB;
        Usuario user;
        Crypto encriptar;

        public FrmUserManage() // constructor del Form
        {
            InitializeComponent();
            DB = new AgregadosEntities();
            user = new Usuario();
            encriptar = new Crypto();

        }

        private void FrmUsuarioInsert_Load(object sender, EventArgs e) // metodo cuando se abre el form
        {
            CargarEstadosUsuario();
            CargarTiposUsuarios();
            ActivarAdd();
            //solicitud linq simple
            //dgvUsers.DataSource = DB.Usuarios.Where(x => x.IdEstado == 1).ToList();
            //nueva solicitud linq para validar y disenar mejor la DataGridView al usuario // empezando la informacion con usuarios ACTIVOS y lo unico que se necesita obtener
            //para agilizar la respuesta y no obtener tantas columnas de datos
            var result = from us in DB.Usuarios
                         join es in DB.Estados
                         on us.IdEstado equals es.IdEstado
                         where (us.IdEstado == 1)
                         select new
                         {
                             us.IdUsuario, us.NombreUsuario, us.NombreEmpleado,
                             us.Identificacion, us.Correo, IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                             TipoUsuario = (us.TipoUsuario == 1) ? "Administrador" : (us.TipoUsuario == 2) ? "Cajero/a" : "Error", //Lambda Expresion para validar tipo de usuario
                         };
            dgvUsers.DataSource = result.ToList();

            /*
            dgvUsers.Columns["Pin"].Visible = false;
            dgvUsers.Columns["CierreCajas"].Visible = false;
            dgvUsers.Columns["Estado"].Visible = false;
            dgvUsers.Columns["Facturas"].Visible = false;
            dgvUsers.Columns["Contrasennia"].Visible = false;
            */
        }

        //si se selecciona un elemento de la lista se va a cargar la informacion en el item
        //local del form para visualizar las opciones a realizar con dicho item
        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 1)
            {
                user = new Usuario();

                DataGridViewRow MiFila = dgvUsers.SelectedRows[0];

                int idUser = Convert.ToInt32(MiFila.Cells["CidUsuario"].Value);

                //una vez tenemos el valor del ID, se llama a una funcion 
                //de consultar por ID que entrega como retorno un objeto de tipo usuario
                //en este caso voy a reutilizar el objeto de usuario local
                //para cargar datos por medio de la funcion 


                //ESTE METODO de consultor RETORNA UN OBJETO de tipo Empleado
                user = DB.Usuarios.FirstOrDefault(x => x.IdUsuario == idUser);



                if (user != null && user.IdUsuario > 0)
                {
                    //una vez me asegure que el objeto posee datos, entonces se procede a representar
                    //en pantalla
                    txtUsername.Text = user.NombreUsuario.ToString();
                    //txtPassword.Text = user.Contrasennia.ToString();
                    txtEmail.Text = user.Correo.ToString();
                    txtEmployer.Text = user.NombreEmpleado.ToString();
                    txtIdent.Text = user.Identificacion.ToString();
                    CboxUserType.SelectedValue = user.TipoUsuario;
                    CboxStates.SelectedValue = user.IdEstado;

                    ActivarUpdateDelete();
                }
            }
        }
        private void CargarEstadosUsuario()
        {

            //Metodo que permite llamar y obtener los datos filtrados de los estados del usuario y mostrarlos en el comboBox
            var dt = DB.Estados.Where(x => x.IdEstado == 1 || x.IdEstado == 2).ToList();

            CboxStates.ValueMember = "IdEstado";
            CboxStates.DisplayMember = "NombreEstado";
            CboxStates.DataSource = dt;
            CboxStates.SelectedIndex = -1;
        } //carga Cbox Estados
        private void CargarTiposUsuarios()
        {
            //Metodo para crear un DataTable manual sin sentencia SQL a la Base de datos y asi disenar un modelo al comboBox que permita seleccionar si es 
            //administrador o cajero el usuario indicado y visualizarlo en el comboBox
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("Id", typeof(int)), new DataColumn("D", typeof(string)) });
            dt.Rows.Add(1, "Administrador");
            dt.Rows.Add(2, "Cajero/a");

            CboxUserType.DataSource = dt;
            CboxUserType.ValueMember = "Id";
            CboxUserType.DisplayMember = "D";
            CboxUserType.SelectedIndex = -1;
            
        }//Carga Cbox Tipos
        //tiempo loading
        void Wait()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(5);
            }
        }
        //metodo que permite realizar validaciones de los espacion / campos del form no se toma en cuenta
        // la contrasennia ya que esta se valida en el momento que se desee cambiar o no
        private bool ValidarCamposRequeridos()
        {
            bool R = false;

            if (!string.IsNullOrEmpty(txtUsername.Text.Trim()) &&
                //!string.IsNullOrEmpty(txtPassword.Text.Trim()) &&
                !string.IsNullOrEmpty(txtEmail.Text.Trim()) &&
                !string.IsNullOrEmpty(txtEmployer.Text.Trim()) &&
                !string.IsNullOrEmpty(txtIdent.Text.Trim()) &&
                CboxUserType.SelectedIndex != -1 &&
                CboxStates.SelectedIndex != -1
                )
            {
                R = true;
            }
            else
            {
                //estas validaciones deben ser puntuales para informar al usuario que falla 

                if (string.IsNullOrEmpty(txtUsername.Text.Trim()))
                {
                    MessageBox.Show("Nombre de Usuario es Requerido", "Error de Validación!", MessageBoxButtons.OK);
                    txtUsername.Focus();
                    return false;
                }
                /*
                if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    MessageBox.Show("Contraseña es Requerida", "Error de Validación!", MessageBoxButtons.OK);
                    txtPassword.Focus();
                    return false;
                }
                */
                if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
                {
                    MessageBox.Show("Correo Electrónico es Requerido", "Error de Validación!", MessageBoxButtons.OK);
                    txtEmail.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtEmployer.Text.Trim()))
                {
                    MessageBox.Show("Nombre del Empleado es Requerido", "Error de Validación!", MessageBoxButtons.OK);
                    txtEmployer.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtIdent.Text.Trim()))
                {
                    MessageBox.Show("Identificación del Empleado es Requerido", "Error de Validación!", MessageBoxButtons.OK);
                    txtIdent.Focus();
                    return false;
                }
                if (CboxUserType.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe Seleccionar un tipo de Usuario", "Error de Validación!", MessageBoxButtons.OK);
                    CboxUserType.Focus();
                    return false;
                }
                if (CboxStates.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe Seleccionar un estado de Usuario", "Error de Validación!", MessageBoxButtons.OK);
                    CboxUserType.Focus();
                    return false;
                }
            }
            return R;
        }

        //validaciones de botones para evitar errores
        private void ActivarAdd()
        {
            imgAdd.Enabled = true;
            imgUpdate.Enabled = false;
            imgDelete.Enabled = false;
        }
        //validaciones de botones para evitar errores
        private void ActivarUpdateDelete() 
        {
            imgAdd.Enabled = false;
            imgUpdate.Enabled = true;
            imgDelete.Enabled = true;
        }

        //metodo para anadir usuario
        private void imgAdd_Click(object sender, EventArgs e)
        {
            if (ValidarCamposRequeridos())
            {
                if (txtUsername.TextLength < 8)
                {
                    MessageBox.Show("Usuario debe ser tener entre 8 a 15 caracteres", "Error de Validación!", MessageBoxButtons.OK);
                    txtUsername.Focus();
                }
                else
                {
                    if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
                    {
                        MessageBox.Show("Contraseña es Requerida, espacio no puede estar vacío", "Error de Validación!", MessageBoxButtons.OK);
                        txtPassword.Focus();
                    }
                    else
                    {
                        using (FrmLoading frmLoading = new FrmLoading(Wait))
                        {
                            try
                            {
                                //validamos que los datos ingresados sean unicos, como nombre de usuario, identificacion o correo
                                Usuario usuarioTemp = new Usuario();
                                if (Validaciones.IsValidPass(txtPassword.Text.Trim()) && Validaciones.IsValidEmail2(txtEmail.Text.Trim()))
                                {
                                    usuarioTemp = DB.Usuarios.Where((x) => x.NombreUsuario == txtUsername.Text.Trim()).FirstOrDefault();
                                    if (usuarioTemp == null)
                                    {
                                        usuarioTemp = DB.Usuarios.Where((x) => x.Correo == txtEmail.Text.Trim()).FirstOrDefault();

                                        if (usuarioTemp == null)
                                        {
                                            usuarioTemp = DB.Usuarios.Where((x) => x.Identificacion == txtIdent.Text.Trim()).FirstOrDefault();

                                            if (usuarioTemp == null)
                                            {
                                                user = new Usuario
                                                {
                                                    NombreUsuario = txtUsername.Text.Trim(),
                                                    Contrasennia = encriptar.EncriptarEnUnSentido(txtPassword.Text.Trim()),
                                                    Correo = txtEmail.Text.Trim(),
                                                    NombreEmpleado = txtEmployer.Text.Trim(),
                                                    Identificacion = txtIdent.Text.Trim(),
                                                    TipoUsuario = Convert.ToInt32(CboxUserType.SelectedValue),
                                                    IdEstado = Convert.ToInt32(CboxStates.SelectedValue)
                                                };

                                                //metodo para anadir el usuario
                                                DB.Usuarios.Add(user);

                                                if (DB.SaveChanges() > 0)
                                                {
                                                    CheckChange();
                                                    limpiarBusqueda();
                                                    MessageBox.Show("Usuario Agregado correctamente!", "Registro de Usuarios", MessageBoxButtons.OK);
                                                    user = null;
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Usuario No fue agregado", "Error Registro de Usuarios", MessageBoxButtons.OK);
                                                    user = null;
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Ya hay un usuario con esa misma identificación, registrado en el sistema.", "Error de Validación!", MessageBoxButtons.OK);
                                                txtIdent.Focus();
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Ya hay un usuario con ese mismo correo registrado en el sistema.", "Error de Validación!", MessageBoxButtons.OK);
                                            txtEmail.Focus();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Ya hay un usuario con ese mismo nombre de usuario registrado en el sistema.", "Error de Validación!", MessageBoxButtons.OK);
                                        txtUsername.Focus();
                                    }
                                    
                                }
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }
                    }
                }
            }
        }
        //metodo para actualizar usuario
        private void imgUpdate_Click(object sender, EventArgs e)
        {
            if (user == null)
            {
                MessageBox.Show("Usuario No existe, o no se ha seleccionado un usuario de la Lista", "Error Registro de Usuarios", MessageBoxButtons.OK);
            }
            else
            {
                if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    if (ValidarCamposRequeridos())
                    {
                        if (txtUsername.TextLength < 8)
                        {
                            MessageBox.Show("Usuario debe ser tener entre 8 a 15 caracteres", "Error de Validación!", MessageBoxButtons.OK);
                            txtUsername.Focus();
                        }
                        else
                        {
                            if (Validaciones.IsValidEmail2(txtEmail.Text.Trim()))
                            {
                                using (FrmLoading frmLoading = new FrmLoading(Wait))
                                {
                                    try
                                    {
                                        Usuario usuarioTemp = new Usuario();

                                        usuarioTemp = DB.Usuarios.Where((x) => x.NombreUsuario == txtUsername.Text.Trim()).FirstOrDefault();
                                        if (usuarioTemp == null || usuarioTemp.IdUsuario == user.IdUsuario)
                                        {
                                            usuarioTemp = DB.Usuarios.Where((x) => x.Correo == txtEmail.Text.Trim()).FirstOrDefault();
                                            if (usuarioTemp == null || usuarioTemp.IdUsuario == user.IdUsuario)
                                            {
                                                usuarioTemp = DB.Usuarios.Where((x) => x.Identificacion == txtIdent.Text.Trim()).FirstOrDefault();
                                                if (usuarioTemp == null || usuarioTemp.IdUsuario == user.IdUsuario)
                                                {
                                                    user.NombreUsuario = txtUsername.Text.Trim();
                                                    //user.Contrasennia = encriptar.EncriptarEnUnSentido(txtPassword.Text.Trim());
                                                    user.Correo = txtEmail.Text.Trim();
                                                    user.NombreEmpleado = txtEmployer.Text.Trim();
                                                    user.Identificacion = txtIdent.Text.Trim();
                                                    user.TipoUsuario = Convert.ToInt32(CboxUserType.SelectedValue);
                                                    user.IdEstado = Convert.ToInt32(CboxStates.SelectedValue);
                                                    DB.Entry(user).State = EntityState.Modified;
                                                    if (DB.SaveChanges() > 0)
                                                    {
                                                        CheckChange();
                                                        limpiarBusqueda();
                                                        MessageBox.Show("Usuario Modificado correctamente!", "Registro de Usuarios", MessageBoxButtons.OK);
                                                        user = null;
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Usuario No fue Modificado, por favor valide", "Error Registro de Usuarios", MessageBoxButtons.OK);
                                                        user = null;
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Ya hay un usuario con esa misma identificación, registrado en el sistema.", "Error de Validación!", MessageBoxButtons.OK);
                                                    txtIdent.Focus();
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Ya hay un usuario con ese mismo correo registrado en el sistema.", "Error de Validación!", MessageBoxButtons.OK);
                                                txtEmail.Focus();
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Ya hay un usuario con ese mismo nombre de usuario registrado en el sistema.", "Error de Validación!", MessageBoxButtons.OK);
                                            txtUsername.Focus();
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
                                MessageBox.Show("Correo Electrónico no tiene un formato valido con @ y dominio correspondiente.", "Error de Validación!", MessageBoxButtons.OK);
                                txtEmail.Focus();
                            }
                        }             
                    }
                }
                else
                {
                    if (ValidarCamposRequeridos())
                    {
                        if (txtUsername.TextLength < 8)
                        {
                            MessageBox.Show("Usuario debe ser tener entre 8 a 15 caracteres", "Error de Validación!", MessageBoxButtons.OK);
                            txtUsername.Focus();
                        }
                        else
                        {
                            if (Validaciones.IsValidEmail2(txtEmail.Text.Trim()))
                            {
                                if (Validaciones.IsValidPass(txtPassword.Text.Trim()))
                                {
                                    using (FrmLoading frmLoading = new FrmLoading(Wait))
                                    {
                                        try
                                        {
                                            Usuario usuarioTemp = new Usuario();

                                            usuarioTemp = DB.Usuarios.Where((x) => x.NombreUsuario == txtUsername.Text.Trim()).FirstOrDefault();
                                            if (usuarioTemp == null || usuarioTemp.IdUsuario == user.IdUsuario)
                                            {
                                                usuarioTemp = DB.Usuarios.Where((x) => x.Correo == txtEmail.Text.Trim()).FirstOrDefault();
                                                if (usuarioTemp == null || usuarioTemp.IdUsuario == user.IdUsuario)
                                                {
                                                    usuarioTemp = DB.Usuarios.Where((x) => x.Identificacion == txtIdent.Text.Trim()).FirstOrDefault();
                                                    if (usuarioTemp == null || usuarioTemp.IdUsuario == user.IdUsuario)
                                                    {
                                                        user.NombreUsuario = txtUsername.Text.Trim();
                                                        user.Contrasennia = encriptar.EncriptarEnUnSentido(txtPassword.Text.Trim());
                                                        user.Correo = txtEmail.Text.Trim();
                                                        user.NombreEmpleado = txtEmployer.Text.Trim();
                                                        user.Identificacion = txtIdent.Text.Trim();
                                                        user.TipoUsuario = Convert.ToInt32(CboxUserType.SelectedValue);
                                                        user.IdEstado = Convert.ToInt32(CboxStates.SelectedValue);
                                                        DB.Entry(user).State = EntityState.Modified;
                                                        if (DB.SaveChanges() > 0)
                                                        {
                                                            CheckChange();
                                                            limpiarBusqueda();
                                                            MessageBox.Show("Usuario Modificado correctamente!", "Registro de Usuarios", MessageBoxButtons.OK);
                                                            user = null;
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Usuario No fue Modificado, por favor valide", "Error Registro de Usuarios", MessageBoxButtons.OK);
                                                            user = null;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Ya hay un usuario con esa misma identificación, registrado en el sistema.", "Error de Validación!", MessageBoxButtons.OK);
                                                        txtIdent.Focus();
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Ya hay un usuario con ese mismo correo registrado en el sistema.", "Error de Validación!", MessageBoxButtons.OK);
                                                    txtEmail.Focus();
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Ya hay un usuario con ese mismo nombre de usuario registrado en el sistema.", "Error de Validación!", MessageBoxButtons.OK);
                                                txtUsername.Focus();
                                            }
                                        }
                                        catch (Exception)
                                        {

                                            throw;
                                        }
                                    }
                                } 
                            }
                            else
                            {
                                MessageBox.Show("Correo Electrónico no tiene un formato valido con @ y dominio correspondiente.", "Error de Validación!", MessageBoxButtons.OK);
                                txtEmail.Focus();
                            }
                        }
                    }
                }
            }
        }
        //metodo para eleminar o inactivar usuario
        private void imgDelete_Click(object sender, EventArgs e) // metodo para eliminar un usuario o inactivar usuario  //TODO validar que no este asociado a otra tabla
        {
            DialogResult Respuesta = MessageBox.Show("¿Desea eliminar por completo el usuario?", "Registro de Usuarios",
                                                                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (Respuesta == DialogResult.Yes)
            {
                if (user == null)
                {
                    MessageBox.Show("Usuario No existe, o no ha sido seleccionado de la lista", "Error Registro de Usuarios", MessageBoxButtons.OK);
                }
                else
                {
                    DB.Usuarios.Remove(user); // metodo para eliminar el usuario, dato de la BD
                    if (DB.SaveChanges() > 0)
                    {
                        CheckChange();
                        limpiarBusqueda();
                        MessageBox.Show("Usuario Eliminado Correctamente!", "Registro de Usuarios", MessageBoxButtons.OK);
                        user = null;
                    }
                    else
                    {
                        MessageBox.Show("Usuario No fue Eliminado, por favor valide", "Error Registro de Usuarios", MessageBoxButtons.OK);
                        user = null;
                    }
                }
            }
            else
            {
                if (Respuesta == DialogResult.No)
                {
                    DialogResult Respuesta2 = MessageBox.Show("¿Desea Inactivar el usuario?", "Registro de Usuarios",
                                                               MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (Respuesta2 == DialogResult.Yes)
                    {
                        if (user == null)
                        {
                            MessageBox.Show("Usuario No existe, o no ha sido seleccionado de la lista", "Error Registro de Usuarios", MessageBoxButtons.OK);
                        }
                        else
                        {
                            if (user.IdEstado == 2)
                            {
                                MessageBox.Show("Usuario ya se encuentra Inactivo", "Registro de Usuarios", MessageBoxButtons.OK);
                            }
                            else
                            {
                                if (user.IdEstado == 1)
                                {
                                    user.IdEstado = 2; // 2 es inactivo en la BD
                                    DB.Entry(user).State = EntityState.Modified;
                                    if (DB.SaveChanges() > 0)
                                    {
                                        CheckChange();
                                        limpiarBusqueda();
                                        MessageBox.Show("Usuario Inactivado correctamente!", "Registro de Usuarios", MessageBoxButtons.OK);
                                        user = null;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Usuario No fue Inactivado, por favor valide", "Error Registro de Usuarios", MessageBoxButtons.OK);
                                        user = null;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        //metodo salir
        private void imgExit_Click(object sender, EventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();

        } //cierra ventana y vuelve a la Principal
        //limpiar el form, ventana
        private void limpiar()
        {
            txtUsername.Text = null;
            txtPassword.Text = null;
            txtEmail.Text = null;
            txtEmployer.Text = null;
            txtIdent.Text = null;
            CboxUserType.SelectedValue = -1;
            CboxStates.SelectedValue = -1;

            ActivarAdd();
           
        }
        private void limpiarBusqueda()
        {
            txtIdUserSearch.Text = null;
            txtNameUserSearch.Text = null;
            txtIdentUserSearch.Text = null;
        }
        private void imgClean_Click(object sender, EventArgs e)
        {
            limpiar();
            limpiarBusqueda();


        } 
        private void SeePass_Click(object sender, EventArgs e)
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
        //check box que permite cambia la vista de la lista de activo a inactivo los usuario de la BD
        private void CheckChange()
        {
            if (checkBox1.Checked)
            {
                //dgvUsers.DataSource = DB.Usuarios.Where(x => x.IdEstado == 1).ToList();

                //nueva solicitud linq para validar y disenar mejor la DataGridView al usuario // empezando la informacion con usuarios ACTIVOS y lo unico que se necesita obtener
                //para agilizar la respuesta y no obtener tantas columnas de datos
                var result = from us in DB.Usuarios
                             join es in DB.Estados
                             on us.IdEstado equals es.IdEstado
                             where (us.IdEstado == 1)
                             select new
                             {
                                 us.IdUsuario,
                                 us.NombreUsuario,
                                 us.NombreEmpleado,
                                 us.Identificacion,
                                 us.Correo,
                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                 TipoUsuario = (us.TipoUsuario == 1) ? "Administrador" : (us.TipoUsuario == 2) ? "Cajero/a" : "Error", //Lambda Expresion para validar tipo de usuario
                             };
                dgvUsers.DataSource = result.ToList();
                limpiar();
            }
            else
            {
                //dgvUsers.DataSource = DB.Usuarios.Where(x => x.IdEstado == 2).ToList();
                //nueva solicitud linq para validar y disenar mejor la DataGridView al usuario // empezando la informacion con usuarios ACTIVOS y lo unico que se necesita obtener
                //para agilizar la respuesta y no obtener tantas columnas de datos
                var result = from us in DB.Usuarios
                             join es in DB.Estados
                             on us.IdEstado equals es.IdEstado
                             where (us.IdEstado == 2)
                             select new
                             {
                                 us.IdUsuario,
                                 us.NombreUsuario,
                                 us.NombreEmpleado,
                                 us.Identificacion,
                                 us.Correo,
                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                 TipoUsuario = (us.TipoUsuario == 1) ? "Administrador" : (us.TipoUsuario == 2) ? "Cajero/a" : "Error", //Lambda Expresion para validar tipo de usuario
                             };
                dgvUsers.DataSource = result.ToList();
                limpiar();
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckChange();
        }

  
        //Busquedas x ID
        private void txtIdUserSearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdUserSearch.Text.Trim()) && txtIdUserSearch.Text.Count() > 0)
            {
                int num = Convert.ToInt32(txtIdUserSearch.Text.Trim());
                if (checkBox1.Checked)
                {
                    dgvUsers.DataSource = DB.Usuarios.Where((x) => x.IdEstado == 1 && x.IdUsuario == num).ToList();
                    limpiar();
                }
                else
                {
                    dgvUsers.DataSource = DB.Usuarios.Where((x) => x.IdEstado == 2 && x.IdUsuario == num).ToList();
                    limpiar();
                }
            }
            else if (string.IsNullOrEmpty(txtIdUserSearch.Text.Trim()))
            {
                CheckChange();
            }
        }
        //Busquedas x Nombre
        private void txtNameUserSearch_TextChanged(object sender, EventArgs e)
        {
            // se comeinza a buscar a partir de 4 caracteres paea no relentelizar el sistema en ejecucion
            if (!string.IsNullOrEmpty(txtNameUserSearch.Text.Trim()) && txtNameUserSearch.Text.Count() > 3) 
            {
                string text = txtNameUserSearch.Text.Trim();
                if (checkBox1.Checked)
                {
                    dgvUsers.DataSource = DB.Usuarios.Where((x) => x.IdEstado == 1 && x.NombreUsuario.Contains(text)).ToList();
                    limpiar();
                }
                else
                {
                    dgvUsers.DataSource = DB.Usuarios.Where((x) => x.IdEstado == 2 && x.NombreUsuario.Contains(text)).ToList();
                    limpiar();
                }
            }
            else if (string.IsNullOrEmpty(txtIdUserSearch.Text.Trim()))
            {
                CheckChange();
            }
        }
        //Busqueda x Identificacion
        private void txtIdentUserSearch_TextChanged(object sender, EventArgs e)
        {
            // se comeinza a buscar a partir de 4 caracteres paea no relentelizar el sistema en ejecucion
            if (!string.IsNullOrEmpty(txtIdentUserSearch.Text.Trim()) && txtIdentUserSearch.Text.Count() > 0)
            {
                string text = txtIdentUserSearch.Text.Trim();
                if (checkBox1.Checked)
                {
                    dgvUsers.DataSource = DB.Usuarios.Where((x) => x.IdEstado == 1 && x.Identificacion.Contains(text)).ToList();
                    limpiar();
                }
                else
                {
                    dgvUsers.DataSource = DB.Usuarios.Where((x) => x.IdEstado == 2 && x.Identificacion.Contains(text)).ToList();
                    limpiar();
                }
            }
            else if (string.IsNullOrEmpty(txtIdUserSearch.Text.Trim()))
            {
                CheckChange();
            }
        }

        //TODO 
        //Validaciones de campos, y usuarios unicos, y de que sea amigable

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, false, true);
        }
        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, false, true);
        }
        private void txtNameUserSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, false, true);
        }
        private void txtIdUserSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }

        //cuando cierre el formulario
        private void FrmUserManage_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
        }
    }
}
