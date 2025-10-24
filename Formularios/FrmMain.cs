using FacturacionDAM.Modelos;
using System.Xml.Linq;

namespace FacturacionDAM.Formularios
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        /******** EVENTOS DEL FORMULARIO Y CONTROLES *********/

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (Program.appDAM.conectado)
                Program.appDAM.DesconectarDB();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
#if !DEBUG
                // Si no estamos en compilaci�n DEBUG, ocultamos el men� de depuraci�n
                tsMenuItemDepura.Visible = false;
#endif

            SeleccionarEmisor();
            RefreshControles();
        }

        private void tsBtnConfig_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo<FrmConfig>();
        }

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            CerrarFormulariosHijos();
            this.Close();
        }

        private void tsMenuItemDepura_Click(object sender, EventArgs e)
        {
#if DEBUG
            AbrirFormularioHijo<FrmDepuracion>();
#endif

        }

        private void tsItemMenuSeleccionarEmisor_Click(object sender, EventArgs e)
        {
            CerrarFormulariosHijos();
            SeleccionarEmisor();
            RefreshControles();
        }


        /*************** M�TODOS PRIVADOS *******************/

        private void SeleccionarEmisor()
        {
            if ( (Program.appDAM.estadoApp == EstadoApp.ConectadoSinEmisor) ||
                 (Program.appDAM.estadoApp == EstadoApp.Conectado) )
            {
                using (var frm = new FrmSeleccionarEmisor())
                {
                    var resultado = frm.ShowDialog();

                    Program.appDAM.estadoApp = 
                        (Program.appDAM.emisor == null) ? EstadoApp.ConectadoSinEmisor : EstadoApp.Conectado;

                }
            }
        }

        private void CerrarFormulariosHijos()
        {
            foreach (Form frm in this.MdiChildren)
                if (frm is not FrmDepuracion)
                    frm.Close();
        }


        /// <summary>
        /// Abre un formulario hijo MDI del tipo indicado. 
        /// Si ya existe, lo activa y lo restaura si estaba minimizado.
        /// </summary>
        private void AbrirFormularioHijo<T>() where T : Form, new()
        {
            // Buscar si ya existe un formulario hijo de ese tipo
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is T)
                {
                    // Si estaba minimizado, lo restauramos
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;

                    frm.Activate();
                    return;
                }
            }

            // No estaba abierto: creamos una nueva instancia
            T nuevoFrm = new T();
            nuevoFrm.MdiParent = this;
            nuevoFrm.WindowState = FormWindowState.Maximized;
            nuevoFrm.Show();
        }


        private void RefreshToolBar()
        {
            if (Program.appDAM.estadoApp != EstadoApp.Conectado)
            {
                foreach (ToolStripItem item in tsToolMain.Items)
                {
                    if (item is ToolStripButton)
                    {
                        switch (item.Name)
                        {
                            case "tsBtnConfig":
                                item.Enabled = true;
                                break;
                            case "tsBtnSalir":
                                item.Enabled = true;
                                break;
                            case "tsBtnEmisores":
                                item.Enabled = (Program.appDAM.estadoApp == EstadoApp.ConectadoSinEmisor) ? true : false;
                                break;
                            default:
                                item.Enabled = false;
                                break;

                        }
                    }
                }
            }
        }

        private void RefreshStatusBar()
        {
            if (Program.appDAM.emisor == null)
                tsLbEmisor.Text = "Sin emisor seleccionado";
            else
                tsLbEmisor.Text = $"{Program.appDAM.emisor.nombre} {Program.appDAM.emisor.apellidos};  NIF: {Program.appDAM.emisor.nifcif}";

            switch (Program.appDAM.estadoApp)
            {
                case EstadoApp.Conectado:
                    tsLbEstado.Text = "Conectado a la base de datos";
                    break;
                case EstadoApp.SinConexion:
                    tsLbEstado.Text = "No se ha establecido la conexi�n a la base de datos.";
                    break;
                case EstadoApp.ConectadoSinEmisor:
                    tsLbEstado.Text = "Conectado a la base de datos, pero no se ha seleccionado un emisor.";
                    break;
                case EstadoApp.Error:
                    if (Program.appDAM.ultimoError != "")
                        tsLbEstado.Text = "Se ha producido un error, revisa el log para m�s detalles.";
                    else
                        tsLbEstado.Text = "Se ha producido un error";
                    break;
            }
        }

        private void RefreshControles()
        {
            RefreshToolBar();
            RefreshStatusBar();
        }
    }
}
