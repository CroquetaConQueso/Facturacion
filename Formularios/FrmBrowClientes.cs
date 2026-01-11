using FacturacionDAM.Modelos;
using FacturacionDAM.Utils;
using MySql.Data.MySqlClient;
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
    public partial class FrmBrowClientes : Form
    {
        private Tabla _tabla;                                     // Tabla a gestionar
        private BindingSource _bs = new BindingSource();          // BindingSource para comunicar con controles.
        private Dictionary<int, string> _provincias;              // Diccionario para la búsqueda de provincias.

        /// <summary>
        /// Constructor.
        /// </summary>
        public FrmBrowClientes()
        {
            InitializeComponent();
            _provincias = new Dictionary<int, string>();
        }


        /*********** MOVIMIENTO POR LOS REGISTROS DEL DATAGRIDVIEW ***************/

        private void tsBtnFirst_Click(object sender, EventArgs e) => _bs.MoveFirst();

        private void tsBtnPrev_Click(object sender, EventArgs e) => _bs.MovePrevious();

        private void tsBtnNext_Click(object sender, EventArgs e) => _bs.MoveNext();

        private void tsBtnLast_Click(object sender, EventArgs e) => _bs.MoveLast();


        /****************** EVENTOS CRUD DE LA TABLA *********************/

        /// <summary>
        /// Evento "Click" sobre el botón de nuevo registro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnNew_Click(object sender, EventArgs e)
        {
            _bs.AddNew();
            FrmCliente frm = new FrmCliente(_bs, _tabla);
            frm.edicion = false;
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                _tabla.Refrescar();
                ActualizarEstado();
            }
        }

        /// <summary>
        /// Evento "Click" sobre el botón de edición.
        /// </summary>
        private void tsBtnEdit_Click(object sender, EventArgs e)
        {
            if (_bs.Current is DataRowView row)
            {
                FrmCliente frm = new FrmCliente(_bs, _tabla);
                frm.edicion = true;
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    _tabla.Refrescar();
                    ActualizarEstado();
                }
            }
        }

        /// <summary>
        /// Evento "doble click" sobre del DataGridView. Realiza la misma acción que el botón de edición.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgTabla_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            tsBtnEdit_Click(sender, e);
        }

        /// <summary>
        /// Evento "Click" del Botón eliminar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDelete_Click(object sender, EventArgs e)
        {
            if (_bs.Current is DataRowView row)
            {
                string mNif = row["nifcif"].ToString();

                if (TieneFacturasEmitidas(mNif))
                {
                    MessageBox.Show("No se puede eliminar el Cliente porque tiene facturas emitidas.");
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

        /// <summary>
        /// Formatea la columna de provincias.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgTabla_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgTabla.Columns[e.ColumnIndex].Name == "idprovincia")
            {
                if (e.Value is int idProvincia)
                {
                    e.Value = ObtenerNombreProvincia(idProvincia);
                    e.FormattingApplied = true;
                }
            }
        }

        /// <summary>
        /// EVento "Load" del formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBrowClientes_Load(object sender, EventArgs e)
        {
            _tabla = new Tabla(Program.appDAM.LaConexion);

            /*
             *  string mSql = "SELECT e.id, e.nifcif, e.nombre, e.apellido, e.nombrecomercial," +
                "e.domicilio, e.codigopostal, e.idprovincia, p.nombreprovincia AS provincia," +
                "e.telefono1, e.telefono2, e.email, e.descripcion " +
                "FROM Clientes e JOIN provincias p ON e.idprovincia = p.id";
            */
            string mSql = "SELECT * FROM clientes";

            if (_tabla.InicializarDatos(mSql))
            {
                _bs.DataSource = _tabla.LaTabla;
                dgTabla.DataSource = _bs;

                CargarProvincias();

                PersonalizarDataGrid();
            }
            else
                MessageBox.Show("No se pudieron cargar los clientes.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            ActualizarEstado();
        }

        /// <summary>
        /// Evento "FormClosing" del formulario. Lo usaré para guardar el estado del ventana, y así poder recuperarlo la próxima vez.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBrowClientes_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfiguracionVentana.Guardar(this, "BrowClientes");
        }


        /// <summary>
        /// Evento que se lanza la primera vez que se renderiza el formulario. Lo utilizo para restarurar
        /// el estado de la ventana.
        /// </summary>
        private void FrmBrowClientes_Shown(object sender, EventArgs e)
        {
            ConfiguracionVentana.Restaurar(this, "BrowClientes");
        }


        /// <summary>
        /// Exportación de los datos del DataGridView a un archivo con formato CSV.
        /// </summary>
        private void tsBtnExportCSV_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archivo CSV (*.csv)|*.csv";
            if (sfd.ShowDialog() == DialogResult.OK)
                ExportarDatos.ExportarCSV((DataTable)_bs.DataSource, sfd.FileName);
                //Exportar_A_CSV(sfd.FileName);
        }

        /// <summary>
        /// Exportación de los datos del DataGridView a un archivo con formato XML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExportXML_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archivo XML (*.xml)|*.xml";
            if (sfd.ShowDialog() == DialogResult.OK)
                ExportarDatos.ExportarXML((DataTable)_bs.DataSource, sfd.FileName, "Clientes");
                    //Exportar_A_XML(sfd.FileName);
        }

        /************************* MÉTODO PRIVADOS ******************************/

        /// <summary>
        /// Actualizo la barra de estado.
        /// </summary>
        private void ActualizarEstado()
        {
            tsLbNumReg.Text = $"Nº de registros: {_bs.Count}";
        }

        /// <summary>
        /// Personaliza columnas y cabeceras del datagridview.
        /// </summary>
        private void PersonalizarDataGrid()
        {
            dgTabla.Columns["id"].Visible = false;
            dgTabla.Columns["telefono2"].Visible = false;
            dgTabla.Columns["domicilio"].Visible = false;

            dgTabla.Columns["nifcif"].HeaderText = "NIF/CIF";
            dgTabla.Columns["nifcif"].Width = 100;
            dgTabla.Columns["nombre"].HeaderText = "Nombre";
            dgTabla.Columns["nombre"].Width = 120;
            dgTabla.Columns["apellidos"].HeaderText = "Apellidos";
            dgTabla.Columns["apellidos"].Width = 160;
            dgTabla.Columns["nombrecomercial"].HeaderText = "Nombre Comercial";
            dgTabla.Columns["nombrecomercial"].Width = 200;
            dgTabla.Columns["codigopostal"].HeaderText = "C.P.";
            dgTabla.Columns["codigopostal"].Width = 75;
            dgTabla.Columns["idprovincia"].HeaderText = "Provincia";
            dgTabla.Columns["idprovincia"].Width = 150;
            dgTabla.Columns["telefono1"].HeaderText = "Teléfono 1";
            dgTabla.Columns["telefono1"].Width = 100;
            dgTabla.Columns["email"].HeaderText = "Correo electrónico";
            dgTabla.Columns["email"].Width = 250;

            // Estilo para la cabecera:
            dgTabla.EnableHeadersVisualStyles = false;
            dgTabla.ColumnHeadersHeight = 34;
            dgTabla.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 240, 240, 240);
            dgTabla.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(255, 33, 33, 33);
            dgTabla.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            // Colorear filas alternas
            dgTabla.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 230, 255, 255);
        }

        /// <summary>
        /// Carga todas las provincias en el diccionario _provincias
        /// </summary>
        private void CargarProvincias()
        {
            // Cargar provincias en memoria
            using var cmd = new MySqlCommand("SELECT id, nombreprovincia FROM provincias", Program.appDAM.LaConexion);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string nombre = reader.GetString(1);
                _provincias[id] = nombre;
            }
        }

        /// <summary>
        /// Obtiene el nombre de provincia asociado al id pasado como parámetro, en la tabla de provincias.
        /// </summary>
        /// <param name="id">El campo "id" de la provincia a buscar.</param>
        /// <returns></returns>
        private string ObtenerNombreProvincia(int id)
        {
            return _provincias.TryGetValue(id, out var nombre) ? nombre : "";
        }

        /// <summary>
        /// Comprueba si el cliente cuyo nif/cif se pasa como parámetro tiene facturas emitidas.
        /// </summary>
        /// <param name="aNifCif">El nif/cif del cliente a comprobar.</param>
        /// <returns>Retorna true si tiene facturas, false si no.</returns>
        private bool TieneFacturasEmitidas(string aNifCif)
        {
            return false;
        }

    }
}
