using Agregados.Forms.Loading;
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

namespace Agregados.Forms.Cashiers
{
    public partial class FrmCashierOpen : Form
    {
        //variables del form
        AgregadosEntities DB;
        CierreApertCajas cierreApertCajas;

        CierreApertCajas apertura; // valor termporal apertura
        CierreApertCajas cierre; // valor termporal cierre

        string Fechap;
        string Horap;
        int Accionp;
        
        public FrmCashierOpen()
        {
            InitializeComponent();
            DB = new AgregadosEntities();
            cierreApertCajas = new CierreApertCajas();
            apertura = new CierreApertCajas();
            cierre = new CierreApertCajas();
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
            catch (Exception)
            {
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
            catch (Exception)
            {
                throw;
            }
        }


        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (BuscarCierreAnterior() != null)
            {
                if (BuscarAperturaActual() != null)
                {
                    MessageBox.Show("Ya hay una caja abierta, no es posible tener otra caja abierta simultaneamente.",
                        "Apertura de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    decimal Faltante = (cierre.MontoEfectivoUsuarioFin > NumMontInicial.Value) ? (cierre.MontoEfectivoUsuarioFin - NumMontInicial.Value) : 0;
                    decimal Sobrante = (cierre.MontoEfectivoUsuarioFin < NumMontInicial.Value) ? (NumMontInicial.Value - cierre.MontoEfectivoUsuarioFin) : 0;

                    if (Faltante != 0 || Sobrante != 0)
                    {
                        MessageBox.Show("Posee diferencia de efectivo en caja, por favor valide." + Environment.NewLine +
                            $"Faltante: {Faltante} ." + Environment.NewLine +
                            $"Sobrante: {Sobrante} ."
                            , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                                    MontoEfectivoUsuarioInicio = NumMontInicial.Value,
                                    MontoEfectivoFinal = 0,
                                    MontoEfectivoUsuarioFin = 0,
                                    MontoTransf = 0,
                                    MontoCompraTransf = 0,
                                    MontoSinpe = 0,
                                    MontoCompraSinpe = 0,
                                    MontoCheque = 0,
                                    MontoCredito = 0,
                                    MontoCompraCredito = 0,
                                    Faltante = (cierre.MontoEfectivoUsuarioFin > NumMontInicial.Value) ? (cierre.MontoEfectivoUsuarioFin - NumMontInicial.Value) : 0,
                                    Sobrante = (cierre.MontoEfectivoUsuarioFin < NumMontInicial.Value) ? (NumMontInicial.Value - cierre.MontoEfectivoUsuarioFin) : 0,
                                    Accion = Accionp,
                                    IdUsuario = Globals.MyGlobalUser.IdUsuario,
                                };
                                DB.CierreApertCajas.Add(cierreApertCajas);

                                if (DB.SaveChanges() > 0)
                                {
                                    MessageBox.Show("Apertura de Caja correcta!", "Apertura de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Apertura de Caja no se pudo realizar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    this.Hide();
                                }
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }
                    }
                }
            }
            else
            {
                if (BuscarAperturaActual() != null)
                {
                    MessageBox.Show("Ya hay una caja abierta, no es posible tener otra caja abierta simultaneamente.",
                        "Apertura de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                MontoEfectivoUsuarioInicio = NumMontInicial.Value,
                                MontoEfectivoFinal = 0,
                                MontoEfectivoUsuarioFin = 0,
                                MontoTransf = 0,
                                MontoCompraTransf = 0,
                                MontoSinpe = 0,
                                MontoCompraSinpe = 0,
                                MontoCheque = 0,
                                MontoCredito = 0,
                                MontoCompraCredito = 0,
                                Faltante = 0,
                                Sobrante = NumMontInicial.Value,
                                Accion = Accionp,
                                IdUsuario = Globals.MyGlobalUser.IdUsuario,
                            };
                            DB.CierreApertCajas.Add(cierreApertCajas);

                            if (DB.SaveChanges() > 0)
                            {
                                MessageBox.Show("Apertura de Caja correcta!", "Apertura de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Apertura de Caja no se pudo realizar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Hide();
                            }
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void tmrFechaHora_Tick(object sender, EventArgs e)
        {
            string hora = DateTime.Now.ToShortTimeString();

            Horap = hora;
        }
    }
}
