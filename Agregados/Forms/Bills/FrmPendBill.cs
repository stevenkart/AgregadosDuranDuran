using Agregados.Reports;
using Agregados.Reports.Facts.FactNow;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Forms.Bills
{
    public partial class FrmPendBill : Form
    {

        //variables del form
        AgregadosEntities DB;
        Facturas fact;
        CierreApertCajas cierreApertCajas;
        CierreApertCajas apertura;
        int Consecutivo;
        int Id;
        int metodo;


        public FrmPendBill()
        {
            InitializeComponent();
            DB = new AgregadosEntities();
            fact = new Facturas();
            cierreApertCajas = new CierreApertCajas();
            apertura = new CierreApertCajas();

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

        private void FrmPendBill_Load(object sender, EventArgs e)
        {
            apertura = BuscarAperturaActual();
            btnPagar.Visible = false;
            metodo = 0;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();

        }

        private void FrmPendBill_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
        }

        private void btnFiltrarHoySoloBackHoe_Click(object sender, EventArgs e)
        {
            dgvFilter.ClearSelection();
            Consecutivo = 0;
            btnPagar.Visible = false;
            // linq para validar y disenar mejor la DataGridView al usuario
            //se llama el procedimiento Almacenado
            var result = DB.SPFactPendSinDetalle().ToList();

            var finalResult = from fa in result
                              select new
                              {
                                  fa.IdFactura,
                                  fa.Consecutivo,
                                  fa.FechaFactura,
                                  fa.CostoTotal,
                                  fa.NombreEstado,
                                  fa.Nombre,
                                  fa.NombreEmpleado,
                                  fa.FechaLimiteP
                              };

            finalResult = finalResult.Distinct();

            dgvFilter.DataSource = finalResult.ToList();
            if (finalResult.ToList().Count > 0)
            {
                btnPagar.Visible = true;
                btnPagar.Enabled = false;
                
            }
            else
            {
                btnPagar.Visible = false;
                btnPagar.Enabled = false;
                MessageBox.Show("No hay datos que mostrar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnFiltrarHoyTodas_Click(object sender, EventArgs e)
        {
            dgvFilter.ClearSelection();
            Consecutivo = 0;
            btnPagar.Visible = false;
            // linq para validar y disenar mejor la DataGridView al usuario
            //se llama el procedimiento Almacenado
            var result = DB.SPFactPendDetalles().ToList();

            var finalResult = from fa in result
                              select new
                              {
                                  fa.IdFactura,
                                  fa.Consecutivo,
                                  fa.FechaFactura,
                                  fa.CostoTotal,
                                  fa.NombreEstado,
                                  fa.Nombre,
                                  fa.NombreEmpleado,
                                  fa.FechaLimiteP
                              };

            finalResult = finalResult.Distinct();

            dgvFilter.DataSource = finalResult.ToList();
            if (finalResult.ToList().Count > 0)
            {
                btnPagar.Visible = true;
                btnPagar.Enabled = false;
                
            }
            else
            {
                btnPagar.Visible = false;
                btnPagar.Enabled = false;
                MessageBox.Show("No hay datos que mostrar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void filtrarTodos()
        {
            dgvFilter.ClearSelection();
            Consecutivo = 0;
            btnPagar.Visible = false;
            // linq para validar y disenar mejor la DataGridView al usuario
            //se llama el procedimiento Almacenado
            var result = DB.SPFactPendAll().ToList();

            var finalResult = from fa in result
                              select new
                              {
                                  fa.IdFactura,
                                  fa.Consecutivo,
                                  fa.FechaFactura,
                                  fa.CostoTotal,
                                  fa.NombreEstado,
                                  fa.Nombre,
                                  fa.NombreEmpleado,
                                  fa.FechaLimiteP
                              };

            finalResult = finalResult.Distinct();

            dgvFilter.DataSource = finalResult.ToList();
            if (finalResult.ToList().Count > 0)
            {
                btnPagar.Visible = true;
                btnPagar.Enabled = false;
  
            }
            else
            {
                btnPagar.Visible = false;
                btnPagar.Enabled = false;
                MessageBox.Show("No hay datos que mostrar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e) // filtra todas las facturas
        {
            filtrarTodos();
        }

        private void txtConsecutivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }

        private void txtConsecutivo_TextChanged(object sender, EventArgs e)
        {
            if (txtConsecutivo.Text.Length > 0)
            {
                btnFiltrar.Enabled = false;
                btnFiltrarHoyTodas.Enabled = false; 
                btnFiltrarHoySoloBackHoe.Enabled = false;
                txtClienteNombre.Enabled = false;

                dgvFilter.ClearSelection();
                Consecutivo = 0;
                btnPagar.Visible = false;
                // linq para validar y disenar mejor la DataGridView al usuario
                //se llama el procedimiento Almacenado
                var result = DB.SPFactPendAll().ToList();

                var finalResult = from fa in result
                                  select new
                                  {
                                      fa.IdFactura,
                                      fa.Consecutivo,
                                      fa.FechaFactura,
                                      fa.CostoTotal,
                                      fa.NombreEstado,
                                      fa.Nombre,
                                      fa.NombreEmpleado,
                                      fa.FechaLimiteP
                                  };
                int busqueda = Convert.ToInt32(txtConsecutivo.Text.Trim());
                finalResult = finalResult.Distinct().Where((x) => x.Consecutivo == busqueda); //filtra que solo aparezca un valor y no repetidos

                dgvFilter.DataSource = finalResult.ToList();
                if (finalResult.ToList().Count > 0)
                {
                    btnPagar.Visible = true;
                    btnPagar.Enabled = false;
                }
                else
                {
                    btnPagar.Visible = false;
                    btnPagar.Enabled = false;
                }
            }
            else
            {
                btnFiltrar.Enabled = true;
                btnFiltrarHoyTodas.Enabled = true;
                btnFiltrarHoySoloBackHoe.Enabled = true;
                txtClienteNombre.Enabled = true;
            }
        }

        private void txtClienteNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = Validaciones.CaracteresTexto(e, false, true);
        }

        private void txtClienteNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtClienteNombre.Text.Length > 0)
            {
                btnFiltrar.Enabled = false;
                btnFiltrarHoyTodas.Enabled = false;
                btnFiltrarHoySoloBackHoe.Enabled = false;
                txtConsecutivo.Enabled = false;

                dgvFilter.ClearSelection();
                Consecutivo = 0;
                btnPagar.Visible = false;
                // linq para validar y disenar mejor la DataGridView al usuario
                //se llama el procedimiento Almacenado
                var result = DB.SPFactPendAll().ToList();

                var finalResult = from fa in result
                                  select new
                                  {
                                      fa.IdFactura,
                                      fa.Consecutivo,
                                      fa.FechaFactura,
                                      fa.CostoTotal,
                                      fa.NombreEstado,
                                      fa.Nombre,
                                      fa.NombreEmpleado,
                                      fa.FechaLimiteP
                                  };
                string busqueda = txtClienteNombre.Text.Trim();
                finalResult = finalResult.Distinct().Where((x) => x.Nombre.Contains(busqueda)); //filtra que solo aparezca un valor y no repetidos

                dgvFilter.DataSource = finalResult.ToList();
                if (finalResult.ToList().Count > 0)
                {
                    btnPagar.Visible = true;
                    btnPagar.Enabled = false;
                }
                else
                {
                    btnPagar.Visible = false;
                    btnPagar.Enabled = false;
                }
            }
            else
            {
                btnFiltrar.Enabled = true;
                btnFiltrarHoyTodas.Enabled = true;
                btnFiltrarHoySoloBackHoe.Enabled = true;
                txtConsecutivo.Enabled = true;
            }
        }

        private void dgvFilter_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFilter.SelectedRows.Count == 1)
            {
                DataGridViewRow MiFila = dgvFilter.SelectedRows[0];

                Consecutivo = Convert.ToInt32(MiFila.Cells["CConsecutivo"].Value);
                Id = Convert.ToInt32(MiFila.Cells["CID"].Value);

                if (Consecutivo > 0 && Id > 0)
                {
                    btnPagar.Visible = true;
                    btnPagar.Enabled = true;

                    txtFecha.Text = Convert.ToString(DateTime.Now.Date.ToString("yyyy-MM-dd"));

                    fact = DB.Facturas.Find(Id);

                    txtDescription.Text = fact.ReferenciaPago;

                }
                else
                {
                    btnPagar.Enabled = false;
                    txtFecha.Text = null;
                    txtDescription.Text = null;

                    Consecutivo = 0;
                    Id = 0;
                    fact = null;
                }
            }
            else
            {
                Consecutivo = 0;
                Id = 0;
                fact = null;
            }
        }

        private void dgvFilter_DataSourceChanged(object sender, EventArgs e)
        {
            btnPagar.Enabled = false;
            txtFecha.Text = null;
            txtDescription.Text = null;

            Consecutivo = 0;
            Id = 0;

            fact = null;
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (Consecutivo > 0 && Id > 0)
            {
                if (rbEfectivo2.Checked == true || rbSinpe2.Checked == true || rbSinpeMovil2.Checked == true)
                {
                    switch (metodo)
                    {
                        case 1: //efectivo

                            if (Convert.ToDecimal(txtTotal.Value) != fact.CostoTotal)
                            {
                                MessageBox.Show("Debes de indicar el monto total de la factura a pagar, deben ser igual para continuar.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                if (txtDescription.Text.Length <= 0)
                                {
                                    DialogResult respuesta = MessageBox.Show("Metodo de pago seleccionado es efectivo, espacio de descripción esta vació, " +
                                        "¿deseas continuar de igual manera?.",
                                        "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (respuesta == DialogResult.Yes)
                                    {
                                        fact.ReferenciaPago = "Se paga crédito el día: " + txtFecha.Text.Trim() + ". " + txtDescription.Text.Trim();
                                        fact.FechaLimiteP = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                                        fact.IdTipoPago = 1;
                                        fact.IdEstado = 4;
                                        DB.Entry(fact).State = EntityState.Modified;


                                        apertura.MontoEfectivoFinal += Convert.ToDecimal(txtTotal.Value);
                                        apertura.MontoVentaEfectivo += Convert.ToDecimal(txtTotal.Value);
                                        DB.Entry(apertura).State = EntityState.Modified;
                                        if (DB.SaveChanges() <= 0)
                                        {
                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja, ni procesar pago de factura", "Error Sistema Caja",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }

                                        MessageBox.Show("Factura pagada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        using (FrmPrintFact frm = new FrmPrintFact(Consecutivo))
                                        {
                                            frm.ShowDialog();
                                        };

                                        fact = null;
                                        limpiar();
                                        break;
                                    }
                                }
                                else
                                {
                                    DialogResult respuesta2 = MessageBox.Show("Metodo de pago seleccionado es efectivo" +
                                    "¿deseas continuar con el detalle indicado?.",
                                    "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (respuesta2 == DialogResult.Yes)
                                    {
                                        fact.ReferenciaPago = "Se paga crédito el día: " + txtFecha.Text.Trim() + ". " + txtDescription.Text.Trim();
                                        fact.FechaLimiteP = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                                        fact.IdTipoPago = 1;
                                        fact.IdEstado = 4;
                                        DB.Entry(fact).State = EntityState.Modified;


                                        apertura.MontoEfectivoFinal += Convert.ToDecimal(txtTotal.Value);
                                        apertura.MontoVentaEfectivo += Convert.ToDecimal(txtTotal.Value);
                                        DB.Entry(apertura).State = EntityState.Modified;
                                        if (DB.SaveChanges() <= 0)
                                        {
                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja, ni procesar pago de factura", "Error Sistema Caja",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }

                                        MessageBox.Show("Factura pagada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        using (FrmPrintFact frm = new FrmPrintFact(Consecutivo))
                                        {
                                            frm.ShowDialog();
                                        };

                                        fact = null;
                                        limpiar();
                                        break;
                                    }
                                }
                            }
                            break;
                        case 2: //transferencia

                            if (Convert.ToDecimal(txtTotal.Value) != fact.CostoTotal)
                            {
                                MessageBox.Show("Debes de indicar el monto total de la factura a pagar, deben ser igual para continuar.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                if (txtDescription.Text.Length <= 0)
                                {
                                    DialogResult respuesta = MessageBox.Show("Metodo de pago seleccionado es transferencia, espacio de descripción esta vació, " +
                                        "¿deseas continuar con el detalle indicado?, recuerda anotar referencia deposito.",
                                        "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (respuesta == DialogResult.Yes)
                                    {
                                        fact.ReferenciaPago = "Se paga crédito el día: " + txtFecha.Text.Trim() + ". " + txtDescription.Text.Trim();
                                        fact.FechaLimiteP = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                                        fact.IdTipoPago = 2;
                                        fact.IdEstado = 4;
                                        DB.Entry(fact).State = EntityState.Modified;


                                        apertura.MontoTransf += Convert.ToDecimal(txtTotal.Value);
                                        DB.Entry(apertura).State = EntityState.Modified;
                                        if (DB.SaveChanges() <= 0)
                                        {
                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja, ni procesar pago de factura", "Error Sistema Caja",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }

                                        MessageBox.Show("Factura pagada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        using (FrmPrintFact frm = new FrmPrintFact(Consecutivo))
                                        {
                                            frm.ShowDialog();
                                        };

                                        fact = null;
                                        limpiar();
                                        break;
                                    }
                                }
                                else
                                {
                                    DialogResult respuesta2 = MessageBox.Show("Metodo de pago seleccionado es transferencia " +
                                    "¿deseas continuar de igual manera, es posible que se necesite indicar referencia de deposito?.",
                                    "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta2 == DialogResult.Yes)
                                    {
                                        fact.ReferenciaPago = "Se paga crédito el día: " + txtFecha.Text.Trim() + ". " + txtDescription.Text.Trim();
                                        fact.FechaLimiteP = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                                        fact.IdTipoPago = 2;
                                        fact.IdEstado = 4;
                                        DB.Entry(fact).State = EntityState.Modified;


                                        apertura.MontoTransf += Convert.ToDecimal(txtTotal.Value);
                                        DB.Entry(apertura).State = EntityState.Modified;
                                        if (DB.SaveChanges() <= 0)
                                        {
                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja, ni procesar pago de factura", "Error Sistema Caja",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }

                                        MessageBox.Show("Factura pagada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        using (FrmPrintFact frm = new FrmPrintFact(Consecutivo))
                                        {
                                            frm.ShowDialog();
                                        };

                                        fact = null;
                                        limpiar();
                                        break;
                                    }
                                }
                            }
                            break;
                        case 3:
                            if (Convert.ToDecimal(txtTotal.Value) != fact.CostoTotal)
                            {
                                MessageBox.Show("Debes de indicar el monto total de la factura a pagar, deben ser igual para continuar.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                if (txtDescription.Text.Length <= 0)
                                {
                                    DialogResult respuesta = MessageBox.Show("Metodo de pago seleccionado es transferencia sinpe movil, espacio de descripción esta vació, " +
                                        "¿deseas continuar con el detalle indicado?, recuerda anotar referencia deposito.",
                                        "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (respuesta == DialogResult.Yes)
                                    {
                                        fact.ReferenciaPago = "Se paga crédito el día: " + txtFecha.Text.Trim() + ". " + txtDescription.Text.Trim();
                                        fact.FechaLimiteP = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                                        fact.IdTipoPago = 3;
                                        fact.IdEstado = 4;
                                        DB.Entry(fact).State = EntityState.Modified;


                                        apertura.MontoSinpe += Convert.ToDecimal(txtTotal.Value);
                                        DB.Entry(apertura).State = EntityState.Modified;
                                        if (DB.SaveChanges() <= 0)
                                        {
                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja, ni procesar pago de factura", "Error Sistema Caja",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }

                                        MessageBox.Show("Factura pagada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        using (FrmPrintFact frm = new FrmPrintFact(Consecutivo))
                                        {
                                            frm.ShowDialog();
                                        };

                                        fact = null;
                                        limpiar();
                                        break;
                                    }
                                }
                                else
                                {
                                    DialogResult respuesta2 = MessageBox.Show("Metodo de pago seleccionado es transferencia sinpe movil " +
                                    "¿deseas continuar de igual manera, es posible que se necesite indicar referencia de deposito?.",
                                    "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta2 == DialogResult.Yes)
                                    {
                                        fact.ReferenciaPago = "Se paga crédito el día: " + txtFecha.Text.Trim() + ". " + txtDescription.Text.Trim();
                                        fact.FechaLimiteP = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                                        fact.IdTipoPago = 2;
                                        fact.IdEstado = 4;
                                        DB.Entry(fact).State = EntityState.Modified;


                                        apertura.MontoSinpe += Convert.ToDecimal(txtTotal.Value);
                                        DB.Entry(apertura).State = EntityState.Modified;
                                        if (DB.SaveChanges() <= 0)
                                        {
                                            MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja, ni procesar pago de factura", "Error Sistema Caja",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }

                                        MessageBox.Show("Factura pagada correctamente!", "Registro de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        using (FrmPrintFact frm = new FrmPrintFact(Consecutivo))
                                        {
                                            frm.ShowDialog();
                                        };


                                        fact = null;
                                        limpiar();
                                        break;
                                    }
                                }
                            }
                            break;
                        default:
                            MessageBox.Show("Error metodo pago no seleccionado, no se pudo actualizar la información de caja, ni procesar pago de factura",
                                "Error Sistema Caja",MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Debes de seleccionar un metodo de pago para continuar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Debes de seleccionar la factura que se va a cancelar, para continuar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void limpiar()
        {
            txtConsecutivo.Text = null;
            txtClienteNombre.Text = null;
            btnPagar.Visible = false;
            metodo = 0;
            Id = 0;
            Consecutivo = 0;

            dgvFilter.ClearSelection();
            filtrarTodos();
        }

        private void rbEfectivo2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEfectivo2.Checked == true)
            {
                metodo = 1;
                rbSinpe2.Checked = false;
                rbSinpeMovil2.Checked = false;
            }
        }

        private void rbSinpe2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSinpe2.Checked == true)
            {
                metodo = 2;
                rbEfectivo2.Checked = false;
                rbSinpeMovil2.Checked = false;
            }
        }

        private void rbSinpeMovil2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSinpeMovil2.Checked == true)
            {
                metodo = 3;
                rbEfectivo2.Checked = false;
                rbSinpe2.Checked = false;
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Este apartado permite registrar las cancelaciones de lo cliente a las facturas que tienen pendientes por cancelar (crédito)," +
                " y que quede registro el día que se cancelan",
                "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
