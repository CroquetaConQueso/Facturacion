using FacturacionDAM.Modelos;
using System;
using System.Data;
using System.Windows.Forms;

namespace FacturacionDAM.Formularios
{
    public partial class FrmSeleccionarEmisor : Form
    {
        private Tabla _tablaEmisores;
        private readonly BindingSource _bsEmisores = new BindingSource();

        public FrmSeleccionarEmisor()
        {
            InitializeComponent();
        }

        private void FrmSeleccionarEmisor_Load(object sender, EventArgs e)
        {
            _tablaEmisores = new Tabla(Program.appDAM.LaConexion);

            if (_tablaEmisores.InicializarDatos("SELECT * FROM emisores"))
            {
                _bsEmisores.DataSource = _tablaEmisores.LaTabla;
                cbEmisores.DataSource = _bsEmisores;
                cbEmisores.DisplayMember = "nombrecomercial";
                cbEmisores.ValueMember = "id";
            }
            else
            {
                MessageBox.Show("No se pudieron cargar los emisores.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.appDAM.estadoApp = EstadoApp.Error;
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Program.appDAM.estadoApp = EstadoApp.ConectadoSinEmisor;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (_bsEmisores.Current is not DataRowView row)
            {
                MessageBox.Show("Debe seleccionar un emisor válido.");
                return;
            }

            var emisorSeleccionado = new Emisor
            {
                id = SafeInt(row["id"], -1),
                nifcif = SafeStr(row["nifcif"]),
                nombre = SafeStr(row["nombre"]),
                apellidos = SafeStr(row["apellidos"]),
                nombreComercial = SafeStr(row["nombrecomercial"]),
                nextNumFac = SafeInt(row["nextnumfac"], 1)
            };

            Program.appDAM.emisor = emisorSeleccionado;
            Program.appDAM.estadoApp = EstadoApp.Conectado;
            DialogResult = DialogResult.OK;
            Close();
        }

        private static string SafeStr(object v)
        {
            return v == null || v == DBNull.Value ? "" : v.ToString();
        }

        private static int SafeInt(object v, int def)
        {
            if (v == null || v == DBNull.Value) return def;
            if (v is int i) return i;
            return int.TryParse(v.ToString(), out int n) ? n : def;
        }

        private void FrmSeleccionarEmisor_FormClosing(object sender, FormClosingEventArgs e)
        {
            _tablaEmisores?.Liberar();
            _tablaEmisores = null;
        }
    }
}
