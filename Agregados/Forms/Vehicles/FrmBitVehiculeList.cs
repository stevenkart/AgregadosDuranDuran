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
using static NPOI.SS.Formula.PTG.ArrayPtg;

namespace Agregados.Forms.Vehicles
{
    public partial class FrmBitVehiculeList : Form
    {
        //variables del form
        AgregadosEntities DB;
        Vehiculos vehiculo;
        BitacoraVehiculo bitacoraVehiculo;


        string Fechap;
        string Horap;
        string HorapSalida;
        int ventana = 0;
        int IDbitacora = 0;
        public FrmBitVehiculeList(int IdVehiculo)
        {
            InitializeComponent();
            DB = new AgregadosEntities();
            vehiculo = new Vehiculos(); //vehiculo local
            bitacoraVehiculo = new BitacoraVehiculo();
            vehiculo = DB.Vehiculos.Find(IdVehiculo);
        }

        private void FrmBitVehiculeList_Load(object sender, EventArgs e)
        {
            DateFin.MaxDate = DateTime.Now;
            DateFin.MinDate = DateTime.Now.AddYears(-5);

            DateInicio.MaxDate = DateTime.Now;
            DateInicio.MinDate = DateTime.Now.AddYears(-5);//resta 5 anios a la fecha actual

       
            if (vehiculo != null)
            {
                lblPlaca.Text = vehiculo.Placa.ToString();
                lblMarca.Text = vehiculo.Marca.ToString();
                lblModelo.Text = vehiculo.Modelo.ToString();
                lblAnnio.Text = vehiculo.Annio.ToString();
                lblMesRTV.Text = (vehiculo.MesRevision == 1) ? "Enero" : (vehiculo.MesRevision == 2) ? "Febrero" : (vehiculo.MesRevision == 3) ? "Marzo" :
                                 (vehiculo.MesRevision == 4) ? "Abril" : (vehiculo.MesRevision == 5) ? "Mayo" : (vehiculo.MesRevision == 6) ? "Junio" :
                                 (vehiculo.MesRevision == 7) ? "Julio" : (vehiculo.MesRevision == 8) ? "Agosto" : (vehiculo.MesRevision == 9) ? "Septiembre" :
                                 (vehiculo.MesRevision == 10) ? "Octubre" : (vehiculo.MesRevision == 11) ? "Noviembre" : (vehiculo.MesRevision == 12) ? "Diciembre" : "";
                lblRTV.Text = (vehiculo.RtvAlDia == 0) ? "No" : (vehiculo.RtvAlDia == 1) ? "Si" : "N/A";
                LBLeSTADO.Text = (vehiculo.IdEstado == 6) ? "Buen estado" : (vehiculo.IdEstado == 8) ? "Reparación" : "N/A";

                //DateTime fechaActual = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                //DateTime fechaAnterior = fechaActual.AddDays(-3); //resta 3 dias a la fecha actual

                /*
                var result = from bi in DB.BitacoraVehiculo
                             join es in DB.Estados on bi.IdEstado equals es.IdEstado
                             join ve in DB.Vehiculos on bi.IdVehiculo equals ve.IdVehiculo
                             where ((bi.IdEstado == 6) || (bi.IdEstado == 8) && bi.IdVehiculo == vehiculo.IdVehiculo)
                             select new
                             {
                                 bi.IdBitacora,
                                 bi.FechaIngreso,
                                 bi.Motivo,
                                 bi.Solucion,
                                 bi.Mecanico,
                                 bi.CostoReparacion,
                                 bi.FechaSalida,
                                 IdEstado = (bi.IdEstado == 6) ? "Reparado" : (bi.IdEstado == 8) ? "En Reparación" : "N/A",

                             };
                */
                var result = DB.SPBitacorasVehiculoTop5(vehiculo.IdVehiculo).ToList();
                var resultFinal = from bi in result
                                  select new
                                  {
                                      bi.IdBitacora,
                                      FechaIngreso = bi.FechaIngreso.ToString("dd-MM-yyyy"),
                                      bi.Motivo,
                                      bi.Solucion,
                                      bi.Mecanico,
                                      bi.CostoReparacion,
                                      bi.FechaSalida,
                                      IdEstado = (bi.IdEstado == 6) ? "Reparado" : (bi.IdEstado == 8) ? "En Reparación" : "N/A",
                                  };

                if (resultFinal.ToList().Count > 0)
                {
                    dgvFilter.DataSource = resultFinal.ToList();
                    MessageBox.Show("Bitácora del Vehículo", "Bitácora", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnReportExcel.Visible = true;
                    btnModificar.Visible = false;
                }
                else
                {
                    btnReportExcel.Visible = false;
                    btnModificar.Visible = false;
                    MessageBox.Show("No hay registro en la bitácora del vehículo", "Bitácora", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No se cargo la información del vehículo correctamente", "Bitácora", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Globals.Bitacora = false;
                this.Hide();
            }
            
        }

        private void RdUltimas5_CheckedChanged(object sender, EventArgs e)
        {
            if (RdUltimas5.Checked)
            {
                //como no hay nada seleccionado, muestra datos generales
                /*
                var result = from bi in DB.BitacoraVehiculo
                             join es in DB.Estados on bi.IdEstado equals es.IdEstado
                             join ve in DB.Vehiculos on bi.IdVehiculo equals ve.IdVehiculo
                             where ((bi.IdEstado == 6) || (bi.IdEstado == 8) && bi.IdVehiculo == vehiculo.IdVehiculo)
                             select new
                             {
                                 bi.IdBitacora,
                                 bi.FechaIngreso,
                                 bi.Motivo,
                                 bi.Solucion,
                                 bi.Mecanico,
                                 bi.CostoReparacion,
                                 bi.FechaSalida,
                                 IdEstado = (bi.IdEstado == 6) ? "Reparado" : (bi.IdEstado == 8) ? "En Reparación" : "N/A",

                             };
                */

                var result = DB.SPBitacorasVehiculoTop5(vehiculo.IdVehiculo).ToList();
                var resultFinal = from bi in result
                                  select new
                                  {
                                      bi.IdBitacora,
                                      FechaIngreso = bi.FechaIngreso.ToString("dd-MM-yyyy"),
                                      bi.Motivo,
                                      bi.Solucion,
                                      bi.Mecanico,
                                      bi.CostoReparacion,
                                      bi.FechaSalida,
                                      IdEstado = (bi.IdEstado == 6) ? "Reparado" : (bi.IdEstado == 8) ? "En Reparación" : "N/A",
                                  };

                if (resultFinal.ToList().Count > 0)
                {
                    dgvFilter.DataSource = resultFinal.ToList();
                    btnReportExcel.Visible = true;
                    btnModificar.Visible = false;
                }
                else
                {
                    btnReportExcel.Visible = false;
                    btnModificar.Visible = false;
                    MessageBox.Show("No hay registro en la bitácora del vehículo", "Bitácora", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // RdFechas.Checked = false;
            }
            else
            {
                //RdFechas.Enabled = true;
                RdUltimas5.Checked = false;
            }
        }

        private void RdFechas_CheckedChanged(object sender, EventArgs e)
        {
            if (RdFechas.Checked)
            {
                RdUltimas5.Enabled = true;

                lblfechas.Visible = true;
                DateInicio.Visible = true;
                DateFin.Visible = true;

                DateTime fechaActual = DateTime.Now.Date;
                DateTime fechaAnterior = fechaActual.AddDays(-3); //resta 3 dias a la fecha actual
                string DateInicio1 = fechaActual.ToShortDateString();
                string DateInicio2 = fechaAnterior.ToShortDateString();
                var result = DB.SPBitacorasVehiculoFechas(DateInicio2, DateInicio1, vehiculo.IdVehiculo).ToList();
                var resultFinal = from bi in result
                                  select new
                                  {
                                      bi.IdBitacora,
                                      FechaIngreso = bi.FechaIngreso.ToString("dd-MM-yyyy"),
                                      bi.Motivo,
                                      bi.Solucion,
                                      bi.Mecanico,
                                      bi.CostoReparacion,
                                      bi.FechaSalida,
                                      IdEstado = (bi.IdEstado == 6) ? "Reparado" : (bi.IdEstado == 8) ? "En Reparación" : "N/A",
                                  };

                if (resultFinal.ToList().Count > 0)
                {
                    dgvFilter.DataSource = resultFinal.ToList();
                    btnReportExcel.Visible = true;
                    btnModificar.Visible = false;
                    //MessageBox.Show("Bitácora del Vehículo últimos 3 días", "Bitácora", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    btnModificar.Visible = false;
                    btnReportExcel.Visible = false;
                    MessageBox.Show("No hay registro en la bitácora del vehículo", "Bitácora", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                RdUltimas5.Enabled = true;

                lblfechas.Visible = false;
                DateInicio.Visible = false;
                DateFin.Visible = false;

                //como no hay nada seleccionado, muestra datos generales
                /*
                var result = from bi in DB.BitacoraVehiculo
                             join es in DB.Estados on bi.IdEstado equals es.IdEstado
                             join ve in DB.Vehiculos on bi.IdVehiculo equals ve.IdVehiculo
                             where ((bi.IdEstado == 6) || (bi.IdEstado == 8) && bi.IdVehiculo == vehiculo.IdVehiculo)
                             select new
                             {
                                 bi.IdBitacora,
                                 bi.FechaIngreso,
                                 bi.Motivo,
                                 bi.Solucion,
                                 bi.Mecanico,
                                 bi.CostoReparacion,
                                 bi.FechaSalida,
                                 IdEstado = (bi.IdEstado == 6) ? "Reparado" : (bi.IdEstado == 8) ? "En Reparación" : "N/A",

                             };
                */
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Globals.Bitacora = false;
            this.Hide();
        }

        private void dgvFilter_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFilter.SelectedRows.Count == 1)
            {
                DataGridViewRow MiFila = dgvFilter.SelectedRows[0];

                int id = Convert.ToInt32(MiFila.Cells["CIdBitacora"].Value);

                if (id > 0)
                {
                    bitacoraVehiculo = DB.BitacoraVehiculo.Find(id);
                    if (bitacoraVehiculo != null)
                    {
                        if (bitacoraVehiculo.IdEstado == 8)
                        {
                            btnModificar.Enabled = true;
                            btnModificar.Visible = true;
                            btnModificar.Text = "Modificar Bitácora";
                        }
                        else
                        {
                            if (bitacoraVehiculo.IdEstado == 6)
                            {
                                btnModificar.Enabled = true;
                                btnModificar.Visible = true;
                                btnModificar.Text = "Ver Bitácora";
                            }
                            else
                            {
                                btnModificar.Enabled = false;
                                btnModificar.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        btnModificar.Enabled = false;
                        btnModificar.Visible = false;
                    }
                }
            }
            else
            {
                btnModificar.Visible = false;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (bitacoraVehiculo != null)
            {
                if (bitacoraVehiculo.IdEstado == 6)
                {
                    Form FrmBitVehiculos = new FrmBitVehiculos(4, vehiculo.IdVehiculo, bitacoraVehiculo.IdBitacora); // 4 para ver bitacora solamente

                    DialogResult resp = FrmBitVehiculos.ShowDialog();

                    if (resp == DialogResult.OK)
                    {
                        MessageBox.Show("Bitácora vista correctamente!", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        var result = DB.SPBitacorasVehiculoTop5(vehiculo.IdVehiculo).ToList();
                        var resultFinal = from bi in result
                                          select new
                                          {
                                              bi.IdBitacora,
                                              FechaIngreso = bi.FechaIngreso.ToString("dd-MM-yyyy"),
                                              bi.Motivo,
                                              bi.Solucion,
                                              bi.Mecanico,
                                              bi.CostoReparacion,
                                              bi.FechaSalida,
                                              IdEstado = (bi.IdEstado == 6) ? "Reparado" : (bi.IdEstado == 8) ? "En Reparación" : "N/A",
                                          };

                        if (resultFinal.ToList().Count > 0)
                        {
                            dgvFilter.ClearSelection();
                            dgvFilter.DataSource = resultFinal.ToList();
                            btnReportExcel.Visible = true;
                            btnModificar.Visible = false;
                        }
                        else
                        {
                            btnReportExcel.Visible = false;
                            btnModificar.Visible = false;
                            MessageBox.Show("No hay registro en la bitácora del vehículo", "Bitácora", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        //
                    }
                }
                else
                {
                    if (bitacoraVehiculo.IdEstado == 8)
                    {
                        Form FrmBitVehiculos = new FrmBitVehiculos(3, vehiculo.IdVehiculo, bitacoraVehiculo.IdBitacora); // 3 para modificar bitacora, segumiento solo se saca de reparacion cuando vehiculo se torna a estado reparado

                        DialogResult resp = FrmBitVehiculos.ShowDialog();

                        if (resp == DialogResult.OK)
                        {
                            MessageBox.Show("Bitácora modificada correctamente!", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            var result = DB.SPBitacorasVehiculoTop5(vehiculo.IdVehiculo).ToList();
                            var resultFinal = from bi in result
                                              select new
                                              {
                                                  bi.IdBitacora,
                                                  FechaIngreso = bi.FechaIngreso.ToString("dd-MM-yyyy"),
                                                  bi.Motivo,
                                                  bi.Solucion,
                                                  bi.Mecanico,
                                                  bi.CostoReparacion,
                                                  bi.FechaSalida,
                                                  IdEstado = (bi.IdEstado == 6) ? "Reparado" : (bi.IdEstado == 8) ? "En Reparación" : "N/A",
                                              };

                            if (resultFinal.ToList().Count > 0)
                            {
                                dgvFilter.ClearSelection();
                                dgvFilter.DataSource = resultFinal.ToList();
                            }
                            else
                            {
                                MessageBox.Show("No hay registro en la bitácora del vehículo", "Bitácora", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se modificó la bitácora seleccionada.",
                                "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void DateFin_Leave(object sender, EventArgs e)
        {
            DateTime Fecha1 = Convert.ToDateTime(DateInicio.Value);
            DateTime Fecha2 = Convert.ToDateTime(DateFin.Value);
            string FechaInicial = Convert.ToString(Fecha1.ToShortDateString());
            string FechaFinal = Convert.ToString(Fecha2.ToShortDateString());

            var result = DB.SPBitacorasVehiculoFechas(FechaInicial, FechaFinal, vehiculo.IdVehiculo).ToList();

            var resultFinal = from bi in result
                              select new
                              {
                                  bi.IdBitacora,
                                  FechaIngreso = bi.FechaIngreso.ToString("dd-MM-yyyy"),
                                  bi.Motivo,
                                  bi.Solucion,
                                  bi.Mecanico,
                                  bi.CostoReparacion,
                                  bi.FechaSalida,
                                  IdEstado = (bi.IdEstado == 6) ? "Reparado" : (bi.IdEstado == 8) ? "En Reparación" : "N/A",
                              };
            if (resultFinal.ToList().Count > 0)
            {
                dgvFilter.DataSource = resultFinal.ToList();
                btnReportExcel.Visible = true;
                btnModificar.Visible = false;
                //MessageBox.Show("Bitácora del Vehículo últimos 3 días", "Bitácora", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                btnReportExcel.Visible = false;
                btnModificar.Visible = false;
                MessageBox.Show("No hay registro en la bitácora del vehículo", "Bitácora", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DateInicio_Leave(object sender, EventArgs e)
        {
            /*
            DateTime Fecha1 = Convert.ToDateTime(DateInicio.Value.ToString("yyyy-MM-dd"));
            DateTime Fecha2 = Convert.ToDateTime(DateFin.Value.ToString("yyyy-MM-dd"));
            string FechaInicial = Convert.ToString(Fecha1);
            string FechaFinal = Convert.ToString(Fecha2);

            var result = DB.SPBitacorasVehiculoFechas(FechaInicial, FechaFinal, vehiculo.IdVehiculo).ToList();

            var resultFinal = from bi in result
                              select new
                              {
                                  bi.IdBitacora,
                                  bi.FechaIngreso,
                                  bi.Motivo,
                                  bi.Solucion,
                                  bi.Mecanico,
                                  bi.CostoReparacion,
                                  bi.FechaSalida,
                                  IdEstado = (bi.IdEstado == 6) ? "Reparado" : (bi.IdEstado == 8) ? "En Reparación" : "N/A",
                              };
            if (resultFinal.ToList().Count > 0)
            {
                dgvFilter.DataSource = result.ToList();
                MessageBox.Show("Bitácora del Vehículo últimos 3 días", "Bitácora", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No hay registro en la bitácora del vehículo", "Lista Facturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            */
        }

        private void FrmBitVehiculeList_FormClosing(object sender, FormClosingEventArgs e)
        {
            Globals.Bitacora = false;
            this.Hide();
        }

        private void btnReportExcel_Click(object sender, EventArgs e)
        {
            if (RdFechas.Checked == false && RdUltimas5.Checked == false)
            {
                MessageBox.Show("Se debe de seleccionar el Filtrar por", "Bitácora", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (RdFechas.Checked)
            {
                DateTime Fecha1 = Convert.ToDateTime(DateInicio.Value.ToString("yyyy-MM-dd"));
                DateTime Fecha2 = Convert.ToDateTime(DateFin.Value.ToString("yyyy-MM-dd"));
                string FechaInicial = Convert.ToString(Fecha1);
                string FechaFinal = Convert.ToString(Fecha2);

                try
                {

                   DialogResult respuesta1 = MessageBox.Show("¿Deseas exportar a excel, la bitácora en el rango de fechas seleccionado?.",
                                                    "Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta1 == DialogResult.Yes)
                    {
                        var result = DB.SPBitacorasVehiculoFechas(FechaInicial, FechaFinal, vehiculo.IdVehiculo).ToList();

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
                                mapper.Save(file, result, "BitacoraVehiculoFechas", true); //true is for saving .xlsx

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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                if (RdUltimas5.Checked)
                {
                    try
                    {

                        DialogResult respuesta1 = MessageBox.Show("¿Deseas exportar a excel, la bitácora en el rango de fechas seleccionado?.",
                                                         "Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta1 == DialogResult.Yes)
                        {
                            var result = DB.SPBitacorasVehiculoTop5(vehiculo.IdVehiculo).ToList();

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
                                    mapper.Save(file, result, "BitacoraVehiculo5Ultimas", true); //true is for saving .xlsx

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
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
        }

        private void RdUltimas5_MouseClick(object sender, MouseEventArgs e)
        {
           //
        }
    }
}
