using System;

namespace Agregados.Forms.Help
{
    partial class FrmManualUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManualUser));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPdfManualUserExit = new System.Windows.Forms.Button();
            this.pdfDocument = new AxAcroPDFLib.AxAcroPDF();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pdfDocument)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnPdfManualUserExit, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pdfDocument, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnPdfManualUserExit
            // 
            this.btnPdfManualUserExit.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPdfManualUserExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPdfManualUserExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPdfManualUserExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPdfManualUserExit.Location = new System.Drawing.Point(3, 419);
            this.btnPdfManualUserExit.Name = "btnPdfManualUserExit";
            this.btnPdfManualUserExit.Size = new System.Drawing.Size(75, 23);
            this.btnPdfManualUserExit.TabIndex = 0;
            this.btnPdfManualUserExit.Text = "Salir";
            this.btnPdfManualUserExit.UseVisualStyleBackColor = false;
            this.btnPdfManualUserExit.Click += new System.EventHandler(this.btnPdfManualUserExit_Click);
            // 
            // pdfDocument
            // 
            this.pdfDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfDocument.Enabled = true;
            this.pdfDocument.Location = new System.Drawing.Point(3, 3);
            this.pdfDocument.Name = "pdfDocument";
            this.pdfDocument.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("pdfDocument.OcxState")));
            this.pdfDocument.Size = new System.Drawing.Size(794, 410);
            this.pdfDocument.TabIndex = 1;
            // 
            // FrmManualUser
            // 
            this.AcceptButton = this.btnPdfManualUserExit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnPdfManualUserExit;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "FrmManualUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manual de Usuario";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmManualUser_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmManualUser_FormClosed);
            this.Load += new System.EventHandler(this.FrmManualUser_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pdfDocument)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnPdfManualUserExit;
        private AxAcroPDFLib.AxAcroPDF pdfDocument;
    }
}