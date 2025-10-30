#if DEBUG
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace FacturacionDAM.Formularios
{
    public partial class FrmDepuracion : Form
    {
        private string _rutaLog = string.Empty;
        private System.Windows.Forms.Timer _timer;

        public FrmDepuracion()
        {
            InitializeComponent();

            // Eventos de la UI, acordarse que no es como FX
            this.Load += FrmDepuracion_Load;
            this.btnRefrescar.Click += BtnRefrescar_Click;
            this.cbRefrescarAutomaticamente.CheckedChanged += CbRefrescarAutomaticamente_CheckedChanged;

            // Timer para el auto-refresco (1 s)
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 1000;
            _timer.Tick += (s, e) => RefrescarLog(true);

            //Colores de la consola y espacio entre letras 
            txtLog.BackColor = Color.Black;
            txtLog.ForeColor = Color.White;
            if (txtLog.Font == null || txtLog.Font.FontFamily.Name != "Consolas")
                txtLog.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);

            //Loop de los botones
            btnRefrescar.Enabled = !cbRefrescarAutomaticamente.Checked;
            _timer.Enabled = cbRefrescarAutomaticamente.Checked;
        }

        private void FrmDepuracion_Load(object sender, EventArgs e)
        {
            // Comprobacion 
            var baseDir = Program.appDAM?.rutaBase ?? AppDomain.CurrentDomain.BaseDirectory;
            _rutaLog = Path.Combine(baseDir, "app.log");

            RefrescarLog(true);
        }

        private void BtnRefrescar_Click(object sender, EventArgs e)
        {
            RefrescarLog(false);
        }

        private void CbRefrescarAutomaticamente_CheckedChanged(object sender, EventArgs e)
        {
            // Si y no del boton
            bool auto = cbRefrescarAutomaticamente.Checked;
            _timer.Enabled = auto;
            btnRefrescar.Enabled = !auto;

            // Cuando activo auto, hago un refresco inicial silencioso
            if (auto) RefrescarLog(true);
        }

        /// <summary>
        /// Lee el archivo de log y lo muestra en el TextBox. Si 'silent' es true, no muestro errores.
        /// </summary>
        private void RefrescarLog(bool silent)
        {
            if (string.IsNullOrWhiteSpace(_rutaLog) || !File.Exists(_rutaLog))
            {
                if (!silent)
                    txtLog.Text = "No se encontró el archivo de log.";
                return;
            }

            try
            {
                using (var fs = new FileStream(_rutaLog, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var sr = new StreamReader(fs, Encoding.UTF8, detectEncodingFromByteOrderMarks: true))
                {
                    txtLog.Text = sr.ReadToEnd();
                }

                // Me posiciono al final para ver lo último
                txtLog.SelectionStart = txtLog.TextLength;
                txtLog.ScrollToCaret();
            }
            catch (Exception ex)
            {
                if (!silent)
                    MessageBox.Show("Error al leer el log:\n" + ex.Message, "Depuración",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
#endif
