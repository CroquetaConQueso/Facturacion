using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacturacionDAM.Formularios
{
    public partial class FrmDepuracion : Form
    {
        public string rutaLog;
        public FrmDepuracion()
        {
            InitializeComponent();
            rutaLog = Program.appDAM.rutaLog;
        }
        private void FrmDepuracion_Load(object sender, EventArgs e)
        {
            txtLog.ForeColor = Color.Gainsboro;

            CargarLog();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarLog();
        }

        private void chkAutoRefrescar_CheckedChanged(object sender, EventArgs e)
        {
            // Activa o desactiva el temporizador según el checkbox
            timerRefresh.Enabled = chbAutoRefresh.Checked;
        }

        private void tmrActualizacion_Tick(object sender, EventArgs e)
        {
            CargarLog();
        }

        private void CargarLog()
        {
            try
            {
                if (File.Exists(rutaLog))
                {
                    // Leer todo el log
                    txtLog.Text = File.ReadAllText(rutaLog);

                    // Mover el scroll al final
                    txtLog.SelectionStart = txtLog.Text.Length;
                    txtLog.ScrollToCaret();
                }
                else
                {
                    txtLog.Text = "No se ha encontrado el archivo de log.";
                }
            }
            catch (Exception ex)
            {
                txtLog.Text = $"Error al cargar el log: {ex.Message}";
            }
        }
    }
}
