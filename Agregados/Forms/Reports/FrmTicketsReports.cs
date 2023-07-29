using Agregados.Reports.Facts.FactNow;
using Agregados.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Agregados.Reports.Facts.FactFiltered;
using Agregados.Reports.Facts.TicketFiltered;
using Ganss.Excel;
using CrystalDecisions.Shared;

namespace Agregados.Forms.Reports
{
    public partial class FrmTicketsReports : Form
    {

        //variables del form
        AgregadosEntities DB;
        Facturas facturas;
        int Consecutivo;
        int Id;

        int valorPendiente = 0; //acciones de credito

        int valorPorFechas = 0; //acciones de rango de fechas

        int valorHoy = 0; //acciones de rango de fechas


        public FrmTicketsReports()
        {
            InitializeComponent();
            DB = new AgregadosEntities();
            facturas = new Facturas();
            valorPendiente = 0;
            valorPorFechas = 0;
            valorHoy = 0;
        }


        private void FrmTicketsReports_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
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

        //para rango de fechas 2
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


        private void FrmTicketsReports_Load(object sender, EventArgs e)
        {

            btnFiltrarHoyTodas.Visible = false;

  
            btnFiltrarTodasVentas.Visible = false;
            DateInicio.Visible = false;
            DateFin.Visible = false;

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
                         join pr in DB.Proveedores on fa.IdProveedor equals pr.IdProveedor
                         join us in DB.Usuarios on fa.IdUsuario equals us.IdUsuario
                         where ((fa.IdEstado == 4) || (fa.IdEstado == 3) && fa.IdProveedor > 0 && (fa.FechaFactura >= fechaAnterior && fa.FechaFactura <= fechaActual))
                         select new
                         {
                             fa.Consecutivo,
                             fa.FechaFactura,
                             fa.CostoTotal,
                             es.NombreEstado,
                             pr.Nombre,
                             us.NombreEmpleado
                         };

            if (result.ToList().Count > 0)
            {
                dgvFilter.DataSource = result.ToList();
                MessageBox.Show("Lista de los últimos 3 días", "Lista Compras", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No hay registro de Compras en los últimos 3 días", "Lista Compras", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RbHoy_CheckedChanged(object sender, EventArgs e)
        {
            if (RbHoy.Checked)
            {
               
                btnFiltrarHoyTodas.Visible = true;

               
                btnFiltrarTodasVentas.Visible = false;
                DateInicio.Visible = false;
                DateFin.Visible = false;


               
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

        private void RbFechas_CheckedChanged(object sender, EventArgs e)
        {
            if (RbFechas.Checked)
            {
             
                btnFiltrarHoyTodas.Visible = false;

            
                btnFiltrarTodasVentas.Visible = true;
                DateInicio.Visible = true;
                DateFin.Visible = true;


            
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

        private void RbPendientes_CheckedChanged(object sender, EventArgs e)
        {
            if (RbPendientes.Checked)
            {
         
                btnFiltrarHoyTodas.Visible = false;

                btnFiltrarTodasVentas.Visible = false;
                DateInicio.Visible = false;
                DateFin.Visible = false;

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
  
                btnFiltrarHoyTodas.Visible = false;

                btnFiltrarTodasVentas.Visible = false;
                DateInicio.Visible = false;
                DateFin.Visible = false;

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

        private void btnFiltrarHoyTodas_Click(object sender, EventArgs e)
        {
            dgvFilter.ClearSelection();
            Consecutivo = 0;
            BtnVerFact.Visible = false;

            DateTime FechaInicial = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            DateTime FechaFinal = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));

            // linq para validar y disenar mejor la DataGridView al usuario

            var result = DB.SPTicketPorRangoFechaAll(FechaInicial, FechaFinal).ToList();


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
            }
            else
            {
                MessageBox.Show("No hay registro de compras a contado el día de hoy, puedes revisar las de crédito", "Lista Compras", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnVerFacturasList.Visible = false;
                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;
                valorHoy = 0;
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

                var result = DB.SPTicketPorRangoFechaAll(FechaInicial, FechaFinal).ToList();

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
                }
                else
                {
                    MessageBox.Show("No hay registro de compras en el rango de fechas indicado", "Lista Compras", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BtnVerFacturasList.Visible = false;
                    btnReportExcel.Visible = false;
                    btnReportPDF.Visible = false;
                    valorPorFechas = 0;
                }
            }
            else
            {
                MessageBox.Show("Selecciona correctamente las fecha para filtrar la información.",
                                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreditoTodas_Click(object sender, EventArgs e)
        {
            dgvFilter.ClearSelection();
            Consecutivo = 0;
            BtnVerFact.Visible = false;
            // linq para validar y disenar mejor la DataGridView al usuario
            //se llama el procedimiento Almacenado
            var result = DB.SPTicketPendAll().ToList();

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
                valorPendiente = 1; // indica la accion a realizar para ver las facturas list
            }
            else
            {
                MessageBox.Show("No hay registro de compras a crédito pendiente por pagar", "Lista Compras", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnVerFacturasList.Visible = false;
                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;
                valorPendiente = 0;
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


                var result = DB.SPTicketReversadasAll(FechaInicial, FechaFinal).ToList();

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
                }
                else
                {
                    MessageBox.Show("No hay registro de compras reversada / anuladas pendiente por pagar", "Lista Compras", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BtnVerFacturasList.Visible = false;
                    btnReportExcel.Visible = false;
                    btnReportPDF.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Selecciona correctamente las fecha para filtrar la información.",
                                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    BtnVerFact.Visible = true;
                }
            }
            else
            {
                Consecutivo = 0;
                Id = 0;
            }
        }

        //boton que muestra el reporte de la factura 
        private void BtnVerFact_Click(object sender, EventArgs e)
        {
            if (Consecutivo > 0)
            {
                facturas = DB.Facturas.Find(Id);
                if (facturas.IdEstado == 5 || facturas.IdEstado == 12 || facturas.IdEstado == 13)
                {
                    using (FrmPrintTicketRev frm = new FrmPrintTicketRev(Consecutivo))
                    {
                        frm.ShowDialog();
                    };
                }
                else
                {
                    using (FrmPrintTicket frm = new FrmPrintTicket(Consecutivo))
                    {
                        frm.ShowDialog();
                    };
                }

            }
            else
            {
                MessageBox.Show("Debes de seleccionar una Compra para poder verla.",
                                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVerFacturasList_Click(object sender, EventArgs e)
        {
            string FechaInicial = Convert.ToString(DateInicio.Value.ToString("yyyy-MM-dd"));
            string FechaFinal = Convert.ToString(DateFin.Value.ToString("yyyy-MM-dd"));

            string FechaInicial2 = Convert.ToString(DateInicio2.Value.ToString("yyyy-MM-dd"));
            string FechaFinal2 = Convert.ToString(DateFin2.Value.ToString("yyyy-MM-dd"));

            string Hoy = Convert.ToString(DateTime.Today.ToString("yyyy-MM-dd"));

            //anuladas reversada
            if (RbAnuladas.Checked)
            {
                DialogResult respuesta = MessageBox.Show("¿Deseas visualizar todas las compras anuladas o reversadas en el rango de fechas indicado?.",
                                                "Registro de Compras", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    using (FrmTicketAnuladas frm = new FrmTicketAnuladas(FechaInicial2, FechaFinal2))
                    {
                        frm.ShowDialog();
                    };
                }
            }

            //pendiente por credito
            if (RbPendientes.Checked)
            {
                switch (valorPendiente)
                {
                    case 0:
                        MessageBox.Show("Debes de seleccionar un filtro para buscar las Compras.",
                      "Registro de Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 1:
                        DialogResult respuesta1 = MessageBox.Show("¿Deseas visualizar todas las compras pendientes por pagar?.",
                                                "Registro de Compras", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta1 == DialogResult.Yes)
                        {
                            using (FrmTicketPendAll frm = new FrmTicketPendAll())
                            {
                                frm.ShowDialog();
                            };
                        }
                        break;
                    default:
                        MessageBox.Show("Debes de seleccionar un filtro para buscar las Compras, si ya lo seleccionaste, " +
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
                        MessageBox.Show("Debes de seleccionar un filtro para buscar las compras.",
                      "Registro de Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 1:
                        DialogResult respuesta1 = MessageBox.Show("¿Deseas visualizar todas las compras procesadas?.",
                                                "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta1 == DialogResult.Yes)
                        {
                            using (FrmTicketPorFechas frm = new FrmTicketPorFechas(FechaInicial, FechaFinal))
                            {
                                frm.ShowDialog();
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

            //hoy
            if (RbHoy.Checked)
            {
                switch (valorHoy)
                {
                    case 0:
                        MessageBox.Show("Debes de seleccionar un filtro para buscar las compras.",
                      "Registro de Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 1:
                        DialogResult respuesta1 = MessageBox.Show("¿Deseas visualizar todas las compras procesadas?.",
                                                "Registro de Facturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta1 == DialogResult.Yes)
                        {
                            using (FrmTicketPorFechas frm = new FrmTicketPorFechas(Hoy, Hoy))
                            {
                                frm.ShowDialog();
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
                            DialogResult respuesta = MessageBox.Show("¿Deseas exportar a excel todas las compras procesadas de hoy?.",
                                                   "Registro de Compras", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (respuesta == DialogResult.Yes)
                            {
                                var result = DB.SPTicketPorRangoFechaAll(Inicial, Final).ToList();

                                if (result.Count > 0)
                                {
                                    SaveFileDialog saveFileDialog = new SaveFileDialog
                                    {
                                        Filter = "Excel|*.xlsx"
                                    };

                                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                    {
                                        ExcelMapper mapper = new ExcelMapper();
                                        var file = saveFileDialog.FileName;
                                        mapper.Save(file, result, $"ReportCompras", true); //true is for saving .xlsx
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
                                    DialogResult respuesta3 = MessageBox.Show("¿Deseas exportar a excel todas las compras procesadas del rango indicado?.",
                                                           "Registro de Compras", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta3 == DialogResult.Yes)
                                    {
                                        var result = DB.SPTicketPorRangoFechaAll(FechaInicial, FechaFinal).ToList();

                                        if (result.Count > 0)
                                        {
                                            SaveFileDialog saveFileDialog = new SaveFileDialog
                                            {
                                                Filter = "Excel|*.xlsx"
                                            };

                                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                            {
                                                ExcelMapper mapper = new ExcelMapper();
                                                var file = saveFileDialog.FileName;
                                                mapper.Save(file, result, "ReportCompras", true); //true is for saving .xlsx
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
                                    MessageBox.Show("Debes de seleccionar un filtro para buscar las Compras.",
                                  "Registro de Compras", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                case 1:
                                    DialogResult respuesta3 = MessageBox.Show("¿Deseas exportar a excel todas las Compras a crédito?.",
                                                           "Registro de Compras", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta3 == DialogResult.Yes)
                                    {
                                        var result = DB.SPTicketPendAll().ToList();

                                        if (result.Count > 0)
                                        {
                                            SaveFileDialog saveFileDialog = new SaveFileDialog
                                            {
                                                Filter = "Excel|*.xlsx"
                                            };

                                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                            {
                                                ExcelMapper mapper = new ExcelMapper();
                                                var file = saveFileDialog.FileName;
                                                mapper.Save(file, result, "ReportComprasCredito", true); //true is for saving .xlsx
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
                                    DialogResult respuesta1 = MessageBox.Show("¿Deseas exportar a excel, todas las Compras anuladas / reversadas?.",
                                                                                           "Registro de Compras", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta1 == DialogResult.Yes)
                                    {
                                        var result = DB.SPTicketReversadasAll(FechaInicial2, FechaFinal2).ToList();

                                        if (result.Count > 0)
                                        {
                                            SaveFileDialog saveFileDialog = new SaveFileDialog
                                            {
                                                Filter = "Excel|*.xlsx"
                                            };

                                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                            {
                                                ExcelMapper mapper = new ExcelMapper();
                                                var file = saveFileDialog.FileName;
                                                mapper.Save(file, result, "ReportComprasAnuladas", true); //true is for saving .xlsx
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
                            MessageBox.Show("Debes de seleccionar un filtro para buscar las Compras.",
                          "Registro de Compras", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 1:
                            DialogResult respuesta = MessageBox.Show("¿Deseas exportar a pdf todas las Compras procesadas de hoy?.",
                                                   "Registro de Compras", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (respuesta == DialogResult.Yes)
                            {
                                var result = DB.SPTicketPorRangoFechaAll(Inicial, Final).ToList();
                                if (result.Count > 0)
                                {
                                    RptTickesPorFechas rptTickesPorFechas = new RptTickesPorFechas();
                                    rptTickesPorFechas.SetParameterValue("@fechaInicio", pInicial2);
                                    rptTickesPorFechas.SetParameterValue("@fechaFin", pFinal2);

                                    //datos para export pdf el report
                                    ExportOptions exportOptions;
                                    DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                                    SaveFileDialog dialog = new SaveFileDialog();
                                    dialog.Filter = "Pdf|*.pdf";
                                    if (dialog.ShowDialog() == DialogResult.OK)
                                    {
                                        diskFileDestinationOptions.DiskFileName = dialog.FileName;
                                    }
                                    exportOptions = rptTickesPorFechas.ExportOptions;
                                    {
                                        exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exportOptions.ExportDestinationOptions = diskFileDestinationOptions;
                                        exportOptions.ExportFormatOptions = new PdfRtfWordFormatOptions();
                                    }
                                    rptTickesPorFechas.Export();

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
                                    MessageBox.Show("Debes de seleccionar un filtro para buscar las Compras.",
                                  "Registro de Compras", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                case 1:
                                    DialogResult respuesta3 = MessageBox.Show("¿Deseas exportar a pdf todas las Compras procesadas del rango indicado?.",
                                                           "Registro de Compras", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta3 == DialogResult.Yes)
                                    {
                                        var result = DB.SPTicketPorRangoFechaAll(FechaInicial, FechaFinal).ToList();
                                        if (result.Count > 0)
                                        {
                                            RptTickesPorFechas rptTickesPorFechas = new RptTickesPorFechas();
                                            rptTickesPorFechas.Refresh();
                                            rptTickesPorFechas.SetParameterValue("@fechaInicio", pFechaInicial);
                                            rptTickesPorFechas.SetParameterValue("@fechaFin", pFechaFinal);

                                            //datos para export pdf el report
                                            ExportOptions exportOptions;
                                            DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                                            SaveFileDialog dialog = new SaveFileDialog();
                                            dialog.Filter = "Pdf|*.pdf";
                                            if (dialog.ShowDialog() == DialogResult.OK)
                                            {
                                                diskFileDestinationOptions.DiskFileName = dialog.FileName;
                                            }
                                            exportOptions = rptTickesPorFechas.ExportOptions;
                                            {
                                                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                                exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                                exportOptions.ExportDestinationOptions = diskFileDestinationOptions;
                                                exportOptions.ExportFormatOptions = new PdfRtfWordFormatOptions();
                                            }
                                            rptTickesPorFechas.Export();

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
                                    MessageBox.Show("Debes de seleccionar un filtro para buscar las Compras.",
                                  "Registro de Compras", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                case 1:
                                    DialogResult respuesta = MessageBox.Show("¿Deseas exportar a pdf todas las Compras a crédito?.",
                                                           "Registro de Compras", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta == DialogResult.Yes)
                                    {
                                        var result = DB.SPTicketPendAll().ToList();
                                        if (result.Count > 0)
                                        {
                                            RptTicketPendAll rptTicketPendAll = new RptTicketPendAll();
                                            rptTicketPendAll.Refresh();

                                            //datos para export pdf el report
                                            ExportOptions exportOptions;
                                            DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                                            SaveFileDialog dialog = new SaveFileDialog();
                                            dialog.Filter = "Pdf|*.pdf";
                                            if (dialog.ShowDialog() == DialogResult.OK)
                                            {
                                                diskFileDestinationOptions.DiskFileName = dialog.FileName;
                                            }
                                            exportOptions = rptTicketPendAll.ExportOptions;
                                            {
                                                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                                exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                                exportOptions.ExportDestinationOptions = diskFileDestinationOptions;
                                                exportOptions.ExportFormatOptions = new PdfRtfWordFormatOptions();
                                            }
                                            rptTicketPendAll.Export();

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
                                    DialogResult respuesta1 = MessageBox.Show("¿Deseas exportar a pdf, todas las compras anuladas / reversadas?.",
                                                                                               "Registro de Compras", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta1 == DialogResult.Yes)
                                    {
                                        var result = DB.SPTicketReversadasAll(FechaInicial2, FechaFinal2).ToList();
                                        if (result.Count > 0)
                                        {
                                            RptTicketAnuladasAll rptTicketAnuladasAll = new RptTicketAnuladasAll();
                                            rptTicketAnuladasAll.Refresh();
                                            rptTicketAnuladasAll.SetParameterValue("@fechaInicio", pFechaInicial2);
                                            rptTicketAnuladasAll.SetParameterValue("@fechaFin", pFechaFinal2);

                                            //datos para export pdf el report
                                            ExportOptions exportOptions;
                                            DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                                            SaveFileDialog dialog = new SaveFileDialog();
                                            dialog.Filter = "Pdf|*.pdf";
                                            if (dialog.ShowDialog() == DialogResult.OK)
                                            {
                                                diskFileDestinationOptions.DiskFileName = dialog.FileName;
                                            }
                                            exportOptions = rptTicketAnuladasAll.ExportOptions;
                                            {
                                                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                                exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                                exportOptions.ExportDestinationOptions = diskFileDestinationOptions;
                                                exportOptions.ExportFormatOptions = new PdfRtfWordFormatOptions();
                                            }
                                            rptTicketAnuladasAll.Export();

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
    }
}
