using Agregados.Forms.Loading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Forms.Cashiers
{
    public partial class FrmCashierOpen : Form
    {
        //variables del form
        AgregadosEntities DB;
        CierreApertCajas cierreApertCajas;

        CierreApertCajas apertura; // valor termporal apertura
        CierreApertCajas cierre; // valor termporal cierre

        Denominaciones denominacionApert;

        string Fechap;
        string Horap;
        byte Accionp;

        
        public int Cinco;
        public int Diez;
        public int VeinteCinco;
        public int Cincuenta;
        public int Cien;
        public int Quinientos;
        public int Mil;
        public int DosMil;
        public int CincoMil;
        public int DiezMil;
        public int VeinteMil;
        public int total;
        

        public FrmCashierOpen()
        {
            InitializeComponent();
            DB = new AgregadosEntities();
            cierreApertCajas = new CierreApertCajas();
            apertura = new CierreApertCajas();
            cierre = new CierreApertCajas();

            denominacionApert = new Denominaciones();   

            Cinco = 0;
            Diez = 0;
            VeinteCinco = 0;
            Cincuenta = 0;
            Cien = 0;
            Quinientos = 0;
            Mil = 0;
            DosMil = 0;
            CincoMil = 0;
            DiezMil = 0;
            VeinteMil = 0;

            total = 0;
        }


        private void FrmCashierOpen_Load(object sender, EventArgs e)
        {
            tmrFechaHora.Enabled = true;
            Fechap = DateTime.Now.Date.ToShortDateString();
            txtFecha.Text = Fechap;
            txtUser.Text = Globals.MyGlobalUser.NombreUsuario.ToString();
            txtDetalle.Text = null;
            Accionp = 1; // 1 = apertura  & 2 = a cierre
        }

        //tiempo loading
        void Wait()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(5);
            }
        }

        //busca el cierre anterior
        public CierreApertCajas BuscarCierreAnterior()
        {
            cierre = null;
            try
            {
                cierreApertCajas = DB.CierreApertCajas.Where((x) => x.Accion == 2).FirstOrDefault();
                if (cierreApertCajas != null)
                {
                    int id = DB.CierreApertCajas.Where((x) => x.Accion == 2).Select((x) => x.IdCierreApert).Max();
                    cierre = DB.CierreApertCajas.Find(id);
                }
                cierreApertCajas = null;
                return cierre;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //busca el cierre anterior
        public CierreApertCajas BuscarAperturaActual()
        {
            apertura = null;
            try
            {
                cierreApertCajas = DB.CierreApertCajas.Where((x) => x.Accion == 1).FirstOrDefault();
                if (cierreApertCajas != null)
                {
                    int id = DB.CierreApertCajas.Where((x) => x.Accion == 1).Select((x) => x.IdCierreApert).Max();
                    apertura = DB.CierreApertCajas.Find(id);
                }
                cierreApertCajas = null;
                return apertura;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }


        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (BuscarCierreAnterior() != null)
                {
                    //si es diferente nulo, busca y continuar la apertura aqui, si encuentra un cierre anterior
                    //procede a validar si existe una apertura de caja
                    if (BuscarAperturaActual() != null)
                    {
                        if (apertura.IdUsuario == Globals.MyGlobalUser.IdUsuario) // si hay apertura y es el mismo usuario
                        {
                            MessageBox.Show("Ya haz realizado la apertura de caja, no es posible tener otra caja abierta simultaneamente.",
                           "Apertura de Caja", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Ya hay una caja abierta por otro usuario, no es posible tener otra caja abierta simultaneamente.",
                            "Apertura de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        decimal Faltante = (cierre.MontoEfectivoUsuarioFin > Convert.ToDecimal(NumMontInicial.Text)) ? (cierre.MontoEfectivoUsuarioFin - Convert.ToDecimal(NumMontInicial.Text)) : 0;
                        decimal Sobrante = (cierre.MontoEfectivoUsuarioFin < Convert.ToDecimal(NumMontInicial.Text)) ? (Convert.ToDecimal(NumMontInicial.Text) - cierre.MontoEfectivoUsuarioFin) : 0;

                        if (Faltante != 0 || Sobrante != 0)
                        {
                            DialogResult pregunta = MessageBox.Show("Posee diferencia de efectivo en caja, por favor valide." + Environment.NewLine +
                                $"Faltante: ¢ {Faltante} ." + Environment.NewLine +
                                $"Sobrante: ¢ {Sobrante} ." + Environment.NewLine +
                                "¿Deseas continuar y abrir caja con la diferencia?"
                                , "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (pregunta == DialogResult.Yes)
                            {
                                using (FrmLoading frmLoading = new FrmLoading(Wait))
                                {
                                    try
                                    {
                                        cierreApertCajas = new CierreApertCajas
                                        {
                                            Fecha = Convert.ToDateTime(Fechap),
                                            Hora = Horap,
                                            Detalles = txtDetalle.Text.Trim(),
                                            MontoEfectivoInicio = cierre.MontoEfectivoUsuarioFin,
                                            MontoEfectivoUsuarioInicio = Convert.ToDecimal(NumMontInicial.Text),
                                            MontoEfectivoFinal = Convert.ToDecimal(NumMontInicial.Text),
                                            MontoEfectivoUsuarioFin = 0,
                                            MontoVentaEfectivo = 0,
                                            MontoCompraEfectivo = 0,
                                            MontoTransf = 0,
                                            MontoCompraTransf = 0,
                                            MontoSinpe = 0,
                                            MontoCompraSinpe = 0,
                                            MontoCheque = 0,
                                            MontoCredito = 0,
                                            MontoCompraCredito = 0,
                                            FaltanteInicio = Faltante,
                                            SobranteInicio = Sobrante,
                                            FaltanteFin = 0,
                                            SobranteFin = 0,
                                            Accion = Accionp,
                                            IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                        };


                                        DB.CierreApertCajas.Add(cierreApertCajas);


                                        if (DB.SaveChanges() > 0)
                                        {
                                            MessageBox.Show("Apertura de Caja correcta!", "Apertura de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            apertura = BuscarAperturaActual();
                                            denominacionApert = new Denominaciones
                                            {

                                                MonedasCinco = Convert.ToInt32(numCinco.Value),
                                                MonedasDiez = Convert.ToInt32(numDiez.Value),
                                                MonedasVeinteCinco = Convert.ToInt32(numVeinteCinco.Value),
                                                MonedasCincuenta = Convert.ToInt32(numCincuenta.Value),
                                                MonedasCien = Convert.ToInt32(numCien.Value),
                                                MonedasQuinientos = Convert.ToInt32(numQuienientos.Value),


                                                BilleteMil = Convert.ToInt32(numMil.Value),
                                                BilleteDosMil = Convert.ToInt32(numDosMil.Value),
                                                BilleteCincoMil = Convert.ToInt32(numCincoMil.Value),
                                                BilleteDiezMil = Convert.ToInt32(numDiezMil.Value),
                                                BilleteVeinteMil = Convert.ToInt32(numVeinteMil.Value),

                                                IdCierreApert = apertura.IdCierreApert,
                                                AperturaCierre = 1
                                            };
                                            DB.Denominaciones.Add(denominacionApert);
                                            if (DB.SaveChanges() > 0)
                                            {
                                                //this.Hide();
                                                this.DialogResult = DialogResult.OK;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Apertura de Caja correcta, pero no se guardo detalle de las denominaciones ingresadas", "Error",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                //this.Hide();
                                                this.DialogResult = DialogResult.OK;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Apertura de Caja no se pudo realizar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            //this.Hide();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        throw;
                                    }
                                }
                            }
                        }
                        else
                        {

                            if (Faltante == 0 && Sobrante == 0 && Convert.ToDecimal(NumMontInicial.Text) == 0)
                            {
                                using (FrmLoading frmLoading = new FrmLoading(Wait))
                                {
                                    try
                                    {
                                        cierreApertCajas = new CierreApertCajas
                                        {
                                            Fecha = Convert.ToDateTime(Fechap),
                                            Hora = Horap,
                                            Detalles = txtDetalle.Text.Trim(),
                                            MontoEfectivoInicio = cierre.MontoEfectivoUsuarioFin,
                                            MontoEfectivoUsuarioInicio = Convert.ToDecimal(NumMontInicial.Text),
                                            MontoEfectivoFinal = Convert.ToDecimal(NumMontInicial.Text),
                                            MontoEfectivoUsuarioFin = 0,
                                            MontoVentaEfectivo = 0,
                                            MontoCompraEfectivo = 0,
                                            MontoTransf = 0,
                                            MontoCompraTransf = 0,
                                            MontoSinpe = 0,
                                            MontoCompraSinpe = 0,
                                            MontoCheque = 0,
                                            MontoCredito = 0,
                                            MontoCompraCredito = 0,
                                            FaltanteInicio = Faltante,
                                            SobranteInicio = Sobrante,
                                            FaltanteFin = 0,
                                            SobranteFin = 0,
                                            Accion = Accionp,
                                            IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                        };

                                        DB.CierreApertCajas.Add(cierreApertCajas);


                                        if (DB.SaveChanges() > 0)
                                        {
                                            MessageBox.Show("Apertura de Caja correcta!", "Apertura de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            apertura = BuscarAperturaActual();
                                            denominacionApert = new Denominaciones
                                            {

                                                MonedasCinco = Convert.ToInt32(numCinco.Value),
                                                MonedasDiez = Convert.ToInt32(numDiez.Value),
                                                MonedasVeinteCinco = Convert.ToInt32(numVeinteCinco.Value),
                                                MonedasCincuenta = Convert.ToInt32(numCincuenta.Value),
                                                MonedasCien = Convert.ToInt32(numCien.Value),
                                                MonedasQuinientos = Convert.ToInt32(numQuienientos.Value),


                                                BilleteMil = Convert.ToInt32(numMil.Value),
                                                BilleteDosMil = Convert.ToInt32(numDosMil.Value),
                                                BilleteCincoMil = Convert.ToInt32(numCincoMil.Value),
                                                BilleteDiezMil = Convert.ToInt32(numDiezMil.Value),
                                                BilleteVeinteMil = Convert.ToInt32(numVeinteMil.Value),

                                                IdCierreApert = apertura.IdCierreApert,
                                                AperturaCierre = 1
                                            };
                                            DB.Denominaciones.Add(denominacionApert);
                                            if (DB.SaveChanges() > 0)
                                            {
                                                //this.Hide();
                                                this.DialogResult = DialogResult.OK;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Apertura de Caja correcta, pero no se guardo detalle de las denominaciones ingresadas", "Error",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                //this.Hide();
                                                this.DialogResult = DialogResult.OK;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Apertura de Caja no se pudo realizar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            //this.Hide();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        throw;
                                    }
                                }
                            }
                            else
                            {
                                using (FrmLoading frmLoading = new FrmLoading(Wait))
                                {
                                    try
                                    {
                                        cierreApertCajas = new CierreApertCajas
                                        {
                                            Fecha = Convert.ToDateTime(Fechap),
                                            Hora = Horap,
                                            Detalles = txtDetalle.Text.Trim(),
                                            MontoEfectivoInicio = cierre.MontoEfectivoUsuarioFin,
                                            MontoEfectivoUsuarioInicio = Convert.ToDecimal(NumMontInicial.Text),
                                            MontoEfectivoFinal = Convert.ToDecimal(NumMontInicial.Text),
                                            MontoEfectivoUsuarioFin = 0,
                                            MontoVentaEfectivo = 0,
                                            MontoCompraEfectivo = 0,
                                            MontoTransf = 0,
                                            MontoCompraTransf = 0,
                                            MontoSinpe = 0,
                                            MontoCompraSinpe = 0,
                                            MontoCheque = 0,
                                            MontoCredito = 0,
                                            MontoCompraCredito = 0,
                                            FaltanteInicio = Faltante,
                                            SobranteInicio = Sobrante,
                                            FaltanteFin = 0,
                                            SobranteFin = 0,
                                            Accion = Accionp,
                                            IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                        };

                                        DB.CierreApertCajas.Add(cierreApertCajas);

                                        if (DB.SaveChanges() > 0)
                                        {
                                            MessageBox.Show("Apertura de Caja correcta!", "Apertura de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            apertura = BuscarAperturaActual();
                                            denominacionApert = new Denominaciones
                                            {

                                                MonedasCinco = Convert.ToInt32(numCinco.Value),
                                                MonedasDiez = Convert.ToInt32(numDiez.Value),
                                                MonedasVeinteCinco = Convert.ToInt32(numVeinteCinco.Value),
                                                MonedasCincuenta = Convert.ToInt32(numCincuenta.Value),
                                                MonedasCien = Convert.ToInt32(numCien.Value),
                                                MonedasQuinientos = Convert.ToInt32(numQuienientos.Value),


                                                BilleteMil = Convert.ToInt32(numMil.Value),
                                                BilleteDosMil = Convert.ToInt32(numDosMil.Value),
                                                BilleteCincoMil = Convert.ToInt32(numCincoMil.Value),
                                                BilleteDiezMil = Convert.ToInt32(numDiezMil.Value),
                                                BilleteVeinteMil = Convert.ToInt32(numVeinteMil.Value),

                                                IdCierreApert = apertura.IdCierreApert,
                                                AperturaCierre = 1
                                            };
                                            DB.Denominaciones.Add(denominacionApert);
                                            if (DB.SaveChanges() > 0)
                                            {
                                                //this.Hide();
                                                this.DialogResult = DialogResult.OK;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Apertura de Caja correcta, pero no se guardo detalle de las denominaciones ingresadas", "Error",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                //this.Hide();
                                                this.DialogResult = DialogResult.OK;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Apertura de Caja no se pudo realizar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            //this.Hide();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        throw;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    //si es nulo, busca y continuar la apertura aqui, sino encuentra un cierre anterior
                    decimal Faltante = (0 > Convert.ToDecimal(NumMontInicial.Text)) ? (0 - Convert.ToDecimal(NumMontInicial.Text)) : 0;
                    decimal Sobrante = (0 < Convert.ToDecimal(NumMontInicial.Text)) ? (Convert.ToDecimal(NumMontInicial.Text) - 0) : 0;

                    if (Faltante != 0 || Sobrante != 0)
                    {
                        DialogResult pregunta = MessageBox.Show("Posee diferencia de efectivo en caja, por favor valide." + Environment.NewLine +
                            $"Faltante: ¢ {Faltante} ." + Environment.NewLine +
                            $"Sobrante: ¢ {Sobrante} ." + Environment.NewLine +
                            "¿Deseas continuar y abrir caja con la diferencia?"
                            , "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                        if (pregunta == DialogResult.Yes)
                        {
                            using (FrmLoading frmLoading = new FrmLoading(Wait))
                            {
                                try
                                {
                                    cierreApertCajas = new CierreApertCajas
                                    {
                                        Fecha = Convert.ToDateTime(Fechap),
                                        Hora = Horap,
                                        Detalles = txtDetalle.Text.Trim(),
                                        MontoEfectivoInicio = 0,
                                        MontoEfectivoUsuarioInicio = Convert.ToDecimal(NumMontInicial.Text),
                                        MontoEfectivoFinal = Convert.ToDecimal(NumMontInicial.Text),
                                        MontoEfectivoUsuarioFin = 0,
                                        MontoVentaEfectivo = 0,
                                        MontoCompraEfectivo = 0,
                                        MontoTransf = 0,
                                        MontoCompraTransf = 0,
                                        MontoSinpe = 0,
                                        MontoCompraSinpe = 0,
                                        MontoCheque = 0,
                                        MontoCredito = 0,
                                        MontoCompraCredito = 0,
                                        FaltanteInicio = Faltante,
                                        SobranteInicio = Sobrante,
                                        FaltanteFin = 0,
                                        SobranteFin = 0,
                                        Accion = Accionp,
                                        IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                    };

                                    DB.CierreApertCajas.Add(cierreApertCajas);


                                    if (DB.SaveChanges() > 0)
                                    {
                                        MessageBox.Show("Apertura de Caja correcta!", "Apertura de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        apertura = BuscarAperturaActual();

                                        denominacionApert = new Denominaciones
                                        {

                                            MonedasCinco = Convert.ToInt32(numCinco.Value),
                                            MonedasDiez = Convert.ToInt32(numDiez.Value),
                                            MonedasVeinteCinco = Convert.ToInt32(numVeinteCinco.Value),
                                            MonedasCincuenta = Convert.ToInt32(numCincuenta.Value),
                                            MonedasCien = Convert.ToInt32(numCien.Value),
                                            MonedasQuinientos = Convert.ToInt32(numQuienientos.Value),


                                            BilleteMil = Convert.ToInt32(numMil.Value),
                                            BilleteDosMil = Convert.ToInt32(numDosMil.Value),
                                            BilleteCincoMil = Convert.ToInt32(numCincoMil.Value),
                                            BilleteDiezMil = Convert.ToInt32(numDiezMil.Value),
                                            BilleteVeinteMil = Convert.ToInt32(numVeinteMil.Value),

                                            IdCierreApert = apertura.IdCierreApert,
                                            AperturaCierre = 1
                                        };
                                        DB.Denominaciones.Add(denominacionApert);
                                        if (DB.SaveChanges() > 0)
                                        {
                                            //this.Hide();
                                            this.DialogResult = DialogResult.OK;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Apertura de Caja correcta, pero no se guardo detalle de las denominaciones ingresadas", "Error",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            //this.Hide();
                                            this.DialogResult = DialogResult.OK;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Apertura de Caja no se pudo realizar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        //this.Hide();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    throw;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Faltante == 0 && Sobrante == 0 && Convert.ToDecimal(NumMontInicial.Text) == 0)
                        {
                            using (FrmLoading frmLoading = new FrmLoading(Wait))
                            {
                                try
                                {
                                    cierreApertCajas = new CierreApertCajas
                                    {
                                        Fecha = Convert.ToDateTime(Fechap),
                                        Hora = Horap,
                                        Detalles = txtDetalle.Text.Trim(),
                                        MontoEfectivoInicio = 0,
                                        MontoEfectivoUsuarioInicio = Convert.ToDecimal(NumMontInicial.Text),
                                        MontoEfectivoFinal = Convert.ToDecimal(NumMontInicial.Text),
                                        MontoEfectivoUsuarioFin = 0,
                                        MontoVentaEfectivo = 0,
                                        MontoCompraEfectivo = 0,
                                        MontoTransf = 0,
                                        MontoCompraTransf = 0,
                                        MontoSinpe = 0,
                                        MontoCompraSinpe = 0,
                                        MontoCheque = 0,
                                        MontoCredito = 0,
                                        MontoCompraCredito = 0,
                                        FaltanteInicio = Faltante,
                                        SobranteInicio = Sobrante,
                                        FaltanteFin = 0,
                                        SobranteFin = 0,
                                        Accion = Accionp,
                                        IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                    };

                                    DB.CierreApertCajas.Add(cierreApertCajas);

                                    if (DB.SaveChanges() > 0)
                                    {
                                        MessageBox.Show("Apertura de Caja correcta!", "Apertura de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        apertura = BuscarAperturaActual();

                                        denominacionApert = new Denominaciones
                                        {

                                            MonedasCinco = Convert.ToInt32(numCinco.Value),
                                            MonedasDiez = Convert.ToInt32(numDiez.Value),
                                            MonedasVeinteCinco = Convert.ToInt32(numVeinteCinco.Value),
                                            MonedasCincuenta = Convert.ToInt32(numCincuenta.Value),
                                            MonedasCien = Convert.ToInt32(numCien.Value),
                                            MonedasQuinientos = Convert.ToInt32(numQuienientos.Value),


                                            BilleteMil = Convert.ToInt32(numMil.Value),
                                            BilleteDosMil = Convert.ToInt32(numDosMil.Value),
                                            BilleteCincoMil = Convert.ToInt32(numCincoMil.Value),
                                            BilleteDiezMil = Convert.ToInt32(numDiezMil.Value),
                                            BilleteVeinteMil = Convert.ToInt32(numVeinteMil.Value),

                                            IdCierreApert = apertura.IdCierreApert,
                                            AperturaCierre = 1
                                        };
                                        DB.Denominaciones.Add(denominacionApert);
                                        if (DB.SaveChanges() > 0)
                                        {
                                            //this.Hide();
                                            this.DialogResult = DialogResult.OK;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Apertura de Caja correcta, pero no se guardo detalle de las denominaciones ingresadas", "Error",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            //this.Hide();
                                            this.DialogResult = DialogResult.OK;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Apertura de Caja no se pudo realizar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        //this.Hide();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    throw;
                                }
                            }
                        }
                        else
                        {
                            using (FrmLoading frmLoading = new FrmLoading(Wait))
                            {
                                try
                                {
                                    cierreApertCajas = new CierreApertCajas
                                    {
                                        Fecha = Convert.ToDateTime(Fechap),
                                        Hora = Horap,
                                        Detalles = txtDetalle.Text.Trim(),
                                        MontoEfectivoInicio = 0,
                                        MontoEfectivoUsuarioInicio = Convert.ToDecimal(NumMontInicial.Text),
                                        MontoEfectivoFinal = Convert.ToDecimal(NumMontInicial.Text),
                                        MontoEfectivoUsuarioFin = 0,
                                        MontoVentaEfectivo = 0,
                                        MontoCompraEfectivo = 0,
                                        MontoTransf = 0,
                                        MontoCompraTransf = 0,
                                        MontoSinpe = 0,
                                        MontoCompraSinpe = 0,
                                        MontoCheque = 0,
                                        MontoCredito = 0,
                                        MontoCompraCredito = 0,
                                        FaltanteInicio = Faltante,
                                        SobranteInicio = Sobrante,
                                        FaltanteFin = 0,
                                        SobranteFin = 0,
                                        Accion = Accionp,
                                        IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                    };

                                    DB.CierreApertCajas.Add(cierreApertCajas);

                                    if (DB.SaveChanges() > 0)
                                    {
                                        MessageBox.Show("Apertura de Caja correcta!", "Apertura de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        apertura = BuscarAperturaActual();

                                        denominacionApert = new Denominaciones
                                        {

                                            MonedasCinco = Convert.ToInt32(numCinco.Value),
                                            MonedasDiez = Convert.ToInt32(numDiez.Value),
                                            MonedasVeinteCinco = Convert.ToInt32(numVeinteCinco.Value),
                                            MonedasCincuenta = Convert.ToInt32(numCincuenta.Value),
                                            MonedasCien = Convert.ToInt32(numCien.Value),
                                            MonedasQuinientos = Convert.ToInt32(numQuienientos.Value),


                                            BilleteMil = Convert.ToInt32(numMil.Value),
                                            BilleteDosMil = Convert.ToInt32(numDosMil.Value),
                                            BilleteCincoMil = Convert.ToInt32(numCincoMil.Value),
                                            BilleteDiezMil = Convert.ToInt32(numDiezMil.Value),
                                            BilleteVeinteMil = Convert.ToInt32(numVeinteMil.Value),

                                            IdCierreApert = apertura.IdCierreApert,
                                            AperturaCierre = 1
                                        };
                                        DB.Denominaciones.Add(denominacionApert);
                                        if (DB.SaveChanges() > 0)
                                        {
                                            //this.Hide();
                                            this.DialogResult = DialogResult.OK;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Apertura de Caja correcta, pero no se guardo detalle de las denominaciones ingresadas", "Error",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            //this.Hide();
                                            this.DialogResult = DialogResult.OK;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Apertura de Caja no se pudo realizar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        //this.Hide();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    throw;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            } finally
            {
                Cursor.Current = Cursors.Default;

            }
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //this.Hide();
            this.DialogResult = DialogResult.Cancel;
        }

        private void tmrFechaHora_Tick(object sender, EventArgs e)
        {
            string hora = DateTime.Now.ToShortTimeString();

            Horap = hora;
        }

        private void NumMontInicial_MouseClick(object sender, MouseEventArgs e)
        {
            //
        }

        private void NumMontInicial_ValueChanged(object sender, EventArgs e)
        {
            //
        }


        private int CalcularTotalMonedas()
        {
            lblCinco.Text = Convert.ToString(Convert.ToInt32(numCinco.Value) * 5);
            lblDiez.Text = Convert.ToString(Convert.ToInt32(numDiez.Value) * 10);
            lblVeinteCinco.Text = Convert.ToString(Convert.ToInt32(numVeinteCinco.Value) * 25);
            lblCincuenta.Text = Convert.ToString(Convert.ToInt32(numCincuenta.Value) * 50);
            lblCien.Text = Convert.ToString(Convert.ToInt32(numCien.Value) * 100);
            lblQuinientos.Text = Convert.ToString(Convert.ToInt32(numQuienientos.Value) * 500);

            int total = (Convert.ToInt32(numCinco.Value) * 5) + (Convert.ToInt32(numDiez.Value) * 10) + (Convert.ToInt32(numVeinteCinco.Value) * 25) +
                (Convert.ToInt32(numCincuenta.Value) * 50) + (Convert.ToInt32(numCien.Value) * 100) + (Convert.ToInt32(numQuienientos.Value) * 500);

            lblTotalMonedas.Text = Convert.ToString(total);

            return total;
        }

        private int CalcularTotalBilletes()
        {
            lblMil.Text = Convert.ToString(Convert.ToInt32(numMil.Value) * 1000);
            lblDosMil.Text = Convert.ToString(Convert.ToInt32(numDosMil.Value) * 2000);
            lblCincoMil.Text = Convert.ToString(Convert.ToInt32(numCincoMil.Value) * 5000);
            lblDiezMil.Text = Convert.ToString(Convert.ToInt32(numDiezMil.Value) * 10000);
            lblVeinteMil.Text = Convert.ToString(Convert.ToInt32(numVeinteMil.Value) * 20000);
            int total = (Convert.ToInt32(numMil.Value) * 1000) + (Convert.ToInt32(numDosMil.Value) * 2000) + (Convert.ToInt32(numCincoMil.Value) * 5000) +
                (Convert.ToInt32(numDiezMil.Value) * 10000) + (Convert.ToInt32(numVeinteMil.Value) * 20000);
            lblTotalBillete.Text = Convert.ToString(total);
            return total;
        }
        private int CalcularTotal()
        {
            int total = CalcularTotalMonedas() + CalcularTotalBilletes();
            lblTotal.Text = Convert.ToString(total);
            NumMontInicial.Text = Convert.ToString(total);
            return total;
        }
        private void resetDenominaciones()
        {
            numCinco.Value = 0;
            numDiez.Value = 0;
            numVeinteCinco.Value = 0;
            numCincuenta.Value = 0;
            numCien.Value = 0;
            numQuienientos.Value = 0;

            numMil.Value = 0;
            numDosMil.Value = 0;
            numCincoMil.Value = 0;
            numDiezMil.Value = 0;
            numVeinteMil.Value = 0;

            CalcularTotal();
        }

        private void NumMontInicial_Click(object sender, EventArgs e)
        {
            Size = new Size(1161, 418);
            //NumMontInicial.Text = total.ToString();    
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(NumMontInicial.Text.Trim()) > 0)
            {
                DialogResult respuesta = MessageBox.Show("¿Deseas borrar las denominaciones ingresadas?","Denominaciones", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    resetDenominaciones();
                    Size = new Size(517, 418);
                }
                else
                {
                    Size = new Size(517, 418);
                }
            }
            else
            {
                Size = new Size(517, 418);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Size = new Size(517, 418);
        }

        private void numCinco_ValueChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void numDiez_ValueChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void numVeinteCinco_ValueChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void numCincuenta_ValueChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void numCien_ValueChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void numQuienientos_ValueChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void numMil_ValueChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void numDosMil_ValueChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void numCincoMil_ValueChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void numDiezMil_ValueChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void numVeinteMil_ValueChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }
    }
}
