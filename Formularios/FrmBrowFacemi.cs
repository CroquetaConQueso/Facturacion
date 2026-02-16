using FacturacionDAM.Modelos;
using FacturacionDAM.Utils;
using MySql.Data.MySqlClient;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

/*
 * Módulo: FrmBrowFacemi
 * Propósito: Gestión principal de facturas emitidas. Permite filtrar por cliente y año,
 * realizar operaciones CRUD (Crear, Leer, Actualizar, Borrar), exportar datos y generar
 * diversos tipos de informes (listados anuales, facturas individuales y por cliente).
 */


namespace FacturacionDAM.Formularios
{
    public partial class FrmBrowFacemi : Form
    {
        #region Campos y Constructor

        private Tabla _tablaClientes;
        private Tabla _tablaFacemi;

        private readonly BindingSource _bsClientes = new BindingSource();
        private readonly BindingSource _bsFacturas = new BindingSource();

        private int _idEmisor;
        private int _yearActual;

        public FrmBrowFacemi()
        {
            InitializeComponent();
        }

        #endregion

        #region Inicialización y Configuración

        // Carga inicial: verifica emisor activo, inicializa tablas y configura rejillas.
        private void FrmBrowFacemi_Load(object sender, EventArgs e)
        {
            if (Program.appDAM?.emisor == null)
            {
                MessageBox.Show("No hay emisor activo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            _idEmisor = Program.appDAM.emisor.id;

            _tablaClientes = new Tabla(Program.appDAM.LaConexion);
            _tablaFacemi = new Tabla(Program.appDAM.LaConexion);

            CargarYearsDesdeBD();
            CargarClientes();

            dgClientes.DataSource = _bsClientes;
            dgFacturas.DataSource = _bsFacturas;

            ConfigurarClientes();
            ConfigurarFacturas();

            CargarFacturasClienteSeleccionado();
        }

        private void FrmBrowFacemi_Shown(object sender, EventArgs e)
        {
            ConfiguracionVentana.Restaurar(this, "BrowFacemi");
        }

        private void FrmBrowFacemi_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfiguracionVentana.Guardar(this, "BrowFacemi");
        }

        // Obtiene los años distintos disponibles en las facturas para poblar el filtro temporal.
        private void CargarYearsDesdeBD()
        {
            tsComboYear.Items.Clear();
            var years = new List<int>();

            try
            {
                const string sql = @"
                    SELECT DISTINCT YEAR(fecha) AS anho
                    FROM facemi
                    WHERE idemisor = @idEmisor
                    ORDER BY anho DESC;";

                using var cmd = new MySqlCommand(sql, Program.appDAM.LaConexion);
                cmd.Parameters.AddWithValue("@idEmisor", _idEmisor);

                using var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (!rd.IsDBNull(0))
                        years.Add(rd.GetInt32(0));
                }
            }
            catch
            {
                years.Clear();
            }

            // Si no hay datos, asegurar al menos el año actual para evitar inconsistencias en la UI.
            if (years.Count == 0)
                years.Add(DateTime.Now.Year);

            foreach (var y in years)
                tsComboYear.Items.Add(y);

            tsComboYear.SelectedIndex = 0;
            _yearActual = (int)tsComboYear.SelectedItem;
        }

        private void CargarClientes()
        {
            const string sql = @"
                SELECT id, nombrecomercial, nombre, apellidos, nifcif
                FROM clientes
                ORDER BY nombrecomercial, nombre, apellidos;";

            if (!_tablaClientes.InicializarDatos(sql))
            {
                MessageBox.Show("No se pudieron cargar los clientes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _bsClientes.DataSource = _tablaClientes.LaTabla;
        }

        private void ConfigurarClientes()
        {
            dgClientes.ReadOnly = true;
            dgClientes.MultiSelect = false;
            dgClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgClientes.AllowUserToAddRows = false;
            dgClientes.AllowUserToDeleteRows = false;

            if (dgClientes.Columns.Contains("id"))
                dgClientes.Columns["id"].Visible = false;

            dgClientes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 240, 255, 255);

            foreach (DataGridViewColumn col in dgClientes.Columns)
            {
                switch (col.Name.ToLower())
                {
                    case "nombrecomercial": col.HeaderText = "Nombre Comercial"; break;
                    case "nombre": col.HeaderText = "Nombre"; break;
                    case "apellidos": col.HeaderText = "Apellidos"; break;
                    case "nifcif": col.HeaderText = "NIF/CIF"; break;
                }
            }
        }

        private void ConfigurarFacturas()
        {
            dgFacturas.ReadOnly = true;
            dgFacturas.MultiSelect = false;
            dgFacturas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgFacturas.AllowUserToAddRows = false;
            dgFacturas.AllowUserToDeleteRows = false;

            if (dgFacturas.Columns.Contains("id"))
                dgFacturas.Columns["id"].Visible = false;

            dgFacturas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 240, 255, 255);
        }

        #endregion

        #region Lógica de Carga y Cálculos

        private void dgClientes_SelectionChanged(object sender, EventArgs e)
        {
            CargarFacturasClienteSeleccionado();
        }

        private void tsComboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tsComboYear.SelectedItem is int y)
                _yearActual = y;

            CargarFacturasClienteSeleccionado();
        }

        // Refresca la lista de facturas basándose en el cliente seleccionado en la grilla superior y el año del filtro.
        private void CargarFacturasClienteSeleccionado()
        {
            _bsFacturas.SuspendBinding();
            _bsFacturas.DataSource = null;
            _bsFacturas.ResumeBinding();

            if (_bsClientes.Current is not DataRowView rowCliente)
            {
                // Sin cliente seleccionado, limpiamos vista y mostramos totales globales del año.
                ActualizarEstado(0, ContarTotalesAnho());
                CalcularTotales();
                return;
            }

            int idCliente = Convert.ToInt32(rowCliente["id"]);

            var p = new Dictionary<string, object>
            {
                ["@idEmisor"] = _idEmisor,
                ["@idCliente"] = idCliente,
                ["@year"] = _yearActual
            };

            const string sql = @"
                SELECT f.*
                FROM facemi f
                WHERE f.idemisor = @idEmisor
                  AND f.idcliente = @idCliente
                  AND YEAR(f.fecha) = @year
                ORDER BY f.fecha DESC, f.id DESC;";

            if (!_tablaFacemi.InicializarDatos(sql, p))
            {
                ActualizarEstado(0, ContarTotalesAnho());
                CalcularTotales();
                return;
            }

            dgFacturas.DataSource = null;
            _bsFacturas.DataSource = _tablaFacemi.LaTabla;
            dgFacturas.AutoGenerateColumns = true;
            dgFacturas.DataSource = _bsFacturas;

            ConfigurarCabeceras();

            if (_bsFacturas.Count > 0)
                _bsFacturas.Position = 0;

            ActualizarEstado(_bsFacturas.Count, ContarTotalesAnho());
            CalcularTotales();
        }

        private void ConfigurarCabeceras()
        {
            foreach (DataGridViewColumn col in dgFacturas.Columns)
            {
                switch (col.Name.ToLower())
                {
                    case "idemisor": col.HeaderText = "ID Emisor"; break;
                    case "idcliente": col.HeaderText = "ID Cliente"; break;
                    case "idconceptofac": col.HeaderText = "ID Concepto"; break;
                    case "fecha": col.HeaderText = "Fecha"; break;
                    case "base":
                        col.HeaderText = "Base Imponible";
                        col.DefaultCellStyle.Format = "N2";
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                    case "cuota":
                        col.HeaderText = "Cuota IVA";
                        col.DefaultCellStyle.Format = "N2";
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                    case "total":
                        col.HeaderText = "Total Factura";
                        col.DefaultCellStyle.Format = "N2";
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                    case "pagada": col.HeaderText = "Pagada"; break;
                    case "numero": col.HeaderText = "Número"; break;
                    default:
                        if (col.Name.ToLower().StartsWith("id") && col.Name.Length > 2)
                        {
                            string rest = col.Name.Substring(2);
                            col.HeaderText = "ID " + char.ToUpper(rest[0]) + rest.Substring(1);
                        }
                        else if (!string.IsNullOrEmpty(col.Name))
                        {
                            string header = col.Name.Replace("_", " ");
                            if (header.Length > 0)
                                col.HeaderText = char.ToUpper(header[0]) + header.Substring(1);
                        }
                        break;
                }
            }
        }

        // Suma los importes visibles en el BindingSource para mostrar totales en la barra de estado.
        private void CalcularTotales()
        {
            decimal totalBase = 0;
            decimal totalCuota = 0;
            decimal totalTotal = 0;

            if (_bsFacturas.DataSource is DataTable dt)
            {
                foreach (DataRowView rowView in _bsFacturas)
                {
                    DataRow row = rowView.Row;

                    if (dt.Columns.Contains("base") && row["base"] != DBNull.Value)
                        totalBase += Convert.ToDecimal(row["base"]);

                    if (dt.Columns.Contains("cuota") && row["cuota"] != DBNull.Value)
                        totalCuota += Convert.ToDecimal(row["cuota"]);

                    if (dt.Columns.Contains("total") && row["total"] != DBNull.Value)
                        totalTotal += Convert.ToDecimal(row["total"]);
                }
            }

            if (tsLbTotalBase != null) tsLbTotalBase.Text = $"Totales base: {totalBase:N2}";
            if (tsLbTotalCuota != null) tsLbTotalCuota.Text = $"Totales cuota: {totalCuota:N2}";
            if (tsLbTotalTotal != null) tsLbTotalTotal.Text = $"Totales totales: {totalTotal:N2}";
        }

        // Cuenta el total de facturas del año para comparar con las filtradas por cliente.
        private int ContarTotalesAnho()
        {
            try
            {
                const string sql = @"
                    SELECT COUNT(*)
                    FROM facemi
                    WHERE idemisor = @idEmisor
                      AND YEAR(fecha) = @year;";

                using var cmd = new MySqlCommand(sql, Program.appDAM.LaConexion);
                cmd.Parameters.AddWithValue("@idEmisor", _idEmisor);
                cmd.Parameters.AddWithValue("@year", _yearActual);

                var obj = cmd.ExecuteScalar();
                return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
            }
            catch
            {
                return 0;
            }
        }

        private void ActualizarEstado(int encontrados, int totales)
        {
            if (tsLbNumReg != null)
                tsLbNumReg.Text = $"Nº de registros: {encontrados}";

            if (tsLbStatus != null)
                tsLbStatus.Text = $"Nº de registros totales: {totales}";
        }

        #endregion

        #region Navegación y CRUD

        private void tsBtnFirst_Click(object sender, EventArgs e) => _bsFacturas.MoveFirst();
        private void tsBtnPrev_Click(object sender, EventArgs e) => _bsFacturas.MovePrevious();
        private void tsBtnNext_Click(object sender, EventArgs e) => _bsFacturas.MoveNext();
        private void tsBtnLast_Click(object sender, EventArgs e) => _bsFacturas.MoveLast();

        private void tsBtnNew_Click(object sender, EventArgs e)
        {
            if (_bsClientes.Current is not DataRowView rowCliente) return;

            int idCliente = Convert.ToInt32(rowCliente["id"]);

            using var frm = new FrmFacemi(_bsFacturas, _tablaFacemi, _idEmisor, idCliente, _yearActual, -1);
            if (frm.ShowDialog(this) == DialogResult.OK)
                CargarFacturasClienteSeleccionado();
        }

        private void tsBtnEdit_Click(object sender, EventArgs e)
        {
            if (_bsClientes.Current is not DataRowView rowCliente) return;
            if (_bsFacturas.Current is not DataRowView rowFactura) return;

            int idCliente = Convert.ToInt32(rowCliente["id"]);
            int idFacemi = Convert.ToInt32(rowFactura["id"]);

            using var frm = new FrmFacemi(_bsFacturas, _tablaFacemi, _idEmisor, idCliente, _yearActual, idFacemi);
            if (frm.ShowDialog(this) == DialogResult.OK)
                CargarFacturasClienteSeleccionado();
        }

        private void dgFacturas_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;
            tsBtnEdit_Click(sender, EventArgs.Empty);
        }

        private void tsBtnDelete_Click(object sender, EventArgs e)
        {
            if (_bsFacturas.Current is not DataRowView) return;

            if (MessageBox.Show("¿Eliminar la factura seleccionada?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            _bsFacturas.RemoveCurrent();
            _tablaFacemi.GuardarCambios();
            _tablaFacemi.Refrescar();
            CargarFacturasClienteSeleccionado();
        }

        #endregion

        #region Exportación

        private void tsBtnExportCSV_Click(object sender, EventArgs e)
        {
            if (_bsFacturas.DataSource is not DataTable dt || dt.Rows.Count == 0) return;

            using var sfd = new SaveFileDialog { Filter = "Archivo CSV (*.csv)|*.csv", FileName = "facemi.csv" };
            if (sfd.ShowDialog(this) == DialogResult.OK)
                ExportarDatos.ExportarCSV(dt, sfd.FileName);
        }

        private void tsBtnExportXML_Click(object sender, EventArgs e)
        {
            if (_bsFacturas.DataSource is not DataTable dt || dt.Rows.Count == 0) return;

            using var sfd = new SaveFileDialog { Filter = "Archivo XML (*.xml)|*.xml", FileName = "facemi.xml" };
            if (sfd.ShowDialog(this) == DialogResult.OK)
                ExportarDatos.ExportarXML(dt, sfd.FileName, "Facemi");
        }

        private void tsBtnExportaciones_ButtonClick(object sender, EventArgs e)
        {
            tsBtnExportaciones.ShowDropDown();
        }

        #endregion

        #region Gestión de Informes

        private void btnInforme_Click(object sender, EventArgs e)
        {
            // Abre el formulario de selección de fechas para el informe anual genérico.
            DateTime fechaInicial = new DateTime(_yearActual, 1, 1);
            DateTime fechaFinal = new DateTime(_yearActual, 12, 31);

            using var frm = new FrmInformeFacemiAnual();

            frm.dTPAnoInicio.MinDate = fechaInicial;
            frm.dTPAnoInicio.MaxDate = fechaFinal;
            frm.dTPAnoInicio.Value = fechaInicial;

            frm.dTPAnoFin.MinDate = fechaInicial;
            frm.dTPAnoFin.MaxDate = fechaFinal;
            frm.dTPAnoFin.Value = fechaFinal;

            frm.ShowDialog(this);
        }

        private void listadoDeFacturasTotalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redirige al mismo formulario de selección de fechas que btnInforme.
            DateTime fechaInicial = new DateTime(_yearActual, 1, 1);
            DateTime fechaFinal = new DateTime(_yearActual, 12, 31);

            using var frm = new FrmInformeFacemiAnual();
            frm.dTPAnoInicio.Value = fechaInicial;
            frm.dTPAnoFin.Value = fechaFinal;
            frm.ShowDialog(this);
        }

        // Genera un listado específico para el cliente seleccionado en la rejilla.
        // Se crean fechas por defecto (año completo) y se carga un informe diseñado específicamente para cliente.
        private void listadoAgrupadoPorClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Verificación de integridad: requiere un registro seleccionado en la lista de clientes.
            if (_bsClientes.Current is not DataRowView rowCliente)
            {
                MessageBox.Show("Por favor, selecciona un cliente en la lista superior.");
                return;
            }

            int idCliente = Convert.ToInt32(rowCliente["id"]);
            DateTime fi = new DateTime(_yearActual, 1, 1);
            DateTime ff = new DateTime(_yearActual, 12, 31);

            string nombreMrt = "InformeFacturasCliente.mrt";
            string ruta = Path.Combine(Application.StartupPath, "informes", nombreMrt);

            if (!File.Exists(ruta))
            {
                MessageBox.Show("No encuentro el archivo de reporte: " + ruta);
                return;
            }

            try
            {
                // Obtención del conjunto de datos mediante el método especializado.
                DataSet ds = CreateDataSetFacturasPorCliente(idCliente, fi, ff);

                if (ds.Tables["ListadoFacturasCliente"].Rows.Count == 0)
                {
                    MessageBox.Show("El cliente no tiene facturas para el periodo seleccionado.");
                    return;
                }

                StiReport report = new StiReport();
                report.Load(ruta);

                // Limpieza de orígenes de datos previos para asegurar el uso exclusivo de la RAM.
                report.Dictionary.Databases.Clear();
                report.Dictionary.DataSources.Clear();
                report.RegData(ds);
                report.Dictionary.Synchronize();

                // Asignación de variables para la cabecera dinámica del informe.
                SetVar(report, "NombreCliente", rowCliente["nombrecomercial"].ToString());
                SetVar(report, "NifCliente", rowCliente["nifcif"].ToString());
                SetVar(report, "RangoFechas", $"Ejercicio: {_yearActual}");

                AplicarVariablesEmisorDesdeBD(report);

                report.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el listado por cliente:\n" + ex.Message);
            }
        }

        private void facturaActualConRetencion_Click(object sender, EventArgs e)
        {
            if (!TryGetFacturaActual(out int idFactura))
            {
                MessageBox.Show("Selecciona una factura primero.");
                return;
            }

            try
            {
                var ds = CrearDataSetFactura(idFactura);
                MostrarInforme("FacturaConRetencion.mrt", ds, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void facturaActualSinRetencion_Click(object sender, EventArgs e)
        {
            if (!TryGetFacturaActual(out int idFactura))
            {
                MessageBox.Show("Selecciona una factura primero.");
                return;
            }

            try
            {
                var ds = CrearDataSetFactura(idFactura);
                MostrarInforme("FacturaSinRetencion.mrt", ds, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar informe: " + ex.Message);
            }
        }

        #region Métodos Auxiliares DataSet e Informes

        private bool TryGetFacturaActual(out int idFactura)
        {
            idFactura = -1;
            if (_bsFacturas.Current is not DataRowView rowFactura) return false;
            idFactura = Convert.ToInt32(rowFactura["id"]);
            return idFactura > 0;
        }

        private DataSet CrearDataSetFactura(int idFactura)
        {
            var ds = new DataSet("DatosFactura");
            var p = new Dictionary<string, object> { ["@id"] = idFactura };

            // Consulta de Cabecera
            string sqlCabecera = @"
                SELECT 
                    f.id,
                    f.numero,
                    f.fecha,
                    f.base,
                    f.cuota,
                    f.retencion,
                    (f.base + f.cuota - IFNULL(f.retencion, 0)) AS total,
                    (f.base + f.cuota) AS totalSinRetencion, 
                    c.nombrecomercial,
                    c.nifcif,
                    c.direccion,
                    c.poblacion,
                    c.cpostal AS codigopostal,
                    c.idprovincia AS provincia
                FROM facemi f
                LEFT JOIN clientes c ON c.id = f.idcliente
                WHERE f.id = @id;";

            var tCabecera = new Tabla(Program.appDAM.LaConexion);

            if (!tCabecera.InicializarDatos(sqlCabecera, p) || tCabecera.LaTabla.Rows.Count == 0)
                throw new InvalidOperationException($"No se ha podido cargar la cabecera de la factura {idFactura}.");

            var dtCabecera = tCabecera.LaTabla.Copy();
            dtCabecera.TableName = "Cabecera";
            ds.Tables.Add(dtCabecera);

            // Consulta de Líneas
            string sqlLineas = @"
                SELECT 
                    l.id,
                    l.idfacemi,
                    l.descripcion,
                    l.cantidad,
                    l.precio,
                    l.tipoiva,
                    (l.base * (IFNULL(l.tipoiva, 0) / 100.0)) AS cuota,
                    l.base,
                    (l.base * (IFNULL(l.tipoiva, 0) / 100.0) + l.base) AS total
                FROM facemilin l 
                WHERE l.idfacemi = @id
                ORDER BY l.id;";

            var tLineas = new Tabla(Program.appDAM.LaConexion);
            tLineas.InicializarDatos(sqlLineas, p);

            var dtLineas = tLineas.LaTabla.Copy();
            dtLineas.TableName = "Lineas";
            ds.Tables.Add(dtLineas);

            if (dtCabecera.Columns.Contains("id") && dtLineas.Columns.Contains("idfacemi"))
            {
                if (!ds.Relations.Contains("Cabecera_Lineas"))
                    ds.Relations.Add("Cabecera_Lineas", dtCabecera.Columns["id"], dtLineas.Columns["idfacemi"], false);
            }

            return ds;
        }

        // Construye el DataSet específico para el listado por cliente.
        // Realiza conversiones manuales de tipos (String para número, Bool para pagada)
        // para garantizar compatibilidad con Stimulsoft.
        private DataSet CreateDataSetFacturasPorCliente(int idCliente, DateTime fi, DateTime ff)
        {
            DataSet ds = new DataSet("ReportData");

            // Consulta SQL con alias que mapean directamente a los campos del diseño del informe.
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
                ["@idEmisor"] = _idEmisor,
                ["@idCliente"] = idCliente,
                ["@fi"] = fi.Date,
                ["@ff"] = ff.Date
            };

            var t = new Tabla(Program.appDAM.LaConexion);

            // Definición manual del esquema de la tabla para forzar tipos de datos correctos.
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

            // Uso del método existente InicializarDatos para cargar la tabla origen.
            if (t.InicializarDatos(sql, p))
            {
                foreach (DataRow r in t.LaTabla.Rows)
                {
                    DataRow nr = dtTyped.NewRow();

                    nr["Id"] = r["Id"] != DBNull.Value ? Convert.ToInt32(r["Id"]) : 0;
                    nr["NumeroFactura"] = r["NumeroFactura"]?.ToString() ?? "";
                    nr["FechaEmision"] = r["FechaEmision"] != DBNull.Value ? Convert.ToDateTime(r["FechaEmision"]) : DateTime.MinValue;
                    nr["Descripcion"] = r["Descripcion"]?.ToString() ?? "";
                    nr["BaseImponible"] = r["BaseImponible"] != DBNull.Value ? Convert.ToDecimal(r["BaseImponible"]) : 0m;
                    nr["CuotaIVA"] = r["CuotaIVA"] != DBNull.Value ? Convert.ToDecimal(r["CuotaIVA"]) : 0m;
                    nr["RetencionIRPF"] = r["RetencionIRPF"] != DBNull.Value ? Convert.ToDecimal(r["RetencionIRPF"]) : 0m;
                    nr["TotalPagar"] = r["TotalPagar"] != DBNull.Value ? Convert.ToDecimal(r["TotalPagar"]) : 0m;

                    // Conversión lógica: MySQL TINYINT (0/1) se traduce a Booleano real de C#.
                    nr["Pagada"] = r["Pagada"] != DBNull.Value && Convert.ToInt32(r["Pagada"]) == 1;

                    dtTyped.Rows.Add(nr);
                }
            }

            ds.Tables.Add(dtTyped);
            return ds;
        }

        private DataSet CreateDataSetListadoFacturasEmitidas(DateTime fi, DateTime ff, bool ordenarPorCliente)
        {
            DataSet ds = new DataSet("DS_ListadoFacturas");
            string orderBy = ordenarPorCliente ? "c.nombrecomercial, f.fecha, f.numero" : "f.fecha DESC, f.numero DESC";

            string sql = @"
                SELECT
                    f.id AS Id,
                    f.numero AS Numero,
                    f.fecha AS Fecha,
                    c.nombrecomercial AS NombreRazonSocial,
                    c.nifcif AS Nif,
                    f.base AS BaseImponible,
                    f.retencion AS RetencionIRPF,
                    f.cuota AS CuotaIVA,
                    f.total AS Total,
                    f.pagada AS Pagada
                FROM facemi f
                LEFT JOIN clientes c ON f.idcliente = c.id
                WHERE f.idemisor = @idEmisor
                  AND f.fecha BETWEEN @fi AND @ff
                ORDER BY " + orderBy + ";";

            var p = new Dictionary<string, object>
            {
                ["@idEmisor"] = _idEmisor,
                ["@fi"] = fi.Date,
                ["@ff"] = ff.Date
            };

            var t = new Tabla(Program.appDAM.LaConexion);
            if (t.InicializarDatos(sql, p))
            {
                var dt = t.LaTabla.Copy();
                dt.TableName = "FacturasEmitidas";
                ds.Tables.Add(dt);
            }

            return ds;
        }

        private void MostrarInforme(string nombreMrt, DataSet ds, Dictionary<string, string> variables)
        {
            var report = new StiReport();
            string ruta = Path.Combine(Application.StartupPath, "informes", nombreMrt);

            if (!File.Exists(ruta))
            {
                MessageBox.Show("No encuentro el archivo: " + ruta);
                return;
            }

            try
            {
                report.Load(ruta);

                report.Dictionary.Databases.Clear();
                report.RegData(ds);
                report.Dictionary.Synchronize();

                if (variables != null)
                {
                    foreach (var kv in variables)
                    {
                        if (report.Dictionary.Variables.Contains(kv.Key))
                            report.Dictionary.Variables[kv.Key].Value = kv.Value ?? "";
                    }
                }

                AplicarVariablesEmisorDesdeBD(report);

                report.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar el informe:\n" + ex.Message);
            }
        }

        // Obtiene datos del emisor activo desde BD y los inyecta como variables en el reporte.
        private void AplicarVariablesEmisorDesdeBD(StiReport report)
        {
            if (report == null) return;
            if (_idEmisor <= 0) return;

            string nombre = Program.appDAM?.emisor?.nombreComercial ?? "";
            string nif = Program.appDAM?.emisor?.nifcif ?? "";

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

                var p = new Dictionary<string, object> { ["@id"] = _idEmisor };
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
            catch
            {
                // Fallo no crítico; continuamos con valores vacíos.
            }

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
        }

        #endregion

        #endregion
    }
}