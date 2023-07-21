using Agregados.Forms.Bills;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Forms.Materials
{
    public partial class FrmMaterialSearch : Form
    {
        //variables del form
        AgregadosEntities DB;
        Materiales material;

        int accion; // si es 1 es agregar material cliente // 2 es para proveedores
        #region PropiedadesDeTotalizacion
        public decimal Cantidad { get; set; } //cantidad indicada por usuario
        public decimal CantidadLimite { get; set; } //cantidad limite disponible
        public decimal PrecioUnitario { set; get; }  // precio unitario por metro cubico
        public decimal SubTotal { get; set; } // subtotal
        public decimal TasaImpuesto { get; set; } //tasa aplicada al 13%
        public decimal Total { get; set; } // total aplicado con el el IVA 13%
        #endregion


        public FrmMaterialSearch(int numAccion)
        {
            InitializeComponent();
            DB = new AgregadosEntities();   
            material = new Materiales();
            NudCantidad.Enabled = false;
            ActivarCancel();
            accion = numAccion;
        }


        //validaciones de botones para evitar errores
        private void ActivarSelect()
        {
            btnSeleccionar.Enabled = true;
            btnCancelar.Enabled = true;
        }
        //validaciones de botones para evitar errores
        private void ActivarCancel()
        {
            btnSeleccionar.Enabled = false;
            btnCancelar.Enabled = true;
        }

        //limpiar el form, ventana
        private void limpiar()
        {
            NudCantidad.Enabled = false;
            NudCantidad.Value = 1;
            txtBuscarId.Text = null;
            txtName.Text = null;
            txtBuscarId.Enabled = true;
            txtName.Enabled = true;
            Cantidad = 0;
            CantidadLimite = 0;
            PrecioUnitario = 0;
            SubTotal = 0;
            TasaImpuesto = 0;
            Total = 0;
            TxtPrecioUnitario.Text = 0.ToString();
            TxtSubTotal.Text = 0.ToString();
            TxtIVA.Text = 0.ToString();
            TxtTotal.Text = 0.ToString();

            ActivarCancel();

        }


        private void imgClean_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        //filtro de bsucar por codigo pero valida que sea numero
        private void txtBuscarId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }
        //llena la lista de datos de cliente
        private void LlenarLista()
        {
            if (accion == 1)
            {
                try
                {
                    // linq para validar y disenar mejor la DataGridView al usuario // empezando la informacion con Estado ACTIVO y lo unico que se necesita obtener
                    //para agilizar la respuesta y no obtener tantas columnas de datos
                    var result = from ma in DB.Materiales
                                 join es in DB.Estados
                                 on ma.IdEstado equals es.IdEstado
                                 where (ma.IdEstado == 10 || ma.IdEstado == 11)
                                 select new
                                 {
                                     ma.IdMaterial,
                                     ma.NombreMaterial,
                                     ma.CantidadMaterial,
                                     ma.Minimos,
                                     ma.Precio,
                                     ma.IdEstado
                                 };
                    dgvListaMateriales.DataSource = result.ToList();
                    limpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    throw;
                }
            }
            else
            {
                if (accion == 2)
                {
                    try
                    {
                        // linq para validar y disenar mejor la DataGridView al usuario // empezando la informacion con Estado ACTIVO y lo unico que se necesita obtener
                        //para agilizar la respuesta y no obtener tantas columnas de datos
                        var result = from ma in DB.Materiales
                                     join es in DB.Estados
                                     on ma.IdEstado equals es.IdEstado
                                     where (ma.IdEstado == 10 || ma.IdEstado == 11 || ma.IdEstado == 9)
                                     select new
                                     {
                                         ma.IdMaterial,
                                         ma.NombreMaterial,
                                         ma.CantidadMaterial,
                                         ma.Minimos,
                                         ma.Precio,
                                         ma.IdEstado
                                     };
                        dgvListaMateriales.DataSource = result.ToList();
                        limpiar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        throw;
                    }
                }
            }
            
        }

        //load 
        private void FrmMaterialSearch_Load(object sender, EventArgs e)
        {
            LlenarLista();
        }
        //filtros de busqueda
        private void txtBuscarId_TextChanged(object sender, EventArgs e)
        {
            if (accion == 1)
            {
                try
                {
                    if (!string.IsNullOrEmpty(txtBuscarId.Text.Trim()) && txtBuscarId.TextLength > 0)
                    {
                        txtName.Enabled = false;
                        int num = Convert.ToInt32(txtBuscarId.Text.Trim());

                        // linq para validar y disenar mejor la DataGridView al usuario // empezando la informacion con Estado ACTIVO y lo unico que se necesita obtener
                        //para agilizar la respuesta y no obtener tantas columnas de datos
                        var result = from ma in DB.Materiales
                                     join es in DB.Estados
                                     on ma.IdEstado equals es.IdEstado
                                     where ((ma.IdEstado == 10 || ma.IdEstado == 11) && ma.IdMaterial == num)
                                     select new
                                     {
                                         ma.IdMaterial,
                                         ma.NombreMaterial,
                                         ma.CantidadMaterial,
                                         ma.Minimos,
                                         ma.Precio,
                                         ma.IdEstado
                                     };
                        dgvListaMateriales.DataSource = result.ToList();
                    }
                    else
                    {
                        txtName.Enabled = true;
                        LlenarLista();
                        limpiar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    throw;
                }
            }
            else
            {
                if (accion == 2)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(txtBuscarId.Text.Trim()) && txtBuscarId.TextLength > 0)
                        {
                            txtName.Enabled = false;
                            int num = Convert.ToInt32(txtBuscarId.Text.Trim());

                            // linq para validar y disenar mejor la DataGridView al usuario // empezando la informacion con Estado ACTIVO y lo unico que se necesita obtener
                            //para agilizar la respuesta y no obtener tantas columnas de datos
                            var result = from ma in DB.Materiales
                                         join es in DB.Estados
                                         on ma.IdEstado equals es.IdEstado
                                         where ((ma.IdEstado == 10 || ma.IdEstado == 11 || ma.IdEstado == 9) && ma.IdMaterial == num)
                                         select new
                                         {
                                             ma.IdMaterial,
                                             ma.NombreMaterial,
                                             ma.CantidadMaterial,
                                             ma.Minimos,
                                             ma.Precio,
                                             ma.IdEstado
                                         };
                            dgvListaMateriales.DataSource = result.ToList();
                        }
                        else
                        {
                            txtName.Enabled = true;
                            LlenarLista();
                            limpiar();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        throw;
                    }
                }
            }
            
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (accion == 1)
            {
                try
                {
                    if (!string.IsNullOrEmpty(txtName.Text.Trim()) && txtName.TextLength > 0)
                    {
                        txtBuscarId.Enabled = false;
                        string data = txtName.Text.Trim();

                        // linq para validar y disenar mejor la DataGridView al usuario // empezando la informacion con Estado ACTIVO y lo unico que se necesita obtener
                        //para agilizar la respuesta y no obtener tantas columnas de datos
                        var result = from ma in DB.Materiales
                                     join es in DB.Estados
                                     on ma.IdEstado equals es.IdEstado
                                     where ((ma.IdEstado == 10 || ma.IdEstado == 11) && ma.NombreMaterial.Contains(data))
                                     select new
                                     {
                                         ma.IdMaterial,
                                         ma.NombreMaterial,
                                         ma.CantidadMaterial,
                                         ma.Minimos,
                                         ma.Precio,
                                         ma.IdEstado
                                     };
                        dgvListaMateriales.DataSource = result.ToList();

                    }
                    else
                    {
                        txtName.Enabled = true;
                        LlenarLista();
                        limpiar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    throw;
                }
            }
            else
            {
                if (accion == 2)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(txtName.Text.Trim()) && txtName.TextLength > 0)
                        {
                            txtBuscarId.Enabled = false;
                            string data = txtName.Text.Trim();

                            // linq para validar y disenar mejor la DataGridView al usuario // empezando la informacion con Estado ACTIVO y lo unico que se necesita obtener
                            //para agilizar la respuesta y no obtener tantas columnas de datos
                            var result = from ma in DB.Materiales
                                         join es in DB.Estados
                                         on ma.IdEstado equals es.IdEstado
                                         where ((ma.IdEstado == 10 || ma.IdEstado == 11 || ma.IdEstado == 9) && ma.NombreMaterial.Contains(data))
                                         select new
                                         {
                                             ma.IdMaterial,
                                             ma.NombreMaterial,
                                             ma.CantidadMaterial,
                                             ma.Minimos,
                                             ma.Precio,
                                             ma.IdEstado
                                         };
                            dgvListaMateriales.DataSource = result.ToList();

                        }
                        else
                        {
                            txtName.Enabled = true;
                            LlenarLista();
                            limpiar();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        throw;
                    }
                }
            }
        }
        
        //valida si ya existe el item en el DATATABLE de facturacion
        private bool ValidarItemLista(int codigo)
        {
            bool R = true; //de que no hay repetido

            if (accion == 1)
            {
                foreach (DataRow Row in Globals.MifrmBillAdd.DtLista.Rows)
                {
                    if (Convert.ToInt32(Row["IdMaterial"]) == codigo)
                    {
                        R = false; // de que ya existe un item con ese ID en la datagridview de Factura
                    }

                }
            }
            else
            {
                if (accion == 2)
                {
                    foreach (DataRow Row in Globals.MifrmBillProviderAdd.DtListaProvedor.Rows)
                    {
                        if (Convert.ToInt32(Row["IdMaterial"]) == codigo)
                        {
                            R = false; // de que ya existe un item con ese ID en la datagridview de Factura
                        }

                    }
                }
            }
            return R;
        }


        //valida si ya existe el item en el DATATABLE de facturacion
        private bool ValidarItemListaTierra(int codigo)
        {
            bool R = true; //de que no hay repetido

            if (accion == 1)
            {             
                //valida que si se selecciono el check de tierra es para entonces cobrar al cliente por trabajo de tierra y no venderle tierra
                // si se quiere vender tierra deben estar si marcar el check y asi solamente seria para venderle tierra 
                if (Globals.MifrmBillAdd.chTierraNormal.Checked && codigo == 1)
                {
                    R = false;
                }

                if (Globals.MifrmBillAdd.chTierraRoja.Checked && codigo == 2)
                {
                    R = false;
                }
            }
            else
            {
                if (accion == 2)
                {
                    foreach (DataRow Row in Globals.MifrmBillProviderAdd.DtListaProvedor.Rows)
                    {
                        if (Convert.ToInt32(Row["IdMaterial"]) == codigo)
                        {
                            R = false; // de que ya existe un item con ese ID en la datagridview de Factura
                        }

                    }
                }
            }
            return R;
        }

        //calcular datos
        private void Calcular()
        {
            //Este método calcula los valores de
            //subtotal, impuesto y total para la línea 
            //no supere el 100%
            if (accion == 1)
            {
                if (dgvListaMateriales.SelectedRows.Count == 1)
                {

                    NudCantidad.Enabled = true;
                    DataGridViewRow fila = dgvListaMateriales.SelectedRows[0];
                    NudCantidad.Maximum = Convert.ToInt32(fila.Cells["CCantidadMaterial"].Value);
                    Cantidad = Convert.ToDecimal(NudCantidad.Value);
                    PrecioUnitario = Convert.ToDecimal(fila.Cells["CPrecio"].Value);

                    //1. Canculo del Subtotal 
                    SubTotal = Cantidad * PrecioUnitario;
                    TasaImpuesto = Convert.ToDecimal((Convert.ToDouble(SubTotal) * Convert.ToDouble(0.13)));
                    Total = Convert.ToDecimal((Convert.ToDouble(SubTotal) * Convert.ToDouble(0.13)) + Convert.ToDouble(SubTotal));

                    TxtPrecioUnitario.Text = string.Format("¢ {0:N2}", PrecioUnitario);
                    TxtSubTotal.Text = string.Format("¢ {0:N2}", SubTotal);
                    TxtIVA.Text = string.Format("¢ {0:N2}", TasaImpuesto);
                    TxtTotal.Text = string.Format("¢ {0:N2}", Total);
                }
            }
            else
            {
                if (accion == 2)
                {
                    if (dgvListaMateriales.SelectedRows.Count == 1)
                    {

                        NudCantidad.Enabled = true;
                        TxtPrecioUnitario.Enabled = true;

                        /*
                        DataGridViewRow fila = dgvListaMateriales.SelectedRows[0];
                        NudCantidad.Maximum = Convert.ToInt32(fila.Cells["CCantidadMaterial"].Value);
                        Cantidad = Convert.ToDecimal(NudCantidad.Value);
                        PrecioUnitario = Convert.ToDecimal(fila.Cells["CPrecio"].Value);
                        */
                        Cantidad = Convert.ToDecimal(NudCantidad.Value);
                        PrecioUnitario = Convert.ToDecimal(TxtPrecioUnitario.Text.Trim());
                        

                        //1. Canculo del Subtotal 
                        SubTotal = Cantidad * PrecioUnitario;
                        TasaImpuesto = Convert.ToDecimal((Convert.ToDouble(SubTotal) * Convert.ToDouble(0.13)));
                        Total = Convert.ToDecimal((Convert.ToDouble(SubTotal) * Convert.ToDouble(0.13)) + Convert.ToDouble(SubTotal));

                      
                        //TxtPrecioUnitario.Text = string.Format("¢ {0:N2}", PrecioUnitario);
                        TxtSubTotal.Text = string.Format("¢ {0:N2}", SubTotal);
                        TxtIVA.Text = string.Format("¢ {0:N2}", TasaImpuesto);
                        TxtTotal.Text = string.Format("¢ {0:N2}", Total);
                    }
                }
            }
        }

        //cancela el form, vuelve a la factura
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (Total <= 0 )
            {
                MessageBox.Show("No se puede agregar Material, ya que el Valor total es cero.",
                                "Validación de Material.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (accion == 1)//venta de material
                {
                    try
                    {
                        if (dgvListaMateriales.SelectedRows.Count == 1)
                        {
                            DataGridViewRow FilaSelected = dgvListaMateriales.SelectedRows[0];
                            int IdMaterial = Convert.ToInt32(FilaSelected.Cells["CIdMaterial"].Value);

                            if (ValidarItemListaTierra(IdMaterial))
                            {
                                if (ValidarItemLista(IdMaterial))
                                {
                                    DataRow NuevaFilaEnFacturacion = Globals.MifrmBillAdd.DtLista.NewRow();

                                    NuevaFilaEnFacturacion["IdMaterial"] = Convert.ToInt32(FilaSelected.Cells["CIdMaterial"].Value);
                                    NuevaFilaEnFacturacion["NombreMaterial"] = Convert.ToString(FilaSelected.Cells["CNombreMaterial"].Value);
                                    NuevaFilaEnFacturacion["CantidadMaterial"] = Convert.ToDecimal(NudCantidad.Value);
                                    NuevaFilaEnFacturacion["Precio"] = Convert.ToDecimal(PrecioUnitario);
                                    NuevaFilaEnFacturacion["Subtotal"] = Convert.ToDecimal(SubTotal);
                                    NuevaFilaEnFacturacion["IVA"] = Convert.ToDecimal(TasaImpuesto);
                                    NuevaFilaEnFacturacion["PrecioFinal"] = Convert.ToDecimal(Total);

                                    Globals.MifrmBillAdd.DtLista.Rows.Add(NuevaFilaEnFacturacion);

                                    this.DialogResult = DialogResult.OK;
                                }
                                else
                                {
                                    DialogResult respuesta = MessageBox.Show("Material ya existe en la lista de la factura." + Environment.NewLine +
                                        "¿Desea modificar los datos del material seleccionado, en la factura?",
                                        "Validación de Material.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (respuesta == DialogResult.Yes)
                                    {
                                        foreach (DataRow Row in Globals.MifrmBillAdd.DtLista.Rows)
                                        {
                                            if (Convert.ToInt32(Row["IdMaterial"]) == IdMaterial)
                                            {
                                                Row["NombreMaterial"] = Convert.ToString(FilaSelected.Cells["CNombreMaterial"].Value);
                                                Row["CantidadMaterial"] = Convert.ToDecimal(NudCantidad.Value);
                                                Row["Precio"] = Convert.ToDecimal(PrecioUnitario);
                                                Row["Subtotal"] = Convert.ToDecimal(SubTotal);
                                                Row["IVA"] = Convert.ToDecimal(TasaImpuesto);
                                                Row["PrecioFinal"] = Convert.ToDecimal(Total);
                                            }
                                        }
                                        this.DialogResult = DialogResult.OK;
                                        this.Hide();
                                    }
                                }
                            }
                            else
                            {
                                switch (IdMaterial)
                                {
                                    case 1:
                                        MessageBox.Show("No se puede agregar Material, ya que se tiene seleccionado trabajo con tierra normal o roja",
                                                                   "Validación de Material.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    case 2:
                                        MessageBox.Show("No se puede agregar Material, ya que se tiene seleccionado trabajo con tierra normal o roja",
                                                                   "Validación de Material.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    default:
                                        MessageBox.Show("Favor validar que no se tenga seleccionado el trabajo con tierra normal o roja",
                                                                  "Validación de Material.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        throw;
                    }
                }
                else
                {
                    if (accion == 2) // 2 simboliza que es ticket compra proveedor
                    {
                        try
                        {
                            if (dgvListaMateriales.SelectedRows.Count == 1)
                            {
                                DataGridViewRow FilaSelected = dgvListaMateriales.SelectedRows[0];
                                int IdMaterial = Convert.ToInt32(FilaSelected.Cells["CIdMaterial"].Value);

                                if (ValidarItemLista(IdMaterial))
                                {
                                    DataRow NuevaFilaEnFacturacion = Globals.MifrmBillProviderAdd.DtListaProvedor.NewRow();

                                    NuevaFilaEnFacturacion["IdMaterial"] = Convert.ToInt32(FilaSelected.Cells["CIdMaterial"].Value);
                                    NuevaFilaEnFacturacion["NombreMaterial"] = Convert.ToString(FilaSelected.Cells["CNombreMaterial"].Value);
                                    NuevaFilaEnFacturacion["CantidadMaterial"] = Convert.ToDecimal(NudCantidad.Value);
                                    NuevaFilaEnFacturacion["Precio"] = Convert.ToDecimal(PrecioUnitario);
                                    NuevaFilaEnFacturacion["Subtotal"] = Convert.ToDecimal(SubTotal);
                                    NuevaFilaEnFacturacion["IVA"] = Convert.ToDecimal(TasaImpuesto);
                                    NuevaFilaEnFacturacion["PrecioFinal"] = Convert.ToDecimal(Total);

                                    Globals.MifrmBillProviderAdd.DtListaProvedor.Rows.Add(NuevaFilaEnFacturacion);

                                    this.DialogResult = DialogResult.OK;
                                    this.Hide();
                                }
                                else
                                {
                                    DialogResult respuesta = MessageBox.Show("Material ya existe en la lista de la factura de ticket de compra." + Environment.NewLine +
                                        "¿Desea modificar los datos del material seleccionado, en la factura?",
                                        "Validación de Material.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (respuesta == DialogResult.Yes)
                                    {
                                        foreach (DataRow Row in Globals.MifrmBillProviderAdd.DtListaProvedor.Rows)
                                        {
                                            if (Convert.ToInt32(Row["IdMaterial"]) == IdMaterial)
                                            {
                                                Row["NombreMaterial"] = Convert.ToString(FilaSelected.Cells["CNombreMaterial"].Value);
                                                Row["CantidadMaterial"] = Convert.ToDecimal(NudCantidad.Value);
                                                Row["Precio"] = Convert.ToDecimal(PrecioUnitario);
                                                Row["Subtotal"] = Convert.ToDecimal(SubTotal);
                                                Row["IVA"] = Convert.ToDecimal(TasaImpuesto);
                                                Row["PrecioFinal"] = Convert.ToDecimal(Total);
                                            }
                                        }
                                        this.DialogResult = DialogResult.OK;
                                        this.Hide();
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            throw;
                        }
                    }
                }
            
            }
            
        }

        private void dgvListaMateriales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            limpiar();

            if (dgvListaMateriales.SelectedRows.Count == 1 && !string.IsNullOrEmpty(TxtPrecioUnitario.Text.Trim()))
            {
                ActivarSelect();
                Calcular();
            }
        }

        private void NudCantidad_ValueChanged(object sender, EventArgs e)
        {
            if (dgvListaMateriales.SelectedRows.Count == 1 && !string.IsNullOrEmpty(TxtPrecioUnitario.Text.Trim()))
            {
                Calcular();
            }
            else
            {
                TxtPrecioUnitario.Text = "0";
            }
        }

        private void TxtPrecioUnitario_ValueChanged(object sender, EventArgs e)
        {
            if (dgvListaMateriales.SelectedRows.Count == 1 && !string.IsNullOrEmpty(TxtPrecioUnitario.Text.Trim()))
            {
                Calcular();
            }
            else
            {
                TxtPrecioUnitario.Text = "0";
            }
        }

        private void TxtPrecioUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, false);
        }

        private void TxtPrecioUnitario_Leave(object sender, EventArgs e)
        {
            if (dgvListaMateriales.SelectedRows.Count == 1 && !string.IsNullOrEmpty(TxtPrecioUnitario.Text.Trim()))
            {
                Calcular();
            }
            else
            {
                TxtPrecioUnitario.Text = "0";
            }
        }
    }
}
