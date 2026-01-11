namespace FacturacionDAM.Formularios
{
    partial class FrmFacemi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFacemi));
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            pnBtns = new Panel();
            btnCancelar = new Button();
            btnAceptar = new Button();
            pnData = new Panel();
            tabControl1 = new TabControl();
            tabData = new TabPage();
            PnFacemilin = new Panel();
            pnGrid = new Panel();
            dgLineasFactura = new DataGridView();
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
            PnFacemi = new Panel();
            gbTotales = new GroupBox();
            lbRetencion = new Label();
            label13 = new Label();
            lbTotal = new Label();
            label12 = new Label();
            lbCuota = new Label();
            label11 = new Label();
            lbBase = new Label();
            label17 = new Label();
            gbFacemi = new GroupBox();
            numTipoRet = new NumericUpDown();
            label3 = new Label();
            chkRetencion = new CheckBox();
            chkPagada = new CheckBox();
            fechaFactura = new DateTimePicker();
            cbConceptFac = new ComboBox();
            label10 = new Label();
            txtDescripcion = new TextBox();
            label9 = new Label();
            label8 = new Label();
            txtNumero = new TextBox();
            label7 = new Label();
            gbEmisorCliente = new GroupBox();
            lbNombreCliente = new Label();
            lbNifcifCliente = new Label();
            label5 = new Label();
            label6 = new Label();
            lbNombreEmisor = new Label();
            lbNifcifEmisor = new Label();
            label2 = new Label();
            label1 = new Label();
            tabNotas = new TabPage();
            txtNotas = new RichTextBox();
            pnBtns.SuspendLayout();
            pnData.SuspendLayout();
            tabControl1.SuspendLayout();
            tabData.SuspendLayout();
            PnFacemilin.SuspendLayout();
            pnGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgLineasFactura).BeginInit();
            pnStatus.SuspendLayout();
            statusStrip1.SuspendLayout();
            pnMenu.SuspendLayout();
            tsHerramientas.SuspendLayout();
            PnFacemi.SuspendLayout();
            gbTotales.SuspendLayout();
            gbFacemi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numTipoRet).BeginInit();
            gbEmisorCliente.SuspendLayout();
            tabNotas.SuspendLayout();
            SuspendLayout();
            // 
            // pnBtns
            // 
            pnBtns.Controls.Add(btnCancelar);
            pnBtns.Controls.Add(btnAceptar);
            pnBtns.Dock = DockStyle.Bottom;
            pnBtns.Location = new Point(0, 919);
            pnBtns.Margin = new Padding(3, 4, 3, 4);
            pnBtns.Name = "pnBtns";
            pnBtns.Size = new Size(1001, 84);
            pnBtns.TabIndex = 1;
            // 
            // btnCancelar
            // 
            btnCancelar.Image = (Image)resources.GetObject("btnCancelar.Image");
            btnCancelar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelar.Location = new Point(512, 19);
            btnCancelar.Margin = new Padding(3, 4, 3, 4);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Padding = new Padding(11, 0, 11, 0);
            btnCancelar.Size = new Size(114, 48);
            btnCancelar.TabIndex = 1;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextAlign = ContentAlignment.MiddleRight;
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnAceptar
            // 
            btnAceptar.Image = (Image)resources.GetObject("btnAceptar.Image");
            btnAceptar.ImageAlign = ContentAlignment.MiddleLeft;
            btnAceptar.Location = new Point(280, 19);
            btnAceptar.Margin = new Padding(3, 4, 3, 4);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Padding = new Padding(11, 0, 11, 0);
            btnAceptar.Size = new Size(114, 48);
            btnAceptar.TabIndex = 0;
            btnAceptar.Text = "Aceptar";
            btnAceptar.TextAlign = ContentAlignment.MiddleRight;
            btnAceptar.UseVisualStyleBackColor = true;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // pnData
            // 
            pnData.Controls.Add(tabControl1);
            pnData.Dock = DockStyle.Fill;
            pnData.Location = new Point(0, 0);
            pnData.Margin = new Padding(3, 4, 3, 4);
            pnData.Name = "pnData";
            pnData.Size = new Size(1001, 919);
            pnData.TabIndex = 2;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabData);
            tabControl1.Controls.Add(tabNotas);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(3, 4, 3, 4);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1001, 919);
            tabControl1.TabIndex = 0;
            // 
            // tabData
            // 
            tabData.Controls.Add(PnFacemilin);
            tabData.Controls.Add(PnFacemi);
            tabData.Location = new Point(4, 29);
            tabData.Margin = new Padding(3, 4, 3, 4);
            tabData.Name = "tabData";
            tabData.Padding = new Padding(3, 4, 3, 4);
            tabData.Size = new Size(993, 886);
            tabData.TabIndex = 0;
            tabData.Text = "Datos";
            tabData.UseVisualStyleBackColor = true;
            // 
            // PnFacemilin
            // 
            PnFacemilin.BorderStyle = BorderStyle.Fixed3D;
            PnFacemilin.Controls.Add(pnGrid);
            PnFacemilin.Controls.Add(pnStatus);
            PnFacemilin.Controls.Add(pnMenu);
            PnFacemilin.Dock = DockStyle.Fill;
            PnFacemilin.Location = new Point(3, 452);
            PnFacemilin.Margin = new Padding(3, 4, 3, 4);
            PnFacemilin.Name = "PnFacemilin";
            PnFacemilin.Size = new Size(987, 430);
            PnFacemilin.TabIndex = 3;
            // 
            // pnGrid
            // 
            pnGrid.Controls.Add(dgLineasFactura);
            pnGrid.Dock = DockStyle.Fill;
            pnGrid.Location = new Point(0, 32);
            pnGrid.Margin = new Padding(3, 4, 3, 4);
            pnGrid.Name = "pnGrid";
            pnGrid.Padding = new Padding(0, 3, 0, 0);
            pnGrid.Size = new Size(983, 365);
            pnGrid.TabIndex = 3;
            // 
            // dgLineasFactura
            // 
            dgLineasFactura.AllowUserToAddRows = false;
            dgLineasFactura.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.LightGray;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgLineasFactura.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgLineasFactura.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgLineasFactura.Dock = DockStyle.Fill;
            dgLineasFactura.Location = new Point(0, 3);
            dgLineasFactura.Margin = new Padding(3, 4, 3, 4);
            dgLineasFactura.Name = "dgLineasFactura";
            dgLineasFactura.ReadOnly = true;
            dgLineasFactura.RowHeadersWidth = 51;
            dgLineasFactura.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgLineasFactura.Size = new Size(983, 362);
            dgLineasFactura.TabIndex = 0;
            // 
            // pnStatus
            // 
            pnStatus.Controls.Add(statusStrip1);
            pnStatus.Dock = DockStyle.Bottom;
            pnStatus.Location = new Point(0, 397);
            pnStatus.Margin = new Padding(3, 4, 3, 4);
            pnStatus.Name = "pnStatus";
            pnStatus.Size = new Size(983, 29);
            pnStatus.TabIndex = 2;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { tsLbNumReg, tsLbStatus });
            statusStrip1.Location = new Point(0, 3);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 16, 0);
            statusStrip1.Size = new Size(983, 26);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // tsLbNumReg
            // 
            tsLbNumReg.Name = "tsLbNumReg";
            tsLbNumReg.Size = new Size(111, 20);
            tsLbNumReg.Text = "Nº de registros:";
            // 
            // tsLbStatus
            // 
            tsLbStatus.Margin = new Padding(10, 3, 0, 2);
            tsLbStatus.Name = "tsLbStatus";
            tsLbStatus.Size = new Size(0, 21);
            // 
            // pnMenu
            // 
            pnMenu.Controls.Add(tsHerramientas);
            pnMenu.Dock = DockStyle.Top;
            pnMenu.Location = new Point(0, 0);
            pnMenu.Margin = new Padding(3, 4, 3, 4);
            pnMenu.Name = "pnMenu";
            pnMenu.Size = new Size(983, 32);
            pnMenu.TabIndex = 1;
            // 
            // tsHerramientas
            // 
            tsHerramientas.ImageScalingSize = new Size(20, 20);
            tsHerramientas.Items.AddRange(new ToolStripItem[] { tsBtnNew, tsBtnEdit, toolStripSeparator1, tsBtnDelete, toolStripSeparator2, tsBtnFirst, tsBtnPrev, tsBtnNext, tsBtnLast, toolStripSeparator3, tsBtnExportCSV, tsBtnExportXML });
            tsHerramientas.Location = new Point(0, 0);
            tsHerramientas.Name = "tsHerramientas";
            tsHerramientas.Size = new Size(983, 27);
            tsHerramientas.TabIndex = 0;
            tsHerramientas.Text = "toolStrip1";
            // 
            // tsBtnNew
            // 
            tsBtnNew.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnNew.Image = (Image)resources.GetObject("tsBtnNew.Image");
            tsBtnNew.ImageTransparentColor = Color.Magenta;
            tsBtnNew.Name = "tsBtnNew";
            tsBtnNew.Size = new Size(29, 24);
            tsBtnNew.Text = "Nuevo registro";
            tsBtnNew.Click += tsBtnNew_Click;
            // 
            // tsBtnEdit
            // 
            tsBtnEdit.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnEdit.Image = (Image)resources.GetObject("tsBtnEdit.Image");
            tsBtnEdit.ImageTransparentColor = Color.Magenta;
            tsBtnEdit.Name = "tsBtnEdit";
            tsBtnEdit.Size = new Size(29, 24);
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
            tsBtnDelete.Size = new Size(29, 24);
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
            tsBtnFirst.Size = new Size(29, 24);
            tsBtnFirst.Text = "Primer registro";
            // 
            // tsBtnPrev
            // 
            tsBtnPrev.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnPrev.Image = (Image)resources.GetObject("tsBtnPrev.Image");
            tsBtnPrev.ImageTransparentColor = Color.Magenta;
            tsBtnPrev.Name = "tsBtnPrev";
            tsBtnPrev.Size = new Size(29, 24);
            tsBtnPrev.Text = "Registro anterior";
            // 
            // tsBtnNext
            // 
            tsBtnNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnNext.Image = (Image)resources.GetObject("tsBtnNext.Image");
            tsBtnNext.ImageTransparentColor = Color.Magenta;
            tsBtnNext.Name = "tsBtnNext";
            tsBtnNext.Size = new Size(29, 24);
            tsBtnNext.Text = "Siguiente registro";
            // 
            // tsBtnLast
            // 
            tsBtnLast.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnLast.Image = (Image)resources.GetObject("tsBtnLast.Image");
            tsBtnLast.ImageTransparentColor = Color.Magenta;
            tsBtnLast.Name = "tsBtnLast";
            tsBtnLast.Size = new Size(29, 24);
            tsBtnLast.Text = "Último registro";
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
            tsBtnExportCSV.Size = new Size(29, 24);
            tsBtnExportCSV.Text = "toolStripButton1";
            tsBtnExportCSV.ToolTipText = "Exportar a formato CSV";
            // 
            // tsBtnExportXML
            // 
            tsBtnExportXML.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnExportXML.Image = (Image)resources.GetObject("tsBtnExportXML.Image");
            tsBtnExportXML.ImageTransparentColor = Color.Magenta;
            tsBtnExportXML.Name = "tsBtnExportXML";
            tsBtnExportXML.Size = new Size(29, 24);
            tsBtnExportXML.Text = "toolStripButton1";
            tsBtnExportXML.ToolTipText = "Exportar a formato XML";
            // 
            // PnFacemi
            // 
            PnFacemi.Controls.Add(gbTotales);
            PnFacemi.Controls.Add(gbFacemi);
            PnFacemi.Controls.Add(gbEmisorCliente);
            PnFacemi.Dock = DockStyle.Top;
            PnFacemi.Location = new Point(3, 4);
            PnFacemi.Margin = new Padding(3, 4, 3, 4);
            PnFacemi.Name = "PnFacemi";
            PnFacemi.Size = new Size(987, 448);
            PnFacemi.TabIndex = 2;
            // 
            // gbTotales
            // 
            gbTotales.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbTotales.BackColor = Color.Gainsboro;
            gbTotales.Controls.Add(lbRetencion);
            gbTotales.Controls.Add(label13);
            gbTotales.Controls.Add(lbTotal);
            gbTotales.Controls.Add(label12);
            gbTotales.Controls.Add(lbCuota);
            gbTotales.Controls.Add(label11);
            gbTotales.Controls.Add(lbBase);
            gbTotales.Controls.Add(label17);
            gbTotales.Location = new Point(11, 328);
            gbTotales.Margin = new Padding(3, 4, 3, 4);
            gbTotales.Name = "gbTotales";
            gbTotales.Padding = new Padding(3, 4, 3, 4);
            gbTotales.Size = new Size(959, 89);
            gbTotales.TabIndex = 4;
            gbTotales.TabStop = false;
            gbTotales.Text = "Totales";
            // 
            // lbRetencion
            // 
            lbRetencion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbRetencion.ForeColor = Color.Black;
            lbRetencion.Location = new Point(623, 27);
            lbRetencion.Name = "lbRetencion";
            lbRetencion.Size = new Size(99, 31);
            lbRetencion.TabIndex = 8;
            lbRetencion.Text = "10,00 €";
            lbRetencion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            label13.Location = new Point(534, 27);
            label13.Name = "label13";
            label13.Size = new Size(91, 31);
            label13.TabIndex = 7;
            label13.Text = "Retención:";
            label13.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lbTotal
            // 
            lbTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbTotal.ForeColor = Color.Black;
            lbTotal.Location = new Point(407, 27);
            lbTotal.Name = "lbTotal";
            lbTotal.Size = new Size(99, 31);
            lbTotal.TabIndex = 6;
            lbTotal.Text = "10,00 €";
            lbTotal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            label12.Location = new Point(340, 27);
            label12.Name = "label12";
            label12.Size = new Size(64, 31);
            label12.TabIndex = 5;
            label12.Text = "Total:";
            label12.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lbCuota
            // 
            lbCuota.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbCuota.ForeColor = Color.Black;
            lbCuota.Location = new Point(248, 27);
            lbCuota.Name = "lbCuota";
            lbCuota.Size = new Size(99, 31);
            lbCuota.TabIndex = 4;
            lbCuota.Text = "10,00 €";
            lbCuota.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            label11.Location = new Point(184, 27);
            label11.Name = "label11";
            label11.Size = new Size(64, 31);
            label11.TabIndex = 3;
            label11.Text = "Cuota:";
            label11.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lbBase
            // 
            lbBase.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbBase.ForeColor = Color.Black;
            lbBase.Location = new Point(95, 27);
            lbBase.Name = "lbBase";
            lbBase.Size = new Size(99, 31);
            lbBase.TabIndex = 2;
            lbBase.Text = "10,00 €";
            lbBase.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            label17.Location = new Point(29, 27);
            label17.Name = "label17";
            label17.Size = new Size(64, 31);
            label17.TabIndex = 0;
            label17.Text = "Base:";
            label17.TextAlign = ContentAlignment.MiddleRight;
            // 
            // gbFacemi
            // 
            gbFacemi.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbFacemi.Controls.Add(numTipoRet);
            gbFacemi.Controls.Add(label3);
            gbFacemi.Controls.Add(chkRetencion);
            gbFacemi.Controls.Add(chkPagada);
            gbFacemi.Controls.Add(fechaFactura);
            gbFacemi.Controls.Add(cbConceptFac);
            gbFacemi.Controls.Add(label10);
            gbFacemi.Controls.Add(txtDescripcion);
            gbFacemi.Controls.Add(label9);
            gbFacemi.Controls.Add(label8);
            gbFacemi.Controls.Add(txtNumero);
            gbFacemi.Controls.Add(label7);
            gbFacemi.Location = new Point(11, 139);
            gbFacemi.Margin = new Padding(3, 4, 3, 4);
            gbFacemi.Name = "gbFacemi";
            gbFacemi.Padding = new Padding(3, 4, 3, 4);
            gbFacemi.Size = new Size(959, 181);
            gbFacemi.TabIndex = 3;
            gbFacemi.TabStop = false;
            gbFacemi.Text = "Datos de la Factura";
            // 
            // numTipoRet
            // 
            numTipoRet.DecimalPlaces = 2;
            numTipoRet.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numTipoRet.Location = new Point(619, 131);
            numTipoRet.Maximum = new decimal(new int[] { 9999, 0, 0, 131072 });
            numTipoRet.Name = "numTipoRet";
            numTipoRet.Size = new Size(69, 27);
            numTipoRet.TabIndex = 10;
            numTipoRet.TextAlign = HorizontalAlignment.Center;
            numTipoRet.Value = new decimal(new int[] { 15, 0, 0, 0 });
            // 
            // label3
            // 
            label3.Location = new Point(495, 131);
            label3.Name = "label3";
            label3.Size = new Size(120, 31);
            label3.TabIndex = 10;
            label3.Text = "Tipo de Retención";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // chkRetencion
            // 
            chkRetencion.AutoSize = true;
            chkRetencion.Location = new Point(334, 136);
            chkRetencion.Margin = new Padding(3, 4, 3, 4);
            chkRetencion.Name = "chkRetencion";
            chkRetencion.Size = new Size(171, 24);
            chkRetencion.TabIndex = 9;
            chkRetencion.Text = "¿Se aplica retención?";
            chkRetencion.UseVisualStyleBackColor = true;
            // 
            // chkPagada
            // 
            chkPagada.AutoSize = true;
            chkPagada.Location = new Point(111, 136);
            chkPagada.Margin = new Padding(3, 4, 3, 4);
            chkPagada.Name = "chkPagada";
            chkPagada.Size = new Size(94, 24);
            chkPagada.TabIndex = 8;
            chkPagada.Text = "¿Pagada?";
            chkPagada.UseVisualStyleBackColor = true;
            // 
            // fechaFactura
            // 
            fechaFactura.Format = DateTimePickerFormat.Short;
            fechaFactura.Location = new Point(286, 36);
            fechaFactura.Margin = new Padding(3, 4, 3, 4);
            fechaFactura.Name = "fechaFactura";
            fechaFactura.Size = new Size(109, 27);
            fechaFactura.TabIndex = 3;
            // 
            // cbConceptFac
            // 
            cbConceptFac.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbConceptFac.FormattingEnabled = true;
            cbConceptFac.Location = new Point(518, 37);
            cbConceptFac.Margin = new Padding(3, 4, 3, 4);
            cbConceptFac.Name = "cbConceptFac";
            cbConceptFac.Size = new Size(421, 28);
            cbConceptFac.TabIndex = 5;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(440, 41);
            label10.Name = "label10";
            label10.Size = new Size(76, 20);
            label10.TabIndex = 4;
            label10.Text = "Concepto:";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDescripcion.Location = new Point(99, 81);
            txtDescripcion.Margin = new Padding(3, 4, 3, 4);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(839, 27);
            txtDescripcion.TabIndex = 7;
            // 
            // label9
            // 
            label9.Location = new Point(11, 81);
            label9.Name = "label9";
            label9.Size = new Size(80, 31);
            label9.TabIndex = 6;
            label9.Text = "Descripción";
            label9.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(232, 41);
            label8.Name = "label8";
            label8.Size = new Size(50, 20);
            label8.TabIndex = 2;
            label8.Text = "Fecha:";
            // 
            // txtNumero
            // 
            txtNumero.Location = new Point(99, 36);
            txtNumero.Margin = new Padding(3, 4, 3, 4);
            txtNumero.Name = "txtNumero";
            txtNumero.Size = new Size(100, 27);
            txtNumero.TabIndex = 1;
            // 
            // label7
            // 
            label7.Location = new Point(11, 36);
            label7.Name = "label7";
            label7.Size = new Size(80, 31);
            label7.TabIndex = 0;
            label7.Text = "Número";
            label7.TextAlign = ContentAlignment.MiddleRight;
            // 
            // gbEmisorCliente
            // 
            gbEmisorCliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbEmisorCliente.Controls.Add(lbNombreCliente);
            gbEmisorCliente.Controls.Add(lbNifcifCliente);
            gbEmisorCliente.Controls.Add(label5);
            gbEmisorCliente.Controls.Add(label6);
            gbEmisorCliente.Controls.Add(lbNombreEmisor);
            gbEmisorCliente.Controls.Add(lbNifcifEmisor);
            gbEmisorCliente.Controls.Add(label2);
            gbEmisorCliente.Controls.Add(label1);
            gbEmisorCliente.Location = new Point(11, 15);
            gbEmisorCliente.Margin = new Padding(3, 4, 3, 4);
            gbEmisorCliente.Name = "gbEmisorCliente";
            gbEmisorCliente.Padding = new Padding(3, 4, 3, 4);
            gbEmisorCliente.Size = new Size(959, 113);
            gbEmisorCliente.TabIndex = 1;
            gbEmisorCliente.TabStop = false;
            gbEmisorCliente.Text = "Emisor y Cliente";
            // 
            // lbNombreCliente
            // 
            lbNombreCliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbNombreCliente.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbNombreCliente.ForeColor = Color.DarkGoldenrod;
            lbNombreCliente.Location = new Point(457, 68);
            lbNombreCliente.Name = "lbNombreCliente";
            lbNombreCliente.Size = new Size(482, 31);
            lbNombreCliente.TabIndex = 7;
            lbNombreCliente.Text = "Nombre comercial del cliente";
            lbNombreCliente.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lbNifcifCliente
            // 
            lbNifcifCliente.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbNifcifCliente.ForeColor = Color.DarkGoldenrod;
            lbNifcifCliente.Location = new Point(117, 68);
            lbNifcifCliente.Name = "lbNifcifCliente";
            lbNifcifCliente.Size = new Size(143, 31);
            lbNifcifCliente.TabIndex = 6;
            lbNifcifCliente.Text = "Nif/Cif Cliente";
            lbNifcifCliente.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.Location = new Point(266, 68);
            label5.Name = "label5";
            label5.Size = new Size(184, 31);
            label5.TabIndex = 5;
            label5.Text = "Nombre Comercial Cliente:";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            label6.Location = new Point(11, 68);
            label6.Name = "label6";
            label6.Size = new Size(104, 31);
            label6.TabIndex = 4;
            label6.Text = "NIF/CIF Cliente:";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lbNombreEmisor
            // 
            lbNombreEmisor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbNombreEmisor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbNombreEmisor.ForeColor = Color.RoyalBlue;
            lbNombreEmisor.Location = new Point(457, 27);
            lbNombreEmisor.Name = "lbNombreEmisor";
            lbNombreEmisor.Size = new Size(482, 31);
            lbNombreEmisor.TabIndex = 3;
            lbNombreEmisor.Text = "Nombre comercial del emisor";
            lbNombreEmisor.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lbNifcifEmisor
            // 
            lbNifcifEmisor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbNifcifEmisor.ForeColor = Color.RoyalBlue;
            lbNifcifEmisor.Location = new Point(117, 27);
            lbNifcifEmisor.Name = "lbNifcifEmisor";
            lbNifcifEmisor.Size = new Size(143, 31);
            lbNifcifEmisor.TabIndex = 2;
            lbNifcifEmisor.Text = "Nif/Cif Emisor";
            lbNifcifEmisor.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.Location = new Point(266, 27);
            label2.Name = "label2";
            label2.Size = new Size(184, 31);
            label2.TabIndex = 1;
            label2.Text = "Nombre Comercial Emisor:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.Location = new Point(11, 27);
            label1.Name = "label1";
            label1.Size = new Size(104, 31);
            label1.TabIndex = 0;
            label1.Text = "NIF/CIF Emisor:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tabNotas
            // 
            tabNotas.Controls.Add(txtNotas);
            tabNotas.Location = new Point(4, 29);
            tabNotas.Margin = new Padding(3, 4, 3, 4);
            tabNotas.Name = "tabNotas";
            tabNotas.Padding = new Padding(3, 4, 3, 4);
            tabNotas.Size = new Size(993, 886);
            tabNotas.TabIndex = 1;
            tabNotas.Text = "Notas";
            tabNotas.UseVisualStyleBackColor = true;
            // 
            // txtNotas
            // 
            txtNotas.Dock = DockStyle.Fill;
            txtNotas.Location = new Point(3, 4);
            txtNotas.Margin = new Padding(3, 4, 3, 4);
            txtNotas.Name = "txtNotas";
            txtNotas.Size = new Size(987, 878);
            txtNotas.TabIndex = 0;
            txtNotas.Text = "";
            // 
            // FrmFacemi
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1001, 1003);
            Controls.Add(pnData);
            Controls.Add(pnBtns);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmFacemi";
            Text = "Factura Emitida";
            FormClosing += FrmFacemi_FormClosing;
            Load += FrmFacemi_Load;
            pnBtns.ResumeLayout(false);
            pnData.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabData.ResumeLayout(false);
            PnFacemilin.ResumeLayout(false);
            pnGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgLineasFactura).EndInit();
            pnStatus.ResumeLayout(false);
            pnStatus.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            pnMenu.ResumeLayout(false);
            pnMenu.PerformLayout();
            tsHerramientas.ResumeLayout(false);
            tsHerramientas.PerformLayout();
            PnFacemi.ResumeLayout(false);
            gbTotales.ResumeLayout(false);
            gbFacemi.ResumeLayout(false);
            gbFacemi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numTipoRet).EndInit();
            gbEmisorCliente.ResumeLayout(false);
            tabNotas.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnBtns;
        private Button btnCancelar;
        private Button btnAceptar;
        private Panel pnData;
        private TabControl tabControl1;
        private TabPage tabData;
        private TabPage tabNotas;
        private RichTextBox txtNotas;
        private Panel PnFacemi;
        private GroupBox gbFacemi;
        private ComboBox cbConceptFac;
        private Label label10;
        private TextBox txtDescripcion;
        private Label label9;
        private TextBox txtCp;
        private Label label8;
        private TextBox txtNumero;
        private Label label7;
        private GroupBox gbEmisorCliente;
        private Label lbNombreEmisor;
        private Label lbNifcifEmisor;
        private Label label2;
        private Label label1;
        private Panel PnFacemilin;
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
        private DateTimePicker fechaFactura;
        private Panel pnStatus;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tsLbNumReg;
        private ToolStripStatusLabel tsLbStatus;
        private Panel pnGrid;
        private DataGridView dgLineasFactura;
        private Label lbNombreCliente;
        private Label lbNifcifCliente;
        private Label label5;
        private Label label6;
        private CheckBox chkRetencion;
        private CheckBox chkPagada;
        private NumericUpDown numTipoRet;
        private Label label3;
        private GroupBox gbTotales;
        private Label label14;
        private Label lbBase;
        private Label label16;
        private Label label17;
        private Label lbCuota;
        private Label label11;
        private Label lbRetencion;
        private Label label13;
        private Label lbTotal;
        private Label label12;
    }
}