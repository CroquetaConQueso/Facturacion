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
 * realizar operaciones CRUD, exportar datos y generar informes (anuales, por cliente,
 * y facturas individuales con/sin retención).
 */

namespace FacturacionDAM.Formularios
{
    public partial class FrmBrowFacemi : Form
    {
        #region Campos y Constructor

        // Acceso a datos tabular (carga/edición/persistencia) para clientes y facturas.
        private Tabla _tablaClientes;
        private Tabla _tablaFacemi;

        // BindingSource para desacoplar DataGridView del DataTable y facilitar refrescos.
        private readonly BindingSource _bsClientes = new BindingSource();
        private readonly BindingSource _bsFacturas = new BindingSource();

        // Contexto de trabajo (emisor y año seleccionado en la UI).
        private int _idEmisor;
        private int _yearActual;

        public FrmBrowFacemi()
        {
            InitializeComponent();
        }

        #endregion

        #region Inicialización y Configuración

        // Entrada principal del formulario: valida emisor activo, inicializa datos y deja UI lista.
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

        // Restaura tamaño/posición del formulario desde configuración persistida.
        private void FrmBrowFacemi_Shown(object sender, EventArgs e)
        {
            ConfiguracionVentana.Restaurar(this, "BrowFacemi");
        }

        // Guarda tamaño/posición del formulario al cerrar.
        private void FrmBrowFacemi_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfiguracionVentana.Guardar(this, "BrowFacemi");
        }

        // Construye el filtro de años según las facturas disponibles del emisor.
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

            // Fallback: evita UI sin opciones cuando aún no hay facturas.
            if (years.Count == 0)
                years.Add(DateTime.Now.Year);

            foreach (var y in years)
                tsComboYear.Items.Add(y);

            tsComboYear.SelectedIndex = 0;
            _yearActual = (int)tsComboYear.SelectedItem;
        }

        // Carga el listado completo de clientes (para seleccionar y filtrar facturas).
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

        // Configura la rejilla de clientes: solo lectura, selección por fila y headers.
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

        // Configura la rejilla de facturas (reglas base; las cabeceras se ajustan tras cargar datos).
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

        // Cambio de cliente: refresca facturas del cliente seleccionado.
        private void dgClientes_SelectionChanged(object sender, EventArgs e)
        {
            CargarFacturasClienteSeleccionado();
        }

        // Cambio de año: refresca facturas aplicando el filtro temporal.
        private void tsComboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tsComboYear.SelectedItem is int y)
                _yearActual = y;

            CargarFacturasClienteSeleccionado();
        }

        // Carga las facturas del cliente seleccionado y del año actual; recalcula estado y totales.
        private void CargarFacturasClienteSeleccionado()
        {
            // Limpieza defensiva: evita que la grilla quede enlazada a datos antiguos.
            _bsFacturas.SuspendBinding();
            _bsFacturas.DataSource = null;
            _bsFacturas.ResumeBinding();

            if (_bsClientes.Current is not DataRowView rowCliente)
            {
                // Sin selección: mantenemos coherencia mostrando totales globales del año.
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

            // Reenlace limpio para evitar problemas de autogeneración/orden de columnas.
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

        // Ajusta cabeceras, formatos y visibilidad según el esquema real cargado en la rejilla.
        private void ConfigurarCabeceras()
        {
            foreach (DataGridViewColumn col in dgFacturas.Columns)
            {
                if (col.Name.ToLower().StartsWith("id"))
                {
                    col.Visible = false;
                    continue;
                }

                switch (col.Name.ToLower())
                {
                    case "fecha":
                        col.HeaderText = "Fecha";
                        break;

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

                    case "pagada":
                        col.HeaderText = "Pagada";
                        break;

                    case "numero":
                        col.HeaderText = "Número";
                        break;

                    default:
                        // Header genérico: mejora presentación cuando se autogeneran columnas.
                        if (!string.IsNullOrEmpty(col.Name))
                        {
                            string header = col.Name.Replace("_", " ");
                            if (header.Length > 0)
                                col.HeaderText = char.ToUpper(header[0]) + header.Substring(1);
                        }
                        break;
                }
            }
        }

        // Calcula importes agregados de la lista visible (filtrada por cliente/año).
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

        // Total anual (sin filtro de cliente), usado como contexto comparativo en la barra de estado.
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

        // Actualiza la barra de estado con conteos (filtrado vs total anual).
        private void ActualizarEstado(int encontrados, int totales)
        {
            if (tsLbNumReg != null)
                tsLbNumReg.Text = $"Nº de registros: {encontrados}";

            if (tsLbStatus != null)
                tsLbStatus.Text = $"Nº de registros totales: {totales}";
        }

        #endregion

        #region Navegación y CRUD

        // Navegación del BindingSource para moverse por la selección actual.
        private void tsBtnFirst_Click(object sender, EventArgs e) => _bsFacturas.MoveFirst();
        private void tsBtnPrev_Click(object sender, EventArgs e) => _bsFacturas.MovePrevious();
        private void tsBtnNext_Click(object sender, EventArgs e) => _bsFacturas.MoveNext();
        private void tsBtnLast_Click(object sender, EventArgs e) => _bsFacturas.MoveLast();

        // Alta: abre el formulario editor en modo nuevo con el cliente/año actuales.
        private void tsBtnNew_Click(object sender, EventArgs e)
        {
            if (_bsClientes.Current is not DataRowView rowCliente) return;

            int idCliente = Convert.ToInt32(rowCliente["id"]);

            using var frm = new FrmFacemi(_bsFacturas, _tablaFacemi, _idEmisor, idCliente, _yearActual, -1);
            if (frm.ShowDialog(this) == DialogResult.OK)
                CargarFacturasClienteSeleccionado();
        }

        // Edición: abre el formulario editor sobre la factura seleccionada.
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

        // Acceso rápido: doble click abre edición.
        private void dgFacturas_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;
            tsBtnEdit_Click(sender, EventArgs.Empty);
        }

        // Borrado: elimina la factura actual del BindingSource y persiste cambios.
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

        // Exporta el dataset visible a CSV (sin tocar estado de BD).
        private void tsBtnExportCSV_Click(object sender, EventArgs e)
        {
            if (_bsFacturas.DataSource is not DataTable dt || dt.Rows.Count == 0) return;

            using var sfd = new SaveFileDialog { Filter = "Archivo CSV (*.csv)|*.csv", FileName = "facemi.csv" };
            if (sfd.ShowDialog(this) == DialogResult.OK)
                ExportarDatos.ExportarCSV(dt, sfd.FileName);
        }

        // Exporta el dataset visible a XML.
        private void tsBtnExportXML_Click(object sender, EventArgs e)
        {
            if (_bsFacturas.DataSource is not DataTable dt || dt.Rows.Count == 0) return;

            using var sfd = new SaveFileDialog { Filter = "Archivo XML (*.xml)|*.xml", FileName = "facemi.xml" };
            if (sfd.ShowDialog(this) == DialogResult.OK)
                ExportarDatos.ExportarXML(dt, sfd.FileName, "Facemi");
        }

        // UI: despliega el menú de exportaciones en la ToolStrip.
        private void tsBtnExportaciones_ButtonClick(object sender, EventArgs e)
        {
            tsBtnExportaciones.ShowDropDown();
        }

        #endregion

        #region Gestión de Informes

        // Informe anual genérico: delega en un formulario de selección de fechas.
        private void btnInforme_Click(object sender, EventArgs e)
        {
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

        // Entrada alternativa al informe anual, reutiliza el mismo flujo que btnInforme.
        private void listadoDeFacturasTotalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime fechaInicial = new DateTime(_yearActual, 1, 1);
            DateTime fechaFinal = new DateTime(_yearActual, 12, 31);

            using var frm = new FrmInformeFacemiAnual();
            frm.dTPAnoInicio.Value = fechaInicial;
            frm.dTPAnoFin.Value = fechaFinal;
            frm.ShowDialog(this);
        }

        // Listado por cliente seleccionado: genera dataset tipado y lanza un .mrt específico.
        private void listadoAgrupadoPorClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_bsClientes.Current is not DataRowView rowCliente)
            {
                MessageBox.Show("Por favor, selecciona un cliente en la lista superior.");
                return;
            }

            int idCliente = Convert.ToInt32(rowCliente["id"]);
            string nombreCliente = rowCliente["nombrecomercial"].ToString();
            string nifCliente = rowCliente["nifcif"].ToString();

            DateTime fi = new DateTime(_yearActual, 1, 1);
            DateTime ff = new DateTime(_yearActual, 12, 31);

            string nombreMrt = "InformeFacturasCliente.mrt";
            string ruta = Path.Combine(Application.StartupPath, "informes", nombreMrt);

            if (!File.Exists(ruta))
            {
                MessageBox.Show("No encuentro el archivo de reporte: " + ruta + "\n\nDebe crearse con el diseñador.");
                return;
            }

            try
            {
                DataSet ds = CreateDataSetFacturasPorCliente(idCliente, fi, ff);

                if (ds.Tables["ListadoFacturasCliente"].Rows.Count == 0)
                {
                    MessageBox.Show($"El cliente {nombreCliente} no tiene facturas en el año {_yearActual}.");
                    return;
                }

                StiReport report = new StiReport();
                report.Load(ruta);

                // Evita conexiones heredadas del diseñador: el informe se alimenta por DataSet.
                report.Dictionary.Databases.Clear();
                report.Dictionary.DataSources.Clear();
                report.RegData(ds);
                report.Dictionary.Synchronize();

                // Variables de cabecera: se inyectan si existen; si no, se crean.
                Action<string, string> setVar = (key, val) =>
                {
                    if (report.Dictionary.Variables.Contains(key)) report.Dictionary.Variables[key].Value = val;
                    else report.Dictionary.Variables.Add(key, val);
                };

                setVar("NombreCliente", nombreCliente);
                setVar("NifCliente", nifCliente);
                setVar("RangoFechas", $"Ejercicio: {_yearActual}");

                AplicarVariablesEmisorDesdeBD(report);

                report.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el listado por cliente:\n" + ex.Message);
            }
        }

        // Factura individual con retención: dataset completo (cabecera + líneas) y .mrt específico.
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

        // Factura individual sin retención: mismo dataset, distinto template.
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

        // Extrae el id de la factura seleccionada y valida que exista selección.
        private bool TryGetFacturaActual(out int idFactura)
        {
            idFactura = -1;
            if (_bsFacturas.Current is not DataRowView rowFactura) return false;
            idFactura = Convert.ToInt32(rowFactura["id"]);
            return idFactura > 0;
        }

        // Dataset para factura individual: cabecera y líneas con relación para Stimulsoft.
        private DataSet CrearDataSetFactura(int idFactura)
        {
            var ds = new DataSet("DatosFactura");
            var p = new Dictionary<string, object> { ["@id"] = idFactura };

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

            // Relación para que el informe pueda recorrer líneas desde la cabecera.
            if (dtCabecera.Columns.Contains("id") && dtLineas.Columns.Contains("idfacemi"))
            {
                if (!ds.Relations.Contains("Cabecera_Lineas"))
                    ds.Relations.Add("Cabecera_Lineas", dtCabecera.Columns["id"], dtLineas.Columns["idfacemi"], false);
            }

            return ds;
        }

        // Dataset tipado para listados por cliente: fuerza tipos compatibles con el diseñador de informes.
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
                ["@idEmisor"] = _idEmisor,
                ["@idCliente"] = idCliente,
                ["@fi"] = fi.Date,
                ["@ff"] = ff.Date
            };

            var t = new Tabla(Program.appDAM.LaConexion);

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

                    // MySQL suele exponer TINYINT como numérico: se normaliza a bool.
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

        // Dataset para listados generales de facturas emitidas (orden configurable).
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

        // Carga y muestra un informe Stimulsoft basado en DataSet + variables opcionales.
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

                // Se fuerza el modo "DataSet" para evitar dependencias de conexiones del diseñador.
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

        // Inyecta variables del emisor (cabecera corporativa) desde BD en el informe.
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
                // Fallo no crítico: el informe se renderiza con valores vacíos por defecto.
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

        // Setter defensivo: solo escribe variables existentes en el diccionario del informe.
        private void SetVar(StiReport report, string nombre, string valor)
        {
            if (report.Dictionary.Variables.Contains(nombre))
                report.Dictionary.Variables[nombre].Value = valor ?? "";
        }

        #endregion

        #endregion
    }
}
