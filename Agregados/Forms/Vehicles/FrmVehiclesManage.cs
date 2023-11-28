using Agregados.Forms.Loading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Agregados.Forms.Vehicles
{
    public partial class FrmVehiclesManage : Form
    {
        //variables del form
        AgregadosEntities DB;
        Vehiculos vehiculo;
        BitacoraVehiculo bitacoraVehiculo;
        public int IDVehiculo = 0;

        public FrmVehiclesManage()
        {
            InitializeComponent();
            DB = new AgregadosEntities();
            vehiculo = new Vehiculos(); //vehiculo local
            bitacoraVehiculo = new BitacoraVehiculo();
        }

        private void FrmVehiclesManage_Load(object sender, EventArgs e)
        {
            CargarRTV();
            CargarMarchamo();
            CargarEstados();
            CargarMeses();
            ActivarAdd();
            


            // linq para validar y disenar mejor la DataGridView al usuario // empezando la informacion con Estado BUENO y lo unico que se necesita obtener
            //para agilizar la respuesta y no obtener tantas columnas de datos
            var result = from ve in DB.Vehiculos
                         join es in DB.Estados
                         on ve.IdEstado equals es.IdEstado
                         where (ve.IdEstado == 6)
                         select new
                         {
                             ve.IdVehiculo,
                             ve.Placa,
                             ve.Marca,
                             ve.Modelo,
                             ve.Annio,
                             RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",

                             IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                             //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                             MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                             (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" : 
                             (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" : 
                             (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",

                            
                         };
            dgvVehicles.DataSource = result.ToList();
        }

        private void dgvVehicles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvVehicles.SelectedRows.Count == 1)
            {
                vehiculo = new Vehiculos();

                DataGridViewRow MiFila = dgvVehicles.SelectedRows[0];

                int IdVehiculo = Convert.ToInt32(MiFila.Cells["CIdVehiculo"].Value);

                //una vez tenemos el valor del ID, se llama a una funcion 
                //de consultar por ID que entrega como retorno un objeto de tipo vehiculo
                //en este caso voy a reutilizar el objeto de vehiculo local
                //para cargar datos por medio de la funcion 


                //ESTE METODO de consultor RETORNA UN OBJETO de tipo Empleado
                vehiculo = DB.Vehiculos.FirstOrDefault(x => x.IdVehiculo == IdVehiculo);



                if (vehiculo != null && vehiculo.IdVehiculo > 0)
                {
                    //una vez me asegure que el objeto posee datos, entonces se procede a representar
                    //en pantalla
                    txtPlaca.Text = vehiculo.Placa.ToString();
                    txtMarca.Text = vehiculo.Marca.ToString();
                    txtModelo.Text = vehiculo.Modelo.ToString();
                    txtAnnio.Text = vehiculo.Annio.ToString();
                    CboxMes.SelectedValue = vehiculo.MesRevision;
                    CboxRTV.SelectedValue = vehiculo.RtvAlDia;
                    CboxMarchamo.SelectedValue = vehiculo.MarchamoAlDia;
                    CboxStates.SelectedValue = vehiculo.IdEstado;

                    ActivarUpdateDelete();
                }
            }
            else
            {
                vehiculo = null;
            }
        }


        private void CargarEstados()
        {

            //Metodo que permite llamar y obtener los datos filtrados de los estados del usuario y mostrarlos en el comboBox
            var dt = DB.Estados.Where(x => x.IdEstado == 6 || x.IdEstado == 8).ToList();

            CboxStates.ValueMember = "IdEstado";
            CboxStates.DisplayMember = "NombreEstado";
            CboxStates.DataSource = dt;
            CboxStates.SelectedIndex = -1;
        } //carga Cbox Estados
        private void CargarMeses()
        {
            //Metodo para crear un DataTable manual sin sentencia SQL a la Base de datos y asi disenar un modelo al comboBox que permita seleccionar los meses 
            //y entonces guarde pero un valor int, y mostrando un valor string 
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("Id", typeof(int)), new DataColumn("D", typeof(string)) });
            dt.Rows.Add(1, "Enero");
            dt.Rows.Add(2, "Febrero");
            dt.Rows.Add(3, "Marzo");
            dt.Rows.Add(4, "Abril");
            dt.Rows.Add(5, "Mayo");
            dt.Rows.Add(6, "Junio");
            dt.Rows.Add(7, "Julio");
            dt.Rows.Add(8, "Agosto");
            dt.Rows.Add(9, "Septiembre");
            dt.Rows.Add(10, "Octubre");
            dt.Rows.Add(11, "Noviembre");
            dt.Rows.Add(12, "Diciembre");

            CboxMes.DataSource = dt;
            CboxMes.ValueMember = "Id";
            CboxMes.DisplayMember = "D";
            CboxMes.SelectedIndex = -1;

        }//Carga Cbox meses


        //Carga Cbox SI O NO RTV

        private void CargarRTV()
        {
            //Metodo para crear un DataTable manual sin sentencia SQL a la Base de datos y asi disenar un modelo al comboBox que permita seleccionar los ESTADOS si o no
            //de RTV AL DIA
            //y entonces guarde pero un valor int, y mostrando un valor string 
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("Id", typeof(int)), new DataColumn("D", typeof(string)) });
            dt.Rows.Add(0, "No");
            dt.Rows.Add(1, "Si");

            CboxRTV.DataSource = dt;
            CboxRTV.ValueMember = "Id";
            CboxRTV.DisplayMember = "D";
            CboxRTV.SelectedIndex = -1;
        }

        //Carga Cbox SI O NO Marchamos

        private void CargarMarchamo()
        {
            //Metodo para crear un DataTable manual sin sentencia SQL a la Base de datos y asi disenar un modelo al comboBox que permita seleccionar los ESTADOS si o no
            //de marchamo
            //y entonces guarde pero un valor int, y mostrando un valor string 
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("Id", typeof(int)), new DataColumn("D", typeof(string)) });
            dt.Rows.Add(0, "No");
            dt.Rows.Add(1, "Si");

            CboxMarchamo.DataSource = dt;
            CboxMarchamo.ValueMember = "Id";
            CboxMarchamo.DisplayMember = "D";
            CboxMarchamo.SelectedIndex = -1;
        }

        //tiempo loading
        void Wait()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(5);
            }
        }


        //validaciones de botones para evitar errores
        private void ActivarAdd()
        {
            imgAdd.Enabled = true;
            imgUpdate.Enabled = false;
            imgDelete.Enabled = false;
        }
        //validaciones de botones para evitar errores
        private void ActivarUpdateDelete()
        {
            imgAdd.Enabled = false;
            if (Globals.MyGlobalUser.TipoUsuario == 1)
            {
                imgUpdate.Enabled = true;
                imgDelete.Enabled = true;
            }
            else
            {
                imgUpdate.Enabled = false;
                imgDelete.Enabled = false;
            }
        }


        //metodo que permite realizar validaciones de los espacion / campos del form no se toma en cuenta
        private bool ValidarCamposRequeridos()
        {
            bool R = false;

            if (!string.IsNullOrEmpty(txtPlaca.Text.Trim()) &&
                !string.IsNullOrEmpty(txtMarca.Text.Trim()) &&
                !string.IsNullOrEmpty(txtModelo.Text.Trim()) &&
                !string.IsNullOrEmpty(txtAnnio.Text.Trim()) &&
                CboxMes.SelectedIndex != -1 &&
                CboxStates.SelectedIndex != -1 &&
                CboxRTV.SelectedIndex != -1 && CboxMarchamo.SelectedIndex != -1
                )
            {
                R = true;
            }
            else
            {
                //estas validaciones deben ser puntuales para informar al usuario que falla 

                if (string.IsNullOrEmpty(txtPlaca.Text.Trim()))
                {
                    MessageBox.Show("Placa es Requerida", "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPlaca.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtMarca.Text.Trim()))
                {
                    MessageBox.Show("Marca es Requerida", "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMarca.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtModelo.Text.Trim()))
                {
                    MessageBox.Show("Modelo es Requerido", "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtModelo.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtAnnio.Text.Trim()))
                {
                    MessageBox.Show("Año es Requerido", "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAnnio.Focus();
                    return false;
                }
                if (CboxMes.SelectedIndex == -1)
                {
                    MessageBox.Show("Mes es Requerido indicarlo", "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CboxMes.Focus();
                    return false;
                }
                if (CboxRTV.SelectedIndex == -1)
                {
                    MessageBox.Show("Se debe indicar estado de R.T.V.", "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CboxRTV.Focus();
                    return false;
                }
                if (CboxMarchamo.SelectedIndex == -1)
                {
                    MessageBox.Show("Se debe indicar estado del Marchamo", "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CboxRTV.Focus();
                    return false;
                }
                if (CboxStates.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe Seleccionar un estado del Vehículo", "Error de Validación!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CboxStates.Focus();
                    return false;
                }
            }
            return R;
        }


        //limpiar el form, ventana
        private void limpiar()
        {
            txtPlaca.Text = null;
            txtMarca.Text = null;
            txtModelo.Text = null;
            txtAnnio.Text = null;
            CboxMes.SelectedValue = -1;
            CboxRTV.SelectedValue = -1;
            CboxMarchamo.SelectedValue = -1;
            CboxStates.SelectedValue = -1;
            vehiculo = null;
            ActivarAdd();
            dgvVehicles.ClearSelection();

        }
        private void limpiarBusqueda()
        {
            txtIdVehicleSearch.Text = null;
            txtPlacaSearch.Text = null;
            txtAnnioSearch.Text = null;
        }
        //limpia los text
        private void imgClean_Click(object sender, EventArgs e)
        {
            limpiar();
            limpiarBusqueda();
        }

        //change de los checkBoxes
        private void CheckChange()
        {
            if (ChBuenEstado.Checked == true && ChMalEstado.Checked == false && ChReparacion.Checked == false)
            {
                var result = from ve in DB.Vehiculos
                             join es in DB.Estados
                             on ve.IdEstado equals es.IdEstado
                             where (ve.IdEstado == 6)
                             select new
                             {
                                 ve.IdVehiculo,
                                 ve.Placa,
                                 ve.Marca,
                                 ve.Modelo,
                                 ve.Annio,
                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                             //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                 MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                 (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                 (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                 (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                 RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                 MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                             };
                dgvVehicles.DataSource = result.ToList();
                limpiar();
            }
            else
            {
                if (ChBuenEstado.Checked == true && ChMalEstado.Checked == true && ChReparacion.Checked == false)
                {
                    var result = from ve in DB.Vehiculos
                                 join es in DB.Estados
                                 on ve.IdEstado equals es.IdEstado
                                 where (ve.IdEstado == 6 || ve.IdEstado == 7)
                                 select new
                                 {
                                     ve.IdVehiculo,
                                     ve.Placa,
                                     ve.Marca,
                                     ve.Modelo,
                                     ve.Annio,
                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                 //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                     MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                     (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                     (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                     (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                     RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                     MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                 };
                    dgvVehicles.DataSource = result.ToList();
                    limpiar();
                }
                else
                {
                    if (ChBuenEstado.Checked == true && ChMalEstado.Checked == true && ChReparacion.Checked == true)
                    {
                        var result = from ve in DB.Vehiculos
                                     join es in DB.Estados
                                     on ve.IdEstado equals es.IdEstado
                                     where (ve.IdEstado == 6 || ve.IdEstado == 7 || ve.IdEstado == 8)
                                     select new
                                     {
                                         ve.IdVehiculo,
                                         ve.Placa,
                                         ve.Marca,
                                         ve.Modelo,
                                         ve.Annio,
                                         IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                     //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                         MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                         (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                         (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                         (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                         RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                         MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                     };
                        dgvVehicles.DataSource = result.ToList();
                        limpiar();
                    }
                    else
                    {
                        if (ChBuenEstado.Checked == false && ChMalEstado.Checked == false && ChReparacion.Checked == false)
                        {
                            var result = from ve in DB.Vehiculos
                                         join es in DB.Estados
                                         on ve.IdEstado equals es.IdEstado
                                         where (ve.IdEstado != 6 && ve.IdEstado != 7 && ve.IdEstado != 8)
                                         select new
                                         {
                                             ve.IdVehiculo,
                                             ve.Placa,
                                             ve.Marca,
                                             ve.Modelo,
                                             ve.Annio,
                                             IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                         //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                             MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                             (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                             (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                             (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                             RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                             MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                         };
                            dgvVehicles.DataSource = result.ToList();
                            limpiar();
                        }
                        else
                        {
                            if (ChBuenEstado.Checked == false && ChMalEstado.Checked == true && ChReparacion.Checked == false)
                            {
                                var result = from ve in DB.Vehiculos
                                             join es in DB.Estados
                                             on ve.IdEstado equals es.IdEstado
                                             where (ve.IdEstado == 7)
                                             select new
                                             {
                                                 ve.IdVehiculo,
                                                 ve.Placa,
                                                 ve.Marca,
                                                 ve.Modelo,
                                                 ve.Annio,
                                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                             //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                                 MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                                 (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                                 (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                                 (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                                 RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                                 MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                             };
                                dgvVehicles.DataSource = result.ToList();
                                limpiar();
                            }
                            else
                            {
                                if (ChBuenEstado.Checked == false && ChMalEstado.Checked == false && ChReparacion.Checked == true)
                                {
                                    var result = from ve in DB.Vehiculos
                                                 join es in DB.Estados
                                                 on ve.IdEstado equals es.IdEstado
                                                 where (ve.IdEstado == 8)
                                                 select new
                                                 {
                                                     ve.IdVehiculo,
                                                     ve.Placa,
                                                     ve.Marca,
                                                     ve.Modelo,
                                                     ve.Annio,
                                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                 //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                                     MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                                     (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                                     (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                                     (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                                     RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                                     MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                                 };
                                    dgvVehicles.DataSource = result.ToList();
                                    limpiar();
                                }
                                else
                                {
                                    if (ChBuenEstado.Checked == false && ChMalEstado.Checked == true && ChReparacion.Checked == true)
                                    {
                                        var result = from ve in DB.Vehiculos
                                                     join es in DB.Estados
                                                     on ve.IdEstado equals es.IdEstado
                                                     where (ve.IdEstado == 8 || ve.IdEstado == 7)
                                                     select new
                                                     {
                                                         ve.IdVehiculo,
                                                         ve.Placa,
                                                         ve.Marca,
                                                         ve.Modelo,
                                                         ve.Annio,
                                                         IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                     //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                                         MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                                         (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                                         (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                                         (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                                         RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                                         MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                                     };
                                        dgvVehicles.DataSource = result.ToList();
                                        limpiar();
                                    }
                                    else
                                    {
                                        if (ChBuenEstado.Checked == true && ChMalEstado.Checked == false && ChReparacion.Checked == true)
                                        {
                                            var result = from ve in DB.Vehiculos
                                                         join es in DB.Estados
                                                         on ve.IdEstado equals es.IdEstado
                                                         where (ve.IdEstado == 8 || ve.IdEstado == 6)
                                                         select new
                                                         {
                                                             ve.IdVehiculo,
                                                             ve.Placa,
                                                             ve.Marca,
                                                             ve.Modelo,
                                                             ve.Annio,
                                                             RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                                             MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                                             IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                         //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                                             MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                                             (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                                             (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                                             (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                                         };
                                            dgvVehicles.DataSource = result.ToList();
                                            limpiar();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void ChBuenEstado_CheckedChanged(object sender, EventArgs e)
        {
            CheckChange();
        }
        private void ChMalEstado_CheckedChanged(object sender, EventArgs e)
        {
            CheckChange();
        }
        private void ChReparacion_CheckedChanged(object sender, EventArgs e)
        {
            CheckChange();
        }



        //Busquedas x ID
        private void txtIdVehicleSearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdVehicleSearch.Text.Trim()) && txtIdVehicleSearch.Text.Count() > 0)
            {
                txtAnnioSearch.Enabled = false;
                txtPlacaSearch.Enabled = false;
                int num = Convert.ToInt32(txtIdVehicleSearch.Text.Trim());
                if (ChBuenEstado.Checked == true && ChMalEstado.Checked == false && ChReparacion.Checked == false)
                {
                    var result = from ve in DB.Vehiculos
                                 join es in DB.Estados
                                 on ve.IdEstado equals es.IdEstado
                                 where (ve.IdEstado == 6 && ve.IdVehiculo.Equals(num))
                                 select new
                                 {
                                     ve.IdVehiculo,
                                     ve.Placa,
                                     ve.Marca,
                                     ve.Modelo,
                                     ve.Annio,
                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                 //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                     MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                     (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                     (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                     (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                     MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                     RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                 };
                    dgvVehicles.DataSource = result.ToList();
                    limpiar();
                }
                else
                {
                    if (ChBuenEstado.Checked == true && ChMalEstado.Checked == true && ChReparacion.Checked == false)
                    {
                        var result = from ve in DB.Vehiculos
                                     join es in DB.Estados
                                     on ve.IdEstado equals es.IdEstado
                                     where ((ve.IdEstado == 6 || ve.IdEstado == 7) && ve.IdVehiculo.Equals(num))
                                     select new
                                     {
                                         ve.IdVehiculo,
                                         ve.Placa,
                                         ve.Marca,
                                         ve.Modelo,
                                         ve.Annio,
                                         IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                     //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                         MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                         (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                         (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                         (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                         MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                         RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                     };
                        dgvVehicles.DataSource = result.ToList();
                        limpiar();
                    }
                    else
                    {
                        if (ChBuenEstado.Checked == true && ChMalEstado.Checked == true && ChReparacion.Checked == true)
                        {
                            var result = from ve in DB.Vehiculos
                                         join es in DB.Estados
                                         on ve.IdEstado equals es.IdEstado
                                         where ((ve.IdEstado == 6 || ve.IdEstado == 7 || ve.IdEstado == 8) && ve.IdVehiculo.Equals(num))
                                         select new
                                         {
                                             ve.IdVehiculo,
                                             ve.Placa,
                                             ve.Marca,
                                             ve.Modelo,
                                             ve.Annio,
                                             IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                         //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                             MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                             (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                             (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                             (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                             MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                             RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                         };
                            dgvVehicles.DataSource = result.ToList();
                            limpiar();
                        }
                        else
                        {
                            if (ChBuenEstado.Checked == false && ChMalEstado.Checked == false && ChReparacion.Checked == false)
                            {
                                var result = from ve in DB.Vehiculos
                                             join es in DB.Estados
                                             on ve.IdEstado equals es.IdEstado
                                             where ((ve.IdEstado != 6 && ve.IdEstado != 7 && ve.IdEstado != 8) && ve.IdVehiculo.Equals(num))
                                             select new
                                             {
                                                 ve.IdVehiculo,
                                                 ve.Placa,
                                                 ve.Marca,
                                                 ve.Modelo,
                                                 ve.Annio,
                                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                             //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                                 MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                                 (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                                 (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                                 (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                                 MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                                 RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                             };
                                dgvVehicles.DataSource = result.ToList();
                                limpiar();
                            }
                            else
                            {
                                if (ChBuenEstado.Checked == false && ChMalEstado.Checked == true && ChReparacion.Checked == false)
                                {
                                    var result = from ve in DB.Vehiculos
                                                 join es in DB.Estados
                                                 on ve.IdEstado equals es.IdEstado
                                                 where (ve.IdEstado == 7 && ve.IdVehiculo.Equals(num))
                                                 select new
                                                 {
                                                     ve.IdVehiculo,
                                                     ve.Placa,
                                                     ve.Marca,
                                                     ve.Modelo,
                                                     ve.Annio,
                                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                 //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                                     MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                                     (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                                     (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                                     (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                                     MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                                     RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                                 };
                                    dgvVehicles.DataSource = result.ToList();
                                    limpiar();
                                }
                                else
                                {
                                    if (ChBuenEstado.Checked == false && ChMalEstado.Checked == false && ChReparacion.Checked == true)
                                    {
                                        var result = from ve in DB.Vehiculos
                                                     join es in DB.Estados
                                                     on ve.IdEstado equals es.IdEstado
                                                     where (ve.IdEstado == 8 && ve.IdVehiculo.Equals(num))
                                                     select new
                                                     {
                                                         ve.IdVehiculo,
                                                         ve.Placa,
                                                         ve.Marca,
                                                         ve.Modelo,
                                                         ve.Annio,
                                                         IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                     //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                                         MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                                         (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                                         (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                                         (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                                         MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                                         RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                                     };
                                        dgvVehicles.DataSource = result.ToList();
                                        limpiar();
                                    }
                                    else
                                    {
                                        if (ChBuenEstado.Checked == false && ChMalEstado.Checked == true && ChReparacion.Checked == true)
                                        {
                                            var result = from ve in DB.Vehiculos
                                                         join es in DB.Estados
                                                         on ve.IdEstado equals es.IdEstado
                                                         where ((ve.IdEstado == 8 || ve.IdEstado == 7) && ve.IdVehiculo.Equals(num))
                                                         select new
                                                         {
                                                             ve.IdVehiculo,
                                                             ve.Placa,
                                                             ve.Marca,
                                                             ve.Modelo,
                                                             ve.Annio,
                                                             IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                         //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                                             MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                                             (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                                             (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                                             (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                                             MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                                             RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                                         };
                                            dgvVehicles.DataSource = result.ToList();
                                            limpiar();
                                        }
                                        else
                                        {
                                            if (ChBuenEstado.Checked == true && ChMalEstado.Checked == false && ChReparacion.Checked == true)
                                            {
                                                var result = from ve in DB.Vehiculos
                                                             join es in DB.Estados
                                                             on ve.IdEstado equals es.IdEstado
                                                             where ((ve.IdEstado == 8 || ve.IdEstado == 6) && ve.IdVehiculo.Equals(num))
                                                             select new
                                                             {
                                                                 ve.IdVehiculo,
                                                                 ve.Placa,
                                                                 ve.Marca,
                                                                 ve.Modelo,
                                                                 ve.Annio,
                                                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                             //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                                                 MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                                                 (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                                                 (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                                                 (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                                                 MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                                                 RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                                             };
                                                dgvVehicles.DataSource = result.ToList();
                                                limpiar();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (string.IsNullOrEmpty(txtIdVehicleSearch.Text.Trim()) && txtIdVehicleSearch.Text.Count() == 0)
            {
                CheckChange();
                txtAnnioSearch.Enabled = true;
                txtPlacaSearch.Enabled = true;
            }
        }

        //bsuquedas x Placa
        private void txtPlacaSearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPlacaSearch.Text.Trim()) && txtPlacaSearch.Text.Count() > 0)
            {
                txtAnnioSearch.Enabled = false;
                txtIdVehicleSearch.Enabled = false;

                string num = txtPlacaSearch.Text.Trim();
                if (ChBuenEstado.Checked == true && ChMalEstado.Checked == false && ChReparacion.Checked == false)
                {
                    var result = from ve in DB.Vehiculos
                                 join es in DB.Estados
                                 on ve.IdEstado equals es.IdEstado
                                 where (ve.IdEstado == 6 && ve.Placa.Contains(num))
                                 select new
                                 {
                                     ve.IdVehiculo,
                                     ve.Placa,
                                     ve.Marca,
                                     ve.Modelo,
                                     ve.Annio,
                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                 //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                     MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                     (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                     (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                     (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                     MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                     RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                 };
                    dgvVehicles.DataSource = result.ToList();
                    limpiar();
                }
                else
                {
                    if (ChBuenEstado.Checked == true && ChMalEstado.Checked == true && ChReparacion.Checked == false)
                    {
                        var result = from ve in DB.Vehiculos
                                     join es in DB.Estados
                                     on ve.IdEstado equals es.IdEstado
                                     where ((ve.IdEstado == 6 || ve.IdEstado == 7) && ve.Placa.Contains(num))
                                     select new
                                     {
                                         ve.IdVehiculo,
                                         ve.Placa,
                                         ve.Marca,
                                         ve.Modelo,
                                         ve.Annio,
                                         IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                     //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                         MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                         (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                         (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                         (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                         MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                         RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                     };
                        dgvVehicles.DataSource = result.ToList();
                        limpiar();
                    }
                    else
                    {
                        if (ChBuenEstado.Checked == true && ChMalEstado.Checked == true && ChReparacion.Checked == true)
                        {
                            var result = from ve in DB.Vehiculos
                                         join es in DB.Estados
                                         on ve.IdEstado equals es.IdEstado
                                         where ((ve.IdEstado == 6 || ve.IdEstado == 7 || ve.IdEstado == 8) && ve.Placa.Contains(num))
                                         select new
                                         {
                                             ve.IdVehiculo,
                                             ve.Placa,
                                             ve.Marca,
                                             ve.Modelo,
                                             ve.Annio,
                                             IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                         //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                             MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                             (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                             (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                             (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                             MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                             RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                         };
                            dgvVehicles.DataSource = result.ToList();
                            limpiar();
                        }
                        else
                        {
                            if (ChBuenEstado.Checked == false && ChMalEstado.Checked == false && ChReparacion.Checked == false)
                            {
                                var result = from ve in DB.Vehiculos
                                             join es in DB.Estados
                                             on ve.IdEstado equals es.IdEstado
                                             where ((ve.IdEstado != 6 && ve.IdEstado != 7 && ve.IdEstado != 8) && ve.Placa.Contains(num))
                                             select new
                                             {
                                                 ve.IdVehiculo,
                                                 ve.Placa,
                                                 ve.Marca,
                                                 ve.Modelo,
                                                 ve.Annio,
                                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                             //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                                 MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                                 (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                                 (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                                 (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                                 MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                                 RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                             };
                                dgvVehicles.DataSource = result.ToList();
                                limpiar();
                            }
                            else
                            {
                                if (ChBuenEstado.Checked == false && ChMalEstado.Checked == true && ChReparacion.Checked == false)
                                {
                                    var result = from ve in DB.Vehiculos
                                                 join es in DB.Estados
                                                 on ve.IdEstado equals es.IdEstado
                                                 where (ve.IdEstado == 7 && ve.Placa.Contains(num))
                                                 select new
                                                 {
                                                     ve.IdVehiculo,
                                                     ve.Placa,
                                                     ve.Marca,
                                                     ve.Modelo,
                                                     ve.Annio,
                                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                 //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                                     MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                                     (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                                     (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                                     (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                                     MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                                     RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                                 };
                                    dgvVehicles.DataSource = result.ToList();
                                    limpiar();
                                }
                                else
                                {
                                    if (ChBuenEstado.Checked == false && ChMalEstado.Checked == false && ChReparacion.Checked == true)
                                    {
                                        var result = from ve in DB.Vehiculos
                                                     join es in DB.Estados
                                                     on ve.IdEstado equals es.IdEstado
                                                     where (ve.IdEstado == 8 && ve.Placa.Contains(num))
                                                     select new
                                                     {
                                                         ve.IdVehiculo,
                                                         ve.Placa,
                                                         ve.Marca,
                                                         ve.Modelo,
                                                         ve.Annio,
                                                         IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                     //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                                         MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                                         (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                                         (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                                         (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                                         MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                                         RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                                     };
                                        dgvVehicles.DataSource = result.ToList();
                                        limpiar();
                                    }
                                    else
                                    {
                                        if (ChBuenEstado.Checked == false && ChMalEstado.Checked == true && ChReparacion.Checked == true)
                                        {
                                            var result = from ve in DB.Vehiculos
                                                         join es in DB.Estados
                                                         on ve.IdEstado equals es.IdEstado
                                                         where ((ve.IdEstado == 8 || ve.IdEstado == 7) && ve.Placa.Contains(num))
                                                         select new
                                                         {
                                                             ve.IdVehiculo,
                                                             ve.Placa,
                                                             ve.Marca,
                                                             ve.Modelo,
                                                             ve.Annio,
                                                             IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                         //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                                             MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                                             (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                                             (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                                             (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                                             MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                                             RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                                         };
                                            dgvVehicles.DataSource = result.ToList();
                                            limpiar();
                                        }
                                        else
                                        {
                                            if (ChBuenEstado.Checked == true && ChMalEstado.Checked == false && ChReparacion.Checked == true)
                                            {
                                                var result = from ve in DB.Vehiculos
                                                             join es in DB.Estados
                                                             on ve.IdEstado equals es.IdEstado
                                                             where ((ve.IdEstado == 8 || ve.IdEstado == 6) && ve.Placa.Contains(num))
                                                             select new
                                                             {
                                                                 ve.IdVehiculo,
                                                                 ve.Placa,
                                                                 ve.Marca,
                                                                 ve.Modelo,
                                                                 ve.Annio,
                                                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                             //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                                                 MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                                                 (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                                                 (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                                                 (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                                                 MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                                                 RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                                             };
                                                dgvVehicles.DataSource = result.ToList();
                                                limpiar();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (string.IsNullOrEmpty(txtIdVehicleSearch.Text.Trim()))
            {
                CheckChange();
                txtAnnioSearch.Enabled = true;
                txtIdVehicleSearch.Enabled = true;
            }
        }

        //busqueda por Annio
        private void txtAnnioSearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAnnioSearch.Text.Trim()) && txtAnnioSearch.Text.Count() > 0)
            {
                txtPlacaSearch.Enabled = false;
                txtIdVehicleSearch.Enabled = false;
                int num = Convert.ToInt32(txtAnnioSearch.Text.Trim());
                if (ChBuenEstado.Checked == true && ChMalEstado.Checked == false && ChReparacion.Checked == false)
                {
                    var result = from ve in DB.Vehiculos
                                 join es in DB.Estados
                                 on ve.IdEstado equals es.IdEstado
                                 where (ve.IdEstado == 6 && ve.Annio.Equals(num))
                                 select new
                                 {
                                     ve.IdVehiculo,
                                     ve.Placa,
                                     ve.Marca,
                                     ve.Modelo,
                                     ve.Annio,
                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                 //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                     MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                     (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                     (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                     (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                     MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                     RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                 };
                    dgvVehicles.DataSource = result.ToList();
                    limpiar();
                }
                else
                {
                    if (ChBuenEstado.Checked == true && ChMalEstado.Checked == true && ChReparacion.Checked == false)
                    {
                        var result = from ve in DB.Vehiculos
                                     join es in DB.Estados
                                     on ve.IdEstado equals es.IdEstado
                                     where ((ve.IdEstado == 6 || ve.IdEstado == 7) && ve.Annio.Equals(num))
                                     select new
                                     {
                                         ve.IdVehiculo,
                                         ve.Placa,
                                         ve.Marca,
                                         ve.Modelo,
                                         ve.Annio,
                                         IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                     //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                         MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                         (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                         (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                         (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                         MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                         RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                     };
                        dgvVehicles.DataSource = result.ToList();
                        limpiar();
                    }
                    else
                    {
                        if (ChBuenEstado.Checked == true && ChMalEstado.Checked == true && ChReparacion.Checked == true)
                        {
                            var result = from ve in DB.Vehiculos
                                         join es in DB.Estados
                                         on ve.IdEstado equals es.IdEstado
                                         where ((ve.IdEstado == 6 || ve.IdEstado == 7 || ve.IdEstado == 8) && ve.Annio.Equals(num))
                                         select new
                                         {
                                             ve.IdVehiculo,
                                             ve.Placa,
                                             ve.Marca,
                                             ve.Modelo,
                                             ve.Annio,
                                             IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                         //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                             MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                             (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                             (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                             (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                             MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                             RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                         };
                            dgvVehicles.DataSource = result.ToList();
                            limpiar();
                        }
                        else
                        {
                            if (ChBuenEstado.Checked == false && ChMalEstado.Checked == false && ChReparacion.Checked == false)
                            {
                                var result = from ve in DB.Vehiculos
                                             join es in DB.Estados
                                             on ve.IdEstado equals es.IdEstado
                                             where ((ve.IdEstado != 6 && ve.IdEstado != 7 && ve.IdEstado != 8) && ve.Annio.Equals(num))
                                             select new
                                             {
                                                 ve.IdVehiculo,
                                                 ve.Placa,
                                                 ve.Marca,
                                                 ve.Modelo,
                                                 ve.Annio,
                                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                             //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                                 MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                                 (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                                 (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                                 (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                                 MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                                 RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                             };
                                dgvVehicles.DataSource = result.ToList();
                                limpiar();
                            }
                            else
                            {
                                if (ChBuenEstado.Checked == false && ChMalEstado.Checked == true && ChReparacion.Checked == false)
                                {
                                    var result = from ve in DB.Vehiculos
                                                 join es in DB.Estados
                                                 on ve.IdEstado equals es.IdEstado
                                                 where (ve.IdEstado == 7 && ve.Annio.Equals(num))
                                                 select new
                                                 {
                                                     ve.IdVehiculo,
                                                     ve.Placa,
                                                     ve.Marca,
                                                     ve.Modelo,
                                                     ve.Annio,
                                                     IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                 //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                                     MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                                     (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                                     (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                                     (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                                     MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                                     RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                                 };
                                    dgvVehicles.DataSource = result.ToList();
                                    limpiar();
                                }
                                else
                                {
                                    if (ChBuenEstado.Checked == false && ChMalEstado.Checked == false && ChReparacion.Checked == true)
                                    {
                                        var result = from ve in DB.Vehiculos
                                                     join es in DB.Estados
                                                     on ve.IdEstado equals es.IdEstado
                                                     where (ve.IdEstado == 8 && ve.Annio.Equals(num))
                                                     select new
                                                     {
                                                         ve.IdVehiculo,
                                                         ve.Placa,
                                                         ve.Marca,
                                                         ve.Modelo,
                                                         ve.Annio,
                                                         IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                     //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                                         MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                                         (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                                         (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                                         (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                                         MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                                         RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                                     };
                                        dgvVehicles.DataSource = result.ToList();
                                        limpiar();
                                    }
                                    else
                                    {
                                        if (ChBuenEstado.Checked == false && ChMalEstado.Checked == true && ChReparacion.Checked == true)
                                        {
                                            var result = from ve in DB.Vehiculos
                                                         join es in DB.Estados
                                                         on ve.IdEstado equals es.IdEstado
                                                         where ((ve.IdEstado == 8 || ve.IdEstado == 7) && ve.Annio.Equals(num))
                                                         select new
                                                         {
                                                             ve.IdVehiculo,
                                                             ve.Placa,
                                                             ve.Marca,
                                                             ve.Modelo,
                                                             ve.Annio,
                                                             IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                         //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                                             MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                                             (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                                             (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                                             (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                                             MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                                             RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                                         };
                                            dgvVehicles.DataSource = result.ToList();
                                            limpiar();
                                        }
                                        else
                                        {
                                            if (ChBuenEstado.Checked == true && ChMalEstado.Checked == false && ChReparacion.Checked == true)
                                            {
                                                var result = from ve in DB.Vehiculos
                                                             join es in DB.Estados
                                                             on ve.IdEstado equals es.IdEstado
                                                             where ((ve.IdEstado == 8 || ve.IdEstado == 6) && ve.Annio.Equals(num))
                                                             select new
                                                             {
                                                                 ve.IdVehiculo,
                                                                 ve.Placa,
                                                                 ve.Marca,
                                                                 ve.Modelo,
                                                                 ve.Annio,
                                                                 IdEstado = es.NombreEstado, // se usa Alias para validar el tipo de Estado e indicarlo como string y no el int relacional
                                                                                             //Lambda Expresion para validar tipo de mes y proceder a indicarlo en modo texto
                                                                 MesRevision = (ve.MesRevision == 1) ? "Enero" : (ve.MesRevision == 2) ? "Febrero" : (ve.MesRevision == 3) ? "Marzo" :
                                                                 (ve.MesRevision == 4) ? "Abril" : (ve.MesRevision == 5) ? "Mayo" : (ve.MesRevision == 6) ? "Junio" :
                                                                 (ve.MesRevision == 7) ? "Julio" : (ve.MesRevision == 8) ? "Agosto" : (ve.MesRevision == 9) ? "Septiembre" :
                                                                 (ve.MesRevision == 10) ? "Octubre" : (ve.MesRevision == 11) ? "Noviembre" : (ve.MesRevision == 12) ? "Diciembre" : "",
                                                                 MarchamoAlDia = (ve.MarchamoAlDia == 0) ? "No" : (ve.MarchamoAlDia == 1) ? "Si" : "N/A",
                                                                 RtvAlDia = (ve.RtvAlDia == 0) ? "No" : (ve.RtvAlDia == 1) ? "Si" : "N/A",
                                                             };
                                                dgvVehicles.DataSource = result.ToList();
                                                limpiar();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (string.IsNullOrEmpty(txtIdVehicleSearch.Text.Trim()))
            {
                CheckChange();
                txtPlacaSearch.Enabled = true;
                txtIdVehicleSearch.Enabled = true;
            }
        }

        
        
        //ADD
        //TODO validar que se realice la bitacora si se selecciona un estado de reparacion o mal estado
        private void imgAdd_Click(object sender, EventArgs e)
        {
            int estado = 0;
            if (ValidarCamposRequeridos())
            {
                estado = Convert.ToInt32(CboxStates.SelectedValue);
                if (estado == 6) // buen estado
                {
                    DialogResult respuesta = MessageBox.Show("¿Deseas agregar el vehículo con la placa " + $"{txtPlaca.Text.Trim()} ?",
                                        "Registro de Vehículos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        using (FrmLoading frmLoading = new FrmLoading(Wait))
                        {
                            try
                            {
                                vehiculo = new Vehiculos
                                {
                                    Placa = txtPlaca.Text.Trim(),
                                    Marca = txtMarca.Text.Trim(),
                                    Modelo = txtModelo.Text.Trim(),
                                    Annio = Convert.ToInt32(txtAnnio.Text.Trim()),
                                    RtvAlDia = (byte)Convert.ToInt32(CboxRTV.SelectedValue),
                                    MarchamoAlDia = (byte)Convert.ToInt32(CboxMarchamo.SelectedValue),
                                    MesRevision = Convert.ToInt32(CboxMes.SelectedValue),
                                    IdEstado = Convert.ToInt32(CboxStates.SelectedValue)
                                };

                                DB.Vehiculos.Add(vehiculo);

                                if (DB.SaveChanges() > 0)
                                {
                                    CheckChange();
                                    limpiar();
                                    limpiarBusqueda();
                                    MessageBox.Show("Vehículo agregado correctamente!", "Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    vehiculo = null;
                                }
                                else
                                {
                                    MessageBox.Show("Vehículo No fue agregado", "Error Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    vehiculo = null;
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
                else
                {
                  
                    if (estado == 8) // reparacion estado
                    {
                        DialogResult respuesta = MessageBox.Show("¿Deseas agregar el vehículo con la placa " + $"{txtPlaca.Text.Trim()}?, esté estará en reparación," +
                            $" debe de crear la bitacora correspondiente.", "Registro de Vehículos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {
                            using (FrmLoading frmLoading = new FrmLoading(Wait))
                            {
                                try
                                {
                                    vehiculo = new Vehiculos
                                    {
                                        Placa = txtPlaca.Text.Trim(),
                                        Marca = txtMarca.Text.Trim(),
                                        Modelo = txtModelo.Text.Trim(),
                                        Annio = Convert.ToInt32(txtAnnio.Text.Trim()),
                                        RtvAlDia = (byte)Convert.ToInt32(CboxRTV.SelectedValue),
                                        MarchamoAlDia = (byte)Convert.ToInt32(CboxMarchamo.SelectedValue),
                                        MesRevision = Convert.ToInt32(CboxMes.SelectedValue),
                                        IdEstado = Convert.ToInt32(CboxStates.SelectedValue)
                                    };

                                    DB.Vehiculos.Add(vehiculo);

                                    if (DB.SaveChanges() > 0)
                                    {
                                        IDVehiculo = vehiculo.IdVehiculo;

                                        Form FrmBitVehiculos = new FrmBitVehiculos(1, IDVehiculo, 0); // 1 para agregar bitacora

                                        DialogResult resp = FrmBitVehiculos.ShowDialog();

                                        if (resp == DialogResult.OK)
                                        {
                                            CheckChange();
                                            limpiar();
                                            limpiarBusqueda();
                                            MessageBox.Show("Vehículo y Bitacora agregados correctamente!", "Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            IDVehiculo = 0;
                                            vehiculo = null;
                                        }
                                        else
                                        {
                                            CheckChange();
                                            limpiar();
                                            limpiarBusqueda();
                                            MessageBox.Show("Vehículo agregado correctamente! pero ocurrio un error con la bitacora, puede realizar el ingreso" +
                                                "manualmente", "Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            IDVehiculo = 0;
                                            vehiculo = null;
                                        }
                                    
                                    }
                                    else
                                    {
                                        MessageBox.Show("Vehículo No fue agregado", "Error Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        vehiculo = null;
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
        }

        //UPDATE
        private void imgUpdate_Click(object sender, EventArgs e)
        {
            int estado = 0;
            if (vehiculo != null)
            {
                if (ValidarCamposRequeridos())
                {
                    estado = Convert.ToInt32(CboxStates.SelectedValue);
                    bitacoraVehiculo = DB.BitacoraVehiculo.Where((x) => x.IdEstado == 8 && x.IdVehiculo == vehiculo.IdVehiculo).FirstOrDefault();
                    if (bitacoraVehiculo != null)
                    {
                        int idBitacora = DB.BitacoraVehiculo.Where((x) => x.IdEstado == 8 && x.IdVehiculo == vehiculo.IdVehiculo).Select((x) => x.IdBitacora).Max();
                        bitacoraVehiculo = DB.BitacoraVehiculo.Find(idBitacora);
                    }
                    else
                    {
                        bitacoraVehiculo = null;
                    }


                    if (bitacoraVehiculo != null)
                    {
                        //hay bitacora, se debe modificar para sacar de reparacion porque el vehiculo queda en estado reparado (buen estado)
                        if (Convert.ToInt32(CboxStates.SelectedValue) == 6)
                        {
                            DialogResult respuesta = MessageBox.Show("¿Deseas modificar el vehículo con la placa " + $"{txtPlaca.Text.Trim()} ?",
                            "Registro de Vehículos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (respuesta == DialogResult.Yes)
                            {
                                using (FrmLoading frmLoading = new FrmLoading(Wait))
                                {
                                    try
                                    {
                                        vehiculo.Placa = txtPlaca.Text.Trim();
                                        vehiculo.Marca = txtMarca.Text.Trim();
                                        vehiculo.Modelo = txtModelo.Text.Trim();
                                        vehiculo.Annio = Convert.ToInt32(txtAnnio.Text.Trim());
                                        vehiculo.MesRevision = Convert.ToInt32(CboxMes.SelectedValue);
                                        vehiculo.MarchamoAlDia = (byte)Convert.ToInt32(CboxMarchamo.SelectedValue);
                                        vehiculo.RtvAlDia = (byte)Convert.ToInt32(CboxRTV.SelectedValue);
                                        vehiculo.IdEstado = Convert.ToInt32(CboxStates.SelectedValue);

                                        DB.Entry(vehiculo).State = EntityState.Modified;

                                        if (DB.SaveChanges() > 0)
                                        {
                                            IDVehiculo = vehiculo.IdVehiculo;
                                            Form FrmBitVehiculos = new FrmBitVehiculos(2, IDVehiculo, bitacoraVehiculo.IdBitacora); // 2 para modificar bitacora sacar de reparacion

                                            DialogResult resp = FrmBitVehiculos.ShowDialog();

                                            if (resp == DialogResult.OK)
                                            {
                                                CheckChange();
                                                limpiar();
                                                limpiarBusqueda();
                                                MessageBox.Show("Vehículo y Bitácora modificados correctamente!", "Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                IDVehiculo = 0;
                                                vehiculo = null;
                                            }
                                            else
                                            {
                                                CheckChange();
                                                limpiar();
                                                limpiarBusqueda();
                                                MessageBox.Show("Vehículo modificado correctamente! pero ocurrio un error con la bitácora, puede realizar la modificacion manual de ella",
                                                    "Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                IDVehiculo = 0;
                                                vehiculo = null;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Vehículo No fue modificado", "Error Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            vehiculo = null;
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
                        else
                        {
                            //hay bitacora, se debe modificar para que  quede aun en reparacion porque el vehiculo queda en estado de reparacion
                            if (Convert.ToInt32(CboxStates.SelectedValue) == 8)
                            {
                                DialogResult respuesta = MessageBox.Show("¿Deseas modificar el vehículo con la placa " + $"{txtPlaca.Text.Trim()} ?",
                                "Registro de Vehículos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (respuesta == DialogResult.Yes)
                                {
                                    using (FrmLoading frmLoading = new FrmLoading(Wait))
                                    {
                                        try
                                        {
                                            vehiculo.Placa = txtPlaca.Text.Trim();
                                            vehiculo.Marca = txtMarca.Text.Trim();
                                            vehiculo.Modelo = txtModelo.Text.Trim();
                                            vehiculo.Annio = Convert.ToInt32(txtAnnio.Text.Trim());
                                            vehiculo.MesRevision = Convert.ToInt32(CboxMes.SelectedValue);
                                            vehiculo.MarchamoAlDia = (byte)Convert.ToInt32(CboxMarchamo.SelectedValue);
                                            vehiculo.RtvAlDia = (byte)Convert.ToInt32(CboxRTV.SelectedValue);
                                            vehiculo.IdEstado = Convert.ToInt32(CboxStates.SelectedValue);

                                            DB.Entry(vehiculo).State = EntityState.Modified;

                                            if (DB.SaveChanges() > 0)
                                            {
                                                IDVehiculo = vehiculo.IdVehiculo;
                                                Form FrmBitVehiculos = new FrmBitVehiculos(3, IDVehiculo, bitacoraVehiculo.IdBitacora); // 3 para modificar bitacora sacar de reparacion si fuera el caso

                                                DialogResult resp = FrmBitVehiculos.ShowDialog();

                                                if (resp == DialogResult.OK)
                                                {
                                                    CheckChange();
                                                    limpiar();
                                                    limpiarBusqueda();
                                                    MessageBox.Show("Vehículo y Bitácora modificados correctamente!", "Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    IDVehiculo = 0;
                                                    vehiculo = null;
                                                }
                                                else
                                                {
                                                    CheckChange();
                                                    limpiar();
                                                    limpiarBusqueda();
                                                    MessageBox.Show("Vehículo modificado correctamente! pero ocurrio un error con la bitácora, puede realizar la modificacion manual de ella",
                                                        "Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    IDVehiculo = 0;
                                                    vehiculo = null;
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Vehículo No fue modificado", "Error Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                vehiculo = null;
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
                    else
                    {
                        //bitacora vacia, esta en buen estado no se procede a abrir bitacora ni modificar una ya que no posee
                        if (Convert.ToInt32(CboxStates.SelectedValue) == 6)
                        {
                            DialogResult respuesta = MessageBox.Show("¿Deseas modificar el vehículo con la placa " + $"{txtPlaca.Text.Trim()} ?",
                           "Registro de Vehículos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (respuesta == DialogResult.Yes)
                            {
                                using (FrmLoading frmLoading = new FrmLoading(Wait))
                                {
                                    try
                                    {

                                        vehiculo.Placa = txtPlaca.Text.Trim();
                                        vehiculo.Marca = txtMarca.Text.Trim();
                                        vehiculo.Modelo = txtModelo.Text.Trim();
                                        vehiculo.Annio = Convert.ToInt32(txtAnnio.Text.Trim());
                                        vehiculo.MesRevision = Convert.ToInt32(CboxMes.SelectedValue);
                                        vehiculo.MarchamoAlDia = (byte)Convert.ToInt32(CboxMarchamo.SelectedValue);
                                        vehiculo.RtvAlDia = (byte)Convert.ToInt32(CboxRTV.SelectedValue);
                                        vehiculo.IdEstado = Convert.ToInt32(CboxStates.SelectedValue);

                                        DB.Entry(vehiculo).State = EntityState.Modified;

                                        if (DB.SaveChanges() > 0)
                                        {
                                            CheckChange();
                                            limpiar();
                                            limpiarBusqueda();
                                            MessageBox.Show("Vehículo modificado correctamente!", "Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            IDVehiculo = 0;
                                            vehiculo = null;

                                        }
                                        else
                                        {
                                            MessageBox.Show("Vehículo No fue modificado", "Error Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            vehiculo = null;
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
                        else
                        {
                            // no hay bitacora abierta y se pasa de bueno a reparacion, se apertura una nueva bitacora
                            if (Convert.ToInt32(CboxStates.SelectedValue) == 8)
                            {
                                DialogResult respuesta = MessageBox.Show("¿Deseas modificar el vehículo con la placa " + $"{txtPlaca.Text.Trim()} ?",
                               "Registro de Vehículos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (respuesta == DialogResult.Yes)
                                {
                                    using (FrmLoading frmLoading = new FrmLoading(Wait))
                                    {
                                        try
                                        {
                                            vehiculo.Placa = txtPlaca.Text.Trim();
                                            vehiculo.Marca = txtMarca.Text.Trim();
                                            vehiculo.Modelo = txtModelo.Text.Trim();
                                            vehiculo.Annio = Convert.ToInt32(txtAnnio.Text.Trim());
                                            vehiculo.MesRevision = Convert.ToInt32(CboxMes.SelectedValue);
                                            vehiculo.MarchamoAlDia = (byte)Convert.ToInt32(CboxMarchamo.SelectedValue);
                                            vehiculo.RtvAlDia = (byte)Convert.ToInt32(CboxRTV.SelectedValue);
                                            vehiculo.IdEstado = Convert.ToInt32(CboxStates.SelectedValue);

                                            DB.Entry(vehiculo).State = EntityState.Modified;

                                            if (DB.SaveChanges() > 0)
                                            {
                                                IDVehiculo = vehiculo.IdVehiculo;

                                                Form FrmBitVehiculos = new FrmBitVehiculos(1, IDVehiculo, 0); //1 para abrir nueva linea bitacora

                                                DialogResult resp = FrmBitVehiculos.ShowDialog();

                                                if (resp == DialogResult.OK)
                                                {
                                                    CheckChange();
                                                    limpiar();
                                                    limpiarBusqueda();
                                                    MessageBox.Show("Vehículo y Bitácora modificados correctamente!", "Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    IDVehiculo = 0;
                                                    vehiculo = null;
                                                }
                                                else
                                                {
                                                    CheckChange();
                                                    limpiar();
                                                    limpiarBusqueda();
                                                    MessageBox.Show("Vehículo modificado correctamente! pero ocurrio un error con la bitácora, puede realizar la modificacion manual de ella",
                                                        "Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    IDVehiculo = 0;
                                                    vehiculo = null;
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Vehículo No fue modificado", "Error Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                vehiculo = null;
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
                }
            }
            else
            {
                MessageBox.Show("Vehículo No existe, o no ha sido seleccionado de la lista", "Error Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        //DELETE -- inactivar porque, puede que tenga bitacora de registro
        private void imgDelete_Click(object sender, EventArgs e)
        {
            if (vehiculo != null)
            {
                if (ValidarCamposRequeridos())
                {

                    DialogResult respuesta = MessageBox.Show("¿Deseas eliminar el vehículo con la placa " + $"{txtPlaca.Text.Trim()} ?" +
                        Environment.NewLine + "Tambien se eliminará los datos de la bitácora que tenga el vehículo." +
                        Environment.NewLine + "Si lo eliminas, no prodras recuperar nuevamente sus datos...",
                        "Registro de Vehículos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        using (FrmLoading frmLoading = new FrmLoading(Wait))
                        {
                            try
                            {
                                if (vehiculo == null)
                                {
                                    MessageBox.Show("Vehículo No existe, o no ha sido seleccionado de la lista", "Error Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    var result = DB.SPBitacorasVehiculoTop5(vehiculo.IdVehiculo).ToList();
                                    if (result.ToList().Count > 0)
                                    {
                                        var resultDelete = DB.SPBitacorasVehiculoDelete(vehiculo.IdVehiculo); // metodo Porcedimiento almacenado para eliminar bitacora del vehiculo
                                        if (resultDelete < 0)
                                        {
                                            DB.SaveChanges();
                                            DB.Vehiculos.Remove(vehiculo); // metodo para eliminar el vehiculo, dato de la BD
                                            if (DB.SaveChanges() > 0)
                                            {
                                                limpiar();
                                                CheckChange();
                                                limpiarBusqueda();
                                                MessageBox.Show("Vehículo Eliminado Correctamente!", "Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                vehiculo = null;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Registro de la bitácora del vehículo se eliminó. pero ocurrio un error al eliminar el registro de vehículo",
                                                    "Error Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Ocurrio un error al eliminar el registro de vehículo",
                                                   "Error Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        } 
                                    }
                                    else
                                    {
                                        DB.Vehiculos.Remove(vehiculo); // metodo para eliminar el vehiculo, dato de la BD
                                        if (DB.SaveChanges() > 0)
                                        {
                                            limpiar();
                                            CheckChange();
                                            limpiarBusqueda();
                                            MessageBox.Show("Vehículo Eliminado Correctamente!", "Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            vehiculo = null;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Vehículo No fue Eliminado, por favor valide", "Error Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                throw;
                            }
                        }
                    }
                    /*
                    DialogResult respuesta = MessageBox.Show("¿Deseas inactivar el vehículo con la placa " + $"{txtPlaca.Text.Trim()} ?",
                                "Registro de Vehículos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        using (FrmLoading frmLoading = new FrmLoading(Wait))
                        {
                            try
                            {
                                vehiculo.IdEstado = 2; //inactivar 

                                DB.Entry(vehiculo).State = EntityState.Modified;

                                if (DB.SaveChanges() > 0)
                                {
                                    CheckChange();
                                    limpiar();
                                    limpiarBusqueda();
                                    MessageBox.Show("Vehículo inactivado correctamente!, mismo ya no se va a mostrar en la lista", 
                                        "Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                                    vehiculo = null;
                                }
                                else
                                {
                                    MessageBox.Show("Vehículo No fue modificado", "Error Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    vehiculo = null;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                throw;
                            }
                        }
                    }*/

                }
            }
            else
            {
                MessageBox.Show("Vehículo No existe, o no ha sido seleccionado de la lista", "Error Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Validaciones de campos
        private void txtIdVehicleSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }

        private void txtPlacaSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, true, false);
        }

        private void txtAnnioSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }

        //cuando cierre el formulario o salir 
        private void imgExit_Click(object sender, EventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
        }

        private void FrmVehiclesManage_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
        }

        private void txtAnnio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }

        private void infoCarImage_Click(object sender, EventArgs e)
        {
            if (vehiculo == null)
            {
                MessageBox.Show("Vehículo No existe, o no ha sido seleccionado de la lista", "Error Registro de Vehículos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (Globals.Bitacora)
                {
                    Globals.MifrmBitVehiculeList.Focus();
                }
                else
                {
                    Globals.MifrmBitVehiculeList = new Vehicles.FrmBitVehiculeList(vehiculo.IdVehiculo);
                    Globals.MifrmBitVehiculeList.Show();
                    Globals.Bitacora = true;
                }
            }
        }
    }
}
