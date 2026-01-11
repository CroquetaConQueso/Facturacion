namespace FacturacionDAM.Formularios
{
    partial class FrmBrowFacemi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBrowFacemi));
            splitContainerFacemi = new SplitContainer();
            pnClientes = new Panel();
            dgClientes = new DataGridView();
            pnHeadClientes = new Panel();
            label1 = new Label();
            pnGrid = new Panel();
            dgFacturas = new DataGridView();
            pnHeadFacemi = new Panel();
            lbHeadFacemi = new Label();
            pnStatus = new Panel();
            statusStrip1 = new StatusStrip();
            tsLbNumReg = new ToolStripStatusLabel();
            tsLbStatus = new ToolStripStatusLabel();
            pnMenu = new Panel();
            tsHerramientas = new ToolStrip();
            tsBtnNew = new ToolStripButton();
            tsBtnEdit = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            tsBtnDelete = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            tsBtnFirst = new ToolStripButton();
            tsBtnPrev = new ToolStripButton();
            tsBtnNext = new ToolStripButton();
            tsBtnLast = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            tsBtnExportCSV = new ToolStripButton();
            tsBtnExportXML = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            toolStripLabel1 = new ToolStripLabel();
            tsComboYear = new ToolStripComboBox();
            ((System.ComponentModel.ISupportInitialize)splitContainerFacemi).BeginInit();
            splitContainerFacemi.Panel1.SuspendLayout();
            splitContainerFacemi.Panel2.SuspendLayout();
            splitContainerFacemi.SuspendLayout();
            pnClientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgClientes).BeginInit();
            pnHeadClientes.SuspendLayout();
            pnGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgFacturas).BeginInit();
            pnHeadFacemi.SuspendLayout();
            pnStatus.SuspendLayout();
            statusStrip1.SuspendLayout();
            pnMenu.SuspendLayout();
            tsHerramientas.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainerFacemi
            // 
            splitContainerFacemi.BorderStyle = BorderStyle.FixedSingle;
            splitContainerFacemi.Dock = DockStyle.Fill;
            splitContainerFacemi.Location = new Point(0, 0);
            splitContainerFacemi.Name = "splitContainerFacemi";
            // 
            // splitContainerFacemi.Panel1
            // 
            splitContainerFacemi.Panel1.Controls.Add(pnClientes);
            splitContainerFacemi.Panel1.Controls.Add(pnHeadClientes);
            splitContainerFacemi.Panel1MinSize = 150;
            // 
            // splitContainerFacemi.Panel2
            // 
            splitContainerFacemi.Panel2.Controls.Add(pnGrid);
            splitContainerFacemi.Panel2.Controls.Add(pnStatus);
            splitContainerFacemi.Panel2.Controls.Add(pnMenu);
            splitContainerFacemi.Panel2MinSize = 250;
            splitContainerFacemi.Size = new Size(846, 450);
            splitContainerFacemi.SplitterDistance = 280;
            splitContainerFacemi.TabIndex = 0;
            // 
            // pnClientes
            // 
            pnClientes.Controls.Add(dgClientes);
            pnClientes.Dock = DockStyle.Fill;
            pnClientes.Location = new Point(0, 27);
            pnClientes.Name = "pnClientes";
            pnClientes.Size = new Size(278, 421);
            pnClientes.TabIndex = 1;
            // 
            // dgClientes
            // 
            dgClientes.AllowUserToAddRows = false;
            dgClientes.AllowUserToDeleteRows = false;
            dgClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgClientes.Dock = DockStyle.Fill;
            dgClientes.Location = new Point(0, 0);
            dgClientes.Name = "dgClientes";
            dgClientes.RowHeadersWidth = 51;
            dgClientes.Size = new Size(278, 421);
            dgClientes.TabIndex = 0;
            dgClientes.SelectionChanged += dgClientes_SelectionChanged;
            // 
            // pnHeadClientes
            // 
            pnHeadClientes.Controls.Add(label1);
            pnHeadClientes.Dock = DockStyle.Top;
            pnHeadClientes.Location = new Point(0, 0);
            pnHeadClientes.Name = "pnHeadClientes";
            pnHeadClientes.Size = new Size(278, 27);
            pnHeadClientes.TabIndex = 0;
            // 
            // label1
            // 
            label1.BackColor = Color.LightGray;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(278, 27);
            label1.TabIndex = 0;
            label1.Text = "Clientes";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnGrid
            // 
            pnGrid.Controls.Add(dgFacturas);
            pnGrid.Controls.Add(pnHeadFacemi);
            pnGrid.Dock = DockStyle.Fill;
            pnGrid.Location = new Point(0, 28);
            pnGrid.Name = "pnGrid";
            pnGrid.Padding = new Padding(0, 2, 0, 0);
            pnGrid.Size = new Size(560, 398);
            pnGrid.TabIndex = 3;
            // 
            // dgFacturas
            // 
            dgFacturas.AllowUserToAddRows = false;
            dgFacturas.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.LightGray;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgFacturas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgFacturas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgFacturas.Dock = DockStyle.Fill;
            dgFacturas.Location = new Point(0, 44);
            dgFacturas.MultiSelect = false;
            dgFacturas.Name = "dgFacturas";
            dgFacturas.ReadOnly = true;
            dgFacturas.RowHeadersWidth = 51;
            dgFacturas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgFacturas.Size = new Size(560, 354);
            dgFacturas.TabIndex = 0;
            dgFacturas.CellMouseDoubleClick += dgFacturas_CellMouseDoubleClick;
            // 
            // pnHeadFacemi
            // 
            pnHeadFacemi.Controls.Add(lbHeadFacemi);
            pnHeadFacemi.Dock = DockStyle.Top;
            pnHeadFacemi.Location = new Point(0, 2);
            pnHeadFacemi.Name = "pnHeadFacemi";
            pnHeadFacemi.Size = new Size(560, 42);
            pnHeadFacemi.TabIndex = 1;
            // 
            // lbHeadFacemi
            // 
            lbHeadFacemi.BackColor = Color.Gainsboro;
            lbHeadFacemi.Dock = DockStyle.Fill;
            lbHeadFacemi.Font = new Font("Segoe UI", 12F);
            lbHeadFacemi.Location = new Point(0, 0);
            lbHeadFacemi.Name = "lbHeadFacemi";
            lbHeadFacemi.Size = new Size(560, 42);
            lbHeadFacemi.TabIndex = 0;
            lbHeadFacemi.Text = "Facturas del cliente seleccionado, en el año seleccionado";
            lbHeadFacemi.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnStatus
            // 
            pnStatus.Controls.Add(statusStrip1);
            pnStatus.Dock = DockStyle.Bottom;
            pnStatus.Location = new Point(0, 426);
            pnStatus.Name = "pnStatus";
            pnStatus.Size = new Size(560, 22);
            pnStatus.TabIndex = 2;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { tsLbNumReg, tsLbStatus });
            statusStrip1.Location = new Point(0, 0);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(560, 22);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // tsLbNumReg
            // 
            tsLbNumReg.Name = "tsLbNumReg";
            tsLbNumReg.Size = new Size(88, 17);
            tsLbNumReg.Text = "Nº de registros:";
            // 
            // tsLbStatus
            // 
            tsLbStatus.Margin = new Padding(10, 3, 0, 2);
            tsLbStatus.Name = "tsLbStatus";
            tsLbStatus.Size = new Size(0, 17);
            // 
            // pnMenu
            // 
            pnMenu.Controls.Add(tsHerramientas);
            pnMenu.Dock = DockStyle.Top;
            pnMenu.Location = new Point(0, 0);
            pnMenu.Name = "pnMenu";
            pnMenu.Size = new Size(560, 28);
            pnMenu.TabIndex = 1;
            // 
            // tsHerramientas
            // 
            tsHerramientas.ImageScalingSize = new Size(20, 20);
            tsHerramientas.Items.AddRange(new ToolStripItem[] { tsBtnNew, tsBtnEdit, toolStripSeparator1, tsBtnDelete, toolStripSeparator2, tsBtnFirst, tsBtnPrev, tsBtnNext, tsBtnLast, toolStripSeparator3, tsBtnExportCSV, tsBtnExportXML, toolStripSeparator4, toolStripLabel1, tsComboYear });
            tsHerramientas.Location = new Point(0, 0);
            tsHerramientas.Name = "tsHerramientas";
            tsHerramientas.Size = new Size(560, 27);
            tsHerramientas.TabIndex = 0;
            tsHerramientas.Text = "toolStrip1";
            // 
            // tsBtnNew
            // 
            tsBtnNew.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnNew.Image = (Image)resources.GetObject("tsBtnNew.Image");
            tsBtnNew.ImageTransparentColor = Color.Magenta;
            tsBtnNew.Name = "tsBtnNew";
            tsBtnNew.Size = new Size(24, 24);
            tsBtnNew.Text = "Nuevo registro";
            tsBtnNew.Click += tsBtnNew_Click;
            // 
            // tsBtnEdit
            // 
            tsBtnEdit.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnEdit.Image = (Image)resources.GetObject("tsBtnEdit.Image");
            tsBtnEdit.ImageTransparentColor = Color.Magenta;
            tsBtnEdit.Name = "tsBtnEdit";
            tsBtnEdit.Size = new Size(24, 24);
            tsBtnEdit.Text = "Editar registro";
            tsBtnEdit.Click += tsBtnEdit_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 27);
            // 
            // tsBtnDelete
            // 
            tsBtnDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnDelete.Image = (Image)resources.GetObject("tsBtnDelete.Image");
            tsBtnDelete.ImageTransparentColor = Color.Magenta;
            tsBtnDelete.Name = "tsBtnDelete";
            tsBtnDelete.Size = new Size(24, 24);
            tsBtnDelete.Text = "Eliminar registro";
            tsBtnDelete.Click += tsBtnDelete_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 27);
            // 
            // tsBtnFirst
            // 
            tsBtnFirst.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnFirst.Image = (Image)resources.GetObject("tsBtnFirst.Image");
            tsBtnFirst.ImageTransparentColor = Color.Magenta;
            tsBtnFirst.Name = "tsBtnFirst";
            tsBtnFirst.Size = new Size(24, 24);
            tsBtnFirst.Text = "Primer registro";
            tsBtnFirst.Click += tsBtnFirst_Click;
            // 
            // tsBtnPrev
            // 
            tsBtnPrev.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnPrev.Image = (Image)resources.GetObject("tsBtnPrev.Image");
            tsBtnPrev.ImageTransparentColor = Color.Magenta;
            tsBtnPrev.Name = "tsBtnPrev";
            tsBtnPrev.Size = new Size(24, 24);
            tsBtnPrev.Text = "Registro anterior";
            tsBtnPrev.Click += tsBtnPrev_Click;
            // 
            // tsBtnNext
            // 
            tsBtnNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnNext.Image = (Image)resources.GetObject("tsBtnNext.Image");
            tsBtnNext.ImageTransparentColor = Color.Magenta;
            tsBtnNext.Name = "tsBtnNext";
            tsBtnNext.Size = new Size(24, 24);
            tsBtnNext.Text = "Siguiente registro";
            tsBtnNext.Click += tsBtnNext_Click;
            // 
            // tsBtnLast
            // 
            tsBtnLast.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnLast.Image = (Image)resources.GetObject("tsBtnLast.Image");
            tsBtnLast.ImageTransparentColor = Color.Magenta;
            tsBtnLast.Name = "tsBtnLast";
            tsBtnLast.Size = new Size(24, 24);
            tsBtnLast.Text = "Último registro";
            tsBtnLast.Click += tsBtnLast_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 27);
            // 
            // tsBtnExportCSV
            // 
            tsBtnExportCSV.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnExportCSV.Image = (Image)resources.GetObject("tsBtnExportCSV.Image");
            tsBtnExportCSV.ImageTransparentColor = Color.Magenta;
            tsBtnExportCSV.Name = "tsBtnExportCSV";
            tsBtnExportCSV.Size = new Size(24, 24);
            tsBtnExportCSV.Text = "toolStripButton1";
            tsBtnExportCSV.ToolTipText = "Exportar a formato CSV";
            tsBtnExportCSV.Click += tsBtnExportCSV_Click;
            // 
            // tsBtnExportXML
            // 
            tsBtnExportXML.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnExportXML.Image = (Image)resources.GetObject("tsBtnExportXML.Image");
            tsBtnExportXML.ImageTransparentColor = Color.Magenta;
            tsBtnExportXML.Name = "tsBtnExportXML";
            tsBtnExportXML.Size = new Size(24, 24);
            tsBtnExportXML.Text = "toolStripButton1";
            tsBtnExportXML.ToolTipText = "Exportar a formato XML";
            tsBtnExportXML.Click += tsBtnExportXML_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Margin = new Padding(5, 0, 20, 0);
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 27);
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(35, 24);
            toolStripLabel1.Text = "Año: ";
            // 
            // tsComboYear
            // 
            tsComboYear.BackColor = Color.Azure;
            tsComboYear.DropDownStyle = ComboBoxStyle.DropDownList;
            tsComboYear.Name = "tsComboYear";
            tsComboYear.Size = new Size(75, 27);
            tsComboYear.SelectedIndexChanged += tsComboYear_SelectedIndexChanged;
            // 
            // FrmBrowFacemi
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(846, 450);
            Controls.Add(splitContainerFacemi);
            Name = "FrmBrowFacemi";
            Text = "Facturas Emitidas";
            FormClosing += FrmBrowFacemi_FormClosing;
            Load += FrmBrowFacemi_Load;
            Shown += FrmBrowFacemi_Shown;
            splitContainerFacemi.Panel1.ResumeLayout(false);
            splitContainerFacemi.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerFacemi).EndInit();
            splitContainerFacemi.ResumeLayout(false);
            pnClientes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgClientes).EndInit();
            pnHeadClientes.ResumeLayout(false);
            pnGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgFacturas).EndInit();
            pnHeadFacemi.ResumeLayout(false);
            pnStatus.ResumeLayout(false);
            pnStatus.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            pnMenu.ResumeLayout(false);
            pnMenu.PerformLayout();
            tsHerramientas.ResumeLayout(false);
            tsHerramientas.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainerFacemi;
        private Panel pnHeadClientes;
        private Label label1;
        private Panel pnMenu;
        private ToolStrip tsHerramientas;
        private ToolStripButton tsBtnNew;
        private ToolStripButton tsBtnEdit;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton tsBtnDelete;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton tsBtnFirst;
        private ToolStripButton tsBtnPrev;
        private ToolStripButton tsBtnNext;
        private ToolStripButton tsBtnLast;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton tsBtnExportCSV;
        private ToolStripButton tsBtnExportXML;
        private Panel pnStatus;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tsLbNumReg;
        private ToolStripStatusLabel tsLbStatus;
        private Panel pnGrid;
        private DataGridView dgFacturas;
        private Panel pnClientes;
        private DataGridView dgClientes;
        private ToolStripSeparator toolStripSeparator4;
        private Panel pnHeadFacemi;
        private Label lbHeadFacemi;
        private ToolStripLabel toolStripLabel1;
        private ToolStripComboBox tsComboYear;
    }
}