using Agregados.Forms.Loading;
using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Forms.Cashiers
{
    public partial class FrmCashierClose : Form
    {
        //variables del form
        AgregadosEntities DB;
        CierreApertCajas cierreApertCajas;

        CierreApertCajas apertura; // valor termporal apertura

        string Fechap;
        string Horap;
        int Accionp;

        public FrmCashierClose()
        {
            InitializeComponent();

            DB = new AgregadosEntities();
            cierreApertCajas = new CierreApertCajas();
            apertura = new CierreApertCajas();


        }

        private void FrmCashierClose_Load(object sender, EventArgs e)
        {
            tmrFechaHora.Enabled = true;
            Fechap = DateTime.Now.Date.ToShortDateString();
            txtFecha.Text = Fechap;
            txtUser.Text = Globals.MyGlobalUser.NombreUsuario.ToString();
            txtDetalle.Text = null;
            Accionp = 2; // 1 = apertura  & 2 = a cierre

            if (BuscarAperturaActual() != null)
            {
                txtDetalle.MaxLength = 1000 - (apertura.Detalles.Length); // valida que luego al unir detalle entrada con detalle de salida, no superen los 1000 caracteres.
                txtMontoVtaTransf.Text = string.Format("¢ {0:N2}", apertura.MontoTransf);
                txtMontoVtaSinpe.Text = string.Format("¢ {0:N2}", apertura.MontoSinpe);
                txtMontoVtaCheque.Text = string.Format("¢ {0:N2}", apertura.MontoCheque);
                txtMontoVtaCredito.Text = string.Format("¢ {0:N2}", apertura.MontoCredito);

                txtMontoCompraTransf.Text = string.Format("¢ {0:N2}", apertura.MontoCompraTransf);
                txtMontoCompraSinpe.Text = string.Format("¢ {0:N2}", apertura.MontoCompraSinpe);
            }
        }

        //tiempo loading
        void Wait()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(5);
            }
        }

    
        //busca la apertura 
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
            if (BuscarAperturaActual() != null && apertura.IdUsuario == Globals.MyGlobalUser.IdUsuario)
            {
               
                //efectivo
                decimal Faltante = (apertura.MontoEfectivoFinal > NumMontInicial.Value) ? (apertura.MontoEfectivoFinal - NumMontInicial.Value) : 0;
                decimal Sobrante = (apertura.MontoEfectivoFinal < NumMontInicial.Value) ? (NumMontInicial.Value - apertura.MontoEfectivoFinal) : 0;

                if (Faltante != 0 || Sobrante != 0)
                {
                    DialogResult pregunta = MessageBox.Show("Posee diferencia de efectivo en caja, por favor valide." + Environment.NewLine +
                        $"Faltante: ¢ {Faltante} ." + Environment.NewLine +
                        $"Sobrante: ¢ {Sobrante} ." + Environment.NewLine +
                        "¿Deseas continuar y cerrar caja con la diferencia?"
                        , "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                    if (pregunta == DialogResult.Yes)
                    {
                        apertura.FechaSalida = Convert.ToDateTime(Fechap);
                        apertura.HoraSalida = Horap;
                        apertura.Detalles = apertura.Detalles + Environment.NewLine + txtDetalle.Text.Trim();
                        apertura.MontoEfectivoUsuarioFin = NumMontInicial.Value;
                        //lo que son monto por transferencia, cheque y credito 
                        //sistema los valida automaticamente ya que conforme se facture por ese medio de pago, y no efectivo
                        //sistema arroja resultado calculado.
                        apertura.FaltanteFin = Faltante;
                        apertura.SobranteFin = Sobrante;
                        apertura.Accion = Accionp;


                        DB.Entry(apertura).State = EntityState.Modified;


                        if (DB.SaveChanges() > 0)
                        {
                            MessageBox.Show("Cierre de Caja correcto!", "Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //this.Hide();
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("Cierre de Caja no se pudo realizar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //this.Hide();
                        }
                    }
                }
                else
                {

                    apertura.FechaSalida = Convert.ToDateTime(Fechap);
                    apertura.HoraSalida = Horap;
                    apertura.Detalles = apertura.Detalles + Environment.NewLine + txtDetalle.Text.Trim();
                    apertura.MontoEfectivoUsuarioFin = NumMontInicial.Value;
                    //lo que son monto por transferencia, cheque y credito 
                    //sistema los valida automaticamente ya que conforme se facture por ese medio de pago, y no efectivo
                    //sistema arroja resultado calculado.
                    apertura.FaltanteFin = Faltante;
                    apertura.SobranteFin = Sobrante;
                    apertura.Accion = Accionp;


                    DB.Entry(apertura).State = EntityState.Modified;


                    if (DB.SaveChanges() > 0)
                    {
                        MessageBox.Show("Cierre de Caja correcto!", "Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //this.Hide();
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Cierre de Caja no se pudo realizar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //this.Hide();
                    }
                }
            }
            else
            {
                MessageBox.Show("Ya hay una caja abierta, no es posible tener otra caja abierta simultaneamente; ya que otro usuario esta manejando el efectivo..",
                   "Apertura de Caja Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

    
    }
}
