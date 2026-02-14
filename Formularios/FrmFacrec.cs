// Ruta: FacturacionDAM/Formularios/FrmFacrec.cs
using FacturacionDAM.Modelos;
using FacturacionDAM.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace FacturacionDAM.Formularios
{
    public partial class FrmFacrec : Form
    {
        private BindingSource _bsFactura;
        private BindingSource _bsLineasFactura;
        private Tabla _tablaFactura;
        private Tabla _tablaLineasFactura;
        private Tabla _tablaConceptos;

        // Contexto: La empresa (emisor activo) es quien recibe la factura (COMPRADOR)
        private int _idEmpresa = -1;
        // El proveedor es quien emite la factura (VENDEDOR)
        private int _idProveedor = -1;
        private int _anhoFactura = -1;

        public int idFactura = -1;
        public bool modoEdicion = false;

        // Constructor vacío requerido por el diseñador
        public FrmFacrec()
        {
            InitializeComponent();
        }

        // Constructor principal
        public FrmFacrec(BindingSource aBs, Tabla aTabla, int idEmpresa, int idProveedor, int anho, int idFac)
        {
            InitializeComponent();

            _idEmpresa = idEmpresa;
            _idProveedor = idProveedor;
            _anhoFactura = anho;
            idFactura = idFac;

            // Si idFac > 0 es edición, sino es nueva
            modoEdicion = (idFactura > 0);

            // 1. Configurar Tablas y BindingSources
            _tablaFactura = aTabla;
            _bsFactura = aBs; // Usamos el BindingSource del navegador para mantener sincronía

            _tablaLineasFactura = new Tabla(Program.appDAM.LaConexion);
            _bsLineasFactura = new BindingSource();
            _tablaConceptos = new Tabla(Program.appDAM.LaConexion);

            // 2. Inicializar Datos
            if (modoEdicion)
            {
                CargarFacturaExistente();
            }
            else
            {
                CrearNuevaFactura();
            }

            // 3. Cargar datos auxiliares (Conceptos, Info Proveedor)
            CargarConceptos();
            CargarInfoProveedor();

            // 4. Enlazar UI
            WireUI();

            // 5. Configurar Grid de Líneas
            dgLineasFactura.DataSource = _bsLineasFactura;
            ConfigurarGridLineas();
        }

        private void CargarFacturaExistente()
        {
            // La factura ya está cargada en el BindingSource del navegador, nos posicionamos
            // (Aunque idealmente deberíamos recargarla para asegurar datos frescos si usamos una tabla distinta)
            // Para simplificar y seguir la lógica de Facemi, asumimos que _bsFactura ya tiene los datos o filtramos.

            // Cargar Líneas de esta factura
            string sqlLineas = "SELECT * FROM facreclin WHERE idfacrec = @id";
            var param = new Dictionary<string, object> { { "@id", idFactura } };
            _tablaLineasFactura.InicializarDatos(sqlLineas, param);
            _bsLineasFactura.DataSource = _tablaLineasFactura.LaTabla;

            RecalcularTotales();
        }

        private void CrearNuevaFactura()
        {
            // Añadir nueva fila al BindingSource
            DataRowView nuevaFila = (DataRowView)_bsFactura.AddNew();

            // Valores por defecto
            nuevaFila["idempresa"] = _idEmpresa;
            nuevaFila["idproveedor"] = _idProveedor;
            nuevaFila["fecha"] = DateTime.Now;
            // En Facturas Recibidas, el número lo suele proveer el papel físico del proveedor.
            // Lo dejamos vacío para que el usuario lo rellene.
            nuevaFila["numero"] = string.Empty;

            // Valores numéricos a 0
            nuevaFila["base"] = 0.00m;
            nuevaFila["cuota"] = 0.00m;
            nuevaFila["total"] = 0.00m;
            nuevaFila["retencion"] = 0.00m;
            nuevaFila["tiporet"] = 0.00m;
            nuevaFila["aplicaret"] = 0;
            nuevaFila["pagada"] = 0;

            nuevaFila["idconceptofac"] = 1; // Valor por defecto o DBNull

            _bsFactura.MoveLast();

            // Inicializar tabla de líneas vacía
            string sqlLineas = "SELECT * FROM facreclin WHERE idfacrec = -1";
            _tablaLineasFactura.InicializarDatos(sqlLineas);
            _bsLineasFactura.DataSource = _tablaLineasFactura.LaTabla;
        }

        private void CargarInfoProveedor()
        {
            // Obtener datos del proveedor para mostrar en cabecera
            try
            {
                string sql = "SELECT nombrecomercial, nifcif FROM proveedores WHERE id = @id";
                using var cmd = new MySqlCommand(sql, Program.appDAM.LaConexion);
                cmd.Parameters.AddWithValue("@id", _idProveedor);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lbNombreCliente.Text = reader["nombrecomercial"].ToString();
                    lbNifcifCliente.Text = reader["nifcif"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedor: " + ex.Message);
            }
        }

        private void CargarConceptos()
        {
            _tablaConceptos.InicializarDatos("SELECT id, nombre FROM conceptosfac ORDER BY nombre");

            // Configurar el ComboBox de Conceptos
            // Asumiendo que existe un cbConcepto en el Designer
            if (Controls.Find("cbConcepto", true).Length > 0)
            {
                ComboBox cb = (ComboBox)Controls.Find("cbConcepto", true)[0];
                cb.DataSource = _tablaConceptos.LaTabla;
                cb.DisplayMember = "nombre";
                cb.ValueMember = "id";
                // El DataBinding se hace en WireUI
            }
        }

        private void WireUI()
        {
            // Enlazar controles con _bsFactura
            txtNumero.DataBindings.Add("Text", _bsFactura, "numero", true, DataSourceUpdateMode.OnPropertyChanged);
            fechaFactura.DataBindings.Add("Value", _bsFactura, "fecha", true, DataSourceUpdateMode.OnPropertyChanged);
            txtDescripcion.DataBindings.Add("Text", _bsFactura, "descripcion", true);
            txtNotas.DataBindings.Add("Text", _bsFactura, "notas", true);

            chkPagada.DataBindings.Add("Checked", _bsFactura, "pagada", true);
            chkRetencion.DataBindings.Add("Checked", _bsFactura, "aplicaret", true);
            numTipoRet.DataBindings.Add("Value", _bsFactura, "tiporet", true);

            // Enlazar Concepto
            if (Controls.Find("cbConcepto", true).Length > 0)
            {
                ((ComboBox)Controls.Find("cbConcepto", true)[0]).DataBindings.Add("SelectedValue", _bsFactura, "idconceptofac", true);
            }

            // Etiquetas de Totales (Solo lectura, se actualizan vía código en RecalcularTotales)
            // No hacemos DataBinding directo para tener control sobre el formato y el momento de cálculo
        }

        private void ConfigurarGridLineas()
        {
            dgLineasFactura.AutoGenerateColumns = false;
            dgLineasFactura.Columns.Clear();

            // Definición de columnas
            dgLineasFactura.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "cantidad", HeaderText = "Cant", Width = 50, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgLineasFactura.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "descripcion", HeaderText = "Descripción", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgLineasFactura.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "precio", HeaderText = "Precio", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgLineasFactura.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "base", HeaderText = "Base", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgLineasFactura.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "tipoiva", HeaderText = "% IVA", Width = 50, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgLineasFactura.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "cuota", HeaderText = "Cuota", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } });

            dgLineasFactura.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            dgLineasFactura.ReadOnly = true;
            dgLineasFactura.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgLineasFactura.MultiSelect = false;
        }

        // LÓGICA DE CÁLCULO
        private void RecalcularTotales()
        {
            if (_bsFactura?.Current == null || _tablaLineasFactura?.LaTabla == null) return;

            decimal baseSum = 0m;
            decimal cuotaSum = 0m;

            // 1. Sumar líneas (Iteración segura)
            foreach (DataRow fila in _tablaLineasFactura.LaTabla.Rows)
            {
                if (fila.RowState == DataRowState.Deleted || fila.RowState == DataRowState.Detached) continue;

                if (fila.Table.Columns.Contains("base") && fila["base"] != DBNull.Value)
                    baseSum += Convert.ToDecimal(fila["base"]);

                if (fila.Table.Columns.Contains("cuota") && fila["cuota"] != DBNull.Value)
                    cuotaSum += Convert.ToDecimal(fila["cuota"]);
            }

            if (_bsFactura.Current is DataRowView row)
            {
                row.BeginEdit();

                row["base"] = baseSum;
                row["cuota"] = cuotaSum;

                // 2. Calcular Retención
                decimal tipoRet = (chkRetencion.Checked && numTipoRet.Value > 0) ? numTipoRet.Value : 0m;
                decimal importeRetencion = 0m;

                if (tipoRet > 0)
                {
                    importeRetencion = Math.Round(baseSum * (tipoRet / 100m), 2, MidpointRounding.AwayFromZero);
                }
                row["retencion"] = importeRetencion;

                // 3. Total Final (Base + IVA - Retención)
                row["total"] = (baseSum + cuotaSum) - importeRetencion;

                row.EndEdit();

                // 4. Actualizar Interfaz
                ActualizarLabelsTotales(baseSum, cuotaSum, importeRetencion, (decimal)row["total"]);
            }
        }

        private void ActualizarLabelsTotales(decimal b, decimal c, decimal r, decimal t)
        {
            lbBase.Text = b.ToString("N2");
            lbCuota.Text = c.ToString("N2");
            lbRetencion.Text = r.ToString("N2");
            lbTotal.Text = t.ToString("N2");
        }

        private void chkRetencion_CheckedChanged(object sender, EventArgs e)
        {
            numTipoRet.Enabled = chkRetencion.Checked;
            RecalcularTotales();
        }

        private void numTipoRet_ValueChanged(object sender, EventArgs e)
        {
            RecalcularTotales();
        }

        // CRUD LINEAS
        private void tsBtnNew_Click(object sender, EventArgs e)
        {
            _bsLineasFactura.AddNew();
            using var frm = new FrmLineaFacrec(_bsLineasFactura, _tablaLineasFactura, idFactura, false);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                RecalcularTotales();
            }
            else
            {
                _bsLineasFactura.CancelEdit();
            }
        }

        private void tsBtnEdit_Click(object sender, EventArgs e)
        {
            if (_bsLineasFactura.Current == null) return;

            using var frm = new FrmLineaFacrec(_bsLineasFactura, _tablaLineasFactura, idFactura, true);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                RecalcularTotales();
            }
        }

        private void tsBtnDelete_Click(object sender, EventArgs e)
        {
            if (_bsLineasFactura.Current == null) return;

            if (MessageBox.Show("¿Borrar línea seleccionada?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _bsLineasFactura.RemoveCurrent();
                RecalcularTotales();
            }
        }

        private void dgLineasFactura_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) tsBtnEdit_Click(sender, null);
        }

        // GUARDADO
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidarFactura())
            {
                GuardarFactura();
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private bool ValidarFactura()
        {
            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                MessageBox.Show("Debe indicar el número de la factura del proveedor.", "Falta número", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumero.Focus();
                return false;
            }
            return true;
        }

        private void GuardarFactura()
        {
            try
            {
                _bsFactura.EndEdit();

                DataRow row = ((DataRowView)_bsFactura.Current).Row;

                // Asegurar valores no nulos
                Utilidades.ForzarValoresNoNulos(row, new[] { "base", "cuota", "total", "retencion", "tiporet" });

                // Guardar Cabecera
                _tablaFactura.GuardarCambios();

                // Si es nueva, recuperar ID generado
                if (!modoEdicion)
                {
                    long newId = _tablaFactura.UltimoIdInsertado();
                    idFactura = (int)newId;

                    // Nota: En facturas recibidas NO actualizamos nextNumFac del emisor
                    // porque el número es externo (del proveedor).
                }

                // Asignar ID a las líneas
                foreach (DataRow linea in _tablaLineasFactura.LaTabla.Rows)
                {
                    if (linea.RowState != DataRowState.Deleted)
                    {
                        linea["idfacrec"] = idFactura;
                    }
                }

                // Guardar Líneas
                _tablaLineasFactura.GuardarCambios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
                DialogResult = DialogResult.None; // Evitar cierre
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            _bsFactura.CancelEdit();
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}