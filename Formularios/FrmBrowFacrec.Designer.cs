namespace FacturacionDAM.Formularios
{
    partial class FrmBrowFacrec
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBrowFacrec));
            splitContainerFacrec = new SplitContainer();
            pnProveedores = new Panel();
            dgProveedores = new DataGridView();
            pnHeadProveedores = new Panel();
            label1 = new Label();
            pnGrid = new Panel();
            dgFacturas = new DataGridView();
            pnHeadFacrec = new Panel();
            lbHeadFacrec = new Label();
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
            tsLbTotalBase = new ToolStripStatusLabel();
            tsLbTotalCuota = new ToolStripStatusLabel();
            tsLbTotalTotal = new ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)splitContainerFacrec).BeginInit();
            splitContainerFacrec.Panel1.SuspendLayout();
            splitContainerFacrec.Panel2.SuspendLayout();
            splitContainerFacrec.SuspendLayout();
            pnProveedores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgProveedores).BeginInit();
            pnHeadProveedores.SuspendLayout();
            pnGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgFacturas).BeginInit();
            pnHeadFacrec.SuspendLayout();
            pnStatus.SuspendLayout();
            statusStrip1.SuspendLayout();
            pnMenu.SuspendLayout();
            tsHerramientas.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainerFacrec
            // 
            splitContainerFacrec.BorderStyle = BorderStyle.FixedSingle;
            splitContainerFacrec.Dock = DockStyle.Fill;
            splitContainerFacrec.Location = new Point(0, 0);
            splitContainerFacrec.Name = "splitContainerFacrec";
            // 
            // splitContainerFacrec.Panel1
            // 
            splitContainerFacrec.Panel1.Controls.Add(pnProveedores);
            splitContainerFacrec.Panel1.Controls.Add(pnHeadProveedores);
            splitContainerFacrec.Panel1MinSize = 150;
            // 
            // splitContainerFacrec.Panel2
            // 
            splitContainerFacrec.Panel2.Controls.Add(pnGrid);
            splitContainerFacrec.Panel2.Controls.Add(pnStatus);
            splitContainerFacrec.Panel2.Controls.Add(pnMenu);
            splitContainerFacrec.Panel2MinSize = 250;
            splitContainerFacrec.Size = new Size(846, 450);
            splitContainerFacrec.SplitterDistance = 280;
            splitContainerFacrec.TabIndex = 0;
            // 
            // pnProveedores
            // 
            pnProveedores.Controls.Add(dgProveedores);
            pnProveedores.Dock = DockStyle.Fill;
            pnProveedores.Location = new Point(0, 27);
            pnProveedores.Name = "pnProveedores";
            pnProveedores.Size = new Size(278, 421);
            pnProveedores.TabIndex = 1;
            // 
            // dgProveedores
            // 
            dgProveedores.AllowUserToAddRows = false;
            dgProveedores.AllowUserToDeleteRows = false;
            dgProveedores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgProveedores.Dock = DockStyle.Fill;
            dgProveedores.Location = new Point(0, 0);
            dgProveedores.Name = "dgProveedores";
            dgProveedores.RowHeadersWidth = 51;
            dgProveedores.Size = new Size(278, 421);
            dgProveedores.TabIndex = 0;
            dgProveedores.SelectionChanged += dgProveedores_SelectionChanged;
            // 
            // pnHeadProveedores
            // 
            pnHeadProveedores.Controls.Add(label1);
            pnHeadProveedores.Dock = DockStyle.Top;
            pnHeadProveedores.Location = new Point(0, 0);
            pnHeadProveedores.Name = "pnHeadProveedores";
            pnHeadProveedores.Size = new Size(278, 27);
            pnHeadProveedores.TabIndex = 0;
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
            label1.Text = "Proveedores";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnGrid
            // 
            pnGrid.Controls.Add(dgFacturas);
            pnGrid.Controls.Add(pnHeadFacrec);
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
            // pnHeadFacrec
            // 
            pnHeadFacrec.Controls.Add(lbHeadFacrec);
            pnHeadFacrec.Dock = DockStyle.Top;
            pnHeadFacrec.Location = new Point(0, 2);
            pnHeadFacrec.Name = "pnHeadFacrec";
            pnHeadFacrec.Size = new Size(560, 42);
            pnHeadFacrec.TabIndex = 1;
            // 
            // lbHeadFacrec
            // 
            lbHeadFacrec.BackColor = Color.Gainsboro;
            lbHeadFacrec.Dock = DockStyle.Fill;
            lbHeadFacrec.Font = new Font("Segoe UI", 12F);
            lbHeadFacrec.Location = new Point(0, 0);
            lbHeadFacrec.Name = "lbHeadFacrec";
            lbHeadFacrec.Size = new Size(560, 42);
            lbHeadFacrec.TabIndex = 0;
            lbHeadFacrec.Text = "Facturas del proveedor seleccionado, en el año seleccionado";
            lbHeadFacrec.TextAlign = ContentAlignment.MiddleCenter;
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
            statusStrip1.Items.AddRange(new ToolStripItem[] { tsLbNumReg, tsLbStatus, tsLbTotalBase, tsLbTotalCuota, tsLbTotalTotal });
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
            // tsLbTotalBase
            // 
            tsLbTotalBase.Margin = new Padding(10, 3, 0, 2);
            tsLbTotalBase.Name = "tsLbTotalBase";
            tsLbTotalBase.Size = new Size(63, 17);
            tsLbTotalBase.Text = "Total Base:";
            // 
            // tsLbTotalCuota
            // 
            tsLbTotalCuota.Margin = new Padding(10, 3, 0, 2);
            tsLbTotalCuota.Name = "tsLbTotalCuota";
            tsLbTotalCuota.Size = new Size(71, 17);
            tsLbTotalCuota.Text = "Total Cuota:";
            // 
            // tsLbTotalTotal
            // 
            tsLbTotalTotal.Margin = new Padding(10, 3, 0, 2);
            tsLbTotalTotal.Name = "tsLbTotalTotal";
            tsLbTotalTotal.Size = new Size(92, 17);
            tsLbTotalTotal.Text = "Total de Totales:";
            // 
            // FrmBrowFacrec
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(846, 450);
            Controls.Add(splitContainerFacrec);
            Name = "FrmBrowFacrec";
            Text = "Facturas Emitidas";
            FormClosing += FrmBrowFacrec_FormClosing;
            Load += FrmBrowFacrec_Load;
            Shown += FrmBrowFacrec_Shown;
            splitContainerFacrec.Panel1.ResumeLayout(false);
            splitContainerFacrec.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerFacrec).EndInit();
            splitContainerFacrec.ResumeLayout(false);
            pnProveedores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgProveedores).EndInit();
            pnHeadProveedores.ResumeLayout(false);
            pnGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgFacturas).EndInit();
            pnHeadFacrec.ResumeLayout(false);
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

        private SplitContainer splitContainerFacrec;
        private Panel pnHeadProveedores;
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
        private Panel pnProveedores;
        private DataGridView dgProveedores;
        private ToolStripSeparator toolStripSeparator4;
        private Panel pnHeadFacrec;
        private Label lbHeadFacrec;
        private ToolStripLabel toolStripLabel1;
        private ToolStripComboBox tsComboYear;
        private ToolStripStatusLabel tsLbTotalBase;
        private ToolStripStatusLabel tsLbTotalCuota;
        private ToolStripStatusLabel tsLbTotalTotal;
    }
}