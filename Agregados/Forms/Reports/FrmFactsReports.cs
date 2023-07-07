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


        public FrmFactsReports()
        {
            InitializeComponent();
            DB = new AgregadosEntities();
            facturas = new Facturas();


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
            btnFiltrarFechas.Visible = false;
            btnFiltrarCredito.Visible = false;
            DateInicio.Visible = false;
            DateFin.Visible = false;
            BtnVerFact.Visible = false;
            BtnVerFacturasList.Visible = false;
            btnReportExcel.Visible = false;
            btnReportPDF.Visible = false;



            DateTime fechaActual = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            DateTime fechaAnterior = fechaActual.AddDays(-7); //resta 7 dias a la fecha actual
      
            // linq para validar y disenar mejor la DataGridView al usuario
            var result = from fa in DB.Facturas
                         join es in DB.Estados on fa.IdEstado equals es.IdEstado
                         join cl in DB.Clientes on fa.IdCliente equals cl.IdCliente
                         join us in DB.Usuarios on fa.IdUsuario equals us.IdUsuario
                         where ((fa.IdEstado == 4 || fa.IdEstado == 5) && fa.IdCliente > 0 && (fa.FechaFactura >= fechaAnterior && fa.FechaFactura <= fechaActual))
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
                         where ((fa.IdEstado == 4 || fa.IdEstado == 5) && fa.IdCliente > 0 && fa.FechaFactura == fechaActual)
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
                var result = from fa in DB.Facturas
                             join es in DB.Estados on fa.IdEstado equals es.IdEstado
                             join cl in DB.Clientes on fa.IdCliente equals cl.IdCliente
                             join us in DB.Usuarios on fa.IdUsuario equals us.IdUsuario
                             where ((fa.IdEstado == 4 || fa.IdEstado == 5) && fa.IdCliente > 0 && (fa.FechaFactura >= FechaInicial && fa.FechaFactura <= FechaFinal))
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
            else
            {
                MessageBox.Show("Selecciona correctamente las fecha para filtrar la información.",
                                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //boton de filtrar datos a facts pendientes
        private void btnFiltrarCredito_Click(object sender, EventArgs e)
        {
            dgvFilter.ClearSelection();
            Consecutivo = 0;
            BtnVerFact.Visible = false;
            // linq para validar y disenar mejor la DataGridView al usuario
            var result = from fa in DB.Facturas
                            join es in DB.Estados on fa.IdEstado equals es.IdEstado
                            join cl in DB.Clientes on fa.IdCliente equals cl.IdCliente
                            join us in DB.Usuarios on fa.IdUsuario equals us.IdUsuario
                            where (fa.IdEstado == 3 && fa.IdCliente > 0)
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


        //cambios de check box
        private void RbHoy_CheckedChanged(object sender, EventArgs e)
        {
            if (RbHoy.Checked)
            {
                btnFiltrarHoy.Visible = true;
                btnFiltrarFechas.Visible = false;
                btnFiltrarCredito.Visible = false;
                DateInicio.Visible = false;
                DateFin.Visible = false;

                BtnVerFacturasList.Visible = false;
                Consecutivo = 0;

                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;


            }
        }

        //cambios de check box
        private void RbFechas_CheckedChanged(object sender, EventArgs e)
        {
            if (RbFechas.Checked)
            {
                btnFiltrarHoy.Visible = false;
                btnFiltrarFechas.Visible = true;
                btnFiltrarCredito.Visible = false;
                DateInicio.Visible = true;
                DateFin.Visible = true;
                
                BtnVerFacturasList.Visible = false;
                Consecutivo = 0;

                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;

            }
        }

        //cambios de check box
        private void RbPendientes_CheckedChanged(object sender, EventArgs e)
        {
            if (RbPendientes.Checked)
            {
                btnFiltrarHoy.Visible = false;
                btnFiltrarFechas.Visible = false;
                btnFiltrarCredito.Visible = true;
                DateInicio.Visible = false;
                DateFin.Visible = false;
                Consecutivo = 0;

                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;
            }
        }


        //boton que muestra el reporte de la factura 
        private void BtnVerFact_Click(object sender, EventArgs e)
        {

            if (Consecutivo > 0)
            {
                facturas = DB.Facturas.Find(Id);
                if (facturas.IdEstado == 5)
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

            string Hoy = Convert.ToString(DateTime.Today.ToString("yyyy-MM-dd"));

            if (RbHoy.Checked)
            {
                using (FrmPrintFactFilter frm = new FrmPrintFactFilter(Hoy, Hoy))
                {
                    frm.ShowDialog();
                };
            }
            else
            {
                if (RbFechas.Checked)
                {
                    if (ValidarFechaLimite())
                    {
                        using (FrmPrintFactFilter frm = new FrmPrintFactFilter(FechaInicial, FechaFinal))
                        {
                            frm.ShowDialog();
                        };
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
    }
}
