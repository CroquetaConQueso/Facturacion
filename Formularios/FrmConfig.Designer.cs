namespace FacturacionDAM.Formularios
{
    partial class FrmConfig
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfig));
            pnData = new Panel();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            txtPassword = new TextBox();
            txtUsuario = new TextBox();
            txtBaseDatos = new TextBox();
            txtPuerto = new TextBox();
            txtServidor = new TextBox();
            label2 = new Label();
            label1 = new Label();
            label6 = new Label();
            btnConexion = new Button();
            statusStrip1 = new StatusStrip();
            tsStatusLabel = new ToolStripStatusLabel();
            tsProgressBarConexion = new ToolStripProgressBar();
            toolStrip1 = new ToolStrip();
            tsBtnCargar = new ToolStripButton();
            tsBtnGuardar = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            tsLbRutaConfig = new ToolStripLabel();
            pnData.SuspendLayout();
            statusStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pnData
            // 
            pnData.Controls.Add(label5);
            pnData.Controls.Add(label4);
            pnData.Controls.Add(label3);
            pnData.Controls.Add(txtPassword);
            pnData.Controls.Add(txtUsuario);
            pnData.Controls.Add(txtBaseDatos);
            pnData.Controls.Add(txtPuerto);
            pnData.Controls.Add(txtServidor);
            pnData.Controls.Add(label2);
            pnData.Controls.Add(label1);
            pnData.Location = new Point(20, 69);
            pnData.Name = "pnData";
            pnData.Size = new Size(510, 196);
            pnData.TabIndex = 10;
            // 
            // label5
            // 
            label5.Location = new Point(16, 153);
            label5.Name = "label5";
            label5.Size = new Size(100, 23);
            label5.TabIndex = 19;
            label5.Text = "Contraseña:";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.Location = new Point(16, 118);
            label4.Name = "label4";
            label4.Size = new Size(100, 23);
            label4.TabIndex = 18;
            label4.Text = "Usuario:";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Location = new Point(16, 83);
            label3.Name = "label3";
            label3.Size = new Size(100, 23);
            label3.TabIndex = 17;
            label3.Text = "Base de datos:";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(122, 153);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(215, 23);
            txtPassword.TabIndex = 16;
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(122, 118);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(215, 23);
            txtUsuario.TabIndex = 15;
            // 
            // txtBaseDatos
            // 
            txtBaseDatos.Location = new Point(122, 83);
            txtBaseDatos.Name = "txtBaseDatos";
            txtBaseDatos.Size = new Size(215, 23);
            txtBaseDatos.TabIndex = 14;
            // 
            // txtPuerto
            // 
            txtPuerto.Location = new Point(122, 48);
            txtPuerto.MaxLength = 5;
            txtPuerto.Name = "txtPuerto";
            txtPuerto.Size = new Size(64, 23);
            txtPuerto.TabIndex = 13;
            // 
            // txtServidor
            // 
            txtServidor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtServidor.Location = new Point(122, 13);
            txtServidor.Name = "txtServidor";
            txtServidor.Size = new Size(362, 23);
            txtServidor.TabIndex = 12;
            // 
            // label2
            // 
            label2.Location = new Point(16, 48);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 11;
            label2.Text = "Puerto:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.Location = new Point(16, 13);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 10;
            label1.Text = "Servidor:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14F);
            label6.Location = new Point(22, 35);
            label6.Name = "label6";
            label6.Size = new Size(300, 25);
            label6.TabIndex = 11;
            label6.Text = "Conexión a bases de datos MySQL";
            // 
            // btnConexion
            // 
            btnConexion.Location = new Point(142, 271);
            btnConexion.Name = "btnConexion";
            btnConexion.Size = new Size(128, 41);
            btnConexion.TabIndex = 12;
            btnConexion.Text = "Probar conexión";
            btnConexion.UseVisualStyleBackColor = true;
            btnConexion.Click += btnProbarConexion_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { tsStatusLabel, tsProgressBarConexion });
            statusStrip1.Location = new Point(0, 332);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(562, 22);
            statusStrip1.TabIndex = 13;
            statusStrip1.Text = "statusStrip1";
            // 
            // tsStatusLabel
            // 
            tsStatusLabel.Name = "tsStatusLabel";
            tsStatusLabel.Size = new Size(85, 17);
            tsStatusLabel.Text = "No conectado.";
            // 
            // tsProgressBarConexion
            // 
            tsProgressBarConexion.Name = "tsProgressBarConexion";
            tsProgressBarConexion.Size = new Size(100, 16);
            tsProgressBarConexion.Style = ProgressBarStyle.Marquee;
            tsProgressBarConexion.Visible = false;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsBtnCargar, tsBtnGuardar, toolStripSeparator1, tsLbRutaConfig });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(562, 25);
            toolStrip1.TabIndex = 14;
            toolStrip1.Text = "toolStrip1";
            // 
            // tsBtnCargar
            // 
            tsBtnCargar.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnCargar.Image = (Image)resources.GetObject("tsBtnCargar.Image");
            tsBtnCargar.ImageTransparentColor = Color.Magenta;
            tsBtnCargar.Name = "tsBtnCargar";
            tsBtnCargar.Size = new Size(23, 22);
            tsBtnCargar.Text = "Cargar archivo de configuración";
            tsBtnCargar.Click += tsBtnCargar_Click;
            // 
            // tsBtnGuardar
            // 
            tsBtnGuardar.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnGuardar.Image = (Image)resources.GetObject("tsBtnGuardar.Image");
            tsBtnGuardar.ImageTransparentColor = Color.Magenta;
            tsBtnGuardar.Name = "tsBtnGuardar";
            tsBtnGuardar.Size = new Size(23, 22);
            tsBtnGuardar.Text = "toolStripButton1";
            tsBtnGuardar.Click += tsBtnGuardar_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // tsLbRutaConfig
            // 
            tsLbRutaConfig.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsLbRutaConfig.Name = "tsLbRutaConfig";
            tsLbRutaConfig.Size = new Size(0, 22);
            tsLbRutaConfig.TextAlign = ContentAlignment.MiddleLeft;
            tsLbRutaConfig.ToolTipText = "Ruta del archivo de configuración";
            // 
            // FrmConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(562, 354);
            Controls.Add(toolStrip1);
            Controls.Add(statusStrip1);
            Controls.Add(btnConexion);
            Controls.Add(label6);
            Controls.Add(pnData);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmConfig";
            Text = "Conexión a Bases de Datos";
            Load += FrmConnection_Load;
            pnData.ResumeLayout(false);
            pnData.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnData;
        private Label label5;
        private Label label4;
        private Label label3;
        private TextBox txtPassword;
        private TextBox txtUsuario;
        private TextBox txtBaseDatos;
        private TextBox txtPuerto;
        private TextBox txtServidor;
        private Label label2;
        private Label label1;
        private Label label6;
        private Button btnConexion;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tsStatusLabel;
        private ToolStrip toolStrip1;
        private ToolStripButton tsBtnCargar;
        private ToolStripButton tsBtnGuardar;
        private ToolStripLabel tsLbRutaConfig;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripProgressBar tsProgressBarConexion;
    }
}
