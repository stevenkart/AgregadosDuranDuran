using Agregados.Reports.Caja;
using Agregados.Reports.Facts.FactNow;
using Agregados.Reports.Facts.TicketFiltered;
using CrystalDecisions.Shared;
using Ganss.Excel;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Forms.Reports
{
    public partial class FrmCajaReports : Form
    {
        //variables del form
        AgregadosEntities DB;
        CierreApertCajas cierreApertCajas;
        CierreApertCajas cierreApertCajas2; //para buscar el cierre actual
        Usuarios usuario;
        int Id;

        int valorPendiente = 0; //acciones de credito

        int valorPorFechas = 0; //acciones de rango de fechas

        int valorHoy = 0; //acciones de rango de fechas

        public FrmCajaReports()
        {
            InitializeComponent(); 
            DB = new AgregadosEntities();
            usuario = new Usuarios();
            cierreApertCajas = new CierreApertCajas();
            cierreApertCajas2 = new CierreApertCajas();
            valorPendiente = 0;
            valorPorFechas = 0;
            valorHoy = 0;
        }

        private void FrmCajaReports_FormClosing(object sender, FormClosingEventArgs e)
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

        private void FrmCajaReports_Load(object sender, EventArgs e)
        {
            btnFiltrarHoyTodas.Visible = false;


            btnFiltrarTodosCierres.Visible = false;
            DateInicio.Visible = false;
            DateFin.Visible = false;

            btnCierresPend.Visible = false;

            BtnVerCierre.Visible = false;
            BtnVerCierreList.Visible = false;
            btnReportExcel.Visible = false;
            btnReportPDF.Visible = false;



            DateTime fechaActual = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));

       
            var result = DB.SPCierreCajaPorFecha(fechaActual, fechaActual).ToList();

            if (result.ToList().Count > 0)
            {
                dgvFilter.DataSource = result.ToList();
                MessageBox.Show($"Lista de cierre caja de hoy {fechaActual}", "Lista de Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var result2 = DB.SPCierreCajaPend();

                if (result2.ToList().Count > 0)
                {
                    dgvFilter.DataSource = result.ToList();
                    MessageBox.Show($"Lista de cierre caja de hoy {fechaActual}, Solo se muestra valor pendiente de cierre;" +
                        $"ya que solo se ha abierto caja una vez hoy y esta actualmente abierta.", "Lista de Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No hay registro de Cierres de Caja de hoy ni pendientes de cierre", "Lista de Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void RbHoy_CheckedChanged(object sender, EventArgs e)
        {
            if (RbHoy.Checked)
            {

                btnFiltrarHoyTodas.Visible = true;


                btnFiltrarTodosCierres.Visible = false;
                DateInicio.Visible = false;
                DateFin.Visible = false;

                btnCierresPend.Visible = false;

                BtnVerCierre.Visible = false;
                BtnVerCierreList.Visible = false;
                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;

          

                valorPendiente = 0; //accion a cierre de pendientes
                valorPorFechas = 0; //accion a cierre de filtro por fechas
                valorHoy = 0; //acciones de rango de fechas hoy
                dgvFilter.ClearSelection();
            }
        }

        private void RbFechas_CheckedChanged(object sender, EventArgs e)
        {
            if (RbFechas.Checked)
            {

                btnFiltrarHoyTodas.Visible = false;


                btnFiltrarTodosCierres.Visible = true;
                DateInicio.Visible = true;
                DateFin.Visible = true;

                btnCierresPend.Visible = false;

                BtnVerCierre.Visible = false;
                BtnVerCierreList.Visible = false;
                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;

             

                valorPendiente = 0; //accion a cierre de pendientes
                valorPorFechas = 0; //accion a cierre de filtro por fechas
                valorHoy = 0; //acciones de rango de fechas hoy
                dgvFilter.ClearSelection();
            }
        }

        private void RbPendientes_CheckedChanged(object sender, EventArgs e)
        {
            if (RbPendientes.Checked)
            {

                btnFiltrarHoyTodas.Visible = false;


                btnFiltrarTodosCierres.Visible = false;
                DateInicio.Visible = false;
                DateFin.Visible = false;

                btnCierresPend.Visible = true;

                BtnVerCierre.Visible = false;
                BtnVerCierreList.Visible = false;
                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;

             

                valorPendiente = 0; //accion a cierre de pendientes
                valorPorFechas = 0; //accion a cierre de filtro por fechas
                valorHoy = 0; //acciones de rango de fechas hoy
                dgvFilter.ClearSelection();
            }
        }

        private void btnFiltrarHoyTodas_Click(object sender, EventArgs e)
        {

            dgvFilter.ClearSelection();
  
            BtnVerCierre.Visible = false;

            DateTime fechaActual = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));


            var result = DB.SPCierreCajaPorFecha(fechaActual, fechaActual).ToList();

            dgvFilter.DataSource = result.ToList();

            if (result.ToList().Count > 0)
            {
                BtnVerCierreList.Visible = true;
                btnReportExcel.Visible = true;
                btnReportPDF.Visible = true;

                valorHoy = 1;
                MessageBox.Show($"Lista de cierre caja de hoy {fechaActual}", "Lista de Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var result2 = DB.SPCierreCajaPend().ToList();
                dgvFilter.DataSource = result2.ToList();

                if (result2.ToList().Count > 0)
                {
                    BtnVerCierreList.Visible = true;
                    btnReportExcel.Visible = true;
                    btnReportPDF.Visible = true;

                    valorPendiente = 1; // indica la accion a realizar 
                    MessageBox.Show($"Lista de cierre caja de hoy {fechaActual}, Solo se muestra valor pendiente de cierre;" +
                        $"ya que solo se ha abierto caja una vez hoy y esta actualmente abierta.", "Lista de Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    BtnVerCierreList.Visible = false;
                    btnReportExcel.Visible = false;
                    btnReportPDF.Visible = false;

                    valorHoy = 0;
                    valorPendiente = 0; // indica la accion a realizar 
                    MessageBox.Show("No hay registro de Cierres de Caja de hoy ni pendientes de cierre", "Lista de Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void btnFiltrarTodosCierres_Click(object sender, EventArgs e)
        {
            dgvFilter.ClearSelection();
          
            BtnVerCierre.Visible = false;


            if (ValidarFechaLimite())
            {
                DateTime FechaInicial = Convert.ToDateTime(DateInicio.Value.ToString("yyyy-MM-dd"));
                DateTime FechaFinal = Convert.ToDateTime(DateFin.Value.ToString("yyyy-MM-dd"));

                var result = DB.SPCierreCajaPorFecha(FechaInicial, FechaFinal).ToList();

                dgvFilter.DataSource = result.ToList();

                if (result.ToList().Count > 0)
                {
                    BtnVerCierreList.Visible = true;
                    btnReportExcel.Visible = true;
                    btnReportPDF.Visible = true;

                    valorPorFechas = 1;
                }
                else
                {
                    MessageBox.Show("No hay registro de Cierres de Caja del rango de fecha consultado", "Lista de Cierre de Caja", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BtnVerCierreList.Visible = false;
                    btnReportExcel.Visible = false;
                    btnReportPDF.Visible = false;

                    valorPorFechas = 0;
                }
            }
            else
            {
                valorPorFechas = 0;
                MessageBox.Show("Selecciona correctamente las fecha para filtrar la información.",
                                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCierresPend_Click(object sender, EventArgs e)
        {
            dgvFilter.ClearSelection();
         
            BtnVerCierre.Visible = false;

            var result = DB.SPCierreCajaPend().ToList();

            dgvFilter.DataSource = result.ToList();

            if (result.ToList().Count > 0)
            {
                //BtnVerCierreList.Visible = true;
                //btnReportExcel.Visible = true;
                //btnReportPDF.Visible = true;

                //valorPendiente = 1; // indica la accion a realizar
                MessageBox.Show("Hay registro de Cierres de Caja, pendiente de cierre, no se puede visualizar reporte hasta que se haya cerrado la caja.",
                "Lista de Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                BtnVerCierreList.Visible = false;
                btnReportExcel.Visible = false;
                btnReportPDF.Visible = false;
                valorPendiente = 0; // indica la accion a realizar 

                MessageBox.Show("No hay registro de Cierres de Caja de hoy ni pendientes de cierre",
                    "Lista de Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void dgvFilter_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFilter.SelectedRows.Count == 1)
            {
                DataGridViewRow MiFila = dgvFilter.SelectedRows[0];

                Id = Convert.ToInt32(MiFila.Cells["CID"].Value);

                if ( Id > 0)
                {
                    BtnVerCierre.Visible = true;
                }
            }
            else
            {
                Id = 0;
            }
        }

        private void BtnVerCierre_Click(object sender, EventArgs e)
        {
            if (Id > 0)
            {
                cierreApertCajas2 = DB.CierreApertCajas.Find(Id);
                if (cierreApertCajas2.Accion == 1 || cierreApertCajas2.Accion == 2 )
                {
                    using (FrmCajaPorID frm = new FrmCajaPorID(Id))
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        frm.ShowDialog();
                        Cursor.Current = Cursors.Default;
                    };
                    
                }
            }
            else
            {
                MessageBox.Show("Debes de seleccionar un cierre de caja para poder visualizar la información.",
                                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVerCierreList_Click(object sender, EventArgs e)
        {
            string FechaInicial = Convert.ToString(DateInicio.Value.ToString("yyyy-MM-dd"));
            string FechaFinal = Convert.ToString(DateFin.Value.ToString("yyyy-MM-dd"));
            string Hoy = Convert.ToString(DateTime.Today.ToString("yyyy-MM-dd"));
            //pendiente de cierre
            if (RbPendientes.Checked)
            {
                switch (valorPendiente)
                {
                    case 0:
                        MessageBox.Show("Debes de seleccionar un filtro para buscar los cierres.",
                      "Registro de Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 1:
                         MessageBox.Show("Selecciona ver cierre de caja, para que puedas visualizarlo.",
                                                "Registro de Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    default:
                        MessageBox.Show("Debes de seleccionar un filtro para buscar los Cierre de Caja, si ya lo seleccionaste, " +
                            "entonces ha ocurrido un error, favor contactar al administrador.",
                      "Registro de Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }

            //por fechas
            if (RbFechas.Checked)
            {
                switch (valorPorFechas)
                {
                    case 0:
                        MessageBox.Show("Debes de seleccionar un filtro para buscar los Registro de Cierre de Caja.",
                      "Registro de Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 1:
                        DialogResult respuesta1 = MessageBox.Show("¿Deseas visualizar todas los Registro de Cierre de Caja?.",
                                                "Registro de Cierre de Caja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta1 == DialogResult.Yes)
                        {
                            using (FrmCajaPorFecha frm = new FrmCajaPorFecha(FechaInicial, FechaFinal))
                            {
                                Cursor.Current = Cursors.WaitCursor;
                                frm.ShowDialog();
                                Cursor.Current = Cursors.Default;
                            };
                        }
                        break;
                    default:
                        MessageBox.Show("Debes de seleccionar un filtro para buscar los Registro de Cierre de Caja, si ya lo seleccionaste, " +
                            "entonces ha ocurrido un error, favor contactar al administrador.",
                      "Registro de Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }

            //hoy
            if (RbHoy.Checked)
            {
                switch (valorHoy)
                {
                    case 0:
                        MessageBox.Show("Debes de seleccionar un filtro para buscar los Registros de Cierre de Caja.",
                      "los Registro de Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 1:
                        DialogResult respuesta1 = MessageBox.Show("¿Deseas visualizar todos los Registro de Cierre de Caja ya cerrados?.",
                                                "Registro de Cierre de Caja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta1 == DialogResult.Yes)
                        {
                            using (FrmCajaPorFecha frm = new FrmCajaPorFecha(Hoy, Hoy))
                            {
                                Cursor.Current = Cursors.WaitCursor;
                                frm.ShowDialog();
                                Cursor.Current = Cursors.Default;
                            };
                        }
                        break;
                    default:
                        MessageBox.Show("Debes de seleccionar un filtro para buscar los Registro de Cierre de Caja, si ya lo seleccionaste, " +
                            "entonces ha ocurrido un error, favor contactar al administrador.",
                      "Registro de Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        private void btnReportExcel_Click(object sender, EventArgs e)
        {
            //parametro para fechas 1
            DateTime FechaInicial = Convert.ToDateTime(DateInicio.Value.ToString("yyyy-MM-dd"));
            DateTime FechaFinal = Convert.ToDateTime(DateFin.Value.ToString("yyyy-MM-dd"));

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
                            MessageBox.Show("Debes de seleccionar un filtro para buscar los Registro de Cierre de Caja.",
                          "Registro de Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 1:
                            DialogResult respuesta = MessageBox.Show("¿Deseas exportar a excel todos Registro de Cierre de Caja de hoy ya cerrados?.",
                                                   "Registro de Cierre de Caja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (respuesta == DialogResult.Yes)
                            {
                                var result = DB.SPCierreCajaPorFecha(Inicial, Final).ToList();

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
                                        mapper.Save(file, result, $"ReportCIERRES", true); //true is for saving .xlsx

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
                            MessageBox.Show("Debes de seleccionar un filtro para buscar los Registro de Cierre de Caja, si ya lo seleccionaste, " +
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
                                    MessageBox.Show("Debes de seleccionar un filtro para buscar los Registro de Cierre de Caja.",
                                  "Registro de Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                case 1:
                                    DialogResult respuesta3 = MessageBox.Show("¿Deseas exportar a excel todas los Registro de Cierre de Caja; ya cerrados, del rango indicado?.",
                                                           "Registro de Cierre de Caja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta3 == DialogResult.Yes)
                                    {
                                        var result = DB.SPCierreCajaPorFecha(FechaInicial, FechaFinal).ToList();

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
                                                mapper.Save(file, result, "ReportCierres", true); //true is for saving .xlsx

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
                                    MessageBox.Show("Debes de seleccionar un filtro para buscar los Registro de Cierre de Caja, si ya lo seleccionaste, " +
                                        "entonces ha ocurrido un error, favor contactar al administrador.",
                                  "Registro de Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        switch (valorPendiente)
                        {
                            case 1:
                                MessageBox.Show("No hay datos que exportar, ya que solo se exporta registros de cierre de caja ya cerrados, busca registros que ya se hayan cerrado.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            default:
                                MessageBox.Show("No hay datos que exportar, ya que solo se exporta registros de cierre de caja ya cerrados, busca registros que ya se hayan cerrado.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
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

            //parametros hoy
            DateTime Inicial = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            DateTime Final = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));

            //----------------------------------------------------------------------------------//

            //para pdf reports
            string pFechaInicial = Convert.ToString(DateInicio.Value.ToString("yyyy-MM-dd"));
            string pFechaFinal = Convert.ToString(DateFin.Value.ToString("yyyy-MM-dd"));

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
                            MessageBox.Show("Debes de seleccionar un filtro para buscar los Registro de Cierre de Caja.",
                          "Registro de Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 1:
                            DialogResult respuesta = MessageBox.Show("¿Deseas exportar a pdf todos los Registro de Cierre de Caja de hoy?.",
                                                   "Registro de Cierre de Caja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (respuesta == DialogResult.Yes)
                            {
                                var result = DB.SPCierreCajaPorFecha(Inicial, Final).ToList();
                                if (result.Count > 0)
                                {
                                    RptCajaPorFecha rptCajaPorFecha = new RptCajaPorFecha();
                                    rptCajaPorFecha.Refresh();
                                    rptCajaPorFecha.SetParameterValue("@fechaInicio", pInicial2);
                                    rptCajaPorFecha.SetParameterValue("@fechaFin", pFinal2);

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

                                    exportOptions = rptCajaPorFecha.ExportOptions;
                                    {
                                        exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exportOptions.ExportDestinationOptions = diskFileDestinationOptions;
                                        exportOptions.ExportFormatOptions = new PdfRtfWordFormatOptions();
                                    }
                                    rptCajaPorFecha.Export();

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
                            MessageBox.Show("Debes de seleccionar un filtro para buscar los Registro de Cierre de Caja, si ya lo seleccionaste, " +
                                "entonces ha ocurrido un error, favor contactar al administrador.",
                          "Registro de Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                    MessageBox.Show("Debes de seleccionar un filtro para buscar los Registro de Cierre de Caja.",
                                  "Registro de Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                case 1:
                                    DialogResult respuesta3 = MessageBox.Show("¿Deseas exportar a pdf todos los Registro de Cierre de Caja cerrados del rango indicado?.",
                                                           "Registro de Cierre de Caja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta3 == DialogResult.Yes)
                                    {
                                        var result = DB.SPCierreCajaPorFecha(FechaInicial, FechaFinal).ToList();
                                        if (result.Count > 0)
                                        {
                                            RptCajaPorFecha rptCajaPorFecha = new RptCajaPorFecha();
                                            rptCajaPorFecha.Refresh();
                                            rptCajaPorFecha.SetParameterValue("@fechaInicio", pFechaInicial);
                                            rptCajaPorFecha.SetParameterValue("@fechaFin", pFechaFinal);

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

                                            exportOptions = rptCajaPorFecha.ExportOptions;
                                            {
                                                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                                exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                                exportOptions.ExportDestinationOptions = diskFileDestinationOptions;
                                                exportOptions.ExportFormatOptions = new PdfRtfWordFormatOptions();
                                            }
                                            rptCajaPorFecha.Export();

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
                                    MessageBox.Show("Debes de seleccionar un filtro para buscar los Registro de Cierre de Caja, si ya lo seleccionaste, " +
                                        "entonces ha ocurrido un error, favor contactar al administrador.",
                                  "Registro de Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        if (RbPendientes.Checked)
                        {
                            switch (valorPendiente)
                            {
                                case 1:
                                    MessageBox.Show("No hay datos que exportar, ya que solo se exporta registros de cierre de caja ya cerrados, busca registros que ya se hayan cerrado.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                default:
                                    MessageBox.Show("No hay datos que exportar, ya que solo se exporta registros de cierre de caja ya cerrados, busca registros que ya se hayan cerrado.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
