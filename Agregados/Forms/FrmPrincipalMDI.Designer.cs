namespace Agregados.Forms
{
    partial class FrmPrincipalMDI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipalMDI));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirCajaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarCajaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cierreCajaForzadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mantenimientosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proveedoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.materialesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vehiculosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.notificacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.procesosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.facturaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.facturaciónComprasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeFacturasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualDeUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblUsuarioLogueado = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFechaHora = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrFechaHora = new System.Windows.Forms.Timer(this.components);
            this.reversiónDeFacturaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.mantenimientosToolStripMenuItem,
            this.procesosToolStripMenuItem,
            this.reportesToolStripMenuItem,
            this.acercaDeToolStripMenuItem,
            this.cerrarSesiónToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(1235, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirCajaToolStripMenuItem,
            this.cerrarCajaToolStripMenuItem,
            this.cierreCajaForzadoToolStripMenuItem,
            this.salirToolStripMenuItem,
            this.salirToolStripMenuItem1});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(54, 21);
            this.archivoToolStripMenuItem.Text = "Inicio";
            // 
            // abrirCajaToolStripMenuItem
            // 
            this.abrirCajaToolStripMenuItem.Name = "abrirCajaToolStripMenuItem";
            this.abrirCajaToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.abrirCajaToolStripMenuItem.Text = "Abrir Caja";
            this.abrirCajaToolStripMenuItem.Click += new System.EventHandler(this.abrirCajaToolStripMenuItem_Click);
            // 
            // cerrarCajaToolStripMenuItem
            // 
            this.cerrarCajaToolStripMenuItem.Name = "cerrarCajaToolStripMenuItem";
            this.cerrarCajaToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.cerrarCajaToolStripMenuItem.Text = "Cerrar Caja";
            this.cerrarCajaToolStripMenuItem.Click += new System.EventHandler(this.cerrarCajaToolStripMenuItem_Click);
            // 
            // cierreCajaForzadoToolStripMenuItem
            // 
            this.cierreCajaForzadoToolStripMenuItem.Name = "cierreCajaForzadoToolStripMenuItem";
            this.cierreCajaForzadoToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.cierreCajaForzadoToolStripMenuItem.Text = "Cierre Caja Forzado";
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.salirToolStripMenuItem.Text = "Cerrar Sesión";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem1
            // 
            this.salirToolStripMenuItem1.Name = "salirToolStripMenuItem1";
            this.salirToolStripMenuItem1.Size = new System.Drawing.Size(195, 22);
            this.salirToolStripMenuItem1.Text = "Salir";
            this.salirToolStripMenuItem1.Click += new System.EventHandler(this.salirToolStripMenuItem1_Click);
            // 
            // mantenimientosToolStripMenuItem
            // 
            this.mantenimientosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usuariosToolStripMenuItem,
            this.toolStripSeparator1,
            this.clientesToolStripMenuItem,
            this.proveedoresToolStripMenuItem,
            this.toolStripSeparator2,
            this.materialesToolStripMenuItem,
            this.vehiculosToolStripMenuItem,
            this.toolStripSeparator3,
            this.notificacionesToolStripMenuItem});
            this.mantenimientosToolStripMenuItem.Name = "mantenimientosToolStripMenuItem";
            this.mantenimientosToolStripMenuItem.Size = new System.Drawing.Size(55, 21);
            this.mantenimientosToolStripMenuItem.Text = "Menú";
            // 
            // usuariosToolStripMenuItem
            // 
            this.usuariosToolStripMenuItem.Name = "usuariosToolStripMenuItem";
            this.usuariosToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.usuariosToolStripMenuItem.Text = "Usuarios";
            this.usuariosToolStripMenuItem.Click += new System.EventHandler(this.usuariosToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // clientesToolStripMenuItem
            // 
            this.clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            this.clientesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.clientesToolStripMenuItem.Text = "Clientes";
            this.clientesToolStripMenuItem.Click += new System.EventHandler(this.clientesToolStripMenuItem_Click);
            // 
            // proveedoresToolStripMenuItem
            // 
            this.proveedoresToolStripMenuItem.Name = "proveedoresToolStripMenuItem";
            this.proveedoresToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.proveedoresToolStripMenuItem.Text = "Proveedores";
            this.proveedoresToolStripMenuItem.Click += new System.EventHandler(this.proveedoresToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // materialesToolStripMenuItem
            // 
            this.materialesToolStripMenuItem.Name = "materialesToolStripMenuItem";
            this.materialesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.materialesToolStripMenuItem.Text = "Materiales";
            this.materialesToolStripMenuItem.Click += new System.EventHandler(this.materialesToolStripMenuItem_Click);
            // 
            // vehiculosToolStripMenuItem
            // 
            this.vehiculosToolStripMenuItem.Name = "vehiculosToolStripMenuItem";
            this.vehiculosToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.vehiculosToolStripMenuItem.Text = "Vehiculos";
            this.vehiculosToolStripMenuItem.Click += new System.EventHandler(this.vehiculosToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // notificacionesToolStripMenuItem
            // 
            this.notificacionesToolStripMenuItem.Name = "notificacionesToolStripMenuItem";
            this.notificacionesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.notificacionesToolStripMenuItem.Text = "Notificaciones";
            this.notificacionesToolStripMenuItem.Click += new System.EventHandler(this.notificacionesToolStripMenuItem_Click);
            // 
            // procesosToolStripMenuItem
            // 
            this.procesosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.facturaciónToolStripMenuItem,
            this.facturaciónComprasToolStripMenuItem,
            this.reversiónDeFacturaToolStripMenuItem});
            this.procesosToolStripMenuItem.Name = "procesosToolStripMenuItem";
            this.procesosToolStripMenuItem.Size = new System.Drawing.Size(74, 21);
            this.procesosToolStripMenuItem.Text = "Procesos";
            // 
            // facturaciónToolStripMenuItem
            // 
            this.facturaciónToolStripMenuItem.Name = "facturaciónToolStripMenuItem";
            this.facturaciónToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.facturaciónToolStripMenuItem.Text = "Facturación Ventas";
            this.facturaciónToolStripMenuItem.Click += new System.EventHandler(this.facturaciónToolStripMenuItem_Click);
            // 
            // facturaciónComprasToolStripMenuItem
            // 
            this.facturaciónComprasToolStripMenuItem.Name = "facturaciónComprasToolStripMenuItem";
            this.facturaciónComprasToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.facturaciónComprasToolStripMenuItem.Text = "Facturación Compras";
            this.facturaciónComprasToolStripMenuItem.Click += new System.EventHandler(this.facturaciónComprasToolStripMenuItem_Click);
            // 
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reporteDeFacturasToolStripMenuItem});
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(74, 21);
            this.reportesToolStripMenuItem.Text = "Reportes";
            // 
            // reporteDeFacturasToolStripMenuItem
            // 
            this.reporteDeFacturasToolStripMenuItem.Name = "reporteDeFacturasToolStripMenuItem";
            this.reporteDeFacturasToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.reporteDeFacturasToolStripMenuItem.Text = "Reporte de Facturas";
            this.reporteDeFacturasToolStripMenuItem.Click += new System.EventHandler(this.reporteDeFacturasToolStripMenuItem_Click);
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manualDeUsuarioToolStripMenuItem,
            this.acercaDeToolStripMenuItem1});
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.acercaDeToolStripMenuItem.Text = "Ayuda";
            // 
            // manualDeUsuarioToolStripMenuItem
            // 
            this.manualDeUsuarioToolStripMenuItem.Name = "manualDeUsuarioToolStripMenuItem";
            this.manualDeUsuarioToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.manualDeUsuarioToolStripMenuItem.Text = "Manual de Usuario";
            // 
            // acercaDeToolStripMenuItem1
            // 
            this.acercaDeToolStripMenuItem1.Name = "acercaDeToolStripMenuItem1";
            this.acercaDeToolStripMenuItem1.Size = new System.Drawing.Size(192, 22);
            this.acercaDeToolStripMenuItem1.Text = "Acerca de...";
            this.acercaDeToolStripMenuItem1.Click += new System.EventHandler(this.acercaDeToolStripMenuItem1_Click);
            // 
            // cerrarSesiónToolStripMenuItem
            // 
            this.cerrarSesiónToolStripMenuItem.Name = "cerrarSesiónToolStripMenuItem";
            this.cerrarSesiónToolStripMenuItem.Size = new System.Drawing.Size(101, 21);
            this.cerrarSesiónToolStripMenuItem.Text = "Cerrar Sesión";
            this.cerrarSesiónToolStripMenuItem.Click += new System.EventHandler(this.cerrarSesiónToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BackgroundImage = global::Agregados.Properties.Resources.MDIPrincipal;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1229, 516);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1235, 545);
            this.panel2.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.77982F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.220183F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1235, 545);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblUsuarioLogueado,
            this.lblFechaHora});
            this.statusStrip1.Location = new System.Drawing.Point(0, 523);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1235, 22);
            this.statusStrip1.TabIndex = 32;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabel1.Text = "USUARIO:";
            // 
            // lblUsuarioLogueado
            // 
            this.lblUsuarioLogueado.Name = "lblUsuarioLogueado";
            this.lblUsuarioLogueado.Size = new System.Drawing.Size(58, 17);
            this.lblUsuarioLogueado.Text = "Nombre";
            // 
            // lblFechaHora
            // 
            this.lblFechaHora.Name = "lblFechaHora";
            this.lblFechaHora.Size = new System.Drawing.Size(1094, 17);
            this.lblFechaHora.Spring = true;
            this.lblFechaHora.Text = "FechaHora";
            this.lblFechaHora.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmrFechaHora
            // 
            this.tmrFechaHora.Tick += new System.EventHandler(this.tmrFechaHora_Tick);
            // 
            // reversiónDeFacturaToolStripMenuItem
            // 
            this.reversiónDeFacturaToolStripMenuItem.Name = "reversiónDeFacturaToolStripMenuItem";
            this.reversiónDeFacturaToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.reversiónDeFacturaToolStripMenuItem.Text = "Reversión de Facturas";
            this.reversiónDeFacturaToolStripMenuItem.Click += new System.EventHandler(this.reversiónDeFacturaToolStripMenuItem_Click);
            // 
            // FrmPrincipalMDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1235, 570);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1226, 600);
            this.Name = "FrmPrincipalMDI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Página Principal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPrincipalMDI_FormClosing);
            this.Load += new System.EventHandler(this.FrmPrincipalMDI_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mantenimientosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuariosToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem proveedoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem materialesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vehiculosToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem notificacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem procesosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem facturaciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesiónToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem manualDeUsuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblUsuarioLogueado;
        private System.Windows.Forms.ToolStripStatusLabel lblFechaHora;
        private System.Windows.Forms.Timer tmrFechaHora;
        private System.Windows.Forms.ToolStripMenuItem reporteDeFacturasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem facturaciónComprasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirCajaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarCajaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cierreCajaForzadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reversiónDeFacturaToolStripMenuItem;
    }
}