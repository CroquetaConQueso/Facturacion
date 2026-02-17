using FacturacionDAM.Modelos;
using System;
using System.Data;
using System.Windows.Forms;

/*
 * Módulo: FrmSeleccionarEmisor
 * Propósito: Permite seleccionar el emisor activo de la aplicación (empresa/persona que emite facturas).
 *            Carga emisores desde BD, los muestra en un ComboBox y actualiza el estado global (Program.appDAM).
 *
 * Flujo principal:
 *  1) Al cargar, consulta la tabla 'emisores' y rellena el ComboBox.
 *  2) Cancelar deja la app conectada pero sin emisor seleccionado.
 *  3) Seleccionar crea un objeto Emisor tipado y lo fija como emisor activo.
 *  4) Al cerrar, libera recursos del acceso tabular.
 */

namespace FacturacionDAM.Formularios
{
    public partial class FrmSeleccionarEmisor : Form
    {
        #region Campos y Constructor

        // Acceso a datos tabular contra la BD para recuperar emisores.
        private Tabla _tablaEmisores;

        // BindingSource para enlazar el DataTable con el ComboBox de manera estable.
        private readonly BindingSource _bsEmisores = new BindingSource();

        public FrmSeleccionarEmisor()
        {
            InitializeComponent();
        }

        #endregion

        #region Carga Inicial (BD -> UI)

        // Carga el listado de emisores desde BD y configura el ComboBox.
        private void FrmSeleccionarEmisor_Load(object sender, EventArgs e)
        {
            _tablaEmisores = new Tabla(Program.appDAM.LaConexion);

            // Consulta completa de emisores (se asume que la tabla contiene los campos necesarios).
            if (_tablaEmisores.InicializarDatos("SELECT * FROM emisores"))
            {
                _bsEmisores.DataSource = _tablaEmisores.LaTabla;

                // Enlace del ComboBox:
                // - DisplayMember: lo que ve el usuario
                // - ValueMember: identificador interno
                cbEmisores.DataSource = _bsEmisores;
                cbEmisores.DisplayMember = "nombrecomercial";
                cbEmisores.ValueMember = "id";
            }
            else
            {
                // Error crítico: sin emisores no se puede continuar el flujo normal.
                MessageBox.Show("No se pudieron cargar los emisores.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.appDAM.estadoApp = EstadoApp.Error;
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        #endregion

        #region Acciones UI (Cancelar / Seleccionar)

        // Cancelación explícita: la app queda conectada, pero sin emisor activo.
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Program.appDAM.estadoApp = EstadoApp.ConectadoSinEmisor;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // Selección confirmada: construye Emisor desde la fila y lo fija en el estado global.
        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            // Validación defensiva: debe existir una fila seleccionada en el BindingSource.
            if (_bsEmisores.Current is not DataRowView row)
            {
                MessageBox.Show("Debe seleccionar un emisor válido.");
                return;
            }

            // Construcción tipada del emisor.
            // Se usan helpers Safe* para tolerar NULL/DBNull y conversiones inseguras.
            var emisorSeleccionado = new Emisor
            {
                id = SafeInt(row["id"], -1),
                nifcif = SafeStr(row["nifcif"]),
                nombre = SafeStr(row["nombre"]),
                apellidos = SafeStr(row["apellidos"]),
                nombreComercial = SafeStr(row["nombrecomercial"]),
                nextNumFac = SafeInt(row["nextnumfac"], 1)
            };

            // Persistencia en el estado global de la app.
            Program.appDAM.emisor = emisorSeleccionado;
            Program.appDAM.estadoApp = EstadoApp.Conectado;

            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        #region Helpers de Conversión Segura

        // Convierte a string tolerando null/DBNull.
        private static string SafeStr(object v)
        {
            return v == null || v == DBNull.Value ? "" : v.ToString();
        }

        // Convierte a int tolerando null/DBNull y tipos distintos (string/int).
        // Si no se puede convertir, devuelve el valor por defecto.
        private static int SafeInt(object v, int def)
        {
            if (v == null || v == DBNull.Value) return def;
            if (v is int i) return i;
            return int.TryParse(v.ToString(), out int n) ? n : def;
        }

        #endregion

        #region Limpieza / Liberación de Recursos

        // Al cerrar: libera recursos asociados a Tabla (dataset/reader/command internos, etc.).
        private void FrmSeleccionarEmisor_FormClosing(object sender, FormClosingEventArgs e)
        {
            _tablaEmisores?.Liberar();
            _tablaEmisores = null;
        }

        #endregion
    }
}
