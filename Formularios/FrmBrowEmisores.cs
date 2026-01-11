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
    public partial class FrmBrowEmisores : Form
    {
        private Tabla _tabla;                                     // Tabla a gestionar
        private BindingSource _bs = new BindingSource();          // BindingSource para comunicar con controles.
        private Dictionary<int, string> _provincias;              // Diccionario para la búsqueda de provincias.

        /// <summary>
        /// Constructor.
        /// </summary>
        public FrmBrowEmisores()
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
            FrmEmisor frm = new FrmEmisor(_bs, _tabla);
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnEdit_Click(object sender, EventArgs e)
        {
            if (_bs.Current is DataRowView row)
            {
                FrmEmisor frm = new FrmEmisor(_bs, _tabla);
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


                string mNif = row["nifcif"].ToString();

                if (EsEmisorSeleccionado(mNif))
                {
                    MessageBox.Show("No se puede eliminar el emisor actualmente seleccionado.");
                    return;
                }

                if (TieneFacturasEmitidas(mNif))
                {
                    MessageBox.Show("No se puede eliminar el emisor porque tiene facturas emitidas.");
                    return;
                }

                if (TieneFacturasRecibidas(mNif))
                {
                    MessageBox.Show("No se puede eliminar el emisor porque tiene facturas recibidas.");
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
        private void FrmBrowEmisores_Load(object sender, EventArgs e)
        {
            _tabla = new Tabla(Program.appDAM.LaConexion);

            /*
             *  string mSql = "SELECT e.id, e.nifcif, e.nombre, e.apellidos, e.nombrecomercial," +
                "e.domicilio, e.codigopostal, e.idprovincia, p.nombreprovincia AS provincia," +
                "e.telefono1, e.telefono2, e.email, e.descripcion " +
                "FROM emisores e JOIN provincias p ON e.idprovincia = p.id";
            */
            string mSql = "SELECT * FROM emisores";

            if (_tabla.InicializarDatos(mSql))
            {
                _bs.DataSource = _tabla.LaTabla;
                dgTabla.DataSource = _bs;

                CargarProvincias();

                PersonalizarDataGrid();
            }
            else
                MessageBox.Show("No se pudieron cargar los emisores.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            ActualizarEstado();
        }

        /// <summary>
        /// Evento "FormClosing" del formulario. Lo usaré para guardar el estado del ventana, y así poder recuperarlo la próxima vez.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBrowEmisores_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfiguracionVentana.Guardar(this, "BrowEmisores");
        }


        /// <summary>
        /// Evento que se lanza la primera vez que se renderiza el formulario. Lo utilizo para restarurar
        /// el estado de la ventana.
        /// </summary>
        private void FrmBrowEmisores_Shown(object sender, EventArgs e)
        {
            ConfiguracionVentana.Restaurar(this, "BrowEmisores");
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
                ExportarDatos.ExportarXML((DataTable)_bs.DataSource, sfd.FileName, "Emisores");
        }

        /************************* MÉTODO PRIVADOS ******************************/

        /// <summary>
        /// Guarda el estado de la ventana (Estado, Posición y Tamaño).
        /// </summary>
        private void GuardarEstadoVentana()
        {
            ConfiguracionVentana.Guardar(this, "BrowEmisores");
            /*
            // Guardar tamaño y posición solo si la ventana está en estado normal
            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.BrowEmisoresSize = this.Size;
                Properties.Settings.Default.BrowEmisoresLocation = this.Location;
            }

            // Guardar el estado de la ventana como string ("Normal", "Maximized", etc.)
            Properties.Settings.Default.BrowEmisoresState = this.WindowState.ToString();

            Properties.Settings.Default.Save();
            */
        }

        /// <summary>
        /// Inicializa el formulario, restaurando el último estado de la ventana (Estado, Posición y Tamaño).
        /// </summary>
        private void RestaurarEstadoVentana()
        {
            ConfiguracionVentana.Restaurar(this, "BrowEmisores");
            /*
            // Restaurar el estado
            string estado = Properties.Settings.Default.BrowEmisoresState;
            switch (estado)
            {
                case "Maximized":
                    this.WindowState = FormWindowState.Maximized;
                    break;
                case "Minimized":
                    this.WindowState = FormWindowState.Minimized;
                    break;
                default:
                    this.WindowState = FormWindowState.Normal;
                    Properties.Settings.Default.BrowEmisoresSize = this.Size;
                    break;
            }

            // Aplicar ubicación y tamaño antes de que se cree el handle
            this.StartPosition = FormStartPosition.Manual;

            // Restaurar tamaño y posición solo si estaba en estado normal
            if (Properties.Settings.Default.BrowEmisoresState == "Normal")
            {
                this.Size = Properties.Settings.Default.BrowEmisoresSize;
                this.Location = Properties.Settings.Default.BrowEmisoresLocation;
            }
            */
        }

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
            dgTabla.Columns["descripcion"].Visible = false;
            dgTabla.Columns["domicilio"].Visible = false;
            dgTabla.Columns["nextnumfac"].Visible = false;
            dgTabla.Columns["prefixfac"].Visible = false;

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
        /// Comprueba si el Nif/Cif pasado como parámetro se corresponde con el Nif/Cif del emisor seleccionado
        /// </summary>
        /// <param name="aNifCif">Nif/Cif a verficar</param>
        /// <returns>Retorn true si el Nif/Cif se corresponde con el emisor seleccionado, fasle en otro caso.</returns>
        private bool EsEmisorSeleccionado(string aNifCif)
        {
            return (Program.appDAM.emisor != null) && (Program.appDAM.emisor.nifcif == aNifCif);
        }

        /// <summary>
        /// Comprueba si el emisor cuyo nif/cif se pasa como parámetro tiene facturas emitidas.
        /// </summary>
        /// <param name="aNifCif">El nif/cif del emisor a comprobar.</param>
        /// <returns>Retorna true si tiene facturas, false si no.</returns>
        private bool TieneFacturasEmitidas(string aNifCif)
        {
            return false;
        }

        /// <summary>
        /// Comprueba si la persona o empresa cuyo nif/cif se pasa como parámetro tiene facturas recibidas.
        /// </summary>
        /// <param name="aNifCif">El nif/cif de la persona o empresa a comprobar.</param>
        /// <returns>Retorna true si tiene facturas, false si no.</returns>

        private bool TieneFacturasRecibidas(string aNifCif)
        {
            return false;
        }

        /// <summary>
        /// Exporta los datos del DataGridView al archivo cuya ruta se pasa como parámetro.
        /// </summary>
        /// <param name="rutaArchivo">Nombre (incluyendo su ruta) del archivo en el que guardar
        /// los datos.</param>
        private void Exportar_A_CSV(string rutaArchivo)
        {
            try
            {
                DataTable dt = (DataTable)_bs.DataSource;
                List<string> lineas = new List<string>();

                // Cabecera
                // dt.Columns es de tipo DataColumnCollection, pero no directamente enumerable
                // razón por la cual se usa "Cast", para convertirlo en un objecto IEnumerable<DataColumn>
                // y poder extraer el nombre de cada columna en un IEnumerable<string>
                var cabecera = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
                lineas.Add(string.Join(";", cabecera));

                /*
                 * Lo anterior podría haberlo hecho así:
                    
                    List<string> nombresColumnas = new List<string>();

                    foreach (DataColumn col in dt.Columns)
                        nombresColumnas.Add(col.ColumnName);

                    lineas.Add(string.Join(";", nombresColumnas));

                */

                // Filas
                foreach (DataRow row in dt.Rows)
                {
                    // row.ItemArray obtiene una array de objetos con los valores de cada celda de la
                    // fila actual. Si el contenido de la celda no es nulo, lo convierte a cadena
                    // y luego remplza ";" por ","
                    var valores = row.ItemArray.Select(v => v?.ToString()?.Replace(";", ","));
                    lineas.Add(string.Join(";", valores));
                }

                /* El anterior bucle también podría haberse hecho en un doble bucle así:
                    foreach (DataRow row in dt.Rows)
                    {
                        List<string> valoresFila = new List<string>();

                        foreach (var valor in row.ItemArray)
                        {
                            string valorLimpio = valor?.ToString()?.Replace(";", ",") ?? "";
                            valoresFila.Add(valorLimpio);
                        }

                        lineas.Add(string.Join(";", valoresFila));
                    }
                */ 

                File.WriteAllLines(rutaArchivo, lineas, Encoding.UTF8);
                MessageBox.Show("Datos exportados a CSV correctamente");
            }
            catch (Exception ex)
            {
                Program.appDAM.RegistrarLog("Exportacion a CSV", ex.Message);
                MessageBox.Show("Se ha producido un error al exportar los datos.");
            }
        }

        /// <summary>
        /// Exportar los datos del DataGridView a formato XML.
        /// </summary>
        /// <param name="rutaArchivo">Nombre del archivo (incluyendo su ruta) en el que guardar los datos.</param>
        private void Exportar_A_XML(string rutaArchivo)
        {
            try
            {
                DataTable dt = (DataTable)_bs.DataSource;
                dt.TableName = "Emisores"; // O clientes, o proveedores, o ...
                dt.WriteXml(rutaArchivo, XmlWriteMode.WriteSchema);
                MessageBox.Show("Datos exportados a XML correctamente");
            }
            catch (Exception ex)
            {
                Program.appDAM.RegistrarLog("Exportacion a XML", ex.Message);
                MessageBox.Show("Se ha producido un error al exportar los datos.");
            }
        }

    }
}
