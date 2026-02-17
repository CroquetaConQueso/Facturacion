using FacturacionDAM.Modelos;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

/*
 * FrmLineaFacemi
 * Edición de una línea de factura emitida (facemilin).
 *
 * Qué hace:
 * - Permite seleccionar un producto (opcional) y rellena descripción, precio e IVA.
 * - Calcula base, cuota e importe total de la línea (solo visual, la BD guarda base y cuota).
 * - Guarda cambios en la tabla de líneas asociada a la factura actual.
 *
 * Notas:
 * - IVA limitado a 100.00% (validación defensiva).
 * - Se usan banderas (_cargandoDatos/_cargandoProductos/_aplicandoProducto) para evitar bucles de eventos.
 */

namespace FacturacionDAM.Formularios
{
    public partial class FrmLineaFacemi : Form
    {
        public bool edicion = false;

        private readonly BindingSource _bsLineas;
        private readonly Tabla _tablaLineas;
        private readonly int _idFacemi;

        private readonly Tabla _tablaProductos;
        private readonly BindingSource _bsProductos;

        // Banderas para evitar reentradas (eventos disparándose mientras cargamos o aplicamos un producto)
        private bool _cargandoProductos = false;
        private bool _aplicandoProducto = false;
        private bool _cargandoDatos = false;

        public FrmLineaFacemi(BindingSource bsLineas, Tabla tablaLineas, int idFacemi, bool edicion = false)
        {
            InitializeComponent();

            _bsLineas = bsLineas ?? throw new ArgumentNullException(nameof(bsLineas));
            _tablaLineas = tablaLineas ?? throw new ArgumentNullException(nameof(tablaLineas));
            _idFacemi = idFacemi;
            this.edicion = edicion;

            _tablaProductos = new Tabla(Program.appDAM.LaConexion);
            _bsProductos = new BindingSource();

            // Rangos de edición para evitar valores inválidos y excepciones al asignar al NumericUpDown
            numCantidad.Maximum = 999999m;
            numCantidad.Minimum = 0.01m;

            numPrecio.Maximum = 999999m;
            numPrecio.Minimum = 0m;

            numTipoIva.Maximum = 100.00m;
            numTipoIva.Minimum = 0m;

            Load += FrmLineaFacemi_Load;

            btnAceptar.Click += btnAceptar_Click;
            btnCancelar.Click += btnCancelar_Click;

            cbProducto.SelectedValueChanged += cbProducto_SelectedValueChanged;
            BtnProducto.Click += BtnProducto_Click;

            numCantidad.ValueChanged += CamposCalculo_ValueChanged;
            numPrecio.ValueChanged += CamposCalculo_ValueChanged;
            numTipoIva.ValueChanged += CamposCalculo_ValueChanged;

            txtDescripcion.TextChanged += TxtDescripcion_TextChanged;

            cbProducto.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void FrmLineaFacemi_Load(object sender, EventArgs e)
        {
            // Si no hay fila actual en el BindingSource, no hay nada que editar
            if (_bsLineas.Current is not DataRowView drv)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            _cargandoDatos = true;

            try
            {
                // Normaliza la fila (defaults, idfacemi, nulls)
                PrepararFilaLinea(drv);

                // Carga catálogo de productos (combo)
                if (!CargarProductos())
                {
                    MessageBox.Show("No se pudieron cargar los productos.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                // Volcado fila -> controles y cálculo inicial
                CargarControlesDesdeFila(drv);
                RecalcularYVolcar();
            }
            finally
            {
                _cargandoDatos = false;
            }
        }

        private void PrepararFilaLinea(DataRowView drv)
        {
            var row = drv.Row;

            // Asegura que la línea queda asociada a la factura (en nuevas líneas)
            if (row.Table.Columns.Contains("idfacemi"))
            {
                if (row["idfacemi"] == DBNull.Value || Convert.ToInt32(row["idfacemi"]) == 0)
                    row["idfacemi"] = _idFacemi;
            }

            // Defaults para evitar DBNull en cálculos / UI
            SetDefault(row, "cantidad", 1m);
            SetDefault(row, "precio", 0m);
            SetDefault(row, "base", 0m);
            SetDefault(row, "tipoiva", 0m);
            SetDefault(row, "cuota", 0m);
            SetDefault(row, "descripcion", "");

            // idproducto puede ser DBNull si la línea no usa producto del catálogo
            if (row.Table.Columns.Contains("idproducto") && (row["idproducto"] == null))
                row["idproducto"] = DBNull.Value;
        }

        private static void SetDefault(DataRow row, string col, object value)
        {
            if (!row.Table.Columns.Contains(col)) return;
            if (row[col] == DBNull.Value) row[col] = value;
        }

        private bool CargarProductos()
        {
            // Se trae el precio unidad y el % IVA (si hay tipo IVA asociado al producto)
            const string sql = @"
                SELECT
                    p.id,
                    p.descripcion,
                    p.preciounidad,
                    t.porcentaje AS iva_porcentaje
                FROM productos p
                LEFT JOIN tiposiva t ON t.id = p.idtipoiva
                WHERE p.activo = 1
                ORDER BY p.descripcion;";

            if (!_tablaProductos.InicializarDatos(sql))
                return false;

            // Para búsqueda rápida por id (Rows.Find)
            if (_tablaProductos.LaTabla.Columns.Contains("id"))
                _tablaProductos.LaTabla.PrimaryKey = new[] { _tablaProductos.LaTabla.Columns["id"] };

            _cargandoProductos = true;

            try
            {
                _bsProductos.DataSource = _tablaProductos.LaTabla;

                cbProducto.DataSource = _bsProductos;
                cbProducto.DisplayMember = "descripcion";
                cbProducto.ValueMember = "id";

                // Si la línea tiene idproducto, preselecciona
                if (_bsLineas.Current is DataRowView drvLinea)
                {
                    if (drvLinea.Row.Table.Columns.Contains("idproducto") && drvLinea["idproducto"] != DBNull.Value)
                    {
                        cbProducto.SelectedValue = Convert.ToInt32(drvLinea["idproducto"]);
                    }
                    else
                    {
                        cbProducto.SelectedIndex = -1;
                    }
                }
            }
            finally
            {
                _cargandoProductos = false;
            }

            return true;
        }

        private void CargarControlesDesdeFila(DataRowView drv)
        {
            var row = drv.Row;

            txtDescripcion.Text = Convert.ToString(row["descripcion"]) ?? "";

            // Asignación defensiva: evita ArgumentOutOfRangeException en NumericUpDown
            numCantidad.Value = ValidarLimite(numCantidad, ToDecimal(row, "cantidad", 1m));
            numPrecio.Value = ValidarLimite(numPrecio, ToDecimal(row, "precio", 0m));
            numTipoIva.Value = ValidarLimite(numTipoIva, ToDecimal(row, "tipoiva", 0m));

            // Selección producto si existe
            if (row.Table.Columns.Contains("idproducto") && row["idproducto"] != DBNull.Value)
            {
                try { cbProducto.SelectedValue = Convert.ToInt32(row["idproducto"]); }
                catch { cbProducto.SelectedIndex = -1; }
            }
            else
            {
                cbProducto.SelectedIndex = -1;
            }
        }

        private decimal ValidarLimite(NumericUpDown num, decimal valor)
        {
            if (valor < num.Minimum) return num.Minimum;
            if (valor > num.Maximum) return num.Maximum;
            return valor;
        }

        private decimal ToDecimal(DataRow row, string col, decimal def)
        {
            if (!row.Table.Columns.Contains(col)) return def;
            var v = row[col];
            if (v == null || v == DBNull.Value) return def;
            try { return Convert.ToDecimal(v); }
            catch { return def; }
        }

        private void TxtDescripcion_TextChanged(object sender, EventArgs e)
        {
            // Evita volcar mientras se está inicializando el formulario
            if (_cargandoDatos) return;
            if (_bsLineas.Current is not DataRowView drv) return;

            if (drv.Row.Table.Columns.Contains("descripcion"))
                drv["descripcion"] = txtDescripcion.Text ?? "";
        }

        private void cbProducto_SelectedValueChanged(object sender, EventArgs e) => AplicarProductoSeleccionado();
        private void BtnProducto_Click(object sender, EventArgs e) => AplicarProductoSeleccionado();

        private void AplicarProductoSeleccionado()
        {
            // Evita reentradas y estados incoherentes
            if (_cargandoDatos || _cargandoProductos || _aplicandoProducto || cbProducto.SelectedValue == null || cbProducto.SelectedValue == DBNull.Value)
                return;

            int idProd = Convert.ToInt32(cbProducto.SelectedValue);

            DataRow prodRow = _tablaProductos.LaTabla.Rows.Find(idProd);
            if (prodRow == null) return;

            _aplicandoProducto = true;

            try
            {
                var rowL = ((DataRowView)_bsLineas.Current).Row;

                if (rowL.Table.Columns.Contains("idproducto"))
                    rowL["idproducto"] = idProd;

                // Rellena descripción desde catálogo
                string desc = Convert.ToString(prodRow["descripcion"]) ?? "";
                txtDescripcion.Text = desc;

                if (rowL.Table.Columns.Contains("descripcion"))
                    rowL["descripcion"] = desc;

                // Precio unidad
                decimal precio = prodRow["preciounidad"] == DBNull.Value ? 0m : Convert.ToDecimal(prodRow["preciounidad"]);
                numPrecio.Value = ValidarLimite(numPrecio, precio);

                // % IVA asociado al producto
                decimal iva = prodRow["iva_porcentaje"] == DBNull.Value ? 0m : Convert.ToDecimal(prodRow["iva_porcentaje"]);
                numTipoIva.Value = ValidarLimite(numTipoIva, iva);

                RecalcularYVolcar();

                // UX: deja el cursor al final para editar rápido
                txtDescripcion.Focus();
                txtDescripcion.SelectionStart = txtDescripcion.Text.Length;
            }
            finally
            {
                _aplicandoProducto = false;
            }
        }

        private void CamposCalculo_ValueChanged(object sender, EventArgs e)
        {
            if (_cargandoDatos) return;
            RecalcularYVolcar();
        }

        private void RecalcularYVolcar()
        {
            // Calcula importes de línea y los vuelca a la fila (base y cuota). El total es solo visual.
            if (_bsLineas.Current is not DataRowView drv) return;

            var row = drv.Row;

            decimal cantidad = numCantidad.Value;
            decimal precio = numPrecio.Value;
            decimal tipoIva = numTipoIva.Value;

            decimal baseLinea = decimal.Round(cantidad * precio, 2, MidpointRounding.AwayFromZero);
            decimal cuotaLinea = decimal.Round(baseLinea * tipoIva / 100m, 2, MidpointRounding.AwayFromZero);
            decimal totalLinea = baseLinea + cuotaLinea;

            if (row.Table.Columns.Contains("cantidad")) row["cantidad"] = cantidad;
            if (row.Table.Columns.Contains("precio")) row["precio"] = precio;
            if (row.Table.Columns.Contains("tipoiva")) row["tipoiva"] = tipoIva;
            if (row.Table.Columns.Contains("base")) row["base"] = baseLinea;
            if (row.Table.Columns.Contains("cuota")) row["cuota"] = cuotaLinea;

            lbBase.Text = baseLinea.ToString("N2");
            lbCuota.Text = cuotaLinea.ToString("N2");
            lbTotal.Text = totalLinea.ToString("N2");
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!ValidarLinea()) return;

            // Normaliza campos antes de guardar
            if (_bsLineas.Current is DataRowView row)
            {
                if (row.Row.Table.Columns.Contains("idfacemi"))
                    row["idfacemi"] = _idFacemi;

                row["descripcion"] = (txtDescripcion.Text ?? "").Trim();
            }

            RecalcularYVolcar();
            _bsLineas.EndEdit();

            try
            {
                _tablaLineas.GuardarCambios();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la línea: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool ValidarLinea()
        {
            if (numCantidad.Value <= 0m)
            {
                MessageBox.Show("La cantidad debe ser mayor que 0.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (numPrecio.Value < 0m)
            {
                MessageBox.Show("El precio no puede ser negativo.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (numTipoIva.Value > 100.00m)
            {
                MessageBox.Show("El porcentaje de IVA no puede ser superior al 100.00%.", "Dato Incorrecto",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("La descripción no puede estar vacía.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Asegura asociación con factura para nuevas filas
            if (_bsLineas.Current is DataRowView drv && drv.Row.Table.Columns.Contains("idfacemi"))
            {
                if (drv["idfacemi"] == DBNull.Value || Convert.ToInt32(drv["idfacemi"]) == 0)
                    drv["idfacemi"] = _idFacemi;
            }

            return true;
        }
    }
}
