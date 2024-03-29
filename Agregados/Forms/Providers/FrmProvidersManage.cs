﻿using Agregados.Forms.Loading;
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

namespace Agregados.Forms.Providers
{
    public partial class FrmProvidersManage : Form
    {
        //variables del form
        AgregadosEntities DB;
        Proveedores proveedor;
        public FrmProvidersManage()
        {
            InitializeComponent();
            DB = new AgregadosEntities();
            proveedor = new Proveedores();
        }

        //cuando se cierre el form op presion el boton salir
        private void FrmProvidersManage_FormClosing(object sender, FormClosingEventArgs e)
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
        //cuando se abra el form
        private void FrmProvidersManage_Load(object sender, EventArgs e)
        {
            CargarEstadosProveedores();
            CargarTipos();

            // linq para validar y disenar mejor la DataGridView al usuario // empezando la informacion con Estado ACTIVO y lo unico que se necesita obtener
            //para agilizar la respuesta y no obtener tantas columnas de datos
            var result = from pr in DB.Proveedores
                         join es in DB.Estados
                         on pr.IdEstado equals es.IdEstado
                         where (pr.IdEstado == 1)
                         select new
                         {
                             pr.IdProveedor,
                             pr.Identificacion,
                             pr.Nombre,
                             //Lambda Expresion IF -ELSE para validar tipo de Cliente y proceder a indicarlo en modo texto
                             TipoProveedor = (pr.IdTipoProveedor == 1) ? "Físico" : (pr.IdTipoProveedor == 2) ? "Júridico" : "",
                             pr.Telefono,
                             pr.Telefono2,
                             pr.Correo,
                             pr.Direccion,
                             pr.Detalles,
                             IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                         };
            dgvProveedores.DataSource = result.ToList();
            limpiar();
        }
        //cuando se seleccione un elemento de datagrid
        private void dgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProveedores.SelectedRows.Count == 1)
            {
                proveedor = new Proveedores();

                DataGridViewRow MiFila = dgvProveedores.SelectedRows[0];

                int IdProveedor = Convert.ToInt32(MiFila.Cells["CIdProveedor"].Value);

                //una vez tenemos el valor del ID, se llama a una funcion 
                //de consultar por ID que entrega como retorno un objeto de tipo cliente
                //en este caso voy a reutilizar el objeto de cliente local
                //para cargar datos por medio de la funcion 


                //ESTE METODO de consultor RETORNA UN OBJETO de tipo Empleado
                proveedor = DB.Proveedores.FirstOrDefault(x => x.IdProveedor == IdProveedor);

                if (proveedor != null && proveedor.IdProveedor > 0)
                {
                    //una vez me asegure que el objeto posee datos, entonces se procede a representar
                    //en pantalla
                    txtIdent.Text = proveedor.Identificacion.ToString();
                    txtName.Text = proveedor.Nombre.ToString();
                    CboxProviderType.SelectedValue = proveedor.IdTipoProveedor;
                    txtMainPhone.Text = proveedor.Telefono.ToString();
                    txtSecondPhone.Text = proveedor.Telefono2.ToString();
                    txtEmail.Text = proveedor.Correo.ToString();
                    txtAddress.Text = proveedor.Direccion.ToString();
                    txtDetails.Text = proveedor.Detalles.ToString();
                    CboxStates.SelectedValue = proveedor.IdEstado;

                    ActivarUpdateDelete();
                }
            }
        }


        //carga Cbox Estados
        private void CargarEstadosProveedores()
        {

            //Metodo que permite llamar y obtener los datos filtrados de los proiveedores y mostrarlos en el comboBox
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

            CboxProviderType.DataSource = dt;
            CboxProviderType.ValueMember = "Id";
            CboxProviderType.DisplayMember = "D";
            CboxProviderType.SelectedIndex = -1;
            */

            //Metodo que permite llamar y obtener los datos filtrados y mostrarlos en el comboBox
            var dt = DB.TiposProveedores.Where(x => x.IdTipoProveedor == 1 || x.IdTipoProveedor == 2).ToList();

            CboxProviderType.ValueMember = "IdTipoProveedor";
            CboxProviderType.DisplayMember = "TipoProveedor";
            CboxProviderType.DataSource = dt;
            CboxProviderType.SelectedIndex = -1;

        }

        private void CargarTiposFisico()
        {

            //Metodo que permite llamar y obtener los datos filtrados y mostrarlos en el comboBox
            var dt = DB.TiposProveedores.Where(x => x.IdTipoProveedor == 1).ToList();

            CboxProviderType.ValueMember = "IdTipoProveedor";
            CboxProviderType.DisplayMember = "TipoProveedor";
            CboxProviderType.DataSource = dt;
            CboxProviderType.SelectedIndex = -1;

        }

        private void CargarTiposJuridico()
        {

            //Metodo que permite llamar y obtener los datos filtrados y mostrarlos en el comboBox
            var dt = DB.TiposProveedores.Where(x => x.IdTipoProveedor == 2).ToList();

            CboxProviderType.ValueMember = "IdTipoProveedor";
            CboxProviderType.DisplayMember = "TipoProveedor";
            CboxProviderType.DataSource = dt;
            CboxProviderType.SelectedIndex = -1;

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

        //tiempo loading
        void Wait()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(5);
            }
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
            CboxProviderType.SelectedValue = -1;
            CboxStates.SelectedValue = -1;

            ActivarAdd();

        }
        private void limpiarBusqueda()
        {
            txtIdProviderSearch.Text = null;
            txtIdentSearch.Text = null;
            txtNameSearch.Text = null;

            txtIdProviderSearch.Enabled = true;
            txtIdentSearch.Enabled = true;
            txtNameSearch.Enabled = true;
        }

        private void imgClean_Click(object sender, EventArgs e)
        {
            limpiar();
            limpiarBusqueda();
        }


        //metodo que permite realizar validaciones de los espacion / campos del form no se toma en cuenta
        private bool ValidarCamposRequeridos()
        {
            bool R = false;

            if (!string.IsNullOrEmpty(txtName.Text.Trim()) &&
                CboxProviderType.SelectedIndex != -1 &&
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
                    MessageBox.Show("Nombre de Proveedor es Requerido", "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtName.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtMainPhone.Text.Trim()))
                {
                    MessageBox.Show("Teléfono Principal es Requerido", "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMainPhone.Focus();
                    return false;
                }
                if (CboxProviderType.SelectedIndex == -1)
                {
                    MessageBox.Show("Tipo de Proveedor debe ser ingresado", "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CboxProviderType.Focus();
                    return false;
                }
                if (CboxStates.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe Seleccionar un estado del Proveedor", "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var result = from cl in DB.Proveedores
                             join es in DB.Estados
                             on cl.IdEstado equals es.IdEstado
                             where (cl.IdEstado == 1)
                             select new
                             {
                                 cl.IdProveedor,
                                 cl.Identificacion,
                                 cl.Nombre,
                                 //Lambda Expresion IF -ELSE para validar tipo de proveedor y proceder a indicarlo en modo texto
                                 TipoProveedor = (cl.IdTipoProveedor == 1) ? "Físico" : (cl.IdTipoProveedor == 2) ? "Júridico" : "",
                                 cl.Telefono,
                                 cl.Telefono2,
                                 cl.Correo,
                                 cl.Direccion,
                                 cl.Detalles,
                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                             };
                dgvProveedores.DataSource = result.ToList();
                limpiar();
            }
            else
            {
                if (ChActivos.Checked == false)
                {
                    var result = from cl in DB.Proveedores
                                 join es in DB.Estados
                                 on cl.IdEstado equals es.IdEstado
                                 where (cl.IdEstado == 2)
                                 select new
                                 {
                                     cl.IdProveedor,
                                     cl.Identificacion,
                                     cl.Nombre,
                                     //Lambda Expresion IF -ELSE para validar tipo de proveedor y proceder a indicarlo en modo texto
                                     TipoProveedor = (cl.IdTipoProveedor == 1) ? "Físico" : (cl.IdTipoProveedor == 2) ? "Júridico" : "",
                                     cl.Telefono,
                                     cl.Telefono2,
                                     cl.Correo,
                                     cl.Direccion,
                                     cl.Detalles,
                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                 };
                    dgvProveedores.DataSource = result.ToList();
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
                    DialogResult respuesta = MessageBox.Show("¿Deseas agregar el proveedor " + $"{txtName.Text.Trim()} ?",
                                                           "Registro de Proveedores", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        using (FrmLoading frmLoading = new FrmLoading(Wait))
                        {
                            try
                            {
                                proveedor = new Proveedores
                                {
                                    Identificacion = txtIdent.Text.Trim(),
                                    Nombre = txtName.Text.Trim(),
                                    IdTipoProveedor = Convert.ToInt32(CboxProviderType.SelectedValue),
                                    Telefono = txtMainPhone.Text.Trim(),
                                    Telefono2 = txtSecondPhone.Text.Trim(),
                                    Correo = txtEmail.Text.Trim(),
                                    Direccion = txtAddress.Text.Trim(),
                                    Detalles = txtDetails.Text.Trim(),
                                    IdEstado = Convert.ToInt32(CboxStates.SelectedValue)
                                };

                                DB.Proveedores.Add(proveedor);

                                if (DB.SaveChanges() > 0)
                                {
                                    CheckChange();
                                    limpiar();
                                    limpiarBusqueda();
                                    MessageBox.Show("Proveedor agregado correctamente!", "Registro de Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    proveedor = null;
                                }
                                else
                                {
                                    MessageBox.Show("Proveedor No fue agregado", "Error Registro de Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    proveedor = null;
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                throw;
                            }
                        }
                    }
                }
                else
                {
                    if (Validaciones.IsValidEmail(txtEmail.Text.Trim()))
                    {
                        DialogResult respuesta = MessageBox.Show("¿Deseas agregar el proveedor " + $"{txtName.Text.Trim()} ?",
                                           "Registro de Proveedores", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {
                            using (FrmLoading frmLoading = new FrmLoading(Wait))
                            {
                                try
                                {
                                    proveedor = new Proveedores
                                    {
                                        Identificacion = txtIdent.Text.Trim(),
                                        Nombre = txtName.Text.Trim(),
                                        IdTipoProveedor = Convert.ToInt32(CboxProviderType.SelectedValue),
                                        Telefono = txtMainPhone.Text.Trim(),
                                        Telefono2 = txtSecondPhone.Text.Trim(),
                                        Correo = txtEmail.Text.Trim(),
                                        Direccion = txtAddress.Text.Trim(),
                                        Detalles = txtDetails.Text.Trim(),
                                        IdEstado = Convert.ToInt32(CboxStates.SelectedValue)
                                    };

                                    DB.Proveedores.Add(proveedor);

                                    if (DB.SaveChanges() > 0)
                                    {
                                        CheckChange();
                                        limpiar();
                                        limpiarBusqueda();
                                        MessageBox.Show("Proveedor agregado correctamente!", "Registro de Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        proveedor = null;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Proveedor No fue agregado", "Error Registro de Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        proveedor = null;
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    throw;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Correo no posee un formato correcto, por favor valida que contenga '@' y que contenga dominio correcto.", 
                            "Error Registro de Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    DialogResult respuesta = MessageBox.Show("¿Deseas Modificar el Proveedor " + $"{txtName.Text.Trim()} ?",
                                       "Registro de Proveedores", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        using (FrmLoading frmLoading = new FrmLoading(Wait))
                        {
                            try
                            {

                                proveedor.Identificacion = txtIdent.Text.Trim();
                                proveedor.Nombre = txtName.Text.Trim();
                                proveedor.IdTipoProveedor = Convert.ToInt32(CboxProviderType.SelectedValue);
                                proveedor.Telefono = txtMainPhone.Text.Trim();
                                proveedor.Telefono2 = txtSecondPhone.Text.Trim();
                                proveedor.Correo = txtEmail.Text.Trim();
                                proveedor.Direccion = txtAddress.Text.Trim();
                                proveedor.Detalles = txtDetails.Text.Trim();
                                proveedor.IdEstado = Convert.ToInt32(CboxStates.SelectedValue);



                                DB.Entry(proveedor).State = EntityState.Modified;

                                if (DB.SaveChanges() > 0)
                                {
                                    CheckChange();
                                    limpiar();
                                    limpiarBusqueda();
                                    MessageBox.Show("Proveedor modificado correctamente!", "Registro de Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    proveedor = null;
                                }
                                else
                                {
                                    MessageBox.Show("Proveedor No fue modificado", "Error Registro de Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    proveedor = null;
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                throw;
                            }
                        }
                    }
                }
                else
                {
                    if (Validaciones.IsValidEmail(txtEmail.Text.Trim()))
                    {
                        DialogResult respuesta = MessageBox.Show("¿Deseas Modificar el Proveedor " + $"{txtName.Text.Trim()} ?",
                                           "Registro de Proveedores", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {
                            using (FrmLoading frmLoading = new FrmLoading(Wait))
                            {
                                try
                                {

                                    proveedor.Identificacion = txtIdent.Text.Trim();
                                    proveedor.Nombre = txtName.Text.Trim();
                                    proveedor.IdTipoProveedor = Convert.ToInt32(CboxProviderType.SelectedValue);
                                    proveedor.Telefono = txtMainPhone.Text.Trim();
                                    proveedor.Telefono2 = txtSecondPhone.Text.Trim();
                                    proveedor.Correo = txtEmail.Text.Trim();
                                    proveedor.Direccion = txtAddress.Text.Trim();
                                    proveedor.Detalles = txtDetails.Text.Trim();
                                    proveedor.IdEstado = Convert.ToInt32(CboxStates.SelectedValue);



                                    DB.Entry(proveedor).State = EntityState.Modified;

                                    if (DB.SaveChanges() > 0)
                                    {
                                        CheckChange();
                                        limpiar();
                                        limpiarBusqueda();
                                        MessageBox.Show("Proveedor modificado correctamente!", "Registro de Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        proveedor = null;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Proveedor No fue modificado", "Error Registro de Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        proveedor = null;
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    throw;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Correo no posee un formato correcto, por favor valida que contenga '@' y que contenga dominio correcto.", "Error Registro de Proveedores", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void imgDelete_Click(object sender, EventArgs e)
        {
            if (ValidarCamposRequeridos())
            {
                DialogResult respuesta = MessageBox.Show("¿Deseas eliminar el Proveedor " + $"{txtName.Text.Trim()} ?" +
                    Environment.NewLine + "Si lo eliminas, no prodras recuperar nuevamente sus datos...",
                    "Registro de Proveedores", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    using (FrmLoading frmLoading = new FrmLoading(Wait))
                    {
                        try
                        {
                            if (proveedor == null)
                            {
                                MessageBox.Show("Proveedor No existe, o no ha sido seleccionado de la lista",
                                    "Error Registro de Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                //linq que consulta si hay relacion con alguna tabla.
                                var list = from fa in DB.Facturas
                                           join pr in DB.Proveedores
                                           on fa.IdFactura equals pr.IdProveedor
                                           where (pr.IdProveedor == proveedor.IdProveedor)
                                           select new
                                           {
                                               fa.IdFactura,
                                           };

                                if (list.ToList().Count <= 0)
                                {
                                    DB.Proveedores.Remove(proveedor); // metodo para eliminar el cliente, dato de la BD
                                    if (DB.SaveChanges() > 0)
                                    {
                                        CheckChange();
                                        limpiarBusqueda();
                                        MessageBox.Show("Proveedor Eliminado Correctamente!", 
                                            "Registro de Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        proveedor = null;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Proveedor No fue Eliminado, por favor valide", 
                                            "Error Registro de Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        proveedor = null;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Proveedor No fue Eliminado, este ya se encuentra relacionado a un que se emitió anteriormente.", 
                                        "Error Registro de Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw;
                        }
                    }
                }
                else
                {
                    if (proveedor != null && proveedor.IdEstado == 1)
                    {
                        DialogResult respuesta2 = MessageBox.Show("¿Deseas inactivar el proveedor " + $"{txtName.Text.Trim()} ?",
                        "Registro de Proveedores", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (respuesta2 == DialogResult.Yes)
                        {
                            using (FrmLoading frmLoading = new FrmLoading(Wait))
                            {
                                try
                                {

                                    proveedor.Identificacion = txtIdent.Text.Trim();
                                    proveedor.Nombre = txtName.Text.Trim();
                                    proveedor.IdTipoProveedor = Convert.ToInt32(CboxProviderType.SelectedValue);
                                    proveedor.Telefono = txtMainPhone.Text.Trim();
                                    proveedor.Telefono2 = txtSecondPhone.Text.Trim();
                                    proveedor.Correo = txtEmail.Text.Trim();
                                    proveedor.Direccion = txtAddress.Text.Trim();
                                    proveedor.Detalles = txtDetails.Text.Trim();
                                    proveedor.IdEstado = 2; // 2 es inactivo

                                    DB.Entry(proveedor).State = EntityState.Modified;

                                    if (DB.SaveChanges() > 0)
                                    {
                                        CheckChange();
                                        limpiar();
                                        limpiarBusqueda();
                                        MessageBox.Show("Proveedor modificado correctamente!",
                                            "Registro de Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        proveedor = null;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Proveedor No fue modificado",
                                            "Error Registro de Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        proveedor = null;
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    throw;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void txtIdProviderSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }

        //Validaciones de Ingreso de Datos en los TXT
        private void txtIdentSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, true, false);
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, false, true);
        }

        private void txtMainPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }

        private void txtSecondPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }

        private void txtNameSearch_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        //EN CASO DE QUE SE LLEGUE A INGRESAR ALGUN DATOS EN LOS TXT DE BUSQUEDA FILTRO
        private void txtIdProviderSearch_TextChanged(object sender, EventArgs e)
        {
            if (ChActivos.Checked == true && !string.IsNullOrEmpty(txtIdProviderSearch.Text.Trim()) && txtIdProviderSearch.Text.Count() > 0)
            {
                txtIdentSearch.Enabled = false;
                txtNameSearch.Enabled = false;
                int num = Convert.ToInt32(txtIdProviderSearch.Text.Trim());
                var result = from cl in DB.Proveedores
                             join es in DB.Estados
                             on cl.IdEstado equals es.IdEstado
                             where (cl.IdEstado == 1 && cl.IdProveedor == num)
                             select new
                             {
                                 cl.IdProveedor,
                                 cl.Identificacion,
                                 cl.Nombre,
                                 //Lambda Expresion IF -ELSE para validar tipo de Proveedor y proceder a indicarlo en modo texto
                                 TipoProveedor = (cl.IdTipoProveedor == 1) ? "Físico" : (cl.IdTipoProveedor == 2) ? "Júridico" : "",
                                 cl.Telefono,
                                 cl.Telefono2,
                                 cl.Correo,
                                 cl.Direccion,
                                 cl.Detalles,
                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                             };
                dgvProveedores.DataSource = result.ToList();
                limpiar();
            }
            else
            {
                if (ChActivos.Checked == false && !string.IsNullOrEmpty(txtIdProviderSearch.Text.Trim()) && txtIdProviderSearch.Text.Count() > 0)
                {
                    txtIdentSearch.Enabled = false;
                    txtNameSearch.Enabled = false;
                    int num = Convert.ToInt32(txtIdProviderSearch.Text.Trim());
                    var result = from cl in DB.Proveedores
                                 join es in DB.Estados
                                 on cl.IdEstado equals es.IdEstado
                                 where (cl.IdEstado == 2 && cl.IdProveedor == num)
                                 select new
                                 {
                                     cl.IdProveedor,
                                     cl.Identificacion,
                                     cl.Nombre,
                                     //Lambda Expresion IF -ELSE para validar tipo de pROVEEDOR y proceder a indicarlo en modo texto
                                     TipoProveedor = (cl.IdTipoProveedor == 1) ? "Físico" : (cl.IdTipoProveedor == 2) ? "Júridico" : "",
                                     cl.Telefono,
                                     cl.Telefono2,
                                     cl.Correo,
                                     cl.Direccion,
                                     cl.Detalles,
                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                 };
                    dgvProveedores.DataSource = result.ToList();
                    limpiar();
                }
                else
                {
                    if (string.IsNullOrEmpty(txtIdProviderSearch.Text.Trim()) && txtIdProviderSearch.Text.Count() == 0)
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
                txtIdProviderSearch.Enabled = false;
                txtNameSearch.Enabled = false;
                string num = txtIdentSearch.Text.Trim();
                var result = from cl in DB.Proveedores
                             join es in DB.Estados
                             on cl.IdEstado equals es.IdEstado
                             where (cl.IdEstado == 1 && cl.Identificacion.Contains(num))
                             select new
                             {
                                 cl.IdProveedor,
                                 cl.Identificacion,
                                 cl.Nombre,
                                 //Lambda Expresion IF -ELSE para validar tipo de Cliente y proceder a indicarlo en modo texto
                                 TipoProveedor = (cl.IdTipoProveedor == 1) ? "Físico" : (cl.IdTipoProveedor == 2) ? "Júridico" : "",
                                 cl.Telefono,
                                 cl.Telefono2,
                                 cl.Correo,
                                 cl.Direccion,
                                 cl.Detalles,
                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                             };
                dgvProveedores.DataSource = result.ToList();
                limpiar();
            }
            else
            {
                if (ChActivos.Checked == false && !string.IsNullOrEmpty(txtIdentSearch.Text.Trim()) && txtIdentSearch.Text.Count() > 0)
                {
                    txtIdProviderSearch.Enabled = false;
                    txtNameSearch.Enabled = false;
                    string num = txtIdentSearch.Text.Trim();
                    var result = from cl in DB.Proveedores
                                 join es in DB.Estados
                                 on cl.IdEstado equals es.IdEstado
                                 where (cl.IdEstado == 2 && cl.Identificacion.Contains(num))
                                 select new
                                 {
                                     cl.IdProveedor,
                                     cl.Identificacion,
                                     cl.Nombre,
                                     //Lambda Expresion IF -ELSE para validar tipo de Cliente y proceder a indicarlo en modo texto
                                     TipoProveedor = (cl.IdTipoProveedor == 1) ? "Físico" : (cl.IdTipoProveedor == 2) ? "Júridico" : "",
                                     cl.Telefono,
                                     cl.Telefono2,
                                     cl.Correo,
                                     cl.Direccion,
                                     cl.Detalles,
                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                 };
                    dgvProveedores.DataSource = result.ToList();
                    limpiar();
                }
                else
                {
                    if (string.IsNullOrEmpty(txtIdentSearch.Text.Trim()) && txtIdentSearch.Text.Count() == 0)
                    {
                        txtIdProviderSearch.Enabled = true;
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
                txtIdProviderSearch.Enabled = false;
                string num = txtNameSearch.Text.Trim();
                var result = from cl in DB.Proveedores
                             join es in DB.Estados
                             on cl.IdEstado equals es.IdEstado
                             where (cl.IdEstado == 1 && cl.Nombre.Contains(num))
                             select new
                             {
                                 cl.IdProveedor,
                                 cl.Identificacion,
                                 cl.Nombre,
                                 //Lambda Expresion IF -ELSE para validar tipo de Cliente y proceder a indicarlo en modo texto
                                 TipoProveedor = (cl.IdTipoProveedor == 1) ? "Físico" : (cl.IdTipoProveedor == 2) ? "Júridico" : "",
                                 cl.Telefono,
                                 cl.Telefono2,
                                 cl.Correo,
                                 cl.Direccion,
                                 cl.Detalles,
                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                             };
                dgvProveedores.DataSource = result.ToList();
                limpiar();
            }
            else
            {
                if (ChActivos.Checked == false && !string.IsNullOrEmpty(txtNameSearch.Text.Trim()) && txtNameSearch.Text.Count() > 0)
                {
                    txtIdentSearch.Enabled = false;
                    txtIdProviderSearch.Enabled = false;
                    string num = txtNameSearch.Text.Trim();
                    var result = from cl in DB.Proveedores
                                 join es in DB.Estados
                                 on cl.IdEstado equals es.IdEstado
                                 where (cl.IdEstado == 2 && cl.Nombre.Contains(num))
                                 select new
                                 {
                                     cl.IdProveedor,
                                     cl.Identificacion,
                                     cl.Nombre,
                                     //Lambda Expresion IF -ELSE para validar tipo de Cliente y proceder a indicarlo en modo texto
                                     TipoProveedor = (cl.IdTipoProveedor == 1) ? "Físico" : (cl.IdTipoProveedor == 2) ? "Júridico" : "",
                                     cl.Telefono,
                                     cl.Telefono2,
                                     cl.Correo,
                                     cl.Direccion,
                                     cl.Detalles,
                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                 };
                    dgvProveedores.DataSource = result.ToList();
                    limpiar();
                }
                else
                {
                    if (string.IsNullOrEmpty(txtNameSearch.Text.Trim()) && txtNameSearch.Text.Count() == 0)
                    {
                        txtIdentSearch.Enabled = true;
                        txtIdProviderSearch.Enabled = true;
                        CheckChange();
                    }
                }
            }

        }

    
        //Validacion de tipo de proveedor
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
