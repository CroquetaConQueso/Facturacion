using System;
using System.IO;

namespace Facturacion.Modelos
{
    /// <summary>
    /// Clase sencilla para registrar mensajes en un archivo de log.
    /// </summary>
    public class DebugDAM
    {
        public string rutaArchivoLog { get; set; }

        public DebugDAM(string rutaArchivo)
        {
            rutaArchivoLog = rutaArchivo;
        }

        /// <summary>
        /// Guarda un mensaje en el archivo de log.
        /// Si el archivo no existe, lo crea.
        /// </summary>
        public void GuardarLog(string mensaje)
        {
            try
            {
                // Crea el directorio si no existe
                string? dir = Path.GetDirectoryName(rutaArchivoLog);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                // Añade el mensaje al final del archivo
                File.AppendAllText(rutaArchivoLog, mensaje + Environment.NewLine);
            }
            catch (Exception ex)
            {
                // En caso de error al registrar el log, mostramos un mensaje simple (para depuración)
                Console.WriteLine($"Error al guardar log: {ex.Message}");
            }
        }
    }
}
