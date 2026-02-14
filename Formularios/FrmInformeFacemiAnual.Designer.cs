namespace FacturacionDAM.Formularios
{
    partial class FrmInformeFacemiAnual
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
            dTPAnoInicio = new DateTimePicker();
            btnInforme = new Button();
            dTPAnoFin = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // dTPAnoInicio
            // 
            dTPAnoInicio.Font = new Font("Segoe UI", 10F);
            dTPAnoInicio.Format = DateTimePickerFormat.Short;
            dTPAnoInicio.Location = new Point(40, 40);
            dTPAnoInicio.Name = "dTPAnoInicio";
            dTPAnoInicio.Size = new Size(96, 25);
            dTPAnoInicio.TabIndex = 0;
            // 
            // btnInforme
            // 
            btnInforme.Font = new Font("Segoe UI", 14F);
            btnInforme.Location = new Point(64, 72);
            btnInforme.Name = "btnInforme";
            btnInforme.Size = new Size(184, 48);
            btnInforme.TabIndex = 1;
            btnInforme.Text = "Generar Informe";
            btnInforme.UseVisualStyleBackColor = true;
            btnInforme.Click += btnInforme_Click;
            // 
            // dTPAnoFin
            // 
            dTPAnoFin.Font = new Font("Segoe UI", 10F);
            dTPAnoFin.Format = DateTimePickerFormat.Short;
            dTPAnoFin.Location = new Point(176, 40);
            dTPAnoFin.Name = "dTPAnoFin";
            dTPAnoFin.Size = new Size(96, 25);
            dTPAnoFin.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F);
            label1.Location = new Point(40, 16);
            label1.Name = "label1";
            label1.Size = new Size(84, 19);
            label1.TabIndex = 3;
            label1.Text = "Fecha inicial:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(176, 16);
            label2.Name = "label2";
            label2.Size = new Size(76, 19);
            label2.TabIndex = 4;
            label2.Text = "Fecha final:";
            // 
            // FrmInformeFacemiAnual
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(309, 136);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dTPAnoFin);
            Controls.Add(btnInforme);
            Controls.Add(dTPAnoInicio);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MaximumSize = new Size(325, 175);
            MinimizeBox = false;
            MinimumSize = new Size(325, 175);
            Name = "FrmInformeFacemiAnual";
            Text = "Facturas Emitidas entre Fechas";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnInforme;
        private Label label1;
        private Label label2;
        public DateTimePicker dTPAnoInicio;
        public DateTimePicker dTPAnoFin;
    }
}