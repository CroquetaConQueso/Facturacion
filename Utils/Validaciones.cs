using Google.Protobuf.Reflection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FacturacionDAM.Utils
{
    public static class Validaciones
    {

        /// <summary>
        /// Verifica si el email recibido como parámetro es un email válido.
        /// </summary>
        /// <param name="email">El email a verificar.</param>
        /// <returns>Retorna true si el email es válido, false sino.</returns>
        public static bool EsEmailValido(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        /// <summary>
        /// Comprueba la existencia registros en una tabla (primer parámetro) que tengan un campo (segundo parámetro)
        /// cuyo valor coincida con el valor pasado como tercer parámetro. El cuarto parámetro nos permite ignorar
        /// registros en la tabla cuyo campo "id" coincida con ese valor pasado como 4º parámetro. 
        /// </summary>
        /// <param name="tabla">El nombre de la tabla.</param>
        /// <param name="campo">El nombre del campo cuyo valor queremos comparar.</param>
        /// <param name="valor">El valor a comparar.</param>
        /// <param name="idActual">Parámetro entero opcional, que representa el valor "id" de los registros a ignorar.</param>
        /// <returns>Retorna true si encuentra registros, false sino.</returns>
        public static bool EsValorCampoUnico(string tabla, string campo, string valor, int? idActual = null)
        {
            string consulta = $"SELECT COUNT(*) FROM {tabla} WHERE {campo} = @valor";
            using var cmd = new MySqlCommand(consulta, Program.appDAM.LaConexion);
            cmd.Parameters.AddWithValue("@valor", valor);

            if (idActual != null)
            {
                cmd.CommandText += " AND id <> @id";
                cmd.Parameters.AddWithValue("@id", idActual);
            }

            return Convert.ToInt32(cmd.ExecuteScalar()) == 0;
        }
    }
}
