using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionDAM.Modelos
{
    public class DebugDam
    {
        public string rutaLog { get; set; }

        public DebugDam(String ruta)
        {
            if(!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }
            rutaLog = Path.Combine(ruta, "app.log");
        }

        public void guardarLog(string mensaje)
        {
            File.AppendAllText(rutaLog,mensaje+System.Environment.NewLine);
        }
    }
}
