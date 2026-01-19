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
    public partial class FrmProveedor : Form
    {
        private Tabla _tabla;
        private BindingSource _bs;

        public bool edicion;

        public FrmProveedor(BindingSource bs, Tabla tabla)
        {
            InitializeComponent();
            _bs = bs;
            _tabla = tabla;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos())
                return;

            _bs.EndEdit();
            _tabla.GuardarCambios();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            _bs.CancelEdit();
            this.Close();
        }

        private void FrmProveedor_Load(object sender, EventArgs e)
        {
            // Obtener la tabla actual del BindingSource (DataTable o DataView)
            DataTable dt = _bs.DataSource as DataTable;
            if (dt == null)
            {
                DataView dv = _bs.DataSource as DataView;
                if (dv != null) dt = dv.Table;
            }

            string Columna(params string[] nombres)
            {
                if (dt == null) return string.Empty;

                foreach (string n in nombres)
                {
                    if (dt.Columns.Contains(n)) return n;
                }

                return string.Empty;
            }

            void BindText(TextBox tb, params string[] columnas)
            {
                string col = Columna(columnas);
                if (string.IsNullOrWhiteSpace(col)) return;

                tb.DataBindings.Clear();
                tb.DataBindings.Add("Text", _bs, col);
            }

            BindText(txtNifCif, "nifcif");
            BindText(txtNombre, "nombre");
            BindText(txtApellidos, "apellidos");
            BindText(txtNombreComercial, "nombrecomercial");

            // Compatibilidad: BD antigua (domicilio/codigopostal) y BD nueva (direccion/cpostal)
            BindText(txtDomicilio, "domicilio", "direccion");
            BindText(txtPob, "poblacion");
            BindText(txtCp, "codigopostal", "cpostal");

            BindText(txtTel1, "telefono1");
            BindText(txtTel2, "telefono2");
            BindText(txtEmail, "email");

            Tabla tablaProvincias = new Tabla(Program.appDAM.LaConexion);
            tablaProvincias.InicializarDatos("SELECT * FROM provincias");
            cbProv.DataSource = tablaProvincias.LaTabla;
            cbProv.DisplayMember = "nombreprovincia";
            cbProv.ValueMember = "id";

            cbProv.DataBindings.Clear();
            cbProv.DataBindings.Add("SelectedValue", _bs, "idprovincia");
        }

        private bool ValidarDatos()
        {
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

            string email = txtEmail.Text.Trim();
            if (!string.IsNullOrEmpty(email) && !Validaciones.EsEmailValido(email))
            {
                MessageBox.Show("El formato del email no es válido.");
                return false;
            }

            if (NifDuplicado(txtNifCif.Text.Trim()))
            {
                MessageBox.Show("El NIF/CIF introducido ya existe en otro registro.");
                return false;
            }

            return true;
        }

        private bool NifDuplicado(string nifCif)
        {
            if (edicion && _bs.Current is DataRowView row && row["id"] is int id)
                return !Validaciones.EsValorCampoUnico("proveedors", "nifcif", txtNifCif.Text.Trim(), id);

            return !Validaciones.EsValorCampoUnico("proveedors", "nifcif", txtNifCif.Text.Trim());
        }
    }
}
