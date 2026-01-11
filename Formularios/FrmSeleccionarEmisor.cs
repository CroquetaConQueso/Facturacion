using FacturacionDAM.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacturacionDAM.Formularios
{
    public partial class FrmSeleccionarEmisor : Form
    {

        private Tabla _tablaEmisores;                                   // Para acceso a la tabla de emisores.
        private BindingSource _bsEmisores = new BindingSource();        // Para visualizr los datos en los controles del formulario.

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
                MessageBox.Show("No se pudieron cargar los emisores.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Program.appDAM.estadoApp = EstadoApp.Error;

                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Program.appDAM.estadoApp = EstadoApp.ConectadoSinEmisor;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            /*
             * El siguiente if se podría haber hecho así:
             *      DataRowView row = _bsEmisores.Current as DataRowView;
             *      if (row != null)
             *      
             * En ese caso sería conveniente usar un try ... catch
             * Sin embargo, con esta forma primero comprueba el tipo, y sólo si es el esperado
             * crea el objeto row.
             */


            if (_bsEmisores.Current is DataRowView row)
            {
                Emisor emisorSeleccionado = new Emisor
                {
                    id = Convert.ToInt32(row["id"]),
                    nifcif = row["nifcif"].ToString(),
                    nombre = row["nombre"].ToString(),
                    apellidos = row["apellidos"].ToString(),
                    nombreComercial = row["nombrecomercial"].ToString(),
                    nextNumFac = Convert.ToInt32(row["nextnumfac"])
                };

                Program.appDAM.emisor = emisorSeleccionado;
                Program.appDAM.estadoApp = EstadoApp.Conectado;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un emisor válido.");
            }
        }

        private void FrmSeleccionarEmisor_FormClosing(object sender, FormClosingEventArgs e)
        {
            _tablaEmisores.Liberar();
            _tablaEmisores = null;
        }
    }
}
