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
    public partial class FrmBrowProveedores : Form
    {
        private Tabla _tabla;
        private BindingSource _bs = new BindingSource();
        private Dictionary<int, string> _provincias;

        public FrmBrowProveedores()
        {
            InitializeComponent();
            _provincias = new Dictionary<int, string>();
        }

        private void tsBtnFirst_Click(object sender, EventArgs e) => _bs.MoveFirst();
        private void tsBtnPrev_Click(object sender, EventArgs e) => _bs.MovePrevious();
        private void tsBtnNext_Click(object sender, EventArgs e) => _bs.MoveNext();
        private void tsBtnLast_Click(object sender, EventArgs e) => _bs.MoveLast();

        private void tsBtnNew_Click(object sender, EventArgs e)
        {
            _bs.AddNew();
            FrmProveedor frm = new FrmProveedor(_bs, _tabla);
            frm.edicion = false;

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                _tabla.Refrescar();
                ActualizarEstado();
            }
            else
            {
                _bs.CancelEdit();
            }
        }

        private void tsBtnEdit_Click(object sender, EventArgs e)
        {
            if (_bs.Current is DataRowView)
            {
                FrmProveedor frm = new FrmProveedor(_bs, _tabla);
                frm.edicion = true;

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    _tabla.Refrescar();
                    ActualizarEstado();
                }
                else
                {
                    _bs.CancelEdit();
                }
            }
        }

        private void dgTabla_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            tsBtnEdit_Click(sender, e);
        }

        private void tsBtnDelete_Click(object sender, EventArgs e)
        {
            if (_bs.Current is DataRowView row)
            {
                string mNif = Convert.ToString(row["nifcif"]) ?? "";

                if (TieneFacturasEmitidas(mNif))
                {
                    MessageBox.Show("No se puede eliminar el Proveedor porque tiene facturas emitidas.");
                    return;
                }

                if (MessageBox.Show("¿Desea eliminar el registro seleccionado?",
                    "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _bs.RemoveCurrent();
                    _tabla.GuardarCambios();
                    ActualizarEstado();
                }
            }
        }

        private void dgTabla_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgTabla.Columns[e.ColumnIndex].Name == "idprovincia")
            {
                if (e.Value is int idProvincia)
                {
                    e.Value = ObtenerNombreProvincia(idProvincia);
                    e.FormattingApplied = true;
                }
                else if (e.Value != null && int.TryParse(Convert.ToString(e.Value), out int idProv))
                {
                    e.Value = ObtenerNombreProvincia(idProv);
                    e.FormattingApplied = true;
                }
            }
        }

        private void FrmBrowProveedores_Load(object sender, EventArgs e)
        {
            _tabla = new Tabla(Program.appDAM.LaConexion);

            string mSql = "SELECT * FROM proveedores";

            if (_tabla.InicializarDatos(mSql))
            {
                _bs.DataSource = _tabla.LaTabla;
                dgTabla.DataSource = _bs;

                CargarProvincias();
                PersonalizarDataGrid();
            }
            else
            {
                MessageBox.Show("No se pudieron cargar los proveedores.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ActualizarEstado();
        }

        private void FrmBrowProveedores_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfiguracionVentana.Guardar(this, "BrowProveedores");
        }

        private void FrmBrowProveedores_Shown(object sender, EventArgs e)
        {
            ConfiguracionVentana.Restaurar(this, "BrowProveedores");
        }

        private void tsBtnExportCSV_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archivo CSV (*.csv)|*.csv";
            if (sfd.ShowDialog() == DialogResult.OK)
                ExportarDatos.ExportarCSV((DataTable)_bs.DataSource, sfd.FileName);
        }

        private void tsBtnExportXML_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archivo XML (*.xml)|*.xml";
            if (sfd.ShowDialog() == DialogResult.OK)
                ExportarDatos.ExportarXML((DataTable)_bs.DataSource, sfd.FileName, "Proveedores");
        }

        private void ActualizarEstado()
        {
            tsLbNumReg.Text = $"Nº de registros: {_bs.Count}";
        }

        private void PersonalizarDataGrid()
        {
            if (dgTabla.Columns.Contains("id"))
                dgTabla.Columns["id"].Visible = false;

            if (dgTabla.Columns.Contains("telefono2"))
                dgTabla.Columns["telefono2"].Visible = false;

            // En tu BD es "direccion" (no "domicilio")
            if (dgTabla.Columns.Contains("direccion"))
                dgTabla.Columns["direccion"].Visible = false;

            if (dgTabla.Columns.Contains("nifcif"))
            {
                dgTabla.Columns["nifcif"].HeaderText = "NIF/CIF";
                dgTabla.Columns["nifcif"].Width = 100;
            }

            if (dgTabla.Columns.Contains("nombre"))
            {
                dgTabla.Columns["nombre"].HeaderText = "Nombre";
                dgTabla.Columns["nombre"].Width = 120;
            }

            if (dgTabla.Columns.Contains("apellidos"))
            {
                dgTabla.Columns["apellidos"].HeaderText = "Apellidos";
                dgTabla.Columns["apellidos"].Width = 160;
            }

            if (dgTabla.Columns.Contains("nombrecomercial"))
            {
                dgTabla.Columns["nombrecomercial"].HeaderText = "Nombre Comercial";
                dgTabla.Columns["nombrecomercial"].Width = 200;
            }

            // En tu BD es "cpostal" (no "codigopostal")
            if (dgTabla.Columns.Contains("cpostal"))
            {
                dgTabla.Columns["cpostal"].HeaderText = "C.P.";
                dgTabla.Columns["cpostal"].Width = 75;
            }

            if (dgTabla.Columns.Contains("idprovincia"))
            {
                dgTabla.Columns["idprovincia"].HeaderText = "Provincia";
                dgTabla.Columns["idprovincia"].Width = 150;
            }

            if (dgTabla.Columns.Contains("telefono1"))
            {
                dgTabla.Columns["telefono1"].HeaderText = "Teléfono 1";
                dgTabla.Columns["telefono1"].Width = 100;
            }

            if (dgTabla.Columns.Contains("email"))
            {
                dgTabla.Columns["email"].HeaderText = "Correo electrónico";
                dgTabla.Columns["email"].Width = 250;
            }

            dgTabla.EnableHeadersVisualStyles = false;
            dgTabla.ColumnHeadersHeight = 34;
            dgTabla.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 240, 240, 240);
            dgTabla.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(255, 33, 33, 33);
            dgTabla.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgTabla.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 230, 255, 255);
        }

        private void CargarProvincias()
        {
            _provincias.Clear();

            using var cmd = new MySqlCommand("SELECT id, nombreprovincia FROM provincias", Program.appDAM.LaConexion);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string nombre = reader.GetString(1);
                _provincias[id] = nombre;
            }
        }

        private string ObtenerNombreProvincia(int id)
        {
            return _provincias.TryGetValue(id, out var nombre) ? nombre : "";
        }

        private bool TieneFacturasEmitidas(string aNifCif)
        {
            if (string.IsNullOrWhiteSpace(aNifCif)) return false;

            try
            {
                using var cmd = new MySqlCommand(@"
                    SELECT COUNT(*)
                    FROM facemi f
                    INNER JOIN proveedores c ON c.id = f.idproveedor
                    WHERE c.nifcif = @nif;", Program.appDAM.LaConexion);

                cmd.Parameters.AddWithValue("@nif", aNifCif);

                object? obj = cmd.ExecuteScalar();
                int count = 0;

                if (obj != null && obj != DBNull.Value)
                    int.TryParse(Convert.ToString(obj), out count);

                return count > 0;
            }
            catch (Exception ex)
            {
                Program.appDAM?.RegistrarLog("TieneFacturasEmitidas Proveedor", ex.Message);
                return false;
            }
        }
    }
}
