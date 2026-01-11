namespace FacturacionDAM.Formularios
{
    partial class FrmLineaFacemi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLineaFacemi));
            pnBtns = new Panel();
            btnCancelar = new Button();
            btnAceptar = new Button();
            pnData = new Panel();
            panel1 = new Panel();
            lbTotal = new Label();
            label12 = new Label();
            lbCuota = new Label();
            label11 = new Label();
            lbBase = new Label();
            label17 = new Label();
            gbCalculo = new GroupBox();
            label6 = new Label();
            numCantidad = new NumericUpDown();
            label4 = new Label();
            numTipoIva = new NumericUpDown();
            label5 = new Label();
            label10 = new Label();
            numPrecio = new NumericUpDown();
            txtDescripcion = new TextBox();
            label2 = new Label();
            label3 = new Label();
            gbProducto = new GroupBox();
            BtnProducto = new Button();
            label1 = new Label();
            cbProducto = new ComboBox();
            label7 = new Label();
            pnBtns.SuspendLayout();
            pnData.SuspendLayout();
            panel1.SuspendLayout();
            gbCalculo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCantidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTipoIva).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPrecio).BeginInit();
            gbProducto.SuspendLayout();
            SuspendLayout();
            // 
            // pnBtns
            // 
            pnBtns.Controls.Add(btnCancelar);
            pnBtns.Controls.Add(btnAceptar);
            pnBtns.Dock = DockStyle.Bottom;
            pnBtns.Location = new Point(0, 299);
            pnBtns.Name = "pnBtns";
            pnBtns.Size = new Size(690, 63);
            pnBtns.TabIndex = 1;
            // 
            // btnCancelar
            // 
            btnCancelar.Image = (Image)resources.GetObject("btnCancelar.Image");
            btnCancelar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelar.Location = new Point(401, 14);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Padding = new Padding(10, 0, 10, 0);
            btnCancelar.Size = new Size(100, 36);
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
            btnAceptar.Location = new Point(198, 14);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Padding = new Padding(10, 0, 10, 0);
            btnAceptar.Size = new Size(100, 36);
            btnAceptar.TabIndex = 0;
            btnAceptar.Text = "Aceptar";
            btnAceptar.TextAlign = ContentAlignment.MiddleRight;
            btnAceptar.UseVisualStyleBackColor = true;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // pnData
            // 
            pnData.Controls.Add(panel1);
            pnData.Controls.Add(gbCalculo);
            pnData.Controls.Add(gbProducto);
            pnData.Dock = DockStyle.Fill;
            pnData.Location = new Point(0, 0);
            pnData.Name = "pnData";
            pnData.Size = new Size(690, 299);
            pnData.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.BackColor = Color.AntiqueWhite;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(lbTotal);
            panel1.Controls.Add(label12);
            panel1.Controls.Add(lbCuota);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(lbBase);
            panel1.Controls.Add(label17);
            panel1.Location = new Point(20, 233);
            panel1.Margin = new Padding(3, 2, 3, 8);
            panel1.Name = "panel1";
            panel1.Size = new Size(650, 48);
            panel1.TabIndex = 36;
            // 
            // lbTotal
            // 
            lbTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbTotal.ForeColor = Color.Black;
            lbTotal.Location = new Point(451, 12);
            lbTotal.Name = "lbTotal";
            lbTotal.Size = new Size(87, 23);
            lbTotal.TabIndex = 18;
            lbTotal.Text = "10,00 €";
            lbTotal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            label12.Location = new Point(393, 12);
            label12.Name = "label12";
            label12.Size = new Size(56, 23);
            label12.TabIndex = 17;
            label12.Text = "Total:";
            label12.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lbCuota
            // 
            lbCuota.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbCuota.ForeColor = Color.Black;
            lbCuota.Location = new Point(308, 12);
            lbCuota.Name = "lbCuota";
            lbCuota.Size = new Size(87, 23);
            lbCuota.TabIndex = 16;
            lbCuota.Text = "10,00 €";
            lbCuota.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            label11.Location = new Point(252, 12);
            label11.Name = "label11";
            label11.Size = new Size(56, 23);
            label11.TabIndex = 15;
            label11.Text = "Cuota:";
            label11.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lbBase
            // 
            lbBase.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbBase.ForeColor = Color.Black;
            lbBase.Location = new Point(157, 12);
            lbBase.Name = "lbBase";
            lbBase.Size = new Size(87, 23);
            lbBase.TabIndex = 14;
            lbBase.Text = "10,00 €";
            lbBase.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            label17.Location = new Point(99, 12);
            label17.Name = "label17";
            label17.Size = new Size(56, 23);
            label17.TabIndex = 13;
            label17.Text = "Base:";
            label17.TextAlign = ContentAlignment.MiddleRight;
            // 
            // gbCalculo
            // 
            gbCalculo.Controls.Add(label6);
            gbCalculo.Controls.Add(numCantidad);
            gbCalculo.Controls.Add(label4);
            gbCalculo.Controls.Add(numTipoIva);
            gbCalculo.Controls.Add(label5);
            gbCalculo.Controls.Add(label10);
            gbCalculo.Controls.Add(numPrecio);
            gbCalculo.Controls.Add(txtDescripcion);
            gbCalculo.Controls.Add(label2);
            gbCalculo.Controls.Add(label3);
            gbCalculo.Location = new Point(20, 120);
            gbCalculo.Margin = new Padding(3, 2, 3, 2);
            gbCalculo.Name = "gbCalculo";
            gbCalculo.Padding = new Padding(3, 2, 3, 2);
            gbCalculo.Size = new Size(649, 100);
            gbCalculo.TabIndex = 18;
            gbCalculo.TabStop = false;
            gbCalculo.Text = "Cálculo de la Linea de Factura";
            // 
            // label6
            // 
            label6.Location = new Point(205, 64);
            label6.Name = "label6";
            label6.Size = new Size(65, 23);
            label6.TabIndex = 34;
            label6.Text = "(sin IVA)";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numCantidad
            // 
            numCantidad.DecimalPlaces = 2;
            numCantidad.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numCantidad.Location = new Point(524, 65);
            numCantidad.Margin = new Padding(3, 2, 3, 2);
            numCantidad.Maximum = new decimal(new int[] { 9999, 0, 0, 131072 });
            numCantidad.Name = "numCantidad";
            numCantidad.Size = new Size(94, 23);
            numCantidad.TabIndex = 33;
            numCantidad.TextAlign = HorizontalAlignment.Right;
            // 
            // label4
            // 
            label4.Location = new Point(449, 64);
            label4.Name = "label4";
            label4.Size = new Size(76, 23);
            label4.TabIndex = 32;
            label4.Text = "Unidades:";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numTipoIva
            // 
            numTipoIva.DecimalPlaces = 2;
            numTipoIva.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numTipoIva.Location = new Point(348, 65);
            numTipoIva.Margin = new Padding(3, 2, 3, 2);
            numTipoIva.Maximum = new decimal(new int[] { 9999, 0, 0, 131072 });
            numTipoIva.Name = "numTipoIva";
            numTipoIva.Size = new Size(64, 23);
            numTipoIva.TabIndex = 31;
            numTipoIva.TextAlign = HorizontalAlignment.Right;
            // 
            // label5
            // 
            label5.Location = new Point(414, 64);
            label5.Name = "label5";
            label5.Size = new Size(25, 23);
            label5.TabIndex = 30;
            label5.Text = "%";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            label10.Location = new Point(263, 64);
            label10.Name = "label10";
            label10.Size = new Size(86, 23);
            label10.TabIndex = 28;
            label10.Text = "Tipo de IVA:";
            label10.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numPrecio
            // 
            numPrecio.DecimalPlaces = 2;
            numPrecio.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numPrecio.Location = new Point(108, 65);
            numPrecio.Margin = new Padding(3, 2, 3, 2);
            numPrecio.Maximum = new decimal(new int[] { 9999, 0, 0, 131072 });
            numPrecio.Name = "numPrecio";
            numPrecio.Size = new Size(95, 23);
            numPrecio.TabIndex = 27;
            numPrecio.TextAlign = HorizontalAlignment.Right;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(108, 33);
            txtDescripcion.MaxLength = 255;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(511, 23);
            txtDescripcion.TabIndex = 26;
            // 
            // label2
            // 
            label2.Location = new Point(5, 64);
            label2.Name = "label2";
            label2.Size = new Size(101, 23);
            label2.TabIndex = 24;
            label2.Text = "Precio/Unidad:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Location = new Point(20, 33);
            label3.Name = "label3";
            label3.Size = new Size(86, 23);
            label3.TabIndex = 25;
            label3.Text = "Descripción:";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // gbProducto
            // 
            gbProducto.Controls.Add(BtnProducto);
            gbProducto.Controls.Add(label1);
            gbProducto.Controls.Add(cbProducto);
            gbProducto.Controls.Add(label7);
            gbProducto.Location = new Point(20, 14);
            gbProducto.Name = "gbProducto";
            gbProducto.Size = new Size(649, 95);
            gbProducto.TabIndex = 5;
            gbProducto.TabStop = false;
            gbProducto.Text = "Producto (opcional)";
            // 
            // BtnProducto
            // 
            BtnProducto.Image = (Image)resources.GetObject("BtnProducto.Image");
            BtnProducto.ImageAlign = ContentAlignment.MiddleLeft;
            BtnProducto.Location = new Point(463, 52);
            BtnProducto.Margin = new Padding(3, 2, 3, 2);
            BtnProducto.Name = "BtnProducto";
            BtnProducto.Padding = new Padding(14, 0, 10, 0);
            BtnProducto.Size = new Size(119, 32);
            BtnProducto.TabIndex = 9;
            BtnProducto.Text = "Trasladar";
            BtnProducto.TextAlign = ContentAlignment.MiddleRight;
            BtnProducto.UseVisualStyleBackColor = true;
            BtnProducto.Click += BtnProducto_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 29);
            label1.Name = "label1";
            label1.Size = new Size(514, 15);
            label1.TabIndex = 8;
            label1.Text = "Selecciona un producto para trasladar su precio y tipo de IVA a los cálculos de la línea de factura.";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cbProducto
            // 
            cbProducto.FormattingEnabled = true;
            cbProducto.Location = new Point(116, 58);
            cbProducto.Name = "cbProducto";
            cbProducto.Size = new Size(334, 23);
            cbProducto.TabIndex = 7;
            // 
            // label7
            // 
            label7.Location = new Point(42, 56);
            label7.Name = "label7";
            label7.Size = new Size(68, 23);
            label7.TabIndex = 0;
            label7.Text = "Producto:";
            label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FrmLineaFacemi
            // 
            AcceptButton = btnAceptar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelar;
            ClientSize = new Size(690, 362);
            Controls.Add(pnData);
            Controls.Add(pnBtns);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmLineaFacemi";
            Text = "Linea de Factura Emitida";
            Load += FrmLineaFacemi_Load;
            pnBtns.ResumeLayout(false);
            pnData.ResumeLayout(false);
            panel1.ResumeLayout(false);
            gbCalculo.ResumeLayout(false);
            gbCalculo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numCantidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTipoIva).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPrecio).EndInit();
            gbProducto.ResumeLayout(false);
            gbProducto.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnBtns;
        private Button btnCancelar;
        private Button btnAceptar;
        private Panel pnData;
        private GroupBox gbProducto;
        private ComboBox cbProducto;
        private Label label7;
        private Label label1;
        private Button BtnProducto;
        private GroupBox gbCalculo;
        private NumericUpDown numTipoIva;
        private Label label5;
        private Label label10;
        private NumericUpDown numPrecio;
        private TextBox txtDescripcion;
        private Label label2;
        private Label label3;
        private NumericUpDown numCantidad;
        private Label label4;
        private Label label6;
        private Panel panel1;
        private Label lbTotal;
        private Label label12;
        private Label lbCuota;
        private Label label11;
        private Label lbBase;
        private Label label17;
    }
}