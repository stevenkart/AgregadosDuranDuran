﻿using Agregados.Forms.Loading;
using Agregados.Forms.Materials;
using Agregados.Forms.Providers;
using Agregados.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Forms.Bills
{
    public partial class FrmBillProviderAdd : Form
    {

        //variables del form
        AgregadosEntities DB;
        Facturas factura;
        Materiales materiales;
        DetalleFacts detalleFact;
        public DataTable DtListaProvedor { get; set; }

        //propiedades para validar que item se seleccione y cantidad linea seleccionada
        public int CantidadItem = 0;

        #region PropiedadesDeTotalizacion
        public decimal SubTotal { get; set; } // subtotal
        public decimal TasaImpuesto { get; set; } //tasa aplicada al 13%
        public decimal Total { get; set; } // total aplicado con el el IVA 13%

        bool aplicarIva;


        #endregion


        public FrmBillProviderAdd()
        {
            InitializeComponent();

            DB = new AgregadosEntities();
            factura = new Facturas();
            detalleFact = new DetalleFacts();
            materiales = new Materiales();
            DtListaProvedor = new DataTable();

        }

        private void FrmBillProviderAdd_Load(object sender, EventArgs e)
        {
            tmrFechaHora.Enabled = true;
            lblUsuarioLogueado.Text = $"( {Globals.MyGlobalUser.NombreUsuario} )" + $" {Globals.MyGlobalUser.NombreEmpleado} ";
            lblTypeFact.Visible = false;
            dateFinal.Visible = false;
            lblDate.Text = DateTime.Now.Date.ToLongDateString();
            CargarTiposFactura();
            CargarTiposPagos();
            /*
            lblReferencia.Visible = false;
            txtReferencia.Visible = false;
            */

            //List<Materiales> materialesDT = new List<Materiales>();

            ActivarAdd();
            DtListaProvedor = makeDataTableSchema();

            aplicarIva = CboxIVA.Checked;

        }

        //validaciones de botones para evitar errores
        private void ActivarAdd()
        {
            mnuAgregarItem.Enabled = true;
            mnuModificarItem.Enabled = false;
            mnuQuitarItem.Enabled = false;
        }
        //validaciones de botones para evitar errores
        private void ActivarUpdateDelete()
        {
            mnuAgregarItem.Enabled = true;
            mnuModificarItem.Enabled = true;
            mnuQuitarItem.Enabled = true;
        }

        //asigna espacios al data table
        public DataTable makeDataTableSchema()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
                {
                new DataColumn("IdMaterial", typeof(int)),
                new DataColumn("NombreMaterial", typeof(string)),
                new DataColumn("CantidadMaterial", typeof(decimal)),
                new DataColumn("Precio", typeof(decimal)),
                new DataColumn("Subtotal", typeof(decimal)),
                new DataColumn("IVA", typeof(decimal)),
                new DataColumn("PrecioFinal", typeof(decimal)),
                }
            );
            return dt;
        }

        //calcular datos del datatable con IVA
        private void Totalizar()
        {
            //Este método calcula los valores de
            //subtotal, impuesto y total para la línea 
            //no supere el 100%

            decimal SubtotalTemp = 0;
            decimal TasaImpuestoTemp = 0;
            decimal TotalTemp = 0;
            SubTotal = 0;
            TasaImpuesto = 0;
            Total = 0;

            if (DtListaProvedor.Rows != null)
            {
                foreach (DataRow Row in DtListaProvedor.Rows)
                {
                    //valida que si IVA es sero, se aplique nuevamente
                    if (Convert.ToDecimal(Row["IVA"]) == 0)
                    {
                        Row["IVA"] = Convert.ToDecimal(Convert.ToDouble(Row["Subtotal"]) * Convert.ToDouble(0.13));
                        Row["PrecioFinal"] = Convert.ToDecimal(Convert.ToDouble(Row["Subtotal"]) + Convert.ToDouble(Row["IVA"]));
                    }
                    SubtotalTemp += Convert.ToDecimal(Row["Subtotal"]);
                    TasaImpuestoTemp += Convert.ToDecimal(Row["IVA"]);
                    TotalTemp += Convert.ToDecimal(Row["PrecioFinal"]);
                }
                SubTotal = SubtotalTemp + Convert.ToDecimal(txtTransporte.Value);
                TasaImpuesto = TasaImpuestoTemp + Convert.ToDecimal((Convert.ToDouble(txtTransporte.Value) * 0.13));
                Total = SubTotal + TasaImpuesto;
            }
            else
            {
                SubTotal = 0;
                TasaImpuesto = 0;
                Total = 0;
            }
            TxtSubTotal.Text = string.Format("¢ {0:N2}", SubTotal);
            TxtIVA.Text = string.Format("¢ {0:N2}", TasaImpuesto);
            TxtTotal.Text = string.Format("¢ {0:N2}", Total);

        }

        //calcular datos del datatable sin IVA
        private void TotalizarSinIVA()
        {
            //Este método calcula los valores de
            //subtotal, impuesto y total para la línea 
            //no supere el 100%

            decimal SubtotalTemp = 0;
            //decimal TasaImpuestoTemp = 0;
            decimal TotalTemp = 0;
            SubTotal = 0;
            TasaImpuesto = 0;
            Total = 0;

            if (DtListaProvedor.Rows != null)
            {
                foreach (DataRow Row in DtListaProvedor.Rows)
                {
                    SubtotalTemp += Convert.ToDecimal(Row["Subtotal"]);
                    Row["IVA"] = Convert.ToDecimal(0);
                    Row["PrecioFinal"] = Convert.ToDecimal(Row["Subtotal"]);
                    //TasaImpuestoTemp += Convert.ToDecimal(Row["IVA"]);
                    TotalTemp += Convert.ToDecimal(Row["PrecioFinal"]);
                }
                SubTotal = SubtotalTemp + Convert.ToDecimal(txtTransporte.Value);
                //TasaImpuesto = TasaImpuestoTemp + Convert.ToDecimal((Convert.ToDouble(txtTransporte.Value) * 0.13));
                Total = SubTotal;
            }
            else
            {
                SubTotal = 0;
                TasaImpuesto = 0;
                Total = 0;
            }
            TxtSubTotal.Text = string.Format("¢ {0:N2}", SubTotal);
            TxtIVA.Text = string.Format("¢ {0:N2}", TasaImpuesto);
            TxtTotal.Text = string.Format("¢ {0:N2}", Total);
        }

        //carga Cbox Tipo Factua
        private void CargarTiposFactura()
        {

            //Metodo que permite llamar y obtener los datos filtrados de los materiales y mostrarlos en el comboBox
            var dt = DB.TiposFacturas.ToList();

            CboxTypeBill.ValueMember = "IdTipo";
            CboxTypeBill.DisplayMember = "Tipo";
            CboxTypeBill.DataSource = dt;
            CboxTypeBill.SelectedIndex = -1;
        }

        private void CboxTypeBill_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(CboxTypeBill.SelectedValue) == 1)
            {
                lblTypeFact.Visible = false;
                dateFinal.Visible = false;
                //carga Cbox Tipo Factua
                CargarTiposPagosNoCredito();
            }
            else
            {
                if (Convert.ToInt32(CboxTypeBill.SelectedValue) == 2)
                {
                    lblTypeFact.Visible = true;
                    dateFinal.Visible = true;

                    CargarTiposPagosCredito();
                }
            }
        }

        //carga Cbox Tipo Pago// todos
        private void CargarTiposPagos()
        {

            //Metodo que permite llamar y obtener los datos filtrados de los materiales y mostrarlos en el comboBox
            var dt = DB.MetodosPagos.ToList();

            CboxMetodoPago.ValueMember = "IdTipoPago";
            CboxMetodoPago.DisplayMember = "TipoPago";
            CboxMetodoPago.DataSource = dt;
            CboxMetodoPago.SelectedIndex = -1;
        }


        //carga Cbox Tipo Pago// como credito seleccionado
        private void CargarTiposPagosCredito()
        {

            //Metodo que permite llamar y obtener los datos filtrados de  metodo de pago y mostrarlos en el comboBox
            var dt = DB.MetodosPagos.Where((x) => x.IdTipoPago == 5).ToList();

            CboxMetodoPago.ValueMember = "IdTipoPago";
            CboxMetodoPago.DisplayMember = "TipoPago";
            CboxMetodoPago.DataSource = dt;
            CboxMetodoPago.SelectedIndex = -1;
        }

        //carga Cbox Tipo Pago// como credito seleccionado
        private void CargarTiposPagosNoCredito()
        {

            //Metodo que permite llamar y obtener los datos filtrados de los metodo de pago y mostrarlos en el comboBox
            var dt = DB.MetodosPagos.Where((x) => x.IdTipoPago != 5).ToList();

            CboxMetodoPago.ValueMember = "IdTipoPago";
            CboxMetodoPago.DisplayMember = "TipoPago";
            CboxMetodoPago.DataSource = dt;
            CboxMetodoPago.SelectedIndex = -1;
        }

        private void FrmBillProviderAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
        }

        private void pictureExit_Click(object sender, EventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
        }

        private void pictureSearchClient_Click(object sender, EventArgs e)
        {
            Form FrmProviderSearch= new FrmProviderSearch();

            DialogResult resp = FrmProviderSearch.ShowDialog();

            if (resp == DialogResult.OK)
            {
                MessageBox.Show("Se selecciono el proveedor " + $" {txtProveedor.ToString()}", "Éxito", MessageBoxButtons.OK);
            }
        }

        //tiempo loading
        void Wait()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(5);
            }
        }

        private void tmrFechaHora_Tick(object sender, EventArgs e)
        {
            string fecha = DateTime.Now.Date.ToLongDateString();
            string hora = DateTime.Now.ToLongTimeString();

            lblFechaHora.Text = fecha + " / " + hora;
        }

        private void mnuAgregarItem_Click(object sender, EventArgs e)
        {
            Form FrmMaterialSearch = new FrmMaterialSearch(2);

            DialogResult resp = FrmMaterialSearch.ShowDialog();

            if (resp == DialogResult.OK)
            {
                if (DtListaProvedor.Rows.Count > 0)
                {
                    dgvMaterials.DataSource = DtListaProvedor;
                    dgvMaterials.ClearSelection();

                    if (CboxIVA.Checked)
                    {
                        Totalizar();
                    }
                    else
                    {
                        TotalizarSinIVA();
                    }
                    CantidadItem = 0;
                    validateLinesFact();
                    //MessageBox.Show("Se obtuvo el ID", "Éxito", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("No se pudo obtener datos del material", "Error Inesperado", MessageBoxButtons.OK);
                }
            }
        }

        private void dgvMaterials_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMaterials.SelectedRows.Count == 1)
            {
                CantidadItem = 1;
                ActivarUpdateDelete();
            }
        }

        private void mnuModificarItem_Click(object sender, EventArgs e)
        {

        }

        private void mnuQuitarItem_Click(object sender, EventArgs e)
        {
            if (dgvMaterials.Rows.Count > 0 && CantidadItem == 1)
            {
                string Msg = string.Format("¿Está seguro de eliminar el material seleccionado?");

                DialogResult respuesta = MessageBox.Show(Msg, "???", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {

                    DtListaProvedor.Rows.RemoveAt(this.dgvMaterials.SelectedRows[0].Index);

                    dgvMaterials.DataSource = DtListaProvedor;
                    dgvMaterials.ClearSelection();
                    CantidadItem = 0;
                    if (CboxIVA.Checked)
                    {
                        Totalizar();
                    }
                    else
                    {
                        TotalizarSinIVA();
                    }
                    validateLinesFact();
                    ActivarAdd();
                }
                else
                {
                    MessageBox.Show("Quitar material fue Cancelado",
                        "Eliminar Material", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CantidadItem = 0;
                    dgvMaterials.ClearSelection();
                    ActivarAdd();
                }
            }
            else
            {
                MessageBox.Show("Debe Seleccionar una Fila (Material), presionando o " +
                    "dando click en el nombre, o precio del *Material* en la lista",
                    "Eliminar Material", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvMaterials.ClearSelection();
                ActivarAdd();
            }
        }

        private void txtTransporte_ValueChanged(object sender, EventArgs e)
        {
            if (txtTransporte.Value >= 0)
            {
                if (CboxIVA.Checked)
                {
                    Totalizar();
                }
                else
                {
                    TotalizarSinIVA();
                }
            }
            else
            {
                txtTransporte.Value = 0;
            }
        }


        //cuando se agrega/elimina una linea nueva a la factura esta valida cuanta hay 
        public void validateLinesFact()
        {
            int valor = 0;
            foreach (DataRow Row in DtListaProvedor.Rows)
            {
                valor = valor + 1;

            }
            lblLineas.Text = Convert.ToString(valor);
        }


        //metodo que permite realizar validaciones a tomar en cuenta para generar registro de factura
        private bool ValidarCamposRequeridos()
        {
            bool R = false;

            if (!string.IsNullOrEmpty(txtNumProve.Text.Trim()) &&
                txtTransporte.Value >= 0 &&
                !string.IsNullOrEmpty(TxtSubTotal.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtIVA.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtTotal.Text.Trim()) &&
                CboxTypeBill.SelectedIndex != -1 &&
                CboxMetodoPago.SelectedIndex != -1
                )
            {
                R = true;
            }
            else
            {
                //estas validaciones deben ser puntuales para informar al usuario que falla 

                if (string.IsNullOrEmpty(txtNumProve.Text.Trim()))
                {
                    MessageBox.Show("Cliente es requerido, favor seleccionar un cliente de la lista, presionando la lupa de busqueda de clientes.",
                        "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNumProve.Focus();
                    return false;
                }
                if (txtTransporte.Value < 0)
                {
                    MessageBox.Show("Costo Transporte debe ser igual a 0 o mayor", "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTransporte.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(TxtSubTotal.Text.Trim()))
                {
                    MessageBox.Show("Valor de subtotal es requerido, favor validar que se haya seleccionado material de la lista para generar factura correctamente.",
                        "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtSubTotal.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(TxtIVA.Text.Trim()))
                {
                    MessageBox.Show("IVA es requerido, favor validar que se haya seleccionado material de la lista para generar factura correctamente.",
                        "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtIVA.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(TxtTotal.Text.Trim()))
                {
                    MessageBox.Show("Valor Total requerido, favor validar que se haya seleccionado material de la lista para generar factura correctamente.",
                        "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtTotal.Focus();
                    return false;
                }
                if (CboxTypeBill.SelectedIndex == -1)
                {
                    MessageBox.Show("Tipo de Factura debe ser ingresado, selecciona contado o a crédito", "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CboxTypeBill.Focus();
                    return false;
                }
                if (CboxMetodoPago.SelectedIndex == -1)
                {
                    MessageBox.Show("Método de Pago debe ser ingresado, selecciona uno en la lista." + Environment.NewLine +
                        "Si seleccionas como método de pago a crédito, debes seleccionar Método de Pago (Pendiente)",
                        "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CboxTypeBill.Focus();
                    return false;
                }
            }
            return R;
        }


        //valida que la fecha limite pago sea mayor al día actual

        private bool ValidarFechaLimite()
        {
            bool R;
            int dias = 0;
            DateTime fechaActual = Convert.ToDateTime(DateTime.Now);
            DateTime fechaProxima = Convert.ToDateTime(dateFinal.Value);
            TimeSpan tiempo = fechaProxima.Subtract(fechaActual);
            dias = Convert.ToInt32(tiempo.Days);
            if (dias == 0)
            {
                R = false;
                dateFinal.Focus();
            }
            else
            {
                if (dias >= 0)
                {
                    R = true;
                }
                else
                {
                    R = false;
                    dateFinal.Focus();
                }
            }
            return R;
        }


        //busca el id maximo de factura generado para proveedor
        public int MaxIdConsecutivo()
        {
            int result = 0;
            try
            {
                factura = DB.Facturas.FirstOrDefault();
                if (factura != null)
                {
                    result = DB.Facturas.Where((x) => x.IdProveedor > 0).Select((X) => X.Consecutivo).Max();
                }
                factura = null;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        //limpiar
        public void limpiar()
        {
            txtNumProve = null;
            CboxTypeBill.SelectedIndex = -1;
            CboxMetodoPago.SelectedIndex = -1;
            txtTransporte.Value = 0;
            DtListaProvedor = new DataTable();
            dgvMaterials.DataSource = DtListaProvedor;
            validateLinesFact();
            Totalizar();
            txtReferencia.Text = null;

        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            if (ValidarCamposRequeridos())
            {
                if (Convert.ToInt32(CboxTypeBill.SelectedValue) == 1)
                {
                    try
                    {
                        int result = MaxIdConsecutivo();

                        int consecutivo = 0;
                        if (result == 0)
                        {
                            consecutivo = 1;
                        }
                        else
                        {
                            consecutivo = (result + 1);
                        }

                        DialogResult respuesta = MessageBox.Show("¿Deseas generar el ticket de compra de contado?",
                                               "Registro Factura de Compra", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {
                            using (FrmLoading frmLoading = new FrmLoading(Wait))
                            {
                                try
                                {
                                    factura = new Facturas
                                    {
                                        Consecutivo = consecutivo,
                                        CostoTransporte = Convert.ToDecimal(txtTransporte.Value),
                                        Subtotal = SubTotal,
                                        IVA = TasaImpuesto,
                                        CostoTotal = Total,

                                        FechaFactura = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()),
                                        MontoPendiente = null,
                                        FechaLimiteP = null,
                                        ReferenciaPago = txtReferencia.Text.Trim(),

                                        IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                        IdTipo = Convert.ToInt32(CboxTypeBill.SelectedValue),
                                        IdEstado = 4,
                                        IdCliente = null,
                                        IdProveedor = Convert.ToInt32(txtNumProve.Text.Trim()),
                                        IdTipoPago = Convert.ToInt32(CboxMetodoPago.SelectedValue),
                                        IdCierreApert = 0
                                    };

                                    DB.Facturas.Add(factura);

                                    if (DB.SaveChanges() > 0)
                                    {

                                        int IdFact = DB.Facturas.Select((x) => x.IdFactura).Max();

                                        foreach (DataRow Row in Globals.MifrmBillAdd.DtLista.Rows)
                                        {
                                            detalleFact = new DetalleFacts
                                            {
                                                Cantidad = Convert.ToDecimal(Row["CantidadMaterial"]),
                                                Precio = Convert.ToDecimal(Row["Precio"]),
                                                Subtotal = Convert.ToDecimal(Row["Subtotal"]),
                                                IVA = Convert.ToDecimal(Row["IVA"]),
                                                Total = Convert.ToDecimal(Row["PrecioFinal"]),
                                                IdFactura = IdFact,
                                                IdMaterial = Convert.ToInt32(Row["IdMaterial"])
                                            };

                                            DB.DetalleFacts.Add(detalleFact);
                                            if (DB.SaveChanges() > 0)
                                            {
                                                int IdMaterial = Convert.ToInt32(Row["IdMaterial"]);
                                                materiales = DB.Materiales.Find(IdMaterial);
                                                materiales.CantidadMaterial = materiales.CantidadMaterial + Convert.ToDecimal(Row["CantidadMaterial"]);

                                                DB.Entry(materiales).State = EntityState.Modified;

                                                if (DB.SaveChanges() > 0)
                                                {
                                                    detalleFact = null;
                                                }
                                            }
                                        }
                                        limpiar();
                                        MessageBox.Show("Factura de Compra agregado correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        using (FrmPrintFact frm = new FrmPrintFact(IdFact))
                                        {
                                            frm.ShowDialog();
                                        };
                                        factura = null;

                                    }
                                    else
                                    {
                                        MessageBox.Show("Factura de Compra no se pudo procesar.", "Error Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        factura = null;
                                    }
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                            }
                        }
                    }
                    catch (NullReferenceException)
                    {

                        throw;
                    }
                }
                else
                {
                    if (Convert.ToInt32(CboxTypeBill.SelectedValue) == 2)
                    {
                        if (ValidarFechaLimite())
                        {
                            try
                            {
                                int result = MaxIdConsecutivo();

                                int consecutivo = 0;
                                if (result == 0)
                                {
                                    consecutivo = 1;
                                }
                                else
                                {
                                    consecutivo = (result + 1);
                                }

                                DialogResult respuesta = MessageBox.Show("¿Deseas generar el ticket de compra a Crédito?",
                                               "Registro Factura de Compra", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (respuesta == DialogResult.Yes)
                                {
                                    using (FrmLoading frmLoading = new FrmLoading(Wait))
                                    {
                                        try
                                        {
                                            factura = new Facturas
                                            {
                                                Consecutivo = consecutivo,
                                                CostoTransporte = Convert.ToDecimal(txtTransporte.Value),
                                                Subtotal = SubTotal,
                                                IVA = TasaImpuesto,
                                                CostoTotal = Total,

                                                FechaFactura = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()),
                                                MontoPendiente = Total,
                                                FechaLimiteP = Convert.ToDateTime(dateFinal.Value),
                                                ReferenciaPago = txtReferencia.Text.Trim(),

                                                IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                                IdTipo = Convert.ToInt32(CboxTypeBill.SelectedValue),
                                                IdEstado = 3,
                                                IdCliente = null,
                                                IdProveedor = Convert.ToInt32(txtNumProve.Text.Trim()),
                                                IdTipoPago = Convert.ToInt32(CboxMetodoPago.SelectedValue),
                                                IdCierreApert = 0
                                            };

                                            DB.Facturas.Add(factura);

                                            if (DB.SaveChanges() > 0)
                                            {

                                                int IdFact = DB.Facturas.Select((x) => x.IdFactura).Max();

                                                foreach (DataRow Row in Globals.MifrmBillAdd.DtLista.Rows)
                                                {
                                                    detalleFact = new DetalleFacts
                                                    {
                                                        Cantidad = Convert.ToDecimal(Row["CantidadMaterial"]),
                                                        Precio = Convert.ToDecimal(Row["Precio"]),
                                                        Subtotal = Convert.ToDecimal(Row["Subtotal"]),
                                                        IVA = Convert.ToDecimal(Row["IVA"]),
                                                        Total = Convert.ToDecimal(Row["PrecioFinal"]),
                                                        IdFactura = IdFact,
                                                        IdMaterial = Convert.ToInt32(Row["IdMaterial"])
                                                    };

                                                    DB.DetalleFacts.Add(detalleFact);
                                                    if (DB.SaveChanges() > 0)
                                                    {
                                                        int IdMaterial = Convert.ToInt32(Row["IdMaterial"]);
                                                        materiales = DB.Materiales.Find(IdMaterial);
                                                        materiales.CantidadMaterial = materiales.CantidadMaterial + Convert.ToDecimal(Row["CantidadMaterial"]);

                                                        DB.Entry(materiales).State = EntityState.Modified;

                                                        if (DB.SaveChanges() > 0)
                                                        {
                                                            detalleFact = null;
                                                        }
                                                    }
                                                }
                                                limpiar();
                                                MessageBox.Show("Factura de Compra agregada correctamente!", "Registro de Factura de Compra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                using (FrmPrintFact frm = new FrmPrintFact(IdFact))
                                                {
                                                    frm.ShowDialog();
                                                };
                                                factura = null;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Factura no se pudo procesar.", "Error Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                factura = null;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            throw;
                                        }
                                    }
                                }
                            }
                            catch (NullReferenceException)
                            {

                                throw;
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se selecciono una fecha mayor al 2 días del actual, favor validar.", "Error Factura de Compra a Crédito.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void CboxIVA_CheckedChanged(object sender, EventArgs e)
        {
            if (CboxIVA.Checked)
            {
                Totalizar();
            }
            else
            {
                TotalizarSinIVA();
            }
        }

        private void txtNumProve_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNumProve.Text.Trim()) && txtNumProve.TextLength > 0)
            {
                int num = Convert.ToInt32(txtNumProve.Text.Trim());
                string name = DB.Clientes.Where((x) => x.IdCliente == num).Select((x) => x.Nombre).FirstOrDefault();

                if (!string.IsNullOrEmpty(name))
                {
                    txtProveedor.Text = name.ToString();
                }
            }
            else
            {
                txtProveedor.Text = "Selecciona un Proveedor";
            }
        }

        private void txtProveedor_TextChanged(object sender, EventArgs e)
        {
            //
        }
    }
}
