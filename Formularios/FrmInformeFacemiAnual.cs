using FacturacionDAM.Modelos;
using FacturacionDAM.Utils;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace FacturacionDAM.Formularios
{
    // Define el tipo de informe anual a generar.
    public enum ModoReporte
    {
        General,
        Agrupado,
        UnCliente
    }

    public partial class FrmInformeFacemiAnual : Form
    {
        // Estado del formulario (modo actual y, si aplica, cliente concreto).
        private ModoReporte _modo;
        private int _idCliente = -1;
        private string _nombreCliente = "";
        private string _nifCliente = "";

        // Modo por defecto: listado general.
        public FrmInformeFacemiAnual()
        {
            InitializeComponent();
            _modo = ModoReporte.General;
            this.Text = "Listado General de Facturas Emitidas";
        }

        // Permite abrir el formulario en modo General o Agrupado.
        public FrmInformeFacemiAnual(ModoReporte modo)
        {
            InitializeComponent();
            _modo = modo;

            if (_modo == ModoReporte.Agrupado)
                this.Text = "Listado de Facturas Agrupadas por Cliente";
            else
                this.Text = "Listado General de Facturas Emitidas";
        }

        // Modo específico para listar facturas de un solo cliente.
        public FrmInformeFacemiAnual(int idCliente, string nombreCliente, string nifCliente)
        {
            InitializeComponent();
            _modo = ModoReporte.UnCliente;
            _idCliente = idCliente;
            _nombreCliente = nombreCliente;
            _nifCliente = nifCliente;

            this.Text = $"Listado de Facturas: {_nombreCliente}";
        }

        private void btnInforme_Click(object sender, EventArgs e)
        {
            DateTime fi = dTPAnoInicio.Value;
            DateTime ff = dTPAnoFin.Value;

            // Selección de plantilla según modo.
            string nombreMrt = "InformeFacemiList1.mrt";

            if (_modo == ModoReporte.Agrupado)
                nombreMrt = "InformeFacturasAgrupadas.mrt";
            else if (_modo == ModoReporte.UnCliente)
                nombreMrt = "InformeFacturasCliente.mrt";

            string ruta = Path.Combine(Application.StartupPath, "informes", nombreMrt);

            if (!File.Exists(ruta))
            {
                MessageBox.Show("No encuentro el archivo de reporte: " + ruta);
                return;
            }

            try
            {
                DataSet ds;

                // Construye el DataSet adecuado y valida que haya datos.
                if (_modo == ModoReporte.UnCliente)
                {
                    ds = CreateDataSetFacturasPorCliente(_idCliente, fi, ff);

                    if (ds.Tables["ListadoFacturasCliente"].Rows.Count == 0)
                    {
                        MessageBox.Show("No hay facturas para el cliente seleccionado en este rango de fechas.");
                        return;
                    }
                }
                else
                {
                    bool agrupar = (_modo == ModoReporte.Agrupado);
                    ds = CreateDataSetListadoFacturasEmitidas(fi, ff, agrupar);

                    if (ds.Tables["ListadoFacturasEmitidas"].Rows.Count == 0)
                    {
                        MessageBox.Show("No hay facturas en el rango seleccionado.");
                        return;
                    }
                }

                // Carga del reporte y registro de datos.
                StiReport report = new StiReport();
                report.Load(ruta);

                report.Dictionary.Databases.Clear();
                report.Dictionary.DataSources.Clear();
                report.RegData(ds);
                report.Dictionary.Synchronize();

                // Fechas como texto para evitar mostrar hora en el informe.
                string strFechaInicio = fi.ToString("dd/MM/yyyy");
                string strFechaFin = ff.ToString("dd/MM/yyyy");

                SetVar(report, "FechaInicio", strFechaInicio);
                SetVar(report, "FechaFin", strFechaFin);

                // Variables extra cuando el informe es de cliente único.
                if (_modo == ModoReporte.UnCliente)
                {
                    SetVar(report, "NombreCliente", _nombreCliente);
                    SetVar(report, "NifCliente", _nifCliente);
                }

                // Variables del emisor (cabecera del informe).
                AplicarVariablesEmisorDesdeBD(report);

                report.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el informe:\n" + ex.Message);
            }
        }

        private DataSet CreateDataSetListadoFacturasEmitidas(DateTime fi, DateTime ff, bool agrupado)
        {
            DataSet ds = new DataSet("ReportData");

            // Si se agrupa por cliente, el orden debe empezar por cliente.
            string orderBy = agrupado
                ? "c.nombrecomercial ASC, f.fecha DESC, f.numero DESC"
                : "f.fecha DESC, f.numero DESC";

            string sql = $@"
            SELECT 
                f.id AS Id,
                f.numero AS NumeroFactura,         
                f.fecha AS FechaEmision,           
                c.nombrecomercial AS NombreCliente,
                c.nifcif AS NifCliente,            
                f.base AS BaseImponible,           
                f.cuota AS CuotaIVA,               
                f.retencion AS RetencionIRPF,      
                f.total AS TotalPagar,             
                f.pagada AS Pagada                 
            FROM facemi f
            LEFT JOIN clientes c ON f.idcliente = c.id
            WHERE f.idemisor = @idEmisor
              AND f.fecha BETWEEN @fi AND @ff
            ORDER BY {orderBy};";

            var p = new Dictionary<string, object>
            {
                ["@idEmisor"] = Program.appDAM.emisor.id,
                ["@fi"] = fi.Date,
                ["@ff"] = ff.Date
            };

            var t = new Tabla(Program.appDAM.LaConexion);

            // Tabla tipada para que Stimulsoft reciba tipos consistentes.
            DataTable dtTyped = new DataTable("ListadoFacturasEmitidas");
            dtTyped.Columns.Add("Id", typeof(int));
            dtTyped.Columns.Add("NumeroFactura", typeof(string));
            dtTyped.Columns.Add("FechaEmision", typeof(DateTime));
            dtTyped.Columns.Add("NombreCliente", typeof(string));
            dtTyped.Columns.Add("NifCliente", typeof(string));
            dtTyped.Columns.Add("BaseImponible", typeof(decimal));
            dtTyped.Columns.Add("CuotaIVA", typeof(decimal));
            dtTyped.Columns.Add("RetencionIRPF", typeof(decimal));
            dtTyped.Columns.Add("TotalPagar", typeof(decimal));
            dtTyped.Columns.Add("Pagada", typeof(bool));

            if (t.InicializarDatos(sql, p))
            {
                foreach (DataRow rowRaw in t.LaTabla.Rows)
                {
                    DataRow newRow = dtTyped.NewRow();

                    newRow["Id"] = rowRaw["Id"] != DBNull.Value ? Convert.ToInt32(rowRaw["Id"]) : 0;
                    newRow["NumeroFactura"] = rowRaw["NumeroFactura"] != DBNull.Value ? rowRaw["NumeroFactura"].ToString() : "";
                    newRow["FechaEmision"] = rowRaw["FechaEmision"] != DBNull.Value ? Convert.ToDateTime(rowRaw["FechaEmision"]) : DateTime.MinValue;
                    newRow["NombreCliente"] = rowRaw["NombreCliente"]?.ToString() ?? "";
                    newRow["NifCliente"] = rowRaw["NifCliente"]?.ToString() ?? "";
                    newRow["BaseImponible"] = rowRaw["BaseImponible"] != DBNull.Value ? Convert.ToDecimal(rowRaw["BaseImponible"]) : 0m;
                    newRow["CuotaIVA"] = rowRaw["CuotaIVA"] != DBNull.Value ? Convert.ToDecimal(rowRaw["CuotaIVA"]) : 0m;
                    newRow["RetencionIRPF"] = rowRaw["RetencionIRPF"] != DBNull.Value ? Convert.ToDecimal(rowRaw["RetencionIRPF"]) : 0m;
                    newRow["TotalPagar"] = rowRaw["TotalPagar"] != DBNull.Value ? Convert.ToDecimal(rowRaw["TotalPagar"]) : 0m;

                    // Normaliza el valor numérico de BD a bool.
                    int valorPagada = 0;
                    if (rowRaw["Pagada"] != DBNull.Value)
                    {
                        valorPagada = Convert.ToInt32(rowRaw["Pagada"]);
                    }
                    newRow["Pagada"] = (valorPagada == 1);

                    dtTyped.Rows.Add(newRow);
                }
            }

            ds.Tables.Add(dtTyped);
            return ds;
        }

        private DataSet CreateDataSetFacturasPorCliente(int idCliente, DateTime fi, DateTime ff)
        {
            DataSet ds = new DataSet("ReportData");

            string sql = @"
                SELECT 
                    f.id AS Id,
                    f.numero AS NumeroFactura,          
                    f.fecha AS FechaEmision,            
                    f.descripcion AS Descripcion,       
                    f.base AS BaseImponible,            
                    f.cuota AS CuotaIVA,                
                    f.retencion AS RetencionIRPF,       
                    f.total AS TotalPagar,              
                    f.pagada AS Pagada                  
                FROM facemi f
                WHERE f.idemisor = @idEmisor
                  AND f.idcliente = @idCliente
                  AND f.fecha BETWEEN @fi AND @ff
                ORDER BY f.fecha DESC, f.numero DESC;";

            var p = new Dictionary<string, object>
            {
                ["@idEmisor"] = Program.appDAM.emisor.id,
                ["@idCliente"] = idCliente,
                ["@fi"] = fi.Date,
                ["@ff"] = ff.Date
            };

            var t = new Tabla(Program.appDAM.LaConexion);

            // Tabla tipada para el informe por cliente.
            DataTable dtTyped = new DataTable("ListadoFacturasCliente");
            dtTyped.Columns.Add("Id", typeof(int));
            dtTyped.Columns.Add("NumeroFactura", typeof(string));
            dtTyped.Columns.Add("FechaEmision", typeof(DateTime));
            dtTyped.Columns.Add("Descripcion", typeof(string));
            dtTyped.Columns.Add("BaseImponible", typeof(decimal));
            dtTyped.Columns.Add("CuotaIVA", typeof(decimal));
            dtTyped.Columns.Add("RetencionIRPF", typeof(decimal));
            dtTyped.Columns.Add("TotalPagar", typeof(decimal));
            dtTyped.Columns.Add("Pagada", typeof(bool));

            if (t.InicializarDatos(sql, p))
            {
                foreach (DataRow rowRaw in t.LaTabla.Rows)
                {
                    DataRow newRow = dtTyped.NewRow();

                    newRow["Id"] = rowRaw["Id"] != DBNull.Value ? Convert.ToInt32(rowRaw["Id"]) : 0;
                    newRow["NumeroFactura"] = rowRaw["NumeroFactura"] != DBNull.Value ? rowRaw["NumeroFactura"].ToString() : "";
                    newRow["FechaEmision"] = rowRaw["FechaEmision"] != DBNull.Value ? Convert.ToDateTime(rowRaw["FechaEmision"]) : DateTime.MinValue;
                    newRow["Descripcion"] = rowRaw["Descripcion"] != DBNull.Value ? rowRaw["Descripcion"].ToString() : "";
                    newRow["BaseImponible"] = rowRaw["BaseImponible"] != DBNull.Value ? Convert.ToDecimal(rowRaw["BaseImponible"]) : 0m;
                    newRow["CuotaIVA"] = rowRaw["CuotaIVA"] != DBNull.Value ? Convert.ToDecimal(rowRaw["CuotaIVA"]) : 0m;
                    newRow["RetencionIRPF"] = rowRaw["RetencionIRPF"] != DBNull.Value ? Convert.ToDecimal(rowRaw["RetencionIRPF"]) : 0m;
                    newRow["TotalPagar"] = rowRaw["TotalPagar"] != DBNull.Value ? Convert.ToDecimal(rowRaw["TotalPagar"]) : 0m;

                    int valorPagada = 0;
                    if (rowRaw["Pagada"] != DBNull.Value)
                        valorPagada = Convert.ToInt32(rowRaw["Pagada"]);

                    newRow["Pagada"] = (valorPagada == 1);

                    dtTyped.Rows.Add(newRow);
                }
            }

            ds.Tables.Add(dtTyped);
            return ds;
        }

        private void AplicarVariablesEmisorDesdeBD(StiReport report)
        {
            if (report == null) return;
            int idEmisor = Program.appDAM.emisor.id;

            string nombre = "";
            string nif = "";
            string domicilio = "";
            string cp = "";
            string poblacion = "";
            string telefono1 = "";
            string telefono2 = "";
            string email = "";

            try
            {
                const string sql = @"
SELECT nombrecomercial, nifcif, domicilio, codigopostal, poblacion, telefono1, telefono2, email
FROM emisores
WHERE id = @id;";

                var p = new Dictionary<string, object> { ["@id"] = idEmisor };
                var t = new Tabla(Program.appDAM.LaConexion);

                // Lee datos del emisor para rellenar cabecera/pie del informe.
                if (t.InicializarDatos(sql, p) && t.LaTabla.Rows.Count > 0)
                {
                    var r = t.LaTabla.Rows[0];
                    if (r.Table.Columns.Contains("nombrecomercial") && r["nombrecomercial"] != DBNull.Value) nombre = r["nombrecomercial"].ToString();
                    if (r.Table.Columns.Contains("nifcif") && r["nifcif"] != DBNull.Value) nif = r["nifcif"].ToString();
                    if (r.Table.Columns.Contains("domicilio") && r["domicilio"] != DBNull.Value) domicilio = r["domicilio"].ToString();
                    if (r.Table.Columns.Contains("codigopostal") && r["codigopostal"] != DBNull.Value) cp = r["codigopostal"].ToString();
                    if (r.Table.Columns.Contains("poblacion") && r["poblacion"] != DBNull.Value) poblacion = r["poblacion"].ToString();
                    if (r.Table.Columns.Contains("telefono1") && r["telefono1"] != DBNull.Value) telefono1 = r["telefono1"].ToString();
                    if (r.Table.Columns.Contains("telefono2") && r["telefono2"] != DBNull.Value) telefono2 = r["telefono2"].ToString();
                    if (r.Table.Columns.Contains("email") && r["email"] != DBNull.Value) email = r["email"].ToString();
                }
            }
            catch
            {
                // Si falla la lectura, el informe sigue saliendo con variables vacías.
            }

            // Se cargan alias duplicados para compatibilidad entre plantillas .mrt.
            SetVar(report, "nombreEmisor", nombre);
            SetVar(report, "nifEmisor", nif);
            SetVar(report, "direccionEmisor", domicilio);
            SetVar(report, "domicilioEmisor", domicilio);
            SetVar(report, "cpEmisor", cp);
            SetVar(report, "codigopostalEmisor", cp);
            SetVar(report, "poblacionEmisor", poblacion);
            SetVar(report, "telefono1Emisor", telefono1);
            SetVar(report, "telefono2Emisor", telefono2);
            SetVar(report, "emailEmisor", email);
        }

        private void SetVar(StiReport report, string nombre, string valor)
        {
            // Upsert de variable de reporte: actualiza si existe, crea si no.
            if (report.Dictionary.Variables.Contains(nombre))
                report.Dictionary.Variables[nombre].Value = valor ?? "";
            else
                report.Dictionary.Variables.Add(nombre, valor ?? "");
        }
    }
}