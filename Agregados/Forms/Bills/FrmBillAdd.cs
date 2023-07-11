using Agregados.Forms.Customers;
using Agregados.Forms.Loading;
using Agregados.Forms.Materials;
using Agregados.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Agregados.Forms.Bills
{
    public partial class FrmBillAdd : Form
    {
        //variables del form
        AgregadosEntities DB;
        Facturas factura;
        Materiales materiales;
        DetalleFacts detalleFact;
        CierreApertCajas cierreApertCajas;
        CierreApertCajas apertura; // valor termporal apertura


        public DataTable DtLista { get; set; }

        //propiedades para validar que item se seleccione y cantidad linea seleccionada
        public int CantidadItem = 0;

        #region PropiedadesDeTotalizacion
        public decimal SubTotal { get; set; } // subtotal
        public decimal TasaImpuesto { get; set; } //tasa aplicada al 13%
        public decimal Total { get; set; } // total aplicado con el el IVA 13%

        bool aplicarIva;


        #endregion


        public FrmBillAdd()
        {
            InitializeComponent();
            DB = new AgregadosEntities();
            factura = new Facturas();    
            detalleFact = new DetalleFacts();
            materiales = new Materiales();
            DtLista = new DataTable();
            cierreApertCajas = new CierreApertCajas();
            apertura = new CierreApertCajas();
        }

        private void FrmBillAdd_Load(object sender, EventArgs e)
        {
            //actualizamos a nivel de sistema la caja
            cierreApertCajas = BuscarAperturaActual();

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
            DtLista = makeDataTableSchema();

            aplicarIva = CboxIVA.Checked;


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

            lblTotalBackHoe.Visible = false;
            totalBackHoe.Visible = false;

            lblTierraNormal.Visible = false;
            lblTierraRoja.Visible = false;
            lblTierraTotal.Visible = false;

            chTierraNormal.Visible = false;
            chTierraRoja.Visible = false;
            txtTierraTotal.Visible = false;

            lblCantTierra.Visible = false;
            txtCantTierra.Visible = false;


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
            catch (Exception)
            {
                throw;
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

        /*
       //importante
       //Generic function to convert Linq query to DataTable.
       public DataTable LinqToDataTable<T>(IEnumerable<T> items)
       {
           //Createa DataTable with the Name of the Class i.e. Customer class.
           DataTable dt = new DataTable(typeof(T).Name);

           //Read all the properties of the Class i.e. Customer class.
           PropertyInfo[] propInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

           //Loop through each property of the Class i.e. Customer class.
           foreach (PropertyInfo propInfo in propInfos)
           {
               //Add Columns in DataTable based on Property Name and Type.
               dt.Columns.Add(new DataColumn(propInfo.Name, propInfo.PropertyType));
           }

           //Loop through the items if the Collection.
           foreach (T item in items)
           {
               //Add a new Row to DataTable.
               DataRow dr = dt.Rows.Add();

               //Loop through each property of the Class i.e. Customer class.
               foreach (PropertyInfo propInfo in propInfos)
               {
                   //Add value Column to the DataRow.
                   dr[propInfo.Name] = propInfo.GetValue(item, null);
               }
           }

           return dt;
       }
        */

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

            if (DtLista.Rows != null)
            {
                foreach (DataRow Row in DtLista.Rows)
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
                SubTotal = SubtotalTemp + Convert.ToDecimal(txtTransporte.Value) + Convert.ToDecimal(totalBackHoe.Value) + Convert.ToDecimal(txtTierraTotal.Value);
                TasaImpuesto = TasaImpuestoTemp + Convert.ToDecimal((Convert.ToDouble(txtTransporte.Value) * 0.13)) + Convert.ToDecimal((Convert.ToDouble(totalBackHoe.Value) * 0.13)) + Convert.ToDecimal((Convert.ToDouble(txtTierraTotal.Value) * 0.13));
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

            if (DtLista.Rows != null)
            {
                foreach (DataRow Row in DtLista.Rows)
                {
                    SubtotalTemp += Convert.ToDecimal(Row["Subtotal"]);
                    Row["IVA"] = Convert.ToDecimal(0);
                    Row["PrecioFinal"] = Convert.ToDecimal(Row["Subtotal"]);
                    //TasaImpuestoTemp += Convert.ToDecimal(Row["IVA"]);
                    TotalTemp += Convert.ToDecimal(Row["PrecioFinal"]);
                }
                SubTotal = SubtotalTemp + Convert.ToDecimal(txtTransporte.Value) + Convert.ToDecimal(totalBackHoe.Value) + Convert.ToDecimal(txtTierraTotal.Value);
                //TasaImpuesto = TasaImpuestoTemp + Convert.ToDecimal((Convert.ToDouble(txtTransporte.Value) * 0.13)) + Convert.ToDecimal((Convert.ToDouble(totalBackHoe.Value) * 0.13)) + Convert.ToDecimal((Convert.ToDouble(txtTierraTotal.Value) * 0.13));
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
       //cambio tipo factura, muestra la info de credito (FECHA)
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

        //cambio tipo pago, muestra la info de pago
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

        //cuando se cierra el form
       private void FrmBillAdd_FormClosing(object sender, FormClosingEventArgs e)
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
           Form FrmCustomerSearch = new FrmCustomerSearch();

           DialogResult resp = FrmCustomerSearch.ShowDialog();

           if (resp == DialogResult.OK)
           {
               MessageBox.Show("Se selecciono el cliente " + $" {txtClient.ToString()}", "Éxito", MessageBoxButtons.OK);
           }
       }

       //busca cliente cuando cambia el numero de cliente
       private void txtNumClient_TextChanged(object sender, EventArgs e)
       {
           if (!string.IsNullOrEmpty(txtNumClient.Text.Trim()) && txtNumClient.TextLength > 0)
           {
               int num = Convert.ToInt32(txtNumClient.Text.Trim());
               string name = DB.Clientes.Where((x) => x.IdCliente == num).Select((x) => x.Nombre).FirstOrDefault();

               if (!string.IsNullOrEmpty(name))
               {
                   txtClient.Text = name.ToString();
               }
            }
            else
            {
                txtClient.Text = "Selecciona un Cliente";
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

        //asignar al label de fecha y hora el valor en formato extendido de la fecha 
        // y ademas la hora
        private void tmrFechaHora_Tick(object sender, EventArgs e)
       {


           string fecha = DateTime.Now.Date.ToLongDateString();
           string hora = DateTime.Now.ToLongTimeString();

           lblFechaHora.Text = fecha + " / " + hora;
        }

        //Agrega item a la lista
        private void mnuAgregarItem_Click(object sender, EventArgs e)
        {
           Form FrmMaterialSearch = new FrmMaterialSearch(1);

           DialogResult resp = FrmMaterialSearch.ShowDialog();

           if (resp == DialogResult.OK)
           {
                if (DtLista.Rows.Count > 0)
                {
                    dgvMaterials.DataSource = DtLista;
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

        //Activa el eliminar item de la lista
        private void dgvMaterials_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMaterials.SelectedRows.Count == 1)
            {
                CantidadItem = 1;
                ActivarUpdateDelete();
            }
        }

        //Elimina item de la lista
        private void mnuQuitarItem_Click(object sender, EventArgs e)
        {
            if (dgvMaterials.Rows.Count > 0 && CantidadItem == 1)
            {
                string Msg = string.Format("¿Está seguro de eliminar el material seleccionado?");

                DialogResult respuesta = MessageBox.Show(Msg, "???", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {

                    DtLista.Rows.RemoveAt(this.dgvMaterials.SelectedRows[0].Index);

                    dgvMaterials.DataSource = DtLista;
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
     

        //cuando se agrega/elimina una linea nueva a la factura esta valida cuanta hay 
        public void validateLinesFact()
        {
            int valor = 0;
            foreach (DataRow Row in DtLista.Rows)
            {
                valor = valor + 1;

            }
            lblLineas.Text = Convert.ToString(valor);
        }

        //metodo que permite realizar validaciones a tomar en cuenta para generar registro de factura
        private bool ValidarCamposRequeridos()
        {
            bool R = false;

            if (!string.IsNullOrEmpty(txtNumClient.Text.Trim()) &&
                Convert.ToInt32(txtNumClient.Text.Trim()) > 0 &&
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

                if (string.IsNullOrEmpty(txtNumClient.Text.Trim()) || 
                    Convert.ToInt32(txtNumClient.Text.Trim()) <= 0)
                {
                    MessageBox.Show("Cliente es requerido, favor seleccionar un cliente de la lista, presionando la lupa de busqueda de clientes.", 
                        "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNumClient.Focus();
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

        //busca el id maximo de factura generado para 
        public int MaxIdConsecutivo()
        {
            int result = 0;
            try
            {
                factura = DB.Facturas.Where((x) => x.IdCliente > 0).FirstOrDefault();
                if (factura != null)
                {
                    result = DB.Facturas.Where((x) => x.IdCliente > 0).Select((X) => X.Consecutivo).Max();
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
            txtNumClient.Text = 0.ToString();
            txtClient.Text = null;
            CboxTypeBill.SelectedIndex = -1;
            CboxMetodoPago.SelectedIndex = -1;
            txtTierraTotal.Value = 0;
            DtLista = new DataTable();
            dgvMaterials.DataSource = DtLista;
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
                if (Convert.ToInt32(CboxTypeBill.SelectedValue) == 1) //Contado
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



                        if (chBackHoe.Checked == false && chTierra.Checked == false) // donde no es trabajo de backhoe ni tierra
                        {
                            DialogResult respuesta = MessageBox.Show("¿Deseas generar la factura de contado?",
                                                 "Registro Factura", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (respuesta == DialogResult.Yes)
                            {
                                if (Convert.ToInt32(CboxTypeBill.SelectedValue) == 1) //contado
                                {
                                    using (FrmLoading frmLoading = new FrmLoading(Wait))
                                    {
                                        try
                                        {
                                            //mixto
                                            if (Convert.ToInt32(CboxMetodoPago.SelectedValue) == 6) //mixto
                                            {
                                                DialogResult respuestaMixto = MessageBox.Show("¿Deseas generar la factura de bajo metodo de pago mixto?," +
                                                    " no se podrá reversar en caso de que se requiera. Para eso sera necesario realizar una nota de crédito.",
                                                  "Registro Factura", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                if (respuestaMixto == DialogResult.Yes)
                                                {
                                                    if ((Convert.ToDecimal(valorPago1.Value) + Convert.ToDecimal(valorPago2.Value)) == Total
                                                                    && Convert.ToDecimal(valorPago1.Value) > 0
                                                                    && Convert.ToDecimal(valorPago2.Value) > 0)
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
                                                            BackHoe = null,
                                                            Tierra = null,
                                                            CantTierra = null,

                                                            IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                                            IdTipo = Convert.ToInt32(CboxTypeBill.SelectedValue),
                                                            IdEstado = 4,
                                                            IdCliente = Convert.ToInt32(txtNumClient.Text.Trim()),
                                                            IdProveedor = null,
                                                            IdTipoPago = Convert.ToInt32(CboxMetodoPago.SelectedValue),
                                                            IdCierreApert = apertura.IdCierreApert,
                                                        };

                                                        DB.Facturas.Add(factura);

                                                        if (DB.SaveChanges() > 0)
                                                        {

                                                            int IdFact = DB.Facturas.Where((x) => x.IdCliente == factura.IdCliente).Select((x) => x.IdFactura).Max();

                                                            if (Globals.MifrmBillAdd.DtLista.Rows.Count > 0)
                                                            {
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
                                                                        materiales.CantidadMaterial = materiales.CantidadMaterial - Convert.ToDecimal(Row["CantidadMaterial"]);
                                                                        //actualiza el estado
                                                                        if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                        {
                                                                            materiales.IdEstado = 11;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                            {
                                                                                materiales.IdEstado = 10;
                                                                            }
                                                                            else
                                                                            {
                                                                                materiales.IdEstado = 9;
                                                                            }
                                                                        }
                                                                        DB.Entry(materiales).State = EntityState.Modified;

                                                                        if (DB.SaveChanges() > 0)
                                                                        {
                                                                            detalleFact = null;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            //medio mixto
                                                            switch (Convert.ToInt32(CboxMetodoPago.SelectedValue))
                                                            {
                                                                case 6: //mixto
                                                                        //validar los campos de pago respectivo para actualizar la caja abierta

                                                                    if (rbEfectivo1.Checked && rbSinpe2.Checked)
                                                                    {
                                                                        apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago1.Value);
                                                                        apertura.MontoTransf += Convert.ToDecimal(valorPago2.Value);
                                                                        DB.Entry(apertura).State = EntityState.Modified;
                                                                        if (DB.SaveChanges() <= 0)
                                                                        {
                                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (rbEfectivo1.Checked && rbSinpeMovil2.Checked)
                                                                        {
                                                                            apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago1.Value);
                                                                            apertura.MontoSinpe += Convert.ToDecimal(valorPago2.Value);
                                                                            DB.Entry(apertura).State = EntityState.Modified;
                                                                            if (DB.SaveChanges() <= 0)
                                                                            {
                                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (rbEfectivo1.Checked && rbCheque2.Checked)
                                                                            {
                                                                                apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago1.Value);
                                                                                apertura.MontoCheque += Convert.ToDecimal(valorPago2.Value);
                                                                                DB.Entry(apertura).State = EntityState.Modified;
                                                                                if (DB.SaveChanges() <= 0)
                                                                                {
                                                                                    MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (rbSinpe1.Checked && rbEfectivo2.Checked)
                                                                                {
                                                                                    apertura.MontoTransf += Convert.ToDecimal(valorPago1.Value);
                                                                                    apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago2.Value);
                                                                                    DB.Entry(apertura).State = EntityState.Modified;
                                                                                    if (DB.SaveChanges() <= 0)
                                                                                    {
                                                                                        MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (rbSinpe1.Checked && rbSinpeMovil2.Checked)
                                                                                    {
                                                                                        apertura.MontoTransf += Convert.ToDecimal(valorPago1.Value);
                                                                                        apertura.MontoSinpe += Convert.ToDecimal(valorPago2.Value);
                                                                                        DB.Entry(apertura).State = EntityState.Modified;
                                                                                        if (DB.SaveChanges() <= 0)
                                                                                        {
                                                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (rbSinpe1.Checked && rbCheque2.Checked)
                                                                                        {
                                                                                            apertura.MontoTransf += Convert.ToDecimal(valorPago1.Value);
                                                                                            apertura.MontoCheque += Convert.ToDecimal(valorPago2.Value);
                                                                                            DB.Entry(apertura).State = EntityState.Modified;
                                                                                            if (DB.SaveChanges() <= 0)
                                                                                            {
                                                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (rbSinpeMovil1.Checked && rbEfectivo2.Checked)
                                                                                            {
                                                                                                apertura.MontoSinpe += Convert.ToDecimal(valorPago1.Value);
                                                                                                apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago2.Value);
                                                                                                DB.Entry(apertura).State = EntityState.Modified;
                                                                                                if (DB.SaveChanges() <= 0)
                                                                                                {
                                                                                                    MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (rbSinpeMovil1.Checked && rbSinpe2.Checked)
                                                                                                {
                                                                                                    apertura.MontoSinpe += Convert.ToDecimal(valorPago1.Value);
                                                                                                    apertura.MontoTransf += Convert.ToDecimal(valorPago2.Value);
                                                                                                    DB.Entry(apertura).State = EntityState.Modified;
                                                                                                    if (DB.SaveChanges() <= 0)
                                                                                                    {
                                                                                                        MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                    }
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (rbSinpeMovil1.Checked && rbCheque2.Checked)
                                                                                                    {
                                                                                                        apertura.MontoSinpe += Convert.ToDecimal(valorPago1.Value);
                                                                                                        apertura.MontoCheque += Convert.ToDecimal(valorPago2.Value);
                                                                                                        DB.Entry(apertura).State = EntityState.Modified;
                                                                                                        if (DB.SaveChanges() <= 0)
                                                                                                        {
                                                                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                        }
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (rbCheque1.Checked && rbEfectivo2.Checked)
                                                                                                        {
                                                                                                            apertura.MontoCheque += Convert.ToDecimal(valorPago1.Value);
                                                                                                            apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago2.Value);
                                                                                                            DB.Entry(apertura).State = EntityState.Modified;
                                                                                                            if (DB.SaveChanges() <= 0)
                                                                                                            {
                                                                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                            }
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (rbCheque1.Checked && rbSinpe2.Checked)
                                                                                                            {
                                                                                                                apertura.MontoCheque += Convert.ToDecimal(valorPago1.Value);
                                                                                                                apertura.MontoTransf += Convert.ToDecimal(valorPago2.Value);
                                                                                                                DB.Entry(apertura).State = EntityState.Modified;
                                                                                                                if (DB.SaveChanges() <= 0)
                                                                                                                {
                                                                                                                    MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                                }
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (rbCheque1.Checked && rbSinpeMovil2.Checked)
                                                                                                                {
                                                                                                                    apertura.MontoCheque += Convert.ToDecimal(valorPago1.Value);
                                                                                                                    apertura.MontoSinpe += Convert.ToDecimal(valorPago2.Value);
                                                                                                                    DB.Entry(apertura).State = EntityState.Modified;
                                                                                                                    if (DB.SaveChanges() <= 0)
                                                                                                                    {
                                                                                                                        MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                                    using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                                    {
                                                                        frm.ShowDialog();
                                                                    };

                                                                    factura = null;
                                                                    limpiar();
                                                                    break;
                                                                default:
                                                                    break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Factura no se pudo procesar, favor validar que el monto indicado en los metodos de pago, " +
                                                                            "sumen y den exacto a la cantidad que se muestra al total de la factura a generar, y se haya seleccionado" +
                                                                            "ambos metodos de pago.",
                                                                            "Error Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        }
                                                    }
                                                }
                                            }
                                            else //no mixto
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
                                                    BackHoe = null,
                                                    Tierra = null,
                                                    CantTierra = null,

                                                    IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                                    IdTipo = Convert.ToInt32(CboxTypeBill.SelectedValue),
                                                    IdEstado = 4,
                                                    IdCliente = Convert.ToInt32(txtNumClient.Text.Trim()),
                                                    IdProveedor = null,
                                                    IdTipoPago = Convert.ToInt32(CboxMetodoPago.SelectedValue),
                                                    IdCierreApert = apertura.IdCierreApert,
                                                };

                                                DB.Facturas.Add(factura);

                                                if (DB.SaveChanges() > 0)
                                                {

                                                    int IdFact = DB.Facturas.Where((x) => x.IdCliente == factura.IdCliente).Select((x) => x.IdFactura).Max();

                                                    if (Globals.MifrmBillAdd.DtLista.Rows.Count > 0)
                                                    {
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
                                                                materiales.CantidadMaterial = materiales.CantidadMaterial - Convert.ToDecimal(Row["CantidadMaterial"]);
                                                                //actualiza el estado
                                                                if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                {
                                                                    materiales.IdEstado = 11;
                                                                }
                                                                else
                                                                {
                                                                    if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                    {
                                                                        materiales.IdEstado = 10;
                                                                    }
                                                                    else
                                                                    {
                                                                        materiales.IdEstado = 9;
                                                                    }
                                                                }
                                                                DB.Entry(materiales).State = EntityState.Modified;

                                                                if (DB.SaveChanges() > 0)
                                                                {
                                                                    detalleFact = null;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    switch (Convert.ToInt32(CboxMetodoPago.SelectedValue))
                                                    {
                                                        case 1: //efectivo
                                                            apertura.MontoEfectivoFinal += Total;
                                                            DB.Entry(apertura).State = EntityState.Modified;
                                                            if (DB.SaveChanges() <= 0)
                                                            {
                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }

                                                            MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                            using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                            {
                                                                frm.ShowDialog();
                                                            };

                                                            factura = null;
                                                            limpiar();
                                                            break;
                                                        case 2: //sinpe
                                                            apertura.MontoTransf += Total;
                                                            DB.Entry(apertura).State = EntityState.Modified;
                                                            if (DB.SaveChanges() <= 0)
                                                            {
                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                            MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                            using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                            {
                                                                frm.ShowDialog();
                                                            };

                                                            factura = null;
                                                            limpiar();
                                                            break;
                                                        case 3: //sinpe movil
                                                            apertura.MontoSinpe += Total;
                                                            DB.Entry(apertura).State = EntityState.Modified;
                                                            if (DB.SaveChanges() <= 0)
                                                            {
                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }

                                                            MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                            using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                            {
                                                                frm.ShowDialog();
                                                            };

                                                            factura = null;
                                                            limpiar();
                                                            break;
                                                        case 4: //cheque
                                                            apertura.MontoCheque += Total;
                                                            DB.Entry(apertura).State = EntityState.Modified;
                                                            if (DB.SaveChanges() <= 0)
                                                            {
                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                            MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                            using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                            {
                                                                frm.ShowDialog();
                                                            };

                                                            factura = null;
                                                            limpiar();
                                                            break;
                                                        default:
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Factura no se pudo procesada.", "Error Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    factura = null;
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
                        else
                        {
                            if (chBackHoe.Checked && chTierra.Checked) // dond es trabajo de backhoe & tierra
                            {
                                if (totalBackHoe.Value > 0 && txtCantTierra.Value > 0 && txtCantTierra.Value > 0)
                                {
                                    DialogResult respuesta = MessageBox.Show("¿Deseas generar la factura de contado?",
                                                 "Registro Factura", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (respuesta == DialogResult.Yes)
                                    {
                                        if (Convert.ToInt32(CboxTypeBill.SelectedValue) == 1) //contado
                                        {
                                            using (FrmLoading frmLoading = new FrmLoading(Wait))
                                            {
                                                try
                                                {
                                                    //mixto
                                                    if (Convert.ToInt32(CboxMetodoPago.SelectedValue) == 6) //mixto
                                                    {
                                                        DialogResult respuestaMixto = MessageBox.Show("¿Deseas generar la factura de bajo metodo de pago mixto?," +
                                                            " no se podrá reversar en caso de que se requiera. Para eso sera necesario realizar una nota de crédito.",
                                                          "Registro Factura", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                        if (respuestaMixto == DialogResult.Yes)
                                                        {
                                                            if ((Convert.ToDecimal(valorPago1.Value) + Convert.ToDecimal(valorPago2.Value)) == Total
                                                                            && Convert.ToDecimal(valorPago1.Value) > 0
                                                                            && Convert.ToDecimal(valorPago2.Value) > 0)
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
                                                                    BackHoe = Convert.ToDecimal(totalBackHoe.Value),
                                                                    Tierra = Convert.ToDecimal(txtTierraTotal.Value),
                                                                    CantTierra = Convert.ToDecimal(txtCantTierra.Text.Trim()),

                                                                    IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                                                    IdTipo = Convert.ToInt32(CboxTypeBill.SelectedValue),
                                                                    IdEstado = 4,
                                                                    IdCliente = Convert.ToInt32(txtNumClient.Text.Trim()),
                                                                    IdProveedor = null,
                                                                    IdTipoPago = Convert.ToInt32(CboxMetodoPago.SelectedValue),
                                                                    IdCierreApert = apertura.IdCierreApert,
                                                                };

                                                                DB.Facturas.Add(factura);

                                                                if (DB.SaveChanges() > 0)
                                                                {

                                                                    int IdFact = DB.Facturas.Where((x) => x.IdCliente == factura.IdCliente).Select((x) => x.IdFactura).Max();


                                                                    if (CboxIVA.Checked)
                                                                    {
                                                                        detalleFact = new DetalleFacts
                                                                        {
                                                                            Cantidad = Convert.ToDecimal(txtCantTierra.Text.Trim()),
                                                                            Precio = Convert.ToDecimal(txtTierraTotal.Value),
                                                                            Subtotal = Convert.ToDecimal(txtTierraTotal.Value),
                                                                            IVA = Convert.ToDecimal((Convert.ToDouble(txtTierraTotal.Value) * 0.13)),
                                                                            Total = Convert.ToDecimal((Convert.ToDouble(txtTierraTotal.Value) * 0.13)) + Convert.ToDecimal(txtTierraTotal.Value),
                                                                            IdFactura = IdFact,
                                                                            IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1,
                                                                        };
                                                                        DB.DetalleFacts.Add(detalleFact);

                                                                        if (DB.SaveChanges() > 0)
                                                                        {
                                                                            int IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1;
                                                                            materiales = DB.Materiales.Find(IdMaterial);
                                                                            materiales.CantidadMaterial = materiales.CantidadMaterial + Convert.ToDecimal(txtCantTierra.Text.Trim());
                                                                            //actualiza el estado
                                                                            if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                            {
                                                                                materiales.IdEstado = 11;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                                {
                                                                                    materiales.IdEstado = 10;
                                                                                }
                                                                                else
                                                                                {
                                                                                    materiales.IdEstado = 9;
                                                                                }
                                                                            }
                                                                            DB.Entry(materiales).State = EntityState.Modified;

                                                                            if (DB.SaveChanges() > 0)
                                                                            {
                                                                                detalleFact = null;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información del detalle de la factura", "Error Sistema Caja",
                                                                                                                                                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        detalleFact = new DetalleFacts
                                                                        {
                                                                            Cantidad = Convert.ToDecimal(txtCantTierra.Text.Trim()),
                                                                            Precio = Convert.ToDecimal(txtTierraTotal.Value),
                                                                            Subtotal = Convert.ToDecimal(txtTierraTotal.Value),
                                                                            IVA = 0,
                                                                            Total = Convert.ToDecimal(txtTierraTotal.Value),
                                                                            IdFactura = IdFact,
                                                                            IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1,
                                                                        };
                                                                        DB.DetalleFacts.Add(detalleFact);

                                                                        if (DB.SaveChanges() > 0)
                                                                        {
                                                                            int IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1;
                                                                            materiales = DB.Materiales.Find(IdMaterial);
                                                                            materiales.CantidadMaterial = materiales.CantidadMaterial + Convert.ToDecimal(txtCantTierra.Text.Trim());
                                                                            //actualiza el estado
                                                                            if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                            {
                                                                                materiales.IdEstado = 11;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                                {
                                                                                    materiales.IdEstado = 10;
                                                                                }
                                                                                else
                                                                                {
                                                                                    materiales.IdEstado = 9;
                                                                                }
                                                                            }
                                                                            DB.Entry(materiales).State = EntityState.Modified;

                                                                            if (DB.SaveChanges() > 0)
                                                                            {
                                                                                detalleFact = null;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información del detalle de la factura", "Error Sistema Caja",
                                                                                                                                                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                        }
                                                                    }

                                                                    if (Globals.MifrmBillAdd.DtLista.Rows.Count > 0)
                                                                    {
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
                                                                                materiales.CantidadMaterial = materiales.CantidadMaterial - Convert.ToDecimal(Row["CantidadMaterial"]);
                                                                                //actualiza el estado
                                                                                if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                                {
                                                                                    materiales.IdEstado = 11;
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                                    {
                                                                                        materiales.IdEstado = 10;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        materiales.IdEstado = 9;
                                                                                    }
                                                                                }
                                                                                DB.Entry(materiales).State = EntityState.Modified;

                                                                                if (DB.SaveChanges() > 0)
                                                                                {
                                                                                    detalleFact = null;
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    //medio mixto
                                                                    switch (Convert.ToInt32(CboxMetodoPago.SelectedValue))
                                                                    {
                                                                        case 6: //mixto
                                                                                //validar los campos de pago respectivo para actualizar la caja abierta

                                                                            if (rbEfectivo1.Checked && rbSinpe2.Checked)
                                                                            {
                                                                                apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago1.Value);
                                                                                apertura.MontoTransf += Convert.ToDecimal(valorPago2.Value);
                                                                                DB.Entry(apertura).State = EntityState.Modified;
                                                                                if (DB.SaveChanges() <= 0)
                                                                                {
                                                                                    MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (rbEfectivo1.Checked && rbSinpeMovil2.Checked)
                                                                                {
                                                                                    apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago1.Value);
                                                                                    apertura.MontoSinpe += Convert.ToDecimal(valorPago2.Value);
                                                                                    DB.Entry(apertura).State = EntityState.Modified;
                                                                                    if (DB.SaveChanges() <= 0)
                                                                                    {
                                                                                        MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (rbEfectivo1.Checked && rbCheque2.Checked)
                                                                                    {
                                                                                        apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago1.Value);
                                                                                        apertura.MontoCheque += Convert.ToDecimal(valorPago2.Value);
                                                                                        DB.Entry(apertura).State = EntityState.Modified;
                                                                                        if (DB.SaveChanges() <= 0)
                                                                                        {
                                                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (rbSinpe1.Checked && rbEfectivo2.Checked)
                                                                                        {
                                                                                            apertura.MontoTransf += Convert.ToDecimal(valorPago1.Value);
                                                                                            apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago2.Value);
                                                                                            DB.Entry(apertura).State = EntityState.Modified;
                                                                                            if (DB.SaveChanges() <= 0)
                                                                                            {
                                                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (rbSinpe1.Checked && rbSinpeMovil2.Checked)
                                                                                            {
                                                                                                apertura.MontoTransf += Convert.ToDecimal(valorPago1.Value);
                                                                                                apertura.MontoSinpe += Convert.ToDecimal(valorPago2.Value);
                                                                                                DB.Entry(apertura).State = EntityState.Modified;
                                                                                                if (DB.SaveChanges() <= 0)
                                                                                                {
                                                                                                    MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (rbSinpe1.Checked && rbCheque2.Checked)
                                                                                                {
                                                                                                    apertura.MontoTransf += Convert.ToDecimal(valorPago1.Value);
                                                                                                    apertura.MontoCheque += Convert.ToDecimal(valorPago2.Value);
                                                                                                    DB.Entry(apertura).State = EntityState.Modified;
                                                                                                    if (DB.SaveChanges() <= 0)
                                                                                                    {
                                                                                                        MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                    }
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (rbSinpeMovil1.Checked && rbEfectivo2.Checked)
                                                                                                    {
                                                                                                        apertura.MontoSinpe += Convert.ToDecimal(valorPago1.Value);
                                                                                                        apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago2.Value);
                                                                                                        DB.Entry(apertura).State = EntityState.Modified;
                                                                                                        if (DB.SaveChanges() <= 0)
                                                                                                        {
                                                                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                        }
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (rbSinpeMovil1.Checked && rbSinpe2.Checked)
                                                                                                        {
                                                                                                            apertura.MontoSinpe += Convert.ToDecimal(valorPago1.Value);
                                                                                                            apertura.MontoTransf += Convert.ToDecimal(valorPago2.Value);
                                                                                                            DB.Entry(apertura).State = EntityState.Modified;
                                                                                                            if (DB.SaveChanges() <= 0)
                                                                                                            {
                                                                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                            }
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (rbSinpeMovil1.Checked && rbCheque2.Checked)
                                                                                                            {
                                                                                                                apertura.MontoSinpe += Convert.ToDecimal(valorPago1.Value);
                                                                                                                apertura.MontoCheque += Convert.ToDecimal(valorPago2.Value);
                                                                                                                DB.Entry(apertura).State = EntityState.Modified;
                                                                                                                if (DB.SaveChanges() <= 0)
                                                                                                                {
                                                                                                                    MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                                }
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (rbCheque1.Checked && rbEfectivo2.Checked)
                                                                                                                {
                                                                                                                    apertura.MontoCheque += Convert.ToDecimal(valorPago1.Value);
                                                                                                                    apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago2.Value);
                                                                                                                    DB.Entry(apertura).State = EntityState.Modified;
                                                                                                                    if (DB.SaveChanges() <= 0)
                                                                                                                    {
                                                                                                                        MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                                    }
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    if (rbCheque1.Checked && rbSinpe2.Checked)
                                                                                                                    {
                                                                                                                        apertura.MontoCheque += Convert.ToDecimal(valorPago1.Value);
                                                                                                                        apertura.MontoTransf += Convert.ToDecimal(valorPago2.Value);
                                                                                                                        DB.Entry(apertura).State = EntityState.Modified;
                                                                                                                        if (DB.SaveChanges() <= 0)
                                                                                                                        {
                                                                                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                                        }
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        if (rbCheque1.Checked && rbSinpeMovil2.Checked)
                                                                                                                        {
                                                                                                                            apertura.MontoCheque += Convert.ToDecimal(valorPago1.Value);
                                                                                                                            apertura.MontoSinpe += Convert.ToDecimal(valorPago2.Value);
                                                                                                                            DB.Entry(apertura).State = EntityState.Modified;
                                                                                                                            if (DB.SaveChanges() <= 0)
                                                                                                                            {
                                                                                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                            MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                                            using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                                            {
                                                                                frm.ShowDialog();
                                                                            };

                                                                            factura = null;
                                                                            limpiar();
                                                                            break;
                                                                        default:
                                                                            break;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    MessageBox.Show("Factura no se pudo procesar, favor validar que el monto indicado en los metodos de pago, " +
                                                                                    "sumen y den exacto a la cantidad que se muestra al total de la factura a generar, y se haya seleccionado" +
                                                                                    "ambos metodos de pago.",
                                                                                    "Error Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else //no mixto
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
                                                            BackHoe = Convert.ToDecimal(totalBackHoe.Value),
                                                            Tierra = Convert.ToDecimal(txtTierraTotal.Value),
                                                            CantTierra = Convert.ToDecimal(txtCantTierra.Text.Trim()),

                                                            IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                                            IdTipo = Convert.ToInt32(CboxTypeBill.SelectedValue),
                                                            IdEstado = 4,
                                                            IdCliente = Convert.ToInt32(txtNumClient.Text.Trim()),
                                                            IdProveedor = null,
                                                            IdTipoPago = Convert.ToInt32(CboxMetodoPago.SelectedValue),
                                                            IdCierreApert = apertura.IdCierreApert,
                                                        };

                                                        DB.Facturas.Add(factura);

                                                        if (DB.SaveChanges() > 0)
                                                        {

                                                            int IdFact = DB.Facturas.Where((x) => x.IdCliente == factura.IdCliente).Select((x) => x.IdFactura).Max();

                                                            if (CboxIVA.Checked)
                                                            {
                                                                detalleFact = new DetalleFacts
                                                                {
                                                                    Cantidad = Convert.ToDecimal(txtCantTierra.Text.Trim()),
                                                                    Precio = Convert.ToDecimal(txtTierraTotal.Value),
                                                                    Subtotal = Convert.ToDecimal(txtTierraTotal.Value),
                                                                    IVA = Convert.ToDecimal((Convert.ToDouble(txtTierraTotal.Value) * 0.13)),
                                                                    Total = Convert.ToDecimal((Convert.ToDouble(txtTierraTotal.Value) * 0.13)) + Convert.ToDecimal(txtTierraTotal.Value),
                                                                    IdFactura = IdFact,
                                                                    IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1,
                                                                };
                                                                DB.DetalleFacts.Add(detalleFact);

                                                                if (DB.SaveChanges() > 0)
                                                                {
                                                                    int IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1;
                                                                    materiales = DB.Materiales.Find(IdMaterial);
                                                                    materiales.CantidadMaterial = materiales.CantidadMaterial + Convert.ToDecimal(txtCantTierra.Text.Trim());
                                                                    //actualiza el estado
                                                                    if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                    {
                                                                        materiales.IdEstado = 11;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                        {
                                                                            materiales.IdEstado = 10;
                                                                        }
                                                                        else
                                                                        {
                                                                            materiales.IdEstado = 9;
                                                                        }
                                                                    }
                                                                    DB.Entry(materiales).State = EntityState.Modified;

                                                                    if (DB.SaveChanges() > 0)
                                                                    {
                                                                        detalleFact = null;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    MessageBox.Show("Error inesperado, no se pudo actualizar la información del detalle de la factura", "Error Sistema Caja",
                                                                                                                                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                detalleFact = new DetalleFacts
                                                                {
                                                                    Cantidad = Convert.ToDecimal(txtCantTierra.Text.Trim()),
                                                                    Precio = Convert.ToDecimal(txtTierraTotal.Value),
                                                                    Subtotal = Convert.ToDecimal(txtTierraTotal.Value),
                                                                    IVA = 0,
                                                                    Total = Convert.ToDecimal(txtTierraTotal.Value),
                                                                    IdFactura = IdFact,
                                                                    IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1,
                                                                };
                                                                DB.DetalleFacts.Add(detalleFact);

                                                                if (DB.SaveChanges() > 0)
                                                                {
                                                                    int IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1;
                                                                    materiales = DB.Materiales.Find(IdMaterial);
                                                                    materiales.CantidadMaterial = materiales.CantidadMaterial + Convert.ToDecimal(txtCantTierra.Text.Trim());
                                                                    //actualiza el estado
                                                                    if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                    {
                                                                        materiales.IdEstado = 11;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                        {
                                                                            materiales.IdEstado = 10;
                                                                        }
                                                                        else
                                                                        {
                                                                            materiales.IdEstado = 9;
                                                                        }
                                                                    }
                                                                    DB.Entry(materiales).State = EntityState.Modified;

                                                                    if (DB.SaveChanges() > 0)
                                                                    {
                                                                        detalleFact = null;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    MessageBox.Show("Error inesperado, no se pudo actualizar la información del detalle de la factura", "Error Sistema Caja",
                                                                                                                                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                }
                                                            }

                                                            if (Globals.MifrmBillAdd.DtLista.Rows.Count > 0)
                                                            {
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
                                                                        materiales.CantidadMaterial = materiales.CantidadMaterial - Convert.ToDecimal(Row["CantidadMaterial"]);
                                                                        //actualiza el estado
                                                                        if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                        {
                                                                            materiales.IdEstado = 11;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                            {
                                                                                materiales.IdEstado = 10;
                                                                            }
                                                                            else
                                                                            {
                                                                                materiales.IdEstado = 9;
                                                                            }
                                                                        }
                                                                        DB.Entry(materiales).State = EntityState.Modified;

                                                                        if (DB.SaveChanges() > 0)
                                                                        {
                                                                            detalleFact = null;
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                            switch (Convert.ToInt32(CboxMetodoPago.SelectedValue))
                                                            {
                                                                case 1: //efectivo
                                                                    apertura.MontoEfectivoFinal += Total;
                                                                    DB.Entry(apertura).State = EntityState.Modified;
                                                                    if (DB.SaveChanges() <= 0)
                                                                    {
                                                                        MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                    }

                                                                    MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                                    using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                                    {
                                                                        frm.ShowDialog();
                                                                    };

                                                                    factura = null;
                                                                    limpiar();
                                                                    break;
                                                                case 2: //sinpe
                                                                    apertura.MontoTransf += Total;
                                                                    DB.Entry(apertura).State = EntityState.Modified;
                                                                    if (DB.SaveChanges() <= 0)
                                                                    {
                                                                        MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                    }
                                                                    MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                                    using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                                    {
                                                                        frm.ShowDialog();
                                                                    };

                                                                    factura = null;
                                                                    limpiar();
                                                                    break;
                                                                case 3: //sinpe movil
                                                                    apertura.MontoSinpe += Total;
                                                                    DB.Entry(apertura).State = EntityState.Modified;
                                                                    if (DB.SaveChanges() <= 0)
                                                                    {
                                                                        MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                    }

                                                                    MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                                    using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                                    {
                                                                        frm.ShowDialog();
                                                                    };

                                                                    factura = null;
                                                                    limpiar();
                                                                    break;
                                                                case 4: //cheque
                                                                    apertura.MontoCheque += Total;
                                                                    DB.Entry(apertura).State = EntityState.Modified;
                                                                    if (DB.SaveChanges() <= 0)
                                                                    {
                                                                        MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                    }
                                                                    MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                                    using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                                    {
                                                                        frm.ShowDialog();
                                                                    };

                                                                    factura = null;
                                                                    limpiar();
                                                                    break;
                                                                default:
                                                                    break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Factura no se pudo procesada.", "Error Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            factura = null;
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
                                else
                                {
                                    MessageBox.Show("Error, trabajo BackHoe & Tierra seleccionado pero montos a cobrar están en 0", "Error",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                if (chBackHoe.Checked) // donde es trabajo de backhoe solamente
                                {
                                    if (totalBackHoe.Value > 0)
                                    {
                                        DialogResult respuesta = MessageBox.Show("¿Deseas generar la factura de contado?",
                                                  "Registro Factura", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                        if (respuesta == DialogResult.Yes)
                                        {
                                            if (Convert.ToInt32(CboxTypeBill.SelectedValue) == 1) //contado
                                            {
                                                using (FrmLoading frmLoading = new FrmLoading(Wait))
                                                {
                                                    try
                                                    {
                                                        //mixto
                                                        if (Convert.ToInt32(CboxMetodoPago.SelectedValue) == 6) //mixto
                                                        {
                                                            DialogResult respuestaMixto = MessageBox.Show("¿Deseas generar la factura de bajo metodo de pago mixto?," +
                                                                " no se podrá reversar en caso de que se requiera. Para eso sera necesario realizar una nota de crédito.",
                                                              "Registro Factura", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                            if (respuestaMixto == DialogResult.Yes)
                                                            {
                                                                if ((Convert.ToDecimal(valorPago1.Value) + Convert.ToDecimal(valorPago2.Value)) == Total
                                                                                && Convert.ToDecimal(valorPago1.Value) > 0
                                                                                && Convert.ToDecimal(valorPago2.Value) > 0)
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
                                                                        BackHoe = Convert.ToDecimal(totalBackHoe.Value),
                                                                        Tierra = null,
                                                                        CantTierra = null,

                                                                        IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                                                        IdTipo = Convert.ToInt32(CboxTypeBill.SelectedValue),
                                                                        IdEstado = 4,
                                                                        IdCliente = Convert.ToInt32(txtNumClient.Text.Trim()),
                                                                        IdProveedor = null,
                                                                        IdTipoPago = Convert.ToInt32(CboxMetodoPago.SelectedValue),
                                                                        IdCierreApert = apertura.IdCierreApert,
                                                                    };

                                                                    DB.Facturas.Add(factura);

                                                                    if (DB.SaveChanges() > 0)
                                                                    {

                                                                        int IdFact = DB.Facturas.Where((x) => x.IdCliente == factura.IdCliente).Select((x) => x.IdFactura).Max();

                                                                        if (Globals.MifrmBillAdd.DtLista.Rows.Count > 0)
                                                                        {
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
                                                                                    materiales.CantidadMaterial = materiales.CantidadMaterial - Convert.ToDecimal(Row["CantidadMaterial"]);
                                                                                    //actualiza el estado
                                                                                    if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                                    {
                                                                                        materiales.IdEstado = 11;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                                        {
                                                                                            materiales.IdEstado = 10;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            materiales.IdEstado = 9;
                                                                                        }
                                                                                    }
                                                                                    DB.Entry(materiales).State = EntityState.Modified;

                                                                                    if (DB.SaveChanges() > 0)
                                                                                    {
                                                                                        detalleFact = null;
                                                                                    }
                                                                                }
                                                                            }
                                                                        }

                                                                        switch (Convert.ToInt32(CboxMetodoPago.SelectedValue))
                                                                        {
                                                                            case 6: //mixto
                                                                                    //validar los campos de pago respectivo para actualizar la caja abierta

                                                                                if (rbEfectivo1.Checked && rbSinpe2.Checked)
                                                                                {
                                                                                    apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago1.Value);
                                                                                    apertura.MontoTransf += Convert.ToDecimal(valorPago2.Value);
                                                                                    DB.Entry(apertura).State = EntityState.Modified;
                                                                                    if (DB.SaveChanges() <= 0)
                                                                                    {
                                                                                        MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (rbEfectivo1.Checked && rbSinpeMovil2.Checked)
                                                                                    {
                                                                                        apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago1.Value);
                                                                                        apertura.MontoSinpe += Convert.ToDecimal(valorPago2.Value);
                                                                                        DB.Entry(apertura).State = EntityState.Modified;
                                                                                        if (DB.SaveChanges() <= 0)
                                                                                        {
                                                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (rbEfectivo1.Checked && rbCheque2.Checked)
                                                                                        {
                                                                                            apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago1.Value);
                                                                                            apertura.MontoCheque += Convert.ToDecimal(valorPago2.Value);
                                                                                            DB.Entry(apertura).State = EntityState.Modified;
                                                                                            if (DB.SaveChanges() <= 0)
                                                                                            {
                                                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (rbSinpe1.Checked && rbEfectivo2.Checked)
                                                                                            {
                                                                                                apertura.MontoTransf += Convert.ToDecimal(valorPago1.Value);
                                                                                                apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago2.Value);
                                                                                                DB.Entry(apertura).State = EntityState.Modified;
                                                                                                if (DB.SaveChanges() <= 0)
                                                                                                {
                                                                                                    MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (rbSinpe1.Checked && rbSinpeMovil2.Checked)
                                                                                                {
                                                                                                    apertura.MontoTransf += Convert.ToDecimal(valorPago1.Value);
                                                                                                    apertura.MontoSinpe += Convert.ToDecimal(valorPago2.Value);
                                                                                                    DB.Entry(apertura).State = EntityState.Modified;
                                                                                                    if (DB.SaveChanges() <= 0)
                                                                                                    {
                                                                                                        MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                    }
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (rbSinpe1.Checked && rbCheque2.Checked)
                                                                                                    {
                                                                                                        apertura.MontoTransf += Convert.ToDecimal(valorPago1.Value);
                                                                                                        apertura.MontoCheque += Convert.ToDecimal(valorPago2.Value);
                                                                                                        DB.Entry(apertura).State = EntityState.Modified;
                                                                                                        if (DB.SaveChanges() <= 0)
                                                                                                        {
                                                                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                        }
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (rbSinpeMovil1.Checked && rbEfectivo2.Checked)
                                                                                                        {
                                                                                                            apertura.MontoSinpe += Convert.ToDecimal(valorPago1.Value);
                                                                                                            apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago2.Value);
                                                                                                            DB.Entry(apertura).State = EntityState.Modified;
                                                                                                            if (DB.SaveChanges() <= 0)
                                                                                                            {
                                                                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                            }
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (rbSinpeMovil1.Checked && rbSinpe2.Checked)
                                                                                                            {
                                                                                                                apertura.MontoSinpe += Convert.ToDecimal(valorPago1.Value);
                                                                                                                apertura.MontoTransf += Convert.ToDecimal(valorPago2.Value);
                                                                                                                DB.Entry(apertura).State = EntityState.Modified;
                                                                                                                if (DB.SaveChanges() <= 0)
                                                                                                                {
                                                                                                                    MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                                }
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (rbSinpeMovil1.Checked && rbCheque2.Checked)
                                                                                                                {
                                                                                                                    apertura.MontoSinpe += Convert.ToDecimal(valorPago1.Value);
                                                                                                                    apertura.MontoCheque += Convert.ToDecimal(valorPago2.Value);
                                                                                                                    DB.Entry(apertura).State = EntityState.Modified;
                                                                                                                    if (DB.SaveChanges() <= 0)
                                                                                                                    {
                                                                                                                        MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                                    }
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    if (rbCheque1.Checked && rbEfectivo2.Checked)
                                                                                                                    {
                                                                                                                        apertura.MontoCheque += Convert.ToDecimal(valorPago1.Value);
                                                                                                                        apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago2.Value);
                                                                                                                        DB.Entry(apertura).State = EntityState.Modified;
                                                                                                                        if (DB.SaveChanges() <= 0)
                                                                                                                        {
                                                                                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                                        }
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        if (rbCheque1.Checked && rbSinpe2.Checked)
                                                                                                                        {
                                                                                                                            apertura.MontoCheque += Convert.ToDecimal(valorPago1.Value);
                                                                                                                            apertura.MontoTransf += Convert.ToDecimal(valorPago2.Value);
                                                                                                                            DB.Entry(apertura).State = EntityState.Modified;
                                                                                                                            if (DB.SaveChanges() <= 0)
                                                                                                                            {
                                                                                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                                            }
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            if (rbCheque1.Checked && rbSinpeMovil2.Checked)
                                                                                                                            {
                                                                                                                                apertura.MontoCheque += Convert.ToDecimal(valorPago1.Value);
                                                                                                                                apertura.MontoSinpe += Convert.ToDecimal(valorPago2.Value);
                                                                                                                                DB.Entry(apertura).State = EntityState.Modified;
                                                                                                                                if (DB.SaveChanges() <= 0)
                                                                                                                                {
                                                                                                                                    MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                                MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                                                using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                                                {
                                                                                    frm.ShowDialog();
                                                                                };

                                                                                factura = null;
                                                                                limpiar();
                                                                                break;
                                                                            default:
                                                                                break;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        MessageBox.Show("Factura no se pudo procesar, favor validar que el monto indicado en los metodos de pago, " +
                                                                                        "sumen y den exacto a la cantidad que se muestra al total de la factura a generar, y se haya seleccionado" +
                                                                                        "ambos metodos de pago.",
                                                                                        "Error Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
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
                                                                BackHoe = Convert.ToDecimal(totalBackHoe.Value),
                                                                Tierra = null,
                                                                CantTierra = null,

                                                                IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                                                IdTipo = Convert.ToInt32(CboxTypeBill.SelectedValue),
                                                                IdEstado = 4,
                                                                IdCliente = Convert.ToInt32(txtNumClient.Text.Trim()),
                                                                IdProveedor = null,
                                                                IdTipoPago = Convert.ToInt32(CboxMetodoPago.SelectedValue),
                                                                IdCierreApert = apertura.IdCierreApert,
                                                            };

                                                            DB.Facturas.Add(factura);

                                                            if (DB.SaveChanges() > 0)
                                                            {

                                                                int IdFact = DB.Facturas.Where((x) => x.IdCliente == factura.IdCliente).Select((x) => x.IdFactura).Max();

                                                                if (Globals.MifrmBillAdd.DtLista.Rows.Count > 0)
                                                                {
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
                                                                            materiales.CantidadMaterial = materiales.CantidadMaterial - Convert.ToDecimal(Row["CantidadMaterial"]);
                                                                            //actualiza el estado
                                                                            if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                            {
                                                                                materiales.IdEstado = 11;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                                {
                                                                                    materiales.IdEstado = 10;
                                                                                }
                                                                                else
                                                                                {
                                                                                    materiales.IdEstado = 9;
                                                                                }
                                                                            }
                                                                            DB.Entry(materiales).State = EntityState.Modified;

                                                                            if (DB.SaveChanges() > 0)
                                                                            {
                                                                                detalleFact = null;
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                switch (Convert.ToInt32(CboxMetodoPago.SelectedValue))
                                                                {
                                                                    case 1: //efectivo
                                                                        apertura.MontoEfectivoFinal += Total;
                                                                        DB.Entry(apertura).State = EntityState.Modified;
                                                                        if (DB.SaveChanges() <= 0)
                                                                        {
                                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                        }

                                                                        MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                                        using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                                        {
                                                                            frm.ShowDialog();
                                                                        };

                                                                        factura = null;
                                                                        limpiar();
                                                                        break;
                                                                    case 2: //sinpe
                                                                        apertura.MontoTransf += Total;
                                                                        DB.Entry(apertura).State = EntityState.Modified;
                                                                        if (DB.SaveChanges() <= 0)
                                                                        {
                                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                        }
                                                                        MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                                        using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                                        {
                                                                            frm.ShowDialog();
                                                                        };

                                                                        factura = null;
                                                                        limpiar();
                                                                        break;
                                                                    case 3: //sinpe movil
                                                                        apertura.MontoSinpe += Total;
                                                                        DB.Entry(apertura).State = EntityState.Modified;
                                                                        if (DB.SaveChanges() <= 0)
                                                                        {
                                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                        }

                                                                        MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                                        using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                                        {
                                                                            frm.ShowDialog();
                                                                        };

                                                                        factura = null;
                                                                        limpiar();
                                                                        break;
                                                                    case 4: //cheque
                                                                        apertura.MontoCheque += Total;
                                                                        DB.Entry(apertura).State = EntityState.Modified;
                                                                        if (DB.SaveChanges() <= 0)
                                                                        {
                                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                        }
                                                                        MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                                        using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                                        {
                                                                            frm.ShowDialog();
                                                                        };

                                                                        factura = null;
                                                                        limpiar();
                                                                        break;
                                                                    default:
                                                                        break;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("Factura no se pudo procesada.", "Error Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                factura = null;
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
                                    else
                                    {
                                        MessageBox.Show("Error, trabajo BackHoe seleccionado pero monto a cobrar esta en 0", "Error",
                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }  // solamente backhoe
                                else
                                {

                                    if (chTierra.Checked)  // donde es trabajo de tierra solamente
                                    {
                                        if (txtTierraTotal.Value > 0 && txtCantTierra.Value > 0)
                                        {
                                            DialogResult respuesta = MessageBox.Show("¿Deseas generar la factura de contado?",
                                                 "Registro Factura", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                            if (respuesta == DialogResult.Yes)
                                            {
                                                if (Convert.ToInt32(CboxTypeBill.SelectedValue) == 1) //contado
                                                {
                                                    using (FrmLoading frmLoading = new FrmLoading(Wait))
                                                    {
                                                        try
                                                        {
                                                            //mixto
                                                            if (Convert.ToInt32(CboxMetodoPago.SelectedValue) == 6) //mixto
                                                            {
                                                                DialogResult respuestaMixto = MessageBox.Show("¿Deseas generar la factura de bajo metodo de pago mixto?," +
                                                                    " no se podrá reversar en caso de que se requiera. Para eso sera necesario realizar una nota de crédito.",
                                                                  "Registro Factura", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                                if (respuestaMixto == DialogResult.Yes)
                                                                {
                                                                    if ((Convert.ToDecimal(valorPago1.Value) + Convert.ToDecimal(valorPago2.Value)) == Total
                                                                                    && Convert.ToDecimal(valorPago1.Value) > 0
                                                                                    && Convert.ToDecimal(valorPago2.Value) > 0)
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
                                                                            BackHoe = null,
                                                                            Tierra = Convert.ToDecimal(txtTierraTotal.Value),
                                                                            CantTierra = Convert.ToDecimal(txtCantTierra.Text.Trim()),

                                                                            IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                                                            IdTipo = Convert.ToInt32(CboxTypeBill.SelectedValue),
                                                                            IdEstado = 4,
                                                                            IdCliente = Convert.ToInt32(txtNumClient.Text.Trim()),
                                                                            IdProveedor = null,
                                                                            IdTipoPago = Convert.ToInt32(CboxMetodoPago.SelectedValue),
                                                                            IdCierreApert = apertura.IdCierreApert,
                                                                        };

                                                                        DB.Facturas.Add(factura);

                                                                        if (DB.SaveChanges() > 0)
                                                                        {

                                                                            int IdFact = DB.Facturas.Where((x) => x.IdCliente == factura.IdCliente).Select((x) => x.IdFactura).Max();


                                                                            if (CboxIVA.Checked)
                                                                            {
                                                                                detalleFact = new DetalleFacts
                                                                                {
                                                                                    Cantidad = Convert.ToDecimal(txtCantTierra.Text.Trim()),
                                                                                    Precio = Convert.ToDecimal(txtTierraTotal.Value),
                                                                                    Subtotal = Convert.ToDecimal(txtTierraTotal.Value),
                                                                                    IVA = Convert.ToDecimal((Convert.ToDouble(txtTierraTotal.Value) * 0.13)),
                                                                                    Total = Convert.ToDecimal((Convert.ToDouble(txtTierraTotal.Value) * 0.13)) + Convert.ToDecimal(txtTierraTotal.Value),
                                                                                    IdFactura = IdFact,
                                                                                    IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1,
                                                                                };
                                                                                DB.DetalleFacts.Add(detalleFact);

                                                                                if (DB.SaveChanges() > 0)
                                                                                {
                                                                                    int IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1;
                                                                                    materiales = DB.Materiales.Find(IdMaterial);
                                                                                    materiales.CantidadMaterial = materiales.CantidadMaterial + Convert.ToDecimal(txtCantTierra.Text.Trim());
                                                                                    //actualiza el estado
                                                                                    if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                                    {
                                                                                        materiales.IdEstado = 11;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                                        {
                                                                                            materiales.IdEstado = 10;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            materiales.IdEstado = 9;
                                                                                        }
                                                                                    }
                                                                                    DB.Entry(materiales).State = EntityState.Modified;

                                                                                    if (DB.SaveChanges() > 0)
                                                                                    {
                                                                                        detalleFact = null;
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    MessageBox.Show("Error inesperado, no se pudo actualizar la información del detalle de la factura", "Error Sistema Caja",
                                                                                                                                                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                detalleFact = new DetalleFacts
                                                                                {
                                                                                    Cantidad = Convert.ToDecimal(txtCantTierra.Text.Trim()),
                                                                                    Precio = Convert.ToDecimal(txtTierraTotal.Value),
                                                                                    Subtotal = Convert.ToDecimal(txtTierraTotal.Value),
                                                                                    IVA = 0,
                                                                                    Total = Convert.ToDecimal(txtTierraTotal.Value),
                                                                                    IdFactura = IdFact,
                                                                                    IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1,
                                                                                };
                                                                                DB.DetalleFacts.Add(detalleFact);

                                                                                if (DB.SaveChanges() > 0)
                                                                                {
                                                                                    int IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1;
                                                                                    materiales = DB.Materiales.Find(IdMaterial);
                                                                                    materiales.CantidadMaterial = materiales.CantidadMaterial + Convert.ToDecimal(txtCantTierra.Text.Trim());
                                                                                    //actualiza el estado
                                                                                    if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                                    {
                                                                                        materiales.IdEstado = 11;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                                        {
                                                                                            materiales.IdEstado = 10;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            materiales.IdEstado = 9;
                                                                                        }
                                                                                    }
                                                                                    DB.Entry(materiales).State = EntityState.Modified;

                                                                                    if (DB.SaveChanges() > 0)
                                                                                    {
                                                                                        detalleFact = null;
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    MessageBox.Show("Error inesperado, no se pudo actualizar la información del detalle de la factura", "Error Sistema Caja",
                                                                                                                                                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                }
                                                                            }

                                                                            if (Globals.MifrmBillAdd.DtLista.Rows.Count > 0)
                                                                            {
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
                                                                                        materiales.CantidadMaterial = materiales.CantidadMaterial - Convert.ToDecimal(Row["CantidadMaterial"]);
                                                                                        //actualiza el estado
                                                                                        if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                                        {
                                                                                            materiales.IdEstado = 11;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                                            {
                                                                                                materiales.IdEstado = 10;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                materiales.IdEstado = 9;
                                                                                            }
                                                                                        }
                                                                                        DB.Entry(materiales).State = EntityState.Modified;

                                                                                        if (DB.SaveChanges() > 0)
                                                                                        {
                                                                                            detalleFact = null;
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }

                                                                            switch (Convert.ToInt32(CboxMetodoPago.SelectedValue))
                                                                            {
                                                                                case 6: //mixto
                                                                                        //validar los campos de pago respectivo para actualizar la caja abierta

                                                                                    if (rbEfectivo1.Checked && rbSinpe2.Checked)
                                                                                    {
                                                                                        apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago1.Value);
                                                                                        apertura.MontoTransf += Convert.ToDecimal(valorPago2.Value);
                                                                                        DB.Entry(apertura).State = EntityState.Modified;
                                                                                        if (DB.SaveChanges() <= 0)
                                                                                        {
                                                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (rbEfectivo1.Checked && rbSinpeMovil2.Checked)
                                                                                        {
                                                                                            apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago1.Value);
                                                                                            apertura.MontoSinpe += Convert.ToDecimal(valorPago2.Value);
                                                                                            DB.Entry(apertura).State = EntityState.Modified;
                                                                                            if (DB.SaveChanges() <= 0)
                                                                                            {
                                                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (rbEfectivo1.Checked && rbCheque2.Checked)
                                                                                            {
                                                                                                apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago1.Value);
                                                                                                apertura.MontoCheque += Convert.ToDecimal(valorPago2.Value);
                                                                                                DB.Entry(apertura).State = EntityState.Modified;
                                                                                                if (DB.SaveChanges() <= 0)
                                                                                                {
                                                                                                    MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (rbSinpe1.Checked && rbEfectivo2.Checked)
                                                                                                {
                                                                                                    apertura.MontoTransf += Convert.ToDecimal(valorPago1.Value);
                                                                                                    apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago2.Value);
                                                                                                    DB.Entry(apertura).State = EntityState.Modified;
                                                                                                    if (DB.SaveChanges() <= 0)
                                                                                                    {
                                                                                                        MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                    }
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (rbSinpe1.Checked && rbSinpeMovil2.Checked)
                                                                                                    {
                                                                                                        apertura.MontoTransf += Convert.ToDecimal(valorPago1.Value);
                                                                                                        apertura.MontoSinpe += Convert.ToDecimal(valorPago2.Value);
                                                                                                        DB.Entry(apertura).State = EntityState.Modified;
                                                                                                        if (DB.SaveChanges() <= 0)
                                                                                                        {
                                                                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                        }
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (rbSinpe1.Checked && rbCheque2.Checked)
                                                                                                        {
                                                                                                            apertura.MontoTransf += Convert.ToDecimal(valorPago1.Value);
                                                                                                            apertura.MontoCheque += Convert.ToDecimal(valorPago2.Value);
                                                                                                            DB.Entry(apertura).State = EntityState.Modified;
                                                                                                            if (DB.SaveChanges() <= 0)
                                                                                                            {
                                                                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                            }
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (rbSinpeMovil1.Checked && rbEfectivo2.Checked)
                                                                                                            {
                                                                                                                apertura.MontoSinpe += Convert.ToDecimal(valorPago1.Value);
                                                                                                                apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago2.Value);
                                                                                                                DB.Entry(apertura).State = EntityState.Modified;
                                                                                                                if (DB.SaveChanges() <= 0)
                                                                                                                {
                                                                                                                    MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                                }
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (rbSinpeMovil1.Checked && rbSinpe2.Checked)
                                                                                                                {
                                                                                                                    apertura.MontoSinpe += Convert.ToDecimal(valorPago1.Value);
                                                                                                                    apertura.MontoTransf += Convert.ToDecimal(valorPago2.Value);
                                                                                                                    DB.Entry(apertura).State = EntityState.Modified;
                                                                                                                    if (DB.SaveChanges() <= 0)
                                                                                                                    {
                                                                                                                        MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                                    }
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    if (rbSinpeMovil1.Checked && rbCheque2.Checked)
                                                                                                                    {
                                                                                                                        apertura.MontoSinpe += Convert.ToDecimal(valorPago1.Value);
                                                                                                                        apertura.MontoCheque += Convert.ToDecimal(valorPago2.Value);
                                                                                                                        DB.Entry(apertura).State = EntityState.Modified;
                                                                                                                        if (DB.SaveChanges() <= 0)
                                                                                                                        {
                                                                                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                                        }
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        if (rbCheque1.Checked && rbEfectivo2.Checked)
                                                                                                                        {
                                                                                                                            apertura.MontoCheque += Convert.ToDecimal(valorPago1.Value);
                                                                                                                            apertura.MontoEfectivoFinal += Convert.ToDecimal(valorPago2.Value);
                                                                                                                            DB.Entry(apertura).State = EntityState.Modified;
                                                                                                                            if (DB.SaveChanges() <= 0)
                                                                                                                            {
                                                                                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                                            }
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            if (rbCheque1.Checked && rbSinpe2.Checked)
                                                                                                                            {
                                                                                                                                apertura.MontoCheque += Convert.ToDecimal(valorPago1.Value);
                                                                                                                                apertura.MontoTransf += Convert.ToDecimal(valorPago2.Value);
                                                                                                                                DB.Entry(apertura).State = EntityState.Modified;
                                                                                                                                if (DB.SaveChanges() <= 0)
                                                                                                                                {
                                                                                                                                    MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                                                }
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                if (rbCheque1.Checked && rbSinpeMovil2.Checked)
                                                                                                                                {
                                                                                                                                    apertura.MontoCheque += Convert.ToDecimal(valorPago1.Value);
                                                                                                                                    apertura.MontoSinpe += Convert.ToDecimal(valorPago2.Value);
                                                                                                                                    DB.Entry(apertura).State = EntityState.Modified;
                                                                                                                                    if (DB.SaveChanges() <= 0)
                                                                                                                                    {
                                                                                                                                        MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                                                                                    }
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                    MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                                                    using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                                                    {
                                                                                        frm.ShowDialog();
                                                                                    };

                                                                                    factura = null;
                                                                                    limpiar();
                                                                                    break;
                                                                                default:
                                                                                    break;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            MessageBox.Show("Factura no se pudo procesar, favor validar que el monto indicado en los metodos de pago, " +
                                                                                            "sumen y den exacto a la cantidad que se muestra al total de la factura a generar, y se haya seleccionado" +
                                                                                            "ambos metodos de pago.",
                                                                                            "Error Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
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
                                                                    BackHoe = null,
                                                                    Tierra = Convert.ToDecimal(txtTierraTotal.Value),
                                                                    CantTierra = Convert.ToDecimal(txtCantTierra.Text.Trim()),

                                                                    IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                                                    IdTipo = Convert.ToInt32(CboxTypeBill.SelectedValue),
                                                                    IdEstado = 4,
                                                                    IdCliente = Convert.ToInt32(txtNumClient.Text.Trim()),
                                                                    IdProveedor = null,
                                                                    IdTipoPago = Convert.ToInt32(CboxMetodoPago.SelectedValue),
                                                                    IdCierreApert = apertura.IdCierreApert,
                                                                };

                                                                DB.Facturas.Add(factura);

                                                                if (DB.SaveChanges() > 0)
                                                                {

                                                                    int IdFact = DB.Facturas.Where((x) => x.IdCliente == factura.IdCliente).Select((x) => x.IdFactura).Max();

                                                                    if (CboxIVA.Checked)
                                                                    {
                                                                        detalleFact = new DetalleFacts
                                                                        {
                                                                            Cantidad = Convert.ToDecimal(txtCantTierra.Text.Trim()),
                                                                            Precio = Convert.ToDecimal(txtTierraTotal.Value),
                                                                            Subtotal = Convert.ToDecimal(txtTierraTotal.Value),
                                                                            IVA = Convert.ToDecimal((Convert.ToDouble(txtTierraTotal.Value) * 0.13)),
                                                                            Total = Convert.ToDecimal((Convert.ToDouble(txtTierraTotal.Value) * 0.13)) + Convert.ToDecimal(txtTierraTotal.Value),
                                                                            IdFactura = IdFact,
                                                                            IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1,
                                                                        };
                                                                        DB.DetalleFacts.Add(detalleFact);

                                                                        if (DB.SaveChanges() > 0)
                                                                        {
                                                                            int IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1;
                                                                            materiales = DB.Materiales.Find(IdMaterial);
                                                                            materiales.CantidadMaterial = materiales.CantidadMaterial + Convert.ToDecimal(txtCantTierra.Text.Trim());
                                                                            //actualiza el estado
                                                                            if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                            {
                                                                                materiales.IdEstado = 11;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                                {
                                                                                    materiales.IdEstado = 10;
                                                                                }
                                                                                else
                                                                                {
                                                                                    materiales.IdEstado = 9;
                                                                                }
                                                                            }
                                                                            DB.Entry(materiales).State = EntityState.Modified;

                                                                            if (DB.SaveChanges() > 0)
                                                                            {
                                                                                detalleFact = null;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información del detalle de la factura", "Error Sistema Caja",
                                                                                                                                                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        detalleFact = new DetalleFacts
                                                                        {
                                                                            Cantidad = Convert.ToDecimal(txtCantTierra.Text.Trim()),
                                                                            Precio = Convert.ToDecimal(txtTierraTotal.Value),
                                                                            Subtotal = Convert.ToDecimal(txtTierraTotal.Value),
                                                                            IVA = 0,
                                                                            Total = Convert.ToDecimal(txtTierraTotal.Value),
                                                                            IdFactura = IdFact,
                                                                            IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1,
                                                                        };
                                                                        DB.DetalleFacts.Add(detalleFact);

                                                                        if (DB.SaveChanges() > 0)
                                                                        {
                                                                            int IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1;
                                                                            materiales = DB.Materiales.Find(IdMaterial);
                                                                            materiales.CantidadMaterial = materiales.CantidadMaterial + Convert.ToDecimal(txtCantTierra.Text.Trim());
                                                                            //actualiza el estado
                                                                            if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                            {
                                                                                materiales.IdEstado = 11;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                                {
                                                                                    materiales.IdEstado = 10;
                                                                                }
                                                                                else
                                                                                {
                                                                                    materiales.IdEstado = 9;
                                                                                }
                                                                            }
                                                                            DB.Entry(materiales).State = EntityState.Modified;

                                                                            if (DB.SaveChanges() > 0)
                                                                            {
                                                                                detalleFact = null;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información del detalle de la factura", "Error Sistema Caja",
                                                                                                                                                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                        }
                                                                    }

                                                                    if (Globals.MifrmBillAdd.DtLista.Rows.Count > 0)
                                                                    {
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
                                                                                materiales.CantidadMaterial = materiales.CantidadMaterial - Convert.ToDecimal(Row["CantidadMaterial"]);
                                                                                //actualiza el estado
                                                                                if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                                {
                                                                                    materiales.IdEstado = 11;
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                                    {
                                                                                        materiales.IdEstado = 10;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        materiales.IdEstado = 9;
                                                                                    }
                                                                                }
                                                                                DB.Entry(materiales).State = EntityState.Modified;

                                                                                if (DB.SaveChanges() > 0)
                                                                                {
                                                                                    detalleFact = null;
                                                                                }
                                                                            }
                                                                        }
                                                                    }

                                                                    switch (Convert.ToInt32(CboxMetodoPago.SelectedValue))
                                                                    {
                                                                        case 1: //efectivo
                                                                            apertura.MontoEfectivoFinal += Total;
                                                                            DB.Entry(apertura).State = EntityState.Modified;
                                                                            if (DB.SaveChanges() <= 0)
                                                                            {
                                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                            }

                                                                            MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                                            using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                                            {
                                                                                frm.ShowDialog();
                                                                            };

                                                                            factura = null;
                                                                            limpiar();
                                                                            break;
                                                                        case 2: //sinpe
                                                                            apertura.MontoTransf += Total;
                                                                            DB.Entry(apertura).State = EntityState.Modified;
                                                                            if (DB.SaveChanges() <= 0)
                                                                            {
                                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                            }
                                                                            MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                                            using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                                            {
                                                                                frm.ShowDialog();
                                                                            };

                                                                            factura = null;
                                                                            limpiar();
                                                                            break;
                                                                        case 3: //sinpe movil
                                                                            apertura.MontoSinpe += Total;
                                                                            DB.Entry(apertura).State = EntityState.Modified;
                                                                            if (DB.SaveChanges() <= 0)
                                                                            {
                                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                            }

                                                                            MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                                            using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                                            {
                                                                                frm.ShowDialog();
                                                                            };

                                                                            factura = null;
                                                                            limpiar();
                                                                            break;
                                                                        case 4: //cheque
                                                                            apertura.MontoCheque += Total;
                                                                            DB.Entry(apertura).State = EntityState.Modified;
                                                                            if (DB.SaveChanges() <= 0)
                                                                            {
                                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                            }
                                                                            MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                                            using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                                            {
                                                                                frm.ShowDialog();
                                                                            };

                                                                            factura = null;
                                                                            limpiar();
                                                                            break;
                                                                        default:
                                                                            break;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    MessageBox.Show("Factura no se pudo procesada.", "Error Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                    factura = null;
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
                                        else
                                        {
                                            MessageBox.Show("Error, trabajo Tierra seleccionado pero monto a cobrar esta en 0 o la cantidad no feu ingresada", "Error",
                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
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
                    if (Convert.ToInt32(CboxTypeBill.SelectedValue) == 2) //credito
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

                                if (chBackHoe.Checked == false && chTierra.Checked == false) // donde no es trabajo de backhoe ni tierra
                                {
                                    DialogResult respuesta = MessageBox.Show("¿Deseas generar la factura de a crédito?",
                                                         "Registro Factura", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (respuesta == DialogResult.Yes)
                                    {
                                        using (FrmLoading frmLoading = new FrmLoading(Wait))
                                        {
                                            try
                                            {
                                                if (Convert.ToInt32(CboxMetodoPago.SelectedValue) == 5)
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
                                                        BackHoe = null,
                                                        Tierra = null,
                                                        CantTierra = null,

                                                        IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                                        IdTipo = Convert.ToInt32(CboxTypeBill.SelectedValue),
                                                        IdEstado = 3, //pendiente
                                                        IdCliente = Convert.ToInt32(txtNumClient.Text.Trim()),
                                                        IdProveedor = null,
                                                        IdTipoPago = Convert.ToInt32(CboxMetodoPago.SelectedValue),
                                                        IdCierreApert = apertura.IdCierreApert,
                                                    };

                                                    DB.Facturas.Add(factura);

                                                    if (DB.SaveChanges() > 0)
                                                    {

                                                        int IdFact = DB.Facturas.Where((x) => x.IdCliente == factura.IdCliente).Select((x) => x.IdFactura).Max();

                                                        if (Globals.MifrmBillAdd.DtLista.Rows.Count > 0)
                                                        {
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
                                                                    materiales.CantidadMaterial = materiales.CantidadMaterial - Convert.ToDecimal(Row["CantidadMaterial"]);
                                                                    //actualiza el estado
                                                                    if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                    {
                                                                        materiales.IdEstado = 11;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                        {
                                                                            materiales.IdEstado = 10;
                                                                        }
                                                                        else
                                                                        {
                                                                            materiales.IdEstado = 9;
                                                                        }
                                                                    }
                                                                    DB.Entry(materiales).State = EntityState.Modified;

                                                                    if (DB.SaveChanges() > 0)
                                                                    {
                                                                        detalleFact = null;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        apertura.MontoCredito += Total;
                                                        DB.Entry(apertura).State = EntityState.Modified;
                                                        if (DB.SaveChanges() <= 0)
                                                        {
                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        }

                                                        MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                        using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                        {
                                                            frm.ShowDialog();
                                                        };

                                                        factura = null;
                                                        limpiar();
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Factura no se pudo procesada.", "Error Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        factura = null;
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
                                else
                                {
                                    if (chBackHoe.Checked && chTierra.Checked) // dond es trabajo de backhoe & tierra
                                    {
                                        if (totalBackHoe.Value > 0 && txtTierraTotal.Value > 0 && txtCantTierra.Value > 0)
                                        {
                                            DialogResult respuesta = MessageBox.Show("¿Deseas generar la factura de a crédito?",
                                                         "Registro Factura", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                            if (respuesta == DialogResult.Yes)
                                            {
                                                using (FrmLoading frmLoading = new FrmLoading(Wait))
                                                {
                                                    try
                                                    {
                                                        if (Convert.ToInt32(CboxMetodoPago.SelectedValue) == 5)
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
                                                                BackHoe = Convert.ToDecimal(totalBackHoe.Value),
                                                                Tierra = Convert.ToDecimal(txtTierraTotal.Value),
                                                                CantTierra = Convert.ToDecimal(txtCantTierra.Text.Trim()),

                                                                IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                                                IdTipo = Convert.ToInt32(CboxTypeBill.SelectedValue),
                                                                IdEstado = 3, //pendiente
                                                                IdCliente = Convert.ToInt32(txtNumClient.Text.Trim()),
                                                                IdProveedor = null,
                                                                IdTipoPago = Convert.ToInt32(CboxMetodoPago.SelectedValue),
                                                                IdCierreApert = apertura.IdCierreApert,
                                                            };

                                                            DB.Facturas.Add(factura);

                                                            if (DB.SaveChanges() > 0)
                                                            {

                                                                int IdFact = DB.Facturas.Where((x) => x.IdCliente == factura.IdCliente).Select((x) => x.IdFactura).Max();

                                                                if (CboxIVA.Checked)
                                                                {
                                                                    detalleFact = new DetalleFacts
                                                                    {
                                                                        Cantidad = Convert.ToDecimal(txtCantTierra.Text.Trim()),
                                                                        Precio = Convert.ToDecimal(txtTierraTotal.Value),
                                                                        Subtotal = Convert.ToDecimal(txtTierraTotal.Value),
                                                                        IVA = Convert.ToDecimal((Convert.ToDouble(txtTierraTotal.Value) * 0.13)),
                                                                        Total = Convert.ToDecimal((Convert.ToDouble(txtTierraTotal.Value) * 0.13)) + Convert.ToDecimal(txtTierraTotal.Value),
                                                                        IdFactura = IdFact,
                                                                        IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1, // else indica que si no es 2 seria tierra normal
                                                                    };
                                                                    DB.DetalleFacts.Add(detalleFact);

                                                                    if (DB.SaveChanges() > 0)
                                                                    {
                                                                        int IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1;
                                                                        materiales = DB.Materiales.Find(IdMaterial);
                                                                        materiales.CantidadMaterial = materiales.CantidadMaterial + Convert.ToDecimal(txtCantTierra.Text.Trim());
                                                                        //actualiza el estado
                                                                        if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                        {
                                                                            materiales.IdEstado = 11;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                            {
                                                                                materiales.IdEstado = 10;
                                                                            }
                                                                            else
                                                                            {
                                                                                materiales.IdEstado = 9;
                                                                            }
                                                                        }
                                                                        DB.Entry(materiales).State = EntityState.Modified;

                                                                        if (DB.SaveChanges() > 0)
                                                                        {
                                                                            detalleFact = null;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        MessageBox.Show("Error inesperado, no se pudo actualizar la información del detalle de la factura", "Error Sistema Caja",
                                                                                                                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    detalleFact = new DetalleFacts
                                                                    {
                                                                        Cantidad = Convert.ToDecimal(txtCantTierra.Text.Trim()),
                                                                        Precio = Convert.ToDecimal(txtTierraTotal.Value),
                                                                        Subtotal = Convert.ToDecimal(txtTierraTotal.Value),
                                                                        IVA = 0,
                                                                        Total = Convert.ToDecimal(txtTierraTotal.Value),
                                                                        IdFactura = IdFact,
                                                                        IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1,
                                                                    };
                                                                    DB.DetalleFacts.Add(detalleFact);

                                                                    if (DB.SaveChanges() > 0)
                                                                    {
                                                                        int IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1;
                                                                        materiales = DB.Materiales.Find(IdMaterial);
                                                                        materiales.CantidadMaterial = materiales.CantidadMaterial + Convert.ToDecimal(txtCantTierra.Text.Trim());
                                                                        //actualiza el estado
                                                                        if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                        {
                                                                            materiales.IdEstado = 11;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                            {
                                                                                materiales.IdEstado = 10;
                                                                            }
                                                                            else
                                                                            {
                                                                                materiales.IdEstado = 9;
                                                                            }
                                                                        }
                                                                        DB.Entry(materiales).State = EntityState.Modified;

                                                                        if (DB.SaveChanges() > 0)
                                                                        {
                                                                            detalleFact = null;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        MessageBox.Show("Error inesperado, no se pudo actualizar la información del detalle de la factura", "Error Sistema Caja",
                                                                                                                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                    }
                                                                }

                                                                if (Globals.MifrmBillAdd.DtLista.Rows.Count > 0)
                                                                {
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
                                                                            materiales.CantidadMaterial = materiales.CantidadMaterial - Convert.ToDecimal(Row["CantidadMaterial"]);
                                                                            //actualiza el estado
                                                                            if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                            {
                                                                                materiales.IdEstado = 11;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                                {
                                                                                    materiales.IdEstado = 10;
                                                                                }
                                                                                else
                                                                                {
                                                                                    materiales.IdEstado = 9;
                                                                                }
                                                                            }
                                                                            DB.Entry(materiales).State = EntityState.Modified;

                                                                            if (DB.SaveChanges() > 0)
                                                                            {
                                                                                detalleFact = null;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                apertura.MontoCredito += Total;
                                                                DB.Entry(apertura).State = EntityState.Modified;
                                                                if (DB.SaveChanges() <= 0)
                                                                {
                                                                    MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                }

                                                                MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                                using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                                {
                                                                    frm.ShowDialog();
                                                                };

                                                                factura = null;
                                                                limpiar();
                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("Factura no se pudo procesada.", "Error Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                factura = null;
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
                                        else
                                        {
                                            MessageBox.Show("Error, trabajo BackHoe & Tierra seleccionado pero montos a cobrar están en 0", "Error",
                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                    {
                                        if (chBackHoe.Checked) // donde es trabajo de backhoe solamente
                                        {
                                            if (totalBackHoe.Value > 0)
                                            {
                                                DialogResult respuesta = MessageBox.Show("¿Deseas generar la factura de a crédito?",
                                                         "Registro Factura", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                                if (respuesta == DialogResult.Yes)
                                                {
                                                    using (FrmLoading frmLoading = new FrmLoading(Wait))
                                                    {
                                                        try
                                                        {
                                                            if (Convert.ToInt32(CboxMetodoPago.SelectedValue) == 5)
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
                                                                    BackHoe = Convert.ToDecimal(totalBackHoe.Value),
                                                                    Tierra = null,
                                                                    CantTierra = null,

                                                                    IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                                                    IdTipo = Convert.ToInt32(CboxTypeBill.SelectedValue),
                                                                    IdEstado = 3, //pendiente
                                                                    IdCliente = Convert.ToInt32(txtNumClient.Text.Trim()),
                                                                    IdProveedor = null,
                                                                    IdTipoPago = Convert.ToInt32(CboxMetodoPago.SelectedValue),
                                                                    IdCierreApert = apertura.IdCierreApert,
                                                                };

                                                                DB.Facturas.Add(factura);

                                                                if (DB.SaveChanges() > 0)
                                                                {

                                                                    int IdFact = DB.Facturas.Where((x) => x.IdCliente == factura.IdCliente).Select((x) => x.IdFactura).Max();


                                                                    if (Globals.MifrmBillAdd.DtLista.Rows.Count > 0)
                                                                    {
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
                                                                                materiales.CantidadMaterial = materiales.CantidadMaterial - Convert.ToDecimal(Row["CantidadMaterial"]);
                                                                                //actualiza el estado
                                                                                if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                                {
                                                                                    materiales.IdEstado = 11;
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                                    {
                                                                                        materiales.IdEstado = 10;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        materiales.IdEstado = 9;
                                                                                    }
                                                                                }
                                                                                DB.Entry(materiales).State = EntityState.Modified;

                                                                                if (DB.SaveChanges() > 0)
                                                                                {
                                                                                    detalleFact = null;
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    apertura.MontoCredito += Total;
                                                                    DB.Entry(apertura).State = EntityState.Modified;
                                                                    if (DB.SaveChanges() <= 0)
                                                                    {
                                                                        MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                    }

                                                                    MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                                    using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                                    {
                                                                        frm.ShowDialog();
                                                                    };

                                                                    factura = null;
                                                                    limpiar();
                                                                }
                                                                else
                                                                {
                                                                    MessageBox.Show("Factura no se pudo procesada.", "Error Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                    factura = null;
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
                                            else
                                            {
                                                MessageBox.Show("Error, trabajo BackHoe seleccionado pero monto a cobrar esta en 0", "Error",
                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                        else
                                        {

                                            if (chTierra.Checked)  // donde es trabajo de tierra solamente
                                            {
                                                if (txtTierraTotal.Value > 0 && txtCantTierra.Value > 0 )
                                                {
                                                    DialogResult respuesta = MessageBox.Show("¿Deseas generar la factura de a crédito?",
                                                         "Registro Factura", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                                    if (respuesta == DialogResult.Yes)
                                                    {
                                                        using (FrmLoading frmLoading = new FrmLoading(Wait))
                                                        {
                                                            try
                                                            {
                                                                if (Convert.ToInt32(CboxMetodoPago.SelectedValue) == 5)
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
                                                                        BackHoe = null,
                                                                        Tierra = Convert.ToDecimal(txtTierraTotal.Value),
                                                                        CantTierra = Convert.ToDecimal(txtCantTierra.Text.Trim()),

                                                                        IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                                                        IdTipo = Convert.ToInt32(CboxTypeBill.SelectedValue),
                                                                        IdEstado = 3, //pendiente
                                                                        IdCliente = Convert.ToInt32(txtNumClient.Text.Trim()),
                                                                        IdProveedor = null,
                                                                        IdTipoPago = Convert.ToInt32(CboxMetodoPago.SelectedValue),
                                                                        IdCierreApert = apertura.IdCierreApert,
                                                                    };

                                                                    DB.Facturas.Add(factura);

                                                                    if (DB.SaveChanges() > 0)
                                                                    {

                                                                        int IdFact = DB.Facturas.Where((x) => x.IdCliente == factura.IdCliente).Select((x) => x.IdFactura).Max();

                                                                        if (CboxIVA.Checked)
                                                                        {
                                                                            detalleFact = new DetalleFacts
                                                                            {
                                                                                Cantidad = Convert.ToDecimal(txtCantTierra.Text.Trim()),
                                                                                Precio = Convert.ToDecimal(txtTierraTotal.Value),
                                                                                Subtotal = Convert.ToDecimal(txtTierraTotal.Value),
                                                                                IVA = Convert.ToDecimal((Convert.ToDouble(txtTierraTotal.Value) * 0.13)),
                                                                                Total = Convert.ToDecimal((Convert.ToDouble(txtTierraTotal.Value) * 0.13)) + Convert.ToDecimal(txtTierraTotal.Value),
                                                                                IdFactura = IdFact,
                                                                                IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1, // else indica que si no es 2 seria tierra normal
                                                                            };
                                                                            DB.DetalleFacts.Add(detalleFact);

                                                                            if (DB.SaveChanges() > 0)
                                                                            {
                                                                                int IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1;
                                                                                materiales = DB.Materiales.Find(IdMaterial);
                                                                                materiales.CantidadMaterial = materiales.CantidadMaterial + Convert.ToDecimal(txtCantTierra.Text.Trim());
                                                                                //actualiza el estado
                                                                                if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                                {
                                                                                    materiales.IdEstado = 11;
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                                    {
                                                                                        materiales.IdEstado = 10;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        materiales.IdEstado = 9;
                                                                                    }
                                                                                }
                                                                                DB.Entry(materiales).State = EntityState.Modified;

                                                                                if (DB.SaveChanges() > 0)
                                                                                {
                                                                                    detalleFact = null;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información del detalle de la factura", "Error Sistema Caja",
                                                                                                                                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            detalleFact = new DetalleFacts
                                                                            {
                                                                                Cantidad = Convert.ToDecimal(txtCantTierra.Text.Trim()),
                                                                                Precio = Convert.ToDecimal(txtTierraTotal.Value),
                                                                                Subtotal = Convert.ToDecimal(txtTierraTotal.Value),
                                                                                IVA = 0,
                                                                                Total = Convert.ToDecimal(txtTierraTotal.Value),
                                                                                IdFactura = IdFact,
                                                                                IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1,
                                                                            };
                                                                            DB.DetalleFacts.Add(detalleFact);

                                                                            if (DB.SaveChanges() > 0)
                                                                            {
                                                                                int IdMaterial = (chTierraNormal.Checked == true) ? 1 : (chTierraRoja.Checked == true) ? 2 : 1;
                                                                                materiales = DB.Materiales.Find(IdMaterial);
                                                                                materiales.CantidadMaterial = materiales.CantidadMaterial + Convert.ToDecimal(txtCantTierra.Text.Trim());
                                                                                //actualiza el estado
                                                                                if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                                {
                                                                                    materiales.IdEstado = 11;
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                                    {
                                                                                        materiales.IdEstado = 10;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        materiales.IdEstado = 9;
                                                                                    }
                                                                                }
                                                                                DB.Entry(materiales).State = EntityState.Modified;

                                                                                if (DB.SaveChanges() > 0)
                                                                                {
                                                                                    detalleFact = null;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                MessageBox.Show("Error inesperado, no se pudo actualizar la información del detalle de la factura", "Error Sistema Caja",
                                                                                                                                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                            }
                                                                        }

                                                                        if (Globals.MifrmBillAdd.DtLista.Rows.Count > 0)
                                                                        {
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
                                                                                    materiales.CantidadMaterial = materiales.CantidadMaterial - Convert.ToDecimal(Row["CantidadMaterial"]);
                                                                                    //actualiza el estado
                                                                                    if (materiales.CantidadMaterial > (materiales.Minimos + 2))
                                                                                    {
                                                                                        materiales.IdEstado = 11;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (((materiales.Minimos + 2) > materiales.CantidadMaterial) && materiales.CantidadMaterial > 0)
                                                                                        {
                                                                                            materiales.IdEstado = 10;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            materiales.IdEstado = 9;
                                                                                        }
                                                                                    }
                                                                                    DB.Entry(materiales).State = EntityState.Modified;

                                                                                    if (DB.SaveChanges() > 0)
                                                                                    {
                                                                                        detalleFact = null;
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                        apertura.MontoCredito += Total;
                                                                        DB.Entry(apertura).State = EntityState.Modified;
                                                                        if (DB.SaveChanges() <= 0)
                                                                        {
                                                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                        }

                                                                        MessageBox.Show("Factura generada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                                        using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                                        {
                                                                            frm.ShowDialog();
                                                                        };

                                                                        factura = null;
                                                                        limpiar();
                                                                    }
                                                                    else
                                                                    {
                                                                        MessageBox.Show("Factura no se pudo procesada.", "Error Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                        factura = null;
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
                                                else
                                                {
                                                    MessageBox.Show("Error, trabajo Tierra seleccionado pero monto a cobrar esta en 0 o la cantidad no fue ingresada", "Error",
                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                            }
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
                            MessageBox.Show("No se selecciono una fecha mayor al 2 días del actual, favor validar.", "Error Factura a Crédito.", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void chBackHoe_CheckedChanged(object sender, EventArgs e)
        {
            if (chBackHoe.Checked)
            {
                lblTotalBackHoe.Visible = true;
                totalBackHoe.Visible = true;
            }
            else
            {
                lblTotalBackHoe.Visible = false;
                totalBackHoe.Visible = false;
                totalBackHoe.Value = 0;
            }
        }

        private void chTierra_CheckedChanged(object sender, EventArgs e)
        {
            if (chTierra.Checked)
            {
                bool R = true;

                foreach (DataRow Row in Globals.MifrmBillAdd.DtLista.Rows)
                {
                    if (Convert.ToInt32(Row["IdMaterial"]) == 1 || Convert.ToInt32(Row["IdMaterial"]) == 2)
                    {
                        MessageBox.Show("Ya se selecciono Tierra Normal o Roja para la lista de venta, no se puede seleccionar trabajo de tierra, " +
                            "eliminelo de la lista para poder seleccionar trabajo de tierra.", "Error Factura a Crédito.",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        R = false;
                    }

                }
                if (R)
                {
                    chTierraNormal.Visible = true;
                    chTierraRoja.Visible = true;
                    lblTierraNormal.Visible = true;
                    lblTierraRoja.Visible = true;
                }
                else
                {
                    chTierra.Checked = false;
                }

            }
            else
            {
                chTierraNormal.Visible = false;
                chTierraRoja.Visible = false;
                lblTierraNormal.Visible = false;
                lblTierraRoja.Visible = false;

                txtTierraTotal.Visible = false;
                lblTierraTotal.Visible = false;
            }
        }

        private void chTierraNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (chTierraNormal.Checked == false && chTierraRoja.Checked == false)
            {
                chTierraNormal.Checked = false;
                chTierraNormal.Enabled = true;
                chTierraRoja.Checked = false;
                chTierraRoja.Enabled = true;
                txtTierraTotal.Visible = false;
                lblTierraTotal.Visible = false;
                lblCantTierra.Visible = false;
                txtCantTierra.Visible = false;
            }
            else
            {
                if (chTierraNormal.Checked)
                {

                    bool R = true;

                    foreach (DataRow Row in Globals.MifrmBillAdd.DtLista.Rows)
                    {
                        if (Convert.ToInt32(Row["IdMaterial"]) == 1)
                        {
                            MessageBox.Show("Ya se selecciono Tierra Normal para la lista de venta, no se puede seleccionar trabajo de tierra, " +
                                "eliminelo de la lista para poder seleccionar trabajo de tierra.", "Error Factura a Crédito.",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            R = false;
                        }

                    }
                    if (R)
                    {
                        chTierraRoja.Checked = false;
                        chTierraRoja.Enabled = false;
                        txtTierraTotal.Visible = true;
                        lblTierraTotal.Visible = true;
                        lblCantTierra.Visible = true;
                        txtCantTierra.Visible = true;
                    }
                    else
                    {
                        chTierra.Checked = false;
                        chTierraNormal.Checked = false;
                    }
                }
            }
        }

        private void chTierraRoja_CheckedChanged(object sender, EventArgs e)
        {
            

            if (chTierraNormal.Checked == false && chTierraRoja.Checked == false)
            {
                chTierraNormal.Checked = false;
                chTierraNormal.Enabled = true;
                chTierraRoja.Checked = false;
                chTierraRoja.Enabled = true;
                txtTierraTotal.Visible = false;
                lblTierraTotal.Visible = false;
                lblCantTierra.Visible = false;
                txtCantTierra.Visible = false;
            }
            else
            {
                if (chTierraRoja.Checked)
                {
                    bool R = true;

                    foreach (DataRow Row in Globals.MifrmBillAdd.DtLista.Rows)
                    {
                        if (Convert.ToInt32(Row["IdMaterial"]) == 2)
                        {
                            MessageBox.Show("Ya se selecciono Tierra Roja para la lista de venta, no se puede seleccionar trabajo de tierra, " +
                                "eliminelo de la lista para poder seleccionar trabajo de tierra.", "Error Factura a Crédito.",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            R = false;
                        }

                    }
                    if (R)
                    {
                        chTierraNormal.Checked = false;
                        chTierraNormal.Enabled = false;
                        txtTierraTotal.Visible = true;
                        lblTierraTotal.Visible = true;
                        lblCantTierra.Visible = true;
                        txtCantTierra.Visible = true;
                    }
                    else
                    {
                        chTierra.Checked = false;
                        chTierraRoja.Checked = false;
                    }

                    
                }
            }

        }

        //actualiza el totalizador de factura cuando cambia el valor del monto por transporte
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

        private void totalBackHoe_ValueChanged(object sender, EventArgs e)
        {
            if (totalBackHoe.Value >= 0)
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
                totalBackHoe.Value = 0;
            }
        }

        private void txtTierraTotal_ValueChanged(object sender, EventArgs e)
        {
            if (txtTierraTotal.Value >= 0)
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
                txtTierraTotal.Value = 0;
            }
        }

        private void txtCantTierra_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }




    }
}
