namespace FacturacionDAM.Modelos
{
    // Mantiene los nombres usados en tu código actual
    public class Emisor
    {

        public int id { get; set; }
        public string nifcif { get; set; } = "";
        public string nombre { get; set; } = "";
        public string apellido { get; set; } = "";
        public string nombrecomercial { get; set; } = "";
        public string domicilio { get; set; } = "";
        public string codigopostal { get; set; } = "";
        public string poblacion { get; set; } = "";
        public int? idprovincia { get; set; }           // puede venir NULL en BD
        public string telefono1 { get; set; } = "";
        public string telefono2 { get; set; } = "";
        public string email { get; set; } = "";
        public string descripcion { get; set; } = "";
    }
}
