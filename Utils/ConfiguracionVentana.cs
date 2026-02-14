using System.Drawing;
using System.Windows.Forms;

namespace FacturacionDAM.Utils
{
    public static class ConfiguracionVentana
    {
        public static void Guardar(Form form, string clave)
        {
            if (form == null || string.IsNullOrWhiteSpace(clave)) return;

            var s = Properties.Settings.Default;

            string kState = $"{clave}_WindowState";
            string kLoc = $"{clave}_Location";
            string kSize = $"{clave}_Size";

            if (s.Properties[kState] == null || s.Properties[kLoc] == null || s.Properties[kSize] == null)
                return;

            s[kState] = (int)form.WindowState;

            if (form.WindowState == FormWindowState.Normal)
            {
                s[kLoc] = form.Location;
                s[kSize] = form.Size;
            }
            else
            {
                s[kLoc] = form.RestoreBounds.Location;
                s[kSize] = form.RestoreBounds.Size;
            }

            s.Save();
        }

        public static void Restaurar(Form form, string clave)
        {
            if (form == null || string.IsNullOrWhiteSpace(clave)) return;

            var s = Properties.Settings.Default;

            string kState = $"{clave}_WindowState";
            string kLoc = $"{clave}_Location";
            string kSize = $"{clave}_Size";

            if (s.Properties[kState] == null || s.Properties[kLoc] == null || s.Properties[kSize] == null)
                return;

            if (s[kLoc] is Point loc)
            {
                form.StartPosition = FormStartPosition.Manual;
                form.Location = loc;
            }

            if (s[kSize] is Size size && size.Width > 0 && size.Height > 0)
                form.Size = size;

            if (s[kState] is int ws)
            {
                var st = (FormWindowState)ws;
                if (st == FormWindowState.Maximized || st == FormWindowState.Normal)
                    form.WindowState = st;
            }
        }
    }
}
