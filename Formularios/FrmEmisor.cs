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
    public partial class FrmEmisor : Form
    {
        private Tabla _tabla;
        private BindingSource _bs;

        public bool edicion;


        public FrmEmisor(BindingSource bs, Tabla tabla)
        {
            InitializeComponent();
            _bs = bs;
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


        private void FrmEmisor_Load(object sender, EventArgs e)
        {
            txtNifCif.DataBindings.Add("Text", _bs, "nifcif");
            txtNombre.DataBindings.Add("Text", _bs, "nombre");
            txtApellidos.DataBindings.Add("Text", _bs, "apellidos");
            txtNombreComercial.DataBindings.Add("Text", _bs, "nombrecomercial");
            txtDomicilio.DataBindings.Add("Text", _bs, "domicilio");
            txtPob.DataBindings.Add("Text", _bs, "poblacion");
            txtCp.DataBindings.Add("Text", _bs, "codigopostal");
            txtTel1.DataBindings.Add("Text", _bs, "telefono1");
            txtTel2.DataBindings.Add("Text", _bs, "telefono2");
            txtEmail.DataBindings.Add("Text", _bs, "email");
            txtNextNumFac.DataBindings.Add("Text", _bs, "nextnumfac");
            txtPrefixFac.DataBindings.Add("Text", _bs, "prefixfac");
            txtDescripcion.DataBindings.Add("Text", _bs, "descripcion");
            
            // Cargar provincias en el ComboBox
            Tabla tablaProvincias = new Tabla(Program.appDAM.LaConexion);
            tablaProvincias.InicializarDatos("SELECT * FROM provincias");
            cbProv.DataSource = tablaProvincias.LaTabla;
            cbProv.DisplayMember = "nombreprovincia";
            cbProv.ValueMember = "id";
            cbProv.DataBindings.Add("SelectedValue", _bs, "idprovincia");
        }

        private bool ValidarDatos()
        {

            // Validación 1: NIF/CIF y nombrecomercial no pueden estar vacíos
            if (string.IsNullOrWhiteSpace(txtNifCif.Text))
            {
                MessageBox.Show("El campo NIF/CIF no puede estar vacío.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombreComercial.Text))
            {
                MessageBox.Show("El campo Nombre Comercial no puede estar vacío.");
                return false;
            }

            // Validación 2: Email válido si se ha introducido
            string email = txtEmail.Text.Trim();
            if (!string.IsNullOrEmpty(email) && !Validaciones.EsEmailValido(email))
            {
                MessageBox.Show("El formato del email no es válido.");
                return false;
            }

            // Validación 3: Ya existe el email.
            if (this.NifDuplicado(txtNifCif.Text.Trim()))
            {
                MessageBox.Show("El NIF/CIF introducido ya existe en otro registro.");
                return false;
            }

            // Si ha llegado aquí, ha pasado todas las validaciones.
            return true;

        }

        /// <summary>
        /// Verifica si el nif/cif pasado como parámetro ya existe en la tabla. Si estamos
        /// en modo edición, busca en todos los registros que no coincida con el actual.
        /// </summary>
        /// <param name="nifCif">El nif/cif a verificar.</param>
        /// <returns>Retorna true si existe, false sino.</returns>
        private bool NifDuplicado(string nifCif)
        {
            if (edicion && _bs.Current is DataRowView row && row["id"] is int id)
                return !Validaciones.EsValorCampoUnico("emisores", "nifcif", txtNifCif.Text.Trim(), id);

            return !Validaciones.EsValorCampoUnico("emisores", "nifcif", txtNifCif.Text.Trim());
        }

    }
}
