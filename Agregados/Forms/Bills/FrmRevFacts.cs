using Agregados.Reports;
using Agregados.Reports.Facts.FactNow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Forms.Bills
{
    public partial class FrmRevFacts : Form
    {
        //variables del form
        AgregadosEntities DB;
        Facturas facturas;
        DetalleFacts detalleFacts;
        Materiales materiales;
        CierreApertCajas cierreApertCajas;

        CierreApertCajas apertura;
        int Consecutivo;
        int Id;

        public FrmRevFacts()
        {
            InitializeComponent();
            DB = new AgregadosEntities();
            facturas = new Facturas();
            detalleFacts = new DetalleFacts();
            materiales = new Materiales();
            cierreApertCajas = new CierreApertCajas();
            apertura = new CierreApertCajas();
            Consecutivo = 0;
            btnReversar.Visible = false;
       
            Id = 0;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
        }

        private void FrmRevFacts_Load(object sender, EventArgs e)
        {

            apertura = BuscarAperturaActual();
            MessageBox.Show("Este apartado permite anular una factura que se haya emitido anteriormente, en el mismo día y apertura de caja actual que se haya emitido con la factura." + Environment.NewLine
                + "No es posible anular o reversar una factura de días anteriores al actual o cuando se haya hecho cierre de caja en el día."
                 + Environment.NewLine + "Si se requiere anular, se debe realizar una nota de crédito, pero si la factura es a crédito, pendiente de pago se procede a reversar por este medio..",
                                              "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void FrmRevFacts_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
        }

        private void Filtrar() // filtra la informacion
        {
            btnReversar.Visible = false;
            DateTime fechaActual = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            int apert = apertura.IdCierreApert;

            if (txtConsecutivo.Text.Length > 0)
            {
                int consec = Convert.ToInt32(txtConsecutivo.Text.Trim());

                // linq para validar y disenar mejor la DataGridView al usuario
                var result = from fa in DB.Facturas
                             join es in DB.Estados on fa.IdEstado equals es.IdEstado
                             join cl in DB.Clientes on fa.IdCliente equals cl.IdCliente
                             join us in DB.Usuarios on fa.IdUsuario equals us.IdUsuario
                             where ( (fa.IdEstado == 4 || fa.IdEstado == 3) && fa.IdCliente > 0 && fa.FechaFactura == fechaActual && fa.Consecutivo == consec && fa.IdCierreApert == apert)
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
                    btnReversar.Visible = true;
                }
                else
                {

                    btnReversar.Visible = false;
                    MessageBox.Show("No hay Facturas disponibles por Reversar.",
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
                                 where ((fa.IdEstado == 4 || fa.IdEstado == 3) && fa.IdCliente > 0 && fa.FechaFactura == fechaActual && cl.Nombre.Contains(cliente) && fa.IdCierreApert == apert)
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
                        btnReversar.Visible = true;
                    }
                    else
                    {

                        btnReversar.Visible = false;
                        MessageBox.Show("No hay Facturas disponibles por Reversar.",
                                                      "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    string cliente = txtClienteNombre.Text.Trim();
                    var result = from fa in DB.Facturas
                                 join es in DB.Estados on fa.IdEstado equals es.IdEstado
                                 join cl in DB.Clientes on fa.IdCliente equals cl.IdCliente
                                 join us in DB.Usuarios on fa.IdUsuario equals us.IdUsuario
                                 where ((fa.IdEstado == 4 || fa.IdEstado == 3) && fa.IdCliente > 0 && fa.FechaFactura == fechaActual && fa.IdCierreApert == apert)
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
                        btnReversar.Visible = true;
                    }
                    else
                    {

                        btnReversar.Visible = false;
                        MessageBox.Show("No hay Facturas disponibles por Reversar.",
                                                      "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            Filtrar();
        }

        private void btnReversar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Consecutivo > 0)
                {
                    facturas = DB.Facturas.Find(Id);

                    if (facturas.IdCierreApert == apertura.IdCierreApert)
                    {
                        DialogResult respuesta = MessageBox.Show("¿Deseas continuar con la reversión de la factura seleccionada?",
                                              "Reversión Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {

                            var listDetalles = from de in DB.DetalleFacts
                                               where (de.IdFactura == Id)
                                               select new
                                               {
                                                   de.Cantidad,
                                                   de.IdMaterial
                                               };

                            if (facturas.IdTipo == 1) // contado
                            {
                                if (facturas.IdTipoPago == 1) //efectivo
                                {
                                    facturas.IdEstado = 5;
                                    DB.Entry(facturas).State = EntityState.Modified;

                                    if (listDetalles.ToList().Count > 0)
                                    {
                                        //reversamos cantidad de materiales
                                        foreach (var listDetalle in listDetalles.ToList())
                                        {
                                            int IdMaterial = listDetalle.IdMaterial;
                                            materiales = DB.Materiales.Find(IdMaterial);
                                            materiales.CantidadMaterial += listDetalle.Cantidad;

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
                                        }
                                    }
                                    //reversamos datos financieros del cierre de caja
                                    cierreApertCajas = DB.CierreApertCajas.Find(facturas.IdCierreApert);
                                    cierreApertCajas.MontoVentaEfectivo -= facturas.CostoTotal;
                                    cierreApertCajas.MontoEfectivoFinal -= facturas.CostoTotal;
                                    DB.Entry(cierreApertCajas).State = EntityState.Modified;

                                    //validamos que se haya aplicado las modificaciones financieras Y DEMAS 
                                    if (DB.SaveChanges() > 0)
                                    {
                                        MessageBox.Show("Factura fue reversada correctamente!",
                                                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        using (FrmPrintFactRev frm = new FrmPrintFactRev(facturas.Consecutivo))
                                        {
                                            Cursor.Current = Cursors.WaitCursor;
                                            frm.ShowDialog();
                                            Cursor.Current = Cursors.Default;
                                        };
                                        cierreApertCajas = null;
                                        materiales = null;
                                        facturas = null;
                                        limpiar();
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se pudo reversar factura, correctamente, favor validar",
                                                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    if (facturas.IdTipoPago == 2) //trans sinpe
                                    {
                                        facturas.IdEstado = 5;
                                        DB.Entry(facturas).State = EntityState.Modified;

                                        if (listDetalles.ToList().Count > 0)
                                        {
                                            //reversamos cantidad de materiales
                                            foreach (var listDetalle in listDetalles.ToList())
                                            {
                                                int IdMaterial = listDetalle.IdMaterial;
                                                materiales = DB.Materiales.Find(IdMaterial);
                                                materiales.CantidadMaterial += listDetalle.Cantidad;

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
                                            }
                                        }
                                        
                                        //reversamos datos financieros del cierre de caja
                                        cierreApertCajas = DB.CierreApertCajas.Find(facturas.IdCierreApert);
                                        cierreApertCajas.MontoTransf -= facturas.CostoTotal;
                                        DB.Entry(cierreApertCajas).State = EntityState.Modified;

                                        //validamos que se haya aplicado las modificaciones financieras Y DEMAS 
                                        if (DB.SaveChanges() > 0)
                                        {
                                            MessageBox.Show("Factura fue reversada correctamente!",
                                                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            using (FrmPrintFactRev frm = new FrmPrintFactRev(facturas.Consecutivo))
                                            {
                                                Cursor.Current = Cursors.WaitCursor;
                                                frm.ShowDialog();
                                                Cursor.Current = Cursors.Default;
                                            };
                                            cierreApertCajas = null;
                                            materiales = null;
                                            facturas = null;
                                            limpiar();
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se pudo reversar factura, correctamente, favor validar",
                                                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                    {
                                        if (facturas.IdTipoPago == 3) // sinpe movil
                                        {
                                            facturas.IdEstado = 5;
                                            DB.Entry(facturas).State = EntityState.Modified;

                                            if (listDetalles.ToList().Count > 0)
                                            {
                                                //reversamos cantidad de materiales
                                                foreach (var listDetalle in listDetalles.ToList())
                                                {
                                                    int IdMaterial = listDetalle.IdMaterial;
                                                    materiales = DB.Materiales.Find(IdMaterial);
                                                    materiales.CantidadMaterial += listDetalle.Cantidad;

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
                                                }
                                            }

                                            //reversamos datos financieros del cierre de caja
                                            cierreApertCajas = DB.CierreApertCajas.Find(facturas.IdCierreApert);
                                            cierreApertCajas.MontoSinpe -= facturas.CostoTotal;
                                            DB.Entry(cierreApertCajas).State = EntityState.Modified;

                                            //validamos que se haya aplicado las modificaciones financieras Y DEMAS 
                                            if (DB.SaveChanges() > 0)
                                            {
                                                MessageBox.Show("Factura fue reversada correctamente!",
                                                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                using (FrmPrintFactRev frm = new FrmPrintFactRev(facturas.Consecutivo))
                                                {
                                                    Cursor.Current = Cursors.WaitCursor;
                                                    frm.ShowDialog();
                                                    Cursor.Current = Cursors.Default;
                                                };
                                                cierreApertCajas = null;
                                                materiales = null;
                                                facturas = null;
                                                limpiar();
                                            }
                                            else
                                            {
                                                MessageBox.Show("No se pudo reversar factura, correctamente, favor validar",
                                                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                        else
                                        {
                                            if (facturas.IdTipoPago == 4) // cheque
                                            {
                                                facturas.IdEstado = 5;
                                                DB.Entry(facturas).State = EntityState.Modified;

                                                if (listDetalles.ToList().Count > 0)
                                                {
                                                    //reversamos cantidad de materiales
                                                    foreach (var listDetalle in listDetalles.ToList())
                                                    {
                                                        int IdMaterial = listDetalle.IdMaterial;
                                                        materiales = DB.Materiales.Find(IdMaterial);
                                                        materiales.CantidadMaterial += listDetalle.Cantidad;

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
                                                    }
                                                }

                                                //reversamos datos financieros del cierre de caja
                                                cierreApertCajas = DB.CierreApertCajas.Find(facturas.IdCierreApert);
                                                cierreApertCajas.MontoCheque -= facturas.CostoTotal;
                                                DB.Entry(cierreApertCajas).State = EntityState.Modified;

                                                //validamos que se haya aplicado las modificaciones financieras Y DEMAS 
                                                if (DB.SaveChanges() > 0)
                                                {
                                                    MessageBox.Show("Factura fue reversada correctamente!",
                                                                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    using (FrmPrintFactRev frm = new FrmPrintFactRev(facturas.Consecutivo))
                                                    {
                                                        Cursor.Current = Cursors.WaitCursor;
                                                        frm.ShowDialog();
                                                        Cursor.Current = Cursors.Default;
                                                    };
                                                    cierreApertCajas = null;
                                                    materiales = null;
                                                    facturas = null;
                                                    limpiar();
                                                }
                                                else
                                                {
                                                    MessageBox.Show("No se pudo reversar factura, correctamente, favor validar",
                                                                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                            }
                                            else
                                            {
                                                if (facturas.IdTipoPago == 6) // Mixto
                                                {
                                                    MessageBox.Show("No se pudo reversar factura, porque fue emitida de forma de pago mixto, se debe realizar una nota de crédito," +
                                                        " y expecificar los montos correspondientes.",
                                                                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (facturas.IdTipo == 2) // credito
                                {
                                    facturas.IdEstado = 5;
                                    DB.Entry(facturas).State = EntityState.Modified;

                                    if (listDetalles.ToList().Count > 0)
                                    {
                                        //reversamos cantidad de materiales
                                        foreach (var listDetalle in listDetalles.ToList())
                                        {
                                            int IdMaterial = listDetalle.IdMaterial;
                                            materiales = DB.Materiales.Find(IdMaterial);
                                            materiales.CantidadMaterial += listDetalle.Cantidad;

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
                                        }
                                    }


                                    //reversamos datos financieros del cierre de caja
                                    cierreApertCajas = DB.CierreApertCajas.Find(facturas.IdCierreApert);
                                    cierreApertCajas.MontoCredito -= facturas.CostoTotal;
                                    DB.Entry(cierreApertCajas).State = EntityState.Modified;

                                    //validamos que se haya aplicado las modificaciones financieras Y DEMAS 
                                    if (DB.SaveChanges() > 0)
                                    {
                                        MessageBox.Show("Factura fue reversada correctamente!",
                                                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        using (FrmPrintFactRev frm = new FrmPrintFactRev(facturas.Consecutivo))
                                        {
                                            Cursor.Current = Cursors.WaitCursor;
                                            frm.ShowDialog();
                                            Cursor.Current = Cursors.Default;
                                        };

                                        cierreApertCajas = null;
                                        materiales = null;
                                        facturas = null;
                                        limpiar();
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se pudo reversar factura, correctamente, favor validar",
                                                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Factura no esta disponible para Reversa ya que apertura caja no corresponde a dicha factura, favor validar detalle de la factura.",
                                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Factura no fue seleccionada correctamente, por favor valida la información en la tabla.",
                                                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Este apartado permite anular una factura que se haya emitido anteriormente, en el mismo día y apertura de caja actual que se haya emitido con la factura." + Environment.NewLine
                + "No es posible anular o reversar una factura de días anteriores al actual o cuando se haya hecho cierre de caja en el día."
                 + Environment.NewLine + "Si se requiere anular, se debe realizar una nota de crédito, pero si la factura es a crédito, pendiente de pago se procede a reversar por este medio..",
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

        private void txtClienteNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, true, false);
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

        private void dgvFilter_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFilter.SelectedRows.Count == 1)
            {
                DataGridViewRow MiFila = dgvFilter.SelectedRows[0];

                Consecutivo = Convert.ToInt32(MiFila.Cells["CConsecutivo"].Value);
                Id = Convert.ToInt32(MiFila.Cells["CID"].Value);

                if (Consecutivo > 0 && Id > 0)
                {
                    btnReversar.Visible = true;
                }

            }
            else
            {
                btnReversar.Visible = false;
                Consecutivo = 0;
                Id = 0;
            }
        }

        private void limpiar()
        {
            txtClienteNombre.Text = null;
            txtConsecutivo.Text = null;

            dgvFilter.ClearSelection();
            Filtrar();
        }
    }
}
