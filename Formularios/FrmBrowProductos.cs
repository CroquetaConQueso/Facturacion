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
    public partial class FrmBrowProductos : Form
    {
        private Tabla _tabla;                                     // Tabla a gestionar
        private BindingSource _bs = new BindingSource();          // BindingSource para comunicar con controles.
        private Dictionary<int, decimal> _tiposiva;              // Diccionario para la búsqueda de tipos de iva.

        /// <summary>
        /// Constructor.
        /// </summary>
        public FrmBrowProductos()
        {
            InitializeComponent();
            _tiposiva = new Dictionary<int, decimal>();
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
            FrmProducto frm = new FrmProducto(_bs, _tabla);
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
                FrmProducto frm = new FrmProducto(_bs, _tabla);
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
                int mId = Convert.ToInt32(row["id"]);

                if (TieneFacturasEmitidas(mId))
                {
                    MessageBox.Show("No se puede eliminar el Producto porque tiene facturas emitidas.");
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
        /// Formatea la columna tipo de iva.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgTabla_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgTabla.Columns[e.ColumnIndex].Name == "idtipoiva")
            {
                if (e.Value is int idTipoIva)
                {
                    e.Value = ObtenerTipoIvaFormateado(idTipoIva);
                    e.FormattingApplied = true;
                }
            }
        }

        /// <summary>
        /// EVento "Load" del formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBrowProductos_Load(object sender, EventArgs e)
        {
            _tabla = new Tabla(Program.appDAM.LaConexion);

            string mSql = "SELECT * FROM productos";

            if (_tabla.InicializarDatos(mSql))
            {
                _bs.DataSource = _tabla.LaTabla;
                dgTabla.DataSource = _bs;

                CargarTiposDeIva();

                PersonalizarDataGrid();
            }
            else
                MessageBox.Show("No se pudieron cargar los productos.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            ActualizarEstado();
        }

        /// <summary>
        /// Evento "FormClosing" del formulario. Lo usaré para guardar el estado del ventana, y así poder recuperarlo la próxima vez.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBrowProductos_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfiguracionVentana.Guardar(this, "BrowProductos");
        }


        /// <summary>
        /// Evento que se lanza la primera vez que se renderiza el formulario. Lo utilizo para restarurar
        /// el estado de la ventana.
        /// </summary>
        private void FrmBrowProductos_Shown(object sender, EventArgs e)
        {
            ConfiguracionVentana.Restaurar(this, "BrowProductos");
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
                ExportarDatos.ExportarXML((DataTable)_bs.DataSource, sfd.FileName, "Productos");
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

            dgTabla.Columns["codigo"].HeaderText = "Código";
            dgTabla.Columns["codigo"].Width = 100;
            dgTabla.Columns["descripcion"].HeaderText = "Descripción";
            dgTabla.Columns["descripcion"].Width = 220;
            dgTabla.Columns["preciounidad"].HeaderText = "Precio/Ud.";
            dgTabla.Columns["preciounidad"].Width = 120;
            dgTabla.Columns["preciounidad"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTabla.Columns["preciounidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTabla.Columns["preciounidad"].DefaultCellStyle.Padding = new Padding(0, 0, 10, 0);
            dgTabla.Columns["idtipoiva"].HeaderText = "Tipo de IVA";
            dgTabla.Columns["idtipoiva"].Width = 120;
            dgTabla.Columns["idtipoiva"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTabla.Columns["idtipoiva"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTabla.Columns["idtipoiva"].DefaultCellStyle.Padding = new Padding(0, 0, 10, 0);
            dgTabla.Columns["activo"].HeaderText = "¿Activo?";
            dgTabla.Columns["activo"].Width = 75;
            dgTabla.Columns["activo"].ReadOnly = true;
            dgTabla.Columns["activo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgTabla.Columns["activo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
        private void CargarTiposDeIva()
        {
            // Cargar provincias en memoria
            using var cmd = new MySqlCommand("SELECT id, porcentaje FROM tiposiva", Program.appDAM.LaConexion);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                decimal tipo = reader.GetDecimal(1); //reader.GetString(1);
                _tiposiva[id] = tipo;
            }
        }

        /// <summary>
        /// Obtiene el tipo de iva asociado al id pasado como parámetro, en la tabla de tipos de iva.
        /// </summary>
        /// <param name="id">El campo "id" del tipo de iva a buscar.</param>
        /// <returns></returns>
        private string ObtenerTipoIvaFormateado(int id)
        {
            decimal mValue = _tiposiva.TryGetValue(id, out var tipo) ? tipo : -1;
            // Retorna la caddena vacía, o el valor decimal convertido a string, formateado con dos decimales.
            return (mValue == -1) ? "" : mValue.ToString("F2");
        }

        /// <summary>
        /// Comprueba si el producto cuyo id se pasa como parámetro tiene facturas emitidas.
        /// </summary>
        /// <param name="aId">El id del producto a comprobar.</param>
        /// <returns>Retorna true si tiene facturas, false si no.</returns>
        private bool TieneFacturasEmitidas(int aId)
        {
            return false;
        }

    }
}
