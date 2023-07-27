namespace Agregados.Forms.Login
{
    partial class FrmForgetPass
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPin = new System.Windows.Forms.TextBox();
            this.lblPin = new System.Windows.Forms.Label();
            this.btnValidatePin = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnChange = new System.Windows.Forms.Button();
            this.lblPass = new System.Windows.Forms.Label();
            this.lblConfirmPass = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.txtConfirmPass = new System.Windows.Forms.TextBox();
            this.btnSeePass = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSeePass)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario:";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(75, 58);
            this.txtUser.MaxLength = 15;
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(145, 22);
            this.txtUser.TabIndex = 1;
            this.txtUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUser_KeyPress);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Firebrick;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.ForeColor = System.Drawing.SystemColors.Window;
            this.btnExit.Location = new System.Drawing.Point(12, 189);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Salir";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnSend.Location = new System.Drawing.Point(119, 189);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(101, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Enviar Pin";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(497, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Por favor ingresa su usuario, para enviar al correo registrado un Pin de";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(390, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Recuperación con el que pueda cambiar la contraseña.";
            // 
            // txtPin
            // 
            this.txtPin.Location = new System.Drawing.Point(75, 97);
            this.txtPin.MaxLength = 6;
            this.txtPin.Name = "txtPin";
            this.txtPin.Size = new System.Drawing.Size(145, 22);
            this.txtPin.TabIndex = 7;
            this.txtPin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPin_KeyPress);
            // 
            // lblPin
            // 
            this.lblPin.AutoSize = true;
            this.lblPin.Location = new System.Drawing.Point(40, 100);
            this.lblPin.Name = "lblPin";
            this.lblPin.Size = new System.Drawing.Size(29, 16);
            this.lblPin.TabIndex = 6;
            this.lblPin.Text = "Pin:";
            // 
            // btnValidatePin
            // 
            this.btnValidatePin.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnValidatePin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValidatePin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValidatePin.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnValidatePin.Location = new System.Drawing.Point(12, 134);
            this.btnValidatePin.Name = "btnValidatePin";
            this.btnValidatePin.Size = new System.Drawing.Size(208, 23);
            this.btnValidatePin.TabIndex = 8;
            this.btnValidatePin.Text = "Validar Pin";
            this.btnValidatePin.UseVisualStyleBackColor = false;
            this.btnValidatePin.Click += new System.EventHandler(this.btnValidatePin_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(240, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(362, 154);
            this.panel1.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.07182F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.92818F));
            this.tableLayoutPanel1.Controls.Add(this.btnChange, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblPass, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblConfirmPass, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtPass, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtConfirmPass, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSeePass, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.61538F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.38462F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(362, 154);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnChange
            // 
            this.btnChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChange.BackColor = System.Drawing.Color.LightGreen;
            this.btnChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChange.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnChange.Location = new System.Drawing.Point(100, 91);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(259, 37);
            this.btnChange.TabIndex = 4;
            this.btnChange.Text = "Cambiar Contraseña";
            this.btnChange.UseVisualStyleBackColor = false;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // lblPass
            // 
            this.lblPass.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(9, 6);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(79, 16);
            this.lblPass.TabIndex = 5;
            this.lblPass.Text = "Contraseña:";
            // 
            // lblConfirmPass
            // 
            this.lblConfirmPass.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblConfirmPass.AutoSize = true;
            this.lblConfirmPass.Location = new System.Drawing.Point(9, 31);
            this.lblConfirmPass.Name = "lblConfirmPass";
            this.lblConfirmPass.Size = new System.Drawing.Size(79, 32);
            this.lblConfirmPass.TabIndex = 6;
            this.lblConfirmPass.Text = "Confirmar Contraseña:";
            this.lblConfirmPass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPass
            // 
            this.txtPass.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPass.Location = new System.Drawing.Point(108, 3);
            this.txtPass.MaxLength = 16;
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(242, 22);
            this.txtPass.TabIndex = 7;
            this.txtPass.UseSystemPasswordChar = true;
            // 
            // txtConfirmPass
            // 
            this.txtConfirmPass.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtConfirmPass.Location = new System.Drawing.Point(108, 36);
            this.txtConfirmPass.MaxLength = 16;
            this.txtConfirmPass.Name = "txtConfirmPass";
            this.txtConfirmPass.Size = new System.Drawing.Size(242, 22);
            this.txtConfirmPass.TabIndex = 8;
            this.txtConfirmPass.UseSystemPasswordChar = true;
            // 
            // btnSeePass
            // 
            this.btnSeePass.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSeePass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSeePass.Image = global::Agregados.Properties.Resources.eye;
            this.btnSeePass.Location = new System.Drawing.Point(20, 83);
            this.btnSeePass.Name = "btnSeePass";
            this.btnSeePass.Size = new System.Drawing.Size(57, 52);
            this.btnSeePass.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnSeePass.TabIndex = 9;
            this.btnSeePass.TabStop = false;
            this.btnSeePass.Click += new System.EventHandler(this.btnSeePass_Click);
            // 
            // FrmForgetPass
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(607, 219);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnValidatePin);
            this.Controls.Add(this.txtPin);
            this.Controls.Add(this.lblPin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(623, 258);
            this.MinimumSize = new System.Drawing.Size(623, 258);
            this.Name = "FrmForgetPass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recuperación Contraseña";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSeePass)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPin;
        private System.Windows.Forms.Label lblPin;
        private System.Windows.Forms.Button btnValidatePin;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.Label lblConfirmPass;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtConfirmPass;
        private System.Windows.Forms.PictureBox btnSeePass;
    }
}