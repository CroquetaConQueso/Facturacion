using FacturacionDAM.Modelos;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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

        private bool _cargandoProductos = false;
        private bool _aplicandoProducto = false;

        public FrmLineaFacemi(BindingSource bsLineas, Tabla tablaLineas, int idFacemi)
        {
            InitializeComponent();

            _bsLineas = bsLineas ?? throw new ArgumentNullException(nameof(bsLineas));
            _tablaLineas = tablaLineas ?? throw new ArgumentNullException(nameof(tablaLineas));
            _idFacemi = idFacemi;

            _tablaProductos = new Tabla(Program.appDAM.LaConexion);
            _bsProductos = new BindingSource();

            Load += FrmLineaFacemi_Load;

            btnAceptar.Click += btnAceptar_Click;
            btnCancelar.Click += btnCancelar_Click;

            cbProducto.SelectedValueChanged += cbProducto_SelectedValueChanged;

            BtnProducto.Click += BtnProducto_Click;

            numCantidad.ValueChanged += CamposCalculo_ValueChanged;
            numPrecio.ValueChanged += CamposCalculo_ValueChanged;
            numTipoIva.ValueChanged += CamposCalculo_ValueChanged;

            txtDescripcion.TextChanged += TxtDescripcion_TextChanged;

            // Evita que el usuario escriba texto libre y se quede un valor raro
            cbProducto.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void FrmLineaFacemi_Load(object sender, EventArgs e)
        {
            if (_bsLineas.Current is not DataRowView drv)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            PrepararFilaLinea(drv);

            if (!CargarProductos())
            {
                MessageBox.Show("No se pudieron cargar los productos.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            CargarControlesDesdeFila(drv);

            // IMPORTANTE RECALCULAR AL INICIO
            RecalcularYVolcar();
        }

        private void PrepararFilaLinea(DataRowView drv)
        {
            var row = drv.Row;

            // Asegurar FK a la factura (idfacemi)
            if (row.Table.Columns.Contains("idfacemi"))
            {
                if (row["idfacemi"] == DBNull.Value || Convert.ToInt32(row["idfacemi"]) == 0)
                    row["idfacemi"] = _idFacemi;
            }

            // Defaults para evitar DBNulls
            SetDefault(row, "cantidad", 1m);
            SetDefault(row, "precio", 0m);
            SetDefault(row, "base", 0m);
            SetDefault(row, "tipoiva", 0m);
            SetDefault(row, "cuota", 0m);
            SetDefault(row, "descripcion", "");

            // idproducto puede ser NULL
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

            // Marcar PK para busquedas rapidas (Find)
            if (_tablaProductos.LaTabla.Columns.Contains("id"))
                _tablaProductos.LaTabla.PrimaryKey = new[] { _tablaProductos.LaTabla.Columns["id"] };

            _cargandoProductos = true;

            try
            {
                _bsProductos.DataSource = _tablaProductos.LaTabla;

                cbProducto.DataSource = _bsProductos;
                cbProducto.DisplayMember = "descripcion";
                cbProducto.ValueMember = "id";

                // Arregla el problema de SelectedValue al cargar
                if (_bsLineas.Current is DataRowView drvLinea)
                {
                    if (drvLinea.Row.Table.Columns.Contains("idproducto") && drvLinea["idproducto"] != DBNull.Value)
                    {
                        int idProd = Convert.ToInt32(drvLinea["idproducto"]);
                        cbProducto.SelectedValue = idProd;
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

            numCantidad.Value = ToDecimal(row, "cantidad", 1m);
            numPrecio.Value = ToDecimal(row, "precio", 0m);
            numTipoIva.Value = ToDecimal(row, "tipoiva", 0m);

            if (row.Table.Columns.Contains("idproducto") && row["idproducto"] != DBNull.Value)
            {
                int idProd = Convert.ToInt32(row["idproducto"]);
                try { cbProducto.SelectedValue = idProd; }
                catch { cbProducto.SelectedIndex = -1; }
            }
            else
            {
                cbProducto.SelectedIndex = -1;
            }
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
            if (_bsLineas.Current is not DataRowView drv) return;
            if (!drv.Row.Table.Columns.Contains("descripcion")) return;

            drv["descripcion"] = txtDescripcion.Text ?? "";
        }

        private void cbProducto_SelectedValueChanged(object sender, EventArgs e)
        {
            AplicarProductoSeleccionado();
        }

        private void BtnProducto_Click(object sender, EventArgs e)
        {
            AplicarProductoSeleccionado();
        }

        private void AplicarProductoSeleccionado()
        {
            if (_cargandoProductos) return;
            if (_aplicandoProducto) return;

            if (_bsLineas.Current is not DataRowView drvLinea) return;

            // Si no hay seleccion real
            if (cbProducto.SelectedValue == null || cbProducto.SelectedValue == DBNull.Value) return;

            int idProd;
            try { idProd = Convert.ToInt32(cbProducto.SelectedValue); }
            catch { return; }

            var dt = _tablaProductos.LaTabla;
            DataRow prodRow = dt.Rows.Find(idProd);
            if (prodRow == null) return;

            _aplicandoProducto = true;

            try
            {
                var rowL = drvLinea.Row;

                // idproducto
                if (rowL.Table.Columns.Contains("idproducto"))
                    rowL["idproducto"] = idProd;

                // descripcion
                string desc = Convert.ToString(prodRow["descripcion"]) ?? "";
                txtDescripcion.Text = desc;
                if (rowL.Table.Columns.Contains("descripcion"))
                    rowL["descripcion"] = desc;

                // precio
                decimal precio = prodRow["preciounidad"] == DBNull.Value ? 0m : Convert.ToDecimal(prodRow["preciounidad"]);
                numPrecio.Value = NormalizarNumeric(numPrecio, precio);

                // tipo IVA
                decimal iva = prodRow["iva_porcentaje"] == DBNull.Value ? 0m : Convert.ToDecimal(prodRow["iva_porcentaje"]);
                numTipoIva.Value = NormalizarNumeric(numTipoIva, iva);

                RecalcularYVolcar();
            }
            finally
            {
                _aplicandoProducto = false;
            }
        }

        private decimal NormalizarNumeric(NumericUpDown num, decimal v)
        {
            if (v < num.Minimum) return num.Minimum;
            if (v > num.Maximum) return num.Maximum;
            return decimal.Round(v, num.DecimalPlaces, MidpointRounding.AwayFromZero);
        }

        private void CamposCalculo_ValueChanged(object sender, EventArgs e)
        {
            RecalcularYVolcar();
        }

        private void RecalcularYVolcar()
        {
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
            if (!ValidarLinea())
                return;

            RecalcularYVolcar();
            _bsLineas.EndEdit();

            DialogResult = DialogResult.OK;
            Close();
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

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("La descripción no puede estar vacía.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // asegurar idfacemi
            if (_bsLineas.Current is DataRowView drv && drv.Row.Table.Columns.Contains("idfacemi"))
            {
                if (drv["idfacemi"] == DBNull.Value || Convert.ToInt32(drv["idfacemi"]) == 0)
                    drv["idfacemi"] = _idFacemi;
            }

            return true;
        }
    }
}
