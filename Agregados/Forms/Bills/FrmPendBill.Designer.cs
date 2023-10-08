namespace Agregados.Forms.Bills
{
    partial class FrmPendBill
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPendBill));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tmrFechaHora = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtClienteNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtConsecutivo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnFiltrarHoyTodas = new System.Windows.Forms.Button();
            this.btnFiltrarHoySoloBackHoe = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPagar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbEfectivo2 = new System.Windows.Forms.RadioButton();
            this.rbSinpe2 = new System.Windows.Forms.RadioButton();
            this.rbSinpeMovil2 = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.txtTotal = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvFilter = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.CID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CConsecutivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCostoTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNombreEmpleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNombreEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CFechaFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CFechaLimiteP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSubTotalFact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIVAFact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCostoTransporte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CBackHoe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CReferenciaPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CTipoPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIVA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNombreMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIdMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal)).BeginInit();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilter)).BeginInit();
            this.tableLayoutPanel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrFechaHora
            // 
            this.tmrFechaHora.Enabled = true;
            this.tmrFechaHora.Interval = 1000;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1092, 133);
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
            this.btnFiltrar.Location = new System.Drawing.Point(838, 59);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(251, 70);
            this.btnFiltrar.TabIndex = 0;
            this.btnFiltrar.UseVisualStyleBackColor = false;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(829, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lista de Facturas Pendientes";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.86692F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.13308F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 265F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 256F));
            this.tableLayoutPanel2.Controls.Add(this.label8, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label7, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtClienteNombre, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtConsecutivo, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFiltrarHoyTodas, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnFiltrarHoySoloBackHoe, 2, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 59);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.43662F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.56338F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(829, 71);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(575, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(251, 18);
            this.label8.TabIndex = 6;
            this.label8.Text = "Filtrar Facturas con Materiales";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(310, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(259, 18);
            this.label7.TabIndex = 4;
            this.label7.Text = "Filtrar Facturas solo BackHoe";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtClienteNombre
            // 
            this.txtClienteNombre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClienteNombre.Location = new System.Drawing.Point(141, 39);
            this.txtClienteNombre.Name = "txtClienteNombre";
            this.txtClienteNombre.Size = new System.Drawing.Size(163, 20);
            this.txtClienteNombre.TabIndex = 3;
            this.txtClienteNombre.TextChanged += new System.EventHandler(this.txtClienteNombre_TextChanged);
            this.txtClienteNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClienteNombre_KeyPress);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Consecutivo";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtConsecutivo
            // 
            this.txtConsecutivo.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.txtConsecutivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConsecutivo.Location = new System.Drawing.Point(3, 39);
            this.txtConsecutivo.Name = "txtConsecutivo";
            this.txtConsecutivo.Size = new System.Drawing.Size(132, 20);
            this.txtConsecutivo.TabIndex = 1;
            this.txtConsecutivo.TextChanged += new System.EventHandler(this.txtConsecutivo_TextChanged);
            this.txtConsecutivo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConsecutivo_KeyPress);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(141, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nombre Cliente";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnFiltrarHoyTodas
            // 
            this.btnFiltrarHoyTodas.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnFiltrarHoyTodas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiltrarHoyTodas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFiltrarHoyTodas.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrarHoyTodas.Image = ((System.Drawing.Image)(resources.GetObject("btnFiltrarHoyTodas.Image")));
            this.btnFiltrarHoyTodas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFiltrarHoyTodas.Location = new System.Drawing.Point(575, 31);
            this.btnFiltrarHoyTodas.Name = "btnFiltrarHoyTodas";
            this.btnFiltrarHoyTodas.Size = new System.Drawing.Size(251, 37);
            this.btnFiltrarHoyTodas.TabIndex = 7;
            this.btnFiltrarHoyTodas.Text = "Filtrar";
            this.btnFiltrarHoyTodas.UseVisualStyleBackColor = false;
            this.btnFiltrarHoyTodas.Click += new System.EventHandler(this.btnFiltrarHoyTodas_Click);
            // 
            // btnFiltrarHoySoloBackHoe
            // 
            this.btnFiltrarHoySoloBackHoe.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnFiltrarHoySoloBackHoe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiltrarHoySoloBackHoe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFiltrarHoySoloBackHoe.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrarHoySoloBackHoe.Image = ((System.Drawing.Image)(resources.GetObject("btnFiltrarHoySoloBackHoe.Image")));
            this.btnFiltrarHoySoloBackHoe.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFiltrarHoySoloBackHoe.Location = new System.Drawing.Point(310, 31);
            this.btnFiltrarHoySoloBackHoe.Name = "btnFiltrarHoySoloBackHoe";
            this.btnFiltrarHoySoloBackHoe.Size = new System.Drawing.Size(259, 37);
            this.btnFiltrarHoySoloBackHoe.TabIndex = 5;
            this.btnFiltrarHoySoloBackHoe.Text = "Filtrar";
            this.btnFiltrarHoySoloBackHoe.UseVisualStyleBackColor = false;
            this.btnFiltrarHoySoloBackHoe.Click += new System.EventHandler(this.btnFiltrarHoySoloBackHoe_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnHelp.BackColor = System.Drawing.Color.GhostWhite;
            this.btnHelp.Cursor = System.Windows.Forms.Cursors.Help;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnHelp.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnHelp.Image = global::Agregados.Properties.Resources.ayuda;
            this.btnHelp.Location = new System.Drawing.Point(1014, 4);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 48);
            this.btnHelp.TabIndex = 1;
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel3);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1092, 615);
            this.panel1.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel7, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 133);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.50139F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.49862F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1092, 482);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.btnPagar, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnVolver, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 401);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1086, 78);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // btnPagar
            // 
            this.btnPagar.BackColor = System.Drawing.Color.LightGreen;
            this.btnPagar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPagar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPagar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPagar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPagar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPagar.Image = global::Agregados.Properties.Resources.factura;
            this.btnPagar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPagar.Location = new System.Drawing.Point(546, 3);
            this.btnPagar.Name = "btnPagar";
            this.btnPagar.Size = new System.Drawing.Size(537, 72);
            this.btnPagar.TabIndex = 0;
            this.btnPagar.Text = "Realizar Pago";
            this.btnPagar.UseVisualStyleBackColor = false;
            this.btnPagar.Click += new System.EventHandler(this.btnPagar_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.Color.Snow;
            this.btnVolver.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVolver.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnVolver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnVolver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnVolver.Image = global::Agregados.Properties.Resources._return;
            this.btnVolver.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVolver.Location = new System.Drawing.Point(3, 3);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(537, 72);
            this.btnVolver.TabIndex = 1;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.79912F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.20088F));
            this.tableLayoutPanel5.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 320);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 69.44444F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1086, 75);
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
            this.groupBox2.Size = new System.Drawing.Size(567, 69);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selecciona Método de Pago";
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
            this.rbEfectivo2.CheckedChanged += new System.EventHandler(this.rbEfectivo2_CheckedChanged);
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
            this.rbSinpe2.CheckedChanged += new System.EventHandler(this.rbSinpe2_CheckedChanged);
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
            this.rbSinpeMovil2.CheckedChanged += new System.EventHandler(this.rbSinpeMovil2_CheckedChanged);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.91509F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 86.08491F));
            this.tableLayoutPanel6.Controls.Add(this.txtTotal, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(576, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(507, 69);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // txtTotal
            // 
            this.txtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotal.DecimalPlaces = 2;
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(73, 21);
            this.txtTotal.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(431, 26);
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
            this.label4.Location = new System.Drawing.Point(3, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "¢";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.dgvFilter, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel8, 0, 1);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(1086, 311);
            this.tableLayoutPanel7.TabIndex = 3;
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
            this.CID,
            this.CConsecutivo,
            this.CCostoTotal,
            this.CNombreEmpleado,
            this.CNombreEstado,
            this.CNombre,
            this.CFechaFactura,
            this.CFechaLimiteP,
            this.CSubTotalFact,
            this.CIVAFact,
            this.CCostoTransporte,
            this.CBackHoe,
            this.CReferenciaPago,
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
            this.dgvFilter.Location = new System.Drawing.Point(3, 3);
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
            this.dgvFilter.Size = new System.Drawing.Size(1080, 225);
            this.dgvFilter.TabIndex = 0;
            this.dgvFilter.DataSourceChanged += new System.EventHandler(this.dgvFilter_DataSourceChanged);
            this.dgvFilter.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFilter_CellClick);
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.83142F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.16858F));
            this.tableLayoutPanel8.Controls.Add(this.txtFecha, 1, 1);
            this.tableLayoutPanel8.Controls.Add(this.label6, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.txtDescription, 0, 1);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 234);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32.43243F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 67.56757F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(1080, 74);
            this.tableLayoutPanel8.TabIndex = 8;
            // 
            // txtFecha
            // 
            this.txtFecha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFecha.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtFecha.Location = new System.Drawing.Point(854, 38);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.ReadOnly = true;
            this.txtFecha.Size = new System.Drawing.Size(223, 20);
            this.txtFecha.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(854, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(223, 18);
            this.label6.TabIndex = 2;
            this.label6.Text = "Fecha Pago";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(845, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "Detalle";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(3, 26);
            this.txtDescription.MaxLength = 1000;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(845, 45);
            this.txtDescription.TabIndex = 1;
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
            this.CConsecutivo.FillWeight = 80F;
            this.CConsecutivo.HeaderText = "Consecutivo";
            this.CConsecutivo.MinimumWidth = 80;
            this.CConsecutivo.Name = "CConsecutivo";
            this.CConsecutivo.ReadOnly = true;
            this.CConsecutivo.Width = 117;
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
            // CNombre
            // 
            this.CNombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CNombre.DataPropertyName = "Nombre";
            this.CNombre.FillWeight = 200F;
            this.CNombre.HeaderText = "Cliente";
            this.CNombre.MinimumWidth = 150;
            this.CNombre.Name = "CNombre";
            this.CNombre.ReadOnly = true;
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
            // CFechaLimiteP
            // 
            this.CFechaLimiteP.DataPropertyName = "FechaLimiteP";
            this.CFechaLimiteP.FillWeight = 150F;
            this.CFechaLimiteP.HeaderText = "Fecha Limite";
            this.CFechaLimiteP.MinimumWidth = 150;
            this.CFechaLimiteP.Name = "CFechaLimiteP";
            this.CFechaLimiteP.ReadOnly = true;
            this.CFechaLimiteP.Width = 150;
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
            // FrmPendBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnVolver;
            this.ClientSize = new System.Drawing.Size(1092, 615);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1010, 526);
            this.Name = "FrmPendBill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cuentas Por Cobrar Pendientes (Facturas Pendientes)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPendBill_FormClosing);
            this.Load += new System.EventHandler(this.FrmPendBill_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal)).EndInit();
            this.tableLayoutPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilter)).EndInit();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrFechaHora;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtClienteNombre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtConsecutivo;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btnPagar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbEfectivo2;
        private System.Windows.Forms.RadioButton rbSinpe2;
        private System.Windows.Forms.RadioButton rbSinpeMovil2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.NumericUpDown txtTotal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvFilter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnFiltrarHoyTodas;
        private System.Windows.Forms.Button btnFiltrarHoySoloBackHoe;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn CID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CConsecutivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCostoTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNombreEmpleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNombreEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn CFechaFactura;
        private System.Windows.Forms.DataGridViewTextBoxColumn CFechaLimiteP;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSubTotalFact;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIVAFact;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCostoTransporte;
        private System.Windows.Forms.DataGridViewTextBoxColumn CBackHoe;
        private System.Windows.Forms.DataGridViewTextBoxColumn CReferenciaPago;
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