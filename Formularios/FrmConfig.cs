using MySql.Data.MySqlClient;
using System.Text.Json;
using FacturacionDAM.Modelos;

namespace FacturacionDAM.Formularios
{
    public partial class FrmConfig : Form
    {
        public FrmConfig()
        {
            InitializeComponent();
        }

        private void btnProbarConexion_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.appDAM.estadoApp == EstadoApp.Conectado)
                {
                    SetControlesEstadoConexion(true);
                    // Cerrar conexión
                    Program.appDAM.DesconectarDB();
                    SetControlesEstadoConexion(false);
                }
                else
                {
                    // Iniciar intento de conexión
                    SetControlesEstadoConexion(true);
                    Program.appDAM.ConectarDB();

                    // Tras el intento, actualizo
                    SetControlesEstadoConexion(false);
                }
            }
            catch (Exception ex)
            {
                SetControlesEstadoConexion(false, ex.Message);
            }
        }

        private void SetControlesEstadoConexion(bool enProceso, string aError = "")
        {
            tsProgressBarConexion.Visible = enProceso;

            if (enProceso)
            {
                // Mostrar mensajes en la barra de estado.
                tsStatusLabel.Text = enProceso ? "Conectando ..." : "";
                tsStatusLabel.ForeColor = Color.Black;
                tsProgressBarConexion.Style = ProgressBarStyle.Marquee;
                btnConexion.Enabled = false;
                pnData.Enabled = false;
            }
            else
            {
                pnData.Enabled = true;
                btnConexion.Enabled = true;
                tsProgressBarConexion.Style = ProgressBarStyle.Blocks;

                if ((Program.appDAM.estadoApp == EstadoApp.Conectado)
                    || (Program.appDAM.estadoApp == EstadoApp.ConectadoSinEmisor))
                {
                    tsStatusLabel.Text = "Conexión establecida correctamente.";
                    tsStatusLabel.ForeColor = Color.Green;
                    btnConexion.Text = "Cerrar conexión";
                }
                else
                {
                    if (aError == "")
                    {
                        tsStatusLabel.Text = "Conexión cerrada.";
                        tsStatusLabel.ForeColor = Color.Black;
                        btnConexion.Text = "Conectar";
                    }
                    else
                    {
                        tsStatusLabel.Text = "Se ha producido un error: " + aError;
                        tsStatusLabel.ForeColor = Color.Red;
                    }
                }
            }
        }


        private void GuardarConfiguracionEnArchivo(string aRuta, ConfiguracionConexion aConfig)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            string jsonText = JsonSerializer.Serialize(aConfig, options);

            // Podría haber hecho lo anterior en la siguiente línea:
            // string json = JsonSerializer.Serialize(aConfig, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(aRuta, jsonText);
            tsLbRutaConfig.Text = aRuta;

        }

        private void tsBtnCargar_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Archivo JSON|*.json",
                Title = "Seleccionar archivo de configuración"
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    // Mientras se conecta desactivo controles.
                    SetControlesEstadoConexion(true);

                    // Configuro y conecto la base de datos
                    Program.appDAM.ConfiguraYConectaDB(dlg.FileName);

                    txtServidor.Text = Program.appDAM.configConexion.servidor;
                    txtPuerto.Text = Program.appDAM.configConexion.puerto.ToString();
                    txtUsuario.Text = Program.appDAM.configConexion.usuario;
                    txtPassword.Text = Program.appDAM.configConexion.password;
                    txtBaseDatos.Text = Program.appDAM.configConexion.baseDatos;

                    // Ajusto controles
                    SetControlesEstadoConexion(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar el archivo:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tsBtnGuardar_Click(object sender, EventArgs e)
        {

            using SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = "Archivo JSON|*.json",
                Title = "Guardar configuración como..."
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ConfiguracionConexion config = new ConfiguracionConexion
                {
                    servidor = txtServidor.Text,
                    puerto = int.Parse(txtPuerto.Text),
                    usuario = txtUsuario.Text,
                    password = txtPassword.Text,
                    baseDatos = txtBaseDatos.Text
                };

                try
                {
                    GuardarConfiguracionEnArchivo(dlg.FileName, config);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FrmConnection_Load(object sender, EventArgs e)
        {
            if ((Program.appDAM.estadoApp == EstadoApp.Conectado) ||
                (Program.appDAM.estadoApp == EstadoApp.ConectadoSinEmisor))
            {
                txtServidor.Text = Program.appDAM.configConexion.servidor;
                txtPuerto.Text = Program.appDAM.configConexion.puerto.ToString();
                txtUsuario.Text = Program.appDAM.configConexion.usuario;
                txtPassword.Text = Program.appDAM.configConexion.password;
                txtBaseDatos.Text = Program.appDAM.configConexion.baseDatos;
            }
            else
            {
                txtServidor.Text = "";
                txtPuerto.Text = "";
                txtUsuario.Text = "";
                txtPassword.Text = "";
                txtBaseDatos.Text = "";
            }

            // Ajusto controles
            SetControlesEstadoConexion(false);

        }
    }
}