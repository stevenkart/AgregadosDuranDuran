using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Agregados.Forms.Customers
{
    public partial class FrmCustomerSearch : Form
    {
        //variables del form
        AgregadosEntities DB;
        Clientes cliente;
        public FrmCustomerSearch()
        {
            InitializeComponent();
            DB = new AgregadosEntities();
            cliente = new Clientes();
        }

        //limpiar el form, ventana
        private void limpiar()
        {
            txtBuscarId.Text = null;
            txtName.Text = null;
            txtBuscarId.Enabled = true;
            txtName.Enabled = true;
        }
        private void imgClean_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        //llena la lista de datos de cliente
        private void LlenarLista()
        {
            try
            {
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
                dgvListaClientes.DataSource = result.ToList();
                limpiar();
            }
            catch (Exception)
            {

                throw;
            }
        }
        //valida que sea numero
        private void txtBuscarId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }

        //cuando inicia el form, llama la funcion Llenar Lista
        private void FrmCustomerSearch_Load(object sender, EventArgs e)
        {
            LlenarLista();
        }

        
        //filtros de busqueda
        private void txtBuscarId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtBuscarId.Text.Trim()) && txtBuscarId.TextLength > 0)
                {
                    txtName.Enabled = false;
                    int num = Convert.ToInt32(txtBuscarId.Text.Trim());
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
                    dgvListaClientes.DataSource = result.ToList();

                }
                else
                {
                    txtName.Enabled = true;
                    LlenarLista();
                    limpiar();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtName.Text.Trim()) && txtName.TextLength > 0)
                {
                    txtBuscarId.Enabled = false;
                    string data = txtName.Text.Trim();
                    var result = from cl in DB.Clientes
                                 join es in DB.Estados
                                 on cl.IdEstado equals es.IdEstado
                                 where (cl.IdEstado == 1 && cl.Identificacion.Contains(data) || cl.Nombre.Contains(data))
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
                    dgvListaClientes.DataSource = result.ToList();

                }
                else
                {
                    txtBuscarId.Enabled = true;
                    LlenarLista();
                    limpiar();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }



        //cancela el form, vuelve a la factura
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        //cuando se cierra el form
        private void FrmCustomerSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        //selecciona un elemento de la lista y obtiene el dato
        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListaClientes.SelectedRows.Count == 1)
                {
                    DataGridViewRow FilaSelected = dgvListaClientes.SelectedRows[0];
                    int IdCliente = Convert.ToInt32(FilaSelected.Cells["CIdCliente"].Value);
                    Globals.MifrmBillAdd.txtNumClient.Text = Convert.ToString(IdCliente);

                    /*
                    DataRow NuevaFilaEnFacturacion = Globals.MifrmBillAdd.DtLista.NewRow();

                    NuevaFilaEnFacturacion["IdCliente"] = Convert.ToInt32(FilaSelected.Cells["CIdCliente"].Value);
                    NuevaFilaEnFacturacion["Nombre"] = Convert.ToInt32(FilaSelected.Cells["CNombre"].Value);
                    NuevaFilaEnFacturacion["Identificacion"] = Convert.ToInt32(FilaSelected.Cells["CIdentificacion"].Value);
                    NuevaFilaEnFacturacion["TipoCliente"] = Convert.ToInt32(FilaSelected.Cells["CTipoCliente"].Value);
                    NuevaFilaEnFacturacion["Telefono"] = Convert.ToDateTime(FilaSelected.Cells["CTelefono"].Value);
                    NuevaFilaEnFacturacion["IDEstado"] = Convert.ToInt32(FilaSelected.Cells["CIDEstado"].Value);

                    Globals.MifrmBillAdd.DtLista.Rows.Add(NuevaFilaEnFacturacion);
                    */

                    this.DialogResult = DialogResult.OK;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

       
    }
}
