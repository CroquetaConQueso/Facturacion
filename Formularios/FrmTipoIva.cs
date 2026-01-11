using FacturacionDAM.Modelos;
using FacturacionDAM.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacturacionDAM.Formularios
{
    public partial class FrmTipoIva : Form
    {
        private Tabla _tabla;
        private BindingSource _bs;

        public bool edicion;


        public FrmTipoIva(BindingSource bs, Tabla tabla)
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


        private void FrmTipoIva_Load(object sender, EventArgs e)
        {
            txtDescripcion.DataBindings.Add("Text", _bs, "descripcion");
            numUpDownPorcentaje.DataBindings.Add("Value", _bs, "porcentaje", true, DataSourceUpdateMode.OnPropertyChanged, 0.0);
            chkActivo.DataBindings.Add("Checked", _bs, "activo", true, DataSourceUpdateMode.OnPropertyChanged, false);
        }

        /*************** MÉTODOS PERSONALES *************************/

        private bool ValidarDatos()
        {
            try
            {
                // Validación 1: "Tipo" y "Descripción" no pueden estar vacíos
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show("El campo 'Descripción' no puede estar vacío.");
                    txtDescripcion.Focus();
                    return false;
                }

                if ( (numUpDownPorcentaje.Value < 0) || (numUpDownPorcentaje.Value >= 100))
                {
                    MessageBox.Show("El campo 'Tipo' tiene que tener un valor entre 0 y 99,99.");
                    numUpDownPorcentaje.Focus();
                    return false;
                }

                // Validación 3: El tipo ya existe en un tipo de iva activo.
                if (TipoDuplicado())
                {
                    MessageBox.Show(
                            "Ya existe un tipo de IVA activo con ese porcentaje.",
                            "Duplicado",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning
                        );
                    numUpDownPorcentaje.Focus();
                    return false;
                }

                return true;
            }
            catch (Exception ex)  {
                MessageBox.Show(
                    "Se ha producido un error al validar el tipo de IVA",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                Program.appDAM.RegistrarLog("Validando tipo de iva", ex.Message);

                return false;

            }

        }

        /// <summary>
        /// Verifica si el campo "porcentaje" pasado como parámetro ya existe en la tabla, para otro tipo
        /// de iva activo. Si estamos en modo edición, busca en todos los registros que no coincida con el actual.
        /// </summary>
        /// <returns>Retorna true si existe, false sino.</returns>
        private bool TipoDuplicado() {

            decimal porcentaje = numUpDownPorcentaje.Value;

            // Si es un nuevo registro idActual será 0
            int idActual = 0;
            if (_bs.Current is DataRowView row) {
                if (row["id"] != DBNull.Value)
                    idActual = Convert.ToInt32(row["id"]);
            }

            string sql = @"SELECT COUNT(*)  FROM tiposiva WHERE (porcentaje = @p)
                         AND (activo = TRUE) AND (id <> @idActual)";

            using (var cmd = new MySqlCommand(sql, Program.appDAM.LaConexion)) {
                cmd.Parameters.AddWithValue("@p", porcentaje);
                cmd.Parameters.AddWithValue("@idActual", idActual);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                return (count > 0);
            }
        }

        /// <summary>
        /// Me aseguro que no envía valores nulos a la base de datos para los campos "porcentaje"
        /// y "activo".
        /// </summary>
        private void ForzarValoresNoNulos()
        {
            if (_bs.Current is DataRowView row)
            {
                // Porcentaje nunca puede ser nulo
                if (row["porcentaje"] == DBNull.Value)
                    row["porcentaje"] = numUpDownPorcentaje.Value;

                // Activo nunca puede ser nulo
                if (row["activo"] == DBNull.Value)
                    row["activo"] = chkActivo.Checked ? 1 : 0;
            }
        }


    }
}
