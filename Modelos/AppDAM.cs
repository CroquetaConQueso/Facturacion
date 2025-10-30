using MySql.Data.MySqlClient;
using System.Text.Json;

namespace FacturacionDAM.Modelos
{
    public class AppDAM
    {
        public ConfiguracionConexion configConexion;        // Objeto con la configuración de la conexión a la BD.
        public EstadoApp estadoApp;                         // Estado de la aplicación en el momento actual.
        public Emisor emisor;                               // Emisor seleccionar
        public string rutaBase { get; private set; }        // Ruta base de la aplicación.
        public string rutaConfigDB;                         // Ruta al archivo de configuración de la base de datos.
        public DebugDam _debug { get; private set; }

        // Me indica si estoy conectado a la base de datos o no.
        public bool conectado => (_conexion != null) && (_conexion.State == System.Data.ConnectionState.Open);

        public string ultimoError { get; private set; }     // Ultimo error registrado.

        private MySqlConnection _conexion = null;           // Cliente MySQL para comunicarnos con la base de datos

        /// <summary>
        /// Constructor.
        /// </summary>
        public AppDAM()
        {
            // Estado inicial de la App.
            estadoApp = EstadoApp.Iniciando;

            // Instancio el cliente mysql.
            _conexion = new MySqlConnection();

            //Inicialmente no hay emisor seleccionado
            emisor = null;

            // Inicializo la aplicación
            InitApp();
        }

        /// <summary>
        /// Inicializa la aplicación (conexión a la base de datos, log de errores, etc.).
        /// </summary>
        private void InitApp()
        {
            // Ruta por defecto en el directorio de la app
            rutaBase = AppDomain.CurrentDomain.BaseDirectory;

            // Ruta al archivo de configuración de la base de datos
            rutaConfigDB = Path.Combine(rutaBase, "configDB.json");

            _debug = new DebugDam(rutaBase);
            RegistrarLog("App", "Inicio de la aplicación");
            // Configuro y me conecto a la base de datos.
            ConfiguraYConectaDB(rutaConfigDB);
        }

        public void ConfiguraYConectaDB(string aRutaConfig)
        {
            // Inicializo la variable que guarda el último error
            ultimoError = "";

            // Cargo los datos de conexión a la base de datos.
            configConexion = CargarConfiguracionDB(aRutaConfig);

            // Intento la conexión a la base de datos
            if (configConexion != null)
            {
                if (ConectarDB())
                    estadoApp = EstadoApp.Conectado;
                else
                    estadoApp = (ultimoError != "") ? EstadoApp.Error : EstadoApp.SinConexion;
            }
            else
            {
                estadoApp = (ultimoError != "") ? EstadoApp.Error : EstadoApp.SinConexion;
            }
        }

        /// <summary>
        /// Carga la configuración de la base de datos en un objeto de la clase "ConfiguracionConexion",
        /// retornando dicho objeto. La configuración la intentará cargar de un archivo llamado
        /// "configDB.json" en el directorio base de la aplicación.
        /// </summary>
        private ConfiguracionConexion CargarConfiguracionDB(string aRuta)
        {
            ConfiguracionConexion resultado = null;

            if (File.Exists(aRuta))
            {
                try
                {
                    string jsonText = File.ReadAllText(aRuta);
                    resultado = JsonSerializer.Deserialize<ConfiguracionConexion>(jsonText);
                }
                catch (Exception ex)
                {
                    ultimoError = "Error al cargar archivo de configuración.\n" + ex.Message;
                }
            }
            return resultado;
        }

        /// <summary>
        /// Intenta conectarse a la base de datos con la configuración de las propiedades de la clase.
        /// Si durante el intento de conexión se produce alguna excepción, almacena el mensaje de error
        /// en el campo "ultimoError".
        /// </summary>
        public bool ConectarDB()
        {
            // Si está conectado me aseguro de cerrar antes de iniciar una nueva conexión.
            if (conectado)
                _conexion.Close();

            // Asigno la cadena de conexión.
            _conexion.ConnectionString = configConexion.CadenaDeConexion();

            try
            {
                _conexion.Open();

                // Verificación mínima: ¿puedo ejecutar algo?
                using (var cmd = new MySqlCommand("SELECT 1", _conexion))
                {
                    cmd.ExecuteScalar();
                }

                // Verificación útil: ¿la BD activa es exactamente la que pedí?
                using (var cmdDb = new MySqlCommand("SELECT DATABASE()", _conexion))
                {
                    var activa = (cmdDb.ExecuteScalar() as string) ?? "";
                    if (!string.Equals(activa, configConexion.baseDatos, StringComparison.OrdinalIgnoreCase))
                        throw new Exception($"Conectado a BD '{activa}', pero se esperaba '{configConexion.baseDatos}'.");
                }

                estadoApp = EstadoApp.Conectado;
                RegistrarLog("App", "Conexión a la base de datos establecida");
                ultimoError = "";
                return true;
            }
            catch (Exception ex)
            {
                // Guarda detalle completo (útil para saber realmente qué pasa)
                ultimoError = "Error al intentar la conexión a la base de datos.\n" + ex.ToString();
                RegistrarLog("App", "No se ha podido conectar a la base de datos. " + ex.Message);

                try
                {
                    if (_conexion.State == System.Data.ConnectionState.Open)
                        _conexion.Close();
                }
                catch { /* ignorar */ }

                estadoApp = EstadoApp.SinConexion;
                return false;
            }
        }

        /// <summary>
        /// Cierra la conexión a la base de datos.
        /// </summary>
        public void DesconectarDB()
        {
            if (conectado)
            {
                try
                {
                    _conexion.Close();
                    RegistrarLog("App", "Conexión a la base de datos cerrada");
                }
                catch (Exception ex)
                {
                    ultimoError = "Error al intentar cerrar conexión a la base de datos.\n" + ex.Message;
                }
            }
            estadoApp = (conectado) ? EstadoApp.Conectado : EstadoApp.SinConexion;
        }

        public void RegistrarLog(string proceso, string mensaje)
        {
            string fecha = DateTime.Now.ToString("dd-MM-yyyy");
            string hora = DateTime.Now.ToString("HH:mm:ss");
            string linea = $"{fecha} | {hora} | {proceso} | {mensaje}";
            _debug?.guardarLog(linea);
        }

        public MySqlConnection LaConexion => _conexion;
    }
}
