namespace Agregados.Forms.Materials
{
    partial class FrmMaterialSearch
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvListaMateriales = new System.Windows.Forms.DataGridView();
            this.CIdMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNombreMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCantidadMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMinimos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIdEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBuscarId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.imgClean = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TxtPrecioUnitario = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtSubTotal = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtTotal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtIVA = new System.Windows.Forms.TextBox();
            this.NudCantidad = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaMateriales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgClean)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudCantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(251, 57);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(472, 20);
            this.txtName.TabIndex = 28;
            this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(237, 20);
            this.label2.TabIndex = 27;
            this.label2.Text = "Buscar Material por Nombre:";
            // 
            // dgvListaMateriales
            // 
            this.dgvListaMateriales.AllowUserToAddRows = false;
            this.dgvListaMateriales.AllowUserToDeleteRows = false;
            this.dgvListaMateriales.AllowUserToOrderColumns = true;
            this.dgvListaMateriales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaMateriales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CIdMaterial,
            this.CNombreMaterial,
            this.CCantidadMaterial,
            this.CMinimos,
            this.CPrecio,
            this.CIdEstado});
            this.dgvListaMateriales.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvListaMateriales.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvListaMateriales.Location = new System.Drawing.Point(12, 107);
            this.dgvListaMateriales.MultiSelect = false;
            this.dgvListaMateriales.Name = "dgvListaMateriales";
            this.dgvListaMateriales.ReadOnly = true;
            this.dgvListaMateriales.RowHeadersVisible = false;
            this.dgvListaMateriales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListaMateriales.Size = new System.Drawing.Size(772, 181);
            this.dgvListaMateriales.TabIndex = 1;
            this.dgvListaMateriales.VirtualMode = true;
            this.dgvListaMateriales.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListaMateriales_CellClick);
            // 
            // CIdMaterial
            // 
            this.CIdMaterial.DataPropertyName = "IdMaterial";
            this.CIdMaterial.FillWeight = 80F;
            this.CIdMaterial.HeaderText = "Cod.";
            this.CIdMaterial.MinimumWidth = 80;
            this.CIdMaterial.Name = "CIdMaterial";
            this.CIdMaterial.ReadOnly = true;
            this.CIdMaterial.Width = 80;
            // 
            // CNombreMaterial
            // 
            this.CNombreMaterial.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CNombreMaterial.DataPropertyName = "NombreMaterial";
            this.CNombreMaterial.FillWeight = 200F;
            this.CNombreMaterial.HeaderText = "Nombre";
            this.CNombreMaterial.MinimumWidth = 120;
            this.CNombreMaterial.Name = "CNombreMaterial";
            this.CNombreMaterial.ReadOnly = true;
            // 
            // CCantidadMaterial
            // 
            this.CCantidadMaterial.DataPropertyName = "CantidadMaterial";
            this.CCantidadMaterial.FillWeight = 160F;
            this.CCantidadMaterial.HeaderText = "Cantidad Disponible";
            this.CCantidadMaterial.MaxInputLength = 100;
            this.CCantidadMaterial.MinimumWidth = 160;
            this.CCantidadMaterial.Name = "CCantidadMaterial";
            this.CCantidadMaterial.ReadOnly = true;
            this.CCantidadMaterial.Width = 160;
            // 
            // CMinimos
            // 
            this.CMinimos.DataPropertyName = "Minimos";
            this.CMinimos.FillWeight = 120F;
            this.CMinimos.HeaderText = "Mínimos";
            this.CMinimos.MinimumWidth = 120;
            this.CMinimos.Name = "CMinimos";
            this.CMinimos.ReadOnly = true;
            this.CMinimos.Width = 120;
            // 
            // CPrecio
            // 
            this.CPrecio.DataPropertyName = "Precio";
            this.CPrecio.FillWeight = 130F;
            this.CPrecio.HeaderText = "Precio por m³";
            this.CPrecio.MinimumWidth = 130;
            this.CPrecio.Name = "CPrecio";
            this.CPrecio.ReadOnly = true;
            this.CPrecio.Width = 130;
            // 
            // CIdEstado
            // 
            this.CIdEstado.DataPropertyName = "IdEstado";
            this.CIdEstado.FillWeight = 120F;
            this.CIdEstado.HeaderText = "Estado";
            this.CIdEstado.MinimumWidth = 120;
            this.CIdEstado.Name = "CIdEstado";
            this.CIdEstado.ReadOnly = true;
            this.CIdEstado.Visible = false;
            this.CIdEstado.Width = 120;
            // 
            // txtBuscarId
            // 
            this.txtBuscarId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuscarId.Location = new System.Drawing.Point(251, 24);
            this.txtBuscarId.Name = "txtBuscarId";
            this.txtBuscarId.Size = new System.Drawing.Size(472, 20);
            this.txtBuscarId.TabIndex = 25;
            this.txtBuscarId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBuscarId.TextChanged += new System.EventHandler(this.txtBuscarId_TextChanged);
            this.txtBuscarId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarId_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 20);
            this.label1.TabIndex = 24;
            this.label1.Text = "Buscar Material por Código:";
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.BackColor = System.Drawing.Color.DarkGreen;
            this.btnSeleccionar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSeleccionar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeleccionar.ForeColor = System.Drawing.Color.Transparent;
            this.btnSeleccionar.Location = new System.Drawing.Point(592, 463);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(193, 30);
            this.btnSeleccionar.TabIndex = 23;
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.UseVisualStyleBackColor = false;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.OrangeRed;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancelar.Location = new System.Drawing.Point(427, 463);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(150, 30);
            this.btnCancelar.TabIndex = 22;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // imgClean
            // 
            this.imgClean.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgClean.Image = global::Agregados.Properties.Resources.clean;
            this.imgClean.Location = new System.Drawing.Point(729, 24);
            this.imgClean.Name = "imgClean";
            this.imgClean.Size = new System.Drawing.Size(56, 53);
            this.imgClean.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgClean.TabIndex = 29;
            this.imgClean.TabStop = false;
            this.imgClean.Click += new System.EventHandler(this.imgClean_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.TxtPrecioUnitario);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.TxtSubTotal);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.TxtTotal);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.TxtIVA);
            this.panel1.Controls.Add(this.NudCantidad);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Location = new System.Drawing.Point(12, 309);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(771, 123);
            this.panel1.TabIndex = 30;
            // 
            // TxtPrecioUnitario
            // 
            this.TxtPrecioUnitario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPrecioUnitario.ForeColor = System.Drawing.Color.ForestGreen;
            this.TxtPrecioUnitario.Location = new System.Drawing.Point(121, 62);
            this.TxtPrecioUnitario.MaxLength = 10;
            this.TxtPrecioUnitario.Name = "TxtPrecioUnitario";
            this.TxtPrecioUnitario.Size = new System.Drawing.Size(151, 26);
            this.TxtPrecioUnitario.TabIndex = 12;
            this.TxtPrecioUnitario.Text = "0";
            this.TxtPrecioUnitario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtPrecioUnitario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPrecioUnitario_KeyPress);
            this.TxtPrecioUnitario.Leave += new System.EventHandler(this.TxtPrecioUnitario_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(291, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "SubTotal";
            // 
            // TxtSubTotal
            // 
            this.TxtSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSubTotal.ForeColor = System.Drawing.Color.ForestGreen;
            this.TxtSubTotal.Location = new System.Drawing.Point(285, 63);
            this.TxtSubTotal.MaxLength = 10;
            this.TxtSubTotal.Name = "TxtSubTotal";
            this.TxtSubTotal.ReadOnly = true;
            this.TxtSubTotal.Size = new System.Drawing.Size(134, 26);
            this.TxtSubTotal.TabIndex = 10;
            this.TxtSubTotal.Text = "0";
            this.TxtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(655, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "TOTAL";
            // 
            // TxtTotal
            // 
            this.TxtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTotal.ForeColor = System.Drawing.Color.ForestGreen;
            this.TxtTotal.Location = new System.Drawing.Point(622, 62);
            this.TxtTotal.MaxLength = 10;
            this.TxtTotal.Name = "TxtTotal";
            this.TxtTotal.ReadOnly = true;
            this.TxtTotal.Size = new System.Drawing.Size(134, 26);
            this.TxtTotal.TabIndex = 8;
            this.TxtTotal.Text = "0";
            this.TxtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(134, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "PRECIO UNIT.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(484, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "IVA 13%";
            // 
            // TxtIVA
            // 
            this.TxtIVA.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtIVA.ForeColor = System.Drawing.Color.ForestGreen;
            this.TxtIVA.Location = new System.Drawing.Point(454, 63);
            this.TxtIVA.MaxLength = 10;
            this.TxtIVA.Name = "TxtIVA";
            this.TxtIVA.ReadOnly = true;
            this.TxtIVA.Size = new System.Drawing.Size(134, 26);
            this.TxtIVA.TabIndex = 4;
            this.TxtIVA.Text = "0";
            this.TxtIVA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // NudCantidad
            // 
            this.NudCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NudCantidad.Location = new System.Drawing.Point(24, 62);
            this.NudCantidad.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.NudCantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NudCantidad.Name = "NudCantidad";
            this.NudCantidad.Size = new System.Drawing.Size(91, 26);
            this.NudCantidad.TabIndex = 2;
            this.NudCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NudCantidad.ValueChanged += new System.EventHandler(this.NudCantidad_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(20, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "CANTIDAD";
            // 
            // FrmMaterialSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(812, 505);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.imgClean);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvListaMateriales);
            this.Controls.Add(this.txtBuscarId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSeleccionar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(828, 544);
            this.MinimumSize = new System.Drawing.Size(828, 544);
            this.Name = "FrmMaterialSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Búsqueda de Material";
            this.Load += new System.EventHandler(this.FrmMaterialSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaMateriales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgClean)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudCantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgClean;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvListaMateriales;
        private System.Windows.Forms.TextBox txtBuscarId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtTotal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtIVA;
        private System.Windows.Forms.NumericUpDown NudCantidad;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIdMaterial;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNombreMaterial;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCantidadMaterial;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMinimos;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIdEstado;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtSubTotal;
        private System.Windows.Forms.TextBox TxtPrecioUnitario;
    }
}