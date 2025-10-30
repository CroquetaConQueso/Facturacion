using System;
using System.IO;
using System.Windows.Forms;
using FacturacionDAM.Modelos;

namespace FacturacionDAM.Formularios
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            this.Load += FrmMain_Load;
            this.Activated += (s, e) => ActualizarVisibilidadDepuracion();
            this.FormClosing += FrmMain_FormClosing;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (!(Program.appDAM.estadoApp == EstadoApp.Conectado || Program.appDAM.conectado))
            {
                Close();
                return;
            }

            if (Program.appDAM.emisor == null)
                SeleccionarEmisor();

            RefreshToolBar();
            RefreshStatusBar();
            ActualizarVisibilidadDepuracion();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.appDAM.conectado)
            {
                var r = MessageBox.Show(
                    "Hay una conexión abierta. ¿Deseas cerrarla y salir?",
                    "Conexión abierta",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (r == DialogResult.Yes) Program.appDAM.DesconectarDB();
                else e.Cancel = true;
            }
        }

        private void tsBtnConfig_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is FrmConfig)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;
                    frm.Activate();
                    return;
                }
            }
            new FrmConfig { MdiParent = this }.Show();
        }

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren) frm.Close();
            Close();
        }

        private void depuracionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!DebeMostrarseDepuracion())
            {
                ActualizarVisibilidadDepuracion();
                return;
            }

            foreach (Form frm in this.MdiChildren)
            {
                if (frm is FrmDepuracion)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;
                    frm.Activate();
                    return;
                }
            }
            new FrmDepuracion { MdiParent = this }.Show();
        }

        public void RefreshToolBar()
        {
            if (tsToolMain == null) return;

            foreach (ToolStripItem it in tsToolMain.Items)
                if (it is ToolStripButton b) b.Enabled = false;

            Habilitar("tsBtnConfig");
            Habilitar("tsBtnSalir");

            switch (Program.appDAM.estadoApp)
            {
                case EstadoApp.Conectado:
                    Habilitar("tsBtnEmisores");
                    Habilitar("tsBtnVentas");
                    Habilitar("tsBtnCompras");
                    Habilitar("tsBtnClientes");
                    Habilitar("tsBtnProveedores");
                    break;

                case EstadoApp.ConectadoSinEmisor:
                    Habilitar("tsBtnEmisores");
                    break;
            }

            ActualizarVisibilidadDepuracion();

            void Habilitar(string name)
            {
                if (tsToolMain.Items[name] is ToolStripButton b) b.Enabled = true;
            }
        }

        private void RefreshStatusBar()
        {
            var ss = statusBar;
            var lbEmisor = ss.Items["tsLbEmisor"] as ToolStripStatusLabel;
            var lbEstado = ss.Items["tsLbEstado"] as ToolStripStatusLabel;

            if (lbEmisor != null)
            {
                lbEmisor.Text = (Program.appDAM.emisor == null)
                    ? "Emisor: Sin emisor seleccionado"
                    : $"Emisor: {Program.appDAM.emisor.nombrecomercial} · NIF {Program.appDAM.emisor.nifcif}";
            }

            if (lbEstado != null)
            {
                lbEstado.Text = Program.appDAM.estadoApp switch
                {
                    EstadoApp.Iniciando => "Estado: Iniciando…",
                    EstadoApp.SinConexion => "Estado: No conectado a la base de datos",
                    EstadoApp.ConectadoSinEmisor => "Estado: Conectado · Sin emisor",
                    EstadoApp.Conectado => "Estado: Conectado a la base de datos",
                    EstadoApp.Error => "Estado: Error · revisa el log",
                    _ => "Estado: Indeterminado"
                };
            }
        }

        private void SeleccionarEmisor()
        {
            using (var frm = new FormSeleccionarEmisores())
            {
                var r = frm.ShowDialog(this);
                if (r == DialogResult.OK && frm.EmisorSeleccionado != null)
                {
                    Program.appDAM.emisor = frm.EmisorSeleccionado;
                    Program.appDAM.estadoApp = EstadoApp.Conectado;
                }
                else
                {
                    Close();
                }
            }
        }

        public void RefrescarMenuDepuracionPorLog()
        {
            ActualizarVisibilidadDepuracion();
        }

        private bool DebeMostrarseDepuracion()
        {
            bool sinConexion =
                !Program.appDAM.conectado ||
                Program.appDAM.estadoApp == EstadoApp.SinConexion ||
                Program.appDAM.estadoApp == EstadoApp.Error;

            return sinConexion || HayErroresEnLog();
        }

        private void ActualizarVisibilidadDepuracion()
        {
            bool show = DebeMostrarseDepuracion();

            depuracionToolStripMenuItem.Visible = show;
            depuracionToolStripMenuItem.Enabled = show;

            var btn = tsToolMain?.Items["tsBtnDepuracion"] as ToolStripButton;
            if (btn != null)
            {
                btn.Visible = show;
                btn.Enabled = show;
            }
        }

        private bool HayErroresEnLog()
        {
            try
            {
                string basePath = Program.appDAM.rutaBase ?? "";
                string logPath = Path.Combine(basePath, "app.log");
                if (!File.Exists(logPath)) return false;

                using var fs = new FileStream(logPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var sr = new StreamReader(fs);
                var txt = sr.ReadToEnd();
                if (string.IsNullOrWhiteSpace(txt)) return false;

                var lower = txt.ToLowerInvariant();
                return lower.Contains("| error |") || lower.Contains(" error ");
            }
            catch
            {
                return false;
            }
        }

        private void tsBtnEmisores_Click(object sender, EventArgs e) => Abrir<FormSeleccionarEmisores>();
        private void tsBtnVentas_Click(object sender, EventArgs e) { }
        private void tsBtnCompras_Click(object sender, EventArgs e) { }
        private void tsBtnClientes_Click(object sender, EventArgs e) { }
        private void tsBtnProveedores_Click(object sender, EventArgs e) { }

        private void Abrir<T>() where T : Form, new()
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f is T)
                {
                    if (f.WindowState == FormWindowState.Minimized)
                        f.WindowState = FormWindowState.Normal;
                    f.Activate();
                    return;
                }
            }
            new T { MdiParent = this }.Show();
        }
    }
}
