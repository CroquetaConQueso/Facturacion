using FacturacionDAM.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;

namespace FacturacionDAM.Formularios
{
    public partial class FrmInformeFacemiAnual : Form
    {
        public FrmInformeFacemiAnual()
        {
            InitializeComponent();
        }

        private void btnInforme_Click(object sender, EventArgs e)
        {
            string sql = @"
                SELECT 
                    * FROM vista_facturas_emitidas
                WHERE Emisor = " + Program.appDAM.emisor.id.ToString() +
                " AND Fecha BETWEEN '" + dTPAnoInicio.Value.Date.ToString("yyyy-MM-dd") +
                "' AND '" + dTPAnoFin.Value.Date.ToString("yyyy-MM-dd") + "'";

            Tabla tabla = new Tabla(Program.appDAM.LaConexion);

            if (!tabla.InicializarDatos(sql))
            {
                MessageBox.Show("No se han podido cargar los datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {

                // Cargar el informe
                StiReport reporte = new StiReport();
                reporte.Load("informes/InformeFacemiList1.mrt");
                reporte.Dictionary.Databases.Clear();
                var ds = new DataSet();
                ds.Tables.Add(tabla.LaTabla.Copy());
                ds.Tables[0].TableName = "vista_facturas_emitidas";
                reporte.RegData(ds);
                reporte.Dictionary.Synchronize();

                // Asignar parámetros por código
                StiDataBand dataBand = reporte.Pages[0].Components.OfType<StiDataBand>().FirstOrDefault();
                if (dataBand != null)
                {
                    dataBand.DataSourceName = "vista_facturas_emitidas";
                }

                // Modificamos las variables del informe
                reporte.Dictionary.Variables["nombreEmisor"].Value = Program.appDAM.emisor.nombreComercial;
                reporte.Dictionary.Variables["rangoFechas"].Value = "desde " + dTPAnoInicio.Value.Date.ToString("dd/MM/yyyy") +
                    " hasta " + dTPAnoFin.Value.Date.ToString("dd/MM/yyyy");

                // Mostrar el informe en el visor
                reporte.Show();
            }
            catch (Exception ex)
            {
                Program.appDAM.RegistrarLog("Informe de facturas emitidas.", ex.Message);
                MessageBox.Show("No se ha podido generar el informe.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
