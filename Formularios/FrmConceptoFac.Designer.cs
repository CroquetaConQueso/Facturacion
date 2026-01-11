namespace FacturacionDAM.Formularios
{
    partial class FrmConceptoFac
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConceptoFac));
            pnBtns = new Panel();
            btnCancelar = new Button();
            btnAceptar = new Button();
            bsEmisor = new BindingSource(components);
            txtDescripcion = new TextBox();
            txtCodigo = new TextBox();
            label2 = new Label();
            label1 = new Label();
            label3 = new Label();
            pnBtns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bsEmisor).BeginInit();
            SuspendLayout();
            // 
            // pnBtns
            // 
            pnBtns.Controls.Add(btnCancelar);
            pnBtns.Controls.Add(btnAceptar);
            pnBtns.Dock = DockStyle.Bottom;
            pnBtns.Location = new Point(0, 157);
            pnBtns.Name = "pnBtns";
            pnBtns.Size = new Size(718, 63);
            pnBtns.TabIndex = 2;
            // 
            // btnCancelar
            // 
            btnCancelar.Image = (Image)resources.GetObject("btnCancelar.Image");
            btnCancelar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelar.Location = new Point(417, 15);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Padding = new Padding(10, 0, 10, 0);
            btnCancelar.Size = new Size(97, 36);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextAlign = ContentAlignment.MiddleRight;
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnAceptar
            // 
            btnAceptar.Image = (Image)resources.GetObject("btnAceptar.Image");
            btnAceptar.ImageAlign = ContentAlignment.MiddleLeft;
            btnAceptar.Location = new Point(214, 15);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Padding = new Padding(10, 0, 10, 0);
            btnAceptar.Size = new Size(97, 36);
            btnAceptar.TabIndex = 3;
            btnAceptar.Text = "Aceptar";
            btnAceptar.TextAlign = ContentAlignment.MiddleRight;
            btnAceptar.UseVisualStyleBackColor = true;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(119, 96);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.PlaceholderText = "Descripción del concepto de facturación";
            txtDescripcion.Size = new Size(562, 23);
            txtDescripcion.TabIndex = 1;
            // 
            // txtCodigo
            // 
            txtCodigo.Location = new Point(119, 64);
            txtCodigo.MaxLength = 20;
            txtCodigo.Name = "txtCodigo";
            txtCodigo.PlaceholderText = "Código contable. Por ej. 700";
            txtCodigo.Size = new Size(169, 23);
            txtCodigo.TabIndex = 0;
            // 
            // label2
            // 
            label2.Location = new Point(33, 96);
            label2.Name = "label2";
            label2.Size = new Size(77, 23);
            label2.TabIndex = 5;
            label2.Text = "Descripción:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.Location = new Point(33, 64);
            label1.Name = "label1";
            label1.Size = new Size(77, 23);
            label1.TabIndex = 4;
            label1.Text = "Código:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(29, 23);
            label3.Name = "label3";
            label3.Size = new Size(260, 21);
            label3.TabIndex = 8;
            label3.Text = "Gestión del concepto de facturación:";
            // 
            // FrmConceptoFac
            // 
            AcceptButton = btnAceptar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelar;
            ClientSize = new Size(718, 220);
            Controls.Add(label3);
            Controls.Add(txtDescripcion);
            Controls.Add(txtCodigo);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pnBtns);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmConceptoFac";
            Text = "Datos del Concepto de Facturación";
            Load += FrmConceptoFac_Load;
            pnBtns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bsEmisor).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnBtns;
        private Button btnCancelar;
        private Button btnAceptar;
        private BindingSource bsEmisor;
        private TextBox txtDescripcion;
        private TextBox txtCodigo;
        private Label label2;
        private Label label1;
        private Label label3;
    }
}