using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace FacturacionDAM.Utils
{
    public static class ExportarDatos
    {
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
                        valor = valor.Replace(";", ",").Replace("\r", " ").Replace("\n", " ");
                        return valor;
                    });

                    lineas.Add(string.Join(";", valores));
                }

                File.WriteAllLines(ruta, lineas, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Program.appDAM.RegistrarLog("Exportacion a CSV", ex.Message);
                MessageBox.Show("Se ha producido un error al exportar los datos.");
            }
        }

        public static void ExportarXML(DataTable dt, string rutaArchivo, string nombreTabla)
        {
            try
            {
                dt.TableName = nombreTabla;
                dt.WriteXml(rutaArchivo, XmlWriteMode.WriteSchema);
            }
            catch (Exception ex)
            {
                Program.appDAM.RegistrarLog("Exportacion a XML", ex.Message);
                MessageBox.Show("Se ha producido un error al exportar los datos.");
            }
        }
    }
}
