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

namespace Agregados.Forms.Materials
{
    public partial class FrmMaterialsManage : Form
    {
        //variables del form
        AgregadosEntities DB;
        Material material;
        public FrmMaterialsManage()
        {
            InitializeComponent();
            DB = new AgregadosEntities();
            material = new Material();
        }

        //cuando se cierre el form op presion el boton salir
        private void imgExit_Click(object sender, EventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
        }
        private void FrmMaterialsManage_Click(object sender, EventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
        }

        //carga Cbox Estados
        private void CargarEstadosMateriales()
        {

            //Metodo que permite llamar y obtener los datos filtrados de los clientes y mostrarlos en el comboBox
            var dt = DB.Estados.Where(x => x.IdEstado == 9 || x.IdEstado == 10 || x.IdEstado == 11).ToList();

            CboxStates.ValueMember = "IdEstado";
            CboxStates.DisplayMember = "NombreEstado";
            CboxStates.DataSource = dt;
            CboxStates.SelectedIndex = -1;
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


        private void FrmMaterialsManage_Load(object sender, EventArgs e)
        {
            CargarEstadosMateriales();
            // linq para validar y disenar mejor la DataGridView al usuario // empezando la informacion con Estado ACTIVO y lo unico que se necesita obtener
            //para agilizar la respuesta y no obtener tantas columnas de datos
            var result = from ma in DB.Materials
                         join es in DB.Estados
                         on ma.IdEstado equals es.IdEstado
                         where (ma.IdEstado == 11)
                         select new
                         {
                             ma.IdMaterial,
                             ma.NombreMaterial,
                             ma.CantidadMaterial,
                             ma.Minimos,
                             ma.Precio,
                             IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                         //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                         };
            dgvMaterials.DataSource = result.ToList();
            limpiar();
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
            txtName.Text = null;
            txtCantidad.Text = null;
            txtMinimos.Text = null;
            txtPrecio.Text = null;
            CboxStates.SelectedValue = -1;

            ActivarAdd();

        }
        private void limpiarBusqueda()
        {
            txtIdMaterialSearch.Text = null;
            txtNameSearch.Text = null;

            txtIdMaterialSearch.Enabled = true;
            txtNameSearch.Enabled = true;
            chCantRegular.Checked = false;
            chVacia.Checked = false;
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
                !string.IsNullOrEmpty(txtCantidad.Text.Trim()) &&
                !string.IsNullOrEmpty(txtMinimos.Text.Trim()) &&
                !string.IsNullOrEmpty(txtPrecio.Text.Trim()) &&
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
                    MessageBox.Show("Nombre de Material es Requerido", "Error de Validación!", MessageBoxButtons.OK);
                    txtName.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtCantidad.Text.Trim()))
                {
                    MessageBox.Show("Cantidad de Material es Requerido", "Error de Validación!", MessageBoxButtons.OK);
                    txtCantidad.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtMinimos.Text.Trim()))
                {
                    MessageBox.Show("Cantidad de Mínimos es Requerido", "Error de Validación!", MessageBoxButtons.OK);
                    txtMinimos.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtPrecio.Text.Trim()))
                {
                    MessageBox.Show("Precio de Material es Requerido", "Error de Validación!", MessageBoxButtons.OK);
                    txtPrecio.Focus();
                    return false;
                }
                if (CboxStates.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe Seleccionar un estado del Material", "Error de Validación!", MessageBoxButtons.OK);
                    CboxStates.Focus();
                    return false;
                }
            }
            return R;
        }


        //change de los checkBoxes
        private void CheckChange()
        {
            if (ChCantBuena.Checked == true && chCantRegular.Checked == false && chVacia.Checked == false)
            {
                var result = from ma in DB.Materials
                             join es in DB.Estados
                             on ma.IdEstado equals es.IdEstado
                             where (ma.IdEstado == 11)
                             select new
                             {
                                 ma.IdMaterial,
                                 ma.NombreMaterial,
                                 ma.CantidadMaterial,
                                 ma.Minimos,
                                 ma.Precio,
                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                             //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                 
                             };
                dgvMaterials.DataSource = result.ToList();
                limpiar();
            }
            else
            {
                if (ChCantBuena.Checked == true && chCantRegular.Checked == true && chVacia.Checked == false)
                {
                    var result = from ma in DB.Materials
                                 join es in DB.Estados
                                 on ma.IdEstado equals es.IdEstado
                                 where (ma.IdEstado == 11 || ma.IdEstado == 10)
                                 select new
                                 {
                                     ma.IdMaterial,
                                     ma.NombreMaterial,
                                     ma.CantidadMaterial,
                                     ma.Minimos,
                                     ma.Precio,
                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                 //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                 };
                    dgvMaterials.DataSource = result.ToList();
                    limpiar();
                }
                else
                {
                    if (ChCantBuena.Checked == true && chCantRegular.Checked == true && chVacia.Checked == true)
                    {
                        var result = from ma in DB.Materials
                                     join es in DB.Estados
                                     on ma.IdEstado equals es.IdEstado
                                     where (ma.IdEstado == 11 || ma.IdEstado == 10 || ma.IdEstado == 9)
                                     select new
                                     {
                                         ma.IdMaterial,
                                         ma.NombreMaterial,
                                         ma.CantidadMaterial,
                                         ma.Minimos,
                                         ma.Precio,
                                         IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                     //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                     };
                        dgvMaterials.DataSource = result.ToList();
                        limpiar();
                    }
                    else
                    {
                        if (ChCantBuena.Checked == false && chCantRegular.Checked == false && chVacia.Checked == false)
                        {
                            var result = from ma in DB.Materials
                                         join es in DB.Estados
                                         on ma.IdEstado equals es.IdEstado
                                         where (ma.IdEstado != 11 || ma.IdEstado != 10 || ma.IdEstado != 9)
                                         select new
                                         {
                                             ma.IdMaterial,
                                             ma.NombreMaterial,
                                             ma.CantidadMaterial,
                                             ma.Minimos,
                                             ma.Precio,
                                             IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                         //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                         };
                            dgvMaterials.DataSource = result.ToList();
                            limpiar();
                        }
                        else
                        {
                            if (ChCantBuena.Checked == false && chCantRegular.Checked == true && chVacia.Checked == false)
                            {
                                var result = from ma in DB.Materials
                                             join es in DB.Estados
                                             on ma.IdEstado equals es.IdEstado
                                             where (ma.IdEstado == 10)
                                             select new
                                             {
                                                 ma.IdMaterial,
                                                 ma.NombreMaterial,
                                                 ma.CantidadMaterial,
                                                 ma.Minimos,
                                                 ma.Precio,
                                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                             //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                             };
                                dgvMaterials.DataSource = result.ToList();
                                limpiar();
                            }
                            else
                            {
                                if (ChCantBuena.Checked == false && chCantRegular.Checked == false && chVacia.Checked == true)
                                {
                                    var result = from ma in DB.Materials
                                                 join es in DB.Estados
                                                 on ma.IdEstado equals es.IdEstado
                                                 where (ma.IdEstado == 9)
                                                 select new
                                                 {
                                                     ma.IdMaterial,
                                                     ma.NombreMaterial,
                                                     ma.CantidadMaterial,
                                                     ma.Minimos,
                                                     ma.Precio,
                                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                 //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                                 };
                                    dgvMaterials.DataSource = result.ToList();
                                    limpiar();
                                }
                                else
                                {
                                    if (ChCantBuena.Checked == false && chCantRegular.Checked == true && chVacia.Checked == true)
                                    {
                                        var result = from ma in DB.Materials
                                                     join es in DB.Estados
                                                     on ma.IdEstado equals es.IdEstado
                                                     where (ma.IdEstado == 9 || ma.IdEstado == 10)
                                                     select new
                                                     {
                                                         ma.IdMaterial,
                                                         ma.NombreMaterial,
                                                         ma.CantidadMaterial,
                                                         ma.Minimos,
                                                         ma.Precio,
                                                         IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                     //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                                     };
                                        dgvMaterials.DataSource = result.ToList();
                                        limpiar();
                                    }
                                    else
                                    {
                                        if (ChCantBuena.Checked == true && chCantRegular.Checked == false && chVacia.Checked == true)
                                        {
                                            var result = from ma in DB.Materials
                                                         join es in DB.Estados
                                                         on ma.IdEstado equals es.IdEstado
                                                         where (ma.IdEstado == 9 || ma.IdEstado == 11)
                                                         select new
                                                         {
                                                             ma.IdMaterial,
                                                             ma.NombreMaterial,
                                                             ma.CantidadMaterial,
                                                             ma.Minimos,
                                                             ma.Precio,
                                                             IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                         //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                                         };
                                            dgvMaterials.DataSource = result.ToList();
                                            limpiar();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void ChActivos_CheckedChanged(object sender, EventArgs e)
        {
            CheckChange();
        }
        private void chCantRegular_CheckedChanged(object sender, EventArgs e)
        {
            CheckChange();
        }
        private void chVacia_CheckedChanged(object sender, EventArgs e)
        {
            CheckChange();
        }


        //metodo add, delete & update
        private void imgDelete_Click(object sender, EventArgs e)
        {
            //todo se debe de validar que MATERIAL no tenga facturas, sino entonces no se puede eliminar
            if (ValidarCamposRequeridos())
            {
                DialogResult respuesta = MessageBox.Show("¿Deseas eliminar el Material " + $"{txtName.Text.Trim()} ?" +
                    Environment.NewLine + "Si lo eliminas, no prodras recuperar nuevamente sus datos...",
                    "Registro de Materiales", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes)
                {
                    using (FrmLoading frmLoading = new FrmLoading(Wait))
                    {
                        try
                        {
                            if (material == null)
                            {
                                MessageBox.Show("Material No existe, o no ha sido seleccionado de la lista", "Error Registro de Materiales", MessageBoxButtons.OK);
                            }
                            else
                            {
                                DB.Materials.Remove(material); // metodo para eliminar el material, dato de la BD
                                if (DB.SaveChanges() > 0)
                                {
                                    CheckChange();
                                    limpiarBusqueda();
                                    MessageBox.Show("Material Eliminado Correctamente!", "Registro de Materiales", MessageBoxButtons.OK);
                                    material = null;
                                }
                                else
                                {
                                    MessageBox.Show("Material No fue Eliminado, por favor valide", "Error Registro de Materiales", MessageBoxButtons.OK);
                                    material = null;
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

        private void imgUpdate_Click(object sender, EventArgs e)
        {
            if (ValidarCamposRequeridos())
            {
                DialogResult respuesta = MessageBox.Show("¿Deseas Modificar el Material " + $"{txtName.Text.Trim()} ?",
                                    "Registro de Materiales", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes)
                {
                    using (FrmLoading frmLoading = new FrmLoading(Wait))
                    {
                        try
                        {
                            material.NombreMaterial = txtName.Text.Trim();
                            material.CantidadMaterial = Convert.ToDecimal(Convert.ToDouble(txtCantidad.Text.Trim()));
                            material.Minimos = Convert.ToDecimal(Convert.ToDouble(txtMinimos.Text.Trim()));
                            material.Precio = Convert.ToDecimal(Convert.ToDouble(txtPrecio.Text.Trim()));
                            material.IdEstado = Convert.ToInt32(CboxStates.SelectedValue);



                            DB.Entry(material).State = EntityState.Modified;

                            if (DB.SaveChanges() > 0)
                            {
                                CheckChange();
                                limpiar();
                                limpiarBusqueda();
                                MessageBox.Show("Material modificado correctamente!", "Registro de Materiales", MessageBoxButtons.OK);
                                material = null;
                            }
                            else
                            {
                                MessageBox.Show("Material No fue modificado", "Error Registro de Materiales", MessageBoxButtons.OK);
                                material = null;
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

        private void imgAdd_Click(object sender, EventArgs e)
        {
            if (ValidarCamposRequeridos())
            {
                DialogResult respuesta = MessageBox.Show("¿Deseas agregar el Material " + $"{txtName.Text.Trim()} ?",
                                    "Registro de Materiales", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes)
                {
                    using (FrmLoading frmLoading = new FrmLoading(Wait))
                    {
                        try
                        {
                            material = new Material
                            {
                                NombreMaterial = txtName.Text.Trim(),
                                CantidadMaterial = Convert.ToDecimal(Convert.ToDouble(txtCantidad.Text.Trim())), //DOBLE CAST PARA EVITAR PROBLEMAS CON DECIMAL
                                Minimos = Convert.ToDecimal(Convert.ToDouble(txtMinimos.Text.Trim())),
                                Precio = Convert.ToDecimal(Convert.ToDouble(txtPrecio.Text.Trim())),
                                IdEstado = Convert.ToInt32(CboxStates.SelectedValue)
                            };

                            DB.Materials.Add(material);

                            if (DB.SaveChanges() > 0)
                            {
                                CheckChange();
                                limpiar();
                                limpiarBusqueda();
                                MessageBox.Show("Material agregado correctamente!", "Registro de Materiales", MessageBoxButtons.OK);
                                material = null;
                            }
                            else
                            {
                                MessageBox.Show("Material No fue agregado", "Error Registro de Materiales", MessageBoxButtons.OK);
                                material = null;
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

        private void dgvMaterials_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgvMaterials.SelectedRows.Count == 1)
            {
                material = new Material();

                DataGridViewRow MiFila = dgvMaterials.SelectedRows[0];

                int IdMaterial = Convert.ToInt32(MiFila.Cells["CIdMaterial"].Value);

                //una vez tenemos el valor del ID, se llama a una funcion 
                //de consultar por ID que entrega como retorno un objeto de tipo cliente
                //en este caso voy a reutilizar el objeto de cliente local
                //para cargar datos por medio de la funcion 


                //ESTE METODO de consultor RETORNA UN OBJETO de tipo material
                material = DB.Materials.FirstOrDefault(x => x.IdMaterial == IdMaterial);

                if (material != null && material.IdMaterial > 0)
                {
                    //una vez me asegure que el objeto posee datos, entonces se procede a representar
                    //en pantalla
                    txtName.Text = material.NombreMaterial.ToString();
                    txtCantidad.Text = material.CantidadMaterial.ToString();
                    txtMinimos.Text = material.Minimos.ToString();
                    txtPrecio.Text = material.Precio.ToString();
                    CboxStates.SelectedValue = material.IdEstado;

                    ActivarUpdateDelete();
                }
            }
        }
        
        //EN CASO DE QUE SE LLEGUE A INGRESAR ALGUN DATOS EN LOS TXT DE BUSQUEDA FILTRO
        private void txtIdMaterialSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e,true);
        }

        private void txtNameSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtIdMaterialSearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdMaterialSearch.Text.Trim()) && txtIdMaterialSearch.Text.Count() > 0)
            {
                txtNameSearch.Enabled = false;
                int num = Convert.ToInt32(txtIdMaterialSearch.Text.Trim());

                if (ChCantBuena.Checked == true && chCantRegular.Checked == false && chVacia.Checked == false)
                {
                    var result = from ma in DB.Materials
                                 join es in DB.Estados
                                 on ma.IdEstado equals es.IdEstado
                                 where (ma.IdEstado == 11 && ma.IdMaterial == num)
                                 select new
                                 {
                                     ma.IdMaterial,
                                     ma.NombreMaterial,
                                     ma.CantidadMaterial,
                                     ma.Minimos,
                                     ma.Precio,
                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                 //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                 };
                    dgvMaterials.DataSource = result.ToList();
                    limpiar();
                }
                else
                {
                    if (ChCantBuena.Checked == true && chCantRegular.Checked == true && chVacia.Checked == false)
                    {
                        var result = from ma in DB.Materials
                                     join es in DB.Estados
                                     on ma.IdEstado equals es.IdEstado
                                     where ((ma.IdEstado == 11 || ma.IdEstado == 10) && ma.IdMaterial == num)
                                     select new
                                     {
                                         ma.IdMaterial,
                                         ma.NombreMaterial,
                                         ma.CantidadMaterial,
                                         ma.Minimos,
                                         ma.Precio,
                                         IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                     //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                     };
                        dgvMaterials.DataSource = result.ToList();
                        limpiar();
                    }
                    else
                    {
                        if (ChCantBuena.Checked == true && chCantRegular.Checked == true && chVacia.Checked == true)
                        {
                            var result = from ma in DB.Materials
                                         join es in DB.Estados
                                         on ma.IdEstado equals es.IdEstado
                                         where ((ma.IdEstado == 11 || ma.IdEstado == 10 || ma.IdEstado == 9) && ma.IdMaterial == num)
                                         select new
                                         {
                                             ma.IdMaterial,
                                             ma.NombreMaterial,
                                             ma.CantidadMaterial,
                                             ma.Minimos,
                                             ma.Precio,
                                             IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                         //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                         };
                            dgvMaterials.DataSource = result.ToList();
                            limpiar();
                        }
                        else
                        {
                            if (ChCantBuena.Checked == false && chCantRegular.Checked == false && chVacia.Checked == false)
                            {
                                var result = from ma in DB.Materials
                                             join es in DB.Estados
                                             on ma.IdEstado equals es.IdEstado
                                             where ((ma.IdEstado != 11 || ma.IdEstado != 10 || ma.IdEstado != 9) && ma.IdMaterial == num)
                                             select new
                                             {
                                                 ma.IdMaterial,
                                                 ma.NombreMaterial,
                                                 ma.CantidadMaterial,
                                                 ma.Minimos,
                                                 ma.Precio,
                                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                             //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                             };
                                dgvMaterials.DataSource = result.ToList();
                                limpiar();
                            }
                            else
                            {
                                if (ChCantBuena.Checked == false && chCantRegular.Checked == true && chVacia.Checked == false)
                                {
                                    var result = from ma in DB.Materials
                                                 join es in DB.Estados
                                                 on ma.IdEstado equals es.IdEstado
                                                 where (ma.IdEstado == 10 && ma.IdMaterial == num)
                                                 select new
                                                 {
                                                     ma.IdMaterial,
                                                     ma.NombreMaterial,
                                                     ma.CantidadMaterial,
                                                     ma.Minimos,
                                                     ma.Precio,
                                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                 //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                                 };
                                    dgvMaterials.DataSource = result.ToList();
                                    limpiar();
                                }
                                else
                                {
                                    if (ChCantBuena.Checked == false && chCantRegular.Checked == false && chVacia.Checked == true)
                                    {
                                        var result = from ma in DB.Materials
                                                     join es in DB.Estados
                                                     on ma.IdEstado equals es.IdEstado
                                                     where (ma.IdEstado == 9 && ma.IdMaterial == num)
                                                     select new
                                                     {
                                                         ma.IdMaterial,
                                                         ma.NombreMaterial,
                                                         ma.CantidadMaterial,
                                                         ma.Minimos,
                                                         ma.Precio,
                                                         IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                     //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                                     };
                                        dgvMaterials.DataSource = result.ToList();
                                        limpiar();
                                    }
                                    else
                                    {
                                        if (ChCantBuena.Checked == false && chCantRegular.Checked == true && chVacia.Checked == true)
                                        {
                                            var result = from ma in DB.Materials
                                                         join es in DB.Estados
                                                         on ma.IdEstado equals es.IdEstado
                                                         where ((ma.IdEstado == 9 || ma.IdEstado == 10) && ma.IdMaterial == num)
                                                         select new
                                                         {
                                                             ma.IdMaterial,
                                                             ma.NombreMaterial,
                                                             ma.CantidadMaterial,
                                                             ma.Minimos,
                                                             ma.Precio,
                                                             IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                         //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                                         };
                                            dgvMaterials.DataSource = result.ToList();
                                            limpiar();
                                        }
                                        else
                                        {
                                            if (ChCantBuena.Checked == true && chCantRegular.Checked == false && chVacia.Checked == true)
                                            {
                                                var result = from ma in DB.Materials
                                                             join es in DB.Estados
                                                             on ma.IdEstado equals es.IdEstado
                                                             where ((ma.IdEstado == 9 || ma.IdEstado == 11) && ma.IdMaterial == num)
                                                             select new
                                                             {
                                                                 ma.IdMaterial,
                                                                 ma.NombreMaterial,
                                                                 ma.CantidadMaterial,
                                                                 ma.Minimos,
                                                                 ma.Precio,
                                                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                             //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                                             };
                                                dgvMaterials.DataSource = result.ToList();
                                                limpiar();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtIdMaterialSearch.Text.Trim()) && txtIdMaterialSearch.Text.Count() == 0)
                {
                    txtNameSearch.Enabled = true;
                    CheckChange();
                }
            }
        }

        private void txtNameSearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNameSearch.Text.Trim()) && txtNameSearch.Text.Count() > 0)
            {
                txtIdMaterialSearch.Enabled = false;
                string num = txtNameSearch.Text.Trim();

                if (ChCantBuena.Checked == true && chCantRegular.Checked == false && chVacia.Checked == false)
                {
                    var result = from ma in DB.Materials
                                 join es in DB.Estados
                                 on ma.IdEstado equals es.IdEstado
                                 where (ma.IdEstado == 11 && ma.NombreMaterial.Contains(num))
                                 select new
                                 {
                                     ma.IdMaterial,
                                     ma.NombreMaterial,
                                     ma.CantidadMaterial,
                                     ma.Minimos,
                                     ma.Precio,
                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                 //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                 };
                    dgvMaterials.DataSource = result.ToList();
                    limpiar();
                }
                else
                {
                    if (ChCantBuena.Checked == true && chCantRegular.Checked == true && chVacia.Checked == false)
                    {
                        var result = from ma in DB.Materials
                                     join es in DB.Estados
                                     on ma.IdEstado equals es.IdEstado
                                     where ((ma.IdEstado == 11 || ma.IdEstado == 10) && ma.NombreMaterial.Contains(num))
                                     select new
                                     {
                                         ma.IdMaterial,
                                         ma.NombreMaterial,
                                         ma.CantidadMaterial,
                                         ma.Minimos,
                                         ma.Precio,
                                         IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                     //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                     };
                        dgvMaterials.DataSource = result.ToList();
                        limpiar();
                    }
                    else
                    {
                        if (ChCantBuena.Checked == true && chCantRegular.Checked == true && chVacia.Checked == true)
                        {
                            var result = from ma in DB.Materials
                                         join es in DB.Estados
                                         on ma.IdEstado equals es.IdEstado
                                         where ((ma.IdEstado == 11 || ma.IdEstado == 10 || ma.IdEstado == 9) && ma.NombreMaterial.Contains(num))
                                         select new
                                         {
                                             ma.IdMaterial,
                                             ma.NombreMaterial,
                                             ma.CantidadMaterial,
                                             ma.Minimos,
                                             ma.Precio,
                                             IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                         //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                         };
                            dgvMaterials.DataSource = result.ToList();
                            limpiar();
                        }
                        else
                        {
                            if (ChCantBuena.Checked == false && chCantRegular.Checked == false && chVacia.Checked == false)
                            {
                                var result = from ma in DB.Materials
                                             join es in DB.Estados
                                             on ma.IdEstado equals es.IdEstado
                                             where ((ma.IdEstado != 11 || ma.IdEstado != 10 || ma.IdEstado != 9) && ma.NombreMaterial.Contains(num))
                                             select new
                                             {
                                                 ma.IdMaterial,
                                                 ma.NombreMaterial,
                                                 ma.CantidadMaterial,
                                                 ma.Minimos,
                                                 ma.Precio,
                                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                             //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                             };
                                dgvMaterials.DataSource = result.ToList();
                                limpiar();
                            }
                            else
                            {
                                if (ChCantBuena.Checked == false && chCantRegular.Checked == true && chVacia.Checked == false)
                                {
                                    var result = from ma in DB.Materials
                                                 join es in DB.Estados
                                                 on ma.IdEstado equals es.IdEstado
                                                 where (ma.IdEstado == 10 && ma.NombreMaterial.Contains(num))
                                                 select new
                                                 {
                                                     ma.IdMaterial,
                                                     ma.NombreMaterial,
                                                     ma.CantidadMaterial,
                                                     ma.Minimos,
                                                     ma.Precio,
                                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                 //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                                 };
                                    dgvMaterials.DataSource = result.ToList();
                                    limpiar();
                                }
                                else
                                {
                                    if (ChCantBuena.Checked == false && chCantRegular.Checked == false && chVacia.Checked == true)
                                    {
                                        var result = from ma in DB.Materials
                                                     join es in DB.Estados
                                                     on ma.IdEstado equals es.IdEstado
                                                     where (ma.IdEstado == 9 && ma.NombreMaterial.Contains(num))
                                                     select new
                                                     {
                                                         ma.IdMaterial,
                                                         ma.NombreMaterial,
                                                         ma.CantidadMaterial,
                                                         ma.Minimos,
                                                         ma.Precio,
                                                         IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                     //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                                     };
                                        dgvMaterials.DataSource = result.ToList();
                                        limpiar();
                                    }
                                    else
                                    {
                                        if (ChCantBuena.Checked == false && chCantRegular.Checked == true && chVacia.Checked == true)
                                        {
                                            var result = from ma in DB.Materials
                                                         join es in DB.Estados
                                                         on ma.IdEstado equals es.IdEstado
                                                         where ((ma.IdEstado == 9 || ma.IdEstado == 10) && ma.NombreMaterial.Contains(num))
                                                         select new
                                                         {
                                                             ma.IdMaterial,
                                                             ma.NombreMaterial,
                                                             ma.CantidadMaterial,
                                                             ma.Minimos,
                                                             ma.Precio,
                                                             IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                         //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                                         };
                                            dgvMaterials.DataSource = result.ToList();
                                            limpiar();
                                        }
                                        else
                                        {
                                            if (ChCantBuena.Checked == true && chCantRegular.Checked == false && chVacia.Checked == true)
                                            {
                                                var result = from ma in DB.Materials
                                                             join es in DB.Estados
                                                             on ma.IdEstado equals es.IdEstado
                                                             where ((ma.IdEstado == 9 || ma.IdEstado == 11) && ma.NombreMaterial.Contains(num))
                                                             select new
                                                             {
                                                                 ma.IdMaterial,
                                                                 ma.NombreMaterial,
                                                                 ma.CantidadMaterial,
                                                                 ma.Minimos,
                                                                 ma.Precio,
                                                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                             //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto

                                                             };
                                                dgvMaterials.DataSource = result.ToList();
                                                limpiar();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtIdMaterialSearch.Text.Trim()) && txtIdMaterialSearch.Text.Count() == 0)
                {
                    txtNameSearch.Enabled = true;
                    CheckChange();
                }
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, false);
        }

        private void txtMinimos_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, false);
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, false);
        }

        private void FrmMaterialsManage_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
        }
    }
}