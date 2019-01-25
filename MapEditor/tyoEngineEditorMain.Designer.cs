namespace tyoEngineEditor
{
    partial class tyoEngineEditorMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tyoEngineEditorMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tileMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemTime = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dealingToolLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.dealingToolProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.timerAnimation = new System.Windows.Forms.Timer(this.components);
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.cbPlay = new System.Windows.Forms.CheckBox();
            this.btPicDel = new System.Windows.Forms.Button();
            this.btPicAdd = new System.Windows.Forms.Button();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.txtAnimation = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.btLoadAnimation = new System.Windows.Forms.Button();
            this.btSaveAnimation = new System.Windows.Forms.Button();
            this.btNewAnimation = new System.Windows.Forms.Button();
            this.dgvAnimation = new System.Windows.Forms.DataGridView();
            this.NameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DirectionCol = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.OpCol = new System.Windows.Forms.DataGridViewButtonColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.panelAnimation = new tyoEngineEditor.tyoEngineTileMapEditWindow.MapEditPanel();
            this.listBoxAnimation = new System.Windows.Forms.ListBox();
            this.tabSystemMenuControl = new System.Windows.Forms.TabControl();
            this.TileMapEditor = new System.Windows.Forms.TabPage();
            this.btShowMap = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.listBoxMapEditor_TileList = new System.Windows.Forms.ListBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.propertyGridMapEditor_TitleInfos = new System.Windows.Forms.PropertyGrid();
            this.btMAP_RefurbishMapTitles = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.pictureBoxMapEditor_TitlePic = new System.Windows.Forms.PictureBox();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.listBoxMapEditor_MapLayer = new System.Windows.Forms.ListBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.propertyGridMapEditor_MapLayer = new System.Windows.Forms.PropertyGrid();
            this.btMAP_DelLayer = new System.Windows.Forms.Button();
            this.btMAP_AddLayer = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.propertyGridMapEditor_MapInfos = new System.Windows.Forms.PropertyGrid();
            this.btMAP_RenewMap = new System.Windows.Forms.Button();
            this.btMAP_Edit = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.groupBox16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnimation)).BeginInit();
            this.groupBox15.SuspendLayout();
            this.tabSystemMenuControl.SuspendLayout();
            this.TileMapEditor.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMapEditor_TitlePic)).BeginInit();
            this.tabPage9.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tileMapToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tileMapToolStripMenuItem
            // 
            this.tileMapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMapToolStripMenuItem,
            this.loadMapToolStripMenuItem});
            this.tileMapToolStripMenuItem.Name = "tileMapToolStripMenuItem";
            this.tileMapToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.tileMapToolStripMenuItem.Text = "Tile Map";
            // 
            // newMapToolStripMenuItem
            // 
            this.newMapToolStripMenuItem.Name = "newMapToolStripMenuItem";
            this.newMapToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newMapToolStripMenuItem.Text = "New Map";
            this.newMapToolStripMenuItem.Click += new System.EventHandler(this.newMapToolStripMenuItem_Click);
            // 
            // loadMapToolStripMenuItem
            // 
            this.loadMapToolStripMenuItem.Name = "loadMapToolStripMenuItem";
            this.loadMapToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadMapToolStripMenuItem.Text = "Load Map";
            this.loadMapToolStripMenuItem.Click += new System.EventHandler(this.loadMapToolStripMenuItem_Click);
            // 
            // systemTime
            // 
            this.systemTime.Tick += new System.EventHandler(this.systemTime_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dealingToolLabel,
            this.dealingToolProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 499);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // dealingToolLabel
            // 
            this.dealingToolLabel.Name = "dealingToolLabel";
            this.dealingToolLabel.Size = new System.Drawing.Size(46, 17);
            this.dealingToolLabel.Text = "处理中";
            this.dealingToolLabel.Visible = false;
            // 
            // dealingToolProgressBar
            // 
            this.dealingToolProgressBar.Maximum = 11;
            this.dealingToolProgressBar.Name = "dealingToolProgressBar";
            this.dealingToolProgressBar.Size = new System.Drawing.Size(100, 16);
            this.dealingToolProgressBar.Step = 1;
            this.dealingToolProgressBar.Visible = false;
            // 
            // timerAnimation
            // 
            this.timerAnimation.Tick += new System.EventHandler(this.timerAnimation_Tick);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.cbPlay);
            this.tabPage6.Controls.Add(this.btPicDel);
            this.tabPage6.Controls.Add(this.btPicAdd);
            this.tabPage6.Controls.Add(this.groupBox16);
            this.tabPage6.Controls.Add(this.dgvAnimation);
            this.tabPage6.Controls.Add(this.groupBox15);
            this.tabPage6.Controls.Add(this.listBoxAnimation);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(757, 455);
            this.tabPage6.TabIndex = 4;
            this.tabPage6.Text = "Animation Edit<动态素材编辑>";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // cbPlay
            // 
            this.cbPlay.AutoSize = true;
            this.cbPlay.Location = new System.Drawing.Point(511, 66);
            this.cbPlay.Name = "cbPlay";
            this.cbPlay.Size = new System.Drawing.Size(72, 16);
            this.cbPlay.TabIndex = 7;
            this.cbPlay.Text = "播放动画";
            this.cbPlay.UseVisualStyleBackColor = true;
            this.cbPlay.CheckedChanged += new System.EventHandler(this.cbPlay_CheckedChanged);
            // 
            // btPicDel
            // 
            this.btPicDel.Enabled = false;
            this.btPicDel.Location = new System.Drawing.Point(87, 66);
            this.btPicDel.Name = "btPicDel";
            this.btPicDel.Size = new System.Drawing.Size(75, 23);
            this.btPicDel.TabIndex = 6;
            this.btPicDel.Text = "删除动态图";
            this.btPicDel.UseVisualStyleBackColor = true;
            this.btPicDel.Click += new System.EventHandler(this.btPicDel_Click);
            // 
            // btPicAdd
            // 
            this.btPicAdd.Enabled = false;
            this.btPicAdd.Location = new System.Drawing.Point(6, 66);
            this.btPicAdd.Name = "btPicAdd";
            this.btPicAdd.Size = new System.Drawing.Size(75, 23);
            this.btPicAdd.TabIndex = 5;
            this.btPicAdd.Text = "增加动态图";
            this.btPicAdd.UseVisualStyleBackColor = true;
            this.btPicAdd.Click += new System.EventHandler(this.btPicAdd_Click);
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.txtAnimation);
            this.groupBox16.Controls.Add(this.label31);
            this.groupBox16.Controls.Add(this.btLoadAnimation);
            this.groupBox16.Controls.Add(this.btSaveAnimation);
            this.groupBox16.Controls.Add(this.btNewAnimation);
            this.groupBox16.Location = new System.Drawing.Point(6, 6);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(745, 54);
            this.groupBox16.TabIndex = 4;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "操作栏";
            // 
            // txtAnimation
            // 
            this.txtAnimation.Location = new System.Drawing.Point(68, 20);
            this.txtAnimation.Name = "txtAnimation";
            this.txtAnimation.Size = new System.Drawing.Size(431, 21);
            this.txtAnimation.TabIndex = 4;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(6, 25);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(65, 12);
            this.label31.TabIndex = 3;
            this.label31.Text = "动画名称：";
            // 
            // btLoadAnimation
            // 
            this.btLoadAnimation.Location = new System.Drawing.Point(586, 20);
            this.btLoadAnimation.Name = "btLoadAnimation";
            this.btLoadAnimation.Size = new System.Drawing.Size(75, 23);
            this.btLoadAnimation.TabIndex = 2;
            this.btLoadAnimation.Text = "加载动画";
            this.btLoadAnimation.UseVisualStyleBackColor = true;
            this.btLoadAnimation.Click += new System.EventHandler(this.btLoadAnimation_Click);
            // 
            // btSaveAnimation
            // 
            this.btSaveAnimation.Location = new System.Drawing.Point(667, 20);
            this.btSaveAnimation.Name = "btSaveAnimation";
            this.btSaveAnimation.Size = new System.Drawing.Size(75, 23);
            this.btSaveAnimation.TabIndex = 1;
            this.btSaveAnimation.Text = "保存动画";
            this.btSaveAnimation.UseVisualStyleBackColor = true;
            this.btSaveAnimation.Click += new System.EventHandler(this.btSaveAnimation_Click);
            // 
            // btNewAnimation
            // 
            this.btNewAnimation.Location = new System.Drawing.Point(505, 20);
            this.btNewAnimation.Name = "btNewAnimation";
            this.btNewAnimation.Size = new System.Drawing.Size(75, 23);
            this.btNewAnimation.TabIndex = 0;
            this.btNewAnimation.Text = "新建动画";
            this.btNewAnimation.UseVisualStyleBackColor = true;
            this.btNewAnimation.Click += new System.EventHandler(this.btNewAnimation_Click);
            // 
            // dgvAnimation
            // 
            this.dgvAnimation.AllowUserToAddRows = false;
            this.dgvAnimation.AllowUserToDeleteRows = false;
            this.dgvAnimation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAnimation.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvAnimation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAnimation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameCol,
            this.DirectionCol,
            this.OpCol,
            this.id});
            this.dgvAnimation.Location = new System.Drawing.Point(6, 90);
            this.dgvAnimation.Name = "dgvAnimation";
            this.dgvAnimation.RowTemplate.Height = 23;
            this.dgvAnimation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAnimation.Size = new System.Drawing.Size(319, 352);
            this.dgvAnimation.TabIndex = 1;
            this.dgvAnimation.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAnimation_CellClick);
            this.dgvAnimation.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAnimation_CellValueChanged);
            // 
            // NameCol
            // 
            this.NameCol.FillWeight = 121.8274F;
            this.NameCol.HeaderText = "动态图名称";
            this.NameCol.Name = "NameCol";
            // 
            // DirectionCol
            // 
            this.DirectionCol.HeaderText = "图片方向";
            this.DirectionCol.Items.AddRange(new object[] {
            "0-无",
            "1-上",
            "2-右上",
            "3-右",
            "4-右下",
            "5-下",
            "6-左下",
            "7-左",
            "8-左上"});
            this.DirectionCol.Name = "DirectionCol";
            // 
            // OpCol
            // 
            this.OpCol.FillWeight = 78.17259F;
            this.OpCol.HeaderText = "上传素材";
            this.OpCol.Name = "OpCol";
            this.OpCol.Text = "上传素材";
            this.OpCol.UseColumnTextForButtonValue = true;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.panelAnimation);
            this.groupBox15.Location = new System.Drawing.Point(503, 90);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(248, 352);
            this.groupBox15.TabIndex = 3;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "图片预览";
            // 
            // panelAnimation
            // 
            this.panelAnimation.Location = new System.Drawing.Point(6, 20);
            this.panelAnimation.Name = "panelAnimation";
            this.panelAnimation.Size = new System.Drawing.Size(236, 326);
            this.panelAnimation.TabIndex = 0;
            this.panelAnimation.Paint += new System.Windows.Forms.PaintEventHandler(this.panelAnimation_Paint);
            // 
            // listBoxAnimation
            // 
            this.listBoxAnimation.FormattingEnabled = true;
            this.listBoxAnimation.ItemHeight = 12;
            this.listBoxAnimation.Location = new System.Drawing.Point(331, 90);
            this.listBoxAnimation.Name = "listBoxAnimation";
            this.listBoxAnimation.Size = new System.Drawing.Size(166, 352);
            this.listBoxAnimation.TabIndex = 2;
            this.listBoxAnimation.SelectedIndexChanged += new System.EventHandler(this.listBoxAnimation_SelectedIndexChanged);
            // 
            // tabSystemMenuControl
            // 
            this.tabSystemMenuControl.Controls.Add(this.TileMapEditor);
            this.tabSystemMenuControl.Controls.Add(this.tabPage6);
            this.tabSystemMenuControl.Location = new System.Drawing.Point(10, 29);
            this.tabSystemMenuControl.Name = "tabSystemMenuControl";
            this.tabSystemMenuControl.SelectedIndex = 0;
            this.tabSystemMenuControl.Size = new System.Drawing.Size(765, 481);
            this.tabSystemMenuControl.TabIndex = 1;
            // 
            // TileMapEditor
            // 
            this.TileMapEditor.Controls.Add(this.btShowMap);
            this.TileMapEditor.Controls.Add(this.tabControl1);
            this.TileMapEditor.Controls.Add(this.groupBox7);
            this.TileMapEditor.Controls.Add(this.btMAP_RenewMap);
            this.TileMapEditor.Controls.Add(this.btMAP_Edit);
            this.TileMapEditor.Location = new System.Drawing.Point(4, 22);
            this.TileMapEditor.Name = "TileMapEditor";
            this.TileMapEditor.Padding = new System.Windows.Forms.Padding(3);
            this.TileMapEditor.Size = new System.Drawing.Size(757, 455);
            this.TileMapEditor.TabIndex = 3;
            this.TileMapEditor.Text = "Tile Map Editor <地图编辑器>";
            this.TileMapEditor.UseVisualStyleBackColor = true;
            // 
            // btShowMap
            // 
            this.btShowMap.Enabled = false;
            this.btShowMap.Location = new System.Drawing.Point(274, 3);
            this.btShowMap.Name = "btShowMap";
            this.btShowMap.Size = new System.Drawing.Size(75, 23);
            this.btShowMap.TabIndex = 4;
            this.btShowMap.Text = "ShowMap";
            this.btShowMap.UseVisualStyleBackColor = true;
            this.btShowMap.Click += new System.EventHandler(this.btShowMap_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage9);
            this.tabControl1.Location = new System.Drawing.Point(274, 49);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(477, 401);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox10);
            this.tabPage2.Controls.Add(this.groupBox9);
            this.tabPage2.Controls.Add(this.btMAP_RefurbishMapTitles);
            this.tabPage2.Controls.Add(this.groupBox8);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(469, 375);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "地图图块编辑";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.listBoxMapEditor_TileList);
            this.groupBox10.Location = new System.Drawing.Point(6, 223);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(239, 147);
            this.groupBox10.TabIndex = 5;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "图块列表";
            // 
            // listBoxMapEditor_TileList
            // 
            this.listBoxMapEditor_TileList.FormattingEnabled = true;
            this.listBoxMapEditor_TileList.ItemHeight = 12;
            this.listBoxMapEditor_TileList.Location = new System.Drawing.Point(6, 20);
            this.listBoxMapEditor_TileList.Name = "listBoxMapEditor_TileList";
            this.listBoxMapEditor_TileList.ScrollAlwaysVisible = true;
            this.listBoxMapEditor_TileList.Size = new System.Drawing.Size(227, 124);
            this.listBoxMapEditor_TileList.TabIndex = 3;
            this.listBoxMapEditor_TileList.SelectedIndexChanged += new System.EventHandler(this.listBoxMapEditor_TileList_SelectedIndexChanged);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.propertyGridMapEditor_TitleInfos);
            this.groupBox9.Location = new System.Drawing.Point(6, 35);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(239, 182);
            this.groupBox9.TabIndex = 5;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "图块信息";
            // 
            // propertyGridMapEditor_TitleInfos
            // 
            this.propertyGridMapEditor_TitleInfos.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGridMapEditor_TitleInfos.Location = new System.Drawing.Point(6, 17);
            this.propertyGridMapEditor_TitleInfos.Name = "propertyGridMapEditor_TitleInfos";
            this.propertyGridMapEditor_TitleInfos.Size = new System.Drawing.Size(227, 159);
            this.propertyGridMapEditor_TitleInfos.TabIndex = 4;
            this.propertyGridMapEditor_TitleInfos.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGridMapEditor_TileInfos_PropertyValueChanged);
            // 
            // btMAP_RefurbishMapTitles
            // 
            this.btMAP_RefurbishMapTitles.Location = new System.Drawing.Point(6, 6);
            this.btMAP_RefurbishMapTitles.Name = "btMAP_RefurbishMapTitles";
            this.btMAP_RefurbishMapTitles.Size = new System.Drawing.Size(75, 23);
            this.btMAP_RefurbishMapTitles.TabIndex = 2;
            this.btMAP_RefurbishMapTitles.Text = "刷新图块";
            this.btMAP_RefurbishMapTitles.UseVisualStyleBackColor = true;
            this.btMAP_RefurbishMapTitles.Click += new System.EventHandler(this.btMAP_RefurbishMapTiles_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.pictureBoxMapEditor_TitlePic);
            this.groupBox8.Location = new System.Drawing.Point(251, 6);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(212, 364);
            this.groupBox8.TabIndex = 1;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "图块预览";
            // 
            // pictureBoxMapEditor_TitlePic
            // 
            this.pictureBoxMapEditor_TitlePic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxMapEditor_TitlePic.Location = new System.Drawing.Point(6, 18);
            this.pictureBoxMapEditor_TitlePic.Name = "pictureBoxMapEditor_TitlePic";
            this.pictureBoxMapEditor_TitlePic.Size = new System.Drawing.Size(200, 340);
            this.pictureBoxMapEditor_TitlePic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMapEditor_TitlePic.TabIndex = 0;
            this.pictureBoxMapEditor_TitlePic.TabStop = false;
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.groupBox12);
            this.tabPage9.Controls.Add(this.groupBox11);
            this.tabPage9.Controls.Add(this.btMAP_DelLayer);
            this.tabPage9.Controls.Add(this.btMAP_AddLayer);
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage9.Size = new System.Drawing.Size(469, 375);
            this.tabPage9.TabIndex = 1;
            this.tabPage9.Text = "地图层编辑";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.listBoxMapEditor_MapLayer);
            this.groupBox12.Location = new System.Drawing.Point(256, 7);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(210, 363);
            this.groupBox12.TabIndex = 5;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "图层列表";
            // 
            // listBoxMapEditor_MapLayer
            // 
            this.listBoxMapEditor_MapLayer.FormattingEnabled = true;
            this.listBoxMapEditor_MapLayer.ItemHeight = 12;
            this.listBoxMapEditor_MapLayer.Location = new System.Drawing.Point(6, 20);
            this.listBoxMapEditor_MapLayer.Name = "listBoxMapEditor_MapLayer";
            this.listBoxMapEditor_MapLayer.ScrollAlwaysVisible = true;
            this.listBoxMapEditor_MapLayer.Size = new System.Drawing.Size(198, 340);
            this.listBoxMapEditor_MapLayer.TabIndex = 3;
            this.listBoxMapEditor_MapLayer.SelectedIndexChanged += new System.EventHandler(this.listBoxMapLayer_SelectedIndexChanged);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.propertyGridMapEditor_MapLayer);
            this.groupBox11.Location = new System.Drawing.Point(7, 36);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(243, 334);
            this.groupBox11.TabIndex = 4;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "地图层信息";
            // 
            // propertyGridMapEditor_MapLayer
            // 
            this.propertyGridMapEditor_MapLayer.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGridMapEditor_MapLayer.Location = new System.Drawing.Point(6, 19);
            this.propertyGridMapEditor_MapLayer.Name = "propertyGridMapEditor_MapLayer";
            this.propertyGridMapEditor_MapLayer.Size = new System.Drawing.Size(231, 309);
            this.propertyGridMapEditor_MapLayer.TabIndex = 2;
            this.propertyGridMapEditor_MapLayer.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGridMapLayer_PropertyValueChanged);
            // 
            // btMAP_DelLayer
            // 
            this.btMAP_DelLayer.Location = new System.Drawing.Point(88, 7);
            this.btMAP_DelLayer.Name = "btMAP_DelLayer";
            this.btMAP_DelLayer.Size = new System.Drawing.Size(75, 23);
            this.btMAP_DelLayer.TabIndex = 1;
            this.btMAP_DelLayer.Text = "删除图层";
            this.btMAP_DelLayer.UseVisualStyleBackColor = true;
            this.btMAP_DelLayer.Click += new System.EventHandler(this.btMAP_DelLayer_Click);
            // 
            // btMAP_AddLayer
            // 
            this.btMAP_AddLayer.Location = new System.Drawing.Point(7, 7);
            this.btMAP_AddLayer.Name = "btMAP_AddLayer";
            this.btMAP_AddLayer.Size = new System.Drawing.Size(75, 23);
            this.btMAP_AddLayer.TabIndex = 0;
            this.btMAP_AddLayer.Text = "增加图层";
            this.btMAP_AddLayer.UseVisualStyleBackColor = true;
            this.btMAP_AddLayer.Click += new System.EventHandler(this.btMAP_AddLayer_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.propertyGridMapEditor_MapInfos);
            this.groupBox7.Location = new System.Drawing.Point(7, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(261, 444);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "地图信息";
            // 
            // propertyGridMapEditor_MapInfos
            // 
            this.propertyGridMapEditor_MapInfos.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGridMapEditor_MapInfos.Location = new System.Drawing.Point(6, 20);
            this.propertyGridMapEditor_MapInfos.Name = "propertyGridMapEditor_MapInfos";
            this.propertyGridMapEditor_MapInfos.Size = new System.Drawing.Size(249, 418);
            this.propertyGridMapEditor_MapInfos.TabIndex = 1;
            // 
            // btMAP_RenewMap
            // 
            this.btMAP_RenewMap.Enabled = false;
            this.btMAP_RenewMap.Location = new System.Drawing.Point(595, 6);
            this.btMAP_RenewMap.Name = "btMAP_RenewMap";
            this.btMAP_RenewMap.Size = new System.Drawing.Size(75, 23);
            this.btMAP_RenewMap.TabIndex = 0;
            this.btMAP_RenewMap.Text = "Renew Map";
            this.btMAP_RenewMap.UseVisualStyleBackColor = true;
            this.btMAP_RenewMap.Click += new System.EventHandler(this.btMAP_RenewMap_Click);
            // 
            // btMAP_Edit
            // 
            this.btMAP_Edit.Enabled = false;
            this.btMAP_Edit.Location = new System.Drawing.Point(676, 6);
            this.btMAP_Edit.Name = "btMAP_Edit";
            this.btMAP_Edit.Size = new System.Drawing.Size(75, 23);
            this.btMAP_Edit.TabIndex = 0;
            this.btMAP_Edit.Text = "编辑地图";
            this.btMAP_Edit.UseVisualStyleBackColor = true;
            this.btMAP_Edit.Click += new System.EventHandler(this.btMAP_Edit_Click);
            // 
            // tyoEngineEditorMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 521);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabSystemMenuControl);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "tyoEngineEditorMain";
            this.Text = "tyo Engine Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.tyoEngineEditorMain_FormClosed);
            this.Load += new System.EventHandler(this.tyoEngineEditorMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnimation)).EndInit();
            this.groupBox15.ResumeLayout(false);
            this.tabSystemMenuControl.ResumeLayout(false);
            this.TileMapEditor.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMapEditor_TitlePic)).EndInit();
            this.tabPage9.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Timer systemTime;
        private System.Windows.Forms.ToolStripMenuItem tileMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMapToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel dealingToolLabel;
        private System.Windows.Forms.ToolStripProgressBar dealingToolProgressBar;
        private System.Windows.Forms.Timer timerAnimation;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.CheckBox cbPlay;
        private System.Windows.Forms.Button btPicDel;
        private System.Windows.Forms.Button btPicAdd;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.TextBox txtAnimation;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Button btLoadAnimation;
        private System.Windows.Forms.Button btSaveAnimation;
        private System.Windows.Forms.Button btNewAnimation;
        private System.Windows.Forms.DataGridView dgvAnimation;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameCol;
        private System.Windows.Forms.DataGridViewComboBoxColumn DirectionCol;
        private System.Windows.Forms.DataGridViewButtonColumn OpCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.GroupBox groupBox15;
        private tyoEngineTileMapEditWindow.MapEditPanel panelAnimation;
        private System.Windows.Forms.ListBox listBoxAnimation;
        private System.Windows.Forms.TabControl tabSystemMenuControl;
        private System.Windows.Forms.TabPage TileMapEditor;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.ListBox listBoxMapEditor_TileList;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.PropertyGrid propertyGridMapEditor_TitleInfos;
        private System.Windows.Forms.Button btMAP_RefurbishMapTitles;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.PictureBox pictureBoxMapEditor_TitlePic;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.ListBox listBoxMapEditor_MapLayer;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.PropertyGrid propertyGridMapEditor_MapLayer;
        private System.Windows.Forms.Button btMAP_DelLayer;
        private System.Windows.Forms.Button btMAP_AddLayer;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.PropertyGrid propertyGridMapEditor_MapInfos;
        private System.Windows.Forms.Button btMAP_RenewMap;
        private System.Windows.Forms.Button btMAP_Edit;
        private System.Windows.Forms.Button btShowMap;
    }
}

