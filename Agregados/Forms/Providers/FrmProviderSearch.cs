using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Forms.Providers
{
    public partial class FrmProviderSearch : Form
    {
        //variables del form
        AgregadosEntities DB;
        Proveedores proveedor;

        public FrmProviderSearch()
        {
            InitializeComponent();

            DB = new AgregadosEntities();
            proveedor = new Proveedores();
        }

        //load event
        private void FrmProviderSearch_Load(object sender, EventArgs e)
        {
            LlenarLista();
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
                var result = from cl in DB.Proveedores
                             join es in DB.Estados
                             on cl.IdEstado equals es.IdEstado
                             where (cl.IdEstado == 1)
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
                dgvListaProveedores.DataSource = result.ToList();
                limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw;
            }
        }


        //filtros de busqueda
        private void txtBuscarId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }
        private void txtBuscarId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtBuscarId.Text.Trim()) && txtBuscarId.TextLength > 0)
                {
                    txtName.Enabled = false;
                    int num = Convert.ToInt32(txtBuscarId.Text.Trim());
                    var result = from cl in DB.Proveedores
                                 join es in DB.Estados
                                 on cl.IdEstado equals es.IdEstado
                                 where (cl.IdEstado == 1 && cl.IdProveedor == num)
                                 select new
                                 {
                                     cl.IdProveedor,
                                     cl.Identificacion,
                                     cl.Nombre,
                                     //Lambda Expresion IF -ELSE para validar tipo de Cliente y proceder a indicarlo en modo texto
                                     TipoProveedor = (cl.IdProveedor == 1) ? "Físico" : (cl.IdProveedor == 2) ? "Júridico" : "",
                                     cl.Telefono,
                                     cl.Telefono2,
                                     cl.Correo,
                                     cl.Direccion,
                                     cl.Detalles,
                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                 };
                    dgvListaProveedores.DataSource = result.ToList();

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
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtName.Text.Trim()) && txtName.TextLength > 0)
                {
                    txtBuscarId.Enabled = false;
                    string data = txtName.Text.Trim();
                    var result = from cl in DB.Proveedores
                                 join es in DB.Estados
                                 on cl.IdEstado equals es.IdEstado
                                 where (cl.IdEstado == 1 && cl.Identificacion.Contains(data) || cl.Nombre.Contains(data))
                                 select new
                                 {
                                     cl.IdProveedor,
                                     cl.Identificacion,
                                     cl.Nombre,
                                     //Lambda Expresion IF -ELSE para validar tipo de Cliente y proceder a indicarlo en modo texto
                                     TipoProveedor = (cl.IdProveedor == 1) ? "Físico" : (cl.IdProveedor == 2) ? "Júridico" : "",
                                     cl.Telefono,
                                     cl.Telefono2,
                                     cl.Correo,
                                     cl.Direccion,
                                     cl.Detalles,
                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                 };
                    dgvListaProveedores.DataSource = result.ToList();

                }
                else
                {
                    txtBuscarId.Enabled = true;
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


        //cancela el form, vuelve a la factura
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void FrmProviderSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListaProveedores.SelectedRows.Count == 1)
                {
                    DataGridViewRow FilaSelected = dgvListaProveedores.SelectedRows[0];
                    int IdProveedor = Convert.ToInt32(FilaSelected.Cells["CIdProveedor"].Value);
                    Globals.MifrmBillProviderAdd.txtNumProve.Text = Convert.ToString(IdProveedor); //todo Fact proveedor ligue

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
                    this.Hide();

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
