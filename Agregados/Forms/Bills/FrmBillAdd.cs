using Agregados.Forms.Customers;
using Agregados.Forms.Materials;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Forms.Bills
{
    public partial class FrmBillAdd : Form
    {
        //variables del form
        AgregadosEntities DB;
        Facturas factura;
        Materiales materiales;
        DetalleFacts detalleFact;
        public DataTable DtLista { get; set; }

        //propiedades para validar que item se seleccione y cantidad linea seleccionada
        public int CantidadItem = 0;

        #region PropiedadesDeTotalizacion
        public decimal SubTotal { get; set; } // subtotal
        public decimal TasaImpuesto { get; set; } //tasa aplicada al 13%
        public decimal Total { get; set; } // total aplicado con el el IVA 13%
        #endregion


        public FrmBillAdd()
        {
            InitializeComponent();
            DB = new AgregadosEntities();
            factura = new Facturas();    
            detalleFact = new DetalleFacts();
            materiales = new Materiales();
            DtLista = new DataTable();
            
        }

        private void FrmBillAdd_Load(object sender, EventArgs e)
        {
            tmrFechaHora.Enabled = true;
            lblUsuarioLogueado.Text = $"( {Globals.MyGlobalUser.NombreUsuario} )" + $" {Globals.MyGlobalUser.NombreEmpleado} "; 
            lblTypeFact.Visible = false;
            dateFinal.Visible = false;
            lblDate.Text = DateTime.Now.Date.ToLongDateString();
            CargarTiposFactura();
            CargarTiposPagos();
            lblReferencia.Visible = false;
            txtReferencia.Visible = false;

            //List<Materiales> materialesDT = new List<Materiales>();

            ActivarAdd();
            DtLista = makeDataTableSchema();



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

        //calcular datos del datatable
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
           }
           else
           {
               if (Convert.ToInt32(CboxTypeBill.SelectedValue) == 2)
               {
                   lblTypeFact.Visible = true;
                   dateFinal.Visible = true;
               }
           }
       }

       //carga Cbox Tipo Pago
       private void CargarTiposPagos()
       {

           //Metodo que permite llamar y obtener los datos filtrados de los materiales y mostrarlos en el comboBox
           var dt = DB.MetodosPagos.ToList();

           CboxMetodoPago.ValueMember = "IdTipoPago";
           CboxMetodoPago.DisplayMember = "TipoPago";
           CboxMetodoPago.DataSource = dt;
           CboxMetodoPago.SelectedIndex = -1;
       }
       //cambio tipo pago, muestra la info de pago

       private void CboxMetodoPago_SelectedValueChanged(object sender, EventArgs e)
       {
           if (Convert.ToInt32(CboxMetodoPago.SelectedValue) == 1)
           {
               lblReferencia.Visible = false;
               txtReferencia.Visible = false;
           }
           else
           {
               lblReferencia.Visible = true;
               txtReferencia.Visible = true;
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
           Form FrmMaterialSearch = new FrmMaterialSearch();

           DialogResult resp = FrmMaterialSearch.ShowDialog();

           if (resp == DialogResult.OK)
           {
                if (DtLista.Rows.Count > 0)
                {
                    dgvMaterials.DataSource = DtLista;
                    dgvMaterials.ClearSelection();

                    Totalizar();
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
                    Totalizar();                 
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
        //actualiza el totalizador de factura cuando cambia el valor del monto por transporte
        private void txtTransporte_ValueChanged(object sender, EventArgs e)
        {
            if (txtTransporte.Value >= 0)
            {
                Totalizar();
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
            foreach (DataRow Row in DtLista.Rows)
            {
                valor = valor + 1;

            }
            lblLineas.Text = Convert.ToString(valor);
        }


    }
}
