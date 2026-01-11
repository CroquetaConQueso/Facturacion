using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionDAM.Modelos
{
    public class Emisor
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string nifcif {  get; set; } 

        public string nombreComercial { get; set; }

        public int nextNumFac {  get; set; }

        public Emisor ()
        {
            id = -1;
        }
    }
}
