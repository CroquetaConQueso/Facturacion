namespace FacturacionDAM.Formularios
{
    partial class FormSeleccionarEmisores
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
            label1 = new Label();
            label2 = new Label();
            btnSeleccionar = new Button();
            btnCancelar = new Button();
            cbEmisor = new ComboBox();
            gbSeleccion = new GroupBox();
            gbSeleccion.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.Location = new Point(33, 23);
            label1.Name = "label1";
            label1.Size = new Size(397, 20);
            label1.TabIndex = 0;
            label1.Text = "Selecciona el emisor del cual desea gestionar sus facturas: ";
            // 
            // label2
            // 
            label2.Location = new Point(60, 184);
            label2.Name = "label2";
            label2.Size = new Size(358, 40);
            label2.TabIndex = 1;
            label2.Text = "Puede cancelar ahora y acceder a la selección del emisor después en el menú \"Archivo\"";
            // 
            // btnSeleccionar
            // 
            btnSeleccionar.Location = new Point(410, 35);
            btnSeleccionar.Name = "btnSeleccionar";
            btnSeleccionar.Padding = new Padding(0, 0, 10, 0);
            btnSeleccionar.Size = new Size(124, 31);
            btnSeleccionar.TabIndex = 3;
            btnSeleccionar.Text = "Seleccionar";
            btnSeleccionar.TextAlign = ContentAlignment.MiddleRight;
            btnSeleccionar.UseVisualStyleBackColor = true;
            btnSeleccionar.Click += btnSeleccionar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(454, 184);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Padding = new Padding(0, 0, 10, 0);
            btnCancelar.Size = new Size(124, 40);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextAlign = ContentAlignment.MiddleRight;
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // cbEmisor
            // 
            cbEmisor.FormattingEnabled = true;
            cbEmisor.Location = new Point(16, 40);
            cbEmisor.Name = "cbEmisor";
            cbEmisor.Size = new Size(358, 23);
            cbEmisor.TabIndex = 5;
            // 
            // gbSeleccion
            // 
            gbSeleccion.Controls.Add(cbEmisor);
            gbSeleccion.Controls.Add(btnSeleccionar);
            gbSeleccion.Location = new Point(44, 57);
            gbSeleccion.Name = "gbSeleccion";
            gbSeleccion.Size = new Size(560, 100);
            gbSeleccion.TabIndex = 6;
            gbSeleccion.TabStop = false;
            gbSeleccion.Text = "Seleccione un emisor: ";
            // 
            // FormSeleccionarEmisores
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(646, 267);
            Controls.Add(gbSeleccion);
            Controls.Add(btnCancelar);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormSeleccionarEmisores";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Selección de Emisores";
            gbSeleccion.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button btnSeleccionar;
        private Button btnCancelar;
        private ComboBox cbEmisor;
        private GroupBox gbSeleccion;
    }
}