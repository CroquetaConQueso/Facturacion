/*
 * Módulo: FrmBrowFacrec
 * Propósito: Gestión de facturas recibidas (compras). Permite filtrar por proveedor y año,
 * gestionar el CRUD básico y exportar listados.
 *
 * Flujo general:
 * 1) Se carga el emisor (empresa activa) desde Program.appDAM.emisor.
 * 2) Se precargan años disponibles desde facrec y se listan proveedores.
 * 3) Al seleccionar proveedor + año, se muestran sus facturas recibidas.
 * 4) Se permiten operaciones CRUD y exportación (CSV/XML) sobre el conjunto filtrado.
 */

using FacturacionDAM.Modelos;
using FacturacionDAM.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FacturacionDAM.Formularios
{
    public partial class FrmBrowFacrec : Form
    {
        #region Campos y Constructor

        // Tablas de acceso a datos (wrappers internos del proyecto)
        private Tabla _tablaProveedores;
        private Tabla _tablaFacrec;

        // BindingSource: desacopla DataTable de la UI y facilita navegación/selección
        private readonly BindingSource _bsProveedores = new BindingSource();
        private readonly BindingSource _bsFacturas = new BindingSource();

        // Empresa activa (emisor) y año seleccionado en el filtro
        private int _idEmpresa;
        private int _yearActual;

        public FrmBrowFacrec()
        {
            InitializeComponent();
        }

        #endregion

        #region Inicialización y Configuración

        // Carga inicial del formulario:
        // - Valida que exista emisor activo.
        // - Inicializa tablas y fuentes de datos.
        // - Configura grids y carga facturas del proveedor seleccionado.
        private void FrmBrowFacrec_Load(object sender, EventArgs e)
        {
            if (Program.appDAM?.emisor == null)
            {
                MessageBox.Show("No hay empresa (emisor) activa.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            _idEmpresa = Program.appDAM.emisor.id;

            _tablaProveedores = new Tabla(Program.appDAM.LaConexion);
            _tablaFacrec = new Tabla(Program.appDAM.LaConexion);

            // Pobla el combo de años usando fechas reales de facrec
            CargarYearsDesdeBD();

            // Pobla el grid de proveedores
            CargarProveedores();

            // Enlaza fuentes a grids
            dgProveedores.DataSource = _bsProveedores;
            dgFacturas.DataSource = _bsFacturas;

            // Ajustes visuales y de comportamiento de selección
            ConfigurarProveedores();
            ConfigurarFacturas();

            // Carga inicial de facturas según proveedor/año (si hay selección)
            CargarFacturasProveedorSeleccionado();
        }

        // Se ejecuta tras mostrarse: restaura tamaño/posición persistidos
        private void FrmBrowFacrec_Shown(object sender, EventArgs e)
        {
            ConfiguracionVentana.Restaurar(this, "BrowFacrec");
        }

        // Antes de cerrar: guarda tamaño/posición persistidos
        private void FrmBrowFacrec_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfiguracionVentana.Guardar(this, "BrowFacrec");
        }

        // Carga en el combo los años existentes en facrec para la empresa activa.
        // Si no hay facturas todavía, fuerza el año actual para no dejar el combo vacío.
        private void CargarYearsDesdeBD()
        {
            tsComboYear.Items.Clear();
            var years = new List<int>();

            try
            {
                const string sql = @"
                    SELECT DISTINCT YEAR(fecha) AS anho
                    FROM facrec
                    WHERE idempresa = @idEmpresa
                    ORDER BY anho DESC;";

                using var cmd = new MySqlCommand(sql, Program.appDAM.LaConexion);
                cmd.Parameters.AddWithValue("@idEmpresa", _idEmpresa);

                using var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (!rd.IsDBNull(0))
                        years.Add(rd.GetInt32(0));
                }
            }
            catch
            {
                // Si algo falla (conexión, tabla, permisos...), evitamos romper la UI.
                years.Clear();
            }

            // Fallback: evita UI sin años y permite trabajar aunque no existan registros.
            if (years.Count == 0)
                years.Add(DateTime.Now.Year);

            foreach (var y in years)
                tsComboYear.Items.Add(y);

            // Establece el año activo a partir del primer valor (más reciente)
            tsComboYear.SelectedIndex = 0;
            _yearActual = (int)tsComboYear.SelectedItem;
        }

        // Carga proveedores y los asigna al BindingSource principal del grid superior.
        private void CargarProveedores()
        {
            const string sql = @"
                SELECT id, nombrecomercial, nombre, apellidos, nifcif
                FROM proveedores
                ORDER BY nombrecomercial, nombre, apellidos;";

            if (!_tablaProveedores.InicializarDatos(sql))
            {
                MessageBox.Show("No se pudieron cargar los proveedores.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _bsProveedores.DataSource = _tablaProveedores.LaTabla;
        }

        // Configura el grid de proveedores:
        // - Solo lectura
        // - Selección de fila completa
        // - Ocultación de ID y ajuste de cabeceras
        private void ConfigurarProveedores()
        {
            dgProveedores.ReadOnly = true;
            dgProveedores.MultiSelect = false;
            dgProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgProveedores.AllowUserToAddRows = false;
            dgProveedores.AllowUserToDeleteRows = false;

            if (dgProveedores.Columns.Contains("id"))
                dgProveedores.Columns["id"].Visible = false;

            dgProveedores.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 240, 255, 255);

            foreach (DataGridViewColumn col in dgProveedores.Columns)
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

        // Configura el grid de facturas:
        // - Solo lectura
        // - Selección de fila completa
        // - Ocultación de ID (si existe)
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

        // Evento: cambia el proveedor seleccionado => recarga facturas filtradas por proveedor/año
        private void dgProveedores_SelectionChanged(object sender, EventArgs e)
        {
            CargarFacturasProveedorSeleccionado();
        }

        // Evento: cambia el año del filtro => recarga facturas filtradas por proveedor/año
        private void tsComboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tsComboYear.SelectedItem is int y)
                _yearActual = y;

            CargarFacturasProveedorSeleccionado();
        }

        // Carga facturas según:
        // - empresa activa (idempresa)
        // - proveedor seleccionado (idproveedor)
        // - año seleccionado (YEAR(fecha))
        //
        // Si no hay proveedor seleccionado, limpia la tabla y muestra totales globales del año.
        private void CargarFacturasProveedorSeleccionado()
        {
            // Limpieza previa del BindingSource para evitar residuos visuales
            _bsFacturas.SuspendBinding();
            _bsFacturas.DataSource = null;
            _bsFacturas.ResumeBinding();

            // Si no hay proveedor seleccionado, no se aplica filtro por proveedor
            if (_bsProveedores.Current is not DataRowView rowProveedor)
            {
                ActualizarEstado(0, ContarTotalesAnho());
                CalcularTotales();
                return;
            }

            int idProveedor = Convert.ToInt32(rowProveedor["id"]);

            var p = new Dictionary<string, object>
            {
                ["@idEmpresa"] = _idEmpresa,
                ["@idProveedor"] = idProveedor,
                ["@year"] = _yearActual
            };

            const string sql = @"
                SELECT f.*
                FROM facrec f
                WHERE f.idempresa = @idEmpresa
                  AND f.idproveedor = @idProveedor
                  AND YEAR(f.fecha) = @year
                ORDER BY f.fecha DESC, f.id DESC;";

            // Si no se puede inicializar, dejamos el panel consistente (estado + totales)
            if (!_tablaFacrec.InicializarDatos(sql, p))
            {
                ActualizarEstado(0, ContarTotalesAnho());
                CalcularTotales();
                return;
            }

            // Reasignación del DataSource para forzar refresco y autogeneración
            dgFacturas.DataSource = null;
            _bsFacturas.DataSource = _tablaFacrec.LaTabla;
            dgFacturas.AutoGenerateColumns = true;
            dgFacturas.DataSource = _bsFacturas;

            // Ajusta cabeceras / formatos / oculta IDs
            ConfigurarCabeceras();

            // Coloca el cursor en el primer registro si hay datos
            if (_bsFacturas.Count > 0)
                _bsFacturas.Position = 0;

            // Actualiza indicadores y totales de importes en barra de estado
            ActualizarEstado(_bsFacturas.Count, ContarTotalesAnho());
            CalcularTotales();
        }

        // Configura cabeceras y formatos del grid de facturas:
        // - Oculta columnas técnicas (id*)
        // - Formatea decimales (base/cuota/total)
        // - Ajusta texto visible
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

        // Suma importes visibles en el BindingSource:
        // - base
        // - cuota
        // - total
        // y los muestra en la StatusStrip.
        private void CalcularTotales()
        {
            decimal totalBase = 0;
            decimal totalCuota = 0;
            decimal totalTotal = 0;

            // El cálculo depende de que el DataSource sea un DataTable
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

            // Se actualizan labels si existen en la UI
            if (tsLbTotalBase != null) tsLbTotalBase.Text = $"Totales base: {totalBase:N2}";
            if (tsLbTotalCuota != null) tsLbTotalCuota.Text = $"Totales cuota: {totalCuota:N2}";
            if (tsLbTotalTotal != null) tsLbTotalTotal.Text = $"Totales totales: {totalTotal:N2}";
        }

        // Cuenta todas las facturas del año (sin filtrar por proveedor) para dar contexto en la barra de estado.
        private int ContarTotalesAnho()
        {
            try
            {
                const string sql = @"
                    SELECT COUNT(*)
                    FROM facrec
                    WHERE idempresa = @idEmpresa
                      AND YEAR(fecha) = @year;";

                using var cmd = new MySqlCommand(sql, Program.appDAM.LaConexion);
                cmd.Parameters.AddWithValue("@idEmpresa", _idEmpresa);
                cmd.Parameters.AddWithValue("@year", _yearActual);

                var obj = cmd.ExecuteScalar();
                return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
            }
            catch
            {
                // Si falla la consulta, devolvemos 0 para no romper la UI.
                return 0;
            }
        }

        // Actualiza contadores en la barra de estado:
        // - encontrados: registros del filtro proveedor/año
        // - totales: registros totales del año (empresa/año)
        private void ActualizarEstado(int encontrados, int totales)
        {
            if (tsLbNumReg != null)
                tsLbNumReg.Text = $"Nº de registros: {encontrados}";

            if (tsLbStatus != null)
                tsLbStatus.Text = $"Nº de registros totales: {totales}";
        }

        #endregion

        #region Navegación y CRUD

        // Navegación sobre el BindingSource de facturas
        private void tsBtnFirst_Click(object sender, EventArgs e) => _bsFacturas.MoveFirst();
        private void tsBtnPrev_Click(object sender, EventArgs e) => _bsFacturas.MovePrevious();
        private void tsBtnNext_Click(object sender, EventArgs e) => _bsFacturas.MoveNext();
        private void tsBtnLast_Click(object sender, EventArgs e) => _bsFacturas.MoveLast();

        // Crea una factura nueva para el proveedor seleccionado
        private void tsBtnNew_Click(object sender, EventArgs e)
        {
            if (_bsProveedores.Current is not DataRowView rowProveedor) return;

            int idProveedor = Convert.ToInt32(rowProveedor["id"]);

            using var frm = new FrmFacrec(_bsFacturas, _tablaFacrec, _idEmpresa, idProveedor, _yearActual, -1);
            if (frm.ShowDialog(this) == DialogResult.OK)
                CargarFacturasProveedorSeleccionado();
        }

        // Edita la factura seleccionada en el grid inferior
        private void tsBtnEdit_Click(object sender, EventArgs e)
        {
            if (_bsProveedores.Current is not DataRowView) return;
            if (_bsFacturas.Current is not DataRowView rowFactura) return;

            int idProveedor = Convert.ToInt32(rowFactura["idproveedor"]);
            int idFacrec = Convert.ToInt32(rowFactura["id"]);

            using var frm = new FrmFacrec(_bsFacturas, _tablaFacrec, _idEmpresa, idProveedor, _yearActual, idFacrec);
            if (frm.ShowDialog(this) == DialogResult.OK)
                CargarFacturasProveedorSeleccionado();
        }

        // Doble clic en factura => edición rápida
        private void dgFacturas_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;
            tsBtnEdit_Click(sender, EventArgs.Empty);
        }

        // Elimina la factura seleccionada y persiste cambios
        private void tsBtnDelete_Click(object sender, EventArgs e)
        {
            if (_bsFacturas.Current is not DataRowView) return;

            if (MessageBox.Show("¿Eliminar la factura recibida seleccionada?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            _bsFacturas.RemoveCurrent();
            _tablaFacrec.GuardarCambios();
            _tablaFacrec.Refrescar();
            CargarFacturasProveedorSeleccionado();
        }

        #endregion

        #region Exportación

        // Exporta el conjunto filtrado actual a CSV (a través de utilidad común)
        private void tsBtnExportCSV_Click(object sender, EventArgs e)
        {
            if (_bsFacturas.DataSource is not DataTable dt || dt.Rows.Count == 0) return;

            using var sfd = new SaveFileDialog { Filter = "Archivo CSV (*.csv)|*.csv", FileName = "facrec.csv" };
            if (sfd.ShowDialog(this) == DialogResult.OK)
                ExportarDatos.ExportarCSV(dt, sfd.FileName);
        }

        // Exporta el conjunto filtrado actual a XML (a través de utilidad común)
        private void tsBtnExportXML_Click(object sender, EventArgs e)
        {
            if (_bsFacturas.DataSource is not DataTable dt || dt.Rows.Count == 0) return;

            using var sfd = new SaveFileDialog { Filter = "Archivo XML (*.xml)|*.xml", FileName = "facrec.xml" };
            if (sfd.ShowDialog(this) == DialogResult.OK)
                ExportarDatos.ExportarXML(dt, sfd.FileName, "Facrec");
        }

        #endregion
    }
}
