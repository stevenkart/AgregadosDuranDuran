using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        Vehiculo vehiculo;

        public FrmVehiclesManage()
        {
            InitializeComponent();
            DB = new AgregadosEntities();
            vehiculo = new Vehiculo(); //vehiculo local
            
        }

        private void FrmVehiclesManage_Load(object sender, EventArgs e)
        {

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
                vehiculo = new Vehiculo();

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
                    CboxStates.SelectedValue = vehiculo.IdEstado;

                    ActivarUpdateDelete();
                }
            }
        }


        private void CargarEstados()
        {

            //Metodo que permite llamar y obtener los datos filtrados de los estados del usuario y mostrarlos en el comboBox
            var dt = DB.Estados.Where(x => x.IdEstado == 6 || x.IdEstado == 7 || x.IdEstado == 8).ToList();

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

        }//Carga Cbox Tipos
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
            imgUpdate.Enabled = true;
            imgDelete.Enabled = true;
        }


        //metodo que permite realizar validaciones de los espacion / campos del form no se toma en cuenta
        // la contrasennia ya que esta se valida en el momento que se desee cambiar o no
        private bool ValidarCamposRequeridos()
        {
            bool R = false;

            if (!string.IsNullOrEmpty(txtPlaca.Text.Trim()) &&
                !string.IsNullOrEmpty(txtMarca.Text.Trim()) &&
                !string.IsNullOrEmpty(txtModelo.Text.Trim()) &&
                !string.IsNullOrEmpty(txtAnnio.Text.Trim()) &&
                CboxMes.SelectedIndex != -1 &&
                CboxStates.SelectedIndex != -1
                )
            {
                R = true;
            }
            else
            {
                //estas validaciones deben ser puntuales para informar al usuario que falla 

                if (string.IsNullOrEmpty(txtPlaca.Text.Trim()))
                {
                    MessageBox.Show("Placa es Requerida", "Error de Validación!", MessageBoxButtons.OK);
                    txtPlaca.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtMarca.Text.Trim()))
                {
                    MessageBox.Show("Marca es Requerida", "Error de Validación!", MessageBoxButtons.OK);
                    txtMarca.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtModelo.Text.Trim()))
                {
                    MessageBox.Show("Modelo es Requerido", "Error de Validación!", MessageBoxButtons.OK);
                    txtModelo.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtAnnio.Text.Trim()))
                {
                    MessageBox.Show("Año es Requerido", "Error de Validación!", MessageBoxButtons.OK);
                    txtAnnio.Focus();
                    return false;
                }
                if (CboxMes.SelectedIndex == -1)
                {
                    MessageBox.Show("Mes es Requerido indicarlo", "Error de Validación!", MessageBoxButtons.OK);
                    CboxMes.Focus();
                    return false;
                }
                if (CboxStates.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe Seleccionar un estado del Vehículo", "Error de Validación!", MessageBoxButtons.OK);
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
            CboxStates.SelectedValue = -1;

            ActivarAdd();

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

        //change de los check boxes
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
            }
        }



        //Todo: bsuquedas x Placa

        //Todo: busqueda por Annio


        //ADD

        //UPDATE

        //DELETE
    







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
    }
}
