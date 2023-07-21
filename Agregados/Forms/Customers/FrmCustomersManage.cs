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

namespace Agregados.Forms.Customers
{
    public partial class FrmCustomersManage : Form
    {
        //variables del form
        AgregadosEntities DB;
        Clientes cliente;

        public FrmCustomersManage()
        {
            InitializeComponent();
            DB = new AgregadosEntities();
            cliente = new Clientes();
        }

        private void FrmCustomersManage_Load(object sender, EventArgs e)
        {
            CargarEstadosClientes();
            CargarTipos();
 
            // linq para validar y disenar mejor la DataGridView al usuario // empezando la informacion con Estado ACTIVO y lo unico que se necesita obtener
            //para agilizar la respuesta y no obtener tantas columnas de datos
            var result = from cl in DB.Clientes
                         join es in DB.Estados
                         on cl.IdEstado equals es.IdEstado
                         where (cl.IdEstado == 1)
                         select new
                         {
                             cl.IdCliente,
                             cl.Identificacion,
                             cl.Nombre,
                             //Lambda Expresion IF -ELSE para validar tipo de Cliente y proceder a indicarlo en modo texto
                             TipoCliente = (cl.IdTipoCliente == 1) ? "Físico" : (cl.IdTipoCliente == 2) ? "Júridico" : "",
                             cl.Telefono,
                             cl.Telefono2,
                             cl.Correo,
                             cl.Direccion,
                             cl.Detalles,
                             IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                         };
            dgvClientes.DataSource = result.ToList();
            limpiar();
        }


        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 1)
            {
                cliente = new Clientes();

                DataGridViewRow MiFila = dgvClientes.SelectedRows[0];

                int IdCliente = Convert.ToInt32(MiFila.Cells["CIdCliente"].Value);

                //una vez tenemos el valor del ID, se llama a una funcion 
                //de consultar por ID que entrega como retorno un objeto de tipo cliente
                //en este caso voy a reutilizar el objeto de cliente local
                //para cargar datos por medio de la funcion 


                //ESTE METODO de consultor RETORNA UN OBJETO de tipo Empleado
                cliente = DB.Clientes.FirstOrDefault(x => x.IdCliente == IdCliente);

                if (cliente != null && cliente.IdCliente > 0)
                {
                    //una vez me asegure que el objeto posee datos, entonces se procede a representar
                    //en pantalla
                    txtIdent.Text = cliente.Identificacion.ToString();
                    txtName.Text = cliente.Nombre.ToString();
                    CboxCustomerType.SelectedValue = cliente.IdTipoCliente;
                    txtMainPhone.Text = cliente.Telefono.ToString();
                    txtSecondPhone.Text = cliente.Telefono2.ToString();
                    txtEmail.Text = cliente.Correo.ToString();
                    txtAddress.Text = cliente.Direccion.ToString();
                    txtDetails.Text = cliente.Detalles.ToString();
                    CboxStates.SelectedValue = cliente.IdEstado;

                    ActivarUpdateDelete();
                }
            }
        }

        //carga Cbox Estados
        private void CargarEstadosClientes()
        {

            //Metodo que permite llamar y obtener los datos filtrados de los clientes y mostrarlos en el comboBox
            var dt = DB.Estados.Where(x => x.IdEstado == 1 || x.IdEstado == 2).ToList();

            CboxStates.ValueMember = "IdEstado";
            CboxStates.DisplayMember = "NombreEstado";
            CboxStates.DataSource = dt;
            CboxStates.SelectedIndex = -1;
        }

        //Carga Cbox Tipos
        private void CargarTipos()
        {
            /*
            //Metodo para crear un DataTable manual sin sentencia SQL a la Base de datos y asi disenar un modelo al comboBox que permita seleccionar los meses 
            //y entonces guarde pero un valor int, y mostrando un valor string 
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("Id", typeof(int)), new DataColumn("D", typeof(string)) });
            dt.Rows.Add(1, "Físico");
            dt.Rows.Add(2, "Júridico");

            CboxCustomerType.DataSource = dt;
            CboxCustomerType.ValueMember = "Id";
            CboxCustomerType.DisplayMember = "D";
            CboxCustomerType.SelectedIndex = -1;
            */


            //Metodo que permite llamar y obtener los datos filtrados de los clientes y mostrarlos en el comboBox
            var dt = DB.TipoClientes.Where(x => x.IdTipoCliente == 1 || x.IdTipoCliente == 2).ToList();

            CboxCustomerType.ValueMember = "IdTipoCliente";
            CboxCustomerType.DisplayMember = "TipoCliente";
            CboxCustomerType.DataSource = dt;
            CboxCustomerType.SelectedIndex = -1;

        }

        private void CargarTiposFisico()
        {
            //Metodo que permite llamar y obtener los datos filtrados de los clientes y mostrarlos en el comboBox
            var dt = DB.TipoClientes.Where(x => x.IdTipoCliente == 1).ToList();

            CboxCustomerType.ValueMember = "IdTipoCliente";
            CboxCustomerType.DisplayMember = "TipoCliente";
            CboxCustomerType.DataSource = dt;
            CboxCustomerType.SelectedIndex = -1;

        }

        private void CargarTiposJuridico()
        {
            //Metodo que permite llamar y obtener los datos filtrados de los clientes y mostrarlos en el comboBox
            var dt = DB.TipoClientes.Where(x => x.IdTipoCliente == 2).ToList();

            CboxCustomerType.ValueMember = "IdTipoCliente";
            CboxCustomerType.DisplayMember = "TipoCliente";
            CboxCustomerType.DataSource = dt;
            CboxCustomerType.SelectedIndex = -1;

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

        //limpiar el form, ventana
        private void limpiar()
        {
            txtIdent.Text = null;
            txtName.Text = null;
            txtMainPhone.Text = null;
            txtSecondPhone.Text = null;
            txtEmail.Text = null;
            txtAddress.Text = null;
            txtDetails.Text = null;
            CboxCustomerType.SelectedValue = -1;
            CboxStates.SelectedValue = -1;

            ActivarAdd();

        }
        private void limpiarBusqueda()
        {
            txtIdCustomerSearch.Text = null;
            txtIdentSearch.Text = null;
            txtNameSearch.Text = null;

            txtIdCustomerSearch.Enabled = true;
            txtIdentSearch.Enabled = true;
            txtNameSearch.Enabled = true;
        }


        private void imgClean_Click(object sender, EventArgs e)
        {
            limpiar();
            limpiarBusqueda();
        }

        //tiempo loading
        void Wait()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(5);
            }
        }


        //metodo que permite realizar validaciones de los espacion / campos del form no se toma en cuenta
        private bool ValidarCamposRequeridos()
        {
            bool R = false;

            if (!string.IsNullOrEmpty(txtName.Text.Trim()) &&
                CboxCustomerType.SelectedIndex != -1 &&
                !string.IsNullOrEmpty(txtMainPhone.Text.Trim()) &&
                CboxStates.SelectedIndex != -1
                )
            {
                R = true;
            }
            else
            {
                //estas validaciones deben ser puntuales para informar al usuario que falla 

                if (string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    MessageBox.Show("Nombre de Cliente es Requerido", "Error de Validación!", MessageBoxButtons.OK);
                    txtName.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtMainPhone.Text.Trim()))
                {
                    MessageBox.Show("Teléfono Principal es Requerido", "Error de Validación!", MessageBoxButtons.OK);
                    txtMainPhone.Focus();
                    return false;
                }
                if (CboxCustomerType.SelectedIndex == -1)
                {
                    MessageBox.Show("Tipo de Cliente debe ser ingresado", "Error de Validación!", MessageBoxButtons.OK);
                    CboxCustomerType.Focus();
                    return false;
                }
                if (CboxStates.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe Seleccionar un estado del Cliente", "Error de Validación!", MessageBoxButtons.OK);
                    CboxStates.Focus();
                    return false;
                }
            }
            return R;
        }


        //check BOX
        private void CheckChange()
        {
            if (ChActivos.Checked == true)
            {
                var result = from cl in DB.Clientes
                             join es in DB.Estados
                             on cl.IdEstado equals es.IdEstado
                             where (cl.IdEstado == 1)
                             select new
                             {
                                 cl.IdCliente,
                                 cl.Identificacion,
                                 cl.Nombre,
                                 //Lambda Expresion IF -ELSE para validar tipo de Cliente y proceder a indicarlo en modo texto
                                 TipoCliente = (cl.IdTipoCliente == 1) ? "Físico" : (cl.IdTipoCliente == 2) ? "Júridico" : "",
                                 cl.Telefono,
                                 cl.Telefono2,
                                 cl.Correo,
                                 cl.Direccion,
                                 cl.Detalles,
                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                             };
                dgvClientes.DataSource = result.ToList();
                limpiar();
            }
            else
            {
                if (ChActivos.Checked == false)
                {
                    var result = from cl in DB.Clientes
                                 join es in DB.Estados
                                 on cl.IdEstado equals es.IdEstado
                                 where (cl.IdEstado == 2)
                                 select new
                                 {
                                     cl.IdCliente,
                                     cl.Identificacion,
                                     cl.Nombre,
                                     //Lambda Expresion IF -ELSE para validar tipo de Cliente y proceder a indicarlo en modo texto
                                     TipoCliente = (cl.IdTipoCliente == 1) ? "Físico" : (cl.IdTipoCliente == 2) ? "Júridico" : "",
                                     cl.Telefono,
                                     cl.Telefono2,
                                     cl.Correo,
                                     cl.Direccion,
                                     cl.Detalles,
                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                 };
                    dgvClientes.DataSource = result.ToList();
                    limpiar();
                }
            }
        }
        private void ChActivos_CheckedChanged(object sender, EventArgs e)
        {
            CheckChange();
        }

        //metodo add, delete & update
        private void imgAdd_Click(object sender, EventArgs e)
        {
            if (ValidarCamposRequeridos())
            {
                if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
                {
                    DialogResult respuesta = MessageBox.Show("¿Deseas agregar la empresa o cliente/a " + $"{txtName.Text.Trim()} ?",
                                           "Registro de Clientes", MessageBoxButtons.YesNo);
                    if (respuesta == DialogResult.Yes)
                    {
                        using (FrmLoading frmLoading = new FrmLoading(Wait))
                        {
                            try
                            {
                                cliente = new Clientes
                                {
                                    Identificacion = txtIdent.Text.Trim(),
                                    Nombre = txtName.Text.Trim(),
                                    IdTipoCliente = Convert.ToInt32(CboxCustomerType.SelectedValue),
                                    Telefono = txtMainPhone.Text.Trim(),
                                    Telefono2 = txtSecondPhone.Text.Trim(),
                                    Correo = txtEmail.Text.Trim(),
                                    Direccion = txtAddress.Text.Trim(),
                                    Detalles = txtDetails.Text.Trim(),
                                    IdEstado = Convert.ToInt32(CboxStates.SelectedValue)
                                };

                                DB.Clientes.Add(cliente);

                                if (DB.SaveChanges() > 0)
                                {
                                    CheckChange();
                                    limpiar();
                                    limpiarBusqueda();
                                    MessageBox.Show("Cliente agregado correctamente!", "Registro de Clientes", MessageBoxButtons.OK);
                                    cliente = null;
                                }
                                else
                                {
                                    MessageBox.Show("Cliente No fue agregado", "Error Registro de Clientes", MessageBoxButtons.OK);
                                    cliente = null;
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                throw;
                            }
                        }
                    }
                }
                else
                {
                    if (Validaciones.IsValidEmail(txtEmail.Text.Trim()))
                    {
                        DialogResult respuesta = MessageBox.Show("¿Deseas agregar la empresa o cliente/a " + $"{txtName.Text.Trim()} ?",
                                           "Registro de Clientes", MessageBoxButtons.YesNo);
                        if (respuesta == DialogResult.Yes)
                        {
                            using (FrmLoading frmLoading = new FrmLoading(Wait))
                            {
                                try
                                {
                                    cliente = new Clientes
                                    {
                                        Identificacion = txtIdent.Text.Trim(),
                                        Nombre = txtName.Text.Trim(),
                                        IdTipoCliente = Convert.ToInt32(CboxCustomerType.SelectedValue),
                                        Telefono = txtMainPhone.Text.Trim(),
                                        Telefono2 = txtSecondPhone.Text.Trim(),
                                        Correo = txtEmail.Text.Trim(),
                                        Direccion = txtAddress.Text.Trim(),
                                        Detalles = txtDetails.Text.Trim(),
                                        IdEstado = Convert.ToInt32(CboxStates.SelectedValue)
                                    };

                                    DB.Clientes.Add(cliente);

                                    if (DB.SaveChanges() > 0)
                                    {
                                        CheckChange();
                                        limpiar();
                                        limpiarBusqueda();
                                        MessageBox.Show("Cliente agregado correctamente!", "Registro de Clientes", MessageBoxButtons.OK);
                                        cliente = null;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Cliente No fue agregado", "Error Registro de Clientes", MessageBoxButtons.OK);
                                        cliente = null;
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    throw;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Correo no posee un formato correcto, por favor valida que contenga '@' y que contenga dominio correcto.", "Error Registro de Clientes", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void imgUpdate_Click(object sender, EventArgs e)
        {
            if (ValidarCamposRequeridos())
            {
                if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
                {
                    DialogResult respuesta = MessageBox.Show("¿Deseas Modificar la empresa o cliente/a " + $"{txtName.Text.Trim()} ?",
                                       "Registro de Clientes", MessageBoxButtons.YesNo);
                    if (respuesta == DialogResult.Yes)
                    {
                        using (FrmLoading frmLoading = new FrmLoading(Wait))
                        {
                            try
                            {

                                cliente.Identificacion = txtIdent.Text.Trim();
                                cliente.Nombre = txtName.Text.Trim();
                                cliente.IdTipoCliente = Convert.ToInt32(CboxCustomerType.SelectedValue);
                                cliente.Telefono = txtMainPhone.Text.Trim();
                                cliente.Telefono2 = txtSecondPhone.Text.Trim();
                                cliente.Correo = txtEmail.Text.Trim();
                                cliente.Direccion = txtAddress.Text.Trim();
                                cliente.Detalles = txtDetails.Text.Trim();
                                cliente.IdEstado = Convert.ToInt32(CboxStates.SelectedValue);



                                DB.Entry(cliente).State = EntityState.Modified;

                                if (DB.SaveChanges() > 0)
                                {
                                    CheckChange();
                                    limpiar();
                                    limpiarBusqueda();
                                    MessageBox.Show("Cliente modificado correctamente!", "Registro de Clientes", MessageBoxButtons.OK);
                                    cliente = null;
                                }
                                else
                                {
                                    MessageBox.Show("Cliente No fue modificado", "Error Registro de Clientes", MessageBoxButtons.OK);
                                    cliente = null;
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                throw;
                            }
                        }
                    }

                }
                else
                {
                    if (Validaciones.IsValidEmail(txtEmail.Text.Trim()))
                    {
                        DialogResult respuesta = MessageBox.Show("¿Deseas Modificar la empresa o cliente/a " + $"{txtName.Text.Trim()} ?",
                                           "Registro de Clientes", MessageBoxButtons.YesNo);
                        if (respuesta == DialogResult.Yes)
                        {
                            using (FrmLoading frmLoading = new FrmLoading(Wait))
                            {
                                try
                                {

                                    cliente.Identificacion = txtIdent.Text.Trim();
                                    cliente.Nombre = txtName.Text.Trim();
                                    cliente.IdTipoCliente = Convert.ToInt32(CboxCustomerType.SelectedValue);
                                    cliente.Telefono = txtMainPhone.Text.Trim();
                                    cliente.Telefono2 = txtSecondPhone.Text.Trim();
                                    cliente.Correo = txtEmail.Text.Trim();
                                    cliente.Direccion = txtAddress.Text.Trim();
                                    cliente.Detalles = txtDetails.Text.Trim();
                                    cliente.IdEstado = Convert.ToInt32(CboxStates.SelectedValue);



                                    DB.Entry(cliente).State = EntityState.Modified;

                                    if (DB.SaveChanges() > 0)
                                    {
                                        CheckChange();
                                        limpiar();
                                        limpiarBusqueda();
                                        MessageBox.Show("Cliente modificado correctamente!", "Registro de Clientes", MessageBoxButtons.OK);
                                        cliente = null;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Cliente No fue modificado", "Error Registro de Clientes", MessageBoxButtons.OK);
                                        cliente = null;
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    throw;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Correo no posee un formato correcto, por favor valida que contenga '@' y que contenga dominio correcto.", "Error Registro de Clientes", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void imgDelete_Click(object sender, EventArgs e)
        {
            if (ValidarCamposRequeridos())
            {
                DialogResult respuesta = MessageBox.Show("¿Deseas eliminar el cliente " + $"{txtName.Text.Trim()} ?" +
                    Environment.NewLine + "Si lo eliminas, no prodras recuperar nuevamente sus datos...",
                    "Registro de Clientes", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes)
                {
                    using (FrmLoading frmLoading = new FrmLoading(Wait))
                    {
                        try
                        {
                            if (cliente == null)
                            {
                                MessageBox.Show("Cliente No existe, o no ha sido seleccionado de la lista", "Error Registro de Clientes", MessageBoxButtons.OK);
                            }
                            else
                            {
                                //linq que consulta si hay relacion con alguna tabla.
                                var list = from fa in DB.Facturas
                                           join cl in DB.Clientes
                                           on fa.IdCliente equals cl.IdCliente
                                           where (cl.IdCliente == cliente.IdCliente)
                                           select new
                                           {
                                               fa.IdFactura,
                                           };

                                if (list.ToList().Count <= 0)
                                {
                                    DB.Clientes.Remove(cliente); // metodo para eliminar el cliente, dato de la BD
                                    if (DB.SaveChanges() > 0)
                                    {
                                        CheckChange();
                                        limpiarBusqueda();
                                        MessageBox.Show("Cliente Eliminado Correctamente!", "Registro de Clientes", MessageBoxButtons.OK);
                                        cliente = null;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Cliente No fue Eliminado, por favor valide", "Error Registro de Clientes", MessageBoxButtons.OK);
                                        cliente = null;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Cliente No fue Eliminado, este ya se encuentra relacionado a una factura que se emitió anteriormente.",
                                        "Error Registro de Clientes", MessageBoxButtons.OK);
                                } 
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw;
                        }
                    }
                }
                else
                {
                    if (cliente != null && cliente.IdEstado == 1)
                    {
                        DialogResult respuesta2 = MessageBox.Show("¿Deseas inactivar el cliente " + $"{txtName.Text.Trim()} ?",
                        "Registro de Clientes", MessageBoxButtons.YesNo);

                        if (respuesta2 == DialogResult.Yes)
                        {
                            using (FrmLoading frmLoading = new FrmLoading(Wait))
                            {
                                try
                                {

                                    cliente.Identificacion = txtIdent.Text.Trim();
                                    cliente.Nombre = txtName.Text.Trim();
                                    cliente.IdTipoCliente = Convert.ToInt32(CboxCustomerType.SelectedValue);
                                    cliente.Telefono = txtMainPhone.Text.Trim();
                                    cliente.Telefono2 = txtSecondPhone.Text.Trim();
                                    cliente.Correo = txtEmail.Text.Trim();
                                    cliente.Direccion = txtAddress.Text.Trim();
                                    cliente.Detalles = txtDetails.Text.Trim();
                                    cliente.IdEstado = 2; // 2 es inactivo

                                    DB.Entry(cliente).State = EntityState.Modified;

                                    if (DB.SaveChanges() > 0)
                                    {
                                        CheckChange();
                                        limpiar();
                                        limpiarBusqueda();
                                        MessageBox.Show("Cliente modificado correctamente!", "Registro de Clientes", MessageBoxButtons.OK);
                                        cliente = null;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Cliente No fue modificado", "Error Registro de Clientes", MessageBoxButtons.OK);
                                        cliente = null;
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    throw;
                                }
                            }
                        }
                    }  
                }
            }

        }


        //al cerrar el form
        private void FrmCustomersManage_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
        }

        private void imgExit_Click(object sender, EventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
        }

        //Validaciones de Ingreso de Datos en los TXT

        private void txtIdent_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, true, false);
        }

        private void txtMainPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }

        private void txtSecondPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, false, true);
        }

        private void txtIdCustomerSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }

        private void txtIdentSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, true, false);
        }

        private void txtIdCustomerSearch_TextChanged(object sender, EventArgs e)
        {          
           
            if (ChActivos.Checked == true && !string.IsNullOrEmpty(txtIdCustomerSearch.Text.Trim()) && txtIdCustomerSearch.Text.Count() > 0)
            {
                txtIdentSearch.Enabled = false;
                txtNameSearch.Enabled = false;
                int num = Convert.ToInt32(txtIdCustomerSearch.Text.Trim());
                var result = from cl in DB.Clientes
                             join es in DB.Estados
                             on cl.IdEstado equals es.IdEstado
                             where (cl.IdEstado == 1 && cl.IdCliente == num)
                             select new
                             {
                                 cl.IdCliente,
                                 cl.Identificacion,
                                 cl.Nombre,
                                 //Lambda Expresion IF -ELSE para validar tipo de Cliente y proceder a indicarlo en modo texto
                                 TipoCliente = (cl.IdTipoCliente == 1) ? "Físico" : (cl.IdTipoCliente == 2) ? "Júridico" : "",
                                 cl.Telefono,
                                 cl.Telefono2,
                                 cl.Correo,
                                 cl.Direccion,
                                 cl.Detalles,
                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                             };
                dgvClientes.DataSource = result.ToList();
                limpiar();
            }
            else
            {
                if (ChActivos.Checked == false && !string.IsNullOrEmpty(txtIdCustomerSearch.Text.Trim()) && txtIdCustomerSearch.Text.Count() > 0)
                {
                    txtIdentSearch.Enabled = false;
                    txtNameSearch.Enabled = false;
                    int num = Convert.ToInt32(txtIdCustomerSearch.Text.Trim());
                    var result = from cl in DB.Clientes
                                 join es in DB.Estados
                                 on cl.IdEstado equals es.IdEstado
                                 where (cl.IdEstado == 2 && cl.IdCliente == num)
                                 select new
                                 {
                                     cl.IdCliente,
                                     cl.Identificacion,
                                     cl.Nombre,
                                     //Lambda Expresion IF -ELSE para validar tipo de Cliente y proceder a indicarlo en modo texto
                                     TipoCliente = (cl.IdTipoCliente == 1) ? "Físico" : (cl.IdTipoCliente == 2) ? "Júridico" : "",
                                     cl.Telefono,
                                     cl.Telefono2,
                                     cl.Correo,
                                     cl.Direccion,
                                     cl.Detalles,
                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                 };
                    dgvClientes.DataSource = result.ToList();
                    limpiar();
                }
                else
                {
                    if (string.IsNullOrEmpty(txtIdCustomerSearch.Text.Trim()) && txtIdCustomerSearch.Text.Count() == 0)
                    {
                        txtIdentSearch.Enabled = true;
                        txtNameSearch.Enabled = true;
                        CheckChange();
                    }   
                }
            }
        }

        private void txtIdentSearch_TextChanged(object sender, EventArgs e)
        {
           
            if (ChActivos.Checked == true && !string.IsNullOrEmpty(txtIdentSearch.Text.Trim()) && txtIdentSearch.Text.Count() > 0)
            {
                txtIdCustomerSearch.Enabled = false;
                txtNameSearch.Enabled = false;
                string num = txtIdentSearch.Text.Trim();
                var result = from cl in DB.Clientes
                             join es in DB.Estados
                             on cl.IdEstado equals es.IdEstado
                             where (cl.IdEstado == 1 && cl.Identificacion.Contains(num))
                             select new
                             {
                                 cl.IdCliente,
                                 cl.Identificacion,
                                 cl.Nombre,
                                 //Lambda Expresion IF -ELSE para validar tipo de Cliente y proceder a indicarlo en modo texto
                                 TipoCliente = (cl.IdTipoCliente == 1) ? "Físico" : (cl.IdTipoCliente == 2) ? "Júridico" : "",
                                 cl.Telefono,
                                 cl.Telefono2,
                                 cl.Correo,
                                 cl.Direccion,
                                 cl.Detalles,
                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                             };
                dgvClientes.DataSource = result.ToList();
                limpiar();
            }
            else
            {
                if (ChActivos.Checked == false && !string.IsNullOrEmpty(txtIdentSearch.Text.Trim()) && txtIdentSearch.Text.Count() > 0)
                {
                    txtIdCustomerSearch.Enabled = false;
                    txtNameSearch.Enabled = false;
                    string num = txtIdentSearch.Text.Trim();
                    var result = from cl in DB.Clientes
                                 join es in DB.Estados
                                 on cl.IdEstado equals es.IdEstado
                                 where (cl.IdEstado == 2 && cl.Identificacion.Contains(num))
                                 select new
                                 {
                                     cl.IdCliente,
                                     cl.Identificacion,
                                     cl.Nombre,
                                     //Lambda Expresion IF -ELSE para validar tipo de Cliente y proceder a indicarlo en modo texto
                                     TipoCliente = (cl.IdTipoCliente == 1) ? "Físico" : (cl.IdTipoCliente == 2) ? "Júridico" : "",
                                     cl.Telefono,
                                     cl.Telefono2,
                                     cl.Correo,
                                     cl.Direccion,
                                     cl.Detalles,
                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                 };
                    dgvClientes.DataSource = result.ToList();
                    limpiar();
                }
                else
                {
                    if (string.IsNullOrEmpty(txtIdentSearch.Text.Trim()) && txtIdentSearch.Text.Count() == 0)
                    {
                        txtIdCustomerSearch.Enabled = true;
                        txtNameSearch.Enabled = true;
                        CheckChange();
                    }
                }
            }
        }

        private void txtNameSearch_TextChanged(object sender, EventArgs e)
        {
           
            if (ChActivos.Checked == true && !string.IsNullOrEmpty(txtNameSearch.Text.Trim()) && txtNameSearch.Text.Count() > 0)
            {
                txtIdentSearch.Enabled = false;
                txtIdCustomerSearch.Enabled = false;
                string num = txtNameSearch.Text.Trim();
                var result = from cl in DB.Clientes
                             join es in DB.Estados
                             on cl.IdEstado equals es.IdEstado
                             where (cl.IdEstado == 1 && cl.Nombre.Contains(num))
                             select new
                             {
                                 cl.IdCliente,
                                 cl.Identificacion,
                                 cl.Nombre,
                                 //Lambda Expresion IF -ELSE para validar tipo de Cliente y proceder a indicarlo en modo texto
                                 TipoCliente = (cl.IdTipoCliente == 1) ? "Físico" : (cl.IdTipoCliente == 2) ? "Júridico" : "",
                                 cl.Telefono,
                                 cl.Telefono2,
                                 cl.Correo,
                                 cl.Direccion,
                                 cl.Detalles,
                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                             };
                dgvClientes.DataSource = result.ToList();
                limpiar();
            }
            else
            {
                if (ChActivos.Checked == false && !string.IsNullOrEmpty(txtNameSearch.Text.Trim()) && txtNameSearch.Text.Count() > 0)
                {
                    txtIdentSearch.Enabled = false;
                    txtIdCustomerSearch.Enabled = false;
                    string num = txtNameSearch.Text.Trim();
                    var result = from cl in DB.Clientes
                                 join es in DB.Estados
                                 on cl.IdEstado equals es.IdEstado
                                 where (cl.IdEstado == 2 && cl.Nombre.Contains(num))
                                 select new
                                 {
                                     cl.IdCliente,
                                     cl.Identificacion,
                                     cl.Nombre,
                                     //Lambda Expresion IF -ELSE para validar tipo de Cliente y proceder a indicarlo en modo texto
                                     TipoCliente = (cl.IdTipoCliente == 1) ? "Físico" : (cl.IdTipoCliente == 2) ? "Júridico" : "",
                                     cl.Telefono,
                                     cl.Telefono2,
                                     cl.Correo,
                                     cl.Direccion,
                                     cl.Detalles,
                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                 };
                    dgvClientes.DataSource = result.ToList();
                    limpiar();
                }
                else
                {
                    if (string.IsNullOrEmpty(txtNameSearch.Text.Trim()) && txtNameSearch.Text.Count() == 0)
                    {
                        txtIdentSearch.Enabled = true;
                        txtIdCustomerSearch.Enabled = true;
                        CheckChange();
                    }
                }
            }
        }

        private void txtIdent_TextChanged(object sender, EventArgs e)
        {
            if (txtIdent.Text.Length <= 15 && txtIdent.Text.Length >= 7)
            {
                string[] tipoClases = {"2-100","2-200", "2-300", "2-400", "3-002", "3-003", "3-004", "3-005", "3-006", "3-007", "3-008", "3-009", "3-010", "3-011", "3-012", "3-013",
            "3-014","3-101","3-102","3-103","3-104","3-105","3-106","3-107","3-108","3-109","3-110","4-000","5-001",
            "2100","2200", "2300", "2400", "3002", "3003", "3004", "3005", "3006", "3007", "3008", "3009", "3010", "3011", "3012", "3013",
            "3014","3101","3102","3103","3104","3105","3106","3107","3108","3109","3110","4000","5001",
            "2-100-","2-200-", "2-300-", "2-400-", "3-002-", "3-003-", "3-004-", "3-005-", "3-006-", "3-007-", "3-008-", "3-009-", "3-010-", "3-011-", "3-012-", "3-013-",
            "3-014-","3-101-","3-102-","3-103-","3-104-","3-105-","3-106-","3-107-","3-108-","3-109-","3-110-","4-000-","5-001-",
                };
                string identificacion = txtIdent.Text.Trim();

                string existe = tipoClases.Where((x) => x.Contains(identificacion.Substring(0, 4)) || x.StartsWith(identificacion.Substring(0, 4))).FirstOrDefault();

                if (existe != null)
                {
                    if (identificacion.Length >= 10 && identificacion.Length <= 12)
                    {
                        string subString0 = identificacion.Substring(0, 4);
                        string subString1 = identificacion.Substring(0, 5);
                        string subString2 = identificacion.Substring(0, 6);


                        string esJuridico0 = tipoClases.Where((x) => x.Contains(subString0) || x.StartsWith(subString0)).FirstOrDefault();
                        string esJuridico1 = tipoClases.Where((x) => x.Contains(subString1) || x.StartsWith(subString1)).FirstOrDefault();
                        string esJuridico2 = tipoClases.Where((x) => x.Contains(subString2) || x.StartsWith(subString2)).FirstOrDefault();

                        if (esJuridico1 != null || esJuridico2 != null || esJuridico0 != null)
                        {
                            CargarTiposJuridico();
                        }
                        else
                        {
                            CargarTiposFisico();
                        }
                    }
                    else
                    {
                        CargarTiposFisico();
                    }
                }
                else
                {
                    CargarTiposFisico();
                }
            }
            else
            {
                CargarTiposFisico();
            }


        }
    }
}
