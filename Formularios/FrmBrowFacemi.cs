// Ruta: FacturacionDAM/Formularios/FrmBrowFacemi.cs
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
    public partial class FrmBrowFacemi : Form
    {
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

            if (years.Count == 0)
                years.Add(DateTime.Now.Year);

            foreach (var y in years)
                tsComboYear.Items.Add(y);

            tsComboYear.SelectedIndex = 0;
            _yearActual = (int)tsComboYear.SelectedItem;
        }

        private void CargarClientes()
        {
            const string sql = @"SELECT id, nombrecomercial, nombre, apellidos, nifcif
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

        private void CargarFacturasClienteSeleccionado()
        {
            _bsFacturas.SuspendBinding();
            _bsFacturas.DataSource = null;
            _bsFacturas.ResumeBinding();

            if (_bsClientes.Current is not DataRowView rowCliente)
            {
                ActualizarEstado(0, ContarTotalesAnho());
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
                return;
            }

            dgFacturas.DataSource = null;

            _bsFacturas.DataSource = _tablaFacemi.LaTabla;

            dgFacturas.AutoGenerateColumns = true;
            dgFacturas.DataSource = _bsFacturas;

            if (_bsFacturas.Count > 0)
                _bsFacturas.Position = 0;

            ActualizarEstado(_bsFacturas.Count, ContarTotalesAnho());
        }


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
    }
}
