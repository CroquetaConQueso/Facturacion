// Ruta: FacturacionDAM/Formularios/FrmBrowConceptosFac.cs
using FacturacionDAM.Modelos;
using FacturacionDAM.Utils;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FacturacionDAM.Formularios
{
    public partial class FrmBrowConceptosFac : Form
    {
        private Tabla _tabla;
        private readonly BindingSource _bs = new BindingSource();

        public FrmBrowConceptosFac()
        {
            InitializeComponent();
        }

        private void FrmBrowConceptosFac_Load(object sender, EventArgs e)
        {
            _tabla = new Tabla(Program.appDAM.LaConexion);

            const string sql = @"SELECT id, codigo, descripcion
                                 FROM conceptosfac
                                 ORDER BY codigo;";

            if (!_tabla.InicializarDatos(sql))
            {
                MessageBox.Show("No se pudieron cargar los conceptos.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _bs.DataSource = _tabla.LaTabla;
            dgTabla.DataSource = _bs;

            ConfigurarGrid();
            ActualizarEstado();
        }

        private void FrmBrowConceptosFac_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfiguracionVentana.Guardar(this, "BrowConceptosFac");
        }

        private void FrmBrowConceptosFac_Shown(object sender, EventArgs e)
        {
            ConfiguracionVentana.Restaurar(this, "BrowConceptosFac");
        }

        private void ConfigurarGrid()
        {
            dgTabla.AutoGenerateColumns = true;
            dgTabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgTabla.MultiSelect = false;
            dgTabla.ReadOnly = true;
            dgTabla.AllowUserToAddRows = false;
            dgTabla.AllowUserToDeleteRows = false;

            if (dgTabla.Columns.Contains("id"))
                dgTabla.Columns["id"].Visible = false;

            if (dgTabla.Columns.Contains("codigo"))
            {
                dgTabla.Columns["codigo"].HeaderText = "Código";
                dgTabla.Columns["codigo"].Width = 120;
            }

            if (dgTabla.Columns.Contains("descripcion"))
            {
                dgTabla.Columns["descripcion"].HeaderText = "Descripción";
                dgTabla.Columns["descripcion"].Width = 450;
            }

            dgTabla.EnableHeadersVisualStyles = false;
            dgTabla.ColumnHeadersHeight = 34;
            dgTabla.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 240, 240, 240);
            dgTabla.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(255, 33, 33, 33);
            dgTabla.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgTabla.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 230, 255, 255);
        }

        private void ActualizarEstado()
        {
            if (tsLbNumReg != null)
                tsLbNumReg.Text = $"Nº de registros: {_bs.Count}";
        }

        private void dgTabla_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;
            tsBtnEdit_Click(sender, EventArgs.Empty);
        }

        private void tsBtnFirst_Click(object sender, EventArgs e) => _bs.MoveFirst();
        private void tsBtnPrev_Click(object sender, EventArgs e) => _bs.MovePrevious();
        private void tsBtnNext_Click(object sender, EventArgs e) => _bs.MoveNext();
        private void tsBtnLast_Click(object sender, EventArgs e) => _bs.MoveLast();

        private void tsBtnNew_Click(object sender, EventArgs e)
        {
            _bs.AddNew();

            using var frm = new FrmConceptoFac(_bs, _tabla) { edicion = false };
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                _tabla.GuardarCambios();
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
            if (_bs.Current is not DataRowView) return;

            using var frm = new FrmConceptoFac(_bs, _tabla) { edicion = true };
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                _tabla.GuardarCambios();
                _tabla.Refrescar();
                ActualizarEstado();
            }
            else
            {
                _bs.CancelEdit();
            }
        }

        private void tsBtnDelete_Click(object sender, EventArgs e)
        {
            if (_bs.Current is not DataRowView) return;

            if (MessageBox.Show("¿Desea eliminar el registro seleccionado?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            _bs.RemoveCurrent();
            _tabla.GuardarCambios();
            _tabla.Refrescar();
            ActualizarEstado();
        }

        private void tsBtnExportCSV_Click(object sender, EventArgs e)
        {
            if (_bs.DataSource is not DataTable dt || dt.Rows.Count == 0) return;

            using var sfd = new SaveFileDialog
            {
                Filter = "Archivo CSV (*.csv)|*.csv",
                FileName = "conceptosfac.csv"
            };

            if (sfd.ShowDialog(this) == DialogResult.OK)
                ExportarDatos.ExportarCSV(dt, sfd.FileName);
        }

        private void tsBtnExportXML_Click(object sender, EventArgs e)
        {
            if (_bs.DataSource is not DataTable dt || dt.Rows.Count == 0) return;

            using var sfd = new SaveFileDialog
            {
                Filter = "Archivo XML (*.xml)|*.xml",
                FileName = "conceptosfac.xml"
            };

            if (sfd.ShowDialog(this) == DialogResult.OK)
                ExportarDatos.ExportarXML(dt, sfd.FileName, "ConceptosFac");
        }
    }
}
