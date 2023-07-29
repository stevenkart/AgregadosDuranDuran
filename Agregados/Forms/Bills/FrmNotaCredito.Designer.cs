namespace Agregados.Forms.Bills
{
    partial class FrmNotaCredito
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNotaCredito));
            this.txtClienteNombre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtConsecutivo = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnEmitirNota = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.CEmpleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNombreEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCostoTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CFechaFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CConsecutivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFilter = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbEfectivo2 = new System.Windows.Forms.RadioButton();
            this.rbSinpe2 = new System.Windows.Forms.RadioButton();
            this.rbSinpeMovil2 = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.txtTotal = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.tmrFechaHora = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilter)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtClienteNombre
            // 
            this.txtClienteNombre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClienteNombre.Location = new System.Drawing.Point(350, 43);
            this.txtClienteNombre.MaxLength = 255;
            this.txtClienteNombre.Name = "txtClienteNombre";
            this.txtClienteNombre.Size = new System.Drawing.Size(342, 20);
            this.txtClienteNombre.TabIndex = 3;
            this.txtClienteNombre.TextChanged += new System.EventHandler(this.txtClienteNombre_TextChanged);
            this.txtClienteNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClienteNombre_KeyPress);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(350, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(342, 18);
            this.label3.TabIndex = 1;
            this.label3.Text = "Filtar Por Nombre Cliente";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(341, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Filtar Por Consecutivo Factura";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(695, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lista de Facturas Disponibles para Realizar Nota de Crédito";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.txtClienteNombre, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtConsecutivo, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 59);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(695, 71);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // txtConsecutivo
            // 
            this.txtConsecutivo.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.txtConsecutivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConsecutivo.Location = new System.Drawing.Point(3, 43);
            this.txtConsecutivo.MaxLength = 10;
            this.txtConsecutivo.Name = "txtConsecutivo";
            this.txtConsecutivo.Size = new System.Drawing.Size(341, 20);
            this.txtConsecutivo.TabIndex = 2;
            this.txtConsecutivo.TextChanged += new System.EventHandler(this.txtConsecutivo_TextChanged);
            this.txtConsecutivo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConsecutivo_KeyPress);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.btnEmitirNota, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnVolver, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 295);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(911, 78);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // btnEmitirNota
            // 
            this.btnEmitirNota.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEmitirNota.BackColor = System.Drawing.Color.LightCoral;
            this.btnEmitirNota.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEmitirNota.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEmitirNota.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnEmitirNota.Image = global::Agregados.Properties.Resources.borrar;
            this.btnEmitirNota.Location = new System.Drawing.Point(458, 5);
            this.btnEmitirNota.Name = "btnEmitirNota";
            this.btnEmitirNota.Size = new System.Drawing.Size(450, 68);
            this.btnEmitirNota.TabIndex = 0;
            this.btnEmitirNota.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnEmitirNota.UseVisualStyleBackColor = false;
            this.btnEmitirNota.Click += new System.EventHandler(this.btnEmitirNota_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolver.BackColor = System.Drawing.Color.Snow;
            this.btnVolver.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnVolver.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnVolver.Image = global::Agregados.Properties.Resources._return;
            this.btnVolver.Location = new System.Drawing.Point(3, 5);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(449, 68);
            this.btnVolver.TabIndex = 1;
            this.btnVolver.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // CEmpleado
            // 
            this.CEmpleado.DataPropertyName = "NombreEmpleado";
            this.CEmpleado.FillWeight = 250F;
            this.CEmpleado.HeaderText = "Empleado";
            this.CEmpleado.MinimumWidth = 250;
            this.CEmpleado.Name = "CEmpleado";
            this.CEmpleado.ReadOnly = true;
            this.CEmpleado.Width = 250;
            // 
            // CCliente
            // 
            this.CCliente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CCliente.DataPropertyName = "Nombre";
            this.CCliente.HeaderText = "Cliente";
            this.CCliente.Name = "CCliente";
            this.CCliente.ReadOnly = true;
            // 
            // CNombreEstado
            // 
            this.CNombreEstado.DataPropertyName = "NombreEstado";
            this.CNombreEstado.FillWeight = 150F;
            this.CNombreEstado.HeaderText = "Estado";
            this.CNombreEstado.MinimumWidth = 150;
            this.CNombreEstado.Name = "CNombreEstado";
            this.CNombreEstado.ReadOnly = true;
            this.CNombreEstado.Width = 150;
            // 
            // CCostoTotal
            // 
            this.CCostoTotal.DataPropertyName = "CostoTotal";
            this.CCostoTotal.FillWeight = 150F;
            this.CCostoTotal.HeaderText = "Costo Total";
            this.CCostoTotal.MinimumWidth = 150;
            this.CCostoTotal.Name = "CCostoTotal";
            this.CCostoTotal.ReadOnly = true;
            this.CCostoTotal.Width = 150;
            // 
            // CFechaFactura
            // 
            this.CFechaFactura.DataPropertyName = "FechaFactura";
            this.CFechaFactura.FillWeight = 150F;
            this.CFechaFactura.HeaderText = "Fecha";
            this.CFechaFactura.MinimumWidth = 150;
            this.CFechaFactura.Name = "CFechaFactura";
            this.CFechaFactura.ReadOnly = true;
            this.CFechaFactura.Width = 150;
            // 
            // CConsecutivo
            // 
            this.CConsecutivo.DataPropertyName = "Consecutivo";
            this.CConsecutivo.HeaderText = "Consecutivo";
            this.CConsecutivo.MinimumWidth = 100;
            this.CConsecutivo.Name = "CConsecutivo";
            this.CConsecutivo.ReadOnly = true;
            // 
            // CID
            // 
            this.CID.DataPropertyName = "IdFactura";
            this.CID.HeaderText = "ID Fact";
            this.CID.Name = "CID";
            this.CID.ReadOnly = true;
            this.CID.Visible = false;
            // 
            // dgvFilter
            // 
            this.dgvFilter.AllowUserToAddRows = false;
            this.dgvFilter.AllowUserToDeleteRows = false;
            this.dgvFilter.AllowUserToOrderColumns = true;
            this.dgvFilter.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvFilter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFilter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CID,
            this.CConsecutivo,
            this.CFechaFactura,
            this.CCostoTotal,
            this.CNombreEstado,
            this.CCliente,
            this.CEmpleado});
            this.dgvFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFilter.Location = new System.Drawing.Point(3, 3);
            this.dgvFilter.MultiSelect = false;
            this.dgvFilter.Name = "dgvFilter";
            this.dgvFilter.ReadOnly = true;
            this.dgvFilter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFilter.Size = new System.Drawing.Size(911, 226);
            this.dgvFilter.TabIndex = 0;
            this.dgvFilter.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFilter_CellClick);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.dgvFilter, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 133);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.50139F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.49862F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(917, 376);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.79912F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.20088F));
            this.tableLayoutPanel5.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 235);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 69.44444F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(911, 54);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbEfectivo2);
            this.groupBox2.Controls.Add(this.rbSinpe2);
            this.groupBox2.Controls.Add(this.rbSinpeMovil2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(475, 48);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selecciona Método de Crédito";
            // 
            // rbEfectivo2
            // 
            this.rbEfectivo2.AutoSize = true;
            this.rbEfectivo2.Location = new System.Drawing.Point(28, 23);
            this.rbEfectivo2.Name = "rbEfectivo2";
            this.rbEfectivo2.Size = new System.Drawing.Size(87, 22);
            this.rbEfectivo2.TabIndex = 0;
            this.rbEfectivo2.TabStop = true;
            this.rbEfectivo2.Text = "Efectivo";
            this.rbEfectivo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbEfectivo2.UseVisualStyleBackColor = true;
            // 
            // rbSinpe2
            // 
            this.rbSinpe2.AutoSize = true;
            this.rbSinpe2.Location = new System.Drawing.Point(188, 23);
            this.rbSinpe2.Name = "rbSinpe2";
            this.rbSinpe2.Size = new System.Drawing.Size(121, 22);
            this.rbSinpe2.TabIndex = 1;
            this.rbSinpe2.TabStop = true;
            this.rbSinpe2.Text = "Trans. Sinpe";
            this.rbSinpe2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbSinpe2.UseVisualStyleBackColor = true;
            // 
            // rbSinpeMovil2
            // 
            this.rbSinpeMovil2.AutoSize = true;
            this.rbSinpeMovil2.Location = new System.Drawing.Point(354, 23);
            this.rbSinpeMovil2.Name = "rbSinpeMovil2";
            this.rbSinpeMovil2.Size = new System.Drawing.Size(113, 22);
            this.rbSinpeMovil2.TabIndex = 2;
            this.rbSinpeMovil2.TabStop = true;
            this.rbSinpeMovil2.Text = "Sinpe Móvil";
            this.rbSinpeMovil2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbSinpeMovil2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.91509F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 86.08491F));
            this.tableLayoutPanel6.Controls.Add(this.txtTotal, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(484, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(424, 48);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // txtTotal
            // 
            this.txtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotal.DecimalPlaces = 2;
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(61, 11);
            this.txtTotal.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(360, 26);
            this.txtTotal.TabIndex = 1;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTotal.ThousandsSeparator = true;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "¢";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel3);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(917, 509);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.54321F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.45679F));
            this.tableLayoutPanel1.Controls.Add(this.btnFiltrar, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnHelp, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.85714F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.14286F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(917, 133);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFiltrar.BackColor = System.Drawing.Color.LightBlue;
            this.btnFiltrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFiltrar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnFiltrar.Image = global::Agregados.Properties.Resources.buscar;
            this.btnFiltrar.Location = new System.Drawing.Point(704, 59);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(210, 70);
            this.btnFiltrar.TabIndex = 0;
            this.btnFiltrar.UseVisualStyleBackColor = false;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnHelp.BackColor = System.Drawing.Color.GhostWhite;
            this.btnHelp.Cursor = System.Windows.Forms.Cursors.Help;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnHelp.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnHelp.Image = global::Agregados.Properties.Resources.ayuda;
            this.btnHelp.Location = new System.Drawing.Point(839, 4);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 48);
            this.btnHelp.TabIndex = 1;
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // tmrFechaHora
            // 
            this.tmrFechaHora.Enabled = true;
            this.tmrFechaHora.Interval = 1000;
            this.tmrFechaHora.Tick += new System.EventHandler(this.tmrFechaHora_Tick);
            // 
            // FrmNotaCredito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 509);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(933, 548);
            this.Name = "FrmNotaCredito";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notas de Crédito";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmNotaCredito_FormClosing);
            this.Load += new System.EventHandler(this.FrmNotaCredito_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilter)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.TextBox txtClienteNombre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtConsecutivo;
        private System.Windows.Forms.Button btnEmitirNota;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.DataGridViewTextBoxColumn CEmpleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNombreEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCostoTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn CFechaFactura;
        private System.Windows.Forms.DataGridViewTextBoxColumn CConsecutivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CID;
        private System.Windows.Forms.DataGridView dgvFilter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbEfectivo2;
        private System.Windows.Forms.RadioButton rbSinpe2;
        private System.Windows.Forms.RadioButton rbSinpeMovil2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer tmrFechaHora;
        private System.Windows.Forms.NumericUpDown txtTotal;
    }
}