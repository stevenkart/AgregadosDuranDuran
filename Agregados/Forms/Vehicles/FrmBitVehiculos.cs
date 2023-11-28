using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Forms.Vehicles
{
    public partial class FrmBitVehiculos : Form
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
        public FrmBitVehiculos(int valor, int IDVehiculo, int IDBitacora) //valor 1, añadir vehiculos es // 2 cambiar datos segun el estado vehiculo // 3 igual que el 2
        {
            ventana = valor;
            IDbitacora = IDBitacora;
            InitializeComponent();
            DB = new AgregadosEntities();
            vehiculo = new Vehiculos(); //vehiculo local
            bitacoraVehiculo = new BitacoraVehiculo();
            vehiculo = DB.Vehiculos.Find(IDVehiculo);
        }

        private void FrmBitVehiculos_Load(object sender, EventArgs e)
        {

            if (ventana == 1) //crear registro
            {
                label11.Visible = true;
                ChFecha.Visible = true;
                dateRepair.MaxDate = DateTime.Now.Date;
                dateRepair.MinDate = DateTime.Now.AddYears(-5);


                dateSalida.Visible = false;
                TimeSalida.Visible = false;


                dateRepair.Visible = false;
                TimerPickerRepair.Visible = false;

                tmrFechaHora.Enabled = true;
                txtDateEnter.Text =  Fechap = DateTime.Now.Date.ToShortDateString();
                txtDateEnter.ReadOnly = true;
                txtHourEnter.ReadOnly = true;

                

                txtSolucion.Enabled = false;
                CostoReparación.Enabled = false;

                txtMecanico.Enabled = false;

                lblVehiculo.Text = $"Placa: {vehiculo.Placa}, Marca: {vehiculo.Marca}, Año: {vehiculo.Annio}" ;

                txtDateExit.Enabled = false;
                txtHourExit.Enabled = false;

                btnCancel.Enabled = false;
                btnCancel.Visible = false;

            }
            else
            {
                if (ventana == 2) //cerrar registro
                {
                    try
                    {
                        bitacoraVehiculo = DB.BitacoraVehiculo.Find(IDbitacora);

                        if (bitacoraVehiculo != null)
                        {
                            label11.Visible = true;
                            ChFecha.Visible = true;
                            dateSalida.MaxDate = DateTime.Now.Date;
                            dateSalida.MinDate = bitacoraVehiculo.FechaIngreso;
                            dateSalida.Visible = false;
                            DateTime dateHourFin = Convert.ToDateTime(bitacoraVehiculo.FechaIngreso);
                            dateHourFin = dateHourFin.AddHours(Convert.ToDouble(Convert.ToDateTime(bitacoraVehiculo.HoraIngreso).Hour + 1));
                            TimeSalida.MinDate = dateHourFin;
                            TimeSalida.Visible = false;


                            dateRepair.Visible = false;
                            TimerPickerRepair.Visible = false;



                            tmrFechaHora.Enabled = true;
                            txtDateEnter.Text = Fechap = bitacoraVehiculo.FechaIngreso.ToString("dd-MM-yyyy");
                            txtHourEnter.Text = bitacoraVehiculo.HoraIngreso.ToString();
                            txtDateEnter.ReadOnly = true;
                            txtHourEnter.ReadOnly = true;

                            txtMotivo.Text = bitacoraVehiculo.Motivo == null || bitacoraVehiculo.Motivo == "" ? "" : bitacoraVehiculo.Motivo.ToString();
                            txtSolucion.Text = bitacoraVehiculo.Solucion == null || bitacoraVehiculo.Solucion == "" ? "" : bitacoraVehiculo.Solucion.ToString();
                            txtMecanico.Text = bitacoraVehiculo.Mecanico == null || bitacoraVehiculo.Mecanico == "" ? "" : bitacoraVehiculo.Mecanico.ToString();
                            CostoReparación.Value = Convert.ToDecimal(bitacoraVehiculo.CostoReparacion.ToString());
                            CostoReparación.Enabled = true;

                            lblVehiculo.Text = $"Placa: {vehiculo.Placa}, Marca: {vehiculo.Marca}, Año: {vehiculo.Annio}";

                            txtDateExit.Text = Fechap = DateTime.Now.Date.ToShortDateString();
                            txtDateExit.ReadOnly = true;
                            txtHourExit.ReadOnly = true;

                            btnCancel.Enabled = false;
                            btnCancel.Visible = false;
                        }
                        else
                        {
                            MessageBox.Show("No se cargo la información de registro en la bitácora del vehículo", 
                                "Bitácora", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.Cancel;
                            this.Hide();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;
                    }

                   
                }
                else
                {
                    if (ventana == 3) //modificar registro
                    {
                        try
                        {
                            bitacoraVehiculo = DB.BitacoraVehiculo.Find(IDbitacora);

                            if (bitacoraVehiculo != null)
                            {
                                label11.Visible = false;
                                ChFecha.Visible = false;
                                dateRepair.MaxDate = DateTime.Now.Date;
                                dateRepair.Visible = false;
                                TimerPickerRepair.Visible = false;


                                tmrFechaHora.Enabled = true;
                                txtDateEnter.Text = Fechap = bitacoraVehiculo.FechaIngreso.ToString("dd-MM-yyyy");
                                txtHourEnter.Text = bitacoraVehiculo.HoraIngreso.ToString();
                                txtDateEnter.ReadOnly = true;
                                txtHourEnter.ReadOnly = true;

                                txtMotivo.Text = bitacoraVehiculo.Motivo == null || bitacoraVehiculo.Motivo == "" ? "" : bitacoraVehiculo.Motivo.ToString();
                                txtSolucion.Text = bitacoraVehiculo.Solucion == null || bitacoraVehiculo.Solucion == "" ? "" : bitacoraVehiculo.Solucion.ToString();
                                txtMecanico.Text = bitacoraVehiculo.Mecanico == null || bitacoraVehiculo.Mecanico == "" ? "" : bitacoraVehiculo.Mecanico.ToString();
                                CostoReparación.Value = Convert.ToDecimal(bitacoraVehiculo.CostoReparacion.ToString());
                                CostoReparación.Enabled = true;

                                lblVehiculo.Text = $"Placa: {vehiculo.Placa}, Marca: {vehiculo.Marca}, Año: {vehiculo.Annio}";

                                txtDateExit.Enabled = false;
                                txtHourExit.Enabled = false;

                                btnCancel.Enabled = true;
                                btnCancel.Visible = true;
                            }
                            else
                            {
                                MessageBox.Show("No se cargo la información de registro en la bitácora del vehículo",
                                "Bitácora", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.DialogResult = DialogResult.Cancel;
                                this.Hide();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw;
                        }
                    }
                    else
                    {
                        if (ventana == 4) //ver registro
                        {
                            try
                            {
                                bitacoraVehiculo = DB.BitacoraVehiculo.Find(IDbitacora);

                                if (bitacoraVehiculo != null)
                                {
                                    label11.Visible = false;
                                    ChFecha.Visible = false;
                                    dateRepair.MaxDate = DateTime.Now.Date;
                                    dateRepair.Visible = false;
                                    TimerPickerRepair.Visible = false;


                                    tmrFechaHora.Enabled = true;
                                    txtDateEnter.Text = bitacoraVehiculo.FechaIngreso.ToString("dd-MM-yyyy");
                                    txtHourEnter.Text = bitacoraVehiculo.HoraIngreso.ToString();
                                    txtDateEnter.ReadOnly = true;
                                    txtHourEnter.ReadOnly = true;

                                    txtMotivo.Text = bitacoraVehiculo.Motivo == null || bitacoraVehiculo.Motivo == "" ? "" : bitacoraVehiculo.Motivo.ToString();
                                    txtSolucion.Text = bitacoraVehiculo.Solucion == null || bitacoraVehiculo.Solucion == "" ? "" : bitacoraVehiculo.Solucion.ToString();
                                    txtMecanico.Text = bitacoraVehiculo.Mecanico == null || bitacoraVehiculo.Mecanico == "" ? "" : bitacoraVehiculo.Mecanico.ToString();
                                    CostoReparación.Value = Convert.ToDecimal(bitacoraVehiculo.CostoReparacion.ToString());
                                    CostoReparación.Enabled = true;

                                    lblVehiculo.Text = $"Placa: {vehiculo.Placa}, Marca: {vehiculo.Marca}, Año: {vehiculo.Annio}";

                                    txtDateExit.Enabled = false;
                                    txtHourExit.Enabled = false;

                                    txtDateExit.Text = bitacoraVehiculo.FechaSalida.ToString();
                                    txtHourExit.Text = bitacoraVehiculo.HoraSalida.ToString();


                                    btnCancel.Enabled = true;
                                    btnCancel.Visible = true;
                                    btnCancel.Text = "Regresar";

                                    btnAceptar.Enabled = false;
                                    btnAceptar.Visible = false;

                                }
                                else
                                {
                                    MessageBox.Show("No se cargo la información de registro en la bitácora del vehículo",
                                    "Bitácora", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.DialogResult = DialogResult.Cancel;
                                    this.Hide();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                throw;
                            }
                        }
                    }
                }
            }
        }

        private void tmrFechaHora_Tick(object sender, EventArgs e)
        {
            if (ventana == 1)
            {
                string hora = DateTime.Now.ToShortTimeString();
                Horap = hora;
                txtHourEnter.Text = hora;
            }
            else
            {
                if (ventana == 2)
                {
                    string hora = DateTime.Now.ToShortTimeString();
                    HorapSalida = hora;
                    txtHourExit.Text = hora;
                }
            }
           
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                switch (ventana)
                {
                    case 1: // ingreso a reparacion
                        if (!string.IsNullOrEmpty(txtMotivo.Text.Trim()) || txtMotivo.Text != null)
                        {
                            bitacoraVehiculo = new BitacoraVehiculo
                            {
                                FechaIngreso = ChFecha.Checked ? Convert.ToDateTime(dateRepair.Value.ToShortDateString()) : Convert.ToDateTime(Convert.ToDateTime(Fechap).ToShortDateString()),
                                HoraIngreso = ChFecha.Checked ? Convert.ToString(DateTime.Parse(TimerPickerRepair.Value.ToString()).ToShortTimeString()) : txtHourEnter.Text.Trim(),
                                FechaSalida = null,
                                HoraSalida = null,
                                Motivo = txtMotivo.Text.Trim(),
                                Solucion = null,
                                Mecanico = txtMecanico.Text.Trim(),
                                CostoReparacion = Convert.ToDecimal(CostoReparación.Value),
                                IdVehiculo = vehiculo.IdVehiculo,
                                IdEstado = 8, // 8 reparacion
                                IdUsuario = Globals.MyGlobalUser.IdUsuario
                            };

                            DB.BitacoraVehiculo.Add(bitacoraVehiculo);
                            if (DB.SaveChanges() > 0)
                            {
                                this.DialogResult = DialogResult.OK;
                                this.Hide();
                            }
                            else
                            {
                                this.DialogResult = DialogResult.Cancel;
                                this.Hide();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Motivo de Reparación no puede quedar vacío o nulo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        
                        break;
                    case 2: //salida de reparacion pero se consulta estado del vehiculo

                        if ( (txtMotivo.Text.Length > 2) && 
                             (txtSolucion.Text.Length > 2) &&
                             ( txtMecanico.Text.Length > 2) )
                        {
                            bitacoraVehiculo.FechaSalida = ChFecha.Checked ? Convert.ToDateTime(dateSalida.Value.ToShortDateString()) : Convert.ToDateTime(Convert.ToDateTime(Fechap).ToShortDateString());
                            bitacoraVehiculo.HoraSalida = ChFecha.Checked ? Convert.ToString(DateTime.Parse(TimeSalida.Value.ToString()).ToShortTimeString()) : txtHourExit.Text.Trim();

                            bitacoraVehiculo.Motivo = txtMotivo.Text.Trim();
                            bitacoraVehiculo.Solucion = txtSolucion.Text.Trim();
                            bitacoraVehiculo.Mecanico = txtMecanico.Text.Trim();
                            bitacoraVehiculo.CostoReparacion = Convert.ToDecimal(CostoReparación.Value);
                            //bitacoraVehiculo.IdVehiculo = vehiculo.IdVehiculo;
                            bitacoraVehiculo.IdEstado = vehiculo.IdEstado; // 6 buen estado
                                                                           //bitacoraVehiculo.IdUsuario = Globals.MyGlobalUser.IdUsuario;

                            DB.Entry(bitacoraVehiculo).State = EntityState.Modified;
                            if (DB.SaveChanges() > 0)
                            {
                                this.DialogResult = DialogResult.OK;
                                this.Hide();
                            }
                            else
                            {
                                this.DialogResult = DialogResult.Cancel;
                                this.Hide();
                            }
                        }
                        else
                        {
                            string error = "";
                            for (int i = 0; i < 3; i++)
                            {
                                if (i == 0 && (string.IsNullOrEmpty(txtMotivo.Text.Trim()) || txtMotivo.Text == null || txtMotivo.Text.Length < 3))
                                {
                                    error += "El Motivo no puede estar vacío o nulo, debe ser mayor a 3 caracteres";
                                }
                                else
                                {
                                    if (i == 1 && (string.IsNullOrEmpty(txtSolucion.Text.Trim()) || txtSolucion.Text == null || txtSolucion.Text.Length < 3))
                                    {
                                        error += Environment.NewLine + "La Solución no puede estar vacía o nula, debe ser mayor a 3 caracteres";
                                    }
                                    else
                                    {
                                        if (i == 2 && (string.IsNullOrEmpty(txtMecanico.Text.Trim()) || txtMecanico.Text == null || txtMecanico.Text.Length < 3))
                                        {
                                            error += Environment.NewLine + "El espacio de Mecánico no puede estar vacío o nulo, debe ser mayor a 3 caracteres";
                                        }
                                    }
                                }
                            }
                            MessageBox.Show(error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                       
                        break;
                    case 3:
                        if ((txtMotivo.Text.Length > 2) &&
                             (txtMecanico.Text.Length > 2))
                        {
                            bitacoraVehiculo.FechaSalida = null;
                            bitacoraVehiculo.HoraSalida = null;
                            bitacoraVehiculo.Motivo = txtMotivo.Text.Trim();
                            bitacoraVehiculo.Solucion = txtSolucion.Text.Trim();
                            bitacoraVehiculo.Mecanico = txtMecanico.Text.Trim();
                            bitacoraVehiculo.CostoReparacion = Convert.ToDecimal(CostoReparación.Value);
                            //bitacoraVehiculo.IdVehiculo = vehiculo.IdVehiculo;
                            //bitacoraVehiculo.IdEstado = 8; // 8 reparacion
                                                           //bitacoraVehiculo.IdUsuario = Globals.MyGlobalUser.IdUsuario;

                            DB.Entry(bitacoraVehiculo).State = EntityState.Modified;
                            if (DB.SaveChanges() > 0)
                            {
                                this.DialogResult = DialogResult.OK;
                                this.Hide();
                            }
                            else
                            {
                                this.DialogResult = DialogResult.Cancel;
                                this.Hide();
                            }
                        }
                        else
                        {
                            string error = "";
                            for (int i = 0; i < 2; i++)
                            {
                                if (i == 0 && (string.IsNullOrEmpty(txtMotivo.Text.Trim()) || txtMotivo.Text == null || txtMotivo.Text.Length < 3))
                                {
                                    error += "El Motivo no puede estar vacío o nulo, debe ser mayor a 3 caracteres";
                                }
                                else
                                {
                                    if (i == 1 && (string.IsNullOrEmpty(txtMecanico.Text.Trim()) || txtMecanico.Text == null || txtMecanico.Text.Length < 3))
                                    {
                                        error += Environment.NewLine + "El espacio de Mecánico no puede estar vacío o nulo, debe ser mayor a 3 caracteres";
                                    }
                                }
                            }
                            MessageBox.Show(error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                       
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void FrmBitVehiculos_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }

        private void ChFecha_CheckedChanged(object sender, EventArgs e)
        {
            if (ChFecha.Checked)
            {
                if (ventana == 1)
                {
                    dateRepair.Visible = true;
                    TimerPickerRepair.Visible = true;
                    txtDateEnter.Visible = false;
                    txtHourEnter.Visible = false;

                    dateSalida.Visible = false;
                    TimeSalida.Visible = false;
                    txtDateExit.Visible = true;
                    txtHourExit.Visible = true;

                }
                else
                {
                    if (ventana == 2)
                    {
                        dateRepair.Visible = false;
                        TimerPickerRepair.Visible = false;

                        txtDateEnter.Visible = true;
                        txtHourEnter.Visible = true;


                        dateSalida.Visible = true;
                        TimeSalida.Visible = true;
                        txtDateExit.Visible = false;
                        txtHourExit.Visible = false;


                    }
                }
               

            }
            else
            {
                dateRepair.Visible = false;
                TimerPickerRepair.Visible = false;

                txtDateEnter.Visible = true;
                txtHourEnter.Visible = true;


                dateSalida.Visible = false;
                TimeSalida.Visible = false;
                txtDateExit.Visible = true;
                txtHourExit.Visible = true;
            }
        }
    }
}
