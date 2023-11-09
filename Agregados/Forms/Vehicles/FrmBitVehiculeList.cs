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
                    dgvFilter.DataSource = resultFinal.ToList();
                    MessageBox.Show("Bitácora del Vehículo", "Bitácora", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No hay registro en la bitácora del vehículo", "Lista Facturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No se cargo la información del vehículo correctamente", "Lista Facturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Globals.Bitacora = false;
                this.Hide();
            }
            
        }

        private void RdUltimas5_CheckedChanged(object sender, EventArgs e)
        {
            if (RdUltimas5.Checked)
            {
                RdFechas.Enabled = true;
            }
            else
            {
                RdFechas.Enabled = true;


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

                    dgvFilter.DataSource = resultFinal.ToList();
                }
                else
                {
                    MessageBox.Show("No hay registro en la bitácora del vehículo", "Lista Facturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
                DateTime fechaActual = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                DateTime fechaAnterior = fechaActual.AddDays(-3); //resta 3 dias a la fecha actual
                string DateInicio1 = fechaActual.ToString();
                string DateInicio2 = fechaAnterior.ToString();
                var result = DB.SPBitacorasVehiculoFechas(DateInicio2, DateInicio1, vehiculo.IdVehiculo).ToList();
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
                    dgvFilter.DataSource = resultFinal.ToList();
                    //MessageBox.Show("Bitácora del Vehículo últimos 3 días", "Bitácora", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No hay registro en la bitácora del vehículo", "Lista Facturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
                        }
                        else
                        {
                            btnModificar.Enabled = false;
                            btnModificar.Visible = false;
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
            Form FrmBitVehiculos = new FrmBitVehiculos(3, vehiculo.IdVehiculo, bitacoraVehiculo.IdBitacora); // 2 para modificar bitacora, segumiento

            DialogResult resp = FrmBitVehiculos.ShowDialog();

            if (resp == DialogResult.OK)
            {
                MessageBox.Show("Bitácora modificada correctamente!", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var result = DB.SPBitacorasVehiculoTop5(vehiculo.IdVehiculo).ToList();
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

                    dgvFilter.DataSource = resultFinal.ToList();
                }
                else
                {
                    MessageBox.Show("No hay registro en la bitácora del vehículo", "Lista Facturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No se modificó la bitácora seleccionada.",
                    "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DateFin_Leave(object sender, EventArgs e)
        {
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
                dgvFilter.DataSource = resultFinal.ToList();
                //MessageBox.Show("Bitácora del Vehículo últimos 3 días", "Bitácora", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No hay registro en la bitácora del vehículo", "Lista Facturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
