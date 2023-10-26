using Agregados.Forms.Loading;
using Agregados.Reports;
using Agregados.Reports.Facts.FactNow;
using NPOI.SS.Formula.Functions;
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

namespace Agregados.Forms.Bills
{
    public partial class FrmNotaDebito : Form
    {
        //variables del form
        AgregadosEntities DB;
        Facturas factura;
        Facturas facturatemp; //max consecutivo
        Facturas notaDebito;
        DetalleFacts detalleFacts;
        DetalleFacts detalleFactsP;
        Materiales materiales;
        CierreApertCajas cierreApertCajas;
        CierreApertCajas apertura;
        int Consecutivo;
        int Id;
        decimal Total;

        public FrmNotaDebito()
        {
            InitializeComponent();
            DB = new AgregadosEntities();
            factura = new Facturas();
            facturatemp = new Facturas();//max consecutivo
            notaDebito = new Facturas();
            detalleFacts = new DetalleFacts();
            detalleFactsP = new DetalleFacts();
            materiales = new Materiales();
            cierreApertCajas = new CierreApertCajas();
            apertura = new CierreApertCajas();
            Consecutivo = 0;
            btnEmitirNota.Visible = false;
            Total = 0;

            Id = 0;
        }

        private void FrmNotaDebito_Load(object sender, EventArgs e)
        {

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

        private void btnVolver_Click(object sender, EventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
        }

        private void Filtrar()
        {
            btnEmitirNota.Visible = false;
            //DateTime fechaActual = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            //int apert = apertura.IdCierreApert;

            if (txtConsecutivo.Text.Length > 0)
            {
                int consec = Convert.ToInt32(txtConsecutivo.Text.Trim());

                // linq para validar y disenar mejor la DataGridView al usuario
                var result = from fa in DB.Facturas
                             join es in DB.Estados on fa.IdEstado equals es.IdEstado
                             join cl in DB.Clientes on fa.IdCliente equals cl.IdCliente
                             join us in DB.Usuarios on fa.IdUsuario equals us.IdUsuario
                             where ((fa.IdEstado == 4 ) && fa.IdCliente > 0 && fa.Consecutivo == consec)
                             select new
                             {
                                 fa.IdFactura,
                                 fa.Consecutivo,
                                 fa.FechaFactura,
                                 fa.CostoTotal,
                                 es.NombreEstado,
                                 cl.Nombre,
                                 us.NombreEmpleado
                             };
                dgvFilter.DataSource = result.ToList();
                if (result.ToList().Count > 0)
                {
                    btnEmitirNota.Visible = true;
                }
                else
                {

                    btnEmitirNota.Visible = false;
                    MessageBox.Show("No hay Facturas disponibles por Emitir Nota débito.",
                                                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (txtClienteNombre.Text.Length > 0)
                {
                    string cliente = txtClienteNombre.Text.Trim();
                    var result = from fa in DB.Facturas
                                 join es in DB.Estados on fa.IdEstado equals es.IdEstado
                                 join cl in DB.Clientes on fa.IdCliente equals cl.IdCliente
                                 join us in DB.Usuarios on fa.IdUsuario equals us.IdUsuario
                                 where ((fa.IdEstado == 4) && fa.IdCliente > 0 && cl.Nombre.Contains(cliente))
                                 select new
                                 {
                                     fa.IdFactura,
                                     fa.Consecutivo,
                                     fa.FechaFactura,
                                     fa.CostoTotal,
                                     es.NombreEstado,
                                     cl.Nombre,
                                     us.NombreEmpleado
                                 };
                    dgvFilter.DataSource = result.ToList();
                    if (result.ToList().Count > 0)
                    {
                        btnEmitirNota.Visible = true;
                    }
                    else
                    {

                        btnEmitirNota.Visible = false;
                        MessageBox.Show("No hay Facturas disponibles por Emitir Nota débito.",
                                                      "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //string cliente = txtClienteNombre.Text.Trim();
                    var result = from fa in DB.Facturas
                                 join es in DB.Estados on fa.IdEstado equals es.IdEstado
                                 join cl in DB.Clientes on fa.IdCliente equals cl.IdCliente
                                 join us in DB.Usuarios on fa.IdUsuario equals us.IdUsuario
                                 where ((fa.IdEstado == 4) && fa.IdCliente > 0)
                                 select new
                                 {
                                     fa.IdFactura,
                                     fa.Consecutivo,
                                     fa.FechaFactura,
                                     fa.CostoTotal,
                                     es.NombreEstado,
                                     cl.Nombre,
                                     us.NombreEmpleado
                                 };
                    dgvFilter.DataSource = result.ToList();
                    if (result.ToList().Count > 0)
                    {
                        btnEmitirNota.Visible = true;
                    }
                    else
                    {

                        btnEmitirNota.Visible = false;
                        MessageBox.Show("No hay Facturas disponibles por emitir nota débito.",
                                                      "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            Filtrar();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Este apartado permite realizar una nota de débito de una factura que se haya emitido anteriormente," +
               " y se requiere cobrar demás al cliente por algun error que se haya cometido al momento de facturar,",
                                             "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void txtConsecutivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }

        private void txtConsecutivo_TextChanged(object sender, EventArgs e)
        {
            if (txtConsecutivo.Text.Length > 0)
            {
                txtClienteNombre.Text = null;
                txtClienteNombre.Enabled = false;
                Consecutivo = 0;
                Id = 0;
            }
            else
            {
                if (txtConsecutivo.Text.Length == 0)
                {
                    txtClienteNombre.Text = null;
                    txtClienteNombre.Enabled = true;
                    Consecutivo = 0;
                    Id = 0;
                }
            }
        }

        private void txtClienteNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtClienteNombre.Text.Length > 0)
            {
                txtConsecutivo.Text = null;
                txtConsecutivo.Enabled = false;
                Consecutivo = 0;
                Id = 0;
            }
            else
            {
                if (txtClienteNombre.Text.Length == 0)
                {
                    txtConsecutivo.Text = null;
                    txtConsecutivo.Enabled = true;
                    Consecutivo = 0;
                    Id = 0;
                }
            }
        }

        private void txtClienteNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, true, false);
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
                    btnEmitirNota.Visible = true;

                    var result = from fa in DB.Facturas
                                 where ((fa.IdEstado == 4) && fa.IdCliente > 0 && fa.IdFactura == Id)
                                 select new
                                 {                            
                                     fa.CostoTotal 
                                 };
                    var result2 = from fa in DB.Facturas
                                 where ((fa.IdEstado == 14) && fa.IdCliente > 0 && fa.NotaDebitoIDFact == Consecutivo)
                                 select new
                                 {
                                     fa.CostoTotal
                                 };

                    decimal resultOriginal = 0;
                    if (result.ToList().Count > 0)
                    {
                        foreach (var item in result.ToList())
                        {
                            resultOriginal += item.CostoTotal;
                        }
                        lblMontoOriginal.Text = string.Format("¢ {0:N2}", resultOriginal);
                    }
                    else
                    {
                        lblMontoOriginal.Text = string.Format("¢ {0:N2}", 0);
                    }

                    decimal resultAdicional = 0;
                    if (result2.ToList().Count > 0)
                    {
                        foreach (var item in result2.ToList())
                        {
                            resultAdicional += item.CostoTotal;
                        }
                        lblMontoAdicional.Text = string.Format("¢ {0:N2}", resultAdicional);
                    }
                    else
                    {
                        resultAdicional = 0;
                        lblMontoAdicional.Text = string.Format("¢ {0:N2}", resultAdicional);
                    }                  
                }
                txtReferenciaPago.Text = "";
            }
            else
            {
                btnEmitirNota.Visible = false;
                Consecutivo = 0;
                Id = 0;
                lblMontoOriginal.Text = 0.ToString();
                lblMontoAdicional.Text = 0.ToString();
            }
        }

        private void FrmNotaDebito_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
        }

        //busca el id maximo de factura generado para 
        private int MaxIdConsecutivo()
        {
            int result = 0;
            try
            {
                facturatemp = DB.Facturas.Where((x) => x.IdCliente > 0).FirstOrDefault();
                if (facturatemp != null)
                {
                    result = DB.Facturas.Where((x) => x.IdCliente > 0).Select((X) => X.Consecutivo).Max();
                }
                facturatemp = null;
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void limpiar()
        {
            txtClienteNombre.Text = null;
            txtConsecutivo.Text = null;
            txtTotal.Value = 0;
            rbEfectivo2.Checked = false;
            rbSinpe2.Checked = false;
            rbSinpeMovil2.Checked = false;
            Filtrar();
        }
        //tiempo loading
        void Wait()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(5);
            }
        }

        private Facturas buscarFact(int id)
        {
            Facturas fact = new Facturas();
            fact = DB.Facturas.Find(Id);
            return fact;
        }

        private void btnEmitirNota_Click(object sender, EventArgs e)
        {
            if (Consecutivo > 0)
            {
                factura = buscarFact(Id);

                apertura = BuscarAperturaActual();

                /*
                if (factura.IdCierreApert == apertura.IdCierreApert)
                {
                    
                }
                */
                if (factura.IdEstado == 4)
                {
                    Total = Convert.ToDecimal(txtTotal.Value);
                    var listDetalles = from de in DB.DetalleFacts
                                        where (de.IdFactura == Id)
                                        select new
                                        {
                                            de.IdDetalle,
                                            de.Cantidad,
                                            de.Precio,
                                            de.Subtotal,
                                            de.IVA,
                                            de.Total,
                                            de.IdFactura,
                                            de.IdMaterial
                                        };

                    int metodo = (rbEfectivo2.Checked == true) ? 1 : (rbSinpe2.Checked == true) ? 2 : (rbSinpeMovil2.Checked == true) ? 3 : 0;

                    if (metodo != 0)
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
                        if (factura.IdTipoPago == 1 || factura.IdTipoPago == 2 || factura.IdTipoPago == 3) // tipo de factura 
                        {

                            DialogResult respuesta = MessageBox.Show("¿Deseas generar la nota de débito de la factura de contado #" + $"{factura.Consecutivo} ?",
                                                "Nota DéBito", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (respuesta == DialogResult.Yes)
                            {
                                decimal iva = Convert.ToDecimal(Convert.ToDouble(txtTotal.Value) * Convert.ToDouble(0.13));
                                decimal subtotal = Convert.ToDecimal(Convert.ToDouble(txtTotal.Value) - Convert.ToDouble(iva));

                                using (FrmLoading frmLoading = new FrmLoading(Wait))
                                {
                                    try
                                    {

                                        notaDebito = new Facturas
                                        {
                                            Consecutivo = consecutivo,
                                            CostoTransporte = Convert.ToDecimal(0),
                                            Subtotal = subtotal,
                                            IVA = iva,
                                            CostoTotal = Convert.ToDecimal(Convert.ToDouble(txtTotal.Value)),

                                            FechaFactura = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()),
                                            MontoPendiente = null,
                                            FechaLimiteP = null,
                                            ReferenciaPago = $"*Nota Dédito de la factura #{factura.Consecutivo}** " + txtReferenciaPago.Text.Trim(),
                                            BackHoe = null,
                                            Tierra = null,
                                            CantTierra = null,

                                            IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                            IdTipo = factura.IdTipo,
                                            IdEstado = 14,
                                            IdCliente = factura.IdCliente,
                                            IdProveedor = null,
                                            IdTipoPago = metodo,
                                            IdCierreApert = apertura.IdCierreApert,
                                            NotaDebitoIDFact = factura.Consecutivo
                                        };

                                        DB.Facturas.Add(notaDebito);
                                        //factura.IdEstado = 4;
                                        //DB.Entry(factura).State = EntityState.Modified;

                                        if (DB.SaveChanges() > 0)
                                        {

                                            //int IdFact = DB.Facturas.Where((x) => x.IdCliente == factura.IdCliente).Select((x) => x.IdFactura).Max();

                                            /*
                                            if (listDetalles.ToList().Count > 0)
                                            {
                                                foreach (var listDetalle in listDetalles.ToList())
                                                {
                                                    detalleFactsP = new DetalleFacts
                                                    {
                                                        Cantidad = listDetalle.Cantidad,
                                                        Precio = listDetalle.Precio,
                                                        Subtotal = listDetalle.Subtotal,
                                                        IVA = listDetalle.IVA,
                                                        Total = listDetalle.Total,
                                                        IdFactura = IdFact,
                                                        IdMaterial = listDetalle.IdMaterial
                                                    };
                                                    int IdMaterial = listDetalle.IdMaterial;
                                                    decimal Cant = listDetalle.Cantidad;
                                                    DB.DetalleFacts.Add(detalleFactsP);

                                                    if (DB.SaveChanges() > 0)
                                                    {
                                                        materiales = DB.Materiales.Find(IdMaterial);
                                                        materiales.CantidadMaterial = materiales.CantidadMaterial + Cant;
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
                                                            detalleFactsP = null;
                                                        }
                                                    }
                                                }
                                            }
                                            */

                                            switch (metodo)
                                            {
                                                case 1: //efectivo
                                                    apertura.MontoEfectivoFinal += Total;
                                                    apertura.MontoVentaEfectivo += Total;
                                                    DB.Entry(apertura).State = EntityState.Modified;
                                                    if (DB.SaveChanges() <= 0)
                                                    {
                                                        MessageBox.Show("Error inesperado, no se pudo actualizar la información de caja", "Error Sistema Caja",
                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    }

                                                    MessageBox.Show("Nota de Débito correctamente!", "Registro de Nota de Débito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                    using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                    {
                                                        Cursor.Current = Cursors.WaitCursor;
                                                        frm.ShowDialog();
                                                        Cursor.Current = Cursors.Default;
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

                                                    MessageBox.Show("Nota de Débito correctamente!", "Registro de Nota de Débito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                    using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                    {
                                                        Cursor.Current = Cursors.WaitCursor;
                                                        frm.ShowDialog();
                                                        Cursor.Current = Cursors.Default;
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

                                                    MessageBox.Show("Nota de Débito correctamente!", "Registro de Nota de Débito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                    using (FrmPrintFact frm = new FrmPrintFact(consecutivo))
                                                    {
                                                        Cursor.Current = Cursors.WaitCursor;
                                                        frm.ShowDialog();
                                                        Cursor.Current = Cursors.Default;
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
                                    catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException ex)
                                    {
                                        MessageBox.Show("Concurrency Exception Occurred." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        throw;

                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error, se debe seleccionar un método de pago para emitir la nota Débito",
                            "Error Sistema Caja", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (factura.IdEstado == 3)
                    {
                        MessageBox.Show("Error, factura fue generada a crédito, " +
                            "en ese caso debes reversar la factura ya que no se ha recibido ningun monto de pago por parte del cliente", "Error Sistema Caja",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
        }

        private void txtTotal_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
