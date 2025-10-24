using FacturacionDAM.Modelos;
using FacturacionDAM.Formularios;

namespace FacturacionDAM
{
    internal static class Program
    {
        public static AppDAM appDAM;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            appDAM = new AppDAM();
            Application.Run(new FrmMain());
        }
    }
}