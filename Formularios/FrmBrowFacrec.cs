/*
 * Módulo: FrmBrowFacrec
 * Propósito: Gestión de facturas recibidas (compras). 
 * Correcciones: 
 * - Refresco automático de proveedores al activar.
 * - Refresco automático de años al crear/editar facturas (SOLUCIONADO).
 * - Mantenimiento de la selección del año tras refrescar.
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

        private Tabla _tablaProveedores;
        private Tabla _tablaFacrec;

        private readonly BindingSource _bsProveedores = new BindingSource();
        private readonly BindingSource _bsFacturas = new BindingSource();

        private int _idEmpresa;
        private int _yearActual;

        private bool _isLoaded = false;

        public FrmBrowFacrec()
        {
            InitializeComponent();
        }

        #endregion

        #region Inicialización y Ciclo de Vida

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

            // Carga inicial
            CargarYearsDesdeBD();
            CargarProveedores();

            dgProveedores.DataSource = _bsProveedores;
            dgFacturas.DataSource = _bsFacturas;

            ConfigurarProveedores();
            ConfigurarFacturas();

            CargarFacturasProveedorSeleccionado();

            _isLoaded = true;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (_isLoaded)
            {
                RefrescarListaProveedores();
            }
        }

        private void RefrescarListaProveedores()
        {
            try
            {
                int idSeleccionado = -1;
                if (_bsProveedores.Current is DataRowView row && row["id"] != DBNull.Value)
                {
                    idSeleccionado = Convert.ToInt32(row["id"]);
                }

                const string sql = @"
                    SELECT id, nombrecomercial, nombre, apellidos, nifcif
                    FROM proveedores
                    ORDER BY nombrecomercial, nombre, apellidos;";

                if (_tablaProveedores.InicializarDatos(sql))
                {
                    _bsProveedores.DataSource = _tablaProveedores.LaTabla;

                    if (idSeleccionado > 0)
                    {
                        int pos = _bsProveedores.Find("id", idSeleccionado);
                        if (pos >= 0)
                            _bsProveedores.Position = pos;
                        else
                            if (_bsProveedores.Count > 0) _bsProveedores.Position = 0;
                    }
                }
            }
            catch
            {
            }
        }

        private void FrmBrowFacrec_Shown(object sender, EventArgs e)
        {
            ConfiguracionVentana.Restaurar(this, "BrowFacrec");
        }

        private void FrmBrowFacrec_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfiguracionVentana.Guardar(this, "BrowFacrec");
        }

        #endregion

        #region Carga de Datos

        // CORREGIDO: Ahora intenta mantener la selección del año tras recargar
        private void CargarYearsDesdeBD()
        {
            // Guardamos selección actual
            object seleccionPrevia = tsComboYear.SelectedItem;

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
                years.Clear();
            }

            if (years.Count == 0)
                years.Add(DateTime.Now.Year);

            foreach (var y in years)
                tsComboYear.Items.Add(y);

            // Restauramos selección o vamos al primero
            if (seleccionPrevia != null && tsComboYear.Items.Contains(seleccionPrevia))
            {
                tsComboYear.SelectedItem = seleccionPrevia;
            }
            else
            {
                tsComboYear.SelectedIndex = 0;
            }

            _yearActual = (int)tsComboYear.SelectedItem;
        }

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

        #endregion

        #region Configuración de Rejillas

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

        #endregion

        #region Lógica de Carga y Cálculos

        private void dgProveedores_SelectionChanged(object sender, EventArgs e)
        {
            CargarFacturasProveedorSeleccionado();
        }

        private void tsComboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tsComboYear.SelectedItem is int y)
                _yearActual = y;

            CargarFacturasProveedorSeleccionado();
        }

        private void CargarFacturasProveedorSeleccionado()
        {
            _bsFacturas.SuspendBinding();
            _bsFacturas.DataSource = null;
            _bsFacturas.ResumeBinding();

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

            if (!_tablaFacrec.InicializarDatos(sql, p))
            {
                ActualizarEstado(0, ContarTotalesAnho());
                CalcularTotales();
                return;
            }

            dgFacturas.DataSource = null;
            _bsFacturas.DataSource = _tablaFacrec.LaTabla;
            dgFacturas.AutoGenerateColumns = true;
            dgFacturas.DataSource = _bsFacturas;

            ConfigurarCabeceras();

            if (_bsFacturas.Count > 0)
                _bsFacturas.Position = 0;

            ActualizarEstado(_bsFacturas.Count, ContarTotalesAnho());
            CalcularTotales();
        }

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
            if (_bsProveedores.Current is not DataRowView rowProveedor) return;

            int idProveedor = Convert.ToInt32(rowProveedor["id"]);

            using var frm = new FrmFacrec(_bsFacturas, _tablaFacrec, _idEmpresa, idProveedor, _yearActual, -1);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                // CORRECCIÓN: Recargar años por si se añadió uno nuevo
                CargarYearsDesdeBD();
                CargarFacturasProveedorSeleccionado();
            }
        }

        private void tsBtnEdit_Click(object sender, EventArgs e)
        {
            if (_bsProveedores.Current is not DataRowView) return;
            if (_bsFacturas.Current is not DataRowView rowFactura) return;

            int idProveedor = Convert.ToInt32(rowFactura["idproveedor"]);
            int idFacrec = Convert.ToInt32(rowFactura["id"]);

            using var frm = new FrmFacrec(_bsFacturas, _tablaFacrec, _idEmpresa, idProveedor, _yearActual, idFacrec);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                // CORRECCIÓN: Recargar años por si la fecha de la factura cambió de año
                CargarYearsDesdeBD();
                CargarFacturasProveedorSeleccionado();
            }
        }

        private void dgFacturas_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;
            tsBtnEdit_Click(sender, EventArgs.Empty);
        }

        private void tsBtnDelete_Click(object sender, EventArgs e)
        {
            if (_bsFacturas.Current is not DataRowView) return;

            if (MessageBox.Show("¿Eliminar la factura recibida seleccionada?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            _bsFacturas.RemoveCurrent();
            _tablaFacrec.GuardarCambios();
            _tablaFacrec.Refrescar();

            // CORRECCIÓN: Recargar años por si se borró la única factura de un año
            CargarYearsDesdeBD();
            CargarFacturasProveedorSeleccionado();
        }

        #endregion

        #region Exportación

        private void tsBtnExportCSV_Click(object sender, EventArgs e)
        {
            if (_bsFacturas.DataSource is not DataTable dt || dt.Rows.Count == 0) return;

            using var sfd = new SaveFileDialog { Filter = "Archivo CSV (*.csv)|*.csv", FileName = "facrec.csv" };
            if (sfd.ShowDialog(this) == DialogResult.OK)
                ExportarDatos.ExportarCSV(dt, sfd.FileName);
        }

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