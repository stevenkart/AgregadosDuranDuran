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
        public FrmBitVehiculos(int valor, int IDVehiculo, int IDBitacora) //valor 1, añadir vehiculos es
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

            if (ventana == 1)
            {
                tmrFechaHora.Enabled = true;
                txtDateEnter.Text =  Fechap = DateTime.Now.Date.ToShortDateString();
                txtDateEnter.ReadOnly = true;
                txtHourEnter.ReadOnly = true;

                txtSolucion.Enabled = false;
                CostoReparación.Enabled = false;

                lblVehiculo.Text = $"Placa: {vehiculo.Placa}, Marca: {vehiculo.Marca}, Año: {vehiculo.Annio}" ;

                txtDateExit.Enabled = false;
                txtHourExit.Enabled = false;

                btnCancel.Enabled = false;
                btnCancel.Visible = false;

            }
            else
            {
                if (ventana == 2)
                {
                    try
                    {
                        bitacoraVehiculo = DB.BitacoraVehiculo.Find(IDbitacora);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;
                    }

                    tmrFechaHora.Enabled = true;
                    txtDateEnter.Text = Fechap = bitacoraVehiculo.FechaIngreso.ToString();
                    txtHourEnter.Text = bitacoraVehiculo.HoraIngreso.ToString();
                    txtDateEnter.ReadOnly = true;
                    txtHourEnter.ReadOnly = true;

                    txtMotivo.Text = bitacoraVehiculo.Motivo.ToString();
                    txtSolucion.Text = bitacoraVehiculo.Solucion.ToString();
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
                    if (ventana == 3)
                    {
                        try
                        {
                            bitacoraVehiculo = DB.BitacoraVehiculo.Find(IDbitacora);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw;
                        }

                        tmrFechaHora.Enabled = true;
                        txtDateEnter.Text = Fechap = bitacoraVehiculo.FechaIngreso.ToString();
                        txtHourEnter.Text = bitacoraVehiculo.HoraIngreso.ToString();
                        txtDateEnter.ReadOnly = true;
                        txtHourEnter.ReadOnly = true;

                        txtMotivo.Text = bitacoraVehiculo.Motivo.ToString();
                        txtSolucion.Text = bitacoraVehiculo.Solucion.ToString();
                        CostoReparación.Enabled = true;

                        lblVehiculo.Text = $"Placa: {vehiculo.Placa}, Marca: {vehiculo.Marca}, Año: {vehiculo.Annio}";

                        txtDateExit.Enabled = false;
                        txtHourExit.Enabled = false;

                        btnCancel.Enabled = false;
                        btnCancel.Visible = false;
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

                        bitacoraVehiculo = new BitacoraVehiculo
                        {
                            FechaIngreso = Convert.ToDateTime(Fechap),
                            HoraIngreso = txtHourEnter.Text.Trim(),
                            FechaSalida = null,
                            HoraSalida = null,
                            Motivo = txtMotivo.Text.Trim(),
                            Solucion = txtSolucion.Text.Trim(),
                            Mecanico = txtMecanico.Text.Trim(),
                            CostoReparacion = Convert.ToDecimal(CostoReparación.Value),
                            IdVehiculo = vehiculo.IdVehiculo,
                            IdEstado = 8 // 8 reparacion
                        };

                        DB.BitacoraVehiculo.Add(bitacoraVehiculo);
                        if (DB.SaveChanges() > 0)
                        {
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            this.DialogResult = DialogResult.Cancel;
                        }
                        break;
                    case 2: //salida de reparacion pero se consulta estado del vehiculo


                        bitacoraVehiculo.FechaSalida = Convert.ToDateTime(Fechap);
                        bitacoraVehiculo.HoraSalida = txtHourExit.Text.Trim();
                        bitacoraVehiculo.Motivo = txtMotivo.Text.Trim();
                        bitacoraVehiculo.Solucion = txtSolucion.Text.Trim();
                        bitacoraVehiculo.Mecanico = txtMecanico.Text.Trim();
                        bitacoraVehiculo.CostoReparacion = Convert.ToDecimal(CostoReparación.Value);
                        bitacoraVehiculo.IdVehiculo = vehiculo.IdVehiculo;
                        bitacoraVehiculo.IdEstado = vehiculo.IdEstado; // 6 buen estado

                        DB.Entry(bitacoraVehiculo).State = EntityState.Modified;          
                        if (DB.SaveChanges() > 0)
                        {
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            this.DialogResult = DialogResult.Cancel;
                        }
                        break;
                    case 3:
                        bitacoraVehiculo.FechaSalida = null;
                        bitacoraVehiculo.HoraSalida = null;
                        bitacoraVehiculo.Motivo = txtMotivo.Text.Trim();
                        bitacoraVehiculo.Solucion = txtSolucion.Text.Trim();
                        bitacoraVehiculo.Mecanico = txtMecanico.Text.Trim();
                        bitacoraVehiculo.CostoReparacion = Convert.ToDecimal(CostoReparación.Value);
                        bitacoraVehiculo.IdVehiculo = vehiculo.IdVehiculo;
                        bitacoraVehiculo.IdEstado = 8; // 8 reparacion

                        DB.Entry(bitacoraVehiculo).State = EntityState.Modified;
                        if (DB.SaveChanges() > 0)
                        {
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            this.DialogResult = DialogResult.Cancel;
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
    }
}
