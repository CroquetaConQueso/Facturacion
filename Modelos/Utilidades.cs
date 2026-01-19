// Ruta: FacturacionDAM/Modelos/Utilidades.cs
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FacturacionDAM.Modelos
{
    public static class Utilidades
    {
        public static void GuardarVentana(Form frm, string clave)
        {
            try
            {
                var state = frm.WindowState;
                Rectangle bounds = state == FormWindowState.Normal ? frm.Bounds : frm.RestoreBounds;

                Properties.Settings.Default[$"{clave}Location"] = bounds.Location;
                Properties.Settings.Default[$"{clave}Size"] = bounds.Size;
                Properties.Settings.Default[$"{clave}WindowState"] = (int)state;
                Properties.Settings.Default.Save();
            }
            catch (SettingsPropertyNotFoundException)
            {
            }
            catch
            {
            }
        }

        public static void RestaurarVentana(Form frm, string clave)
        {
            try
            {
                if (Properties.Settings.Default[$"{clave}Location"] is Point p)
                    frm.StartPosition = FormStartPosition.Manual;

                if (Properties.Settings.Default[$"{clave}Size"] is Size s)
                    frm.Size = s;

                if (Properties.Settings.Default[$"{clave}Location"] is Point p2)
                    frm.Location = p2;

                if (Properties.Settings.Default[$"{clave}WindowState"] is int st)
                {
                    var ws = (FormWindowState)st;
                    if (ws == FormWindowState.Maximized) frm.WindowState = FormWindowState.Maximized;
                    else frm.WindowState = FormWindowState.Normal;
                }
            }
            catch (SettingsPropertyNotFoundException)
            {
            }
            catch
            {
            }
        }

        public static int LeerIntSetting(string key, int porDefecto)
        {
            try
            {
                var v = Properties.Settings.Default[key];
                if (v == null) return porDefecto;
                if (v is int i) return i;
                if (int.TryParse(Convert.ToString(v), out var n)) return n;
                return porDefecto;
            }
            catch (SettingsPropertyNotFoundException)
            {
                return porDefecto;
            }
            catch
            {
                return porDefecto;
            }
        }

        public static void EscribirIntSetting(string key, int valor)
        {
            try
            {
                Properties.Settings.Default[key] = valor;
                Properties.Settings.Default.Save();
            }
            catch (SettingsPropertyNotFoundException)
            {
            }
            catch
            {
            }
        }

        public static void ExportarCSV(DataTable dt, string rutaArchivo)
        {
            if (dt == null) throw new ArgumentNullException(nameof(dt));

            var cols = dt.Columns.Cast<DataColumn>().ToList();
            var sb = new StringBuilder();

            sb.AppendLine(string.Join(";", cols.Select(c => c.ColumnName)));

            foreach (DataRow row in dt.Rows)
            {
                var values = cols.Select(c =>
                {
                    var v = row[c];
                    var s = v == null || v == DBNull.Value ? "" : Convert.ToString(v) ?? "";
                    s = s.Replace(";", ",");
                    return s;
                });

                sb.AppendLine(string.Join(";", values));
            }

            File.WriteAllText(rutaArchivo, sb.ToString(), Encoding.UTF8);
        }

        public static void ExportarXML(DataTable dt, string rutaArchivo, string? nombreDataset = null)
        {
            if (dt == null) throw new ArgumentNullException(nameof(dt));
            var copia = dt.Copy();
            if (!string.IsNullOrWhiteSpace(nombreDataset))
                copia.TableName = nombreDataset;

            copia.WriteXml(rutaArchivo, XmlWriteMode.WriteSchema);
        }
    }
}
