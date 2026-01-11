using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionDAM.Utils
{
    public static class ConfiguracionVentana
    {

        /// <summary>
        /// Guarda el estado de la ventana.
        /// </summary>
        /// <param name="frm">Objeto de tipo Form cuyas propiedades queremos guardar.</param>
        /// <param name="nombreForm">Nombre que le he puesto al form, para encontrar la propiedad adecuada en los Settings.</param>
        public static void Guardar(Form frm, string nombreForm)
        {
            Properties.Settings.Default[$"{nombreForm}State"] = frm.WindowState.ToString();
            if (frm.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default[$"{nombreForm}Location"] = frm.Location;
                Properties.Settings.Default[$"{nombreForm}Size"] = frm.Size;
            }
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Restaura el estado de la ventana.
        /// </summary>
        /// <param name="frm">Objeto de tipo Form cuyas propiedades queremos restaurar.</param>
        /// <param name="nombreForm">Nombre que le he puesto al form, para encontrar la propiedad adecuada en los Settings.</param>

        public static void Restaurar(Form frm, string nombreForm)
        {

            // Restaurar el estado
            string estado = Properties.Settings.Default[$"{nombreForm}State"].ToString();
            switch (estado)
            {
                case "Maximized":
                    frm.WindowState = FormWindowState.Maximized;
                    break;
                case "Minimized":
                    frm.WindowState = FormWindowState.Minimized;
                    break;
                default:
                    frm.WindowState = FormWindowState.Normal;
                    break;
            }

            //if (Properties.Settings.Default[$"{nombreForm}State"] is string estado)
                //frm.WindowState = Enum.Parse<FormWindowState>(estado);

            if (frm.WindowState == FormWindowState.Normal)
            {
                if (Properties.Settings.Default[$"{nombreForm}Location"] is Point loc)
                    frm.Location = loc;

                if (Properties.Settings.Default[$"{nombreForm}Size"] is Size sz)
                    frm.Size = sz;
            }
        }

    }
}
