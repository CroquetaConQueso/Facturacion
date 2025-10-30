namespace FacturacionDAM.Formularios
{
    partial class FrmEmisor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEmisor));
            pnButtons = new Panel();
            btnCancelar = new Button();
            btnAceptar = new Button();
            tbControl = new TabControl();
            tbDatos = new TabPage();
            gbFacturacion = new GroupBox();
            lblPre = new Label();
            lblSigNum = new Label();
            txtBoxPre = new TextBox();
            txtBoxSigNum = new TextBox();
            gbContacto = new GroupBox();
            lblEmail = new Label();
            lblTel1 = new Label();
            lblTel2 = new Label();
            txtBoxEmail = new TextBox();
            txtBoxTel1 = new TextBox();
            txtBoxTel2 = new TextBox();
            gbDomicilio = new GroupBox();
            lblProvincia = new Label();
            lblCP = new Label();
            lblPoblacion = new Label();
            lblDom = new Label();
            cbProvincia = new ComboBox();
            txtBoxCP = new TextBox();
            txtBoxPob = new TextBox();
            txtBoxDom = new TextBox();
            gbIdentidad = new GroupBox();
            lblApellidos = new Label();
            lblNombre = new Label();
            lblRazonSocial = new Label();
            lblNifCif = new Label();
            txtBoxNifCif = new TextBox();
            txtBoxNombre = new TextBox();
            txtBoxRazonSocial = new TextBox();
            txBoxtApellidos = new TextBox();
            tbDetalles = new TabPage();
            rtbDetalles = new RichTextBox();
            pnButtons.SuspendLayout();
            tbControl.SuspendLayout();
            tbDatos.SuspendLayout();
            gbFacturacion.SuspendLayout();
            gbContacto.SuspendLayout();
            gbDomicilio.SuspendLayout();
            gbIdentidad.SuspendLayout();
            tbDetalles.SuspendLayout();
            SuspendLayout();
            // 
            // pnButtons
            // 
            pnButtons.Controls.Add(btnCancelar);
            pnButtons.Controls.Add(btnAceptar);
            pnButtons.Dock = DockStyle.Bottom;
            pnButtons.Location = new Point(0, 553);
            pnButtons.Name = "pnButtons";
            pnButtons.Size = new Size(847, 66);
            pnButtons.TabIndex = 1;
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Image = (Image)resources.GetObject("btnCancelar.Image");
            btnCancelar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelar.Location = new Point(456, 16);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Padding = new Padding(20, 0, 0, 0);
            btnCancelar.Size = new Size(128, 32);
            btnCancelar.TabIndex = 1;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            btnAceptar.DialogResult = DialogResult.OK;
            btnAceptar.Image = (Image)resources.GetObject("btnAceptar.Image");
            btnAceptar.ImageAlign = ContentAlignment.MiddleLeft;
            btnAceptar.Location = new Point(264, 16);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Padding = new Padding(20, 0, 0, 0);
            btnAceptar.Size = new Size(128, 32);
            btnAceptar.TabIndex = 0;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = true;
            // 
            // tbControl
            // 
            tbControl.Controls.Add(tbDatos);
            tbControl.Controls.Add(tbDetalles);
            tbControl.Dock = DockStyle.Fill;
            tbControl.Location = new Point(0, 0);
            tbControl.Name = "tbControl";
            tbControl.SelectedIndex = 0;
            tbControl.Size = new Size(847, 553);
            tbControl.TabIndex = 0;
            // 
            // tbDatos
            // 
            tbDatos.Controls.Add(gbFacturacion);
            tbDatos.Controls.Add(gbContacto);
            tbDatos.Controls.Add(gbDomicilio);
            tbDatos.Controls.Add(gbIdentidad);
            tbDatos.Location = new Point(4, 24);
            tbDatos.Name = "tbDatos";
            tbDatos.Padding = new Padding(3);
            tbDatos.Size = new Size(839, 525);
            tbDatos.TabIndex = 0;
            tbDatos.Text = "Datos";
            tbDatos.UseVisualStyleBackColor = true;
            // 
            // gbFacturacion
            // 
            gbFacturacion.Controls.Add(lblPre);
            gbFacturacion.Controls.Add(lblSigNum);
            gbFacturacion.Controls.Add(txtBoxPre);
            gbFacturacion.Controls.Add(txtBoxSigNum);
            gbFacturacion.Location = new Point(16, 424);
            gbFacturacion.Name = "gbFacturacion";
            gbFacturacion.Size = new Size(808, 80);
            gbFacturacion.TabIndex = 3;
            gbFacturacion.TabStop = false;
            gbFacturacion.Text = "Facturación";
            // 
            // lblPre
            // 
            lblPre.AutoSize = true;
            lblPre.Location = new Point(280, 39);
            lblPre.Name = "lblPre";
            lblPre.Size = new Size(44, 15);
            lblPre.TabIndex = 2;
            lblPre.Text = "Prefijo:";
            // 
            // lblSigNum
            // 
            lblSigNum.AutoSize = true;
            lblSigNum.Location = new Point(24, 40);
            lblSigNum.Name = "lblSigNum";
            lblSigNum.Size = new Size(74, 15);
            lblSigNum.TabIndex = 0;
            lblSigNum.Text = "Siguiente nº:";
            // 
            // txtBoxPre
            // 
            txtBoxPre.Location = new Point(328, 35);
            txtBoxPre.Name = "txtBoxPre";
            txtBoxPre.Size = new Size(100, 23);
            txtBoxPre.TabIndex = 3;
            // 
            // txtBoxSigNum
            // 
            txtBoxSigNum.Location = new Point(104, 36);
            txtBoxSigNum.Name = "txtBoxSigNum";
            txtBoxSigNum.Size = new Size(100, 23);
            txtBoxSigNum.TabIndex = 1;
            // 
            // gbContacto
            // 
            gbContacto.Controls.Add(lblEmail);
            gbContacto.Controls.Add(lblTel1);
            gbContacto.Controls.Add(lblTel2);
            gbContacto.Controls.Add(txtBoxEmail);
            gbContacto.Controls.Add(txtBoxTel1);
            gbContacto.Controls.Add(txtBoxTel2);
            gbContacto.Location = new Point(16, 288);
            gbContacto.Name = "gbContacto";
            gbContacto.Size = new Size(808, 120);
            gbContacto.TabIndex = 2;
            gbContacto.TabStop = false;
            gbContacto.Text = "Contacto";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(56, 88);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(39, 15);
            lblEmail.TabIndex = 4;
            lblEmail.Text = "Email:";
            // 
            // lblTel1
            // 
            lblTel1.AutoSize = true;
            lblTel1.Location = new Point(33, 35);
            lblTel1.Name = "lblTel1";
            lblTel1.Size = new Size(65, 15);
            lblTel1.TabIndex = 0;
            lblTel1.Text = "Teléfono 1:";
            lblTel1.Click += lblTel1_Click;
            // 
            // lblTel2
            // 
            lblTel2.AutoSize = true;
            lblTel2.Location = new Point(363, 35);
            lblTel2.Name = "lblTel2";
            lblTel2.Size = new Size(65, 15);
            lblTel2.TabIndex = 2;
            lblTel2.Text = "Teléfono 2:";
            // 
            // txtBoxEmail
            // 
            txtBoxEmail.Location = new Point(104, 80);
            txtBoxEmail.Name = "txtBoxEmail";
            txtBoxEmail.Size = new Size(512, 23);
            txtBoxEmail.TabIndex = 5;
            // 
            // txtBoxTel1
            // 
            txtBoxTel1.Location = new Point(104, 32);
            txtBoxTel1.Name = "txtBoxTel1";
            txtBoxTel1.Size = new Size(180, 23);
            txtBoxTel1.TabIndex = 1;
            // 
            // txtBoxTel2
            // 
            txtBoxTel2.Location = new Point(434, 32);
            txtBoxTel2.Name = "txtBoxTel2";
            txtBoxTel2.Size = new Size(180, 23);
            txtBoxTel2.TabIndex = 3;
            // 
            // gbDomicilio
            // 
            gbDomicilio.Controls.Add(lblProvincia);
            gbDomicilio.Controls.Add(lblCP);
            gbDomicilio.Controls.Add(lblPoblacion);
            gbDomicilio.Controls.Add(lblDom);
            gbDomicilio.Controls.Add(cbProvincia);
            gbDomicilio.Controls.Add(txtBoxCP);
            gbDomicilio.Controls.Add(txtBoxPob);
            gbDomicilio.Controls.Add(txtBoxDom);
            gbDomicilio.Location = new Point(16, 152);
            gbDomicilio.Name = "gbDomicilio";
            gbDomicilio.Size = new Size(808, 120);
            gbDomicilio.TabIndex = 1;
            gbDomicilio.TabStop = false;
            gbDomicilio.Text = "Domicilio";
            // 
            // lblProvincia
            // 
            lblProvincia.AutoSize = true;
            lblProvincia.Location = new Point(520, 84);
            lblProvincia.Name = "lblProvincia";
            lblProvincia.Size = new Size(59, 15);
            lblProvincia.TabIndex = 6;
            lblProvincia.Text = "Provincia:";
            // 
            // lblCP
            // 
            lblCP.AutoSize = true;
            lblCP.Location = new Point(608, 37);
            lblCP.Name = "lblCP";
            lblCP.Size = new Size(84, 15);
            lblCP.TabIndex = 2;
            lblCP.Text = "Código Postal:";
            // 
            // lblPoblacion
            // 
            lblPoblacion.AutoSize = true;
            lblPoblacion.Location = new Point(40, 85);
            lblPoblacion.Name = "lblPoblacion";
            lblPoblacion.Size = new Size(63, 15);
            lblPoblacion.TabIndex = 4;
            lblPoblacion.Text = "Población:";
            // 
            // lblDom
            // 
            lblDom.AutoSize = true;
            lblDom.Location = new Point(40, 37);
            lblDom.Name = "lblDom";
            lblDom.Size = new Size(61, 15);
            lblDom.TabIndex = 0;
            lblDom.Text = "Domicilio:";
            // 
            // cbProvincia
            // 
            cbProvincia.FormattingEnabled = true;
            cbProvincia.Location = new Point(584, 80);
            cbProvincia.Name = "cbProvincia";
            cbProvincia.Size = new Size(208, 23);
            cbProvincia.TabIndex = 7;
            // 
            // txtBoxCP
            // 
            txtBoxCP.Location = new Point(696, 32);
            txtBoxCP.Name = "txtBoxCP";
            txtBoxCP.Size = new Size(100, 23);
            txtBoxCP.TabIndex = 3;
            // 
            // txtBoxPob
            // 
            txtBoxPob.Location = new Point(104, 80);
            txtBoxPob.Name = "txtBoxPob";
            txtBoxPob.Size = new Size(392, 23);
            txtBoxPob.TabIndex = 5;
            // 
            // txtBoxDom
            // 
            txtBoxDom.Location = new Point(104, 32);
            txtBoxDom.Name = "txtBoxDom";
            txtBoxDom.PlaceholderText = "Calle, número, planta....";
            txtBoxDom.Size = new Size(488, 23);
            txtBoxDom.TabIndex = 1;
            // 
            // gbIdentidad
            // 
            gbIdentidad.Controls.Add(lblApellidos);
            gbIdentidad.Controls.Add(lblNombre);
            gbIdentidad.Controls.Add(lblRazonSocial);
            gbIdentidad.Controls.Add(lblNifCif);
            gbIdentidad.Controls.Add(txtBoxNifCif);
            gbIdentidad.Controls.Add(txtBoxNombre);
            gbIdentidad.Controls.Add(txtBoxRazonSocial);
            gbIdentidad.Controls.Add(txBoxtApellidos);
            gbIdentidad.Location = new Point(16, 16);
            gbIdentidad.Name = "gbIdentidad";
            gbIdentidad.Size = new Size(808, 120);
            gbIdentidad.TabIndex = 0;
            gbIdentidad.TabStop = false;
            gbIdentidad.Text = "Identidad";
            // 
            // lblApellidos
            // 
            lblApellidos.AutoSize = true;
            lblApellidos.Location = new Point(392, 85);
            lblApellidos.Name = "lblApellidos";
            lblApellidos.Size = new Size(59, 15);
            lblApellidos.TabIndex = 6;
            lblApellidos.Text = "Apellidos:";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(395, 37);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(54, 15);
            lblNombre.TabIndex = 4;
            lblNombre.Text = "Nombre:";
            // 
            // lblRazonSocial
            // 
            lblRazonSocial.AutoSize = true;
            lblRazonSocial.Location = new Point(25, 84);
            lblRazonSocial.Name = "lblRazonSocial";
            lblRazonSocial.Size = new Size(76, 15);
            lblRazonSocial.TabIndex = 2;
            lblRazonSocial.Text = "Razón Social:";
            // 
            // lblNifCif
            // 
            lblNifCif.AutoSize = true;
            lblNifCif.Location = new Point(49, 37);
            lblNifCif.Name = "lblNifCif";
            lblNifCif.Size = new Size(50, 15);
            lblNifCif.TabIndex = 0;
            lblNifCif.Text = "NIF/CIF:";
            // 
            // txtBoxNifCif
            // 
            txtBoxNifCif.Location = new Point(104, 32);
            txtBoxNifCif.Name = "txtBoxNifCif";
            txtBoxNifCif.Size = new Size(152, 23);
            txtBoxNifCif.TabIndex = 1;
            // 
            // txtBoxNombre
            // 
            txtBoxNombre.Location = new Point(456, 32);
            txtBoxNombre.Name = "txtBoxNombre";
            txtBoxNombre.Size = new Size(224, 23);
            txtBoxNombre.TabIndex = 5;
            // 
            // txtBoxRazonSocial
            // 
            txtBoxRazonSocial.Location = new Point(104, 80);
            txtBoxRazonSocial.Name = "txtBoxRazonSocial";
            txtBoxRazonSocial.Size = new Size(264, 23);
            txtBoxRazonSocial.TabIndex = 3;
            // 
            // txBoxtApellidos
            // 
            txBoxtApellidos.Location = new Point(456, 80);
            txBoxtApellidos.Name = "txBoxtApellidos";
            txBoxtApellidos.Size = new Size(328, 23);
            txBoxtApellidos.TabIndex = 7;
            // 
            // tbDetalles
            // 
            tbDetalles.Controls.Add(rtbDetalles);
            tbDetalles.Location = new Point(4, 24);
            tbDetalles.Name = "tbDetalles";
            tbDetalles.Padding = new Padding(3);
            tbDetalles.Size = new Size(839, 525);
            tbDetalles.TabIndex = 1;
            tbDetalles.Text = "Otros detalles";
            tbDetalles.UseVisualStyleBackColor = true;
            // 
            // rtbDetalles
            // 
            rtbDetalles.Dock = DockStyle.Fill;
            rtbDetalles.Location = new Point(3, 3);
            rtbDetalles.Name = "rtbDetalles";
            rtbDetalles.Size = new Size(833, 519);
            rtbDetalles.TabIndex = 0;
            rtbDetalles.Text = "";
            // 
            // FrmEmisor
            // 
            AcceptButton = btnAceptar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelar;
            ClientSize = new Size(847, 619);
            Controls.Add(tbControl);
            Controls.Add(pnButtons);
            MaximizeBox = false;
            MaximumSize = new Size(863, 658);
            MinimizeBox = false;
            MinimumSize = new Size(863, 658);
            Name = "FrmEmisor";
            Text = "Datos del Emisor";
            pnButtons.ResumeLayout(false);
            tbControl.ResumeLayout(false);
            tbDatos.ResumeLayout(false);
            gbFacturacion.ResumeLayout(false);
            gbFacturacion.PerformLayout();
            gbContacto.ResumeLayout(false);
            gbContacto.PerformLayout();
            gbDomicilio.ResumeLayout(false);
            gbDomicilio.PerformLayout();
            gbIdentidad.ResumeLayout(false);
            gbIdentidad.PerformLayout();
            tbDetalles.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnButtons;
        private Button btnCancelar;
        private Button btnAceptar;
        private TabControl tbControl;
        private TabPage tbDatos;
        private GroupBox gbDomicilio;
        private GroupBox gbIdentidad;
        private TabPage tbDetalles;
        private GroupBox gbFacturacion;
        private GroupBox gbContacto;
        private RichTextBox rtbDetalles;
        private Label lblEmail;
        private Label lblTel1;
        private Label lblTel2;
        private TextBox txtBoxEmail;
        private TextBox txtBoxTel1;
        private TextBox txtBoxTel2;
        private Label lblProvincia;
        private Label lblCP;
        private Label lblPoblacion;
        private Label lblDom;
        private ComboBox cbProvincia;
        private TextBox txtBoxCP;
        private TextBox txtBoxPob;
        private TextBox txtBoxDom;
        private Label lblApellidos;
        private Label lblNombre;
        private Label lblRazonSocial;
        private Label lblNifCif;
        private TextBox txBoxtApellidos;
        private TextBox txtBoxNombre;
        private TextBox txtBoxRazonSocial;
        private TextBox txtBoxNifCif;
        private Label lblSigNum;
        private TextBox txtBoxPre;
        private TextBox txtBoxSigNum;
        private Label lblPre;
    }
}