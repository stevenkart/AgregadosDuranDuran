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






    }
}
