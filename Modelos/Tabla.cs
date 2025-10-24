using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionDAM.Modelos
{
    public class Tabla
    {
        private MySqlConnection _conn;              // Objeto de conexión a la base de datos.
        private MySqlDataAdapter _adapter;          // Puente entre la tabla en la BD y la tabla en memoria (el DataTable).
        private MySqlCommandBuilder _builder;       // Objeto que nos facilita la ejecución de comandos sql automáticamente.
        private DataTable _tabla;                   // La tabla en memoria.

        /// <summary>
        /// Constructor. Recibe el objeto MySqlConnection de conexión a la base de datos.
        /// </summary>
        /// <param name="conexion"></param>
        public Tabla(MySqlConnection conexion)
        {
            _conn = conexion;
            _adapter = new MySqlDataAdapter();
        }

        /// <summary>
        /// Inicializa la selección de datos de la tabla.
        /// </summary>
        /// <param name="sql">Sentencia SQL de acceso.</param>
        /// <returns></returns>
        public bool InicializarDatos(string sql)
        {
            try
            {
                _adapter.SelectCommand = new MySqlCommand(sql, _conn);
                _builder = new MySqlCommandBuilder(_adapter);
                _tabla = new DataTable();
                _adapter.Fill(_tabla);
                return true;
            }
            catch (Exception ex)
            {
                Program.appDAM.RegistrarLog("Cargando emisores", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Actualiza los datos que contiene el DataTable asociado.
        /// </summary>
        public void Refrescar()
        {
            _tabla.Clear();
            _adapter.Fill(_tabla);
        }

        /// <summary>
        /// Guarda los cambios, si los hubiera.
        /// </summary>
        public void GuardarCambios()
        {
            _adapter.Update(_tabla);
        }

        /// <summary>
        /// Liberamos recursos de forma explícita.
        /// </summary>
        public void Liberar()
        {
            _tabla?.Dispose();
            _adapter?.Dispose();
            _builder = null;
        }

        /// <summary>
        /// Acceso de sólo lectura al DataTable.
        /// </summary>
        public DataTable LaTabla => _tabla;
    }

}
