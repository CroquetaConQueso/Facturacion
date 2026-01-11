using FacturacionDAM.Modelos;

namespace FacturacionDAM.Formularios
{
    partial class FrmSeleccionarEmisor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSeleccionarEmisor));
            gbSeleccion = new GroupBox();
            btnSeleccionar = new Button();
            cbEmisores = new ComboBox();
            label1 = new Label();
            btnCancelar = new Button();
            label2 = new Label();
            gbSeleccion.SuspendLayout();
            SuspendLayout();
            // 
            // gbSeleccion
            // 
            gbSeleccion.Controls.Add(btnSeleccionar);
            gbSeleccion.Controls.Add(cbEmisores);
            gbSeleccion.Location = new Point(42, 74);
            gbSeleccion.Name = "gbSeleccion";
            gbSeleccion.Size = new Size(512, 84);
            gbSeleccion.TabIndex = 1;
            gbSeleccion.TabStop = false;
            gbSeleccion.Text = "Seleccione un emisor:";
            // 
            // btnSeleccionar
            // 
            btnSeleccionar.Image = (Image)resources.GetObject("btnSeleccionar.Image");
            btnSeleccionar.ImageAlign = ContentAlignment.MiddleLeft;
            btnSeleccionar.Location = new Point(371, 26);
            btnSeleccionar.Margin = new Padding(20, 3, 3, 3);
            btnSeleccionar.Name = "btnSeleccionar";
            btnSeleccionar.Padding = new Padding(10, 0, 6, 0);
            btnSeleccionar.Size = new Size(118, 33);
            btnSeleccionar.TabIndex = 2;
            btnSeleccionar.Text = "Seleccionar";
            btnSeleccionar.TextAlign = ContentAlignment.MiddleRight;
            btnSeleccionar.UseVisualStyleBackColor = true;
            btnSeleccionar.Click += btnSeleccionar_Click;
            // 
            // cbEmisores
            // 
            cbEmisores.FormattingEnabled = true;
            cbEmisores.Location = new Point(41, 32);
            cbEmisores.Name = "cbEmisores";
            cbEmisores.Size = new Size(307, 23);
            cbEmisores.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.Location = new Point(42, 28);
            label1.Name = "label1";
            label1.Size = new Size(393, 20);
            label1.TabIndex = 4;
            label1.Text = "Selecciona el emisor del cual desea gestionar sus facturas:";
            // 
            // btnCancelar
            // 
            btnCancelar.Image = (Image)resources.GetObject("btnCancelar.Image");
            btnCancelar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelar.Location = new Point(413, 176);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Padding = new Padding(12, 0, 6, 0);
            btnCancelar.Size = new Size(108, 38);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextAlign = ContentAlignment.MiddleRight;
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // label2
            // 
            label2.AutoEllipsis = true;
            label2.Location = new Point(83, 167);
            label2.Name = "label2";
            label2.Size = new Size(307, 56);
            label2.TabIndex = 6;
            label2.Text = "Puede cancelar ahora y acceder a la selección del emisor después en el menú \"Archivo\".";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FrmSeleccionarEmisor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(595, 241);
            Controls.Add(label2);
            Controls.Add(btnCancelar);
            Controls.Add(label1);
            Controls.Add(gbSeleccion);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmSeleccionarEmisor";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Selección de Emisores";
            FormClosing += FrmSeleccionarEmisor_FormClosing;
            Load += FrmSeleccionarEmisor_Load;
            gbSeleccion.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox gbSeleccion;
        private Button btnSeleccionar;
        private ComboBox cbEmisores;
        private Label label1;
        private Button btnCancelar;
        private Label label2;
    }
}