using System;
using System.Data;
using System.Windows.Forms;
using FacturacionDAM.Modelos;

namespace FacturacionDAM.Formularios
{
    public partial class FrmBrowEmisores : Form
    {
        private readonly Tabla _tabla;
        private readonly BindingSource _bs;

        public FrmBrowEmisores()
        {
            InitializeComponent();

            _bs = new BindingSource();
            _tabla = new Tabla(Program.appDAM.LaConexion);

            this.Load += FrmBrowEmisores_Load;

            if (dgTabla != null)
            {
                dgTabla.AutoGenerateColumns = true;
                dgTabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgTabla.MultiSelect = false;
                dgTabla.ReadOnly = false;
                dgTabla.AllowUserToAddRows = true;
                dgTabla.AllowUserToDeleteRows = true;
                dgTabla.CellDoubleClick += (s, e) => AceptarSeleccion();
            }

            this.KeyPreview = true;
            this.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) AceptarSeleccion(); };
        }

        private void FrmBrowEmisores_Load(object sender, EventArgs e)
        {
            const string sql = @"
                SELECT
                    id,
                    nifcif,
                    nombre,
                    apellido,
                    nombrecomercial,
                    domicilio,
                    codigopostal,
                    poblacion,
                    idprovincia,
                    telefono1,
                    telefono2,
                    email,
                    descripcion
                FROM emisores
                ORDER BY nombrecomercial, nombre, apellido;";

            if (_tabla.InicializarDatos(sql))
            {
                _bs.DataSource = _tabla.LaTabla;
                dgTabla.DataSource = _bs;
                ActualizarEstado();
            }
            else
            {
                MessageBox.Show("No se han podido cargar los emisores.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e) => _bs.MoveFirst();
        private void btnPrev_Click(object sender, EventArgs e) => _bs.MovePrevious();
        private void btnNext_Click(object sender, EventArgs e) => _bs.MoveNext();
        private void btnLast_Click(object sender, EventArgs e) => _bs.MoveLast();

        private void btnNew_Click(object sender, EventArgs e)
        {
            _bs.AddNew();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_bs.Current is DataRowView) { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!(_bs.Current is DataRowView drv)) return;

            int idActual = 0;
            var row = drv.Row;
            if (row.Table.Columns.Contains("id"))
                int.TryParse(Convert.ToString(row["id"]), out idActual);

            int idProtegido = Program.appDAM?.emisor?.id ?? -1;
            if (idProtegido != -1 && idActual == idProtegido)
            {
                MessageBox.Show(
                    "No puedes borrar el emisor actualmente seleccionado/activo.",
                    "Operación no permitida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("¿Eliminar este emisor?", "Confirmar eliminación",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _bs.RemoveCurrent();
                _tabla.GuardarDatos();
                _tabla.Refrescar();
                ActualizarEstado();
            }
        }

        public int? SelectedEmisorId { get; private set; }

        private void AceptarSeleccion()
        {
            if (!(_bs.Current is DataRowView drv))
            {
                MessageBox.Show("Selecciona un emisor.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int id = 0;
            var row = drv.Row;
            if (row.Table.Columns.Contains("id"))
                int.TryParse(Convert.ToString(row["id"]), out id);

            if (id <= 0)
            {
                MessageBox.Show("No se pudo determinar el ID del emisor.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SelectedEmisorId = id;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e) => AceptarSeleccion();

        private void ActualizarEstado()
        {
            try
            {
                if (tsStatusLabel != null)
                    tsStatusLabel.Text = $"Nº de Registros: {_bs.Count}";
            }
            catch { }
        }
    }
}
