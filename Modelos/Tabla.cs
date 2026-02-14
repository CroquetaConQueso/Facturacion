using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace FacturacionDAM.Modelos
{
    public class Tabla
    {
        private readonly MySqlConnection _cn;
        private MySqlDataAdapter? _da;
        private MySqlCommandBuilder? _cb;

        private string? _sqlBase;
        private Dictionary<string, object>? _paramBase;

        public DataTable LaTabla { get; private set; } = new DataTable();

        public Tabla(MySqlConnection cn)
        {
            _cn = cn ?? throw new ArgumentNullException(nameof(cn));
        }

        public bool InicializarDatos(string sql, Dictionary<string, object>? parametros = null)
        {
            if (string.IsNullOrWhiteSpace(sql)) return false;

            Liberar();

            _sqlBase = sql;
            _paramBase = parametros != null ? new Dictionary<string, object>(parametros) : null;

            LaTabla = new DataTable();

            _da = new MySqlDataAdapter();
            _da.SelectCommand = CrearCommand(sql, parametros);
            _da.Fill(LaTabla);

            return true;
        }

        public void Refrescar()
        {
            if (_sqlBase == null) return;
            InicializarDatos(_sqlBase, _paramBase);
        }

        public int GuardarCambios()
        {
            if (_da == null) return 0;

            if (_cb == null)
                _cb = new MySqlCommandBuilder(_da);

            return _da.Update(LaTabla);
        }

        public int EjecutarComando(string sql, Dictionary<string, object>? parametros = null)
        {
            using var cmd = CrearCommand(sql, parametros);
            return cmd.ExecuteNonQuery();
        }

        public object? EjecutarEscalar(string sql, Dictionary<string, object>? parametros = null)
        {
            using var cmd = CrearCommand(sql, parametros);
            return cmd.ExecuteScalar();
        }

        // Método añadido solicitado
        public long UltimoIdInsertado()
        {
            var res = EjecutarEscalar("SELECT LAST_INSERT_ID()");
            if (res != null && res != DBNull.Value)
            {
                return Convert.ToInt64(res);
            }
            return 0;
        }

        public void Liberar()
        {
            _cb?.Dispose();
            _cb = null;

            _da?.Dispose();
            _da = null;
        }

        private MySqlCommand CrearCommand(string sql, Dictionary<string, object>? parametros)
        {
            var cmd = new MySqlCommand(sql, _cn);

            if (parametros != null)
            {
                foreach (var kv in parametros)
                    cmd.Parameters.AddWithValue(kv.Key, kv.Value ?? DBNull.Value);
            }

            return cmd;
        }
    }
}