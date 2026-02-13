// Ruta: FacturacionDAM/Formularios/FrmBrowFacrec.cs
using FacturacionDAM.Modelos;
using FacturacionDAM.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FacturacionDAM.Formularios
{
    public partial class FrmBrowFacrec : Form
    {
        private Tabla _tablaProveedores;
        private Tabla _tablaFacrec;

        private readonly BindingSource _bsProveedores = new BindingSource();
        private readonly BindingSource _bsFacturas = new BindingSource();

        private int _idEmpresa; // Corresponde al emisor activo
        private int _yearActual;

        public FrmBrowFacrec()
        {
            InitializeComponent();
        }

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

            CargarYearsDesdeBD();
            CargarProveedores();

            dgProveedores.DataSource = _bsProveedores;
            dgFacturas.DataSource = _bsFacturas;

            ConfigurarProveedores();
            ConfigurarFacturas();

            CargarFacturasProveedorSeleccionado();
        }

        private void FrmBrowFacrec_Shown(object sender, EventArgs e)
        {
            ConfiguracionVentana.Restaurar(this, "BrowFacrec");
        }

        private void FrmBrowFacrec_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfiguracionVentana.Guardar(this, "BrowFacrec");
        }

        private void CargarYearsDesdeBD()
        {
            tsComboYear.Items.Clear();
            var years = new List<int>();

            try
            {
                // Buscamos años disponibles en facturas recibidas para la empresa actual
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

            tsComboYear.SelectedIndex = 0;
            _yearActual = (int)tsComboYear.SelectedItem;
        }

        private void CargarProveedores()
        {
            const string sql = @"SELECT id, nombrecomercial, nombre, apellidos, nifcif
                                 FROM proveedores
                                 ORDER BY nombrecomercial, nombre, apellidos;";

            if (!_tablaProveedores.InicializarDatos(sql))
            {
                MessageBox.Show("No se pudieron cargar los proveedores.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _bsProveedores.DataSource = _tablaProveedores.LaTabla;
        }

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
                    case "nombrecomercial":
                        col.HeaderText = "Nombre Comercial";
                        break;
                    case "nombre":
                        col.HeaderText = "Nombre";
                        break;
                    case "apellidos":
                        col.HeaderText = "Apellidos";
                        break;
                    case "nifcif":
                        col.HeaderText = "NIFCIF";
                        break;
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

        private void ConfigurarCabeceras()
        {
            foreach (DataGridViewColumn col in dgFacturas.Columns)
            {
                switch (col.Name.ToLower())
                {
                    case "idempresa":
                        col.HeaderText = "ID Empresa";
                        break;
                    case "idproveedor":
                        col.HeaderText = "ID Proveedor";
                        break;
                    case "idconceptofac":
                        col.HeaderText = "ID Concepto";
                        break;
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
                        // Formato User Friendly genérico
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

        // Navegación del BindingSource
        private void tsBtnFirst_Click(object sender, EventArgs e) => _bsFacturas.MoveFirst();
        private void tsBtnPrev_Click(object sender, EventArgs e) => _bsFacturas.MovePrevious();
        private void tsBtnNext_Click(object sender, EventArgs e) => _bsFacturas.MoveNext();
        private void tsBtnLast_Click(object sender, EventArgs e) => _bsFacturas.MoveLast();

        // Acciones CRUD
        private void tsBtnNew_Click(object sender, EventArgs e)
        {
            if (_bsProveedores.Current is not DataRowView rowProveedor) return;

            int idProveedor = Convert.ToInt32(rowProveedor["id"]);

            // Asumimos que existirá FrmFacrec similar a FrmFacemi
            using var frm = new FrmFacrec(_bsFacturas, _tablaFacrec, _idEmpresa, idProveedor, _yearActual, -1);
            if (frm.ShowDialog(this) == DialogResult.OK)
                CargarFacturasProveedorSeleccionado();
        }

        private void tsBtnEdit_Click(object sender, EventArgs e)
        {
            if (_bsProveedores.Current is not DataRowView rowProveedor) return;
            if (_bsFacturas.Current is not DataRowView rowFactura) return;

            int idProveedor = Convert.ToInt32(rowProveedor["id"]);
            int idFacrec = Convert.ToInt32(rowFactura["id"]);

            using var frm = new FrmFacrec(_bsFacturas, _tablaFacrec, _idEmpresa, idProveedor, _yearActual, idFacrec);
            if (frm.ShowDialog(this) == DialogResult.OK)
                CargarFacturasProveedorSeleccionado();
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
            CargarFacturasProveedorSeleccionado();
        }

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
    }
}