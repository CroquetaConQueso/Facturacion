namespace FacturacionDAM.Formularios
{
    partial class FrmTipoIva
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTipoIva));
            pnBtns = new Panel();
            btnCancelar = new Button();
            btnAceptar = new Button();
            pnData = new Panel();
            numUpDownPorcentaje = new NumericUpDown();
            chkActivo = new CheckBox();
            label3 = new Label();
            txtDescripcion = new TextBox();
            label1 = new Label();
            label2 = new Label();
            pnBtns.SuspendLayout();
            pnData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numUpDownPorcentaje).BeginInit();
            SuspendLayout();
            // 
            // pnBtns
            // 
            pnBtns.Controls.Add(btnCancelar);
            pnBtns.Controls.Add(btnAceptar);
            pnBtns.Dock = DockStyle.Bottom;
            pnBtns.Location = new Point(0, 212);
            pnBtns.Margin = new Padding(3, 4, 3, 4);
            pnBtns.Name = "pnBtns";
            pnBtns.Size = new Size(651, 84);
            pnBtns.TabIndex = 4;
            // 
            // btnCancelar
            // 
            btnCancelar.Image = (Image)resources.GetObject("btnCancelar.Image");
            btnCancelar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelar.Location = new Point(328, 19);
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
            btnAceptar.Location = new Point(152, 19);
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
            pnData.Controls.Add(numUpDownPorcentaje);
            pnData.Controls.Add(chkActivo);
            pnData.Controls.Add(label3);
            pnData.Controls.Add(txtDescripcion);
            pnData.Controls.Add(label1);
            pnData.Controls.Add(label2);
            pnData.Dock = DockStyle.Fill;
            pnData.Location = new Point(0, 0);
            pnData.Margin = new Padding(3, 4, 3, 4);
            pnData.Name = "pnData";
            pnData.Size = new Size(651, 212);
            pnData.TabIndex = 0;
            // 
            // numUpDownPorcentaje
            // 
            numUpDownPorcentaje.DecimalPlaces = 2;
            numUpDownPorcentaje.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numUpDownPorcentaje.Location = new Point(139, 75);
            numUpDownPorcentaje.Maximum = new decimal(new int[] { 9999, 0, 0, 131072 });
            numUpDownPorcentaje.Name = "numUpDownPorcentaje";
            numUpDownPorcentaje.Size = new Size(69, 27);
            numUpDownPorcentaje.TabIndex = 1;
            numUpDownPorcentaje.TextAlign = HorizontalAlignment.Center;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Location = new Point(139, 161);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(463, 24);
            chkActivo.TabIndex = 3;
            chkActivo.Text = "¿Activo? Marca el checkbox si el tipo está activo en la actualidad.";
            chkActivo.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(29, 24);
            label3.Name = "label3";
            label3.Size = new Size(220, 28);
            label3.TabIndex = 9;
            label3.Text = "Gestión del Tipo de IVA:";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(139, 117);
            txtDescripcion.Margin = new Padding(3, 4, 3, 4);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(463, 27);
            txtDescripcion.TabIndex = 2;
            // 
            // label1
            // 
            label1.Location = new Point(31, 75);
            label1.Name = "label1";
            label1.Size = new Size(98, 31);
            label1.TabIndex = 4;
            label1.Text = "Tipo:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Location = new Point(31, 117);
            label2.Name = "label2";
            label2.Size = new Size(98, 31);
            label2.TabIndex = 5;
            label2.Text = "Descripción:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // FrmTipoIva
            // 
            AcceptButton = btnAceptar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelar;
            ClientSize = new Size(651, 296);
            Controls.Add(pnData);
            Controls.Add(pnBtns);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmTipoIva";
            Text = "Datos del Tipo de IVA";
            Load += FrmTipoIva_Load;
            pnBtns.ResumeLayout(false);
            pnData.ResumeLayout(false);
            pnData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numUpDownPorcentaje).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnBtns;
        private Button btnCancelar;
        private Button btnAceptar;
        private Panel pnData;
        private TextBox txtDescripcion;
        private Label label1;
        private Label label2;
        private Label label3;
        private CheckBox chkActivo;
        private NumericUpDown numUpDownPorcentaje;
    }
}