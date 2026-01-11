
namespace FacturacionDAM.Modelos
{
    public class ConfiguracionConexion
    {
        public string servidor { get; set; }                // Datos del servidor
        public int puerto { get; set; }                     // Puerto de conexión en el servidor
        public string usuario { get; set; }                 // Usuario que se conecta a la base de datos
        public string password { get; set; }                // Contraseña de conexión usada por el usuario
        public string baseDatos { get; set; }               // Base de datos a la que se conecta.
        public bool conectado { get; private set; }         // De sólo lectura. Indica si está conectado o no.

        /// <summary>
        /// Construye la cadena de conexión a la base de datos, en función de los campos de la clase, 
        /// y la retorna.
        /// </summary>
        /// <returns>La cadena de conexión a la base de datos.</returns>
        public string CadenaDeConexion ()
        {
            return $"server={servidor};port={puerto.ToString()};user={usuario};password={password};database={baseDatos}";
        }
    }
}