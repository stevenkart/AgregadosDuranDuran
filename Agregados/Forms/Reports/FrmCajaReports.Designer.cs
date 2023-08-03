namespace Agregados.Forms.Reports
{
    partial class FrmCajaReports
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCajaReports));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.RbPendientes = new System.Windows.Forms.RadioButton();
            this.RbFechas = new System.Windows.Forms.RadioButton();
            this.RbHoy = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.btnFiltrarHoyTodas = new System.Windows.Forms.Button();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCierresPend = new System.Windows.Forms.Button();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.DateFin = new System.Windows.Forms.DateTimePicker();
            this.DateInicio = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnFiltrarTodosCierres = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnReportPDF = new System.Windows.Forms.Button();
            this.BtnVerCierre = new System.Windows.Forms.Button();
            this.BtnVerCierreList = new System.Windows.Forms.Button();
            this.btnReportExcel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvFilter = new System.Windows.Forms.DataGridView();
            this.CID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHora = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CFechaSalida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHoraSalida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNombreEmpleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNombreUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CDetalles = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMontoEfectivoInicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMontoEfectivoUsuarioInicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMontoEfectivoFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMontoEfectivoUsuarioFin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMontoTransf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMontoCompraTransf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMontoSinpe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMontoCompraSinpe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMontoCheque = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMontoCredito = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMontoCompraCredito = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CFaltanteInicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CFaltanteFin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSobranteInicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSobranteFin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilter)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1196, 250);
            this.panel1.TabIndex = 6;
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
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.60163F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.13115F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.4918F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.4918F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(932, 244);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Búsqueda de Cierre Caja";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RbPendientes
            // 
            this.RbPendientes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.RbPendientes.AutoSize = true;
            this.RbPendientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbPendientes.Location = new System.Drawing.Point(3, 208);
            this.RbPendientes.Name = "RbPendientes";
            this.RbPendientes.Size = new System.Drawing.Size(262, 20);
            this.RbPendientes.TabIndex = 2;
            this.RbPendientes.TabStop = true;
            this.RbPendientes.Text = "Cierres Pendientes";
            this.RbPendientes.UseVisualStyleBackColor = true;
            this.RbPendientes.CheckedChanged += new System.EventHandler(this.RbPendientes_CheckedChanged);
            // 
            // RbFechas
            // 
            this.RbFechas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.RbFechas.AutoSize = true;
            this.RbFechas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbFechas.Location = new System.Drawing.Point(3, 128);
            this.RbFechas.Name = "RbFechas";
            this.RbFechas.Size = new System.Drawing.Size(262, 20);
            this.RbFechas.TabIndex = 1;
            this.RbFechas.TabStop = true;
            this.RbFechas.Text = "Cierres por Rango Fechas";
            this.RbFechas.UseVisualStyleBackColor = true;
            this.RbFechas.CheckedChanged += new System.EventHandler(this.RbFechas_CheckedChanged);
            // 
            // RbHoy
            // 
            this.RbHoy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.RbHoy.AutoSize = true;
            this.RbHoy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbHoy.Location = new System.Drawing.Point(3, 46);
            this.RbHoy.Name = "RbHoy";
            this.RbHoy.Size = new System.Drawing.Size(262, 20);
            this.RbHoy.TabIndex = 0;
            this.RbHoy.TabStop = true;
            this.RbHoy.Text = "Cierres de Hoy";
            this.RbHoy.UseVisualStyleBackColor = true;
            this.RbHoy.CheckedChanged += new System.EventHandler(this.RbHoy_CheckedChanged);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel6.Controls.Add(this.btnFiltrarHoyTodas, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(271, 33);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(658, 47);
            this.tableLayoutPanel6.TabIndex = 7;
            // 
            // btnFiltrarHoyTodas
            // 
            this.btnFiltrarHoyTodas.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnFiltrarHoyTodas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiltrarHoyTodas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFiltrarHoyTodas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrarHoyTodas.Image = ((System.Drawing.Image)(resources.GetObject("btnFiltrarHoyTodas.Image")));
            this.btnFiltrarHoyTodas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFiltrarHoyTodas.Location = new System.Drawing.Point(3, 3);
            this.btnFiltrarHoyTodas.Name = "btnFiltrarHoyTodas";
            this.btnFiltrarHoyTodas.Size = new System.Drawing.Size(652, 41);
            this.btnFiltrarHoyTodas.TabIndex = 5;
            this.btnFiltrarHoyTodas.Text = "Filtrar Cierres de Hoy";
            this.btnFiltrarHoyTodas.UseVisualStyleBackColor = false;
            this.btnFiltrarHoyTodas.Click += new System.EventHandler(this.btnFiltrarHoyTodas_Click);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.Controls.Add(this.btnCierresPend, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(271, 196);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(658, 45);
            this.tableLayoutPanel7.TabIndex = 8;
            // 
            // btnCierresPend
            // 
            this.btnCierresPend.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnCierresPend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCierresPend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCierresPend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCierresPend.Image = ((System.Drawing.Image)(resources.GetObject("btnCierresPend.Image")));
            this.btnCierresPend.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCierresPend.Location = new System.Drawing.Point(3, 3);
            this.btnCierresPend.Name = "btnCierresPend";
            this.btnCierresPend.Size = new System.Drawing.Size(652, 39);
            this.btnCierresPend.TabIndex = 8;
            this.btnCierresPend.Text = "Filtrar Cierres Pendientes";
            this.btnCierresPend.UseVisualStyleBackColor = false;
            this.btnCierresPend.Click += new System.EventHandler(this.btnCierresPend_Click);
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel9, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(271, 86);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(658, 104);
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
            this.tableLayoutPanel9.Size = new System.Drawing.Size(652, 46);
            this.tableLayoutPanel9.TabIndex = 0;
            // 
            // DateFin
            // 
            this.DateFin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DateFin.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.DateFin.Location = new System.Drawing.Point(319, 13);
            this.DateFin.Name = "DateFin";
            this.DateFin.Size = new System.Drawing.Size(330, 20);
            this.DateFin.TabIndex = 1;
            // 
            // DateInicio
            // 
            this.DateInicio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DateInicio.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.DateInicio.Location = new System.Drawing.Point(3, 13);
            this.DateInicio.Name = "DateInicio";
            this.DateInicio.Size = new System.Drawing.Size(310, 20);
            this.DateInicio.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.Controls.Add(this.btnFiltrarTodosCierres, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 55);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(652, 46);
            this.tableLayoutPanel4.TabIndex = 5;
            // 
            // btnFiltrarTodosCierres
            // 
            this.btnFiltrarTodosCierres.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnFiltrarTodosCierres.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiltrarTodosCierres.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFiltrarTodosCierres.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrarTodosCierres.Image = ((System.Drawing.Image)(resources.GetObject("btnFiltrarTodosCierres.Image")));
            this.btnFiltrarTodosCierres.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFiltrarTodosCierres.Location = new System.Drawing.Point(3, 3);
            this.btnFiltrarTodosCierres.Name = "btnFiltrarTodosCierres";
            this.btnFiltrarTodosCierres.Size = new System.Drawing.Size(646, 40);
            this.btnFiltrarTodosCierres.TabIndex = 6;
            this.btnFiltrarTodosCierres.Text = "Filtrar Cierres ";
            this.btnFiltrarTodosCierres.UseVisualStyleBackColor = false;
            this.btnFiltrarTodosCierres.Click += new System.EventHandler(this.btnFiltrarTodosCierres_Click);
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
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 503);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10);
            this.panel3.Size = new System.Drawing.Size(1196, 105);
            this.panel3.TabIndex = 5;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 5;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel5.Controls.Add(this.btnVolver, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnReportPDF, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.BtnVerCierre, 4, 0);
            this.tableLayoutPanel5.Controls.Add(this.BtnVerCierreList, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnReportExcel, 2, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1176, 85);
            this.tableLayoutPanel5.TabIndex = 2;
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
            this.btnVolver.Size = new System.Drawing.Size(386, 79);
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
            this.btnReportPDF.Location = new System.Drawing.Point(395, 3);
            this.btnReportPDF.Name = "btnReportPDF";
            this.btnReportPDF.Size = new System.Drawing.Size(190, 79);
            this.btnReportPDF.TabIndex = 2;
            this.btnReportPDF.Text = "Export PDF";
            this.btnReportPDF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReportPDF.UseVisualStyleBackColor = false;
            this.btnReportPDF.Click += new System.EventHandler(this.btnReportPDF_Click);
            // 
            // BtnVerCierre
            // 
            this.BtnVerCierre.BackColor = System.Drawing.Color.LightGreen;
            this.BtnVerCierre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnVerCierre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnVerCierre.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnVerCierre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVerCierre.Image = global::Agregados.Properties.Resources.cajaRegistradora;
            this.BtnVerCierre.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnVerCierre.Location = new System.Drawing.Point(983, 3);
            this.BtnVerCierre.Name = "BtnVerCierre";
            this.BtnVerCierre.Size = new System.Drawing.Size(190, 79);
            this.BtnVerCierre.TabIndex = 0;
            this.BtnVerCierre.Text = "Ver Cierre";
            this.BtnVerCierre.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnVerCierre.UseVisualStyleBackColor = false;
            this.BtnVerCierre.Click += new System.EventHandler(this.BtnVerCierre_Click);
            // 
            // BtnVerCierreList
            // 
            this.BtnVerCierreList.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BtnVerCierreList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnVerCierreList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnVerCierreList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnVerCierreList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVerCierreList.Image = global::Agregados.Properties.Resources.listaCierreCaja;
            this.BtnVerCierreList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnVerCierreList.Location = new System.Drawing.Point(787, 3);
            this.BtnVerCierreList.Name = "BtnVerCierreList";
            this.BtnVerCierreList.Size = new System.Drawing.Size(190, 79);
            this.BtnVerCierreList.TabIndex = 1;
            this.BtnVerCierreList.Text = "Ver listas de Cierres";
            this.BtnVerCierreList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnVerCierreList.UseVisualStyleBackColor = false;
            this.BtnVerCierreList.Click += new System.EventHandler(this.BtnVerCierreList_Click);
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
            this.btnReportExcel.Location = new System.Drawing.Point(591, 3);
            this.btnReportExcel.Name = "btnReportExcel";
            this.btnReportExcel.Size = new System.Drawing.Size(190, 79);
            this.btnReportExcel.TabIndex = 3;
            this.btnReportExcel.Text = "Export Excel";
            this.btnReportExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReportExcel.UseVisualStyleBackColor = false;
            this.btnReportExcel.Click += new System.EventHandler(this.btnReportExcel_Click);
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
            this.panel2.TabIndex = 7;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1196, 257);
            this.tableLayoutPanel1.TabIndex = 1;
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
            this.CFecha,
            this.CHora,
            this.CFechaSalida,
            this.CHoraSalida,
            this.CNombreEmpleado,
            this.CNombreUsuario,
            this.CDetalles,
            this.CMontoEfectivoInicio,
            this.CMontoEfectivoUsuarioInicio,
            this.CMontoEfectivoFinal,
            this.CMontoEfectivoUsuarioFin,
            this.CMontoTransf,
            this.CMontoCompraTransf,
            this.CMontoSinpe,
            this.CMontoCompraSinpe,
            this.CMontoCheque,
            this.CMontoCredito,
            this.CMontoCompraCredito,
            this.CFaltanteInicio,
            this.CFaltanteFin,
            this.CSobranteInicio,
            this.CSobranteFin});
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
            // CID
            // 
            this.CID.DataPropertyName = "IdCierreApert";
            this.CID.HeaderText = "ID";
            this.CID.Name = "CID";
            this.CID.ReadOnly = true;
            this.CID.Visible = false;
            // 
            // CFecha
            // 
            this.CFecha.DataPropertyName = "Fecha";
            this.CFecha.FillWeight = 150F;
            this.CFecha.HeaderText = "Fecha Ingreso";
            this.CFecha.MinimumWidth = 150;
            this.CFecha.Name = "CFecha";
            this.CFecha.ReadOnly = true;
            this.CFecha.Width = 150;
            // 
            // CHora
            // 
            this.CHora.DataPropertyName = "Hora";
            this.CHora.HeaderText = "Hora Ingreso";
            this.CHora.MinimumWidth = 100;
            this.CHora.Name = "CHora";
            this.CHora.ReadOnly = true;
            // 
            // CFechaSalida
            // 
            this.CFechaSalida.DataPropertyName = "FechaSalida";
            this.CFechaSalida.FillWeight = 150F;
            this.CFechaSalida.HeaderText = "Fecha Cierre";
            this.CFechaSalida.MinimumWidth = 150;
            this.CFechaSalida.Name = "CFechaSalida";
            this.CFechaSalida.ReadOnly = true;
            this.CFechaSalida.Width = 150;
            // 
            // CHoraSalida
            // 
            this.CHoraSalida.DataPropertyName = "HoraSalida";
            this.CHoraSalida.HeaderText = "Hora Cierre";
            this.CHoraSalida.MinimumWidth = 100;
            this.CHoraSalida.Name = "CHoraSalida";
            this.CHoraSalida.ReadOnly = true;
            this.CHoraSalida.Width = 180;
            // 
            // CNombreEmpleado
            // 
            this.CNombreEmpleado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CNombreEmpleado.DataPropertyName = "NombreEmpleado";
            this.CNombreEmpleado.HeaderText = "Empleado";
            this.CNombreEmpleado.Name = "CNombreEmpleado";
            this.CNombreEmpleado.ReadOnly = true;
            // 
            // CNombreUsuario
            // 
            this.CNombreUsuario.DataPropertyName = "NombreUsuario";
            this.CNombreUsuario.FillWeight = 150F;
            this.CNombreUsuario.HeaderText = "Usuario";
            this.CNombreUsuario.MinimumWidth = 150;
            this.CNombreUsuario.Name = "CNombreUsuario";
            this.CNombreUsuario.ReadOnly = true;
            this.CNombreUsuario.Width = 150;
            // 
            // CDetalles
            // 
            this.CDetalles.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CDetalles.DataPropertyName = "Detalles";
            this.CDetalles.FillWeight = 180F;
            this.CDetalles.HeaderText = "Detalles";
            this.CDetalles.MinimumWidth = 180;
            this.CDetalles.Name = "CDetalles";
            this.CDetalles.ReadOnly = true;
            this.CDetalles.Visible = false;
            // 
            // CMontoEfectivoInicio
            // 
            this.CMontoEfectivoInicio.DataPropertyName = "MontoEfectivoInicio";
            this.CMontoEfectivoInicio.FillWeight = 180F;
            this.CMontoEfectivoInicio.HeaderText = "Monto Efectivo Inicial";
            this.CMontoEfectivoInicio.MinimumWidth = 180;
            this.CMontoEfectivoInicio.Name = "CMontoEfectivoInicio";
            this.CMontoEfectivoInicio.ReadOnly = true;
            this.CMontoEfectivoInicio.Visible = false;
            this.CMontoEfectivoInicio.Width = 180;
            // 
            // CMontoEfectivoUsuarioInicio
            // 
            this.CMontoEfectivoUsuarioInicio.DataPropertyName = "MontoEfectivoUsuarioInicio";
            this.CMontoEfectivoUsuarioInicio.FillWeight = 180F;
            this.CMontoEfectivoUsuarioInicio.HeaderText = "Monto Efectivo de Usuario Inicio";
            this.CMontoEfectivoUsuarioInicio.MinimumWidth = 180;
            this.CMontoEfectivoUsuarioInicio.Name = "CMontoEfectivoUsuarioInicio";
            this.CMontoEfectivoUsuarioInicio.ReadOnly = true;
            this.CMontoEfectivoUsuarioInicio.Visible = false;
            this.CMontoEfectivoUsuarioInicio.Width = 180;
            // 
            // CMontoEfectivoFinal
            // 
            this.CMontoEfectivoFinal.DataPropertyName = "MontoEfectivoFinal";
            this.CMontoEfectivoFinal.HeaderText = "Monto Efectivo Final";
            this.CMontoEfectivoFinal.Name = "CMontoEfectivoFinal";
            this.CMontoEfectivoFinal.ReadOnly = true;
            this.CMontoEfectivoFinal.Visible = false;
            // 
            // CMontoEfectivoUsuarioFin
            // 
            this.CMontoEfectivoUsuarioFin.DataPropertyName = "MontoEfectivoUsuarioFin";
            this.CMontoEfectivoUsuarioFin.HeaderText = "Monto Efectivo Usuario Fin";
            this.CMontoEfectivoUsuarioFin.Name = "CMontoEfectivoUsuarioFin";
            this.CMontoEfectivoUsuarioFin.ReadOnly = true;
            this.CMontoEfectivoUsuarioFin.Visible = false;
            // 
            // CMontoTransf
            // 
            this.CMontoTransf.DataPropertyName = "MontoTransf";
            this.CMontoTransf.HeaderText = "MontoTransf";
            this.CMontoTransf.Name = "CMontoTransf";
            this.CMontoTransf.ReadOnly = true;
            this.CMontoTransf.Visible = false;
            // 
            // CMontoCompraTransf
            // 
            this.CMontoCompraTransf.DataPropertyName = "MontoCompraTransf";
            this.CMontoCompraTransf.HeaderText = "MontoCompraTransf";
            this.CMontoCompraTransf.Name = "CMontoCompraTransf";
            this.CMontoCompraTransf.ReadOnly = true;
            this.CMontoCompraTransf.Visible = false;
            // 
            // CMontoSinpe
            // 
            this.CMontoSinpe.DataPropertyName = "MontoSinpe";
            this.CMontoSinpe.HeaderText = "MontoSinpe";
            this.CMontoSinpe.Name = "CMontoSinpe";
            this.CMontoSinpe.ReadOnly = true;
            this.CMontoSinpe.Visible = false;
            // 
            // CMontoCompraSinpe
            // 
            this.CMontoCompraSinpe.DataPropertyName = "MontoCompraSinpe";
            this.CMontoCompraSinpe.HeaderText = "MontoCompraSinpe";
            this.CMontoCompraSinpe.Name = "CMontoCompraSinpe";
            this.CMontoCompraSinpe.ReadOnly = true;
            this.CMontoCompraSinpe.Visible = false;
            // 
            // CMontoCheque
            // 
            this.CMontoCheque.DataPropertyName = "MontoCheque";
            this.CMontoCheque.HeaderText = "MontoCheque";
            this.CMontoCheque.Name = "CMontoCheque";
            this.CMontoCheque.ReadOnly = true;
            this.CMontoCheque.Visible = false;
            // 
            // CMontoCredito
            // 
            this.CMontoCredito.DataPropertyName = "MontoCredito";
            this.CMontoCredito.HeaderText = "MontoCredito";
            this.CMontoCredito.Name = "CMontoCredito";
            this.CMontoCredito.ReadOnly = true;
            this.CMontoCredito.Visible = false;
            // 
            // CMontoCompraCredito
            // 
            this.CMontoCompraCredito.DataPropertyName = "MontoCompraCredito";
            this.CMontoCompraCredito.HeaderText = "MontoCompraCredito";
            this.CMontoCompraCredito.Name = "CMontoCompraCredito";
            this.CMontoCompraCredito.ReadOnly = true;
            this.CMontoCompraCredito.Visible = false;
            // 
            // CFaltanteInicio
            // 
            this.CFaltanteInicio.DataPropertyName = "FaltanteInicio";
            this.CFaltanteInicio.HeaderText = "FaltanteInicio";
            this.CFaltanteInicio.Name = "CFaltanteInicio";
            this.CFaltanteInicio.ReadOnly = true;
            this.CFaltanteInicio.Visible = false;
            // 
            // CFaltanteFin
            // 
            this.CFaltanteFin.DataPropertyName = "FaltanteFin";
            this.CFaltanteFin.HeaderText = "FaltanteFin";
            this.CFaltanteFin.Name = "CFaltanteFin";
            this.CFaltanteFin.ReadOnly = true;
            this.CFaltanteFin.Visible = false;
            // 
            // CSobranteInicio
            // 
            this.CSobranteInicio.DataPropertyName = "SobranteInicio";
            this.CSobranteInicio.HeaderText = "SobranteInicio";
            this.CSobranteInicio.Name = "CSobranteInicio";
            this.CSobranteInicio.ReadOnly = true;
            this.CSobranteInicio.Visible = false;
            // 
            // CSobranteFin
            // 
            this.CSobranteFin.DataPropertyName = "SobranteFin";
            this.CSobranteFin.HeaderText = "SobranteFin";
            this.CSobranteFin.Name = "CSobranteFin";
            this.CSobranteFin.ReadOnly = true;
            this.CSobranteFin.Visible = false;
            // 
            // FrmCajaReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1196, 608);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1212, 647);
            this.Name = "FrmCajaReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte de Cierre de Caja";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCajaReports_FormClosing);
            this.Load += new System.EventHandler(this.FrmCajaReports_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton RbPendientes;
        private System.Windows.Forms.RadioButton RbFechas;
        private System.Windows.Forms.RadioButton RbHoy;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Button btnFiltrarHoyTodas;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Button btnCierresPend;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.DateTimePicker DateFin;
        private System.Windows.Forms.DateTimePicker DateInicio;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btnFiltrarTodosCierres;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnReportPDF;
        private System.Windows.Forms.Button BtnVerCierre;
        private System.Windows.Forms.Button BtnVerCierreList;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button btnReportExcel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn CID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHora;
        private System.Windows.Forms.DataGridViewTextBoxColumn CFechaSalida;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHoraSalida;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNombreEmpleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNombreUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn CDetalles;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMontoEfectivoInicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMontoEfectivoUsuarioInicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMontoEfectivoFinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMontoEfectivoUsuarioFin;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMontoTransf;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMontoCompraTransf;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMontoSinpe;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMontoCompraSinpe;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMontoCheque;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMontoCredito;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMontoCompraCredito;
        private System.Windows.Forms.DataGridViewTextBoxColumn CFaltanteInicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn CFaltanteFin;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSobranteInicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSobranteFin;
    }
}