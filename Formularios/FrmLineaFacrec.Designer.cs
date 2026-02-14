namespace FacturacionDAM.Formularios
{
    // CAMBIO IMPORTANTE: Aquí debe decir FrmLineaFacrec, NO FrmLineaFacemi
    partial class FrmLineaFacrec
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLineaFacrec));
            this.pnBtns = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.pnData = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbTotal = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbCuota = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbBase = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.gbCalculo = new System.Windows.Forms.GroupBox();
            this.numTipoIva = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.numPrecio = new System.Windows.Forms.NumericUpDown();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numCantidad = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.gbProducto = new System.Windows.Forms.GroupBox();
            this.cbProducto = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnProducto = new System.Windows.Forms.Button();
            this.pnBtns.SuspendLayout();
            this.pnData.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbCalculo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTipoIva)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).BeginInit();
            this.gbProducto.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnBtns
            // 
            this.pnBtns.BackColor = System.Drawing.Color.Gainsboro;
            this.pnBtns.Controls.Add(this.btnCancelar);
            this.pnBtns.Controls.Add(this.btnAceptar);
            this.pnBtns.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnBtns.Location = new System.Drawing.Point(0, 362);
            this.pnBtns.Name = "pnBtns";
            this.pnBtns.Size = new System.Drawing.Size(534, 49);
            this.pnBtns.TabIndex = 2;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(438, 12);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(84, 25);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.Location = new System.Drawing.Point(339, 12);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(84, 25);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            // 
            // pnData
            // 
            this.pnData.Controls.Add(this.panel1);
            this.pnData.Controls.Add(this.gbCalculo);
            this.pnData.Controls.Add(this.gbProducto);
            this.pnData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnData.Location = new System.Drawing.Point(0, 0);
            this.pnData.Name = "pnData";
            this.pnData.Size = new System.Drawing.Size(534, 362);
            this.pnData.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbTotal);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.lbCuota);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.lbBase);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Location = new System.Drawing.Point(12, 280);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(510, 65);
            this.panel1.TabIndex = 2;
            // 
            // lbTotal
            // 
            this.lbTotal.BackColor = System.Drawing.Color.White;
            this.lbTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbTotal.Location = new System.Drawing.Point(389, 30);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(100, 23);
            this.lbTotal.TabIndex = 19;
            this.lbTotal.Text = "0.00";
            this.lbTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label12.Location = new System.Drawing.Point(389, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 15);
            this.label12.TabIndex = 18;
            this.label12.Text = "TOTAL";
            // 
            // lbCuota
            // 
            this.lbCuota.BackColor = System.Drawing.Color.White;
            this.lbCuota.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCuota.Location = new System.Drawing.Point(267, 30);
            this.lbCuota.Name = "lbCuota";
            this.lbCuota.Size = new System.Drawing.Size(100, 23);
            this.lbCuota.TabIndex = 17;
            this.lbCuota.Text = "0.00";
            this.lbCuota.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(267, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 15);
            this.label11.TabIndex = 16;
            this.label11.Text = "Cuota";
            // 
            // lbBase
            // 
            this.lbBase.BackColor = System.Drawing.Color.White;
            this.lbBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbBase.Location = new System.Drawing.Point(145, 30);
            this.lbBase.Name = "lbBase";
            this.lbBase.Size = new System.Drawing.Size(100, 23);
            this.lbBase.TabIndex = 15;
            this.lbBase.Text = "0.00";
            this.lbBase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(145, 10);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(87, 15);
            this.label17.TabIndex = 14;
            this.label17.Text = "Base Imponible";
            // 
            // gbCalculo
            // 
            this.gbCalculo.Controls.Add(this.numTipoIva);
            this.gbCalculo.Controls.Add(this.label5);
            this.gbCalculo.Controls.Add(this.label10);
            this.gbCalculo.Controls.Add(this.numPrecio);
            this.gbCalculo.Controls.Add(this.txtDescripcion);
            this.gbCalculo.Controls.Add(this.label2);
            this.gbCalculo.Controls.Add(this.label3);
            this.gbCalculo.Controls.Add(this.numCantidad);
            this.gbCalculo.Controls.Add(this.label4);
            this.gbCalculo.Controls.Add(this.label6);
            this.gbCalculo.Location = new System.Drawing.Point(12, 97);
            this.gbCalculo.Name = "gbCalculo";
            this.gbCalculo.Size = new System.Drawing.Size(510, 177);
            this.gbCalculo.TabIndex = 1;
            this.gbCalculo.TabStop = false;
            this.gbCalculo.Text = "Detalle de Línea";
            // 
            // numTipoIva
            // 
            this.numTipoIva.DecimalPlaces = 2;
            this.numTipoIva.Location = new System.Drawing.Point(92, 128);
            this.numTipoIva.Name = "numTipoIva";
            this.numTipoIva.Size = new System.Drawing.Size(73, 23);
            this.numTipoIva.TabIndex = 13;
            this.numTipoIva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(171, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "%";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 130);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 15);
            this.label10.TabIndex = 11;
            this.label10.Text = "Tipo IVA:";
            // 
            // numPrecio
            // 
            this.numPrecio.DecimalPlaces = 2;
            this.numPrecio.Location = new System.Drawing.Point(92, 96);
            this.numPrecio.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numPrecio.Name = "numPrecio";
            this.numPrecio.Size = new System.Drawing.Size(100, 23);
            this.numPrecio.TabIndex = 8;
            this.numPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(92, 32);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(397, 23);
            this.txtDescripcion.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Descripción:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Cantidad:";
            // 
            // numCantidad
            // 
            this.numCantidad.DecimalPlaces = 2;
            this.numCantidad.Location = new System.Drawing.Point(92, 64);
            this.numCantidad.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numCantidad.Name = "numCantidad";
            this.numCantidad.Size = new System.Drawing.Size(100, 23);
            this.numCantidad.TabIndex = 7;
            this.numCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Precio:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(198, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "€";
            // 
            // gbProducto
            // 
            this.gbProducto.Controls.Add(this.cbProducto);
            this.gbProducto.Controls.Add(this.label7);
            this.gbProducto.Controls.Add(this.label1);
            this.gbProducto.Controls.Add(this.BtnProducto);
            this.gbProducto.Location = new System.Drawing.Point(12, 12);
            this.gbProducto.Name = "gbProducto";
            this.gbProducto.Size = new System.Drawing.Size(510, 79);
            this.gbProducto.TabIndex = 0;
            this.gbProducto.TabStop = false;
            this.gbProducto.Text = "Selección Rápida (Opcional)";
            // 
            // cbProducto
            // 
            this.cbProducto.FormattingEnabled = true;
            this.cbProducto.Location = new System.Drawing.Point(82, 33);
            this.cbProducto.Name = "cbProducto";
            this.cbProducto.Size = new System.Drawing.Size(326, 23);
            this.cbProducto.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label7.Location = new System.Drawing.Point(452, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "(Ctrl+F)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Producto:";
            // 
            // BtnProducto
            // 
            this.BtnProducto.Image = ((System.Drawing.Image)(resources.GetObject("BtnProducto.Image")));
            this.BtnProducto.Location = new System.Drawing.Point(414, 32);
            this.BtnProducto.Name = "BtnProducto";
            this.BtnProducto.Size = new System.Drawing.Size(32, 24);
            this.BtnProducto.TabIndex = 0;
            this.BtnProducto.UseVisualStyleBackColor = true;
            // 
            // FrmLineaFacrec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 411);
            this.Controls.Add(this.pnData);
            this.Controls.Add(this.pnBtns);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLineaFacrec";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Detalle de Línea (Recibida)";
            this.pnBtns.ResumeLayout(false);
            this.pnData.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbCalculo.ResumeLayout(false);
            this.gbCalculo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTipoIva)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).EndInit();
            this.gbProducto.ResumeLayout(false);
            this.gbProducto.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnBtns;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Panel pnData;
        private System.Windows.Forms.GroupBox gbProducto;
        private System.Windows.Forms.ComboBox cbProducto;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnProducto;
        private System.Windows.Forms.GroupBox gbCalculo;
        private System.Windows.Forms.NumericUpDown numTipoIva;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numPrecio;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numCantidad;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbTotal;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbCuota;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbBase;
        private System.Windows.Forms.Label label17;
    }
}