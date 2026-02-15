
using Facturacion.Modelos;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System.Data;
using System.Diagnostics;
using System.Text.Json;

namespace FacturacionDAM.Modelos {
    public class AppDAM {

        public ConfiguracionConexion configConexion;        // Objeto con la configuración de la conexión a la BD.
        public EstadoApp estadoApp;                         // Estado de la aplicación en el momento actual.
        public Emisor emisor;                               // Objeto que representa el emisor seleccionado.
        public string rutaBase { get; private set; }        // Ruta base de la aplicación.
        public string rutaConfigDB;                         // Ruta al archivo de configuración de la base de datos.
        public string rutaLog;                              // Ruta al archivo de logs.

        // Me indica si estoy conectado a la base de datos o no.
        public bool conectado => (_conexion != null) && (_conexion.State == System.Data.ConnectionState.Open);

        public string ultimoError { get; private set; }     // Ultimo error registrado.
        public DebugDAM debug { get; private set; }         // Objeto para la depuración

        private MySqlConnection _conexion = null;           // Cliente MySQL para comunicarnos con la base de datos

        /// <summary>
        /// Constructor.
        /// </summary>
        public AppDAM() {

            // Estado inicial de la App.
            estadoApp = EstadoApp.Iniciando;

            // Instancio el cliente mysql.
            _conexion = new MySqlConnection();

            emisor = null;

            // Inicializo la aplicación
            InitApp();            
        }

        /// <summary>
        /// Inicializa la aplicación (conexión a la base de datos, log de errores, etc.).
        /// </summary>
        private void InitApp () {
            // Ruta por defecto en Documentos
            rutaBase = AppDomain.CurrentDomain.BaseDirectory;

            // Ruta al archivo de configuración de la base de datos
            rutaConfigDB = Path.Combine(rutaBase, "configDB.json");

            // Creación del objeto para registro de los logs
            rutaLog = Path.Combine(rutaBase, "logs", "info.log");
            debug = new DebugDAM (rutaLog);

            // Configuro y me conecto a la base de datos.
            ConfiguraYConectaDB(rutaConfigDB);
        }



        public void ConfiguraYConectaDB(string aRutaConfig)  {
            // Inicializo la variable que guarda el último error
            ultimoError = "";

            // Cargo los datos de conexión a la base de datos.
            configConexion = CargarConfiguracionDB(aRutaConfig);

            // Intento la conexión a la base de datos
            if (configConexion != null)
            {
                if (ConectarDB())
                    estadoApp = EstadoApp.ConectadoSinEmisor;
                else
                    estadoApp = (ultimoError != "") ? EstadoApp.Error : EstadoApp.SinConexion;
            }
            else
                estadoApp = (ultimoError != "") ? EstadoApp.Error : EstadoApp.SinConexion;

        }

        /// <summary>
        /// Carga la configuración de la base de datos en un objeto de la clase "ConfiguracionConexion",
        /// retornando dicho objeto. La configuración la intentará cargar de un archivo llamado
        /// "configDB.json" en el directorio base de la aplicación.
        /// </summary>
        /// <returns>Retorna el objeto de tipo "ConfiguracionConexion" con la configuración de la base
        /// de datos, null si no lo ha conseguido.</returns>
        private ConfiguracionConexion CargarConfiguracionDB(string aRuta)  {
            
            ConfiguracionConexion resultado = null;
            
            if (File.Exists(aRuta)) {
                try {
                    string jsonText = File.ReadAllText(aRuta);
                    resultado = JsonSerializer.Deserialize<ConfiguracionConexion>(jsonText);
                }
                catch (Exception ex) {
                    ultimoError = "Error al cargar archivo de configuración.\n" + ex.Message;
                    RegistrarLog("Cargar configuración DB", "Error al cargar archivo de configuración." + ex.Message);
                }
            }
            return resultado;
        }

        /// <summary>
        /// Intenta conectarse a la base de datos con la configuración de las propiedades de la clase.
        /// Si durante el intenta de conexión se produce alguna excepción, almacena el mensaje de error
        /// en el campo "UltimoError".
        /// </summary>
        /// <returns>True si se ha conectado correctamente, false sino.</returns>
        public bool ConectarDB () {
            
            // Si está conectado me aseguro de cerrar antes de iniciar una nueva conexión.
            if (conectado)
                _conexion.Close();

            // Asigno la cadena de conexión.
            _conexion.ConnectionString = configConexion.CadenaDeConexion();

            // Intento la conexión.
            try {
                _conexion.Open();
                RegistrarLog("Conexión a la DB", "Conexión abierta correctamente");
            }
            catch (Exception ex) {
                ultimoError = "Error al intentar la conexión a la base de datos.\n" + ex.Message;
                RegistrarLog("Conexión a la DB", "Error al intentar la conexión a la base de datos." + ex.Message);
            }
            estadoApp = (conectado) ? EstadoApp.Conectado : EstadoApp.SinConexion;
            return conectado;
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
                    RegistrarLog("Desonexión de la DB", "Conexión cerrada correctamente.");
                }
                catch (Exception ex)
                {
                    ultimoError = "Error al intentar cerrar conexión a la base de datos.\n" + ex.Message;
                    RegistrarLog("Desonexión de la DB", "Error al intentar cerrar conexión a la base de datos." + ex.Message);
                }
            }
            estadoApp = (conectado) ? EstadoApp.Conectado : EstadoApp.SinConexion;
        }

        /// <summary>
        /// Registra un mensaje en el archivo de log con formato:
        /// Fecha | Hora | Proceso | Mensaje
        /// </summary>
        public void RegistrarLog(string proceso, string mensaje)
        {
            string textoLog = $"{DateTime.Now:yyyy-MM-dd} | {DateTime.Now:HH:mm:ss} | {proceso} | {mensaje}";
            debug.GuardarLog(textoLog);
        }

        /// <summary>
        /// Acceso de sólo lectura al objeto de conexion a la base de datos.
        /// </summary>
        public MySqlConnection LaConexion => _conexion;

        /// <summary>
        /// Verifica si hay clientes en la base de datos.
        /// </summary>
        /// <returns>Retorna true si los hay, false sino.</returns>
        public bool HayClientes()
        {
            using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM clientes", _conexion))
            {
                var n = Convert.ToInt32(cmd.ExecuteScalar());
                return n > 0;
            }
        }

        /// <summary>
        /// Verifica si hay proveedores en la base de datos.
        /// </summary>
        /// <returns>Retorna true si los hay, false sino.</returns>
        public bool HayProveedores()
        {
            using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM proveedores", _conexion))
            {
                var n = Convert.ToInt32(cmd.ExecuteScalar());
                return n > 0;
            }
        }

    }
}