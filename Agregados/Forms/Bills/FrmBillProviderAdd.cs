using Agregados.Forms.Loading;
using Agregados.Forms.Materials;
using Agregados.Forms.Providers;
using Agregados.Reports;
using Agregados.Reports.Facts.FactNow;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Agregados.Forms.Bills
{
    public partial class FrmBillProviderAdd : Form
    {

        //variables del form
        AgregadosEntities DB;
        Facturas factura;
        Materiales materiales;
        DetalleFacts detalleFact;
        CierreApertCajas cierreApertCajas;
        CierreApertCajas apertura; // valor termporal apertura
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
            cierreApertCajas = new CierreApertCajas();
            apertura = new CierreApertCajas();

        }

        private void FrmBillProviderAdd_Load(object sender, EventArgs e) 
        { 

            //actualizamos a nivel de sistema la caja
            cierreApertCajas = BuscarAperturaActual();

            tmrFechaHora.Enabled = true;
            lblUsuarioLogueado.Text = $"( {Globals.MyGlobalUser.NombreUsuario} )" + $" {Globals.MyGlobalUser.NombreEmpleado} ";
            lblTypeFact.Visible = false;
            dateFinal.Visible = false;
            lblDate.Text = DateTime.Now.Date.ToLongDateString();

            validarConsecutivoLbl();

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
            checkBox1.Checked = false;
            if (checkBox1.Checked)
            {
                txtFactProveedor.Enabled = true;
                txtFactProveedor.Text = "";
            }
            else
            {
                if (checkBox1.Checked == false)
                {
                    txtFactProveedor.Enabled = false;
                    txtFactProveedor.Text = "No Proporcionada";
                }
            }

        }

        //busca la apertura para actualizar datos
        public CierreApertCajas BuscarAperturaActual()
        {
            apertura = null;
            try
            {
                cierreApertCajas = DB.CierreApertCajas.Where((x) => x.Accion == 1).FirstOrDefault();
                if (cierreApertCajas != null)
                {
                    int id = DB.CierreApertCajas.Where((x) => x.Accion == 1).Select((x) => x.IdCierreApert).Max();
                    apertura = DB.CierreApertCajas.Find(id);
                }
                cierreApertCajas = null;
                return apertura;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

        }



        private void validarConsecutivoLbl()
        {
            //indica el consecutivo a generar en la factura de compra
            int result = MaxIdConsecutivo();
            int consecutivo = 0;
            if (result == 0)
            {
                consecutivo = 1;
                lblFactNueva.Text = consecutivo.ToString();
            }
            else
            {
                consecutivo = (result + 1);
                lblFactNueva.Text = consecutivo.ToString();
            }
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
            if (Convert.ToInt32(lblLineas.Text.Trim()) >= 3)
            {
                mnuAgregarItem.Enabled = false;
                mnuModificarItem.Enabled = true;
                mnuQuitarItem.Enabled = true;
            }
            else
            {
                mnuAgregarItem.Enabled = true;
                mnuModificarItem.Enabled = true;
                mnuQuitarItem.Enabled = true;
            }
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

            //método que permite llamar y obtener los datos filtrados de los materiales y mostrarlos en el comboBox
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
                CargarTiposPagos();
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

            //método que permite llamar y obtener los datos filtrados de los materiales y mostrarlos en el comboBox
            var dt = DB.MetodosPagos.Where((x) => x.IdTipoPago != 5 && x.IdTipoPago != 4 && x.IdTipoPago != 6).ToList();

            CboxMetodoPago.ValueMember = "IdTipoPago";
            CboxMetodoPago.DisplayMember = "TipoPago";
            CboxMetodoPago.DataSource = dt;
            CboxMetodoPago.SelectedIndex = -1;
        }


        //carga Cbox Tipo Pago// como credito seleccionado
        private void CargarTiposPagosCredito()
        {

            //método que permite llamar y obtener los datos filtrados de  método de pago y mostrarlos en el comboBox
            var dt = DB.MetodosPagos.Where((x) => x.IdTipoPago == 5).ToList();

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
            Form FrmProviderSearch = new FrmProviderSearch();

            DialogResult resp = FrmProviderSearch.ShowDialog();

            
            if (resp == DialogResult.OK)
            {
                //MessageBox.Show("Se selecciono el proveedor " + $" {txtProveedor.Text}", "Éxito", MessageBoxButtons.OK,MessageBoxIcon.Information);
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


        //método que permite realizar validaciones a tomar en cuenta para generar registro de factura
        private bool ValidarCamposRequeridos()
        {
            bool R = false;

            if (!string.IsNullOrEmpty(txtNumProve.Text.Trim()) &&
                Convert.ToInt32(txtNumProve.Text.Trim()) > 0 &&
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

                if (string.IsNullOrEmpty(txtNumProve.Text.Trim()) ||
                    Convert.ToInt32(txtNumProve.Text.Trim()) <= 0)
                {
                    MessageBox.Show("Proveedor es requerido, favor seleccionar un Proveedor de la lista, presionando la lupa de búsqueda de Proveedores.",
                        "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNumProve.Focus();
                    return false;
                }
                if (txtTransporte.Value < 0)
                {
                    MessageBox.Show("Costo Transporte (Combustible) debe ser igual a 0 o mayor", "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Tipo de Factura Compra debe ser ingresado, selecciona contado o a crédito", "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                factura = DB.Facturas.Where((x) => x.IdProveedor > 0).FirstOrDefault();
                if (factura != null)
                {
                    result = DB.Facturas.Where((x) => x.IdProveedor > 0).Select((X) => X.Consecutivo).Max();
                }
                factura = null;
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }


        //limpiar
        public void limpiar()
        {
            validarConsecutivoLbl();
            txtNumProve.Text = 0.ToString();
            txtProveedor.Text = null;
            CboxTypeBill.SelectedIndex = -1;
            CboxMetodoPago.SelectedIndex = -1;
            txtTransporte.Value = 0;
            DtListaProvedor = new DataTable();
            dgvMaterials.DataSource = DtListaProvedor;
            validateLinesFact();
            Totalizar();
            txtReferencia.Text = null;


            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();

        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            if (ValidarCamposRequeridos())
            {
                if (checkBox1.Checked && (txtFactProveedor.Text.Trim() == null || txtFactProveedor.Text.Trim() == ""))
                {
                    MessageBox.Show("Error, número factura proveedor proporcionado esta vacío, pero se selecciono que se proporciona" +
                        "", "Error Sistema Caja",
                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    if (DtListaProvedor.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(CboxTypeBill.SelectedValue) == 1) //contado
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
                                    if (Convert.ToInt32(CboxMetodoPago.SelectedValue) == 1) // metodo efectivo   //TODO validar datos para el cierre de caja
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
                                                    MontoPendiente = 0,
                                                    FechaLimiteP = null,
                                                    ReferenciaPago = txtReferencia.Text.Trim(),


                                                    DetalleNoCobroIVA = null,
                                                    FactProveedor = txtFactProveedor.Text.Trim(),
                                                    PrecioEspecial = 0,
                                                    Descuento = 0,

                                                    IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                                    IdTipo = Convert.ToInt32(CboxTypeBill.SelectedValue),
                                                    IdEstado = 4,
                                                    IdCliente = null,
                                                    IdProveedor = Convert.ToInt32(txtNumProve.Text.Trim()),
                                                    IdTipoPago = Convert.ToInt32(CboxMetodoPago.SelectedValue),
                                                    IdCierreApert = apertura.IdCierreApert,
                                                };

                                                DB.Facturas.Add(factura);

                                                if (DB.SaveChanges() > 0)
                                                {

                                                    int IdFact = DB.Facturas.Where((x) => x.IdProveedor == factura.IdProveedor).Select((x) => x.IdFactura).Max();

                                                    foreach (DataRow Row in Globals.MifrmBillProviderAdd.DtListaProvedor.Rows)
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
                                                            //actualiza el estado
                                                            if (materiales.CantidadMaterial >= (materiales.Minimos + 2))
                                                            {
                                                                materiales.IdEstado = 11; //cantidad buena
                                                            }
                                                            else
                                                            {
                                                                if (((materiales.Minimos) > materiales.CantidadMaterial) || materiales.CantidadMaterial > 0)
                                                                {
                                                                    materiales.IdEstado = 10;//cantidad regular
                                                                }
                                                                else
                                                                {
                                                                    materiales.IdEstado = 9;//cantidad sin material
                                                                }
                                                            }
                                                            DB.Entry(materiales).State = EntityState.Modified;

                                                            if (DB.SaveChanges() > 0)
                                                            {
                                                                detalleFact = null;
                                                            }
                                                        }
                                                    }

                                                    apertura.MontoEfectivoFinal -= Total;
                                                    apertura.MontoCompraEfectivo += Total;
                                                    DB.Entry(apertura).State = EntityState.Modified;
                                                    if (DB.SaveChanges() <= 0)
                                                    {
                                                        MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    }


                                                    MessageBox.Show("Factura de Compra agregada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                    using (FrmPrintTicket frm = new FrmPrintTicket(consecutivo))
                                                    {
                                                        Cursor.Current = Cursors.WaitCursor;
                                                        frm.ShowDialog();
                                                        Cursor.Current = Cursors.Default;
                                                    };

                                                    factura = null;
                                                    limpiar();

                                                }
                                                else
                                                {
                                                    MessageBox.Show("Factura de Compra no se pudo procesar.", "Error Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    factura = null;
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
                                        if (Convert.ToInt32(CboxMetodoPago.SelectedValue) == 2) // método transferencia sinpe
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
                                                        MontoPendiente = 0,
                                                        FechaLimiteP = null,
                                                        ReferenciaPago = txtReferencia.Text.Trim(),


                                                        DetalleNoCobroIVA = null,
                                                        FactProveedor = txtFactProveedor.Text.Trim(),
                                                        PrecioEspecial = 0,
                                                        Descuento = 0,

                                                        IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                                        IdTipo = Convert.ToInt32(CboxTypeBill.SelectedValue),
                                                        IdEstado = 4,
                                                        IdCliente = null,
                                                        IdProveedor = Convert.ToInt32(txtNumProve.Text.Trim()),
                                                        IdTipoPago = Convert.ToInt32(CboxMetodoPago.SelectedValue),
                                                        IdCierreApert = apertura.IdCierreApert,
                                                    };

                                                    DB.Facturas.Add(factura);

                                                    if (DB.SaveChanges() > 0)
                                                    {

                                                        int IdFact = DB.Facturas.Where((x) => x.IdProveedor == factura.IdProveedor).Select((x) => x.IdFactura).Max();

                                                        foreach (DataRow Row in Globals.MifrmBillProviderAdd.DtListaProvedor.Rows)
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
                                                                //actualiza el estado
                                                                if (materiales.CantidadMaterial >= (materiales.Minimos + 2))
                                                                {
                                                                    materiales.IdEstado = 11; //cantidad buena
                                                                }
                                                                else
                                                                {
                                                                    if (((materiales.Minimos) > materiales.CantidadMaterial) || materiales.CantidadMaterial > 0)
                                                                    {
                                                                        materiales.IdEstado = 10;//cantidad regular
                                                                    }
                                                                    else
                                                                    {
                                                                        materiales.IdEstado = 9;//cantidad sin material
                                                                    }
                                                                }
                                                                DB.Entry(materiales).State = EntityState.Modified;

                                                                if (DB.SaveChanges() > 0)
                                                                {
                                                                    detalleFact = null;
                                                                }
                                                            }
                                                        }

                                                        apertura.MontoCompraTransf += Total;

                                                        DB.Entry(apertura).State = EntityState.Modified;
                                                        if (DB.SaveChanges() <= 0)
                                                        {
                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        }
                                                        MessageBox.Show("Factura de Compra agregada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        using (FrmPrintTicket frm = new FrmPrintTicket(consecutivo))
                                                        {
                                                            Cursor.Current = Cursors.WaitCursor;
                                                            frm.ShowDialog();
                                                            Cursor.Current = Cursors.Default;
                                                        };
                                                        factura = null;
                                                        limpiar();

                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Factura de Compra no se pudo procesar.", "Error Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        factura = null;
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
                                            if (Convert.ToInt32(CboxMetodoPago.SelectedValue) == 3) // método sinpe movil
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
                                                            MontoPendiente = 0,
                                                            FechaLimiteP = null,
                                                            ReferenciaPago = txtReferencia.Text.Trim(),

                                                            DetalleNoCobroIVA = null,
                                                            FactProveedor = txtFactProveedor.Text.Trim(),
                                                            PrecioEspecial = 0,
                                                            Descuento = 0,

                                                            IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                                            IdTipo = Convert.ToInt32(CboxTypeBill.SelectedValue),
                                                            IdEstado = 4,
                                                            IdCliente = null,
                                                            IdProveedor = Convert.ToInt32(txtNumProve.Text.Trim()),
                                                            IdTipoPago = Convert.ToInt32(CboxMetodoPago.SelectedValue),
                                                            IdCierreApert = apertura.IdCierreApert,
                                                        };

                                                        DB.Facturas.Add(factura);

                                                        if (DB.SaveChanges() > 0)
                                                        {

                                                            int IdFact = DB.Facturas.Where((x) => x.IdProveedor == factura.IdProveedor).Select((x) => x.IdFactura).Max();

                                                            foreach (DataRow Row in Globals.MifrmBillProviderAdd.DtListaProvedor.Rows)
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
                                                                    //actualiza el estado
                                                                    if (materiales.CantidadMaterial >= (materiales.Minimos + 2))
                                                                    {
                                                                        materiales.IdEstado = 11; //cantidad buena
                                                                    }
                                                                    else
                                                                    {
                                                                        if (((materiales.Minimos) > materiales.CantidadMaterial) || materiales.CantidadMaterial > 0)
                                                                        {
                                                                            materiales.IdEstado = 10;//cantidad regular
                                                                        }
                                                                        else
                                                                        {
                                                                            materiales.IdEstado = 9;//cantidad sin material
                                                                        }
                                                                    }
                                                                    DB.Entry(materiales).State = EntityState.Modified;

                                                                    if (DB.SaveChanges() > 0)
                                                                    {
                                                                        detalleFact = null;
                                                                    }
                                                                }
                                                            }
                                                            apertura.MontoCompraSinpe += Total;

                                                            DB.Entry(apertura).State = EntityState.Modified;
                                                            if (DB.SaveChanges() <= 0)
                                                            {
                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }

                                                            MessageBox.Show("Ticket de Compra agregada correctamente!", "Registro de Ticket Compra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            using (FrmPrintTicket frm = new FrmPrintTicket(consecutivo))
                                                            {
                                                                Cursor.Current = Cursors.WaitCursor;
                                                                frm.ShowDialog();
                                                                Cursor.Current = Cursors.Default;
                                                            };
                                                            factura = null;
                                                            limpiar();

                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Ticket de Compra no se pudo procesar.", "Error Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            factura = null;
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
                                                if (Convert.ToInt32(CboxMetodoPago.SelectedValue) == 4) // método cheque
                                                {
                                                    MessageBox.Show("Factura no se puede realizar con el método de pago indicado," +
                                                        " por favor valide que sea en efectivo, transferencia, o sinpe móvil",
                                                        "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Factura no se puede realizar con el método de pago indicado," +
                                                        " por favor valide que sea en efectivo, transferencia, o sinpe móvil",
                                                        "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                throw;
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(CboxTypeBill.SelectedValue) == 2) // credito => método pago pendiente (credito)
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

                                                        DetalleNoCobroIVA = null,
                                                        FactProveedor = txtFactProveedor.Text.Trim(),
                                                        PrecioEspecial = 0,
                                                        Descuento = 0,

                                                        IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                                        IdTipo = Convert.ToInt32(CboxTypeBill.SelectedValue),
                                                        IdEstado = 3,
                                                        IdCliente = null,
                                                        IdProveedor = Convert.ToInt32(txtNumProve.Text.Trim()),
                                                        IdTipoPago = Convert.ToInt32(CboxMetodoPago.SelectedValue),
                                                        IdCierreApert = apertura.IdCierreApert,
                                                    };

                                                    DB.Facturas.Add(factura);

                                                    if (DB.SaveChanges() > 0)
                                                    {

                                                        int IdFact = DB.Facturas.Where((x) => x.IdProveedor == factura.IdProveedor).Select((x) => x.IdFactura).Max();

                                                        foreach (DataRow Row in Globals.MifrmBillProviderAdd.DtListaProvedor.Rows)
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

                                                                //actualiza el estado
                                                                if (materiales.CantidadMaterial >= (materiales.Minimos + 2))
                                                                {
                                                                    materiales.IdEstado = 11; //cantidad buena
                                                                }
                                                                else
                                                                {
                                                                    if (((materiales.Minimos) > materiales.CantidadMaterial) || materiales.CantidadMaterial > 0)
                                                                    {
                                                                        materiales.IdEstado = 10;//cantidad regular
                                                                    }
                                                                    else
                                                                    {
                                                                        materiales.IdEstado = 9;//cantidad sin material
                                                                    }
                                                                }

                                                                DB.Entry(materiales).State = EntityState.Modified;

                                                                if (DB.SaveChanges() > 0)
                                                                {
                                                                    detalleFact = null;
                                                                }
                                                            }
                                                        }
                                                        apertura.MontoCompraCredito += Total;

                                                        DB.Entry(apertura).State = EntityState.Modified;
                                                        if (DB.SaveChanges() <= 0)
                                                        {
                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        }
                                                        limpiar();
                                                        MessageBox.Show("Factura de Compra agregada correctamente!", "Registro de Factura de Compra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        using (FrmPrintTicket frm = new FrmPrintTicket(consecutivo))
                                                        {
                                                            Cursor.Current = Cursors.WaitCursor;
                                                            frm.ShowDialog();
                                                            Cursor.Current = Cursors.Default;
                                                        };
                                                        factura = null;
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Factura no se pudo procesar.", "Error Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        factura = null;
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
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    else
                    {
                        MessageBox.Show("No has seleccionado un material, para poder emitir ticket de compra debes de seleccionar un material.",
                                                   "Registro Factura de Compra", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string name = DB.Proveedores.Where((x) => x.IdProveedor == num).Select((x) => x.Nombre).FirstOrDefault();

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


        //validaciones de los radio button para que sea un pago mixto

        private void rbEfectivo1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEfectivo1.Checked)
            {
                valorPago1.Enabled = true;
                valorPago1.Value = 0;

                rbEfectivo2.Enabled = false;
                rbEfectivo2.Checked = false;
            }
            else
            {
                rbEfectivo2.Enabled = true;
                
            }
        }

        private void rbSinpe1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSinpe1.Checked)
            {
                valorPago1.Enabled = true;
                valorPago1.Value = 0;

                rbSinpe2.Enabled = false;
                rbSinpe2.Checked = false;
            }
            else
            {
                rbSinpe2.Enabled = true;
                
            }
        }

        private void rbSinpeMovil1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSinpeMovil1.Checked)
            {
                valorPago1.Enabled = true;
                valorPago1.Value = 0;

                rbSinpeMovil2.Checked = false;
                rbSinpeMovil2.Enabled = false;
            }
            else
            {
                
                rbSinpeMovil2.Enabled = true;

            }
        }

        private void rbCheque1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCheque1.Checked)
            {
                valorPago1.Enabled = true;
                valorPago1.Value = 0;

                rbCheque2.Enabled = false;
                rbCheque2.Checked = false;
            }
            else
            {
                rbCheque2.Enabled = true;
               
            }
        }


        private void rbEfectivo2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEfectivo2.Checked)
            {
                valorPago2.Enabled = true;
                valorPago2.Value = 0;

                rbEfectivo1.Enabled = false;
                rbEfectivo1.Checked = false;
            }
            else
            {
                rbEfectivo1.Enabled = true;
                
            }
        }

        private void rbSinpe2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSinpe2.Checked)
            {
                valorPago2.Enabled = true;
                valorPago2.Value = 0;

                rbSinpe1.Enabled = false;
                rbSinpe1.Checked = false;
            }
            else
            {
                rbSinpe1.Enabled = true;
               
            }
        }

        private void rbSinpeMovil2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSinpeMovil2.Checked)
            {
                valorPago2.Enabled = true;
                valorPago2.Value = 0;

                rbSinpeMovil1.Checked = false;
                rbSinpeMovil1.Enabled = false;
            }
            else
            {

               
                rbSinpeMovil1.Enabled = true;

            }
        }

        private void rbCheque2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCheque2.Checked)
            {
                valorPago2.Enabled = true;
                valorPago2.Value = 0;

                rbCheque1.Enabled = false;
                rbCheque1.Checked = false;
            }
            else
            {
                rbCheque1.Enabled = true;
                
            }
        }

        private void CboxMetodoPago_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(CboxMetodoPago.SelectedValue) == 6)
            {
                groupBox1.Visible = true;
                groupBox2.Visible = true;
                valorPago1.Visible = true;
                valorPago2.Visible = true;
            }
            else
            {
                rbEfectivo1.Checked = false;
                rbEfectivo2.Checked = false;
                rbSinpe1.Checked = false;
                rbSinpe2.Checked = false;
                rbSinpeMovil1.Checked = false;
                rbSinpeMovil2.Checked = false;
                rbCheque1.Checked = false;
                rbCheque2.Checked = false;

                groupBox1.Visible = false;
                groupBox2.Visible = false;
                valorPago1.Visible = false;
                valorPago2.Visible = false;
                valorPago1.Value = 0;
                valorPago2.Value = 0;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtFactProveedor.Enabled = true;
                txtFactProveedor.Text = "";
            }
            else
            {
                if (checkBox1.Checked == false)
                {
                    txtFactProveedor.Enabled = false;
                    txtFactProveedor.Text = "No Proporcionada";
                }
            }
        }

        private void lblLineas_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lblLineas.Text.Trim()) >= 3)
            {
                mnuAgregarItem.Enabled = false;
            }
            else
            {
                mnuAgregarItem.Enabled = true;
            }
        }
    }
}
