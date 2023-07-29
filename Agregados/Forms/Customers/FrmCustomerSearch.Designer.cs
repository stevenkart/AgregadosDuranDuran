namespace Agregados.Forms.Customers
{
    partial class FrmCustomerSearch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCustomerSearch));
            this.dgvListaClientes = new System.Windows.Forms.DataGridView();
            this.CIdCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIdentificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CTelefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CTipoCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIdEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBuscarId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.imgClean = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgClean)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvListaClientes
            // 
            this.dgvListaClientes.AllowUserToAddRows = false;
            this.dgvListaClientes.AllowUserToDeleteRows = false;
            this.dgvListaClientes.AllowUserToOrderColumns = true;
            this.dgvListaClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaClientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CIdCliente,
            this.CNombre,
            this.CIdentificacion,
            this.CTelefono,
            this.CTipoCliente,
            this.CIdEstado});
            this.dgvListaClientes.Location = new System.Drawing.Point(30, 95);
            this.dgvListaClientes.MultiSelect = false;
            this.dgvListaClientes.Name = "dgvListaClientes";
            this.dgvListaClientes.ReadOnly = true;
            this.dgvListaClientes.RowHeadersVisible = false;
            this.dgvListaClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListaClientes.Size = new System.Drawing.Size(772, 120);
            this.dgvListaClientes.TabIndex = 16;
            this.dgvListaClientes.VirtualMode = true;
            // 
            // CIdCliente
            // 
            this.CIdCliente.DataPropertyName = "IdCliente";
            this.CIdCliente.FillWeight = 80F;
            this.CIdCliente.HeaderText = "Cod.";
            this.CIdCliente.MinimumWidth = 80;
            this.CIdCliente.Name = "CIdCliente";
            this.CIdCliente.ReadOnly = true;
            this.CIdCliente.Width = 80;
            // 
            // CNombre
            // 
            this.CNombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CNombre.DataPropertyName = "Nombre";
            this.CNombre.FillWeight = 200F;
            this.CNombre.HeaderText = "Nombre";
            this.CNombre.MinimumWidth = 120;
            this.CNombre.Name = "CNombre";
            this.CNombre.ReadOnly = true;
            // 
            // CIdentificacion
            // 
            this.CIdentificacion.DataPropertyName = "Identificacion";
            this.CIdentificacion.FillWeight = 160F;
            this.CIdentificacion.HeaderText = "Identificación";
            this.CIdentificacion.MinimumWidth = 160;
            this.CIdentificacion.Name = "CIdentificacion";
            this.CIdentificacion.ReadOnly = true;
            this.CIdentificacion.Width = 160;
            // 
            // CTelefono
            // 
            this.CTelefono.DataPropertyName = "Telefono";
            this.CTelefono.FillWeight = 120F;
            this.CTelefono.HeaderText = "Teléfono";
            this.CTelefono.MinimumWidth = 120;
            this.CTelefono.Name = "CTelefono";
            this.CTelefono.ReadOnly = true;
            this.CTelefono.Width = 120;
            // 
            // CTipoCliente
            // 
            this.CTipoCliente.DataPropertyName = "TipoCliente";
            this.CTipoCliente.FillWeight = 120F;
            this.CTipoCliente.HeaderText = "TipoCliente";
            this.CTipoCliente.MinimumWidth = 120;
            this.CTipoCliente.Name = "CTipoCliente";
            this.CTipoCliente.ReadOnly = true;
            this.CTipoCliente.Width = 120;
            // 
            // CIdEstado
            // 
            this.CIdEstado.DataPropertyName = "IdEstado";
            this.CIdEstado.FillWeight = 120F;
            this.CIdEstado.HeaderText = "Estado";
            this.CIdEstado.MinimumWidth = 120;
            this.CIdEstado.Name = "CIdEstado";
            this.CIdEstado.ReadOnly = true;
            this.CIdEstado.Width = 120;
            // 
            // txtBuscarId
            // 
            this.txtBuscarId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuscarId.Location = new System.Drawing.Point(389, 22);
            this.txtBuscarId.MaxLength = 10;
            this.txtBuscarId.Name = "txtBuscarId";
            this.txtBuscarId.Size = new System.Drawing.Size(351, 20);
            this.txtBuscarId.TabIndex = 15;
            this.txtBuscarId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBuscarId.TextChanged += new System.EventHandler(this.txtBuscarId_TextChanged);
            this.txtBuscarId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarId_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(309, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Buscar Cliente por Código de Cliente:";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(389, 55);
            this.txtName.MaxLength = 255;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(351, 20);
            this.txtName.TabIndex = 18;
            this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(357, 20);
            this.label2.TabIndex = 17;
            this.label2.Text = "Buscar Cliente por Nombre o Identificación:";
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnSeleccionar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSeleccionar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeleccionar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSeleccionar.Image = ((System.Drawing.Image)(resources.GetObject("btnSeleccionar.Image")));
            this.btnSeleccionar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSeleccionar.Location = new System.Drawing.Point(417, 235);
            this.btnSeleccionar.Margin = new System.Windows.Forms.Padding(15);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnSeleccionar.Size = new System.Drawing.Size(385, 42);
            this.btnSeleccionar.TabIndex = 13;
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.UseVisualStyleBackColor = false;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Salmon;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancelar.Image = global::Agregados.Properties.Resources.cancelar;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(30, 235);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnCancelar.Size = new System.Drawing.Size(381, 42);
            this.btnCancelar.TabIndex = 12;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // imgClean
            // 
            this.imgClean.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgClean.Image = global::Agregados.Properties.Resources.clean;
            this.imgClean.Location = new System.Drawing.Point(762, 22);
            this.imgClean.Name = "imgClean";
            this.imgClean.Size = new System.Drawing.Size(56, 53);
            this.imgClean.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgClean.TabIndex = 21;
            this.imgClean.TabStop = false;
            this.imgClean.Click += new System.EventHandler(this.imgClean_Click);
            // 
            // FrmCustomerSearch
            // 
            this.AcceptButton = this.btnSeleccionar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(839, 300);
            this.Controls.Add(this.imgClean);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvListaClientes);
            this.Controls.Add(this.txtBuscarId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSeleccionar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(855, 339);
            this.MinimumSize = new System.Drawing.Size(855, 339);
            this.Name = "FrmCustomerSearch";
            this.Text = "Búsqueda de Clientes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCustomerSearch_FormClosing);
            this.Load += new System.EventHandler(this.FrmCustomerSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgClean)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvListaClientes;
        private System.Windows.Forms.TextBox txtBuscarId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIdCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIdentificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn CTelefono;
        private System.Windows.Forms.DataGridViewTextBoxColumn CTipoCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIdEstado;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox imgClean;
    }
}