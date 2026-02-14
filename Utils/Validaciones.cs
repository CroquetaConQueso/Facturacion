// Validaciones.cs
using MySql.Data.MySqlClient;
using System;
using System.Text.RegularExpressions;

namespace FacturacionDAM.Utils
{
    public static class Validaciones
    {
        public static bool EsEmailValido(string email)
        {
            return Regex.IsMatch(email ?? "", @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool EsValorCampoUnico(string tabla, string campo, string valor, int? idActual = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tabla) || string.IsNullOrWhiteSpace(campo)) return true;

                string consulta = $"SELECT COUNT(*) FROM {tabla} WHERE {campo} = @valor";

                using var cmd = new MySqlCommand(consulta, Program.appDAM.LaConexion);
                cmd.Parameters.AddWithValue("@valor", valor);

                if (idActual != null)
                {
                    cmd.CommandText += " AND id <> @id";
                    cmd.Parameters.AddWithValue("@id", idActual.Value);
                }

                if (Program.appDAM.LaConexion.State != System.Data.ConnectionState.Open)
                    Program.appDAM.LaConexion.Open();

                int count = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                return count == 0;
            }
            catch (Exception ex)
            {
               
                Program.appDAM.RegistrarLog("Error en Validación Unica", ex.Message);

                return true;
            }
        }
    }
    }
