namespace FacturacionDAM.Formularios
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            pnMenu = new Panel();
            menuMain = new MenuStrip();
            archivoToolStripMenuItem = new ToolStripMenuItem();
            tsItemMenuSeleccionarEmisor = new ToolStripMenuItem();
            tsItemMenuSalir = new ToolStripMenuItem();
            facturaciónToolStripMenuItem = new ToolStripMenuItem();
            conceptosDeFacturaciónToolStripMenuItem = new ToolStripMenuItem();
            tiposDeIVAToolStripMenuItem = new ToolStripMenuItem();
            productosYServiciosToolStripMenuItem = new ToolStripMenuItem();
            ventanasToolStripMenuItem = new ToolStripMenuItem();
            cascadaToolStripMenuItem = new ToolStripMenuItem();
            mosaicoHorizontalToolStripMenuItem = new ToolStripMenuItem();
            mosaicoVerticalToolStripMenuItem = new ToolStripMenuItem();
            cerrarTodasToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            tsMenuItemDepura = new ToolStripMenuItem();
            pnTools = new Panel();
            tsToolMain = new ToolStrip();
            tsBtnVentas = new ToolStripButton();
            tsBtnCompras = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            tsBtnClientes = new ToolStripButton();
            tsBtnProveedores = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            tsBtnEmisores = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            tsBtnConfig = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            tsBtnSalir = new ToolStripButton();
            pnStatus = new Panel();
            statusBar = new StatusStrip();
            tsLbEtiquetaEmisor = new ToolStripStatusLabel();
            tsLbEmisor = new ToolStripStatusLabel();
            tsLbEtiquetaInfo = new ToolStripStatusLabel();
            tsLbEstado = new ToolStripStatusLabel();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            pnMenu.SuspendLayout();
            menuMain.SuspendLayout();
            pnTools.SuspendLayout();
            tsToolMain.SuspendLayout();
            pnStatus.SuspendLayout();
            statusBar.SuspendLayout();
            SuspendLayout();
            // 
            // pnMenu
            // 
            pnMenu.Controls.Add(menuMain);
            pnMenu.Dock = DockStyle.Top;
            pnMenu.Location = new Point(0, 0);
            pnMenu.Name = "pnMenu";
            pnMenu.Size = new Size(936, 31);
            pnMenu.TabIndex = 3;
            // 
            // menuMain
            // 
            menuMain.BackColor = Color.Azure;
            menuMain.ImageScalingSize = new Size(20, 20);
            menuMain.Items.AddRange(new ToolStripItem[] { archivoToolStripMenuItem, facturaciónToolStripMenuItem, ventanasToolStripMenuItem, helpToolStripMenuItem });
            menuMain.Location = new Point(0, 0);
            menuMain.Name = "menuMain";
            menuMain.Size = new Size(936, 24);
            menuMain.TabIndex = 0;
            menuMain.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            archivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tsItemMenuSeleccionarEmisor, tsItemMenuSalir });
            archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            archivoToolStripMenuItem.Size = new Size(60, 20);
            archivoToolStripMenuItem.Text = "&Archivo";
            // 
            // tsItemMenuSeleccionarEmisor
            // 
            tsItemMenuSeleccionarEmisor.Name = "tsItemMenuSeleccionarEmisor";
            tsItemMenuSeleccionarEmisor.Size = new Size(173, 22);
            tsItemMenuSeleccionarEmisor.Text = "Seleccionar &Emisor";
            tsItemMenuSeleccionarEmisor.Click += tsItemMenuSeleccionarEmisor_Click;
            // 
            // tsItemMenuSalir
            // 
            tsItemMenuSalir.Name = "tsItemMenuSalir";
            tsItemMenuSalir.ShortcutKeys = Keys.Control | Keys.F4;
            tsItemMenuSalir.Size = new Size(173, 22);
            tsItemMenuSalir.Text = "&Salir";
            tsItemMenuSalir.Click += tsBtnSalir_Click;
            // 
            // facturaciónToolStripMenuItem
            // 
            facturaciónToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { conceptosDeFacturaciónToolStripMenuItem, tiposDeIVAToolStripMenuItem, productosYServiciosToolStripMenuItem });
            facturaciónToolStripMenuItem.Name = "facturaciónToolStripMenuItem";
            facturaciónToolStripMenuItem.Size = new Size(81, 20);
            facturaciónToolStripMenuItem.Text = "Facturación";
            // 
            // conceptosDeFacturaciónToolStripMenuItem
            // 
            conceptosDeFacturaciónToolStripMenuItem.Name = "conceptosDeFacturaciónToolStripMenuItem";
            conceptosDeFacturaciónToolStripMenuItem.Size = new Size(212, 22);
            conceptosDeFacturaciónToolStripMenuItem.Text = "Conceptos de Facturación";
            conceptosDeFacturaciónToolStripMenuItem.Click += conceptosDeFacturaciónToolStripMenuItem_Click;
            // 
            // tiposDeIVAToolStripMenuItem
            // 
            tiposDeIVAToolStripMenuItem.Name = "tiposDeIVAToolStripMenuItem";
            tiposDeIVAToolStripMenuItem.Size = new Size(212, 22);
            tiposDeIVAToolStripMenuItem.Text = "Tipos de IVA";
            tiposDeIVAToolStripMenuItem.Click += tiposDeIVAToolStripMenuItem_Click;
            // 
            // productosYServiciosToolStripMenuItem
            // 
            productosYServiciosToolStripMenuItem.Name = "productosYServiciosToolStripMenuItem";
            productosYServiciosToolStripMenuItem.Size = new Size(212, 22);
            productosYServiciosToolStripMenuItem.Text = "Productos y Servicios";
            productosYServiciosToolStripMenuItem.Click += productosYServiciosToolStripMenuItem_Click;
            // 
            // ventanasToolStripMenuItem
            // 
            ventanasToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cascadaToolStripMenuItem, mosaicoHorizontalToolStripMenuItem, mosaicoVerticalToolStripMenuItem, cerrarTodasToolStripMenuItem });
            ventanasToolStripMenuItem.Name = "ventanasToolStripMenuItem";
            ventanasToolStripMenuItem.Size = new Size(66, 20);
            ventanasToolStripMenuItem.Text = "&Ventanas";
            // 
            // cascadaToolStripMenuItem
            // 
            cascadaToolStripMenuItem.Name = "cascadaToolStripMenuItem";
            cascadaToolStripMenuItem.Size = new Size(175, 22);
            cascadaToolStripMenuItem.Text = "&Cascada";
            cascadaToolStripMenuItem.Click += cascadaToolStripMenuItem_Click;
            // 
            // mosaicoHorizontalToolStripMenuItem
            // 
            mosaicoHorizontalToolStripMenuItem.Name = "mosaicoHorizontalToolStripMenuItem";
            mosaicoHorizontalToolStripMenuItem.Size = new Size(175, 22);
            mosaicoHorizontalToolStripMenuItem.Text = "Mosaico &horizontal";
            mosaicoHorizontalToolStripMenuItem.Click += mosaicoHorizontalToolStripMenuItem_Click;
            // 
            // mosaicoVerticalToolStripMenuItem
            // 
            mosaicoVerticalToolStripMenuItem.Name = "mosaicoVerticalToolStripMenuItem";
            mosaicoVerticalToolStripMenuItem.Size = new Size(175, 22);
            mosaicoVerticalToolStripMenuItem.Text = "Mosaico ve&rtical";
            mosaicoVerticalToolStripMenuItem.Click += mosaicoVerticalToolStripMenuItem_Click;
            // 
            // cerrarTodasToolStripMenuItem
            // 
            cerrarTodasToolStripMenuItem.Name = "cerrarTodasToolStripMenuItem";
            cerrarTodasToolStripMenuItem.Size = new Size(175, 22);
            cerrarTodasToolStripMenuItem.Text = "Cerrar &todas";
            cerrarTodasToolStripMenuItem.Click += cerrarTodasToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tsMenuItemDepura });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(53, 20);
            helpToolStripMenuItem.Text = "Ay&uda";
            // 
            // tsMenuItemDepura
            // 
            tsMenuItemDepura.Name = "tsMenuItemDepura";
            tsMenuItemDepura.Size = new Size(135, 22);
            tsMenuItemDepura.Text = "&Depuración";
            tsMenuItemDepura.Click += tsMenuItemDepura_Click;
            // 
            // pnTools
            // 
            pnTools.BackColor = Color.Azure;
            pnTools.Controls.Add(tsToolMain);
            pnTools.Dock = DockStyle.Left;
            pnTools.Location = new Point(0, 31);
            pnTools.Name = "pnTools";
            pnTools.Padding = new Padding(8);
            pnTools.Size = new Size(115, 637);
            pnTools.TabIndex = 4;
            // 
            // tsToolMain
            // 
            tsToolMain.Dock = DockStyle.Fill;
            tsToolMain.ImageScalingSize = new Size(64, 64);
            tsToolMain.Items.AddRange(new ToolStripItem[] { tsBtnVentas, tsBtnCompras, toolStripSeparator1, tsBtnClientes, tsBtnProveedores, toolStripSeparator2, tsBtnEmisores, toolStripSeparator3, tsBtnConfig, toolStripSeparator4, tsBtnSalir });
            tsToolMain.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
            tsToolMain.Location = new Point(8, 8);
            tsToolMain.Name = "tsToolMain";
            tsToolMain.Size = new Size(99, 621);
            tsToolMain.TabIndex = 3;
            tsToolMain.Text = "toolStrip1";
            // 
            // tsBtnVentas
            // 
            tsBtnVentas.Image = (Image)resources.GetObject("tsBtnVentas.Image");
            tsBtnVentas.ImageScaling = ToolStripItemImageScaling.None;
            tsBtnVentas.ImageTransparentColor = Color.Magenta;
            tsBtnVentas.Margin = new Padding(0, 10, 0, 2);
            tsBtnVentas.Name = "tsBtnVentas";
            tsBtnVentas.Size = new Size(97, 51);
            tsBtnVentas.Text = "Ventas";
            tsBtnVentas.TextAlign = ContentAlignment.BottomCenter;
            tsBtnVentas.TextImageRelation = TextImageRelation.ImageAboveText;
            tsBtnVentas.Click += tsBtnVentas_Click;
            // 
            // tsBtnCompras
            // 
            tsBtnCompras.Image = (Image)resources.GetObject("tsBtnCompras.Image");
            tsBtnCompras.ImageScaling = ToolStripItemImageScaling.None;
            tsBtnCompras.ImageTransparentColor = Color.Magenta;
            tsBtnCompras.Margin = new Padding(0, 10, 0, 2);
            tsBtnCompras.Name = "tsBtnCompras";
            tsBtnCompras.Size = new Size(97, 51);
            tsBtnCompras.Text = "Compras";
            tsBtnCompras.TextImageRelation = TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Margin = new Padding(5);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(87, 6);
            // 
            // tsBtnClientes
            // 
            tsBtnClientes.Image = (Image)resources.GetObject("tsBtnClientes.Image");
            tsBtnClientes.ImageScaling = ToolStripItemImageScaling.None;
            tsBtnClientes.ImageTransparentColor = Color.Magenta;
            tsBtnClientes.Margin = new Padding(0, 10, 0, 2);
            tsBtnClientes.Name = "tsBtnClientes";
            tsBtnClientes.Size = new Size(97, 51);
            tsBtnClientes.Text = "Clientes";
            tsBtnClientes.TextImageRelation = TextImageRelation.ImageAboveText;
            tsBtnClientes.Click += tsBtnClientes_Click;
            // 
            // tsBtnProveedores
            // 
            tsBtnProveedores.Image = (Image)resources.GetObject("tsBtnProveedores.Image");
            tsBtnProveedores.ImageScaling = ToolStripItemImageScaling.None;
            tsBtnProveedores.ImageTransparentColor = Color.Magenta;
            tsBtnProveedores.Margin = new Padding(0, 10, 0, 2);
            tsBtnProveedores.Name = "tsBtnProveedores";
            tsBtnProveedores.Size = new Size(97, 51);
            tsBtnProveedores.Text = "Proveedores";
            tsBtnProveedores.TextImageRelation = TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Margin = new Padding(5);
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(87, 6);
            // 
            // tsBtnEmisores
            // 
            tsBtnEmisores.Image = (Image)resources.GetObject("tsBtnEmisores.Image");
            tsBtnEmisores.ImageScaling = ToolStripItemImageScaling.None;
            tsBtnEmisores.ImageTransparentColor = Color.Magenta;
            tsBtnEmisores.Margin = new Padding(0, 10, 0, 2);
            tsBtnEmisores.Name = "tsBtnEmisores";
            tsBtnEmisores.Size = new Size(97, 51);
            tsBtnEmisores.Text = "Emisores";
            tsBtnEmisores.TextImageRelation = TextImageRelation.ImageAboveText;
            tsBtnEmisores.Click += tsBtnEmisores_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Margin = new Padding(5);
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(87, 6);
            // 
            // tsBtnConfig
            // 
            tsBtnConfig.Image = (Image)resources.GetObject("tsBtnConfig.Image");
            tsBtnConfig.ImageScaling = ToolStripItemImageScaling.None;
            tsBtnConfig.ImageTransparentColor = Color.Magenta;
            tsBtnConfig.Margin = new Padding(0, 10, 0, 2);
            tsBtnConfig.Name = "tsBtnConfig";
            tsBtnConfig.Size = new Size(97, 51);
            tsBtnConfig.Text = "Configuración";
            tsBtnConfig.TextImageRelation = TextImageRelation.ImageAboveText;
            tsBtnConfig.Click += tsBtnConfig_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Margin = new Padding(5);
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(87, 6);
            // 
            // tsBtnSalir
            // 
            tsBtnSalir.Image = (Image)resources.GetObject("tsBtnSalir.Image");
            tsBtnSalir.ImageScaling = ToolStripItemImageScaling.None;
            tsBtnSalir.ImageTransparentColor = Color.Magenta;
            tsBtnSalir.Margin = new Padding(0, 10, 0, 2);
            tsBtnSalir.Name = "tsBtnSalir";
            tsBtnSalir.Size = new Size(97, 51);
            tsBtnSalir.Text = "Salir";
            tsBtnSalir.TextImageRelation = TextImageRelation.ImageAboveText;
            tsBtnSalir.Click += tsBtnSalir_Click;
            // 
            // pnStatus
            // 
            pnStatus.Controls.Add(statusBar);
            pnStatus.Dock = DockStyle.Bottom;
            pnStatus.Location = new Point(115, 650);
            pnStatus.Name = "pnStatus";
            pnStatus.Size = new Size(821, 18);
            pnStatus.TabIndex = 5;
            // 
            // statusBar
            // 
            statusBar.ImageScalingSize = new Size(20, 20);
            statusBar.Items.AddRange(new ToolStripItem[] { tsLbEtiquetaEmisor, tsLbEmisor, tsLbEtiquetaInfo, tsLbEstado });
            statusBar.Location = new Point(0, -8);
            statusBar.Name = "statusBar";
            statusBar.Size = new Size(821, 26);
            statusBar.TabIndex = 0;
            statusBar.Text = "statusStrip1";
            // 
            // tsLbEtiquetaEmisor
            // 
            tsLbEtiquetaEmisor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            tsLbEtiquetaEmisor.Margin = new Padding(0, 4, 6, 2);
            tsLbEtiquetaEmisor.Name = "tsLbEtiquetaEmisor";
            tsLbEtiquetaEmisor.Size = new Size(47, 20);
            tsLbEtiquetaEmisor.Text = "Emisor:";
            // 
            // tsLbEmisor
            // 
            tsLbEmisor.AutoSize = false;
            tsLbEmisor.Name = "tsLbEmisor";
            tsLbEmisor.Size = new Size(250, 21);
            tsLbEmisor.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tsLbEtiquetaInfo
            // 
            tsLbEtiquetaInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            tsLbEtiquetaInfo.Margin = new Padding(0, 4, 6, 2);
            tsLbEtiquetaInfo.Name = "tsLbEtiquetaInfo";
            tsLbEtiquetaInfo.Size = new Size(46, 20);
            tsLbEtiquetaInfo.Text = "Estado:";
            // 
            // tsLbEstado
            // 
            tsLbEstado.Margin = new Padding(10, 4, 0, 2);
            tsLbEstado.Name = "tsLbEstado";
            tsLbEstado.Size = new Size(0, 20);
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(936, 668);
            Controls.Add(pnStatus);
            Controls.Add(pnTools);
            Controls.Add(pnMenu);
            IsMdiContainer = true;
            Name = "FrmMain";
            Text = "Factura DAM";
            WindowState = FormWindowState.Maximized;
            FormClosing += FrmMain_FormClosing;
            Load += FrmMain_Load;
            pnMenu.ResumeLayout(false);
            pnMenu.PerformLayout();
            menuMain.ResumeLayout(false);
            menuMain.PerformLayout();
            pnTools.ResumeLayout(false);
            pnTools.PerformLayout();
            tsToolMain.ResumeLayout(false);
            tsToolMain.PerformLayout();
            pnStatus.ResumeLayout(false);
            pnStatus.PerformLayout();
            statusBar.ResumeLayout(false);
            statusBar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel pnMenu;
        private MenuStrip menuMain;
        private Panel pnTools;
        private Panel pnStatus;
        private StatusStrip statusBar;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private ToolStrip tsToolMain;
        private ToolStripButton tsBtnVentas;
        private ToolStripButton tsBtnCompras;
        private ToolStripButton tsBtnClientes;
        private ToolStripButton tsBtnProveedores;
        private ToolStripButton tsBtnEmisores;
        private ToolStripButton tsBtnConfig;
        private ToolStripButton tsBtnSalir;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripStatusLabel tsLbEmisor;
        private ToolStripStatusLabel tsLbEstado;
        private ToolStripStatusLabel tsLbEtiquetaEmisor;
        private ToolStripStatusLabel tsLbEtiquetaInfo;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem tsMenuItemDepura;
        private ToolStripMenuItem archivoToolStripMenuItem;
        private ToolStripMenuItem tsItemMenuSeleccionarEmisor;
        private ToolStripMenuItem tsItemMenuSalir;
        private ToolStripMenuItem ventanasToolStripMenuItem;
        private ToolStripMenuItem cascadaToolStripMenuItem;
        private ToolStripMenuItem mosaicoHorizontalToolStripMenuItem;
        private ToolStripMenuItem mosaicoVerticalToolStripMenuItem;
        private ToolStripMenuItem cerrarTodasToolStripMenuItem;
        private ToolStripMenuItem facturaciónToolStripMenuItem;
        private ToolStripMenuItem conceptosDeFacturaciónToolStripMenuItem;
        private ToolStripMenuItem tiposDeIVAToolStripMenuItem;
        private ToolStripMenuItem productosYServiciosToolStripMenuItem;
    }
}
