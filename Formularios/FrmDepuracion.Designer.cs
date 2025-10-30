#if DEBUG
namespace FacturacionDAM.Formularios
{
    partial class FrmDepuracion
    {
        /// <summary>
        ///  Necesario para el diseñador de Windows Forms
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controles del formulario
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.CheckBox cbRefrescarAutomaticamente;
        private System.Windows.Forms.TextBox txtLog;

        /// <summary>
        ///  Limpieza de recursos
        /// </summary>
        /// <param name="disposing">true si los recursos administrados deben eliminarse</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            pnlMenu = new Panel();
            btnRefrescar = new Button();
            cbRefrescarAutomaticamente = new CheckBox();
            txtLog = new TextBox();
            pnlMenu.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMenu
            // 
            pnlMenu.Controls.Add(btnRefrescar);
            pnlMenu.Controls.Add(cbRefrescarAutomaticamente);
            pnlMenu.Dock = DockStyle.Bottom;
            pnlMenu.Location = new Point(0, 419);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(800, 41);
            pnlMenu.TabIndex = 0;
            // 
            // btnRefrescar
            // 
            btnRefrescar.Location = new Point(12, 9);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new Size(90, 23);
            btnRefrescar.TabIndex = 1;
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.UseVisualStyleBackColor = true;
            // 
            // cbRefrescarAutomaticamente
            // 
            cbRefrescarAutomaticamente.AutoSize = true;
            cbRefrescarAutomaticamente.Location = new Point(120, 12);
            cbRefrescarAutomaticamente.Name = "cbRefrescarAutomaticamente";
            cbRefrescarAutomaticamente.Size = new Size(171, 19);
            cbRefrescarAutomaticamente.TabIndex = 2;
            cbRefrescarAutomaticamente.Text = "Refrescar automáticamente";
            cbRefrescarAutomaticamente.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            txtLog.Dock = DockStyle.Fill;
            txtLog.Font = new Font("Consolas", 9F);
            txtLog.Location = new Point(0, 0);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Both;
            txtLog.Size = new Size(800, 419);
            txtLog.TabIndex = 3;
            txtLog.WordWrap = false;
            // 
            // FrmDepuracion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 460);
            Controls.Add(txtLog);
            Controls.Add(pnlMenu);
            MaximizeBox = false;
            Name = "FrmDepuracion";
            Text = "Depuración (solo DEBUG)";
            pnlMenu.ResumeLayout(false);
            pnlMenu.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
#endif
