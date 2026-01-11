using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionDAM.Utils
{
    public static class ExportarDatos
    {

        /// <summary>
        /// Guarda el contenido del DataTable pasado como parámetro en formato CSV en el archivo
        /// cuya ruta se pasa como segundo parámetro.
        /// Sustituye puntos y coma por coma, y saltos de línea por espacios en blanco.
        /// </summary>
        /// <param name="dt">El objeto DataTable del cual extraer los datos.</param>
        /// <param name="ruta">Archivo (con su ruta) en la cual guardar los datos en formato csv.</param>
        public static void ExportarCSV(DataTable dt, string ruta)
        {
            try
            {
                var lineas = new List<string>();
                var columnas = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
                lineas.Add(string.Join(";", columnas));

                foreach (DataRow row in dt.Rows)
                {
                    var valores = row.ItemArray.Select(v =>
                    {
                        string valor = v?.ToString() ?? "";

                        // Reemplaza puntos y coma por coma, y elimina saltos de línea.
                        valor = valor.Replace(";", ",")
                            .Replace("\r", " ")
                            .Replace("\n", " ");

                        return valor;
                    });

                    lineas.Add(string.Join(";", valores));
                }

                // Guardar en archivo.
                File.WriteAllLines(ruta, lineas, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Program.appDAM.RegistrarLog("Exportacion a CSV", ex.Message);
                MessageBox.Show("Se ha producido un error al exportar los datos.");
            }
        }

        /// <summary>
        /// Guarda el contenido del DataTable pasado como parámetro en formato XML en el archivo
        /// cuya ruta se pasa como segundo parámetro.
        /// </summary>
        /// <param name="dt">El objeto DataTable del cual extraer los datos.</param>
        /// <param name="ruta">Archivo (con su ruta) en la cual guardar los datos en formato XLM.</param>
        /// <param name="nombreTabla">Nombre de la tabla de la cual se extraen los datos.</param>
        public static void ExportarXML(DataTable dt, string rutaArchivo, string nombreTabla)        {
            try {
                dt.TableName = nombreTabla; // O clientes, o proveedores, o ...
                dt.WriteXml(rutaArchivo, XmlWriteMode.WriteSchema);
            }
            catch (Exception ex)
            {
                Program.appDAM.RegistrarLog("Exportacion a XML", ex.Message);
                MessageBox.Show("Se ha producido un error al exportar los datos.");
            }
            // Escribe el contenido del DataTable como XML completo (estructura + datos)
            dt.WriteXml(rutaArchivo, XmlWriteMode.WriteSchema);
        }


    }
}
