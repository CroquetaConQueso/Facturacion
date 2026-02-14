namespace FacturacionDAM.Formularios
{
    partial class FrmProveedor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProveedor));
            pnBtns = new Panel();
            btnCancelar = new Button();
            btnAceptar = new Button();
            pnData = new Panel();
            gbContacto = new GroupBox();
            txtEmail = new TextBox();
            label13 = new Label();
            txtTel2 = new TextBox();
            label12 = new Label();
            txtTel1 = new TextBox();
            label11 = new Label();
            gbDomicilio = new GroupBox();
            cbProv = new ComboBox();
            label10 = new Label();
            txtPob = new TextBox();
            label9 = new Label();
            txtCp = new TextBox();
            label8 = new Label();
            txtDomicilio = new TextBox();
            label7 = new Label();
            gbIdentificacion = new GroupBox();
            txtApellidos = new TextBox();
            txtNombre = new TextBox();
            label3 = new Label();
            label4 = new Label();
            txtNombreComercial = new TextBox();
            txtNifCif = new TextBox();
            label2 = new Label();
            label1 = new Label();
            pnBtns.SuspendLayout();
            pnData.SuspendLayout();
            gbContacto.SuspendLayout();
            gbDomicilio.SuspendLayout();
            gbIdentificacion.SuspendLayout();
            SuspendLayout();
            // 
            // pnBtns
            // 
            pnBtns.Controls.Add(btnCancelar);
            pnBtns.Controls.Add(btnAceptar);
            pnBtns.Dock = DockStyle.Bottom;
            pnBtns.Location = new Point(0, 501);
            pnBtns.Margin = new Padding(3, 4, 3, 4);
            pnBtns.Name = "pnBtns";
            pnBtns.Size = new Size(918, 84);
            pnBtns.TabIndex = 0;
            // 
            // btnCancelar
            // 
            btnCancelar.Image = (Image)resources.GetObject("btnCancelar.Image");
            btnCancelar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelar.Location = new Point(517, 19);
            btnCancelar.Margin = new Padding(3, 4, 3, 4);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Padding = new Padding(11, 0, 11, 0);
            btnCancelar.Size = new Size(114, 48);
            btnCancelar.TabIndex = 1;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextAlign = ContentAlignment.MiddleRight;
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnAceptar
            // 
            btnAceptar.Image = (Image)resources.GetObject("btnAceptar.Image");
            btnAceptar.ImageAlign = ContentAlignment.MiddleLeft;
            btnAceptar.Location = new Point(285, 19);
            btnAceptar.Margin = new Padding(3, 4, 3, 4);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Padding = new Padding(11, 0, 11, 0);
            btnAceptar.Size = new Size(114, 48);
            btnAceptar.TabIndex = 0;
            btnAceptar.Text = "Aceptar";
            btnAceptar.TextAlign = ContentAlignment.MiddleRight;
            btnAceptar.UseVisualStyleBackColor = true;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // pnData
            // 
            pnData.Controls.Add(gbContacto);
            pnData.Controls.Add(gbDomicilio);
            pnData.Controls.Add(gbIdentificacion);
            pnData.Dock = DockStyle.Fill;
            pnData.Location = new Point(0, 0);
            pnData.Margin = new Padding(3, 4, 3, 4);
            pnData.Name = "pnData";
            pnData.Size = new Size(918, 501);
            pnData.TabIndex = 1;
            // 
            // gbContacto
            // 
            gbContacto.Controls.Add(txtEmail);
            gbContacto.Controls.Add(label13);
            gbContacto.Controls.Add(txtTel2);
            gbContacto.Controls.Add(label12);
            gbContacto.Controls.Add(txtTel1);
            gbContacto.Controls.Add(label11);
            gbContacto.Location = new Point(30, 323);
            gbContacto.Margin = new Padding(3, 4, 3, 4);
            gbContacto.Name = "gbContacto";
            gbContacto.Padding = new Padding(3, 4, 3, 4);
            gbContacto.Size = new Size(850, 144);
            gbContacto.TabIndex = 6;
            gbContacto.TabStop = false;
            gbContacto.Text = "Contacto";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(120, 84);
            txtEmail.Margin = new Padding(3, 4, 3, 4);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(506, 27);
            txtEmail.TabIndex = 5;
            // 
            // label13
            // 
            label13.Location = new Point(11, 84);
            label13.Name = "label13";
            label13.Size = new Size(98, 31);
            label13.TabIndex = 4;
            label13.Text = "Email:";
            label13.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtTel2
            // 
            txtTel2.Location = new Point(442, 35);
            txtTel2.Margin = new Padding(3, 4, 3, 4);
            txtTel2.MaxLength = 20;
            txtTel2.Name = "txtTel2";
            txtTel2.Size = new Size(183, 27);
            txtTel2.TabIndex = 3;
            // 
            // label12
            // 
            label12.Location = new Point(358, 40);
            label12.Name = "label12";
            label12.Size = new Size(73, 20);
            label12.TabIndex = 2;
            label12.Text = "Teléfono 2:";
            label12.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtTel1
            // 
            txtTel1.Location = new Point(120, 35);
            txtTel1.Margin = new Padding(3, 4, 3, 4);
            txtTel1.MaxLength = 20;
            txtTel1.Name = "txtTel1";
            txtTel1.Size = new Size(183, 27);
            txtTel1.TabIndex = 1;
            // 
            // label11
            // 
            label11.Location = new Point(11, 35);
            label11.Name = "label11";
            label11.Size = new Size(98, 31);
            label11.TabIndex = 0;
            label11.Text = "Teléfono 1:";
            label11.TextAlign = ContentAlignment.MiddleRight;
            // 
            // gbDomicilio
            // 
            gbDomicilio.Controls.Add(cbProv);
            gbDomicilio.Controls.Add(label10);
            gbDomicilio.Controls.Add(txtPob);
            gbDomicilio.Controls.Add(label9);
            gbDomicilio.Controls.Add(txtCp);
            gbDomicilio.Controls.Add(label8);
            gbDomicilio.Controls.Add(txtDomicilio);
            gbDomicilio.Controls.Add(label7);
            gbDomicilio.Location = new Point(30, 169);
            gbDomicilio.Margin = new Padding(3, 4, 3, 4);
            gbDomicilio.Name = "gbDomicilio";
            gbDomicilio.Padding = new Padding(3, 4, 3, 4);
            gbDomicilio.Size = new Size(850, 145);
            gbDomicilio.TabIndex = 5;
            gbDomicilio.TabStop = false;
            gbDomicilio.Text = "Domicilio";
            // 
            // cbProv
            // 
            cbProv.FormattingEnabled = true;
            cbProv.Location = new Point(605, 81);
            cbProv.Margin = new Padding(3, 4, 3, 4);
            cbProv.Name = "cbProv";
            cbProv.Size = new Size(225, 28);
            cbProv.TabIndex = 7;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(530, 87);
            label10.Name = "label10";
            label10.Size = new Size(72, 20);
            label10.TabIndex = 6;
            label10.Text = "Provincia:";
            // 
            // txtPob
            // 
            txtPob.Location = new Point(120, 81);
            txtPob.Margin = new Padding(3, 4, 3, 4);
            txtPob.Name = "txtPob";
            txtPob.Size = new Size(382, 27);
            txtPob.TabIndex = 5;
            // 
            // label9
            // 
            label9.Location = new Point(11, 81);
            label9.Name = "label9";
            label9.Size = new Size(98, 31);
            label9.TabIndex = 4;
            label9.Text = "Población:";
            label9.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtCp
            // 
            txtCp.Location = new Point(744, 36);
            txtCp.Margin = new Padding(3, 4, 3, 4);
            txtCp.MaxLength = 5;
            txtCp.Name = "txtCp";
            txtCp.Size = new Size(85, 27);
            txtCp.TabIndex = 3;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(635, 41);
            label8.Name = "label8";
            label8.Size = new Size(104, 20);
            label8.TabIndex = 2;
            label8.Text = "Código Postal:";
            // 
            // txtDomicilio
            // 
            txtDomicilio.Location = new Point(120, 36);
            txtDomicilio.Margin = new Padding(3, 4, 3, 4);
            txtDomicilio.Name = "txtDomicilio";
            txtDomicilio.PlaceholderText = "Calle, número, planta, ...";
            txtDomicilio.Size = new Size(477, 27);
            txtDomicilio.TabIndex = 1;
            // 
            // label7
            // 
            label7.Location = new Point(11, 36);
            label7.Name = "label7";
            label7.Size = new Size(98, 31);
            label7.TabIndex = 0;
            label7.Text = "Domicilio:";
            label7.TextAlign = ContentAlignment.MiddleRight;
            // 
            // gbIdentificacion
            // 
            gbIdentificacion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbIdentificacion.Controls.Add(txtApellidos);
            gbIdentificacion.Controls.Add(txtNombre);
            gbIdentificacion.Controls.Add(label3);
            gbIdentificacion.Controls.Add(label4);
            gbIdentificacion.Controls.Add(txtNombreComercial);
            gbIdentificacion.Controls.Add(txtNifCif);
            gbIdentificacion.Controls.Add(label2);
            gbIdentificacion.Controls.Add(label1);
            gbIdentificacion.Location = new Point(30, 28);
            gbIdentificacion.Margin = new Padding(3, 4, 3, 4);
            gbIdentificacion.Name = "gbIdentificacion";
            gbIdentificacion.Padding = new Padding(3, 4, 3, 4);
            gbIdentificacion.Size = new Size(841, 133);
            gbIdentificacion.TabIndex = 4;
            gbIdentificacion.TabStop = false;
            gbIdentificacion.Text = "Identidad";
            // 
            // txtApellidos
            // 
            txtApellidos.Location = new Point(539, 76);
            txtApellidos.Margin = new Padding(3, 4, 3, 4);
            txtApellidos.Name = "txtApellidos";
            txtApellidos.Size = new Size(279, 27);
            txtApellidos.TabIndex = 7;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(539, 33);
            txtNombre.Margin = new Padding(3, 4, 3, 4);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(178, 27);
            txtNombre.TabIndex = 6;
            // 
            // label3
            // 
            label3.Location = new Point(447, 76);
            label3.Name = "label3";
            label3.Size = new Size(83, 31);
            label3.TabIndex = 5;
            label3.Text = "Apellidos:";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.Location = new Point(447, 33);
            label4.Name = "label4";
            label4.Size = new Size(83, 31);
            label4.TabIndex = 4;
            label4.Text = "Nombre:";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtNombreComercial
            // 
            txtNombreComercial.Location = new Point(120, 76);
            txtNombreComercial.Margin = new Padding(3, 4, 3, 4);
            txtNombreComercial.Name = "txtNombreComercial";
            txtNombreComercial.Size = new Size(310, 27);
            txtNombreComercial.TabIndex = 3;
            // 
            // txtNifCif
            // 
            txtNifCif.Location = new Point(120, 33);
            txtNifCif.Margin = new Padding(3, 4, 3, 4);
            txtNifCif.Name = "txtNifCif";
            txtNifCif.Size = new Size(138, 27);
            txtNifCif.TabIndex = 2;
            // 
            // label2
            // 
            label2.Location = new Point(11, 76);
            label2.Name = "label2";
            label2.Size = new Size(98, 31);
            label2.TabIndex = 1;
            label2.Text = "Razón Social:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.Location = new Point(11, 33);
            label1.Name = "label1";
            label1.Size = new Size(98, 31);
            label1.TabIndex = 0;
            label1.Text = "NIF/CIF:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // FrmProveedor
            // 
            AcceptButton = btnAceptar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelar;
            ClientSize = new Size(918, 585);
            Controls.Add(pnData);
            Controls.Add(pnBtns);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmProveedor";
            Text = "Datos del Proveedor";
            Load += FrmProveedor_Load;
            pnBtns.ResumeLayout(false);
            pnData.ResumeLayout(false);
            gbContacto.ResumeLayout(false);
            gbContacto.PerformLayout();
            gbDomicilio.ResumeLayout(false);
            gbDomicilio.PerformLayout();
            gbIdentificacion.ResumeLayout(false);
            gbIdentificacion.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnBtns;
        private Button btnCancelar;
        private Button btnAceptar;
        private Panel pnData;
        private GroupBox gbContacto;
        private TextBox txtEmail;
        private Label label13;
        private TextBox txtTel2;
        private Label label12;
        private TextBox txtTel1;
        private Label label11;
        private GroupBox gbDomicilio;
        private ComboBox cbProv;
        private Label label10;
        private TextBox txtPob;
        private Label label9;
        private TextBox txtCp;
        private Label label8;
        private TextBox txtDomicilio;
        private Label label7;
        private GroupBox gbIdentificacion;
        private TextBox txtApellidos;
        private TextBox txtNombre;
        private Label label3;
        private Label label4;
        private TextBox txtNombreComercial;
        private TextBox txtNifCif;
        private Label label2;
        private Label label1;
    }
}