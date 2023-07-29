namespace Agregados.Forms.Providers
{
    partial class FrmProviderSearch
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvListaProveedores = new System.Windows.Forms.DataGridView();
            this.CIdProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIdentificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CTelefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CTipoProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIdEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBuscarId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.imgClean = new System.Windows.Forms.PictureBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaProveedores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgClean)).BeginInit();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(402, 66);
            this.txtName.MaxLength = 255;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(322, 20);
            this.txtName.TabIndex = 28;
            this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(382, 20);
            this.label2.TabIndex = 27;
            this.label2.Text = "Buscar Proveedor por Nombre o Identificación:";
            // 
            // dgvListaProveedores
            // 
            this.dgvListaProveedores.AllowUserToAddRows = false;
            this.dgvListaProveedores.AllowUserToDeleteRows = false;
            this.dgvListaProveedores.AllowUserToOrderColumns = true;
            this.dgvListaProveedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaProveedores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CIdProveedor,
            this.CNombre,
            this.CIdentificacion,
            this.CTelefono,
            this.CTipoProveedor,
            this.CIdEstado});
            this.dgvListaProveedores.Location = new System.Drawing.Point(15, 108);
            this.dgvListaProveedores.MultiSelect = false;
            this.dgvListaProveedores.Name = "dgvListaProveedores";
            this.dgvListaProveedores.ReadOnly = true;
            this.dgvListaProveedores.RowHeadersVisible = false;
            this.dgvListaProveedores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListaProveedores.Size = new System.Drawing.Size(772, 120);
            this.dgvListaProveedores.TabIndex = 26;
            this.dgvListaProveedores.VirtualMode = true;
            // 
            // CIdProveedor
            // 
            this.CIdProveedor.DataPropertyName = "IdProveedor";
            this.CIdProveedor.FillWeight = 80F;
            this.CIdProveedor.HeaderText = "Cod.";
            this.CIdProveedor.MinimumWidth = 80;
            this.CIdProveedor.Name = "CIdProveedor";
            this.CIdProveedor.ReadOnly = true;
            this.CIdProveedor.Width = 80;
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
            // CTipoProveedor
            // 
            this.CTipoProveedor.DataPropertyName = "TipoProveedor";
            this.CTipoProveedor.FillWeight = 120F;
            this.CTipoProveedor.HeaderText = "Tipo Proveedor";
            this.CTipoProveedor.MinimumWidth = 120;
            this.CTipoProveedor.Name = "CTipoProveedor";
            this.CTipoProveedor.ReadOnly = true;
            this.CTipoProveedor.Width = 120;
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
            this.txtBuscarId.Location = new System.Drawing.Point(402, 33);
            this.txtBuscarId.MaxLength = 10;
            this.txtBuscarId.Name = "txtBuscarId";
            this.txtBuscarId.Size = new System.Drawing.Size(322, 20);
            this.txtBuscarId.TabIndex = 25;
            this.txtBuscarId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBuscarId.TextChanged += new System.EventHandler(this.txtBuscarId_TextChanged);
            this.txtBuscarId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarId_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 20);
            this.label1.TabIndex = 24;
            this.label1.Text = "Buscar Proveedor por Código:";
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.BackColor = System.Drawing.Color.ForestGreen;
            this.btnSeleccionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeleccionar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeleccionar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSeleccionar.Image = global::Agregados.Properties.Resources.lupa1;
            this.btnSeleccionar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSeleccionar.Location = new System.Drawing.Point(402, 248);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(385, 40);
            this.btnSeleccionar.TabIndex = 23;
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.UseVisualStyleBackColor = false;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // imgClean
            // 
            this.imgClean.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgClean.Image = global::Agregados.Properties.Resources.clean;
            this.imgClean.Location = new System.Drawing.Point(747, 35);
            this.imgClean.Name = "imgClean";
            this.imgClean.Size = new System.Drawing.Size(56, 53);
            this.imgClean.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgClean.TabIndex = 29;
            this.imgClean.TabStop = false;
            this.imgClean.Click += new System.EventHandler(this.imgClean_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Salmon;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancelar.Image = global::Agregados.Properties.Resources.cancelar1;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(15, 248);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(381, 40);
            this.btnCancelar.TabIndex = 22;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FrmProviderSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 300);
            this.Controls.Add(this.imgClean);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvListaProveedores);
            this.Controls.Add(this.txtBuscarId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSeleccionar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmProviderSearch";
            this.Text = "Búsqueda de Proveedores";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmProviderSearch_FormClosing);
            this.Load += new System.EventHandler(this.FrmProviderSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaProveedores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgClean)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgClean;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvListaProveedores;
        private System.Windows.Forms.TextBox txtBuscarId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIdProveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIdentificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn CTelefono;
        private System.Windows.Forms.DataGridViewTextBoxColumn CTipoProveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIdEstado;
    }
}