// Ruta: FacturacionDAM/Formularios/FrmFacemi.cs
using FacturacionDAM.Modelos;
using FacturacionDAM.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace FacturacionDAM.Formularios
{
    public partial class FrmFacemi : Form
    {
        private BindingSource _bsFactura;
        private BindingSource _bsLineasFactura;
        private Tabla _tablaFactura;
        private Tabla _tablaLineasFactura;
        private Tabla _tablaConceptos;

        private int _idEmisor = -1;
        private int _idCliente = -1;
        private int _anhoFactura = -1;

        public int idFactura = -1;
        public bool modoEdicion = false;

        public FrmFacemi()
        {
            InitializeComponent();

            _tablaFactura = new Tabla(Program.appDAM.LaConexion);
            _tablaLineasFactura = new Tabla(Program.appDAM.LaConexion);
            _tablaConceptos = new Tabla(Program.appDAM.LaConexion);

            _bsFactura = new BindingSource();
            _bsLineasFactura = new BindingSource();

            InitFactura();
            WireUI();
        }

        public FrmFacemi(BindingSource aBs, Tabla aTabla, int aIdEmisor, int aIdCliente, int aYear, int aIdFactura = -1)
        {
            InitializeComponent();

            if (aBs == null) throw new ArgumentNullException(nameof(aBs));
            if (aTabla == null) throw new ArgumentNullException(nameof(aTabla));

            _idCliente = aIdCliente;
            _idEmisor = aIdEmisor;
            _anhoFactura = aYear;

            idFactura = aIdFactura;
            modoEdicion = (aIdFactura > 0);

            _bsFactura = new BindingSource();
            _tablaFactura = new Tabla(Program.appDAM.LaConexion);
            _tablaLineasFactura = new Tabla(Program.appDAM.LaConexion);
            _tablaConceptos = new Tabla(Program.appDAM.LaConexion);
            _bsLineasFactura = new BindingSource();

            InitFactura();
            WireUI();
        }


        private void WireUI()
        {
            tsBtnFirst.Click += (s, e) => { _bsLineasFactura?.MoveFirst(); ActualizarEstado(); };
            tsBtnPrev.Click += (s, e) => { _bsLineasFactura?.MovePrevious(); ActualizarEstado(); };
            tsBtnNext.Click += (s, e) => { _bsLineasFactura?.MoveNext(); ActualizarEstado(); };
            tsBtnLast.Click += (s, e) => { _bsLineasFactura?.MoveLast(); ActualizarEstado(); };

            tsBtnExportCSV.Click += tsBtnExportCSV_Click;
            tsBtnExportXML.Click += tsBtnExportXML_Click;

            chkRetencion.CheckedChanged += (s, e) => RecalcularTotales();
            numTipoRet.ValueChanged += (s, e) => RecalcularTotales();
        }

        private void FrmFacemi_Load(object sender, EventArgs e)
        {
            try
            {
                if (!CargarConceptos() || !CargarDatosEmisorYCliente())
                    return;

                if (!AsegurarFacturaEnMemoria())
                {
                    MessageBox.Show("No se pudo preparar la factura en memoria.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                PrepararBindingFactura();

                if (modoEdicion)
                    CargarLineasFacturaExistente();
                else
                    CrearLineasFacturaNueva();

                PrepararBindingLineas();
                ActualizarEstado();
                RecalcularTotales();
            }
            catch (Exception ex)
            {
                Program.appDAM.RegistrarLog("Inicializar factura. Edición: " + modoEdicion, ex.Message);
                MessageBox.Show("Se ha producido un error al inicializar la factura.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmFacemi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK && _bsFactura != null)
                _bsFactura.CancelEdit();
        }

        private void tsBtnNew_Click(object sender, EventArgs e)
        {
            bool crearLinea = false;

            if (!modoEdicion)
            {
                if (MessageBox.Show(
                        "No ha guardado la nueva factura.\n¿Guardar la nueva factura antes de crear la línea?",
                        "Confirmación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    crearLinea = GuardarFactura();
                }
            }
            else
            {
                crearLinea = true;
            }

            if (!crearLinea) return;

            _bsLineasFactura.AddNew();

            FrmLineaFacemi frm = new FrmLineaFacemi(_bsLineasFactura, _tablaLineasFactura, idFactura);

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                _tablaLineasFactura.Refrescar();
                ActualizarEstado();
                RecalcularTotales();
            }
            else
            {
                _bsLineasFactura.CancelEdit();
            }
        }

        private void tsBtnEdit_Click(object sender, EventArgs e)
        {
            if (!(_bsLineasFactura.Current is DataRowView)) return;

            FrmLineaFacemi frm = new FrmLineaFacemi(_bsLineasFactura, _tablaLineasFactura, idFactura);
            frm.edicion = true;

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                _tablaLineasFactura.Refrescar();
                ActualizarEstado();
                RecalcularTotales();
            }
        }

        private void tsBtnDelete_Click(object sender, EventArgs e)
        {
            if (!(_bsLineasFactura.Current is DataRowView)) return;

            if (MessageBox.Show("¿Eliminar la línea de factura seleccionada?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            _bsLineasFactura.RemoveCurrent();
            _tablaLineasFactura.GuardarCambios();

            ActualizarEstado();
            RecalcularTotales();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (GuardarFactura())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ActualizarEstado()
        {
            tsLbNumReg.Text = _bsLineasFactura == null
                ? "Nº de registros: 0"
                : $"Nº de registros: {_bsLineasFactura.Count}";
        }

        private void SetStatus(string txt)
        {
            tsLbStatus.Text = txt ?? "";
        }

        private bool GuardarFactura()
        {
            try
            {
                if (!AsegurarFacturaEnMemoria())
                {
                    MessageBox.Show("No hay factura activa en memoria (BindingSource.Current es null).", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (!ValidarDatos())
                    return false;

                ForzarValoresNoNulos();

                _bsFactura.EndEdit();
                _tablaFactura.GuardarCambios();

                if (!modoEdicion)
                {
                    using (var cmd = new MySqlCommand("SELECT LAST_INSERT_ID()", Program.appDAM.LaConexion))
                    {
                        object res = cmd.ExecuteScalar();
                        idFactura = Convert.ToInt32(res);
                    }

                    ActualizarNumeracionEmisorSiEsNuevaFactura();
                    modoEdicion = true;
                }

                SetStatus("Factura guardada.");
                return true;
            }
            catch (Exception ex)
            {
                Program.appDAM.RegistrarLog("Guardar factura", ex.Message);
                MessageBox.Show("Se ha producido un error al guardar la factura.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool ValidarDatos()
        {
            if (!(_bsFactura.Current is DataRowView row))
                return false;

            if (_anhoFactura <= 0)
            {
                MessageBox.Show("_anhoFactura no está inicializado (año fiscal inválido).", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (row["numero"] == DBNull.Value || string.IsNullOrWhiteSpace(row["numero"].ToString()))
            {
                MessageBox.Show("El campo 'Número' es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumero.Focus();
                return false;
            }

            if (row["fecha"] == DBNull.Value)
            {
                MessageBox.Show("El campo 'Fecha' es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fechaFactura.Focus();
                return false;
            }

            if (row["idconceptofac"] == DBNull.Value || Convert.ToInt32(row["idconceptofac"]) <= 0)
            {
                MessageBox.Show("Debe seleccionar un concepto de facturación.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbConceptFac.Focus();
                return false;
            }

            if (row["descripcion"] == DBNull.Value || string.IsNullOrWhiteSpace(row["descripcion"].ToString()))
            {
                MessageBox.Show("El campo 'Descripción' es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return false;
            }

            DateTime fecha = Convert.ToDateTime(row["fecha"]);
            DateTime inicio = new DateTime(_anhoFactura, 1, 1);
            DateTime fin = new DateTime(_anhoFactura, 12, 31);

            if (fecha < inicio || fecha > fin)
            {
                MessageBox.Show($"La fecha debe estar entre el {inicio:dd/MM/yyyy} y el {fin:dd/MM/yyyy}.",
                    "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fechaFactura.Focus();
                return false;
            }

            int numero = Convert.ToInt32(row["numero"]);
            int idActual = modoEdicion ? idFactura : -1;

            string sqlCheck = @"
                SELECT COUNT(*)
                FROM facemi
                WHERE idemisor = @idemisor
                  AND numero = @numero
                  AND YEAR(fecha) = @anho
                  AND id <> @idActual";

            var parametros = new Dictionary<string, object>
            {
                { "@idemisor", _idEmisor },
                { "@numero", numero },
                { "@anho", _anhoFactura },
                { "@idActual", idActual }
            };

            object? esc = _tablaFactura.EjecutarEscalar(sqlCheck, parametros);
            int duplicados = Convert.ToInt32(esc ?? 0);

            if (duplicados > 0)
            {
                MessageBox.Show(
                    $"Ya existe otra factura del emisor con el número {numero} en el año {_anhoFactura}.",
                    "Número duplicado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtNumero.Focus();
                return false;
            }

            return true;
        }

        private void ForzarValoresNoNulos()
        {
            if (!(_bsFactura.Current is DataRowView row))
                return;

            if (row["tiporet"] == DBNull.Value)
                row["tiporet"] = numTipoRet.Value;

            if (row["aplicaret"] == DBNull.Value)
                row["aplicaret"] = chkRetencion.Checked ? 1 : 0;

            if (row["pagada"] == DBNull.Value)
                row["pagada"] = chkPagada.Checked ? 1 : 0;
        }

        private void ActualizarNumeracionEmisorSiEsNuevaFactura()
        {
            string sql = "UPDATE emisores SET nextnumfac = nextnumfac + 1 WHERE id=@id";

            var p = new Dictionary<string, object>
            {
                { "@id", Program.appDAM.emisor.id }
            };

            _tablaFactura.EjecutarComando(sql, p);
            Program.appDAM.emisor.nextNumFac++;
        }

        private bool CargarDatosEmisorYCliente()
        {
            lbNifcifEmisor.Text = Program.appDAM.emisor.nifcif;
            lbNombreEmisor.Text = Program.appDAM.emisor.nombreComercial;

            Tabla tCli = new Tabla(Program.appDAM.LaConexion);

            if (tCli.InicializarDatos($"SELECT id, nifcif, nombrecomercial FROM clientes WHERE id = {_idCliente}")
                && tCli.LaTabla.Rows.Count > 0)
            {
                lbNifcifCliente.Text = tCli.LaTabla.Rows[0]["nifcif"].ToString();
                lbNombreCliente.Text = tCli.LaTabla.Rows[0]["nombrecomercial"].ToString();

                if (!modoEdicion && _bsFactura.Current is DataRowView row)
                {
                    row["idemisor"] = Program.appDAM.emisor.id;
                    row["idcliente"] = _idCliente;
                }

                return true;
            }

            MessageBox.Show("No se pudieron cargar los datos del cliente.", "Aviso",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return false;
        }

        private bool CargarConceptos()
        {
            if (_tablaConceptos.InicializarDatos("SELECT id, descripcion FROM conceptosfac ORDER BY descripcion"))
            {
                cbConceptFac.DataSource = _tablaConceptos.LaTabla;
                cbConceptFac.DisplayMember = "descripcion";
                cbConceptFac.ValueMember = "id";
                return true;
            }

            cbConceptFac.Enabled = false;
            MessageBox.Show("No se pudieron cargar los conceptos de facturación.", "Aviso",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return false;
        }

        private void InitFactura()
        {
            lbNifcifEmisor.Text = "";
            lbNombreEmisor.Text = "";
            lbNifcifCliente.Text = "";
            lbNombreCliente.Text = "";
            txtNumero.Text = "";
            fechaFactura.Value = DateTime.Now;
            txtDescripcion.Text = "";
            chkPagada.Checked = false;
            chkRetencion.Checked = false;
            numTipoRet.Value = 0;

            lbBase.Text = "";
            lbCuota.Text = "";
            lbTotal.Text = "";
            lbRetencion.Text = "";
            tsLbStatus.Text = "";
        }

        private bool AsegurarFacturaEnMemoria()
        {
            if (_bsFactura?.Current is DataRowView) return true;

            if (_bsFactura != null && _bsFactura.DataSource == null && _tablaFactura?.LaTabla != null)
                _bsFactura.DataSource = _tablaFactura.LaTabla;

            if (_tablaFactura?.LaTabla == null || _tablaFactura.LaTabla.Columns.Count == 0)
            {
                string sql = modoEdicion && idFactura > 0
                    ? $"SELECT * FROM facemi WHERE id = {idFactura}"
                    : "SELECT * FROM facemi WHERE 1=0";

                if (!_tablaFactura.InicializarDatos(sql))
                    return false;

                if (_bsFactura != null)
                    _bsFactura.DataSource = _tablaFactura.LaTabla;
            }

            if (!modoEdicion && _bsFactura != null)
                _bsFactura.AddNew();

            if (modoEdicion && _bsFactura != null && _bsFactura.Count > 0)
                _bsFactura.MoveFirst();

            return (_bsFactura?.Current is DataRowView);
        }

        private void CargarLineasFacturaExistente()
        {
            int idFacemi = idFactura;

            if (idFacemi <= 0)
            {
                if (_bsFactura?.Current is DataRowView drv &&
                    drv.Row != null &&
                    drv.Row.Table.Columns.Contains("id") &&
                    drv["id"] != DBNull.Value)
                {
                    idFacemi = Convert.ToInt32(drv["id"]);
                }
            }

            if (idFacemi <= 0)
            {
                _tablaLineasFactura.InicializarDatos("SELECT * FROM facemilin WHERE 1=0");
                _bsLineasFactura.DataSource = _tablaLineasFactura.LaTabla;
                return;
            }

            string sql = $"SELECT * FROM facemilin WHERE idfacemi = {idFacemi}";
            if (_tablaLineasFactura.InicializarDatos(sql))
                _bsLineasFactura.DataSource = _tablaLineasFactura.LaTabla;
        }

        private void CrearLineasFacturaNueva()
        {
            string sql = "SELECT * FROM facemilin WHERE 1=0";
            if (_tablaLineasFactura.InicializarDatos(sql))
                _bsLineasFactura.DataSource = _tablaLineasFactura.LaTabla;
        }

        private void PrepararBindingFactura()
        {
            if (!(_bsFactura.Current is DataRowView row))
                return;

            if (row["fecha"] == DBNull.Value)
                row["fecha"] = new DateTime(_anhoFactura, DateTime.Today.Month, DateTime.Today.Day);

            if (!modoEdicion)
                row["numero"] = Program.appDAM.emisor.nextNumFac;

            txtNumero.DataBindings.Clear();
            fechaFactura.DataBindings.Clear();
            cbConceptFac.DataBindings.Clear();
            txtDescripcion.DataBindings.Clear();
            chkPagada.DataBindings.Clear();
            chkRetencion.DataBindings.Clear();
            numTipoRet.DataBindings.Clear();
            txtNotas.DataBindings.Clear();

            lbBase.DataBindings.Clear();
            lbCuota.DataBindings.Clear();
            lbTotal.DataBindings.Clear();
            lbRetencion.DataBindings.Clear();

            txtNumero.DataBindings.Add("Text", _bsFactura, "numero");
            fechaFactura.DataBindings.Add("Value", _bsFactura, "fecha");
            cbConceptFac.DataBindings.Add("SelectedValue", _bsFactura, "idconceptofac");
            txtDescripcion.DataBindings.Add("Text", _bsFactura, "descripcion");

            chkPagada.DataBindings.Add("Checked", _bsFactura, "pagada", true, DataSourceUpdateMode.OnPropertyChanged, false);
            chkRetencion.DataBindings.Add("Checked", _bsFactura, "aplicaret", true, DataSourceUpdateMode.OnPropertyChanged, false);

            numTipoRet.DataBindings.Add("Value", _bsFactura, "tiporet", true, DataSourceUpdateMode.OnPropertyChanged, 0m);

            txtNotas.DataBindings.Add("Text", _bsFactura, "notas");

            lbBase.DataBindings.Add("Text", _bsFactura, "base", true, DataSourceUpdateMode.OnPropertyChanged, 0.0, "N2");
            lbCuota.DataBindings.Add("Text", _bsFactura, "cuota", true, DataSourceUpdateMode.OnPropertyChanged, 0.0, "N2");
            lbTotal.DataBindings.Add("Text", _bsFactura, "total", true, DataSourceUpdateMode.OnPropertyChanged, 0.0, "N2");
            lbRetencion.DataBindings.Add("Text", _bsFactura, "retencion", true, DataSourceUpdateMode.OnPropertyChanged, 0.0, "N2");
        }

        private void PrepararBindingLineas()
        {
            _bsLineasFactura.DataSource = _tablaLineasFactura.LaTabla;
            dgLineasFactura.DataSource = _bsLineasFactura;

            if (dgLineasFactura.Columns.Contains("id"))
                dgLineasFactura.Columns["id"].Visible = false;

            if (dgLineasFactura.Columns.Contains("idfacemi"))
                dgLineasFactura.Columns["idfacemi"].Visible = false;

            if (dgLineasFactura.Columns.Contains("descripcion"))
                dgLineasFactura.Columns["descripcion"].HeaderText = "Descripción";

            if (dgLineasFactura.Columns.Contains("cantidad"))
                dgLineasFactura.Columns["cantidad"].HeaderText = "Cantidad";

            if (dgLineasFactura.Columns.Contains("precio"))
                dgLineasFactura.Columns["precio"].HeaderText = "Precio";

            if (dgLineasFactura.Columns.Contains("base"))
                dgLineasFactura.Columns["base"].HeaderText = "Base";

            if (dgLineasFactura.Columns.Contains("tipoiva"))
                dgLineasFactura.Columns["tipoiva"].HeaderText = "IVA %";

            if (dgLineasFactura.Columns.Contains("cuota"))
                dgLineasFactura.Columns["cuota"].HeaderText = "Cuota IVA";
        }

        private void RecalcularTotales()
        {
            if (_tablaLineasFactura?.LaTabla == null || _tablaLineasFactura.LaTabla.Rows.Count == 0)
            {
                if (_bsFactura?.Current is DataRowView row0)
                {
                    row0["base"] = 0m;
                    row0["cuota"] = 0m;
                    row0["total"] = 0m;
                    row0["retencion"] = 0m;
                }
                return;
            }

            decimal baseSum = 0m, cuotaSum = 0m;

            foreach (DataRow fila in _tablaLineasFactura.LaTabla.Rows)
            {
                baseSum += fila.Field<decimal?>("base") ?? 0m;
                cuotaSum += fila.Field<decimal?>("cuota") ?? 0m;
            }

            decimal total = baseSum + cuotaSum;

            decimal tipoRet = chkRetencion.Checked ? numTipoRet.Value : 0m;
            decimal retencion = Math.Round(baseSum * (tipoRet / 100m), 2, MidpointRounding.AwayFromZero);

            if (_bsFactura.Current is DataRowView row)
            {
                row["base"] = baseSum;
                row["cuota"] = cuotaSum;
                row["total"] = total;
                row["retencion"] = retencion;
            }
        }

        private void tsBtnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (_tablaLineasFactura?.LaTabla == null || _tablaLineasFactura.LaTabla.Rows.Count == 0)
                {
                    MessageBox.Show("No hay líneas para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using var sfd = new SaveFileDialog
                {
                    Filter = "CSV (*.csv)|*.csv",
                    FileName = $"facemilin_{idFactura}.csv"
                };

                if (sfd.ShowDialog(this) != DialogResult.OK) return;

                ExportarDatos.ExportarCSV(_tablaLineasFactura.LaTabla, sfd.FileName);
                SetStatus("Exportado CSV.");
            }
            catch (Exception ex)
            {
                Program.appDAM.RegistrarLog("Export CSV líneas", ex.Message);
                MessageBox.Show("Error al exportar CSV.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsBtnExportXML_Click(object sender, EventArgs e)
        {
            try
            {
                if (_tablaLineasFactura?.LaTabla == null || _tablaLineasFactura.LaTabla.Rows.Count == 0)
                {
                    MessageBox.Show("No hay líneas para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using var sfd = new SaveFileDialog
                {
                    Filter = "XML (*.xml)|*.xml",
                    FileName = $"facemilin_{idFactura}.xml"
                };

                if (sfd.ShowDialog(this) != DialogResult.OK) return;

                ExportarDatos.ExportarXML(_tablaLineasFactura.LaTabla, sfd.FileName, "facemilin");
                SetStatus("Exportado XML.");
            }
            catch (Exception ex)
            {
                Program.appDAM.RegistrarLog("Export XML líneas", ex.Message);
                MessageBox.Show("Error al exportar XML.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
