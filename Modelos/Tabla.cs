/*
 * Clase: Tabla
 * Propósito: Abstracción para la gestión de datos mediante DataTables y DataAdapters.
 * Facilita la carga, manipulación y persistencia de datos contra MySQL, gestionando
 * automáticamente la concurrencia y la sincronización de IDs autonuméricos tras inserciones.
 */

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

        // Inicializa el DataAdapter y carga los datos en el DataTable.
        // Configura la recuperación automática de IDs para evitar errores de concurrencia.
        public bool InicializarDatos(string sql, Dictionary<string, object>? parametros = null)
        {
            if (string.IsNullOrWhiteSpace(sql)) return false;

            Liberar();

            _sqlBase = sql;
            _paramBase = parametros != null ? new Dictionary<string, object>(parametros) : null;

            LaTabla = new DataTable();

            _da = new MySqlDataAdapter();
            _da.SelectCommand = CrearCommand(sql, parametros);

            // Sincronización de IDs tras INSERT para evitar DBConcurrencyException en borrados posteriores.
            _da.RowUpdated += (s, e) =>
            {
                if (e.Status == UpdateStatus.Continue && e.StatementType == StatementType.Insert)
                {
                    try
                    {
                        if (e.Row.Table.Columns.Contains("id"))
                        {
                            var cmd = new MySqlCommand("SELECT LAST_INSERT_ID()", _da.SelectCommand.Connection);
                            long newId = Convert.ToInt64(cmd.ExecuteScalar());
                            e.Row["id"] = newId;
                        }
                    }
                    catch { } // Continuar flujo aunque falle la sincronización del ID.
                }
            };

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