/*
 * FrmFacrec
 * Crear/editar facturas recibidas (facrec):
 * - Cabecera (proveedor, fecha, número, concepto, pagada, retención, totales).
 * - Líneas (facreclin) con grid en modo solo lectura y editor aparte.
 * - Recalcula base/cuota/retención/total desde líneas en memoria.
 * - Valida duplicidad de número por proveedor y año (antes de guardar).
 */

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
        #region Campos (DAL + Binding)

        // Binding de cabecera y líneas (se reutiliza el BindingSource del formulario browse).
        private BindingSource _bsFactura;
        private BindingSource _bsLineasFactura;

        // Tablas DAL: cabecera (inyectada), líneas y conceptos (cargadas aquí).
        private Tabla _tablaFactura;
        private Tabla _tablaLineasFactura;
        private Tabla _tablaConceptos;

        // Contexto: empresa/proveedor/año con el que se abre el formulario.
        private int _idEmpresa = -1;
        private int _idProveedor = -1;
        private int _anhoFactura = -1;

        // Estado del formulario: idFactura y modo.
        public int idFactura = -1;
        public bool modoEdicion = false;

        #endregion

        public FrmFacrec()
        {
            InitializeComponent();
        }

        // Constructor principal: recibe el contexto + el BindingSource/Tabla de cabecera que ya existen en el browse.
        public FrmFacrec(BindingSource aBs, Tabla aTabla, int idEmpresa, int idProveedor, int anho, int idFac)
        {
            InitializeComponent();

            _idEmpresa = idEmpresa;
            _idProveedor = idProveedor;
            _anhoFactura = anho;
            idFactura = idFac;
            modoEdicion = (idFactura > 0);

            _tablaFactura = aTabla;
            _bsFactura = aBs;

            _tablaLineasFactura = new Tabla(Program.appDAM.LaConexion);
            _bsLineasFactura = new BindingSource();
            _tablaConceptos = new Tabla(Program.appDAM.LaConexion);

            // Carga inicial según modo.
            if (modoEdicion)
                CargarFacturaExistente();
            else
                CrearNuevaFactura();

            // Datos auxiliares de UI.
            CargarConceptos();
            CargarInfoProveedor();

            // Bindings y eventos.
            WireUI();

            // Grid de líneas.
            dgLineasFactura.DataSource = _bsLineasFactura;
            ConfigurarGridLineas();

            // Si no hay ID real aún, se bloquea el manejo de líneas.
            ConfigurarEstadoEdicion();

            // Estado visual inicial.
            ActualizarEstado();
        }

        // Actualiza el StatusStrip con el recuento de líneas (feedback inmediato).
        private void ActualizarEstado()
        {
            if (tsLbNumReg != null)
            {
                int count = _bsLineasFactura != null ? _bsLineasFactura.Count : 0;
                tsLbNumReg.Text = $"Nº de líneas: {count}";
            }
        }

        // Habilita/deshabilita el grid y botones de líneas en función de si existe cabecera guardada (idFactura > 0).
        private void ConfigurarEstadoEdicion()
        {
            bool permitirLineas = (idFactura > 0);

            dgLineasFactura.Enabled = permitirLineas;

            try { if (tsBtnNew != null) tsBtnNew.Enabled = permitirLineas; } catch { }
            try { if (tsBtnEdit != null) tsBtnEdit.Enabled = permitirLineas; } catch { }
            try { if (tsBtnDelete != null) tsBtnDelete.Enabled = permitirLineas; } catch { }
        }

        // Modo edición: carga líneas desde BD y recalcula totales de cabecera desde esas líneas.
        private void CargarFacturaExistente()
        {
            string sqlLineas = "SELECT * FROM facreclin WHERE idfacrec = @id";
            var param = new Dictionary<string, object> { { "@id", idFactura } };
            _tablaLineasFactura.InicializarDatos(sqlLineas, param);
            _bsLineasFactura.DataSource = _tablaLineasFactura.LaTabla;

            RecalcularTotales();
        }

        // Modo nuevo: crea una fila de cabecera en el BindingSource y prepara tabla de líneas vacía (placeholder).
        private void CrearNuevaFactura()
        {
            DataRowView nuevaFila = (DataRowView)_bsFactura.AddNew();

            nuevaFila["idempresa"] = _idEmpresa;
            nuevaFila["idproveedor"] = _idProveedor;
            nuevaFila["fecha"] = DateTime.Now;

            nuevaFila["numero"] = string.Empty;
            nuevaFila["descripcion"] = string.Empty;
            nuevaFila["notas"] = string.Empty;

            // Valores por defecto coherentes para cálculos posteriores.
            nuevaFila["base"] = 0.00m;
            nuevaFila["cuota"] = 0.00m;
            nuevaFila["total"] = 0.00m;
            nuevaFila["retencion"] = 0.00m;
            nuevaFila["tiporet"] = 0.00m;
            nuevaFila["aplicaret"] = 0;
            nuevaFila["pagada"] = 0;
            nuevaFila["idconceptofac"] = 1;

            _bsFactura.MoveLast();

            // Tabla “vacía” para líneas: se usa id = -1 para no traer nada.
            string sqlLineas = "SELECT * FROM facreclin WHERE idfacrec = -1";
            _tablaLineasFactura.InicializarDatos(sqlLineas);
            _bsLineasFactura.DataSource = _tablaLineasFactura.LaTabla;
        }

        // Carga datos básicos del proveedor para cabecera visual (nombre + NIF/CIF).
        private void CargarInfoProveedor()
        {
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
            catch { }
        }

        // Carga conceptos y los asigna al ComboBox de concepto si existe en el formulario.
        private void CargarConceptos()
        {
            _tablaConceptos.InicializarDatos("SELECT id, descripcion FROM conceptosfac ORDER BY descripcion");

            if (Controls.Find("cbConcepto", true).Length > 0)
            {
                ComboBox cb = (ComboBox)Controls.Find("cbConcepto", true)[0];
                cb.DataSource = _tablaConceptos.LaTabla;
                cb.DisplayMember = "descripcion";
                cb.ValueMember = "id";
            }
        }

        // Wire de bindings de cabecera + eventos mínimos para refrescos.
        private void WireUI()
        {
            txtNumero.DataBindings.Add("Text", _bsFactura, "numero", true, DataSourceUpdateMode.OnPropertyChanged);
            fechaFactura.DataBindings.Add("Value", _bsFactura, "fecha", true, DataSourceUpdateMode.OnPropertyChanged);
            txtDescripcion.DataBindings.Add("Text", _bsFactura, "descripcion", true, DataSourceUpdateMode.OnPropertyChanged);
            txtNotas.DataBindings.Add("Text", _bsFactura, "notas", true, DataSourceUpdateMode.OnPropertyChanged);

            chkPagada.DataBindings.Add("Checked", _bsFactura, "pagada", true, DataSourceUpdateMode.OnPropertyChanged);
            chkRetencion.DataBindings.Add("Checked", _bsFactura, "aplicaret", true, DataSourceUpdateMode.OnPropertyChanged);
            numTipoRet.DataBindings.Add("Value", _bsFactura, "tiporet", true, DataSourceUpdateMode.OnPropertyChanged);

            if (Controls.Find("cbConcepto", true).Length > 0)
            {
                ((ComboBox)Controls.Find("cbConcepto", true)[0]).DataBindings.Add("SelectedValue", _bsFactura, "idconceptofac", true, DataSourceUpdateMode.OnPropertyChanged);
            }

            // Cuando cambian líneas, refrescamos el indicador de recuento.
            _bsLineasFactura.ListChanged += (s, e) => ActualizarEstado();
        }

        // Define columnas manuales: control de orden, formato y visibilidad.
        private void ConfigurarGridLineas()
        {
            dgLineasFactura.AutoGenerateColumns = false;
            dgLineasFactura.Columns.Clear();

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

        // Recalcula base/cuota desde las líneas en memoria y aplica retención según check + porcentaje.
        private void RecalcularTotales()
        {
            try
            {
                if (_bsFactura == null || _bsFactura.Count == 0 || _bsFactura.Position < 0) return;

                var current = _bsFactura.Current;
                if (current == null) return;

                if (_tablaLineasFactura?.LaTabla == null) return;

                decimal baseSum = 0m;
                decimal cuotaSum = 0m;

                foreach (DataRow fila in _tablaLineasFactura.LaTabla.Rows)
                {
                    if (fila.RowState == DataRowState.Deleted || fila.RowState == DataRowState.Detached) continue;

                    if (fila.Table.Columns.Contains("base") && fila["base"] != DBNull.Value)
                        baseSum += Convert.ToDecimal(fila["base"]);

                    if (fila.Table.Columns.Contains("cuota") && fila["cuota"] != DBNull.Value)
                        cuotaSum += Convert.ToDecimal(fila["cuota"]);
                }

                if (current is DataRowView row)
                {
                    if (row.Row.RowState == DataRowState.Detached && !row.IsNew) return;

                    row["base"] = baseSum;
                    row["cuota"] = cuotaSum;

                    decimal tipoRet = (chkRetencion.Checked && numTipoRet.Value > 0) ? numTipoRet.Value : 0m;
                    decimal importeRetencion = 0m;

                    if (tipoRet > 0)
                        importeRetencion = Math.Round(baseSum * (tipoRet / 100m), 2, MidpointRounding.AwayFromZero);

                    row["retencion"] = importeRetencion;
                    row["total"] = (baseSum + cuotaSum) - importeRetencion;

                    ActualizarLabelsTotales(baseSum, cuotaSum, importeRetencion, (decimal)row["total"]);
                }
            }
            catch (Exception) { }
        }

        // Refresco de labels de totales (UI).
        private void ActualizarLabelsTotales(decimal b, decimal c, decimal r, decimal t)
        {
            lbBase.Text = b.ToString("N2");
            lbCuota.Text = c.ToString("N2");
            lbRetencion.Text = r.ToString("N2");
            lbTotal.Text = t.ToString("N2");
        }

        // Retención: habilita/deshabilita porcentaje y fuerza recálculo.
        private void chkRetencion_CheckedChanged(object sender, EventArgs e)
        {
            numTipoRet.Enabled = chkRetencion.Checked;
            RecalcularTotales();
        }

        private void numTipoRet_ValueChanged(object sender, EventArgs e) => RecalcularTotales();

        // Alta de línea: requiere que la cabecera esté guardada (idFactura real).
        private void tsBtnNew_Click(object sender, EventArgs e)
        {
            if (idFactura <= 0)
            {
                MessageBox.Show("Debes guardar la cabecera de la factura antes de añadir productos.", "Guardar primero", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _bsLineasFactura.AddNew();
            using var frm = new FrmLineaFacrec(_bsLineasFactura, _tablaLineasFactura, idFactura, false);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                RecalcularTotales();
                ActualizarEstado();
            }
            else
            {
                _bsLineasFactura.CancelEdit();
            }
        }

        // Edición de línea: abre editor sobre la línea actual.
        private void tsBtnEdit_Click(object sender, EventArgs e)
        {
            if (idFactura <= 0) return;
            if (_bsLineasFactura.Current == null) return;

            using var frm = new FrmLineaFacrec(_bsLineasFactura, _tablaLineasFactura, idFactura, true);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                RecalcularTotales();
            }
        }

        // Borrado de línea: elimina del BindingSource y recalcula.
        private void tsBtnDelete_Click(object sender, EventArgs e)
        {
            if (idFactura <= 0) return;
            if (_bsLineasFactura.Current == null) return;

            if (MessageBox.Show("¿Borrar línea seleccionada?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _bsLineasFactura.RemoveCurrent();
                RecalcularTotales();
                ActualizarEstado();
            }
        }

        private void dgLineasFactura_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) tsBtnEdit_Click(sender, null);
        }

        // Aceptar: valida, guarda, cierra con OK.
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidarFactura())
            {
                GuardarFactura();
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        // Validaciones:
        // - Número obligatorio.
        // - Duplicidad: mismo proveedor + mismo número + mismo año (excluye el id actual si editas).
        private bool ValidarFactura()
        {
            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                MessageBox.Show("Debe indicar el número de la factura del proveedor.", "Campo Obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumero.Focus();
                return false;
            }

            if (_bsFactura.Current is DataRowView row)
            {
                string numero = txtNumero.Text.Trim();
                DateTime fecha = fechaFactura.Value;
                int anho = fecha.Year;

                string sqlCheck = @"
                    SELECT COUNT(*) 
                    FROM facrec 
                    WHERE idproveedor = @idProveedor 
                      AND numero = @numero 
                      AND YEAR(fecha) = @anho
                      AND id <> @idActual";

                var parametros = new Dictionary<string, object>
                {
                    { "@idProveedor", _idProveedor },
                    { "@numero", numero },
                    { "@anho", anho },
                    { "@idActual", modoEdicion ? idFactura : -1 }
                };

                object resultado = _tablaFactura.EjecutarEscalar(sqlCheck, parametros);
                int duplicados = Convert.ToInt32(resultado ?? 0);

                if (duplicados > 0)
                {
                    MessageBox.Show(
                        $"Ya existe una factura con el número '{numero}' para este proveedor en el año {anho}.",
                        "Número de factura duplicado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtNumero.Focus();
                    return false;
                }
            }

            return true;
        }

        // Guarda cabecera y después líneas (asignando idfacrec a cada línea viva).
        private void GuardarFactura()
        {
            try
            {
                _bsFactura.EndEdit();
                DataRow row = ((DataRowView)_bsFactura.Current).Row;

                if (row["descripcion"] == DBNull.Value) row["descripcion"] = "";
                if (row["notas"] == DBNull.Value) row["notas"] = "";

                Utilidades.ForzarValoresNoNulos(row, new[] { "base", "cuota", "total", "retencion", "tiporet" });

                _tablaFactura.GuardarCambios();

                if (!modoEdicion)
                {
                    if (row.Table.Columns.Contains("id") && row["id"] != DBNull.Value)
                        idFactura = Convert.ToInt32(row["id"]);
                }

                if (idFactura > 0)
                {
                    foreach (DataRow linea in _tablaLineasFactura.LaTabla.Rows)
                    {
                        if (linea.RowState != DataRowState.Deleted && linea.RowState != DataRowState.Detached)
                            linea["idfacrec"] = idFactura;
                    }
                    _tablaLineasFactura.GuardarCambios();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
                DialogResult = DialogResult.None;
            }
        }

        // Cancelar: revierte edición en memoria y cierra.
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            _bsFactura.CancelEdit();
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
