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
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ayudaToolStripMenuItem = new ToolStripMenuItem();
            depuracionToolStripMenuItem = new ToolStripMenuItem();
            pnMenu.SuspendLayout();
            menuMain.SuspendLayout();
            pnTools.SuspendLayout();
            tsToolMain.SuspendLayout();
            pnStatus.SuspendLayout();
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
            menuMain.Items.AddRange(new ToolStripItem[] { ayudaToolStripMenuItem });
            menuMain.Location = new Point(0, 0);
            menuMain.Name = "menuMain";
            menuMain.Size = new Size(936, 24);
            menuMain.TabIndex = 0;
            menuMain.Text = "menuStrip1";
            // 
            // pnTools
            // 
            pnTools.BackColor = Color.Azure;
            pnTools.Controls.Add(tsToolMain);
            pnTools.Dock = DockStyle.Left;
            pnTools.Location = new Point(0, 31);
            pnTools.Name = "pnTools";
            pnTools.Padding = new Padding(8);
            pnTools.Size = new Size(93, 637);
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
            tsToolMain.Size = new Size(77, 621);
            tsToolMain.TabIndex = 3;
            tsToolMain.Text = "toolStrip1";
            // 
            // tsBtnVentas
            // 
            tsBtnVentas.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnVentas.Image = (Image)resources.GetObject("tsBtnVentas.Image");
            tsBtnVentas.ImageTransparentColor = Color.Magenta;
            tsBtnVentas.Name = "tsBtnVentas";
            tsBtnVentas.Size = new Size(75, 68);
            tsBtnVentas.Text = "Ventas";
            tsBtnVentas.TextAlign = ContentAlignment.BottomCenter;
            tsBtnVentas.TextImageRelation = TextImageRelation.ImageAboveText;
            // 
            // tsBtnCompras
            // 
            tsBtnCompras.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnCompras.Image = (Image)resources.GetObject("tsBtnCompras.Image");
            tsBtnCompras.ImageTransparentColor = Color.Magenta;
            tsBtnCompras.Name = "tsBtnCompras";
            tsBtnCompras.Size = new Size(75, 68);
            tsBtnCompras.Text = "Compras";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Margin = new Padding(5);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(65, 6);
            // 
            // tsBtnClientes
            // 
            tsBtnClientes.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnClientes.Image = (Image)resources.GetObject("tsBtnClientes.Image");
            tsBtnClientes.ImageTransparentColor = Color.Magenta;
            tsBtnClientes.Name = "tsBtnClientes";
            tsBtnClientes.Size = new Size(75, 68);
            tsBtnClientes.Text = "Clientes";
            // 
            // tsBtnProveedores
            // 
            tsBtnProveedores.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnProveedores.Image = (Image)resources.GetObject("tsBtnProveedores.Image");
            tsBtnProveedores.ImageTransparentColor = Color.Magenta;
            tsBtnProveedores.Name = "tsBtnProveedores";
            tsBtnProveedores.Size = new Size(75, 68);
            tsBtnProveedores.Text = "Proveedores";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Margin = new Padding(5);
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(65, 6);
            // 
            // tsBtnEmisores
            // 
            tsBtnEmisores.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnEmisores.Image = (Image)resources.GetObject("tsBtnEmisores.Image");
            tsBtnEmisores.ImageTransparentColor = Color.Magenta;
            tsBtnEmisores.Name = "tsBtnEmisores";
            tsBtnEmisores.Size = new Size(75, 68);
            tsBtnEmisores.Text = "Emisores";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Margin = new Padding(5);
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(65, 6);
            // 
            // tsBtnConfig
            // 
            tsBtnConfig.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnConfig.Image = (Image)resources.GetObject("tsBtnConfig.Image");
            tsBtnConfig.ImageTransparentColor = Color.Magenta;
            tsBtnConfig.Name = "tsBtnConfig";
            tsBtnConfig.Size = new Size(75, 68);
            tsBtnConfig.Text = "Configuración";
            tsBtnConfig.Click += tsBtnConfig_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Margin = new Padding(5);
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(65, 6);
            // 
            // tsBtnSalir
            // 
            tsBtnSalir.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnSalir.Image = (Image)resources.GetObject("tsBtnSalir.Image");
            tsBtnSalir.ImageTransparentColor = Color.Magenta;
            tsBtnSalir.Name = "tsBtnSalir";
            tsBtnSalir.Size = new Size(75, 68);
            tsBtnSalir.Text = "Salir";
            tsBtnSalir.Click += tsBtnSalir_Click;
            // 
            // pnStatus
            // 
            pnStatus.Controls.Add(statusBar);
            pnStatus.Dock = DockStyle.Bottom;
            pnStatus.Location = new Point(93, 641);
            pnStatus.Name = "pnStatus";
            pnStatus.Size = new Size(843, 27);
            pnStatus.TabIndex = 5;
            // 
            // statusBar
            // 
            statusBar.Location = new Point(0, 5);
            statusBar.Name = "statusBar";
            statusBar.Size = new Size(843, 22);
            statusBar.TabIndex = 0;
            statusBar.Text = "statusStrip1";
            // 
            // ayudaToolStripMenuItem
            // 
            ayudaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { depuracionToolStripMenuItem });
            ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            ayudaToolStripMenuItem.Size = new Size(53, 20);
            ayudaToolStripMenuItem.Text = "&Ayuda";
            // 
            // depuracionToolStripMenuItem
            // 
            depuracionToolStripMenuItem.Name = "depuracionToolStripMenuItem";
            depuracionToolStripMenuItem.Size = new Size(180, 22);
            depuracionToolStripMenuItem.Text = "&Depuracion";
            depuracionToolStripMenuItem.Click += depuracionToolStripMenuItem_Click;
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
        private ToolStripMenuItem ayudaToolStripMenuItem;
        private ToolStripMenuItem depuracionToolStripMenuItem;
    }
}
