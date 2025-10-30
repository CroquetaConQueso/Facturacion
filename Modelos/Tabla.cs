using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace FacturacionDAM.Modelos
{
    /// <summary>
    /// Wrapper simple para DataTable + DataAdapter + CommandBuilder.
    /// </summary>
    public class Tabla
    {
        private readonly MySqlConnection _conn;
        private readonly MySqlDataAdapter _adapter;
        private MySqlCommandBuilder _builder;
        private DataTable _tabla;

        public Tabla(MySqlConnection conexion)
        {
            _conn = conexion ?? throw new ArgumentNullException(nameof(conexion));
            _adapter = new MySqlDataAdapter();
        }

        public bool InicializarDatos(string sql)
        {
            try
            {
                _adapter.SelectCommand = new MySqlCommand(sql, _conn);

                _builder = new MySqlCommandBuilder(_adapter);
                _adapter.InsertCommand = _builder.GetInsertCommand();
                _adapter.UpdateCommand = _builder.GetUpdateCommand();
                _adapter.DeleteCommand = _builder.GetDeleteCommand();

                _tabla = new DataTable();
                _adapter.Fill(_tabla);
                return true;
            }
            catch (Exception ex)
            {
                Program.appDAM?.RegistrarLog("Cargar tabla", ex.Message);
                return false;
            }
        }

        public void Refrescar()
        {
            _tabla.Clear();
            _adapter.Fill(_tabla);
        }

        public void GuardarDatos()
        {
            _adapter.Update(_tabla);
        }

        public void Liberar()
        {
            _tabla?.Dispose();
            _adapter?.Dispose();
            _builder = null;
        }

        public DataTable LaTabla => _tabla;

        
        public int ValorEntero(DataRowView drv, string columna, int porDefecto = 0)
        {
            if (drv == null) return porDefecto;
            var row = drv.Row;
            if (!row.Table.Columns.Contains(columna)) return porDefecto;

            var obj = row[columna];
            if (obj == null || obj == DBNull.Value) return porDefecto;

            if (obj is int i) return i;
            if (int.TryParse(Convert.ToString(obj), out int v)) return v;
            return porDefecto;
        }

        public string ValorTexto(DataRowView drv, string columna, string porDefecto = "")
        {
            if (drv == null) return porDefecto;
            var row = drv.Row;
            if (!row.Table.Columns.Contains(columna)) return porDefecto;

            var obj = row[columna];
            if (obj == null || obj == DBNull.Value) return porDefecto;

            return Convert.ToString(obj) ?? porDefecto;
        }
    }
}
