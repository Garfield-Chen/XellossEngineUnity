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
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.btAni_SortAnimationFrameList = new System.Windows.Forms.Button();
            this.aniTextBox_FPS = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.aniTextBox_Name = new System.Windows.Forms.TextBox();
            this.btAni_DelAniTexture = new System.Windows.Forms.Button();
            this.btAni_AddAniTexture = new System.Windows.Forms.Button();
            this.aniListBox_FrameList = new System.Windows.Forms.ListBox();
            this.aniPropertyGrid_Infos = new System.Windows.Forms.PropertyGrid();
            this.btAni_PlayAnimation = new System.Windows.Forms.Button();
            this.btAni_LoadAnimation = new System.Windows.Forms.Button();
            this.btAni_ClearAnimation = new System.Windows.Forms.Button();
            this.btAni_SaveAnimation = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.aniPictureBox_frameTexture = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.aniPictureBox_AniShow = new System.Windows.Forms.PictureBox();
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
            this.aniTimer_AnimationPlayDt = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.aniTextBox_Descirpt = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aniPictureBox_frameTexture)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aniPictureBox_AniShow)).BeginInit();
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
            this.newMapToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.newMapToolStripMenuItem.Text = "New Map";
            this.newMapToolStripMenuItem.Click += new System.EventHandler(this.newMapToolStripMenuItem_Click);
            // 
            // loadMapToolStripMenuItem
            // 
            this.loadMapToolStripMenuItem.Name = "loadMapToolStripMenuItem";
            this.loadMapToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
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
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.btAni_SortAnimationFrameList);
            this.tabPage6.Controls.Add(this.aniTextBox_FPS);
            this.tabPage6.Controls.Add(this.label2);
            this.tabPage6.Controls.Add(this.label3);
            this.tabPage6.Controls.Add(this.label1);
            this.tabPage6.Controls.Add(this.aniTextBox_Descirpt);
            this.tabPage6.Controls.Add(this.aniTextBox_Name);
            this.tabPage6.Controls.Add(this.btAni_DelAniTexture);
            this.tabPage6.Controls.Add(this.btAni_AddAniTexture);
            this.tabPage6.Controls.Add(this.aniListBox_FrameList);
            this.tabPage6.Controls.Add(this.aniPropertyGrid_Infos);
            this.tabPage6.Controls.Add(this.btAni_PlayAnimation);
            this.tabPage6.Controls.Add(this.btAni_LoadAnimation);
            this.tabPage6.Controls.Add(this.btAni_ClearAnimation);
            this.tabPage6.Controls.Add(this.btAni_SaveAnimation);
            this.tabPage6.Controls.Add(this.groupBox2);
            this.tabPage6.Controls.Add(this.groupBox1);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(757, 455);
            this.tabPage6.TabIndex = 4;
            this.tabPage6.Text = "Animation Edit<动态素材编辑>";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // btAni_SortAnimationFrameList
            // 
            this.btAni_SortAnimationFrameList.Location = new System.Drawing.Point(152, 418);
            this.btAni_SortAnimationFrameList.Name = "btAni_SortAnimationFrameList";
            this.btAni_SortAnimationFrameList.Size = new System.Drawing.Size(75, 23);
            this.btAni_SortAnimationFrameList.TabIndex = 8;
            this.btAni_SortAnimationFrameList.Text = "重新排序";
            this.btAni_SortAnimationFrameList.UseVisualStyleBackColor = true;
            this.btAni_SortAnimationFrameList.Click += new System.EventHandler(this.btAni_SortAnimationFrameList_Click);
            // 
            // aniTextBox_FPS
            // 
            this.aniTextBox_FPS.Location = new System.Drawing.Point(540, 125);
            this.aniTextBox_FPS.Name = "aniTextBox_FPS";
            this.aniTextBox_FPS.Size = new System.Drawing.Size(44, 21);
            this.aniTextBox_FPS.TabIndex = 7;
            this.aniTextBox_FPS.Text = "60";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(490, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "FPS ：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(234, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "动画名字 ：";
            // 
            // aniTextBox_Name
            // 
            this.aniTextBox_Name.Location = new System.Drawing.Point(311, 18);
            this.aniTextBox_Name.Name = "aniTextBox_Name";
            this.aniTextBox_Name.Size = new System.Drawing.Size(166, 21);
            this.aniTextBox_Name.TabIndex = 5;
            // 
            // btAni_DelAniTexture
            // 
            this.btAni_DelAniTexture.Location = new System.Drawing.Point(87, 18);
            this.btAni_DelAniTexture.Name = "btAni_DelAniTexture";
            this.btAni_DelAniTexture.Size = new System.Drawing.Size(75, 23);
            this.btAni_DelAniTexture.TabIndex = 2;
            this.btAni_DelAniTexture.Text = "删除素材";
            this.btAni_DelAniTexture.UseVisualStyleBackColor = true;
            this.btAni_DelAniTexture.Click += new System.EventHandler(this.btAni_DelAniTexture_Click);
            // 
            // btAni_AddAniTexture
            // 
            this.btAni_AddAniTexture.Location = new System.Drawing.Point(6, 18);
            this.btAni_AddAniTexture.Name = "btAni_AddAniTexture";
            this.btAni_AddAniTexture.Size = new System.Drawing.Size(75, 23);
            this.btAni_AddAniTexture.TabIndex = 2;
            this.btAni_AddAniTexture.Text = "添加素材";
            this.btAni_AddAniTexture.UseVisualStyleBackColor = true;
            this.btAni_AddAniTexture.Click += new System.EventHandler(this.btAni_AddAniTexture_Click);
            // 
            // aniListBox_FrameList
            // 
            this.aniListBox_FrameList.FormattingEnabled = true;
            this.aniListBox_FrameList.ItemHeight = 12;
            this.aniListBox_FrameList.Location = new System.Drawing.Point(6, 47);
            this.aniListBox_FrameList.Name = "aniListBox_FrameList";
            this.aniListBox_FrameList.ScrollAlwaysVisible = true;
            this.aniListBox_FrameList.Size = new System.Drawing.Size(224, 364);
            this.aniListBox_FrameList.TabIndex = 3;
            this.aniListBox_FrameList.SelectedIndexChanged += new System.EventHandler(this.aniListBox_FrameList_SelectedIndexChanged);
            // 
            // aniPropertyGrid_Infos
            // 
            this.aniPropertyGrid_Infos.Location = new System.Drawing.Point(236, 79);
            this.aniPropertyGrid_Infos.Name = "aniPropertyGrid_Infos";
            this.aniPropertyGrid_Infos.Size = new System.Drawing.Size(241, 356);
            this.aniPropertyGrid_Infos.TabIndex = 3;
            this.aniPropertyGrid_Infos.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.aniPropertyGrid_Infos_PropertyValueChanged);
            // 
            // btAni_PlayAnimation
            // 
            this.btAni_PlayAnimation.Location = new System.Drawing.Point(509, 96);
            this.btAni_PlayAnimation.Name = "btAni_PlayAnimation";
            this.btAni_PlayAnimation.Size = new System.Drawing.Size(75, 23);
            this.btAni_PlayAnimation.TabIndex = 2;
            this.btAni_PlayAnimation.Text = "播放动画";
            this.btAni_PlayAnimation.UseVisualStyleBackColor = true;
            this.btAni_PlayAnimation.Click += new System.EventHandler(this.btAni_PlayAnimation_Click);
            // 
            // btAni_LoadAnimation
            // 
            this.btAni_LoadAnimation.Location = new System.Drawing.Point(509, 67);
            this.btAni_LoadAnimation.Name = "btAni_LoadAnimation";
            this.btAni_LoadAnimation.Size = new System.Drawing.Size(75, 23);
            this.btAni_LoadAnimation.TabIndex = 2;
            this.btAni_LoadAnimation.Text = "加载动画";
            this.btAni_LoadAnimation.UseVisualStyleBackColor = true;
            this.btAni_LoadAnimation.Click += new System.EventHandler(this.btAni_LoadAnimation_Click);
            // 
            // btAni_ClearAnimation
            // 
            this.btAni_ClearAnimation.Location = new System.Drawing.Point(509, 9);
            this.btAni_ClearAnimation.Name = "btAni_ClearAnimation";
            this.btAni_ClearAnimation.Size = new System.Drawing.Size(75, 23);
            this.btAni_ClearAnimation.TabIndex = 2;
            this.btAni_ClearAnimation.Text = "清空动画";
            this.btAni_ClearAnimation.UseVisualStyleBackColor = true;
            this.btAni_ClearAnimation.Click += new System.EventHandler(this.btAni_ClearAnimation_Click);
            // 
            // btAni_SaveAnimation
            // 
            this.btAni_SaveAnimation.Location = new System.Drawing.Point(509, 38);
            this.btAni_SaveAnimation.Name = "btAni_SaveAnimation";
            this.btAni_SaveAnimation.Size = new System.Drawing.Size(75, 23);
            this.btAni_SaveAnimation.TabIndex = 2;
            this.btAni_SaveAnimation.Text = "保存动画";
            this.btAni_SaveAnimation.UseVisualStyleBackColor = true;
            this.btAni_SaveAnimation.Click += new System.EventHandler(this.btAni_SaveAnimation_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.aniPictureBox_frameTexture);
            this.groupBox2.Location = new System.Drawing.Point(607, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(144, 158);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "素材预览";
            // 
            // aniPictureBox_frameTexture
            // 
            this.aniPictureBox_frameTexture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.aniPictureBox_frameTexture.Location = new System.Drawing.Point(6, 20);
            this.aniPictureBox_frameTexture.Name = "aniPictureBox_frameTexture";
            this.aniPictureBox_frameTexture.Size = new System.Drawing.Size(128, 128);
            this.aniPictureBox_frameTexture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.aniPictureBox_frameTexture.TabIndex = 0;
            this.aniPictureBox_frameTexture.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.aniPictureBox_AniShow);
            this.groupBox1.Location = new System.Drawing.Point(483, 154);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 281);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "动画预览";
            // 
            // aniPictureBox_AniShow
            // 
            this.aniPictureBox_AniShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.aniPictureBox_AniShow.Location = new System.Drawing.Point(6, 20);
            this.aniPictureBox_AniShow.Name = "aniPictureBox_AniShow";
            this.aniPictureBox_AniShow.Size = new System.Drawing.Size(256, 256);
            this.aniPictureBox_AniShow.TabIndex = 0;
            this.aniPictureBox_AniShow.TabStop = false;
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
            // aniTimer_AnimationPlayDt
            // 
            this.aniTimer_AnimationPlayDt.Tick += new System.EventHandler(this.aniTimer_AnimationPlayDt_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(234, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "动画描述 ：";
            // 
            // aniTextBox_Descirpt
            // 
            this.aniTextBox_Descirpt.Location = new System.Drawing.Point(311, 47);
            this.aniTextBox_Descirpt.Name = "aniTextBox_Descirpt";
            this.aniTextBox_Descirpt.Size = new System.Drawing.Size(166, 21);
            this.aniTextBox_Descirpt.TabIndex = 5;
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
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.aniPictureBox_frameTexture)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.aniPictureBox_AniShow)).EndInit();
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
        private System.Windows.Forms.TabPage tabPage6;
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
        private System.Windows.Forms.PropertyGrid aniPropertyGrid_Infos;
        private System.Windows.Forms.Button btAni_LoadAnimation;
        private System.Windows.Forms.Button btAni_ClearAnimation;
        private System.Windows.Forms.Button btAni_SaveAnimation;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btAni_AddAniTexture;
        private System.Windows.Forms.PictureBox aniPictureBox_frameTexture;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox aniPictureBox_AniShow;
        private System.Windows.Forms.Button btAni_DelAniTexture;
        private System.Windows.Forms.ListBox aniListBox_FrameList;
        private System.Windows.Forms.Button btAni_PlayAnimation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox aniTextBox_Name;
        private System.Windows.Forms.Timer aniTimer_AnimationPlayDt;
        private System.Windows.Forms.TextBox aniTextBox_FPS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btAni_SortAnimationFrameList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox aniTextBox_Descirpt;
    }
}

