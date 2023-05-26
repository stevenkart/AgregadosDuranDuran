using Agregados.Forms.Customers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Forms.Bills
{
    public partial class FrmBillAdd : Form
    {
        //variables del form
        AgregadosEntities DB;
        Factura factura;
        DetalleFact detalleFact;
        public DataTable DtLista { get; set; }
        public FrmBillAdd()
        {
            InitializeComponent();
            DB = new AgregadosEntities();
            factura = new Factura();    
            detalleFact = new DetalleFact();

            DtLista = new DataTable();
        }

        private void FrmBillAdd_Load(object sender, EventArgs e)
        {
            lblTypeFact.Visible = false;
            dateFinal.Visible = false;
            lblDate.Text = DateTime.Now.ToShortDateString();
            CargarTiposFactura();


        }

        //carga Cbox Tipo Factua
        private void CargarTiposFactura()
        {

            //Metodo que permite llamar y obtener los datos filtrados de los materiales y mostrarlos en el comboBox
            var dt = DB.TipoFacturas.ToList();

            CboxTypeBill.ValueMember = "IdTipo";
            CboxTypeBill.DisplayMember = "Tipo";
            CboxTypeBill.DataSource = dt;
            CboxTypeBill.SelectedIndex = -1;
        }
        //cambio tipo factura, muestra la info de credito (FECHA)
        private void CboxTypeBill_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(CboxTypeBill.SelectedValue) == 1)
            {
                lblTypeFact.Visible = false;
                dateFinal.Visible = false;
            }
            else
            {
                if (Convert.ToInt32(CboxTypeBill.SelectedValue) == 2)
                {
                    lblTypeFact.Visible = true;
                    dateFinal.Visible = true;
                }
            }
        }

        private void FrmBillAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();
        }

        private void pictureExit_Click(object sender, EventArgs e)
        {
            FrmPrincipalMDI frmPrincipalMDI = new FrmPrincipalMDI();
            frmPrincipalMDI.Show();
            this.Hide();

        }

        private void pictureSearchClient_Click(object sender, EventArgs e)
        {
            Form FrmCustomerSearch = new FrmCustomerSearch();

            DialogResult resp = FrmCustomerSearch.ShowDialog();

            if (resp == DialogResult.OK)
            {
                if (DtLista.Rows.Count > 0)
                {
                    /*
                    dgvMaterials.DataSource = DtLista;
                    dgvMaterials.SelectAll();
                    //dgvLista.ClearSelection();

                    
                    Totalizar();
                    activarModificarQuitar();
                    */
                    MessageBox.Show("Se obtuvo el ID", "Éxito", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("No se pudo obtener la fila", "Error Inesperado", MessageBoxButtons.OK);
                }
            }
        }

        private void txtNumClient_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNumClient.Text.Trim()) && txtNumClient.TextLength > 0)
            {
                int num = Convert.ToInt32(txtNumClient.Text.Trim());
                string name = DB.Clientes.Where((x) => x.IdCliente == num).Select((x) => x.Nombre).FirstOrDefault();

                if (!string.IsNullOrEmpty(name))
                {
                    txtClient.Text = name.ToString();
                }
            }
        }
    }
}
