namespace Agregados.Forms.Reports
{
    partial class FrmFactsReports
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFactsReports));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFiltrarCredito = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.DateFin = new System.Windows.Forms.DateTimePicker();
            this.DateInicio = new System.Windows.Forms.DateTimePicker();
            this.btnFiltrarFechas = new System.Windows.Forms.Button();
            this.btnFiltrarHoy = new System.Windows.Forms.Button();
            this.RbPendientes = new System.Windows.Forms.RadioButton();
            this.RbFechas = new System.Windows.Forms.RadioButton();
            this.RbHoy = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvFilter = new System.Windows.Forms.DataGridView();
            this.CConsecutivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CFechaFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCostoTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNombreEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNombreEmpleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnReportPDF = new System.Windows.Forms.Button();
            this.BtnVerFact = new System.Windows.Forms.Button();
            this.BtnVerFacturasList = new System.Windows.Forms.Button();
            this.btnReportExcel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilter)).BeginInit();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1149, 144);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.98556F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.01445F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pictureBox1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1149, 144);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.97561F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.02439F));
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnFiltrarCredito, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.btnFiltrarHoy, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.RbPendientes, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.RbFechas, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.RbHoy, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(947, 138);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Búsqueda de Facturas";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnFiltrarCredito
            // 
            this.btnFiltrarCredito.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnFiltrarCredito.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnFiltrarCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrarCredito.Location = new System.Drawing.Point(494, 108);
            this.btnFiltrarCredito.Name = "btnFiltrarCredito";
            this.btnFiltrarCredito.Size = new System.Drawing.Size(252, 23);
            this.btnFiltrarCredito.TabIndex = 6;
            this.btnFiltrarCredito.Text = "Filtrar";
            this.btnFiltrarCredito.UseVisualStyleBackColor = false;
            this.btnFiltrarCredito.Click += new System.EventHandler(this.btnFiltrarCredito_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.55433F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.44567F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 144F));
            this.tableLayoutPanel4.Controls.Add(this.DateFin, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.DateInicio, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnFiltrarFechas, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(296, 71);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(648, 28);
            this.tableLayoutPanel4.TabIndex = 5;
            // 
            // DateFin
            // 
            this.DateFin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DateFin.Location = new System.Drawing.Point(257, 4);
            this.DateFin.Name = "DateFin";
            this.DateFin.Size = new System.Drawing.Size(243, 20);
            this.DateFin.TabIndex = 1;
            // 
            // DateInicio
            // 
            this.DateInicio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DateInicio.Location = new System.Drawing.Point(3, 4);
            this.DateInicio.Name = "DateInicio";
            this.DateInicio.Size = new System.Drawing.Size(248, 20);
            this.DateInicio.TabIndex = 0;
            // 
            // btnFiltrarFechas
            // 
            this.btnFiltrarFechas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFiltrarFechas.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnFiltrarFechas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrarFechas.Location = new System.Drawing.Point(506, 3);
            this.btnFiltrarFechas.Name = "btnFiltrarFechas";
            this.btnFiltrarFechas.Size = new System.Drawing.Size(139, 22);
            this.btnFiltrarFechas.TabIndex = 4;
            this.btnFiltrarFechas.Text = "Filtrar";
            this.btnFiltrarFechas.UseVisualStyleBackColor = false;
            this.btnFiltrarFechas.Click += new System.EventHandler(this.btnFiltrarFechas_Click);
            // 
            // btnFiltrarHoy
            // 
            this.btnFiltrarHoy.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnFiltrarHoy.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnFiltrarHoy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrarHoy.Location = new System.Drawing.Point(494, 39);
            this.btnFiltrarHoy.Name = "btnFiltrarHoy";
            this.btnFiltrarHoy.Size = new System.Drawing.Size(252, 23);
            this.btnFiltrarHoy.TabIndex = 3;
            this.btnFiltrarHoy.Text = "Filtrar";
            this.btnFiltrarHoy.UseVisualStyleBackColor = false;
            this.btnFiltrarHoy.Click += new System.EventHandler(this.btnFiltrarHoy_Click);
            // 
            // RbPendientes
            // 
            this.RbPendientes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.RbPendientes.AutoSize = true;
            this.RbPendientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbPendientes.Location = new System.Drawing.Point(3, 110);
            this.RbPendientes.Name = "RbPendientes";
            this.RbPendientes.Size = new System.Drawing.Size(287, 20);
            this.RbPendientes.TabIndex = 2;
            this.RbPendientes.TabStop = true;
            this.RbPendientes.Text = "Facturas Pendientes por Crédito";
            this.RbPendientes.UseVisualStyleBackColor = true;
            this.RbPendientes.CheckedChanged += new System.EventHandler(this.RbPendientes_CheckedChanged);
            // 
            // RbFechas
            // 
            this.RbFechas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.RbFechas.AutoSize = true;
            this.RbFechas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbFechas.Location = new System.Drawing.Point(3, 75);
            this.RbFechas.Name = "RbFechas";
            this.RbFechas.Size = new System.Drawing.Size(287, 20);
            this.RbFechas.TabIndex = 1;
            this.RbFechas.TabStop = true;
            this.RbFechas.Text = "Facturas por Rango Fechas";
            this.RbFechas.UseVisualStyleBackColor = true;
            this.RbFechas.CheckedChanged += new System.EventHandler(this.RbFechas_CheckedChanged);
            // 
            // RbHoy
            // 
            this.RbHoy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.RbHoy.AutoSize = true;
            this.RbHoy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbHoy.Location = new System.Drawing.Point(3, 41);
            this.RbHoy.Name = "RbHoy";
            this.RbHoy.Size = new System.Drawing.Size(287, 20);
            this.RbHoy.TabIndex = 0;
            this.RbHoy.TabStop = true;
            this.RbHoy.Text = "Facturas de Hoy";
            this.RbHoy.UseVisualStyleBackColor = true;
            this.RbHoy.CheckedChanged += new System.EventHandler(this.RbHoy_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Agregados.Properties.Resources.agregadoImg;
            this.pictureBox1.Location = new System.Drawing.Point(956, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(190, 138);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 144);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1149, 332);
            this.panel2.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dgvFilter, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1149, 332);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // dgvFilter
            // 
            this.dgvFilter.AllowUserToAddRows = false;
            this.dgvFilter.AllowUserToDeleteRows = false;
            this.dgvFilter.AllowUserToResizeColumns = false;
            this.dgvFilter.AllowUserToResizeRows = false;
            this.dgvFilter.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvFilter.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFilter.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFilter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFilter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CConsecutivo,
            this.CFechaFactura,
            this.CCostoTotal,
            this.CNombreEstado,
            this.CNombre,
            this.CNombreEmpleado});
            this.dgvFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFilter.GridColor = System.Drawing.SystemColors.Control;
            this.dgvFilter.Location = new System.Drawing.Point(13, 13);
            this.dgvFilter.MultiSelect = false;
            this.dgvFilter.Name = "dgvFilter";
            this.dgvFilter.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFilter.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvFilter.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvFilter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFilter.Size = new System.Drawing.Size(1123, 306);
            this.dgvFilter.TabIndex = 2;
            this.dgvFilter.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFilter_CellClick);
            // 
            // CConsecutivo
            // 
            this.CConsecutivo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.CConsecutivo.DataPropertyName = "Consecutivo";
            this.CConsecutivo.Frozen = true;
            this.CConsecutivo.HeaderText = "Consecutivo";
            this.CConsecutivo.MinimumWidth = 100;
            this.CConsecutivo.Name = "CConsecutivo";
            this.CConsecutivo.ReadOnly = true;
            this.CConsecutivo.Width = 117;
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
            // CNombre
            // 
            this.CNombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CNombre.DataPropertyName = "Nombre";
            this.CNombre.FillWeight = 180F;
            this.CNombre.HeaderText = "Cliente";
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
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 476);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10);
            this.panel3.Size = new System.Drawing.Size(1149, 96);
            this.panel3.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 5;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.96899F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.03101F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 197F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel5.Controls.Add(this.btnReportPDF, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.BtnVerFact, 4, 0);
            this.tableLayoutPanel5.Controls.Add(this.BtnVerFacturasList, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnReportExcel, 2, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1129, 76);
            this.tableLayoutPanel5.TabIndex = 2;
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
            this.btnReportPDF.Location = new System.Drawing.Point(493, 3);
            this.btnReportPDF.Name = "btnReportPDF";
            this.btnReportPDF.Size = new System.Drawing.Size(149, 70);
            this.btnReportPDF.TabIndex = 2;
            this.btnReportPDF.Text = "Export PDF";
            this.btnReportPDF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReportPDF.UseVisualStyleBackColor = false;
            this.btnReportPDF.Click += new System.EventHandler(this.btnReportPDF_Click);
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
            this.BtnVerFact.Location = new System.Drawing.Point(1001, 3);
            this.BtnVerFact.Name = "BtnVerFact";
            this.BtnVerFact.Size = new System.Drawing.Size(125, 70);
            this.BtnVerFact.TabIndex = 0;
            this.BtnVerFact.Text = "Ver Factura";
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
            this.BtnVerFacturasList.Location = new System.Drawing.Point(804, 3);
            this.BtnVerFacturasList.Name = "BtnVerFacturasList";
            this.BtnVerFacturasList.Size = new System.Drawing.Size(191, 70);
            this.BtnVerFacturasList.TabIndex = 1;
            this.BtnVerFacturasList.Text = "Ver listas de Facturas";
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
            this.btnReportExcel.Location = new System.Drawing.Point(648, 3);
            this.btnReportExcel.Name = "btnReportExcel";
            this.btnReportExcel.Size = new System.Drawing.Size(150, 70);
            this.btnReportExcel.TabIndex = 3;
            this.btnReportExcel.Text = "Export Excel";
            this.btnReportExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReportExcel.UseVisualStyleBackColor = false;
            this.btnReportExcel.Click += new System.EventHandler(this.btnReportExcel_Click);
            // 
            // FrmFactsReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 572);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1165, 611);
            this.MinimumSize = new System.Drawing.Size(1165, 611);
            this.Name = "FrmFactsReports";
            this.Text = "Reporte de Facturas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmFactsReports_FormClosing);
            this.Load += new System.EventHandler(this.FrmFactsReports_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilter)).EndInit();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.RadioButton RbHoy;
        private System.Windows.Forms.RadioButton RbFechas;
        private System.Windows.Forms.RadioButton RbPendientes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFiltrarCredito;
        private System.Windows.Forms.Button btnFiltrarHoy;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.DateTimePicker DateFin;
        private System.Windows.Forms.DateTimePicker DateInicio;
        private System.Windows.Forms.Button btnFiltrarFechas;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button BtnVerFact;
        private System.Windows.Forms.Button BtnVerFacturasList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn CConsecutivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CFechaFactura;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCostoTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNombreEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNombreEmpleado;
        private System.Windows.Forms.Button btnReportPDF;
        private System.Windows.Forms.Button btnReportExcel;
    }
}