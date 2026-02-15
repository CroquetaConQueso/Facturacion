using FacturacionDAM.Modelos;
using System;
using System.Linq;
using System.Windows.Forms;

namespace FacturacionDAM.Formularios
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.appDAM.conectado)
                Program.appDAM.DesconectarDB();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
#if !DEBUG
            tsMenuItemDepura.Visible = false;
#endif
            menuMain.MdiWindowListItem = ventanasToolStripMenuItem;

            HookToolButton(tsToolMain, "tsBtnCompras", "Compras", tsBtnCompras_Click);
            HookToolButton(tsToolMain, "tsBtnProveedores", "Proveedores", tsBtnProveedores_Click);

            SeleccionarEmisor();
            RefreshControles();
        }

        private void tsBtnConfig_Click(object sender, EventArgs e) => AbrirFormularioHijo<FrmConfig>();

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            CerrarFormulariosHijos();
            Close();
        }

        private void tsMenuItemDepura_Click(object sender, EventArgs e)
        {
#if DEBUG
            AbrirFormularioHijo<FrmDepuracion>();
#endif
        }
        private void tsBtnProveedores_Click_1(object sender, EventArgs e) =>AbrirFormularioHijo<FrmBrowProveedores>();

        private void tsBtnEmisores_Click(object sender, EventArgs e) => AbrirFormularioHijo<FrmBrowEmisores>();

        private void tsBtnClientes_Click(object sender, EventArgs e) => AbrirFormularioHijoPorNombre("FrmBrowClientes", "FrmBrowCliente");

        private void conceptosDeFacturaciónToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormularioHijo<FrmBrowConceptosFac>();

        private void tiposDeIVAToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormularioHijo<FrmBrowTiposIva>();

        private void productosYServiciosToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormularioHijo<FrmBrowProductos>();

        private void tsBtnVentas_Click(object sender, EventArgs e)
        {
            if (Program.appDAM.emisor == null)
            {
                MessageBox.Show("Debe seleccionar un emisor antes de gestionar facturas.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Program.appDAM.HayClientes())
            {
                MessageBox.Show(
                    "No hay clientes registrados.\nDebe registrar al menos un cliente antes de gestionar facturas.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AbrirFormularioHijo<FrmBrowFacemi>();
        }

        private void tsBtnCompras_Click(object sender, EventArgs e)
        {
            if (Program.appDAM.emisor == null)
            {
                MessageBox.Show("Debe seleccionar un emisor (empresa) antes de gestionar compras.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Program.appDAM.HayProveedores())
            {
                MessageBox.Show(
                    "No hay proveedores registrados.\nDebe registrar al menos un proveedor antes de gestionar facturas recibidas.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AbrirFormularioHijo<FrmBrowFacrec>();
        }

        private void tsBtnProveedores_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijoPorNombre("FrmBrowProveedores", "FrmBrowProveedor", "FrmProveedores", "FrmProveedor");
        }

        private void tsItemMenuSeleccionarEmisor_Click(object sender, EventArgs e)
        {
            CerrarFormulariosHijos();
            SeleccionarEmisor();
            RefreshControles();
        }

        private void cascadaToolStripMenuItem_Click(object sender, EventArgs e) => LayoutMdi(MdiLayout.Cascade);

        private void mosaicoHorizontalToolStripMenuItem_Click(object sender, EventArgs e) => LayoutMdi(MdiLayout.TileHorizontal);

        private void mosaicoVerticalToolStripMenuItem_Click(object sender, EventArgs e) => LayoutMdi(MdiLayout.TileVertical);

        private void cerrarTodasToolStripMenuItem_Click(object sender, EventArgs e) => CerrarFormulariosHijos();

        private void SeleccionarEmisor()
        {
            if ((Program.appDAM.estadoApp == EstadoApp.ConectadoSinEmisor) ||
                (Program.appDAM.estadoApp == EstadoApp.Conectado))
            {
                using (var frm = new FrmSeleccionarEmisor())
                {
                    frm.ShowDialog();
                    Program.appDAM.estadoApp =
                        (Program.appDAM.emisor == null) ? EstadoApp.ConectadoSinEmisor : EstadoApp.Conectado;
                }
            }
        }

        private void CerrarFormulariosHijos()
        {
            foreach (Form frm in MdiChildren)
                frm.Close();
        }

        private void AbrirFormularioHijo<T>() where T : Form, new()
        {
            foreach (Form frm in MdiChildren)
            {
                if (frm is T)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;

                    frm.Activate();
                    return;
                }
            }

            T nuevoFrm = new T
            {
                MdiParent = this,
                WindowState = FormWindowState.Maximized
            };
            nuevoFrm.Show();
        }

        private void AbrirFormularioHijoPorNombre(params string[] nombres)
        {
            var asm = typeof(FrmMain).Assembly;

            Type tipo = null;
            foreach (var n in nombres)
            {
                tipo = asm.GetTypes().FirstOrDefault(t => typeof(Form).IsAssignableFrom(t) && t.Name == n);
                if (tipo != null) break;
            }

            if (tipo == null)
                return;

            foreach (Form frm in MdiChildren)
            {
                if (frm.GetType() == tipo)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;

                    frm.Activate();
                    return;
                }
            }

            var nuevo = (Form)Activator.CreateInstance(tipo);
            nuevo.MdiParent = this;
            nuevo.WindowState = FormWindowState.Maximized;
            nuevo.Show();
        }

        private void HookToolButton(ToolStrip strip, string name, string text, EventHandler handler)
        {
            ToolStripItem item = strip.Items.Cast<ToolStripItem>()
                .FirstOrDefault(i => i.Name == name);

            if (item == null)
            {
                item = strip.Items.Cast<ToolStripItem>()
                    .FirstOrDefault(i => string.Equals(i.Text?.Trim(), text, StringComparison.OrdinalIgnoreCase));
            }

            if (item != null)
            {
                item.Click -= handler;
                item.Click += handler;
            }
        }

        private void RefreshToolBar()
        {
            bool conectado = (Program.appDAM.estadoApp == EstadoApp.Conectado ||
                              Program.appDAM.estadoApp == EstadoApp.ConectadoSinEmisor);

            foreach (ToolStripItem item in tsToolMain.Items)
            {
                if (item is ToolStripButton)
                {
                    switch (item.Name)
                    {
                        case "tsBtnConfig":
                        case "tsBtnSalir":
                            item.Enabled = true;
                            break;
                        default:
                            item.Enabled = conectado;
                            break;
                    }
                }
            }
        }

        private void RefreshStatusBar()
        {
            if (Program.appDAM.emisor == null)
                tsLbEmisor.Text = "Sin emisor seleccionado";
            else
                tsLbEmisor.Text = $"{Program.appDAM.emisor.nombre} {Program.appDAM.emisor.apellidos};  NIF: {Program.appDAM.emisor.nifcif}";

            switch (Program.appDAM.estadoApp)
            {
                case EstadoApp.Conectado:
                    tsLbEstado.Text = "Conectado a la base de datos";
                    break;
                case EstadoApp.SinConexion:
                    tsLbEstado.Text = "No se ha establecido la conexión a la base de datos.";
                    break;
                case EstadoApp.ConectadoSinEmisor:
                    tsLbEstado.Text = "Conectado a la base de datos, pero no se ha seleccionado un emisor.";
                    break;
                case EstadoApp.Error:
                    tsLbEstado.Text = (Program.appDAM.ultimoError != "")
                        ? "Se ha producido un error, revisa el log para más detalles."
                        : "Se ha producido un error";
                    break;
            }
        }

        private void RefreshControles()
        {
            RefreshToolBar();
            RefreshStatusBar();
        }

    }
}
