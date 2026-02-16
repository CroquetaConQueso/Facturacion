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
    public partial class FrmInformeFacemiAnual : Form
    {
        public FrmInformeFacemiAnual()
        {
            InitializeComponent();
        }

        private void btnInforme_Click(object sender, EventArgs e)
        {
            DateTime fi = dTPAnoInicio.Value;
            DateTime ff = dTPAnoFin.Value;

            string nombreMrt = "InformeFacemiList1.mrt";
            string ruta = Path.Combine(Application.StartupPath, "informes", nombreMrt);

            if (!File.Exists(ruta))
            {
                MessageBox.Show("No encuentro el archivo de reporte: " + ruta);
                return;
            }

            try
            {
                // 1. Crear el DataSet
                DataSet ds = CreateDataSetListadoFacturasEmitidas(fi, ff);

                if (ds.Tables["ListadoFacturasEmitidas"].Rows.Count == 0)
                {
                    MessageBox.Show("No hay facturas en el rango seleccionado.");
                    return;
                }

                // 2. Cargar Reporte
                StiReport report = new StiReport();
                report.Load(ruta);

                // 3. Limpieza y Vinculación de Datos
                report.Dictionary.Databases.Clear();
                report.Dictionary.DataSources.Clear();
                report.RegData(ds);
                report.Dictionary.Synchronize();

                // 4. ENVÍO DE VARIABLES DE FECHA (SOLUCIÓN A TU PREGUNTA)
                // Enviamos las fechas formateadas como texto para evitar horas '00:00:00'
                // Esta lógica crea la variable si no existe, o la actualiza si ya está creada.

                string strFechaInicio = fi.ToString("dd/MM/yyyy");
                string strFechaFin = ff.ToString("dd/MM/yyyy");

                // Asignar o Crear variable FechaInicio
                if (report.Dictionary.Variables.Contains("FechaInicio"))
                    report.Dictionary.Variables["FechaInicio"].Value = strFechaInicio;
                else
                    report.Dictionary.Variables.Add("FechaInicio", strFechaInicio);

                // Asignar o Crear variable FechaFin
                if (report.Dictionary.Variables.Contains("FechaFin"))
                    report.Dictionary.Variables["FechaFin"].Value = strFechaFin;
                else
                    report.Dictionary.Variables.Add("FechaFin", strFechaFin);

                // 5. Cargar datos del Emisor
                AplicarVariablesEmisorDesdeBD(report);

                // 6. Mostrar
                report.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el informe:\n" + ex.Message);
            }
        }

        private DataSet CreateDataSetListadoFacturasEmitidas(DateTime fi, DateTime ff)
        {
            DataSet ds = new DataSet("ReportData");

            string sql = @"
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
            ORDER BY f.fecha DESC, f.numero DESC;";

            var p = new Dictionary<string, object>
            {
                ["@idEmisor"] = Program.appDAM.emisor.id,
                ["@fi"] = fi.Date,
                ["@ff"] = ff.Date
            };

            var t = new Tabla(Program.appDAM.LaConexion);

            // Definición manual de tipos
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
                    newRow["NombreCliente"] = rowRaw["NombreCliente"].ToString();
                    newRow["NifCliente"] = rowRaw["NifCliente"].ToString();
                    newRow["BaseImponible"] = rowRaw["BaseImponible"] != DBNull.Value ? Convert.ToDecimal(rowRaw["BaseImponible"]) : 0m;
                    newRow["CuotaIVA"] = rowRaw["CuotaIVA"] != DBNull.Value ? Convert.ToDecimal(rowRaw["CuotaIVA"]) : 0m;
                    newRow["RetencionIRPF"] = rowRaw["RetencionIRPF"] != DBNull.Value ? Convert.ToDecimal(rowRaw["RetencionIRPF"]) : 0m;
                    newRow["TotalPagar"] = rowRaw["TotalPagar"] != DBNull.Value ? Convert.ToDecimal(rowRaw["TotalPagar"]) : 0m;

                    // Conversión booleana
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
            catch { }

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
            if (report.Dictionary.Variables.Contains(nombre))
                report.Dictionary.Variables[nombre].Value = valor ?? "";
            else
                report.Dictionary.Variables.Add(nombre, valor ?? "");
        }
    }
}