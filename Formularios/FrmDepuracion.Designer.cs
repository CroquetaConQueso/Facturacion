namespace FacturacionDAM.Formularios
{
    partial class FrmDepuracion
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
            components = new System.ComponentModel.Container();
            pnBottom = new Panel();
            chbAutoRefresh = new CheckBox();
            btnRefresh = new Button();
            pnLog = new Panel();
            txtLog = new TextBox();
            timerRefresh = new System.Windows.Forms.Timer(components);
            pnBottom.SuspendLayout();
            pnLog.SuspendLayout();
            SuspendLayout();
            // 
            // pnBottom
            // 
            pnBottom.Controls.Add(chbAutoRefresh);
            pnBottom.Controls.Add(btnRefresh);
            pnBottom.Dock = DockStyle.Bottom;
            pnBottom.Location = new Point(0, 394);
            pnBottom.Name = "pnBottom";
            pnBottom.Size = new Size(800, 57);
            pnBottom.TabIndex = 0;
            // 
            // chbAutoRefresh
            // 
            chbAutoRefresh.AutoSize = true;
            chbAutoRefresh.Location = new Point(177, 19);
            chbAutoRefresh.Name = "chbAutoRefresh";
            chbAutoRefresh.Size = new Size(213, 24);
            chbAutoRefresh.TabIndex = 1;
            chbAutoRefresh.Text = "Refrescar automáticametne";
            chbAutoRefresh.UseVisualStyleBackColor = true;
            chbAutoRefresh.CheckedChanged += chkAutoRefrescar_CheckedChanged;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(27, 15);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(94, 29);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "Refrescar";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefrescar_Click;
            // 
            // pnLog
            // 
            pnLog.Controls.Add(txtLog);
            pnLog.Dock = DockStyle.Fill;
            pnLog.Location = new Point(0, 0);
            pnLog.Name = "pnLog";
            pnLog.Size = new Size(800, 394);
            pnLog.TabIndex = 1;
            // 
            // txtLog
            // 
            txtLog.Dock = DockStyle.Fill;
            txtLog.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtLog.ForeColor = Color.Black;
            txtLog.Location = new Point(0, 0);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Both;
            txtLog.Size = new Size(800, 394);
            txtLog.TabIndex = 0;
            // 
            // timerRefresh
            // 
            timerRefresh.Interval = 2000;
            // 
            // FrmDepuracion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 451);
            Controls.Add(pnLog);
            Controls.Add(pnBottom);
            Name = "FrmDepuracion";
            Text = "Depuración";
            Load += FrmDepuracion_Load;
            pnBottom.ResumeLayout(false);
            pnBottom.PerformLayout();
            pnLog.ResumeLayout(false);
            pnLog.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnBottom;
        private Panel pnLog;
        private CheckBox chbAutoRefresh;
        private Button btnRefresh;
        private TextBox txtLog;
        private System.Windows.Forms.Timer timerRefresh;
    }
}