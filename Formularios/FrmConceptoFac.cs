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
    public partial class FrmConceptoFac : Form
    {
        private Tabla _tabla;
        private BindingSource _bs;

        public bool edicion;


        public FrmConceptoFac(BindingSource bs, Tabla tabla)
        {
            InitializeComponent();
            _bs = bs;
            bsEmisor.DataSource = bs;
            _tabla = tabla;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos())
                return;

            _bs.EndEdit();            // Finaliza edición del registro actual
            _tabla.GuardarCambios();  // Se propaga a la BD
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            _bs.CancelEdit(); // Cancela cambios si es nueva fila
            this.Close();
        }


        private void FrmConceptoFac_Load(object sender, EventArgs e)
        {
            txtDescripcion.DataBindings.Add("Text", _bs, "descripcion");
            txtCodigo.DataBindings.Add("Text", _bs, "codigo");
        }

        private bool ValidarDatos()
        {

            // Validación 1: "Código" y "Descripción" no pueden estar vacíos
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("El campo 'Descripción' no puede estar vacío.");
                txtDescripcion.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCodigo.Text.Trim()))
            {
                MessageBox.Show("El campo 'Código' no puede estar vacío.");
                return false;
            }

            // Validación 3: El nif ya existe.
            if (CodigoDuplicado(txtCodigo.Text.Trim())) { 
                MessageBox.Show("El 'Código' introducido ya existe en otro registro.");
                txtCodigo.Focus();
                return false;
            }

            return true;
            
        }

        /// <summary>
        /// Verifica si el campo "codigo" pasado como parámetro ya existe en la tabla. Si estamos
        /// en modo edición, busca en todos los registros que no coincida con el actual.
        /// </summary>
        /// <param name="aCodigo">El campo "codigo" a verificar.</param>
        /// <returns>Retorna true si existe, false sino.</returns>
        private bool CodigoDuplicado(string aCodigo)
        {
            if (edicion && _bs.Current is DataRowView row && row["id"] is int id)
                return !Validaciones.EsValorCampoUnico("conceptosfac", "codigo", aCodigo, id);

            return !Validaciones.EsValorCampoUnico("conceptosfac", "codigo", aCodigo);
        }


    }
}
