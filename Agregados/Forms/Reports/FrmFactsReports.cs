using Agregados.Reports;
using Agregados.Reports.Facts.FactFiltered;
using Agregados.Reports.Facts.FactNow;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Ganss.Excel;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static NPOI.SS.Formula.PTG.ArrayPtg;

namespace Agregados.Forms.Reports
{
    public partial class FrmFactsReports : Form
    {
        //variables del form
        AgregadosEntities DB;
        Facturas facturas;
        int Consecutivo;
        int Id;


        int valorPendiente = 0; //acciones de credito

        int valorPorFechas = 0; //acciones de rango de fechas

        int valorHoy = 0; //acciones de rango de fechas


        public FrmFactsReports()
        {
            InitializeComponent();
            DB = new AgregadosEntities();
            facturas = new Facturas();
            valorPendiente = 0;
            valorPorFechas = 0;
            valorHoy = 0;

        }

        private void FrmFactsReports_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
        }



        //carga la info al form
        private void FrmFactsReports_Load(object sender, EventArgs e)
        {
            btnFiltrarHoy.Visible = false;
            btnFiltrarHoySoloBackHoe.Visible = false;
            btnFiltrarHoyTodas.Visible = false;

            btnFiltrarFechasCorrecta.Visible = false;
            btnFiltrarFechaBachHoe.Visible = false;
            btnFiltrarTodasVentas.Visible = false;
            DateInicio.Visible = false;
            DateFin.Visible = false;

            btnFiltrarCredito.Visible = false;
            btnCredSinLineas.Visible = false;
            btnCreditoTodas.Visible = false;


            btnAnuladas.Visible = false;
            DateInicio2.Visible = false;
            DateFin2.Visible = false;



            BtnVerFact.Visible = false;
            BtnVerFacturasList.Visible = false;
            btnReportExcel.Visible = false;
            btnReportPDF.Visible = false;



            DateTime fechaActual = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            DateTime fechaAnterior = fechaActual.AddDays(-3); //resta 3 dias a la fecha actual
      
            // linq para validar y disenar mejor la DataGridView al usuario
            var result = from fa in DB.Facturas
                         join es in DB.Estados on fa.IdEstado equals es.IdEstado
                         join cl in DB.Clientes on fa.IdCliente equals cl.IdCliente
                         join us in DB.Usuarios on fa.IdUsuario equals us.IdUsuario
                         where ((fa.IdEstado == 4) && fa.IdCliente > 0 && (fa.FechaFactura >= fechaAnterior && fa.FechaFactura <= fechaActual))
                         select new
                         {
                             fa.Consecutivo,
                             fa.FechaFactura,
                             fa.CostoTotal,
                             es.NombreEstado,
                             cl.Nombre,
                             us.NombreEmpleado
                         };

            if (result.ToList().Count > 0)
            {
                dgvFilter.DataSource = result.ToList();
                MessageBox.Show("Lista de los últimos 3 días", "Lista Facturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No hay registro de facturas en los últimos 3 días", "Lista Facturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //boton de filtrar datos a hoy
        private void btnFiltrarHoy_Click(object sender, EventArgs e)
        {
            dgvFilter.ClearSelection();
            Consecutivo = 0;
            BtnVerFact.Visible = false;

            DateTime FechaInicial = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            DateTime FechaFinal = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));

            // linq para validar y disenar mejor la DataGridView al usuario

            var result = DB.SPFactPorRangoFechaDetalles(FechaInicial, FechaFinal).ToList();


            var finalResult = from fa in result
                              select new
                              {
                                  fa.IdFactura,
                                  fa.Consecutivo,
                                  fa.FechaFactura,
                                  fa.CostoTotal,
                                  fa.NombreEstado,
                                  fa.Nombre,
                                  fa.NombreEmpleado
                              };

            finalResult = finalResult.Distinct();

            dgvFilter.DataSource = finalResult.ToList();


            if (result.ToList().Count > 0)
            {
                BtnVerFacturasList.Visible = true;
                btnReportExcel.Visible = true;
                btnReportPDF.Visible = true;
                valorHoy = 2;

                //para que se muestre el total (monto) de la consulta
                decimal valor = 0;
                foreach (var item in finalResult)
                {
                    valor += item.CostoTotal;
                }
                lblTotalMont.Text = string.Format("¢ {0:N2}", valor);
            }
            else
            {
                MessageBox.Show("No hay registro de facturas el día de hoy, puedes revisar las de crédito", "Lista Facturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnVerFacturasList.Visible = false;
                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;
                valorHoy = 0;

                lblTotalMont.Text = string.Format("¢ {0:N2}", 0);
            }
        }

        //valida que la fecha inicial no sea mayor a la fecha fin, pueden ser iguales
        private bool ValidarFechaLimite()
        {
            bool R;
            double dias = 0;
            DateTime FechaInicial = Convert.ToDateTime(DateInicio.Value.ToString("yyyy-MM-dd"));
            DateTime FechaFinal = Convert.ToDateTime(DateFin.Value.ToString("yyyy-MM-dd"));
            TimeSpan tiempo = FechaFinal.Subtract(FechaInicial);
            dias = Convert.ToInt32(tiempo.Days);
            
            if (dias >= 0)
            {
                R = true;
            }
            else
            {
                R = false;
            }
            
            return R;
        }

        private bool ValidarFechaLimite2()
        {
            bool R;
            double dias = 0;
            DateTime FechaInicial = Convert.ToDateTime(DateInicio2.Value.ToString("yyyy-MM-dd"));
            DateTime FechaFinal = Convert.ToDateTime(DateFin2.Value.ToString("yyyy-MM-dd"));
            TimeSpan tiempo = FechaFinal.Subtract(FechaInicial);
            dias = Convert.ToInt32(tiempo.Days);

            if (dias >= 0)
            {
                R = true;
            }
            else
            {
                R = false;
            }

            return R;
        }


        //boton de filtrar datos por fechas
        private void btnFiltrarFechas_Click(object sender, EventArgs e)
        {
            dgvFilter.ClearSelection();
            Consecutivo = 0;
            BtnVerFact.Visible = false;
            if (ValidarFechaLimite())
            {
                DateTime FechaInicial = Convert.ToDateTime(DateInicio.Value.ToString("yyyy-MM-dd"));
                DateTime FechaFinal = Convert.ToDateTime(DateFin.Value.ToString("yyyy-MM-dd"));

                // linq para validar y disenar mejor la DataGridView al usuario

                var result = DB.SPFactPorRangoFechaDetalles(FechaInicial, FechaFinal).ToList();

                var finalResult = from fa in result
                                  select new
                                  {
                                      fa.IdFactura,
                                      fa.Consecutivo,
                                      fa.FechaFactura,
                                      fa.CostoTotal,
                                      fa.NombreEstado,
                                      fa.Nombre,
                                      fa.NombreEmpleado
                                  };
                finalResult = finalResult.Distinct();

                dgvFilter.DataSource = finalResult.ToList();
                if (result.ToList().Count > 0)
                {
                    BtnVerFacturasList.Visible = true;
                    btnReportExcel.Visible = true;
                    btnReportPDF.Visible = true;
                    valorPorFechas = 2;

                    //para que se muestre el total (monto) de la consulta
                    decimal valor = 0;
                    foreach (var item in finalResult)
                    {
                        valor += item.CostoTotal;
                    }
                    lblTotalMont.Text = string.Format("¢ {0:N2}", valor);
                }
                else
                {
                    MessageBox.Show("No hay registro de facturas en el rango de fechas indicado, puedes revisar las de crédito", "Lista Facturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BtnVerFacturasList.Visible = false;
                    btnReportExcel.Visible = false;
                    btnReportPDF.Visible = false;
                    valorPorFechas = 0;
                    
                    lblTotalMont.Text = string.Format("¢ {0:N2}", 0);
                }
            }
            else
            {
                MessageBox.Show("Selecciona correctamente las fecha para filtrar la información.",
                                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //boton de filtrar datos a facts pendientes conlineas
        private void btnFiltrarCredito_Click(object sender, EventArgs e)
        {
            dgvFilter.ClearSelection();
            Consecutivo = 0;
            BtnVerFact.Visible = false;
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
                                  fa.NombreEmpleado
                              };

            finalResult = finalResult.Distinct();

            dgvFilter.DataSource = finalResult.ToList();
            if (finalResult.ToList().Count > 0)
            {
                BtnVerFacturasList.Visible = true;
                btnReportExcel.Visible = true;
                btnReportPDF.Visible = true;
                valorPendiente = 3;

                //para que se muestre el total (monto) de la consulta
                decimal valor = 0;
                foreach (var item in finalResult)
                {
                    valor += item.CostoTotal;
                }
                lblTotalMont.Text = string.Format("¢ {0:N2}", valor);
            }
            else
            {
                MessageBox.Show("No hay registro de facturas a crédito", "Lista Facturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnVerFacturasList.Visible = false;
                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;
                valorPendiente = 0;

                
                lblTotalMont.Text = string.Format("¢ {0:N2}", 0);
            }
        }


        //cambios de check box
        private void RbHoy_CheckedChanged(object sender, EventArgs e)
        {
            if (RbHoy.Checked)
            {
                btnFiltrarHoy.Visible = true;
                btnFiltrarHoySoloBackHoe.Visible = true;
                btnFiltrarHoyTodas.Visible = true;

                btnFiltrarFechasCorrecta.Visible = false;
                btnFiltrarFechaBachHoe.Visible = false;
                btnFiltrarTodasVentas.Visible = false;
                DateInicio.Visible = false;
                DateFin.Visible = false;


                btnFiltrarCredito.Visible = false;
                btnCredSinLineas.Visible = false;
                btnCreditoTodas.Visible = false;

                btnAnuladas.Visible = false;
                DateInicio2.Visible = false;
                DateFin2.Visible = false;


                BtnVerFacturasList.Visible = false;
                Consecutivo = 0;

                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;

                valorPendiente = 0; //accion a facturas de credito
                valorPorFechas = 0; //accion a facturas de filtro por fechas
                valorHoy = 0; //acciones de rango de fechas
                dgvFilter.ClearSelection();
            }
        }

        //cambios de check box
        private void RbFechas_CheckedChanged(object sender, EventArgs e)
        {
            if (RbFechas.Checked)
            {
                btnFiltrarHoy.Visible = false;
                btnFiltrarHoySoloBackHoe.Visible = false;
                btnFiltrarHoyTodas.Visible = false;

                btnFiltrarFechasCorrecta.Visible = true;
                btnFiltrarFechaBachHoe.Visible = true;
                btnFiltrarTodasVentas.Visible = true;
                DateInicio.Visible = true;
                DateFin.Visible = true;


                btnFiltrarCredito.Visible = false;
                btnCredSinLineas.Visible = false;
                btnCreditoTodas.Visible = false;

                btnAnuladas.Visible = false;
                DateInicio2.Visible = false;
                DateFin2.Visible = false;

                BtnVerFacturasList.Visible = false;
                Consecutivo = 0;

                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;


                valorPendiente = 0; //accion a facturas de credito
                valorPorFechas = 0; //accion a facturas de filtro por fechas
                valorHoy = 0; //acciones de rango de fechas
                dgvFilter.ClearSelection();
            }
        }

        //cambios de check box
        private void RbPendientes_CheckedChanged(object sender, EventArgs e)
        {
            if (RbPendientes.Checked)
            {
                btnFiltrarHoy.Visible = false;
                btnFiltrarHoySoloBackHoe.Visible = false;
                btnFiltrarHoyTodas.Visible = false;

                btnFiltrarFechasCorrecta.Visible = false;
                btnFiltrarFechaBachHoe.Visible = false;
                btnFiltrarTodasVentas.Visible = false;
                DateInicio.Visible = false;
                DateFin.Visible = false;


                btnFiltrarCredito.Visible = true;
                btnCredSinLineas.Visible = true;
                btnCreditoTodas.Visible = true;

                btnAnuladas.Visible = false;
                DateInicio2.Visible = false;
                DateFin2.Visible = false;


                BtnVerFacturasList.Visible = false;
                Consecutivo = 0;

                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;

                valorPendiente = 0; //accion a facturas credito
                valorPorFechas = 0; //accion a facturas de filtro por fechas
                valorHoy = 0; //acciones de rango de fechas
                dgvFilter.ClearSelection();
            }
        }

        private void RbAnuladas_CheckedChanged(object sender, EventArgs e)
        {
            if (RbAnuladas.Checked)
            {
                btnFiltrarHoy.Visible = false;
                btnFiltrarHoySoloBackHoe.Visible = false;
                btnFiltrarHoyTodas.Visible = false;

                btnFiltrarFechasCorrecta.Visible = false;
                btnFiltrarFechaBachHoe.Visible = false;
                btnFiltrarTodasVentas.Visible = false;
                DateInicio.Visible = false;
                DateFin.Visible = false;


                btnFiltrarCredito.Visible = false;
                btnCredSinLineas.Visible = false;
                btnCreditoTodas.Visible = false;

                btnAnuladas.Visible = true;
                DateInicio2.Visible = true;
                DateFin2.Visible = true;


                BtnVerFacturasList.Visible = false;
                Consecutivo = 0;

                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;


                valorPendiente = 0; //accion a facturas credito
                valorPorFechas = 0; //accion a facturas de filtro por fechas
                valorHoy = 0; //acciones de rango de fechas

                dgvFilter.ClearSelection();
            }
        }

        //boton que muestra el reporte de la factura 
        private void BtnVerFact_Click(object sender, EventArgs e)
        {

            if (Consecutivo > 0)
            {
                facturas = DB.Facturas.Find(Id);
                if (facturas.IdEstado == 5  || facturas.IdEstado == 12 || facturas.IdEstado == 13)
                {
                    using (FrmPrintFactRev frm = new FrmPrintFactRev(Consecutivo))
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        frm.ShowDialog();
                        Cursor.Current = Cursors.Default;
                    };
                }
                else
                {
               
                    using (FrmPrintFact frm = new FrmPrintFact(Consecutivo))
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        frm.ShowDialog();
                        Cursor.Current = Cursors.Default;
                    };
                }
               
            }
            else
            {
                MessageBox.Show("Debes de seleccionar una factura para poder verla.",
                                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            

        }
        //boton que muestra el listado de reportes de fracturas
        private void BtnVerFacturasList_Click(object sender, EventArgs e)
        {

            string FechaInicial = Convert.ToString(DateInicio.Value.ToString("yyyy-MM-dd"));
            string FechaFinal = Convert.ToString(DateFin.Value.ToString("yyyy-MM-dd"));

            string FechaInicial2 = Convert.ToString(DateInicio2.Value.ToString("yyyy-MM-dd"));
            string FechaFinal2 = Convert.ToString(DateFin2.Value.ToString("yyyy-MM-dd"));

            string Hoy = Convert.ToString(DateTime.Today.ToString("yyyy-MM-dd"));

            //anuladas reversada o notas credito
            if (RbAnuladas.Checked)
            {
                DialogResult respuesta = MessageBox.Show("¿Deseas visualizar todas las facturas anuladas o reversadas en el rango de fechas indicado?.",
                                                "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    using (FrmPrintFactAnuladas frm = new FrmPrintFactAnuladas(FechaInicial2, FechaFinal2))
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        frm.ShowDialog();
                        Cursor.Current = Cursors.Default;
                    };
                }
            }

            //pendiente por credito
            if (RbPendientes.Checked)
            {
                 switch (valorPendiente)
                {
                    case 0:
                        MessageBox.Show("Debes de seleccionar un filtro para buscar las facturas.",
                      "Registro de Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 1:
                        DialogResult respuesta1 = MessageBox.Show("¿Deseas visualizar todas las facturas pendientes de solo backhoe y transporte?.",
                                                "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta1 == DialogResult.Yes)
                        {
                            using (FrmPrintFactPendSinD frm = new FrmPrintFactPendSinD())
                            {
                                Cursor.Current = Cursors.WaitCursor;
                                frm.ShowDialog();
                                Cursor.Current = Cursors.Default;
                            };
                        }
                        break;
                    case 2:
                        DialogResult respuesta2 = MessageBox.Show("¿Deseas visualizar todas las facturas pendientes con detalles de materiales?.",
                                               "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta2 == DialogResult.Yes)
                        {
                            using (FrmPrintFactPendConD frm = new FrmPrintFactPendConD())
                            {
                                Cursor.Current = Cursors.WaitCursor;
                                frm.ShowDialog();
                                Cursor.Current = Cursors.Default;
                            };
                        }
                        break;
                    case 3:
                        DialogResult respuesta3 = MessageBox.Show("¿Deseas visualizar todas las facturas pendientes?.",
                                               "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta3 == DialogResult.Yes)
                        {
                            using (FrmPrintFactPendAll frm = new FrmPrintFactPendAll())
                            {
                                Cursor.Current = Cursors.WaitCursor;
                                frm.ShowDialog();
                                Cursor.Current = Cursors.Default;
                            };
                        }
                        break;
                    default:
                        MessageBox.Show("Debes de seleccionar un filtro para buscar las facturas, si ya lo seleccionaste, " +
                            "entonces ha ocurrido un error, favor contactar al administrador.",
                      "Registro de Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }

            //por fechas
            if (RbFechas.Checked)
            {
                switch (valorPorFechas)
                {
                    case 0:
                        MessageBox.Show("Debes de seleccionar un filtro para buscar las facturas.",
                      "Registro de Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 1:
                        DialogResult respuesta1 = MessageBox.Show("¿Deseas visualizar todas las facturas procesadas de solo backhoe y transporte?.",
                                                "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta1 == DialogResult.Yes)
                        {
                            using (FrmPrintFactFilterSinDetalle frm = new FrmPrintFactFilterSinDetalle(FechaInicial, FechaFinal))
                            {
                                Cursor.Current = Cursors.WaitCursor;
                                frm.ShowDialog();
                                Cursor.Current = Cursors.Default;
                            };
                        }
                        break;
                    case 2:
                        DialogResult respuesta2 = MessageBox.Show("¿Deseas visualizar todas las facturas procesadas con detalles de materiales?.",
                                               "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta2 == DialogResult.Yes)
                        {
                            using (FrmPrintFactFilterDetalle frm = new FrmPrintFactFilterDetalle(FechaInicial, FechaFinal))
                            {
                                Cursor.Current = Cursors.WaitCursor;
                                frm.ShowDialog();
                                Cursor.Current = Cursors.Default;
                            };
                        }
                        break;
                    case 3:
                        DialogResult respuesta3 = MessageBox.Show("¿Deseas visualizar todas las facturas procesadas?.",
                                               "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta3 == DialogResult.Yes)
                        {
                            using (FrmPrintFactFilterAll frm = new FrmPrintFactFilterAll(FechaInicial, FechaFinal))
                            {
                                Cursor.Current = Cursors.WaitCursor;
                                frm.ShowDialog();
                                Cursor.Current = Cursors.Default;
                            };
                        }
                        break;
                    default:
                        MessageBox.Show("Debes de seleccionar un filtro para buscar las facturas, si ya lo seleccionaste, " +
                            "entonces ha ocurrido un error, favor contactar al administrador.",
                      "Registro de Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }

            //por fechas hoy
            if (RbHoy.Checked)
            {
                switch (valorHoy)
                {
                    case 0:
                        MessageBox.Show("Debes de seleccionar un filtro para buscar las facturas.",
                      "Registro de Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 1:
                        DialogResult respuesta1 = MessageBox.Show("¿Deseas visualizar todas las facturas procesadas de solo backhoe y transporte?.",
                                                "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta1 == DialogResult.Yes)
                        {
                            using (FrmPrintFactFilterSinDetalle frm = new FrmPrintFactFilterSinDetalle(Hoy, Hoy))
                            {
                                Cursor.Current = Cursors.WaitCursor;
                                frm.ShowDialog();
                                Cursor.Current = Cursors.Default;
                            };
                        }
                        break;
                    case 2:
                        DialogResult respuesta2 = MessageBox.Show("¿Deseas visualizar todas las facturas procesadas con detalles de materiales?.",
                                               "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta2 == DialogResult.Yes)
                        {
                            using (FrmPrintFactFilterDetalle frm = new FrmPrintFactFilterDetalle(Hoy, Hoy))
                            {
                                Cursor.Current = Cursors.WaitCursor;
                                frm.ShowDialog();
                                Cursor.Current = Cursors.Default;
                            };
                        }
                        break;
                    case 3:
                        DialogResult respuesta3 = MessageBox.Show("¿Deseas visualizar todas las facturas procesadas?.",
                                               "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta3 == DialogResult.Yes)
                        {
                            using (FrmPrintFactFilterAll frm = new FrmPrintFactFilterAll(Hoy, Hoy))
                            {
                                Cursor.Current = Cursors.WaitCursor;
                                frm.ShowDialog();
                                Cursor.Current = Cursors.Default;
                            };
                        }
                        break;
                    default:
                        MessageBox.Show("Debes de seleccionar un filtro para buscar las facturas, si ya lo seleccionaste, " +
                            "entonces ha ocurrido un error, favor contactar al administrador.",
                      "Registro de Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }

        }

        //evento que cuando se selecciona un elemento en el datagrid, toma el consecutivo para ser usado en los botones
        private void dgvFilter_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFilter.SelectedRows.Count == 1)
            {
                DataGridViewRow MiFila = dgvFilter.SelectedRows[0];

                Consecutivo = Convert.ToInt32(MiFila.Cells["CConsecutivo"].Value);
                Id = Convert.ToInt32(MiFila.Cells["CID"].Value);

                if (Consecutivo > 0 && Id > 0)
                {
                    BtnVerFact.Visible = true;
                }
            }
            else
            {
                Consecutivo = 0;
                Id = 0;
            }
        }

        private void gridViewNoVisible()
        {
            this.dgvFilter.Columns["CID"].Visible = false;
            this.dgvFilter.Columns["CConsecutivo"].Visible = true;
            this.dgvFilter.Columns["CCostoTransporte"].Visible = false;
            this.dgvFilter.Columns["CBackHoe"].Visible = false;
            this.dgvFilter.Columns["CFechaFactura"].Visible = true;
            this.dgvFilter.Columns["CSubTotalFact"].Visible = false;
            this.dgvFilter.Columns["CIVAFact"].Visible = false;
            this.dgvFilter.Columns["CCostoTotal"].Visible = true;
            this.dgvFilter.Columns["CReferenciaPago"].Visible = false;
            this.dgvFilter.Columns["CFechaLimiteP"].Visible = false;
            this.dgvFilter.Columns["CNombreEstado"].Visible = true;
            this.dgvFilter.Columns["CTipoPago"].Visible = false;
            this.dgvFilter.Columns["CNombreEmpleado"].Visible = true;
            this.dgvFilter.Columns["CNombre"].Visible = true;
            this.dgvFilter.Columns["CTipo"].Visible = false;
            this.dgvFilter.Columns["CCantidad"].Visible = false;
            this.dgvFilter.Columns["CPrecio"].Visible = false;
            this.dgvFilter.Columns["CSubtotal"].Visible = false;
            this.dgvFilter.Columns["CIVA"].Visible = false;
            this.dgvFilter.Columns["CTotal"].Visible = false;
            this.dgvFilter.Columns["CNombreMaterial"].Visible = false;
            this.dgvFilter.Columns["CIdMaterial"].Visible = false;
            
        }
        private void gridViewVisible()
        {
            this.dgvFilter.Columns["CID"].Visible = true;
            this.dgvFilter.Columns["CConsecutivo"].Visible = true;
            this.dgvFilter.Columns["CCostoTransporte"].Visible = true;
            this.dgvFilter.Columns["CBackHoe"].Visible = true;
            this.dgvFilter.Columns["CFechaFactura"].Visible = true;
            this.dgvFilter.Columns["CSubTotalFact"].Visible = true;
            this.dgvFilter.Columns["CIVAFact"].Visible = true;
            this.dgvFilter.Columns["CCostoTotal"].Visible = true;
            this.dgvFilter.Columns["CReferenciaPago"].Visible = true;
            this.dgvFilter.Columns["CFechaLimiteP"].Visible = true;
            this.dgvFilter.Columns["CNombreEstado"].Visible = true;
            this.dgvFilter.Columns["CTipoPago"].Visible = true;
            this.dgvFilter.Columns["CNombreEmpleado"].Visible = true;
            this.dgvFilter.Columns["CNombre"].Visible = true;
            this.dgvFilter.Columns["CTipo"].Visible = true;
            this.dgvFilter.Columns["CCantidad"].Visible = true;
            this.dgvFilter.Columns["CPrecio"].Visible = true;
            this.dgvFilter.Columns["CSubtotal"].Visible = true;
            this.dgvFilter.Columns["CIVA"].Visible = true;
            this.dgvFilter.Columns["CTotal"].Visible = true;
            this.dgvFilter.Columns["CNombreMaterial"].Visible = true;
            this.dgvFilter.Columns["CIdMaterial"].Visible = true;

        }


        //export to excel segun el check box elegido, valida que no este en uso el archivo
        private void btnReportExcel_Click(object sender, EventArgs e)
        {
            //parametro para fechas 1
            DateTime FechaInicial = Convert.ToDateTime(DateInicio.Value.ToString("yyyy-MM-dd"));
            DateTime FechaFinal = Convert.ToDateTime(DateFin.Value.ToString("yyyy-MM-dd"));

            //paraemtros para fechas 2
            DateTime FechaInicial2 = Convert.ToDateTime(DateInicio2.Value.ToString("yyyy-MM-dd"));
            DateTime FechaFinal2 = Convert.ToDateTime(DateFin2.Value.ToString("yyyy-MM-dd"));

            //parametros hoy
            DateTime Inicial = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            DateTime Final = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));


            if (RbHoy.Checked)
            {
                try
                {
                    switch (valorHoy)
                    {
                        case 0:
                            MessageBox.Show("Debes de seleccionar un filtro para buscar las facturas.",
                          "Registro de Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 1:
                            DialogResult respuesta1 = MessageBox.Show("¿Deseas exportar a excel, todas las facturas procesadas de solo backhoe y transporte del día de hoy?.",
                                                    "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (respuesta1 == DialogResult.Yes)
                            {
                                var result = DB.SPFactPorRangoFechaSinDetalles(Inicial, Final).ToList();

                                if (result.Count > 0)
                                {
                                    SaveFileDialog saveFileDialog = new SaveFileDialog
                                    {
                                        Filter = "Excel|*.xlsx"
                                    };

                                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                    {
                                        Cursor.Current = Cursors.WaitCursor;

                                        ExcelMapper mapper = new ExcelMapper();
                                        var file = saveFileDialog.FileName;
                                        mapper.Save(file, result, "ReportVtasHoyTodo", true); //true is for saving .xlsx

                                        Cursor.Current = Cursors.Default;

                                        MessageBox.Show("Se exporto correctamente el documento.",
                                                            "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No hay datos que exportar",
                                                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            break;
                        case 2:
                            DialogResult respuesta2 = MessageBox.Show("¿Deseas exportar a excel todas las facturas procesadas con detalles de materiales del día de hoy?.",
                                                   "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (respuesta2 == DialogResult.Yes)
                            {
                                var result = DB.SPFactPorRangoFechaDetalles(Inicial, Final).ToList();
                               
                                if (result.Count > 0)
                                {
                                    SaveFileDialog saveFileDialog = new SaveFileDialog
                                    {
                                        Filter = "Excel|*.xlsx"
                                    };

                                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                    {
                                        Cursor.Current = Cursors.WaitCursor;

                                        ExcelMapper mapper = new ExcelMapper();
                                        var file = saveFileDialog.FileName;
                                        mapper.Save(file, result, "ReportVtasHoy", true); //true is for saving .xlsx

                                        Cursor.Current = Cursors.Default;

                                        MessageBox.Show("Se exporto correctamente el documento.",
                                                            "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No hay datos que exportar",
                                                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            break;
                        case 3:
                            DialogResult respuesta3 = MessageBox.Show("¿Deseas exportar a excel todas las facturas procesadas de hoy?.",
                                                   "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (respuesta3 == DialogResult.Yes)
                            {
                                var result = DB.SPFactPorRangoFechaAll(Inicial, Final).ToList();

                                if (result.Count > 0)
                                {
                                    SaveFileDialog saveFileDialog = new SaveFileDialog
                                    {
                                        Filter = "Excel|*.xlsx"
                                    };

                                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                    {
                                        Cursor.Current = Cursors.WaitCursor;

                                        ExcelMapper mapper = new ExcelMapper();
                                        var file = saveFileDialog.FileName;
                                        mapper.Save(file, result, "ReportVtasHoyBachHoe", true); //true is for saving .xlsx

                                        Cursor.Current = Cursors.Default;
                                        MessageBox.Show("Se exporto correctamente el documento.",
                                                            "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No hay datos que exportar",
                                                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            break;
                        default:
                            MessageBox.Show("Debes de seleccionar un filtro para buscar las facturas, si ya lo seleccionaste, " +
                                "entonces ha ocurrido un error, favor contactar al administrador.",
                          "Registro de Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                if (RbFechas.Checked)
                {
                    try
                    {
                        if (ValidarFechaLimite())
                        {


                            switch (valorPorFechas)
                            {
                                case 0:
                                    MessageBox.Show("Debes de seleccionar un filtro para buscar las facturas.",
                                  "Registro de Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                case 1:
                                    DialogResult respuesta1 = MessageBox.Show("¿Deseas exportar a excel, todas las facturas procesadas de solo backhoe y transporte del rango indicado?.",
                                                            "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta1 == DialogResult.Yes)
                                    {
                                        var result = DB.SPFactPorRangoFechaSinDetalles(FechaInicial, FechaFinal).ToList();

                                        if (result.Count > 0)
                                        {
                                            SaveFileDialog saveFileDialog = new SaveFileDialog
                                            {
                                                Filter = "Excel|*.xlsx"
                                            };

                                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                            {
                                                Cursor.Current = Cursors.WaitCursor;

                                                ExcelMapper mapper = new ExcelMapper();
                                                var file = saveFileDialog.FileName;
                                                mapper.Save(file, result, "ReportVtasBachHoe", true); //true is for saving .xlsx
                                                Cursor.Current = Cursors.Default;
                                                MessageBox.Show("Se exporto correctamente el documento.",
                                                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("No hay datos que exportar",
                                                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    break;
                                case 2:
                                    DialogResult respuesta2 = MessageBox.Show("¿Deseas exportar a excel todas las facturas procesadas con detalles de materiales del rango indicado?.",
                                                           "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta2 == DialogResult.Yes)
                                    {
                                        var result = DB.SPFactPorRangoFechaDetalles(FechaInicial, FechaFinal).ToList();

                                        if (result.Count > 0)
                                        {
                                            SaveFileDialog saveFileDialog = new SaveFileDialog
                                            {
                                                Filter = "Excel|*.xlsx"
                                            };

                                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                            {
                                                Cursor.Current = Cursors.WaitCursor;
                                                ExcelMapper mapper = new ExcelMapper();
                                                var file = saveFileDialog.FileName;
                                                mapper.Save(file, result, "ReportVtas", true); //true is for saving .xlsx
                                                Cursor.Current = Cursors.Default;
                                                MessageBox.Show("Se exporto correctamente el documento.",
                                                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("No hay datos que exportar",
                                                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    break;
                                case 3:
                                    DialogResult respuesta3 = MessageBox.Show("¿Deseas exportar a excel todas las facturas procesadas del rango indicado?.",
                                                           "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta3 == DialogResult.Yes)
                                    {
                                        var result = DB.SPFactPorRangoFechaAll(FechaInicial, FechaFinal).ToList();
                                        if (result.Count > 0)
                                        {
                                            SaveFileDialog saveFileDialog = new SaveFileDialog
                                            {
                                                Filter = "Excel|*.xlsx"
                                            };

                                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                            {
                                                Cursor.Current = Cursors.WaitCursor;

                                                ExcelMapper mapper = new ExcelMapper();
                                                var file = saveFileDialog.FileName;
                                                mapper.Save(file, result, "ReportVtasTodas", true); //true is for saving .xlsx
                                                Cursor.Current = Cursors.Default;
                                                MessageBox.Show("Se exporto correctamente el documento.",
                                                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("No hay datos que exportar",
                                                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    break;
                                default:
                                    MessageBox.Show("Debes de seleccionar un filtro para buscar las facturas, si ya lo seleccionaste, " +
                                        "entonces ha ocurrido un error, favor contactar al administrador.",
                                  "Registro de Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Selecciona correctamente las fecha para filtrar la información.",
                                                           "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (RbPendientes.Checked)
                    {
                        try
                        {
                            switch (valorPendiente)
                            {
                                case 0:
                                    MessageBox.Show("Debes de seleccionar un filtro para buscar las facturas.",
                                  "Registro de Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                case 1:
                                    DialogResult respuesta1 = MessageBox.Show("¿Deseas exportar a excel, todas las facturas a crédito de solo backhoe y transporte?.",
                                                            "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta1 == DialogResult.Yes)
                                    {
                                        var result = DB.SPFactPendSinDetalle().ToList();
                                        if (result.Count > 0)
                                        {
                                            SaveFileDialog saveFileDialog = new SaveFileDialog
                                            {
                                                Filter = "Excel|*.xlsx"
                                            };

                                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                            {
                                                Cursor.Current = Cursors.WaitCursor;
                                                ExcelMapper mapper = new ExcelMapper();
                                                var file = saveFileDialog.FileName;
                                                mapper.Save(file, result, "ReportVtasCreditoBackHoe", true); //true is for saving .xlsx
                                                Cursor.Current = Cursors.Default;
                                                MessageBox.Show("Se exporto correctamente el documento.",
                                                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("No hay datos que exportar",
                                                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    break;
                                case 2:
                                    DialogResult respuesta2 = MessageBox.Show("¿Deseas exportar a excel todas las facturas a crédito con detalles de materiales?.",
                                                           "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta2 == DialogResult.Yes)
                                    {
                                        var result = DB.SPFactPendDetalles().ToList();

                                        if (result.Count > 0)
                                        {
                                            SaveFileDialog saveFileDialog = new SaveFileDialog
                                            {
                                                Filter = "Excel|*.xlsx"
                                            };

                                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                            {
                                                Cursor.Current = Cursors.WaitCursor;
                                                ExcelMapper mapper = new ExcelMapper();
                                                var file = saveFileDialog.FileName;
                                                mapper.Save(file, result, "ReportVtasCredito", true); //true is for saving .xlsx
                                                Cursor.Current = Cursors.Default;
                                                MessageBox.Show("Se exporto correctamente el documento.",
                                                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("No hay datos que exportar",
                                                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    break;
                                case 3:
                                    DialogResult respuesta3 = MessageBox.Show("¿Deseas exportar a excel todas las facturas a crédito?.",
                                                           "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta3 == DialogResult.Yes)
                                    {
                                        var result = DB.SPFactPendAll().ToList();

                                        if (result.Count > 0)
                                        {
                                            SaveFileDialog saveFileDialog = new SaveFileDialog
                                            {
                                                Filter = "Excel|*.xlsx"
                                            };

                                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                            {
                                                Cursor.Current = Cursors.WaitCursor;
                                                ExcelMapper mapper = new ExcelMapper();
                                                var file = saveFileDialog.FileName;
                                                mapper.Save(file, result, "ReportVtasCreditoTodas", true); //true is for saving .xlsx
                                                Cursor.Current = Cursors.Default;
                                                MessageBox.Show("Se exporto correctamente el documento.",
                                                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("No hay datos que exportar",
                                                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    break;
                                default:
                                    MessageBox.Show("Debes de seleccionar un filtro para buscar las facturas, si ya lo seleccionaste, " +
                                        "entonces ha ocurrido un error, favor contactar al administrador.",
                                  "Registro de Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    else
                    {
                        if (RbAnuladas.Checked)
                        {
                            try
                            {
                                if (ValidarFechaLimite2())
                                {
                                    DialogResult respuesta1 = MessageBox.Show("¿Deseas exportar a excel, todas las facturas anuladas / reversadas / notas de crédito?.",
                                                                                           "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta1 == DialogResult.Yes)
                                    {
                                        var result = DB.SPFactReversadasAll(FechaInicial2, FechaFinal2).ToList();

                                        if (result.Count > 0)
                                        {
                                            SaveFileDialog saveFileDialog = new SaveFileDialog
                                            {
                                                Filter = "Excel|*.xlsx"
                                            };

                                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                            {
                                                Cursor.Current = Cursors.WaitCursor;
                                                ExcelMapper mapper = new ExcelMapper();
                                                var file = saveFileDialog.FileName;
                                                mapper.Save(file, result, "ReportVtasAnuladas", true); //true is for saving .xlsx
                                                Cursor.Current = Cursors.Default;
                                                MessageBox.Show("Se exporto correctamente el documento.",
                                                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("No hay datos que exportar",
                                                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }
                    }
                }
            }
        }



        //export to PDF segun el check box elegido, valida que no este en uso el archivo
        private void btnReportPDF_Click(object sender, EventArgs e)
        {
            //para SP consultas
            //parametro para fechas 1
            DateTime FechaInicial = Convert.ToDateTime(DateInicio.Value.ToString("yyyy-MM-dd"));
            DateTime FechaFinal = Convert.ToDateTime(DateFin.Value.ToString("yyyy-MM-dd"));

            //paraemtros para fechas 2
            DateTime FechaInicial2 = Convert.ToDateTime(DateInicio2.Value.ToString("yyyy-MM-dd"));
            DateTime FechaFinal2 = Convert.ToDateTime(DateFin2.Value.ToString("yyyy-MM-dd"));

            //parametros hoy
            DateTime Inicial = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            DateTime Final = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));

            //----------------------------------------------------------------------------------//

            //para pdf reports
            string pFechaInicial = Convert.ToString(DateInicio.Value.ToString("yyyy-MM-dd"));
            string pFechaFinal = Convert.ToString(DateFin.Value.ToString("yyyy-MM-dd"));

            string pFechaInicial2 = Convert.ToString(DateInicio2.Value.ToString("yyyy-MM-dd"));
            string pFechaFinal2 = Convert.ToString(DateFin2.Value.ToString("yyyy-MM-dd"));

            //parametros hoy
            string pInicial2 = Convert.ToString(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            string pFinal2 = Convert.ToString(DateTime.Now.Date.ToString("yyyy-MM-dd"));

            if (RbHoy.Checked)
            {
                try
                {
                    switch (valorHoy)
                    {
                        case 0:
                            MessageBox.Show("Debes de seleccionar un filtro para buscar las facturas.",
                          "Registro de Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 1:
                            DialogResult respuesta1 = MessageBox.Show("¿Deseas exportar a pdf, todas las facturas procesadas de solo backhoe y transporte del día de hoy?.",
                                                    "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (respuesta1 == DialogResult.Yes)
                            {
                                var result = DB.SPFactPorRangoFechaSinDetalles(Inicial, Final).ToList();
                                if (result.Count > 0)
                                {
                                    RptFactFilteredSinDetalle rptFactFilteredSinDetalle = new RptFactFilteredSinDetalle();
                                    rptFactFilteredSinDetalle.SetParameterValue("@fechaInicio", pInicial2);
                                    rptFactFilteredSinDetalle.SetParameterValue("@fechaFin", pFinal2);

                                    //datos para export pdf el report
                                    ExportOptions exportOptions;
                                    DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                                    SaveFileDialog dialog = new SaveFileDialog();
                                    dialog.Filter = "Pdf|*.pdf";
                                    if (dialog.ShowDialog() == DialogResult.OK)
                                    {
                                        diskFileDestinationOptions.DiskFileName = dialog.FileName;
                                    }
                                    Cursor.Current = Cursors.WaitCursor;

                                    exportOptions = rptFactFilteredSinDetalle.ExportOptions;
                                    {
                                        exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exportOptions.ExportDestinationOptions = diskFileDestinationOptions;
                                        exportOptions.ExportFormatOptions = new PdfRtfWordFormatOptions();
                                    }
                                    rptFactFilteredSinDetalle.Export();

                                    Cursor.Current = Cursors.Default;

                                    MessageBox.Show("Se exporto correctamente el documento.",
                                                            "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No hay datos que exportar",
                                                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            break;
                        case 2:
                            DialogResult respuesta2 = MessageBox.Show("¿Deseas exportar a pdf todas las facturas procesadas con detalles de materiales del día de hoy?.",
                                                   "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (respuesta2 == DialogResult.Yes)
                            {
                                var result = DB.SPFactPorRangoFechaDetalles(Inicial, Final).ToList();
                                if (result.Count > 0)
                                {
                                    RptFactFilteredDetalle rptFactFilteredDetalle = new RptFactFilteredDetalle();
                                    rptFactFilteredDetalle.SetParameterValue("@fechaInicio", pInicial2);
                                    rptFactFilteredDetalle.SetParameterValue("@fechaFin", pFinal2);

                                    //datos para export pdf el report
                                    ExportOptions exportOptions;
                                    DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                                    SaveFileDialog dialog = new SaveFileDialog();
                                    dialog.Filter = "Pdf|*.pdf";
                                    if (dialog.ShowDialog() == DialogResult.OK)
                                    {
                                        diskFileDestinationOptions.DiskFileName = dialog.FileName;
                                    }
                                    Cursor.Current = Cursors.WaitCursor;
                                    exportOptions = rptFactFilteredDetalle.ExportOptions;
                                    {
                                        exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exportOptions.ExportDestinationOptions = diskFileDestinationOptions;
                                        exportOptions.ExportFormatOptions = new PdfRtfWordFormatOptions();
                                    }
                                    rptFactFilteredDetalle.Export();
                                    Cursor.Current = Cursors.Default;
                                    MessageBox.Show("Se exporto correctamente el documento.",
                                                            "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No hay datos que exportar",
                                                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            break;
                        case 3:
                            DialogResult respuesta3 = MessageBox.Show("¿Deseas exportar a pdf todas las facturas procesadas de hoy?.",
                                                   "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (respuesta3 == DialogResult.Yes)
                            {
                                var result = DB.SPFactPorRangoFechaAll(Inicial, Final).ToList();
                                if (result.Count > 0)
                                {
                                    RptPrintFactFilterAll rptPrintFactFilterAll = new RptPrintFactFilterAll();
                                    rptPrintFactFilterAll.SetParameterValue("@fechaInicio", pInicial2);
                                    rptPrintFactFilterAll.SetParameterValue("@fechaFin", pFinal2);

                                    //datos para export pdf el report
                                    ExportOptions exportOptions;
                                    DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                                    SaveFileDialog dialog = new SaveFileDialog();
                                    dialog.Filter = "Pdf|*.pdf";
                                    if (dialog.ShowDialog() == DialogResult.OK)
                                    {
                                        diskFileDestinationOptions.DiskFileName = dialog.FileName;
                                    }
                                    Cursor.Current = Cursors.WaitCursor;
                                    exportOptions = rptPrintFactFilterAll.ExportOptions;
                                    {
                                        exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exportOptions.ExportDestinationOptions = diskFileDestinationOptions;
                                        exportOptions.ExportFormatOptions = new PdfRtfWordFormatOptions();
                                    }
                                    rptPrintFactFilterAll.Export();
                                    Cursor.Current = Cursors.Default;
                                    MessageBox.Show("Se exporto correctamente el documento.",
                                                            "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No hay datos que exportar",
                                                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            break;
                        default:
                            MessageBox.Show("Debes de seleccionar un filtro para buscar las facturas, si ya lo seleccionaste, " +
                                "entonces ha ocurrido un error, favor contactar al administrador.",
                          "Registro de Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                if (RbFechas.Checked)
                {
                    try
                    {
                        if (ValidarFechaLimite())
                        {
                            switch (valorPorFechas)
                            {
                                case 0:
                                    MessageBox.Show("Debes de seleccionar un filtro para buscar las facturas.",
                                  "Registro de Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                case 1:
                                    DialogResult respuesta1 = MessageBox.Show("¿Deseas exportar a pdf, todas las facturas procesadas de solo backhoe y transporte del rango indicado?.",
                                                            "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta1 == DialogResult.Yes)
                                    {
                                        var result = DB.SPFactPorRangoFechaSinDetalles(FechaInicial, FechaFinal).ToList();
                                        if (result.Count > 0)
                                        {
                                            RptFactFilteredSinDetalle rptFactFilteredSinDetalle = new RptFactFilteredSinDetalle();
                                            rptFactFilteredSinDetalle.SetParameterValue("@fechaInicio", pFechaInicial);
                                            rptFactFilteredSinDetalle.SetParameterValue("@fechaFin", pFechaFinal);

                                            //datos para export pdf el report
                                            ExportOptions exportOptions;
                                            DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                                            SaveFileDialog dialog = new SaveFileDialog();
                                            dialog.Filter = "Pdf|*.pdf";
                                            if (dialog.ShowDialog() == DialogResult.OK)
                                            {
                                                diskFileDestinationOptions.DiskFileName = dialog.FileName;
                                            }
                                            Cursor.Current = Cursors.WaitCursor;
                                            exportOptions = rptFactFilteredSinDetalle.ExportOptions;
                                            {
                                                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                                exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                                exportOptions.ExportDestinationOptions = diskFileDestinationOptions;
                                                exportOptions.ExportFormatOptions = new PdfRtfWordFormatOptions();
                                            }
                                            rptFactFilteredSinDetalle.Export();
                                            Cursor.Current = Cursors.Default;
                                            MessageBox.Show("Se exporto correctamente el documento.",
                                                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            MessageBox.Show("No hay datos que exportar",
                                                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    break;
                                case 2:
                                    DialogResult respuesta2 = MessageBox.Show("¿Deseas exportar a pdf todas las facturas procesadas con detalles de materiales del rango indicado?.",
                                                           "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta2 == DialogResult.Yes)
                                    {
                                        var result = DB.SPFactPorRangoFechaDetalles(FechaInicial, FechaFinal).ToList();
                                        if (result.Count > 0)
                                        {
                                            RptFactFilteredDetalle rptFactFilteredDetalle = new RptFactFilteredDetalle();
                                            rptFactFilteredDetalle.SetParameterValue("@fechaInicio", pFechaInicial);
                                            rptFactFilteredDetalle.SetParameterValue("@fechaFin", pFechaFinal);

                                            //datos para export pdf el report
                                            ExportOptions exportOptions;
                                            DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                                            SaveFileDialog dialog = new SaveFileDialog();
                                            dialog.Filter = "Pdf|*.pdf";
                                            if (dialog.ShowDialog() == DialogResult.OK)
                                            {
                                                diskFileDestinationOptions.DiskFileName = dialog.FileName;
                                            }
                                            Cursor.Current = Cursors.WaitCursor;
                                            exportOptions = rptFactFilteredDetalle.ExportOptions;
                                            {
                                                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                                exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                                exportOptions.ExportDestinationOptions = diskFileDestinationOptions;
                                                exportOptions.ExportFormatOptions = new PdfRtfWordFormatOptions();
                                            }
                                            rptFactFilteredDetalle.Export();
                                            Cursor.Current = Cursors.Default;
                                            MessageBox.Show("Se exporto correctamente el documento.",
                                                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            MessageBox.Show("No hay datos que exportar",
                                                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    break;
                                case 3:
                                    DialogResult respuesta3 = MessageBox.Show("¿Deseas exportar a pdf todas las facturas procesadas del rango indicado?.",
                                                           "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta3 == DialogResult.Yes)
                                    {
                                        var result = DB.SPFactPorRangoFechaAll(FechaInicial, FechaFinal).ToList();
                                        if (result.Count > 0)
                                        {
                                            RptPrintFactFilterAll rptPrintFactFilterAll = new RptPrintFactFilterAll();
                                            rptPrintFactFilterAll.SetParameterValue("@fechaInicio", pFechaInicial);
                                            rptPrintFactFilterAll.SetParameterValue("@fechaFin", pFechaFinal);

                                            //datos para export pdf el report
                                            ExportOptions exportOptions;
                                            DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                                            SaveFileDialog dialog = new SaveFileDialog();
                                            dialog.Filter = "Pdf|*.pdf";
                                            if (dialog.ShowDialog() == DialogResult.OK)
                                            {
                                                diskFileDestinationOptions.DiskFileName = dialog.FileName;
                                            }
                                            Cursor.Current = Cursors.WaitCursor;
                                            exportOptions = rptPrintFactFilterAll.ExportOptions;
                                            {
                                                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                                exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                                exportOptions.ExportDestinationOptions = diskFileDestinationOptions;
                                                exportOptions.ExportFormatOptions = new PdfRtfWordFormatOptions();
                                            }
                                            rptPrintFactFilterAll.Export();
                                            Cursor.Current = Cursors.Default;
                                            MessageBox.Show("Se exporto correctamente el documento.",
                                                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            MessageBox.Show("No hay datos que exportar",
                                                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    break;
                                default:
                                    MessageBox.Show("Debes de seleccionar un filtro para buscar las facturas, si ya lo seleccionaste, " +
                                        "entonces ha ocurrido un error, favor contactar al administrador.",
                                  "Registro de Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Selecciona correctamente las fecha para filtrar la información.",
                                                           "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (RbPendientes.Checked)
                    {
                        try
                        {
                            switch (valorPendiente)
                            {
                                case 0:
                                    MessageBox.Show("Debes de seleccionar un filtro para buscar las facturas.",
                                  "Registro de Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                case 1:
                                    DialogResult respuesta1 = MessageBox.Show("¿Deseas exportar a pdf, todas las facturas a crédito de solo backhoe y transporte?.",
                                                            "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta1 == DialogResult.Yes)
                                    {
                                        var result = DB.SPFactPendSinDetalle().ToList();
                                        if (result.Count > 0)
                                        {
                                            RptFactPendSinD rptFactPendSinD = new RptFactPendSinD();

                                            //datos para export pdf el report
                                            ExportOptions exportOptions;
                                            DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                                            SaveFileDialog dialog = new SaveFileDialog();
                                            dialog.Filter = "Pdf|*.pdf";
                                            if (dialog.ShowDialog() == DialogResult.OK)
                                            {
                                                diskFileDestinationOptions.DiskFileName = dialog.FileName;
                                            }
                                            Cursor.Current = Cursors.WaitCursor;
                                            exportOptions = rptFactPendSinD.ExportOptions;
                                            {
                                                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                                exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                                exportOptions.ExportDestinationOptions = diskFileDestinationOptions;
                                                exportOptions.ExportFormatOptions = new PdfRtfWordFormatOptions();
                                            }
                                            rptFactPendSinD.Export();
                                            Cursor.Current = Cursors.Default;
                                            MessageBox.Show("Se exporto correctamente el documento.",
                                                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            MessageBox.Show("No hay datos que exportar",
                                                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    break;
                                case 2:
                                    DialogResult respuesta2 = MessageBox.Show("¿Deseas exportar a pdf todas las facturas a crédito con detalles de materiales?.",
                                                           "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta2 == DialogResult.Yes)
                                    {
                                        var result = DB.SPFactPendDetalles().ToList();
                                        if (result.Count > 0)
                                        {
                                            RptFactPendConD rptFactPendConD = new RptFactPendConD();

                                            //datos para export pdf el report
                                            ExportOptions exportOptions;
                                            DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                                            SaveFileDialog dialog = new SaveFileDialog();
                                            dialog.Filter = "Pdf|*.pdf";
                                            if (dialog.ShowDialog() == DialogResult.OK)
                                            {
                                                diskFileDestinationOptions.DiskFileName = dialog.FileName;
                                            }
                                            Cursor.Current = Cursors.WaitCursor;
                                            exportOptions = rptFactPendConD.ExportOptions;
                                            {
                                                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                                exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                                exportOptions.ExportDestinationOptions = diskFileDestinationOptions;
                                                exportOptions.ExportFormatOptions = new PdfRtfWordFormatOptions();
                                            }
                                            rptFactPendConD.Export();
                                            Cursor.Current = Cursors.Default;
                                            MessageBox.Show("Se exporto correctamente el documento.",
                                                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            MessageBox.Show("No hay datos que exportar",
                                                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    break;
                                case 3:
                                    DialogResult respuesta3 = MessageBox.Show("¿Deseas exportar a pdf todas las facturas a crédito?.",
                                                           "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta3 == DialogResult.Yes)
                                    {
                                        var result = DB.SPFactPendAll().ToList();
                                        if (result.Count > 0)
                                        {
                                            RptFactPendAll rptFactPendAll = new RptFactPendAll();

                                            //datos para export pdf el report
                                            ExportOptions exportOptions;
                                            DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                                            SaveFileDialog dialog = new SaveFileDialog();
                                            dialog.Filter = "Pdf|*.pdf";
                                            if (dialog.ShowDialog() == DialogResult.OK)
                                            {
                                                diskFileDestinationOptions.DiskFileName = dialog.FileName;
                                            }
                                            Cursor.Current = Cursors.WaitCursor;
                                            exportOptions = rptFactPendAll.ExportOptions;
                                            {
                                                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                                exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                                exportOptions.ExportDestinationOptions = diskFileDestinationOptions;
                                                exportOptions.ExportFormatOptions = new PdfRtfWordFormatOptions();
                                            }
                                            rptFactPendAll.Export();
                                            Cursor.Current = Cursors.Default;

                                            MessageBox.Show("Se exporto correctamente el documento.",
                                                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            MessageBox.Show("No hay datos que exportar",
                                                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    break;
                                default:
                                    MessageBox.Show("Debes de seleccionar un filtro para buscar las facturas, si ya lo seleccionaste, " +
                                        "entonces ha ocurrido un error, favor contactar al administrador.",
                                  "Registro de Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    else
                    {
                        if (RbAnuladas.Checked)
                        {
                            try
                            {
                                if (ValidarFechaLimite2())
                                {
                                    DialogResult respuesta1 = MessageBox.Show("¿Deseas exportar a pdf, todas las facturas anuladas / reversadas / notas de crédito?.",
                                                                                               "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta1 == DialogResult.Yes)
                                    {
                                        var result = DB.SPFactReversadasAll(FechaInicial2, FechaFinal2).ToList();
                                        if (result.Count > 0)
                                        {
                                            RptFactsAnuladas rptFactsAnuladas = new RptFactsAnuladas();
                                            rptFactsAnuladas.SetParameterValue("@fechaInicio", pFechaInicial2);
                                            rptFactsAnuladas.SetParameterValue("@fechaFin", pFechaFinal2);

                                            //datos para export pdf el report
                                            ExportOptions exportOptions;
                                            DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                                            SaveFileDialog dialog = new SaveFileDialog();
                                            dialog.Filter = "Pdf|*.pdf";
                                            if (dialog.ShowDialog() == DialogResult.OK)
                                            {
                                                diskFileDestinationOptions.DiskFileName = dialog.FileName;
                                            }
                                            Cursor.Current = Cursors.WaitCursor;
                                            exportOptions = rptFactsAnuladas.ExportOptions;
                                            {
                                                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                                exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                                exportOptions.ExportDestinationOptions = diskFileDestinationOptions;
                                                exportOptions.ExportFormatOptions = new PdfRtfWordFormatOptions();
                                            }
                                            rptFactsAnuladas.Export();
                                            Cursor.Current = Cursors.Default;
                                            MessageBox.Show("Se exporto correctamente el documento.",
                                                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            MessageBox.Show("No hay datos que exportar",
                                                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }
                    }
                }
            }
        }

        private void btnCredSinLineas_Click(object sender, EventArgs e)
        {
            dgvFilter.ClearSelection();
            Consecutivo = 0;
            BtnVerFact.Visible = false;
            // linq para validar y disenar mejor la DataGridView al usuario
            //se llama el procedimiento Almacenado
            var result = DB.SPFactPendSinDetalle().ToList();

            var finalResult =   from fa in result
                                 select new
                                 {
                                     fa.IdFactura,
                                     fa.Consecutivo,
                                     fa.FechaFactura,
                                     fa.CostoTotal,
                                     fa.NombreEstado,
                                     fa.Nombre,
                                     fa.NombreEmpleado
                                 };

            finalResult = finalResult.Distinct();

            dgvFilter.DataSource = finalResult.ToList();
            if (finalResult.ToList().Count > 0)
            {
                BtnVerFacturasList.Visible = true;
                btnReportExcel.Visible = true;
                btnReportPDF.Visible = true;
                valorPendiente = 1;

                //para que se muestre el total (monto) de la consulta
                decimal valor = 0;
                foreach (var item in finalResult)
                {
                    valor += item.CostoTotal;
                }
                lblTotalMont.Text = string.Format("¢ {0:N2}", valor);
            }
            else
            {
                MessageBox.Show("No hay registro de facturas a crédito", "Lista Facturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnVerFacturasList.Visible = false;
                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;
                valorPendiente = 0;

               
                lblTotalMont.Text = string.Format("¢ {0:N2}", 0);
            }
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
        }
        private void btnFiltrarHoyTodas_Click(object sender, EventArgs e)
        {
            dgvFilter.ClearSelection();
            Consecutivo = 0;
            BtnVerFact.Visible = false;
        
            DateTime FechaInicial = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            DateTime FechaFinal = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));

            // linq para validar y disenar mejor la DataGridView al usuario

            var result = DB.SPFactPorRangoFechaAll(FechaInicial, FechaFinal).ToList();

                
            var finalResult = from fa in result
                                select new
                                {
                                    fa.IdFactura,
                                    fa.Consecutivo,
                                    fa.FechaFactura,
                                    fa.CostoTotal,
                                    fa.NombreEstado,
                                    fa.Nombre,
                                    fa.NombreEmpleado
                                };

            finalResult = finalResult.Distinct();

            dgvFilter.DataSource = finalResult.ToList();
            if (result.ToList().Count > 0)
            {
                BtnVerFacturasList.Visible = true;
                btnReportExcel.Visible = true;
                btnReportPDF.Visible = true;
                valorHoy = 3;

                //para que se muestre el total (monto) de la consulta
                decimal valor = 0;
                foreach (var item in finalResult)
                {
                    valor += item.CostoTotal;
                }
                lblTotalMont.Text = string.Format("¢ {0:N2}", valor);
            }
            else
            {
                MessageBox.Show("No hay registro de facturas el día de hoy, puedes revisar las de crédito", "Lista Facturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnVerFacturasList.Visible = false;
                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;
                valorHoy = 0;

                lblTotalMont.Text = string.Format("¢ {0:N2}", 0);
            }
            
        }

        private void btnCreditoTodas_Click(object sender, EventArgs e)
        {
            dgvFilter.ClearSelection();
            Consecutivo = 0;
            BtnVerFact.Visible = false;
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
                                  fa.NombreEmpleado
                              };

            finalResult = finalResult.Distinct();

            dgvFilter.DataSource = finalResult.ToList();
            if (finalResult.ToList().Count > 0)
            {
                BtnVerFacturasList.Visible = true;
                btnReportExcel.Visible = true;
                btnReportPDF.Visible = true;
                valorPendiente = 3; // indica la accion a realizar para ver las facturas list

                //para que se muestre el total (monto) de la consulta
                decimal valor = 0;
                foreach (var item in finalResult)
                {
                    valor += item.CostoTotal;
                }
                lblTotalMont.Text = string.Format("¢ {0:N2}", valor);
            }
            else
            {
                MessageBox.Show("No hay registro de facturas a crédito", "Lista Facturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnVerFacturasList.Visible = false;
                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;
                valorPendiente = 0;

                
                lblTotalMont.Text = string.Format("¢ {0:N2}", 0);
            }
        }

        private void btnAnuladas_Click(object sender, EventArgs e)
        {
            dgvFilter.ClearSelection();
            Consecutivo = 0;
            BtnVerFact.Visible = false;
            // linq para validar y disenar mejor la DataGridView al usuario
            //se llama el procedimiento Almacenado

            if (ValidarFechaLimite2())
            {
                DateTime FechaInicial = Convert.ToDateTime(DateInicio2.Value.ToString("yyyy-MM-dd"));
                DateTime FechaFinal = Convert.ToDateTime(DateFin2.Value.ToString("yyyy-MM-dd"));


                var result = DB.SPFactReversadasAll(FechaInicial, FechaFinal).ToList();

                var finalResult = from fa in result
                                  select new
                                  {
                                      fa.IdFactura,
                                      fa.Consecutivo,
                                      fa.FechaFactura,
                                      fa.CostoTotal,
                                      fa.NombreEstado,
                                      fa.Nombre,
                                      fa.NombreEmpleado
                                  };

                finalResult = finalResult.Distinct();

                dgvFilter.DataSource = finalResult.ToList();
                if (finalResult.ToList().Count > 0)
                {
                    BtnVerFacturasList.Visible = true;
                    btnReportExcel.Visible = true;
                    btnReportPDF.Visible = true;

                    //para que se muestre el total (monto) de la consulta
                    decimal valor = 0;
                    foreach (var item in finalResult)
                    {
                        valor += item.CostoTotal;
                    }
                    lblTotalMont.Text = string.Format("¢ {0:N2}", valor);
                }
                else
                {
                    MessageBox.Show("No hay registro de facturas a anuladas o con nota de crédito.", "Lista Facturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BtnVerFacturasList.Visible = false;
                    btnReportExcel.Visible = false;
                    btnReportPDF.Visible = false;

                   
                    lblTotalMont.Text = string.Format("¢ {0:N2}", 0);
                }
            }
            else
            {
                MessageBox.Show("Selecciona correctamente las fecha para filtrar la información.",
                                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFiltrarFechaBachHoe_Click(object sender, EventArgs e)
        {
            dgvFilter.ClearSelection();
            Consecutivo = 0;
            BtnVerFact.Visible = false;
            if (ValidarFechaLimite())
            {
                DateTime FechaInicial = Convert.ToDateTime(DateInicio.Value.ToString("yyyy-MM-dd"));
                DateTime FechaFinal = Convert.ToDateTime(DateFin.Value.ToString("yyyy-MM-dd"));

                // linq para validar y disenar mejor la DataGridView al usuario

                var result = DB.SPFactPorRangoFechaSinDetalles(FechaInicial, FechaFinal).ToList();

                var finalResult = from fa in result
                                  select new
                                  {
                                      fa.IdFactura,
                                      fa.Consecutivo,
                                      fa.FechaFactura,
                                      fa.CostoTotal,
                                      fa.NombreEstado,
                                      fa.Nombre,
                                      fa.NombreEmpleado
                                  };

                finalResult = finalResult.Distinct();

                dgvFilter.DataSource = finalResult.ToList();
                if (result.ToList().Count > 0)
                {
                    BtnVerFacturasList.Visible = true;
                    btnReportExcel.Visible = true;
                    btnReportPDF.Visible = true;
                    valorPorFechas = 1;

                    //para que se muestre el total (monto) de la consulta
                    decimal valor = 0;
                    foreach (var item in finalResult)
                    {
                        valor += item.CostoTotal;
                    }
                    lblTotalMont.Text = string.Format("¢ {0:N2}", valor);
                }
                else
                {
                    MessageBox.Show("No hay registro de facturas en el rango de fechas indicado, puedes revisar las de crédito", "Lista Facturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BtnVerFacturasList.Visible = false;
                    btnReportExcel.Visible = false;
                    btnReportPDF.Visible = false;
                    valorPorFechas = 0;
                    
                    lblTotalMont.Text = string.Format("¢ {0:N2}", 0);
                }
            }
            else
            {
                MessageBox.Show("Selecciona correctamente las fecha para filtrar la información.",
                                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFiltrarTodasVentas_Click(object sender, EventArgs e)
        {
            dgvFilter.ClearSelection();
            Consecutivo = 0;
            BtnVerFact.Visible = false;
            if (ValidarFechaLimite())
            {
                DateTime FechaInicial = Convert.ToDateTime(DateInicio.Value.ToString("yyyy-MM-dd"));
                DateTime FechaFinal = Convert.ToDateTime(DateFin.Value.ToString("yyyy-MM-dd"));

                // linq para validar y disenar mejor la DataGridView al usuario

                var result = DB.SPFactPorRangoFechaAll(FechaInicial, FechaFinal).ToList();

                var finalResult = from fa in result
                                  select new
                                  {
                                      fa.IdFactura,
                                      fa.Consecutivo,
                                      fa.FechaFactura,
                                      fa.CostoTotal,
                                      fa.NombreEstado,
                                      fa.Nombre,
                                      fa.NombreEmpleado
                                  };
                finalResult = finalResult.Distinct();

                dgvFilter.DataSource = finalResult.ToList();
                if (result.ToList().Count > 0)
                {
                    BtnVerFacturasList.Visible = true;
                    btnReportExcel.Visible = true;
                    btnReportPDF.Visible = true;
                    valorPorFechas = 3;

                    //para que se muestre el total (monto) de la consulta
                    decimal valor = 0;
                    foreach (var item in finalResult)
                    {
                        valor += item.CostoTotal;
                    }
                    lblTotalMont.Text = string.Format("¢ {0:N2}", valor);
                }
                else
                {
                    MessageBox.Show("No hay registro de facturas en el rango de fechas indicado, puedes revisar las de crédito", "Lista Facturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BtnVerFacturasList.Visible = false;
                    btnReportExcel.Visible = false;
                    btnReportPDF.Visible = false;
                    valorPorFechas = 0;

                    lblTotalMont.Text = string.Format("¢ {0:N2}", 0);
                }
            }
            else
            {
                MessageBox.Show("Selecciona correctamente las fecha para filtrar la información.",
                                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFiltrarHoySoloBackHoe_Click(object sender, EventArgs e)
        {
            dgvFilter.ClearSelection();
            Consecutivo = 0;
            BtnVerFact.Visible = false;

            DateTime FechaInicial = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            DateTime FechaFinal = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));

            // linq para validar y disenar mejor la DataGridView al usuario

            var result = DB.SPFactPorRangoFechaSinDetalles(FechaInicial, FechaFinal).ToList();


            var finalResult = from fa in result
                              select new
                              {
                                  fa.IdFactura,
                                  fa.Consecutivo,
                                  fa.FechaFactura,
                                  fa.CostoTotal,
                                  fa.NombreEstado,
                                  fa.Nombre,
                                  fa.NombreEmpleado
                              };

            finalResult = finalResult.Distinct();
            dgvFilter.DataSource = finalResult.ToList();


            if (result.ToList().Count > 0)
            {
                BtnVerFacturasList.Visible = true;
                btnReportExcel.Visible = true;
                btnReportPDF.Visible = true;
                valorHoy = 1;

                //para que se muestre el total (monto) de la consulta
                decimal valor = 0;
                foreach (var item in finalResult)
                {
                    valor += item.CostoTotal;
                }
                lblTotalMont.Text = string.Format("¢ {0:N2}", valor);
            }
            else
            {
                MessageBox.Show("No hay registro de facturas el día de hoy, puedes revisar las de crédito", "Lista Facturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnVerFacturasList.Visible = false;
                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;
                valorHoy = 0;

                lblTotalMont.Text = string.Format("¢ {0:N2}", 0);
            }
        }
    }
}
