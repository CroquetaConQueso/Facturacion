using FacturacionDAM.Modelos;
using FacturacionDAM.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacturacionDAM.Formularios
{
    public partial class FrmBrowTiposIva : Form
    {
        private Tabla _tabla;                                     // Tabla a gestionar
        private BindingSource _bs = new BindingSource();          // BindingSource para comunicar con controles.

        /// <summary>
        /// Constructor.
        /// </summary>
        public FrmBrowTiposIva()
        {
            InitializeComponent();
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
            FrmTipoIva frm = new FrmTipoIva(_bs, _tabla);
            //_tabla.LaTabla.Columns["porcentaje"].DefaultValue = 0.0;
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
                FrmTipoIva frm = new FrmTipoIva(_bs, _tabla);
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
                    MessageBox.Show("No se puede eliminar el Tipo de Iva porque tiene facturas emitidas.");
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
        /// EVento "Load" del formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBrowTiposIva_Load(object sender, EventArgs e)
        {
            _tabla = new Tabla(Program.appDAM.LaConexion);

            string mSql = "SELECT * FROM tiposiva";

            if (_tabla.InicializarDatos(mSql))
            {
                _bs.DataSource = _tabla.LaTabla;
                dgTabla.DataSource = _bs;
                _tabla.LaTabla.Columns["activo"].DefaultValue = false;

                PersonalizarDataGrid();
            }
            else
                MessageBox.Show("No se pudieron cargar los tipos de iva.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            ActualizarEstado();
        }

        /// <summary>
        /// Evento "FormClosing" del formulario. Lo usaré para guardar el estado del ventana, y así poder recuperarlo la próxima vez.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBrowTiposIva_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfiguracionVentana.Guardar(this, "BrowTiposIva");
        }


        /// <summary>
        /// Evento que se lanza la primera vez que se renderiza el formulario. Lo utilizo para restarurar
        /// el estado de la ventana.
        /// </summary>
        private void FrmBrowTiposIva_Shown(object sender, EventArgs e)
        {
            ConfiguracionVentana.Restaurar(this, "BrowTiposIva");
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
                ExportarDatos.ExportarXML((DataTable)_bs.DataSource, sfd.FileName, "Tipos de IVA");
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

            dgTabla.Columns["porcentaje"].HeaderText = "Tipo";
            dgTabla.Columns["porcentaje"].Width = 60;
            dgTabla.Columns["porcentaje"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTabla.Columns["porcentaje"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTabla.Columns["porcentaje"].DefaultCellStyle.Padding = new Padding(0, 0, 10, 0);
            dgTabla.Columns["descripcion"].HeaderText = "Descripción";
            dgTabla.Columns["descripcion"].Width = 180;
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
        /// Comprueba si el tipo de iva cuyo id se pasa como parámetro tiene facturas emitidas.
        /// </summary>
        /// <param name="aId">El id del tipo de iva a comprobar.</param>
        /// <returns>Retorna true si tiene facturas, false si no.</returns>
        private bool TieneFacturasEmitidas(int aId)
        {
            return false;
        }

    }
}
