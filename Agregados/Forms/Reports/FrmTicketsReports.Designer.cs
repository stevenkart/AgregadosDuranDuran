namespace Agregados.Forms.Reports
{
    partial class FrmTicketsReports
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTicketsReports));
            this.dgvFilter = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.RbPendientes = new System.Windows.Forms.RadioButton();
            this.RbFechas = new System.Windows.Forms.RadioButton();
            this.RbHoy = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.btnFiltrarHoyTodas = new System.Windows.Forms.Button();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCreditoTodas = new System.Windows.Forms.Button();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.DateFin = new System.Windows.Forms.DateTimePicker();
            this.DateInicio = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnFiltrarTodasVentas = new System.Windows.Forms.Button();
            this.RbAnuladas = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAnuladas = new System.Windows.Forms.Button();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.DateFin2 = new System.Windows.Forms.DateTimePicker();
            this.DateInicio2 = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CConsecutivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CFechaFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSubTotalFact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIVAFact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCostoTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNombreEmpleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNombreEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCostoTransporte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CBackHoe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CReferenciaPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CFechaLimiteP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CTipoPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIVA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNombreMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIdMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnReportPDF = new System.Windows.Forms.Button();
            this.BtnVerFact = new System.Windows.Forms.Button();
            this.BtnVerFacturasList = new System.Windows.Forms.Button();
            this.btnReportExcel = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilter)).BeginInit();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvFilter
            // 
            this.dgvFilter.AllowUserToAddRows = false;
            this.dgvFilter.AllowUserToDeleteRows = false;
            this.dgvFilter.AllowUserToOrderColumns = true;
            this.dgvFilter.AllowUserToResizeColumns = false;
            this.dgvFilter.AllowUserToResizeRows = false;
            this.dgvFilter.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvFilter.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgvFilter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFilter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CID,
            this.CConsecutivo,
            this.CFechaFactura,
            this.CSubTotalFact,
            this.CIVAFact,
            this.CCostoTotal,
            this.CNombre,
            this.CNombreEmpleado,
            this.CNombreEstado,
            this.CCostoTransporte,
            this.CBackHoe,
            this.CReferenciaPago,
            this.CFechaLimiteP,
            this.CTipoPago,
            this.CTipo,
            this.CCantidad,
            this.CPrecio,
            this.CSubtotal,
            this.CIVA,
            this.CTotal,
            this.CNombreMaterial,
            this.CIdMaterial});
            this.dgvFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFilter.GridColor = System.Drawing.SystemColors.Control;
            this.dgvFilter.Location = new System.Drawing.Point(13, 13);
            this.dgvFilter.MultiSelect = false;
            this.dgvFilter.Name = "dgvFilter";
            this.dgvFilter.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvFilter.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFilter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFilter.Size = new System.Drawing.Size(1170, 231);
            this.dgvFilter.TabIndex = 2;
            this.dgvFilter.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFilter_CellClick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 503);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.panel3.Size = new System.Drawing.Size(1196, 105);
            this.panel3.TabIndex = 2;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 5;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.54859F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.45141F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 157F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 199F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 182F));
            this.tableLayoutPanel5.Controls.Add(this.btnVolver, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnReportPDF, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.BtnVerFact, 4, 0);
            this.tableLayoutPanel5.Controls.Add(this.BtnVerFacturasList, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnReportExcel, 2, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1176, 85);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dgvFilter, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1196, 257);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.86266F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.13734F));
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.RbPendientes, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.RbFechas, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.RbHoy, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel6, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel7, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel8, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.RbAnuladas, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel10, 1, 4);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.60163F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.10582F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.61404F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.22222F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(932, 244);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Búsqueda de Facturas";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RbPendientes
            // 
            this.RbPendientes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.RbPendientes.AutoSize = true;
            this.RbPendientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbPendientes.Location = new System.Drawing.Point(3, 142);
            this.RbPendientes.Name = "RbPendientes";
            this.RbPendientes.Size = new System.Drawing.Size(262, 20);
            this.RbPendientes.TabIndex = 2;
            this.RbPendientes.TabStop = true;
            this.RbPendientes.Text = "Compras Pendientes por Pagar";
            this.RbPendientes.UseVisualStyleBackColor = true;
            this.RbPendientes.CheckedChanged += new System.EventHandler(this.RbPendientes_CheckedChanged);
            // 
            // RbFechas
            // 
            this.RbFechas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.RbFechas.AutoSize = true;
            this.RbFechas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbFechas.Location = new System.Drawing.Point(3, 84);
            this.RbFechas.Name = "RbFechas";
            this.RbFechas.Size = new System.Drawing.Size(262, 20);
            this.RbFechas.TabIndex = 1;
            this.RbFechas.TabStop = true;
            this.RbFechas.Text = "Compras por Rango Fechas";
            this.RbFechas.UseVisualStyleBackColor = true;
            this.RbFechas.CheckedChanged += new System.EventHandler(this.RbFechas_CheckedChanged);
            // 
            // RbHoy
            // 
            this.RbHoy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.RbHoy.AutoSize = true;
            this.RbHoy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbHoy.Location = new System.Drawing.Point(3, 28);
            this.RbHoy.Name = "RbHoy";
            this.RbHoy.Size = new System.Drawing.Size(262, 20);
            this.RbHoy.TabIndex = 0;
            this.RbHoy.TabStop = true;
            this.RbHoy.Text = "Compras de Hoy";
            this.RbHoy.UseVisualStyleBackColor = true;
            this.RbHoy.CheckedChanged += new System.EventHandler(this.RbHoy_CheckedChanged);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel6.Controls.Add(this.btnFiltrarHoyTodas, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(271, 24);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(658, 28);
            this.tableLayoutPanel6.TabIndex = 7;
            // 
            // btnFiltrarHoyTodas
            // 
            this.btnFiltrarHoyTodas.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnFiltrarHoyTodas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiltrarHoyTodas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFiltrarHoyTodas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrarHoyTodas.Location = new System.Drawing.Point(3, 3);
            this.btnFiltrarHoyTodas.Name = "btnFiltrarHoyTodas";
            this.btnFiltrarHoyTodas.Size = new System.Drawing.Size(652, 22);
            this.btnFiltrarHoyTodas.TabIndex = 5;
            this.btnFiltrarHoyTodas.Text = "Filtrar Compras de Hoy";
            this.btnFiltrarHoyTodas.UseVisualStyleBackColor = false;
            this.btnFiltrarHoyTodas.Click += new System.EventHandler(this.btnFiltrarHoyTodas_Click);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.Controls.Add(this.btnCreditoTodas, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(271, 136);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(658, 32);
            this.tableLayoutPanel7.TabIndex = 8;
            // 
            // btnCreditoTodas
            // 
            this.btnCreditoTodas.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnCreditoTodas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreditoTodas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCreditoTodas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreditoTodas.Location = new System.Drawing.Point(3, 3);
            this.btnCreditoTodas.Name = "btnCreditoTodas";
            this.btnCreditoTodas.Size = new System.Drawing.Size(652, 26);
            this.btnCreditoTodas.TabIndex = 8;
            this.btnCreditoTodas.Text = "Filtrar a Crédito Todas las Ventas";
            this.btnCreditoTodas.UseVisualStyleBackColor = false;
            this.btnCreditoTodas.Click += new System.EventHandler(this.btnCreditoTodas_Click);
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel9, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(271, 58);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(658, 72);
            this.tableLayoutPanel8.TabIndex = 9;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.54071F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.45929F));
            this.tableLayoutPanel9.Controls.Add(this.DateFin, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.DateInicio, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(652, 30);
            this.tableLayoutPanel9.TabIndex = 0;
            // 
            // DateFin
            // 
            this.DateFin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DateFin.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.DateFin.Location = new System.Drawing.Point(319, 5);
            this.DateFin.Name = "DateFin";
            this.DateFin.Size = new System.Drawing.Size(330, 20);
            this.DateFin.TabIndex = 1;
            // 
            // DateInicio
            // 
            this.DateInicio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DateInicio.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.DateInicio.Location = new System.Drawing.Point(3, 5);
            this.DateInicio.Name = "DateInicio";
            this.DateInicio.Size = new System.Drawing.Size(310, 20);
            this.DateInicio.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.Controls.Add(this.btnFiltrarTodasVentas, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 39);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(652, 30);
            this.tableLayoutPanel4.TabIndex = 5;
            // 
            // btnFiltrarTodasVentas
            // 
            this.btnFiltrarTodasVentas.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnFiltrarTodasVentas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiltrarTodasVentas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFiltrarTodasVentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrarTodasVentas.Location = new System.Drawing.Point(3, 3);
            this.btnFiltrarTodasVentas.Name = "btnFiltrarTodasVentas";
            this.btnFiltrarTodasVentas.Size = new System.Drawing.Size(646, 24);
            this.btnFiltrarTodasVentas.TabIndex = 6;
            this.btnFiltrarTodasVentas.Text = "Filtrar Todas";
            this.btnFiltrarTodasVentas.UseVisualStyleBackColor = false;
            this.btnFiltrarTodasVentas.Click += new System.EventHandler(this.btnFiltrarTodasVentas_Click);
            // 
            // RbAnuladas
            // 
            this.RbAnuladas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.RbAnuladas.AutoSize = true;
            this.RbAnuladas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbAnuladas.Location = new System.Drawing.Point(3, 197);
            this.RbAnuladas.Name = "RbAnuladas";
            this.RbAnuladas.Size = new System.Drawing.Size(262, 20);
            this.RbAnuladas.TabIndex = 10;
            this.RbAnuladas.TabStop = true;
            this.RbAnuladas.Text = "Compras Anuladas / Reversadas";
            this.RbAnuladas.UseVisualStyleBackColor = true;
            this.RbAnuladas.CheckedChanged += new System.EventHandler(this.RbAnuladas_CheckedChanged);
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Controls.Add(this.btnAnuladas, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.tableLayoutPanel11, 0, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(271, 174);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 2;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(658, 67);
            this.tableLayoutPanel10.TabIndex = 11;
            // 
            // btnAnuladas
            // 
            this.btnAnuladas.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnAnuladas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnuladas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAnuladas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnuladas.Location = new System.Drawing.Point(3, 38);
            this.btnAnuladas.Name = "btnAnuladas";
            this.btnAnuladas.Size = new System.Drawing.Size(652, 26);
            this.btnAnuladas.TabIndex = 7;
            this.btnAnuladas.Text = "Filtrar Facturas";
            this.btnAnuladas.UseVisualStyleBackColor = false;
            this.btnAnuladas.Click += new System.EventHandler(this.btnAnuladas_Click);
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 2;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel11.Controls.Add(this.DateFin2, 1, 0);
            this.tableLayoutPanel11.Controls.Add(this.DateInicio2, 0, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(652, 29);
            this.tableLayoutPanel11.TabIndex = 0;
            // 
            // DateFin2
            // 
            this.DateFin2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DateFin2.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.DateFin2.Location = new System.Drawing.Point(329, 4);
            this.DateFin2.Name = "DateFin2";
            this.DateFin2.Size = new System.Drawing.Size(320, 20);
            this.DateFin2.TabIndex = 3;
            // 
            // DateInicio2
            // 
            this.DateInicio2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DateInicio2.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.DateInicio2.Location = new System.Drawing.Point(3, 4);
            this.DateInicio2.Name = "DateInicio2";
            this.DateInicio2.Size = new System.Drawing.Size(320, 20);
            this.DateInicio2.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1196, 250);
            this.panel1.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.50304F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.49695F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pictureBox1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1196, 250);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Location = new System.Drawing.Point(0, 250);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1196, 257);
            this.panel2.TabIndex = 4;
            // 
            // CID
            // 
            this.CID.DataPropertyName = "IdFactura";
            this.CID.HeaderText = "ID";
            this.CID.Name = "CID";
            this.CID.ReadOnly = true;
            this.CID.Visible = false;
            // 
            // CConsecutivo
            // 
            this.CConsecutivo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.CConsecutivo.DataPropertyName = "Consecutivo";
            this.CConsecutivo.HeaderText = "Consecutivo";
            this.CConsecutivo.MinimumWidth = 100;
            this.CConsecutivo.Name = "CConsecutivo";
            this.CConsecutivo.ReadOnly = true;
            // 
            // CFechaFactura
            // 
            this.CFechaFactura.DataPropertyName = "FechaFactura";
            this.CFechaFactura.FillWeight = 180F;
            this.CFechaFactura.HeaderText = "Fecha";
            this.CFechaFactura.MinimumWidth = 180;
            this.CFechaFactura.Name = "CFechaFactura";
            this.CFechaFactura.ReadOnly = true;
            this.CFechaFactura.Width = 180;
            // 
            // CSubTotalFact
            // 
            this.CSubTotalFact.DataPropertyName = "SubTotalFact";
            this.CSubTotalFact.HeaderText = "SubTotalFact";
            this.CSubTotalFact.Name = "CSubTotalFact";
            this.CSubTotalFact.ReadOnly = true;
            this.CSubTotalFact.Visible = false;
            // 
            // CIVAFact
            // 
            this.CIVAFact.DataPropertyName = "IVAFact";
            this.CIVAFact.HeaderText = "IVAFact";
            this.CIVAFact.Name = "CIVAFact";
            this.CIVAFact.ReadOnly = true;
            this.CIVAFact.Visible = false;
            // 
            // CCostoTotal
            // 
            this.CCostoTotal.DataPropertyName = "CostoTotal";
            this.CCostoTotal.FillWeight = 180F;
            this.CCostoTotal.HeaderText = "Monto Total";
            this.CCostoTotal.MinimumWidth = 180;
            this.CCostoTotal.Name = "CCostoTotal";
            this.CCostoTotal.ReadOnly = true;
            this.CCostoTotal.Width = 180;
            // 
            // CNombre
            // 
            this.CNombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CNombre.DataPropertyName = "Nombre";
            this.CNombre.FillWeight = 180F;
            this.CNombre.HeaderText = "Proveedor";
            this.CNombre.MinimumWidth = 180;
            this.CNombre.Name = "CNombre";
            this.CNombre.ReadOnly = true;
            // 
            // CNombreEmpleado
            // 
            this.CNombreEmpleado.DataPropertyName = "NombreEmpleado";
            this.CNombreEmpleado.FillWeight = 180F;
            this.CNombreEmpleado.HeaderText = "Empleado";
            this.CNombreEmpleado.MinimumWidth = 180;
            this.CNombreEmpleado.Name = "CNombreEmpleado";
            this.CNombreEmpleado.ReadOnly = true;
            this.CNombreEmpleado.Width = 180;
            // 
            // CNombreEstado
            // 
            this.CNombreEstado.DataPropertyName = "NombreEstado";
            this.CNombreEstado.FillWeight = 180F;
            this.CNombreEstado.HeaderText = "Estado";
            this.CNombreEstado.MinimumWidth = 180;
            this.CNombreEstado.Name = "CNombreEstado";
            this.CNombreEstado.ReadOnly = true;
            this.CNombreEstado.Width = 180;
            // 
            // CCostoTransporte
            // 
            this.CCostoTransporte.DataPropertyName = "CostoTransporte";
            this.CCostoTransporte.HeaderText = "CostoTransporte";
            this.CCostoTransporte.Name = "CCostoTransporte";
            this.CCostoTransporte.ReadOnly = true;
            this.CCostoTransporte.Visible = false;
            // 
            // CBackHoe
            // 
            this.CBackHoe.DataPropertyName = "BackHoe";
            this.CBackHoe.HeaderText = "BackHoe";
            this.CBackHoe.Name = "CBackHoe";
            this.CBackHoe.ReadOnly = true;
            this.CBackHoe.Visible = false;
            // 
            // CReferenciaPago
            // 
            this.CReferenciaPago.DataPropertyName = "ReferenciaPago";
            this.CReferenciaPago.HeaderText = "ReferenciaPago";
            this.CReferenciaPago.Name = "CReferenciaPago";
            this.CReferenciaPago.ReadOnly = true;
            this.CReferenciaPago.Visible = false;
            // 
            // CFechaLimiteP
            // 
            this.CFechaLimiteP.DataPropertyName = "FechaLimiteP";
            this.CFechaLimiteP.HeaderText = "FechaLimiteP";
            this.CFechaLimiteP.Name = "CFechaLimiteP";
            this.CFechaLimiteP.ReadOnly = true;
            this.CFechaLimiteP.Visible = false;
            // 
            // CTipoPago
            // 
            this.CTipoPago.DataPropertyName = "TipoPago";
            this.CTipoPago.HeaderText = "TipoPago";
            this.CTipoPago.Name = "CTipoPago";
            this.CTipoPago.ReadOnly = true;
            this.CTipoPago.Visible = false;
            // 
            // CTipo
            // 
            this.CTipo.DataPropertyName = "Tipo";
            this.CTipo.HeaderText = "TipoFactura";
            this.CTipo.Name = "CTipo";
            this.CTipo.ReadOnly = true;
            this.CTipo.Visible = false;
            // 
            // CCantidad
            // 
            this.CCantidad.DataPropertyName = "Cantidad";
            this.CCantidad.HeaderText = "CantidadMaterial";
            this.CCantidad.Name = "CCantidad";
            this.CCantidad.ReadOnly = true;
            this.CCantidad.Visible = false;
            // 
            // CPrecio
            // 
            this.CPrecio.DataPropertyName = "Precio";
            this.CPrecio.HeaderText = "PrecioMaterial";
            this.CPrecio.Name = "CPrecio";
            this.CPrecio.ReadOnly = true;
            this.CPrecio.Visible = false;
            // 
            // CSubtotal
            // 
            this.CSubtotal.DataPropertyName = "Subtotal";
            this.CSubtotal.HeaderText = "SubtotalMaterial";
            this.CSubtotal.Name = "CSubtotal";
            this.CSubtotal.ReadOnly = true;
            this.CSubtotal.Visible = false;
            // 
            // CIVA
            // 
            this.CIVA.DataPropertyName = "IVA";
            this.CIVA.HeaderText = "IVAMaterial";
            this.CIVA.Name = "CIVA";
            this.CIVA.ReadOnly = true;
            this.CIVA.Visible = false;
            // 
            // CTotal
            // 
            this.CTotal.DataPropertyName = "Total";
            this.CTotal.HeaderText = "TotalMaterial";
            this.CTotal.Name = "CTotal";
            this.CTotal.ReadOnly = true;
            this.CTotal.Visible = false;
            // 
            // CNombreMaterial
            // 
            this.CNombreMaterial.DataPropertyName = "NombreMaterial";
            this.CNombreMaterial.HeaderText = "NombreMaterial";
            this.CNombreMaterial.Name = "CNombreMaterial";
            this.CNombreMaterial.ReadOnly = true;
            this.CNombreMaterial.Visible = false;
            // 
            // CIdMaterial
            // 
            this.CIdMaterial.DataPropertyName = "IdMaterial";
            this.CIdMaterial.HeaderText = "CodMaterial";
            this.CIdMaterial.Name = "CIdMaterial";
            this.CIdMaterial.ReadOnly = true;
            this.CIdMaterial.Visible = false;
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.Color.Snow;
            this.btnVolver.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVolver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnVolver.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnVolver.Image = global::Agregados.Properties.Resources._return;
            this.btnVolver.Location = new System.Drawing.Point(3, 3);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(476, 79);
            this.btnVolver.TabIndex = 6;
            this.btnVolver.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnReportPDF
            // 
            this.btnReportPDF.BackColor = System.Drawing.Color.RosyBrown;
            this.btnReportPDF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReportPDF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReportPDF.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReportPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportPDF.Image = global::Agregados.Properties.Resources.pdf;
            this.btnReportPDF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReportPDF.Location = new System.Drawing.Point(485, 3);
            this.btnReportPDF.Name = "btnReportPDF";
            this.btnReportPDF.Size = new System.Drawing.Size(150, 79);
            this.btnReportPDF.TabIndex = 2;
            this.btnReportPDF.Text = "Export PDF";
            this.btnReportPDF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReportPDF.UseVisualStyleBackColor = false;
            // 
            // BtnVerFact
            // 
            this.BtnVerFact.BackColor = System.Drawing.Color.LightGreen;
            this.BtnVerFact.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnVerFact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnVerFact.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnVerFact.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVerFact.Image = global::Agregados.Properties.Resources.factura;
            this.BtnVerFact.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnVerFact.Location = new System.Drawing.Point(997, 3);
            this.BtnVerFact.Name = "BtnVerFact";
            this.BtnVerFact.Size = new System.Drawing.Size(176, 79);
            this.BtnVerFact.TabIndex = 0;
            this.BtnVerFact.Text = "Ver Ticket Compra";
            this.BtnVerFact.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnVerFact.UseVisualStyleBackColor = false;
            this.BtnVerFact.Click += new System.EventHandler(this.BtnVerFact_Click);
            // 
            // BtnVerFacturasList
            // 
            this.BtnVerFacturasList.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BtnVerFacturasList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnVerFacturasList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnVerFacturasList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnVerFacturasList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVerFacturasList.Image = global::Agregados.Properties.Resources.reporte;
            this.BtnVerFacturasList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnVerFacturasList.Location = new System.Drawing.Point(798, 3);
            this.BtnVerFacturasList.Name = "BtnVerFacturasList";
            this.BtnVerFacturasList.Size = new System.Drawing.Size(193, 79);
            this.BtnVerFacturasList.TabIndex = 1;
            this.BtnVerFacturasList.Text = "Ver listas de Compras";
            this.BtnVerFacturasList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnVerFacturasList.UseVisualStyleBackColor = false;
            this.BtnVerFacturasList.Click += new System.EventHandler(this.BtnVerFacturasList_Click);
            // 
            // btnReportExcel
            // 
            this.btnReportExcel.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnReportExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReportExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReportExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportExcel.Image = global::Agregados.Properties.Resources.excel;
            this.btnReportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReportExcel.Location = new System.Drawing.Point(641, 3);
            this.btnReportExcel.Name = "btnReportExcel";
            this.btnReportExcel.Size = new System.Drawing.Size(151, 79);
            this.btnReportExcel.TabIndex = 3;
            this.btnReportExcel.Text = "Export Excel";
            this.btnReportExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReportExcel.UseVisualStyleBackColor = false;
            this.btnReportExcel.Click += new System.EventHandler(this.btnReportExcel_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Agregados.Properties.Resources.agregadoImg;
            this.pictureBox1.Location = new System.Drawing.Point(941, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(252, 244);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // FrmTicketsReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1196, 608);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(825, 486);
            this.Name = "FrmTicketsReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte de Compras";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTicketsReports_FormClosing);
            this.Load += new System.EventHandler(this.FrmTicketsReports_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilter)).EndInit();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFilter;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnReportPDF;
        private System.Windows.Forms.Button BtnVerFact;
        private System.Windows.Forms.Button BtnVerFacturasList;
        private System.Windows.Forms.Button btnReportExcel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton RbPendientes;
        private System.Windows.Forms.RadioButton RbFechas;
        private System.Windows.Forms.RadioButton RbHoy;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Button btnFiltrarHoyTodas;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Button btnCreditoTodas;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.DateTimePicker DateFin;
        private System.Windows.Forms.DateTimePicker DateInicio;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btnFiltrarTodasVentas;
        private System.Windows.Forms.RadioButton RbAnuladas;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.Button btnAnuladas;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.DateTimePicker DateFin2;
        private System.Windows.Forms.DateTimePicker DateInicio2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CConsecutivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CFechaFactura;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSubTotalFact;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIVAFact;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCostoTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNombreEmpleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNombreEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCostoTransporte;
        private System.Windows.Forms.DataGridViewTextBoxColumn CBackHoe;
        private System.Windows.Forms.DataGridViewTextBoxColumn CReferenciaPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn CFechaLimiteP;
        private System.Windows.Forms.DataGridViewTextBoxColumn CTipoPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn CTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSubtotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIVA;
        private System.Windows.Forms.DataGridViewTextBoxColumn CTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNombreMaterial;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIdMaterial;
    }
}