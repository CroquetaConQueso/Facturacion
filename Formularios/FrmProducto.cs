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
    public partial class FrmProducto : Form
    {
        private Tabla _tabla;
        private BindingSource _bs;

        public bool edicion;


        public FrmProducto(BindingSource bs, Tabla tabla)
        {
            InitializeComponent();
            _bs = bs;
            _tabla = tabla;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos())
                return;

            ForzarValoresNoNulos();

            _bs.EndEdit();            // Finaliza edición del registro actual

            _tabla.GuardarCambios();  // Se propaga a la BD
            _tabla.Refrescar();
            _bs.ResetBindings(false);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            _bs.CancelEdit(); // Cancela cambios si es nueva fila
            this.Close();
        }


        private void FrmProducto_Load(object sender, EventArgs e)
        {
            txtCodigo.DataBindings.Add("Text", _bs, "codigo");
            txtDescripcion.DataBindings.Add("Text", _bs, "descripcion");

            numUpDownPrecio.DataBindings.Add("Value", _bs, "preciounidad", true, DataSourceUpdateMode.OnPropertyChanged, 0.0);
            chkActivo.DataBindings.Add("Checked", _bs, "activo", true, DataSourceUpdateMode.OnPropertyChanged, false);
            
            // Cargar Tipos de IVA en el ComboBox
            Tabla tablaTiposIva = new Tabla(Program.appDAM.LaConexion);
            tablaTiposIva.InicializarDatos("SELECT * FROM tiposiva where activo=1");
            cbTipoIva.DataSource = tablaTiposIva.LaTabla;
            cbTipoIva.DisplayMember = "porcentaje";
            cbTipoIva.ValueMember = "id";
            cbTipoIva.DataBindings.Add("SelectedValue", _bs, "idtipoiva");
        }


        /******************* MÉTODOS PERSONALIZADOS *************************/

        private bool ValidarDatos()
        {

            // Validación 1: Código y descripción no pueden estar vacíos
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("El campo 'Código' no puede estar vacío.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("El campo 'Descripción' no puede estar vacío.");
                return false;
            }

            // Validación 2: El código ya existe.
            if (CodigoDuplicado(txtCodigo.Text.Trim())) { 
                MessageBox.Show("El 'Código' de producto introducido ya existe en otro registro.");
                return false;
            }

            return true;
            
        }

        /// <summary>
        /// Verifica si el "código" pasado como parámetro ya existe en la tabla. Si estamos
        /// en modo edición, busca en todos los registros que no coincida con el actual.
        /// </summary>
        /// <param name="aCodigo">El código a verificar.</param>
        /// <returns>Retorna true si existe, false sino.</returns>
        private bool CodigoDuplicado(string aCodigo)
        {
            if (edicion && _bs.Current is DataRowView row && row["id"] is int id)
                return !Validaciones.EsValorCampoUnico("productos", "codigo", aCodigo, id);

            return !Validaciones.EsValorCampoUnico("productos", "codigo", aCodigo);
        }



        /// <summary>
        /// Me aseguro que no envía valores nulos a la base de datos para los campos "preciounidad"
        /// y "activo".
        /// </summary>
        private void ForzarValoresNoNulos()
        {
            if (_bs.Current is DataRowView row)
            {
                // Porcentaje nunca puede ser nulo
                if (row["preciounidad"] == DBNull.Value)
                    row["preciounidad"] = numUpDownPrecio.Value;

                // Activo nunca puede ser nulo
                if (row["activo"] == DBNull.Value)
                    row["activo"] = chkActivo.Checked ? 1 : 0;
            }
        }

    }
}
