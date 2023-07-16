using Agregados.Reports;
using Agregados.Reports.Facts.FactFiltered;
using Agregados.Reports.Facts.FactNow;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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


        public FrmFactsReports()
        {
            InitializeComponent();
            DB = new AgregadosEntities();
            facturas = new Facturas();
            valorPendiente = 0;
            valorPorFechas = 0;

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
            btnFiltrarHoyAnuladas.Visible = false;

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
            dgvFilter.DataSource = result.ToList();
            
            BtnVerFacturasList.Visible = false;
            
        }

        //boton de filtrar datos a hoy
        private void btnFiltrarHoy_Click(object sender, EventArgs e)
        {
            dgvFilter.ClearSelection();
            Consecutivo = 0;
            BtnVerFact.Visible = false;
            DateTime fechaActual = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));

            // linq para validar y disenar mejor la DataGridView al usuario
            var result = from fa in DB.Facturas
                         join es in DB.Estados on fa.IdEstado equals es.IdEstado
                         join cl in DB.Clientes on fa.IdCliente equals cl.IdCliente
                         join us in DB.Usuarios on fa.IdUsuario equals us.IdUsuario
                         where ((fa.IdEstado == 4) && fa.IdCliente > 0 && fa.FechaFactura == fechaActual)
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
                BtnVerFacturasList.Visible = true;
                btnReportExcel.Visible = true;
                btnReportPDF.Visible = true;
            }
            else
            {
                BtnVerFacturasList.Visible = false;
                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;
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

                var result = DB.SPFactPorRangoFechaDetalles1(FechaInicial, FechaFinal).ToList();

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
                dgvFilter.DataSource = finalResult.ToList();
                if (result.ToList().Count > 0)
                {
                    BtnVerFacturasList.Visible = true;
                    btnReportExcel.Visible = true;
                    btnReportPDF.Visible = true;
                    valorPorFechas = 2;
                }
                else
                {
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

            dgvFilter.DataSource = finalResult.ToList();
            if (finalResult.ToList().Count > 0)
            {
                BtnVerFacturasList.Visible = true;
                btnReportExcel.Visible = true;
                btnReportPDF.Visible = true;
                valorPendiente = 3;
            }
            else
            {
                BtnVerFacturasList.Visible = false;
                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;
                valorPendiente = 0;
            }
        }


        //cambios de check box
        private void RbHoy_CheckedChanged(object sender, EventArgs e)
        {
            if (RbHoy.Checked)
            {
                btnFiltrarHoy.Visible = true;
                btnFiltrarHoyAnuladas.Visible = true;

                btnFiltrarFechasCorrecta.Visible = false;
                btnFiltrarFechaBachHoe.Visible = false;
                btnFiltrarTodasVentas.Visible = false;
                DateInicio.Visible = false;
                DateFin.Visible = false;


                btnFiltrarCredito.Visible = false;
                btnCredSinLineas.Visible = false;
                btnCreditoTodas.Visible = false;


                BtnVerFacturasList.Visible = false;
                Consecutivo = 0;

                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;

                valorPendiente = 0; //accion a facturas de credito
                valorPorFechas = 0; //accion a facturas de filtro por fechas
                dgvFilter.ClearSelection();
            }
        }

        //cambios de check box
        private void RbFechas_CheckedChanged(object sender, EventArgs e)
        {
            if (RbFechas.Checked)
            {
                btnFiltrarHoy.Visible = false;
                btnFiltrarHoyAnuladas.Visible = false;

                btnFiltrarFechasCorrecta.Visible = true;
                btnFiltrarFechaBachHoe.Visible = true;
                btnFiltrarTodasVentas.Visible = true;
                DateInicio.Visible = true;
                DateFin.Visible = true;


                btnFiltrarCredito.Visible = false;
                btnCredSinLineas.Visible = false;
                btnCreditoTodas.Visible = false;


                BtnVerFacturasList.Visible = false;
                Consecutivo = 0;

                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;


                valorPendiente = 0; //accion a facturas de credito
                valorPorFechas = 0; //accion a facturas de filtro por fechas
                dgvFilter.ClearSelection();
            }
        }

        //cambios de check box
        private void RbPendientes_CheckedChanged(object sender, EventArgs e)
        {
            if (RbPendientes.Checked)
            {
                btnFiltrarHoy.Visible = false;
                btnFiltrarHoyAnuladas.Visible = false;

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
                dgvFilter.ClearSelection();
            }
        }

        private void RbAnuladas_CheckedChanged(object sender, EventArgs e)
        {
            if (RbAnuladas.Checked)
            {
                btnFiltrarHoy.Visible = false;
                btnFiltrarHoyAnuladas.Visible = false;

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
                        frm.ShowDialog();
                    };
                }
                else
                {
                    using (FrmPrintFact frm = new FrmPrintFact(Consecutivo))
                    {
                        frm.ShowDialog();
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
                                frm.ShowDialog();
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
                                frm.ShowDialog();
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

            //pendiente por credito
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
                                frm.ShowDialog();
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
                                frm.ShowDialog();
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



            /*

             if (RbHoy.Checked)
             {

                 DialogResult respuesta = MessageBox.Show("¿Deseas visualizar detalle de facturas de venta de materiales o solo de servicios de backhoe?." 
                     + Environment.NewLine + "'Si', para visualizar con detalles, 'No', para visualizar sin detalles. 'Cancel', para cancelar solicitud.",
                                                 "Registro de Facturas", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                 if (respuesta == DialogResult.Yes)
                 {
                     using (FrmPrintFactFilterDetalle frm = new FrmPrintFactFilterDetalle(Hoy, Hoy))
                     {
                         frm.ShowDialog();
                     };
                 }
                 else
                 {
                     if (respuesta == DialogResult.No)
                     {
                         using (FrmPrintFactFilterSinDetalle frm = new FrmPrintFactFilterSinDetalle(Hoy, Hoy))
                         {
                             frm.ShowDialog();
                         };
                     }
                 }                
             }
             else
             {
                 if (RbFechas.Checked)
                 {
                     if (ValidarFechaLimite())
                     {
                         DialogResult respuesta = MessageBox.Show("¿Deseas visualizar detalle de facturas de venta de materiales o solo de servicios de backhoe?."
                         + Environment.NewLine + "'Si', para visualizar con detalles, 'No', para visualizar sin detalles. 'Cancel', para cancelar solicitud.",
                                               "Registro de Facturas", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                         if (respuesta == DialogResult.Yes)
                         {
                             using (FrmPrintFactFilterDetalle frm = new FrmPrintFactFilterDetalle(FechaInicial, FechaFinal))
                             {
                                 frm.ShowDialog();
                             };
                         }
                         else
                         {
                             if (respuesta == DialogResult.No)
                             {
                                 using (FrmPrintFactFilterSinDetalle frm = new FrmPrintFactFilterSinDetalle(FechaInicial, FechaFinal))
                                 {
                                     frm.ShowDialog();
                                 };
                             }
                         }
                     }
                     else
                     {
                         MessageBox.Show("Selecciona correctamente las fecha para filtrar la información.",
                                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                 }
                 else
                 {
                     if (RbPendientes.Checked)
                     {
                         //TODO REPORT PENDIENTES
                     }
                 }
             }

             */
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

        //export to excel segun el check box elegido, valida que no este en uso el archivo
        private void btnReportExcel_Click(object sender, EventArgs e)
        {
            string FechaInicial = Convert.ToString(DateInicio.Value.ToString("yyyy-MM-dd"));
            string FechaFinal = Convert.ToString(DateFin.Value.ToString("yyyy-MM-dd"));

            string Hoy = Convert.ToString(DateTime.Today.ToString("yyyy-MM-dd"));

            if (RbHoy.Checked)
            {
                try
                {
                    RptFactsDataDetalle rptFactsData = new RptFactsDataDetalle();
                    rptFactsData.SetParameterValue("@fechaInicio", Hoy);
                    rptFactsData.SetParameterValue("@fechaFin", Hoy);

                    //datos para export excel el report
                    ExportOptions exportOptions;
                    DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = "Excel|*.xls";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        diskFileDestinationOptions.DiskFileName = dialog.FileName;
                    }
                    exportOptions = rptFactsData.ExportOptions;
                    {
                        exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        exportOptions.ExportFormatType = ExportFormatType.Excel;
                        exportOptions.ExportDestinationOptions = diskFileDestinationOptions;
                        exportOptions.ExportFormatOptions = new ExcelFormatOptions();
                    }
                    rptFactsData.Export();

                    MessageBox.Show("Documento se exporto correctamente.",
                                                           "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {

                    MessageBox.Show("Selecciona correctamente las fecha para filtrar la información.",
                                                           "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            RptFactsDataDetalle rptFactsData = new RptFactsDataDetalle();
                            rptFactsData.SetParameterValue("@fechaInicio", FechaInicial);
                            rptFactsData.SetParameterValue("@fechaFin", FechaFinal);

                            //datos para export excel el report
                            ExportOptions exportOptions;
                            DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                            SaveFileDialog dialog = new SaveFileDialog();
                            dialog.Filter = "Excel|*.xls";
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                diskFileDestinationOptions.DiskFileName = dialog.FileName;
                            }
                            exportOptions = rptFactsData.ExportOptions;
                            {
                                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                exportOptions.ExportFormatType = ExportFormatType.Excel;
                                exportOptions.ExportDestinationOptions = diskFileDestinationOptions;
                                exportOptions.ExportFormatOptions = new ExcelFormatOptions();
                            }
                            rptFactsData.Export();
                            MessageBox.Show("Documento se exporto correctamente.",
                                                          "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Selecciona correctamente las fecha para filtrar la información.",
                                                           "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Ah ocurrido un error inesperado, por favor validar que documento no este en uso. Para poder guardarlo correctamente.",
                                                                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (RbPendientes.Checked)
                    {
                        try
                        {
                            RptFactsPendDetalle rptFactsPend = new RptFactsPendDetalle();
                            rptFactsPend.Refresh();

                            //datos para export excel el report
                            ExportOptions exportOptions;
                            DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                            SaveFileDialog dialog = new SaveFileDialog();
                            dialog.Filter = "Excel|*.xls";
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                diskFileDestinationOptions.DiskFileName = dialog.FileName;
                            }
                            exportOptions = rptFactsPend.ExportOptions;
                            {
                                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                exportOptions.ExportFormatType = ExportFormatType.Excel;
                                exportOptions.ExportDestinationOptions = diskFileDestinationOptions;
                                exportOptions.ExportFormatOptions = new ExcelFormatOptions();
                            }
                            rptFactsPend.Export();
                            MessageBox.Show("Documento se exporto correctamente.",
                                                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Ah ocurrido un error inesperado, por favor validar que documento no este en uso. Para poder guardarlo correctamente.",
                                                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        } 
                    }
                }
            }
        }



        //export to PDF segun el check box elegido, valida que no este en uso el archivo
        private void btnReportPDF_Click(object sender, EventArgs e)
        {
            string FechaInicial = Convert.ToString(DateInicio.Value.ToString("yyyy-MM-dd"));
            string FechaFinal = Convert.ToString(DateFin.Value.ToString("yyyy-MM-dd"));

            string Hoy = Convert.ToString(DateTime.Today.ToString("yyyy-MM-dd"));

            if (RbHoy.Checked)
            {
                try
                {
                    RptFactFilteredDetalle rptFactFiltered = new RptFactFilteredDetalle();
                    rptFactFiltered.SetParameterValue("@fechaInicio", Hoy);
                    rptFactFiltered.SetParameterValue("@fechaFin", Hoy);

                    //datos para export pdf el report
                    ExportOptions exportOptions;
                    DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = "Pdf Files|*.pdf";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        diskFileDestinationOptions.DiskFileName = dialog.FileName;
                    }
                    exportOptions = rptFactFiltered.ExportOptions;
                    {
                        exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        exportOptions.ExportDestinationOptions = diskFileDestinationOptions;
                        exportOptions.ExportFormatOptions = new PdfRtfWordFormatOptions();
                    }
                    rptFactFiltered.Export();

                    MessageBox.Show("Documento se exporto correctamente.",
                                                           "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {

                    MessageBox.Show("Selecciona correctamente las fecha para filtrar la información.",
                                                           "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            RptFactFilteredDetalle rptFactFiltered = new RptFactFilteredDetalle();
                            rptFactFiltered.SetParameterValue("@fechaInicio", FechaInicial);
                            rptFactFiltered.SetParameterValue("@fechaFin", FechaFinal);

                            //datos para export pdf el report
                            ExportOptions exportOptions;
                            DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                            SaveFileDialog dialog = new SaveFileDialog();
                            dialog.Filter = "Pdf Files|*.pdf";
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                diskFileDestinationOptions.DiskFileName = dialog.FileName;
                            }
                            exportOptions = rptFactFiltered.ExportOptions;
                            {
                                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                exportOptions.ExportDestinationOptions = diskFileDestinationOptions;
                                exportOptions.ExportFormatOptions = new PdfRtfWordFormatOptions();
                            }
                            rptFactFiltered.Export();

                            MessageBox.Show("Documento se exporto correctamente.",
                                                                   "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Selecciona correctamente las fecha para filtrar la información.",
                                                           "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Ah ocurrido un error inesperado, por favor validar que documento no este en uso. Para poder guardarlo correctamente.",
                                                                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (RbPendientes.Checked)
                    { 
                        try
                        {
                            RptFactsPDF rptFactsPDF = new RptFactsPDF();

                            //datos para export excel el report
                            ExportOptions exportOptions;
                            DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                            SaveFileDialog dialog = new SaveFileDialog();
                            dialog.Filter = "Pdf Files|*.pdf";
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                diskFileDestinationOptions.DiskFileName = dialog.FileName;
                            }
                            exportOptions = rptFactsPDF.ExportOptions;
                            {
                                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                exportOptions.ExportDestinationOptions = diskFileDestinationOptions;
                                exportOptions.ExportFormatOptions = new PdfRtfWordFormatOptions();
                            }
                            rptFactsPDF.Export();
                            MessageBox.Show("Documento se exporto correctamente.",
                                                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Ah ocurrido un error inesperado, por favor validar que documento no este en uso. Para poder guardarlo correctamente.",
                                                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            dgvFilter.DataSource = finalResult.ToList();
            if (finalResult.ToList().Count > 0)
            {
                BtnVerFacturasList.Visible = true;
                btnReportExcel.Visible = true;
                btnReportPDF.Visible = true;
                valorPendiente = 1;
            }
            else
            {
                BtnVerFacturasList.Visible = false;
                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;
                valorPendiente = 0;
            }
        }
        private void btnFiltrarHoyAnuladas_Click(object sender, EventArgs e)
        {
            dgvFilter.ClearSelection();
            Consecutivo = 0;
            BtnVerFact.Visible = false;
            DateTime fechaActual = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));

            // linq para validar y disenar mejor la DataGridView al usuario
            var result = from fa in DB.Facturas
                         join es in DB.Estados on fa.IdEstado equals es.IdEstado
                         join cl in DB.Clientes on fa.IdCliente equals cl.IdCliente
                         join us in DB.Usuarios on fa.IdUsuario equals us.IdUsuario
                         where ((fa.IdEstado == 5 || fa.IdEstado == 12 || fa.IdEstado == 13) && fa.IdCliente > 0 && fa.FechaFactura == fechaActual)
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
                BtnVerFacturasList.Visible = true;
                btnReportExcel.Visible = true;
                btnReportPDF.Visible = true;
            }
            else
            {
                BtnVerFacturasList.Visible = false;
                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;
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

            dgvFilter.DataSource = finalResult.ToList();
            if (finalResult.ToList().Count > 0)
            {
                BtnVerFacturasList.Visible = true;
                btnReportExcel.Visible = true;
                btnReportPDF.Visible = true;
                valorPendiente = 3; // indica la accion a realizar para ver las facturas list
            }
            else
            {
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

            if (ValidarFechaLimite())
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

                dgvFilter.DataSource = finalResult.ToList();
                if (finalResult.ToList().Count > 0)
                {
                    BtnVerFacturasList.Visible = true;
                    btnReportExcel.Visible = true;
                    btnReportPDF.Visible = true;
                }
                else
                {
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

                var result = DB.SPFactPorRangoFechaSinDetalles1(FechaInicial, FechaFinal).ToList();

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

                var result = DB.SPFactPorRangoFechaAll1(FechaInicial, FechaFinal).ToList();

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
                dgvFilter.DataSource = finalResult.ToList();
                if (result.ToList().Count > 0)
                {
                    BtnVerFacturasList.Visible = true;
                    btnReportExcel.Visible = true;
                    btnReportPDF.Visible = true;
                    valorPorFechas = 3;
                }
                else
                {
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
    }
}
