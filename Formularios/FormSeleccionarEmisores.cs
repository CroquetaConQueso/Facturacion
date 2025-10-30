using FacturacionDAM.Modelos;
using System;
using System.Data;
using System.Windows.Forms;

namespace FacturacionDAM.Formularios
{
    public partial class FormSeleccionarEmisores : Form
    {
        private Tabla _tablaEmisores;
        private readonly BindingSource _bsEmisores = new BindingSource();

        public Emisor EmisorSeleccionado { get; private set; }

        public FormSeleccionarEmisores()
        {
            InitializeComponent();
            this.Load += FrmSeleccionarEmisor_Load;
        }

        private void FrmSeleccionarEmisor_Load(object sender, EventArgs e)
        {
            _tablaEmisores = new Tabla(Program.appDAM.LaConexion);

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

            if (_tablaEmisores.InicializarDatos(sql))
            {
                _bsEmisores.DataSource = _tablaEmisores.LaTabla;
                cbEmisor.DataSource = _bsEmisores;
                cbEmisor.DisplayMember = "nombrecomercial";
                cbEmisor.ValueMember = "id";
            }
            else
            {
                MessageBox.Show("No se pudieron cargar los emisores", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.appDAM.estadoApp = EstadoApp.Error;
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (!(_bsEmisores.Current is DataRowView row))
            {
                MessageBox.Show("No se ha seleccionado ningún emisor.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            EmisorSeleccionado = new Emisor
            {
                id = row.Row.Table.Columns.Contains("id") ? Convert.ToInt32(row["id"]) : 0,
                nifcif = Convert.ToString(row["nifcif"]) ?? "",
                nombre = Convert.ToString(row["nombre"]) ?? "",
                apellido = Convert.ToString(row["apellido"]) ?? "",
                nombrecomercial = Convert.ToString(row["nombrecomercial"]) ?? "",
                domicilio = Convert.ToString(row["domicilio"]) ?? "",
                codigopostal = Convert.ToString(row["codigopostal"]) ?? "",
                poblacion = Convert.ToString(row["poblacion"]) ?? "",
                idprovincia = row["idprovincia"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["idprovincia"]),
                telefono1 = Convert.ToString(row["telefono1"]) ?? "",
                telefono2 = Convert.ToString(row["telefono2"]) ?? "",
                email = Convert.ToString(row["email"]) ?? "",
                descripcion = Convert.ToString(row["descripcion"]) ?? ""
            };

            Program.appDAM.emisor = EmisorSeleccionado;
            Program.appDAM.estadoApp = EstadoApp.Conectado;

            var frmBrow = new FrmBrowEmisores();
            if (this.Owner is FrmMain main) frmBrow.MdiParent = main;
            frmBrow.Show();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Program.appDAM.estadoApp = EstadoApp.ConectadoSinEmisor;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
