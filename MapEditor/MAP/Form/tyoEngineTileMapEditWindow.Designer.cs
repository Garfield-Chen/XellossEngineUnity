﻿namespace tyoEngineEditor
{
    partial class tyoEngineTileMapEditWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tyoEngineTileMapEditWindow));
            this.panelMap = new tyoEngineEditor.tyoEngineTileMapEditWindow.MapEditPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelMapMousPos = new System.Windows.Forms.Label();
            this.btMapSharp = new System.Windows.Forms.Button();
            this.mapVScrollBar = new System.Windows.Forms.VScrollBar();
            this.mapHScrollBar = new System.Windows.Forms.HScrollBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btTileAttribute = new System.Windows.Forms.Button();
            this.btEditLayer = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btAnimation = new System.Windows.Forms.Button();
            this.hSystemFPSBar = new System.Windows.Forms.HScrollBar();
            this.loadMonsterBtn = new System.Windows.Forms.Button();
            this.comboBoxFun1 = new System.Windows.Forms.ComboBox();
            this.comboBoxFun3 = new System.Windows.Forms.ComboBox();
            this.comboBoxMapLayer = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonPreview = new System.Windows.Forms.Button();
            this.btShowMapTitleDlg = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBoxNowSelect = new System.Windows.Forms.PictureBox();
            this.checkedListBoxMapLayerShow = new System.Windows.Forms.CheckedListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btOutputPNGFile = new System.Windows.Forms.ToolStripMenuItem();
            this.btOutputMapDataFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveEditorDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animationUpdater_Timer = new System.Windows.Forms.Timer(this.components);
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNowSelect)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMap
            // 
            this.panelMap.AutoScroll = true;
            this.panelMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMap.Location = new System.Drawing.Point(12, 38);
            this.panelMap.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panelMap.Name = "panelMap";
            this.panelMap.Size = new System.Drawing.Size(1492, 758);
            this.panelMap.TabIndex = 4;
            this.panelMap.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMap_Paint);
            this.panelMap.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelMap_MouseDoubleClick);
            this.panelMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMap_MouseDown);
            this.panelMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelMap_MouseMove);
            this.panelMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelMap_MouseUp);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelMapMousPos);
            this.groupBox2.Controls.Add(this.btMapSharp);
            this.groupBox2.Controls.Add(this.panelMap);
            this.groupBox2.Controls.Add(this.mapVScrollBar);
            this.groupBox2.Controls.Add(this.mapHScrollBar);
            this.groupBox2.Location = new System.Drawing.Point(14, 330);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Size = new System.Drawing.Size(1552, 852);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "绘制区";
            // 
            // labelMapMousPos
            // 
            this.labelMapMousPos.AutoSize = true;
            this.labelMapMousPos.Location = new System.Drawing.Point(1376, 0);
            this.labelMapMousPos.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelMapMousPos.Name = "labelMapMousPos";
            this.labelMapMousPos.Size = new System.Drawing.Size(190, 24);
            this.labelMapMousPos.TabIndex = 4;
            this.labelMapMousPos.Text = "坐标: X=0 Y=110";
            // 
            // btMapSharp
            // 
            this.btMapSharp.Location = new System.Drawing.Point(1512, 804);
            this.btMapSharp.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btMapSharp.Name = "btMapSharp";
            this.btMapSharp.Size = new System.Drawing.Size(36, 36);
            this.btMapSharp.TabIndex = 4;
            this.btMapSharp.Text = "#";
            this.btMapSharp.UseVisualStyleBackColor = true;
            this.btMapSharp.Click += new System.EventHandler(this.btMapSharp_Click);
            // 
            // mapVScrollBar
            // 
            this.mapVScrollBar.LargeChange = 1;
            this.mapVScrollBar.Location = new System.Drawing.Point(1512, 38);
            this.mapVScrollBar.Name = "mapVScrollBar";
            this.mapVScrollBar.Size = new System.Drawing.Size(18, 758);
            this.mapVScrollBar.TabIndex = 4;
            this.mapVScrollBar.ValueChanged += new System.EventHandler(this.mapVScrollBar_ValueChanged);
            // 
            // mapHScrollBar
            // 
            this.mapHScrollBar.LargeChange = 1;
            this.mapHScrollBar.Location = new System.Drawing.Point(12, 804);
            this.mapHScrollBar.Name = "mapHScrollBar";
            this.mapHScrollBar.Size = new System.Drawing.Size(1474, 16);
            this.mapHScrollBar.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btTileAttribute);
            this.groupBox3.Controls.Add(this.btEditLayer);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.btAnimation);
            this.groupBox3.Controls.Add(this.hSystemFPSBar);
            this.groupBox3.Controls.Add(this.loadMonsterBtn);
            this.groupBox3.Controls.Add(this.comboBoxFun1);
            this.groupBox3.Controls.Add(this.comboBoxFun3);
            this.groupBox3.Controls.Add(this.comboBoxMapLayer);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(570, 6);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox3.Size = new System.Drawing.Size(994, 246);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "功能区";
            // 
            // btTileAttribute
            // 
            this.btTileAttribute.Location = new System.Drawing.Point(542, 190);
            this.btTileAttribute.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btTileAttribute.Name = "btTileAttribute";
            this.btTileAttribute.Size = new System.Drawing.Size(166, 46);
            this.btTileAttribute.TabIndex = 14;
            this.btTileAttribute.Text = " 图块属性";
            this.btTileAttribute.UseVisualStyleBackColor = true;
            this.btTileAttribute.Click += new System.EventHandler(this.btTileAttribute_Click);
            // 
            // btEditLayer
            // 
            this.btEditLayer.Location = new System.Drawing.Point(368, 190);
            this.btEditLayer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btEditLayer.Name = "btEditLayer";
            this.btEditLayer.Size = new System.Drawing.Size(166, 46);
            this.btEditLayer.TabIndex = 14;
            this.btEditLayer.Text = "图层编辑";
            this.btEditLayer.UseVisualStyleBackColor = true;
            this.btEditLayer.Click += new System.EventHandler(this.btEditLayer_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 134);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(130, 24);
            this.label8.TabIndex = 13;
            this.label8.Text = "调整频率：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(750, 134);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 24);
            this.label1.TabIndex = 11;
            this.label1.Text = "100 FPS";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(152, 134);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 24);
            this.label7.TabIndex = 10;
            this.label7.Text = "5 FPS";
            // 
            // btAnimation
            // 
            this.btAnimation.Location = new System.Drawing.Point(194, 190);
            this.btAnimation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btAnimation.Name = "btAnimation";
            this.btAnimation.Size = new System.Drawing.Size(166, 46);
            this.btAnimation.TabIndex = 12;
            this.btAnimation.Text = "动画图块";
            this.btAnimation.UseVisualStyleBackColor = true;
            this.btAnimation.Click += new System.EventHandler(this.btAnimation_Click);
            // 
            // hSystemFPSBar
            // 
            this.hSystemFPSBar.LargeChange = 1;
            this.hSystemFPSBar.Location = new System.Drawing.Point(228, 126);
            this.hSystemFPSBar.Minimum = 5;
            this.hSystemFPSBar.Name = "hSystemFPSBar";
            this.hSystemFPSBar.Size = new System.Drawing.Size(516, 16);
            this.hSystemFPSBar.TabIndex = 9;
            this.hSystemFPSBar.Value = 20;
            this.hSystemFPSBar.ValueChanged += new System.EventHandler(this.hSystemFPSBar_ValueChanged);
            // 
            // loadMonsterBtn
            // 
            this.loadMonsterBtn.Location = new System.Drawing.Point(16, 190);
            this.loadMonsterBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.loadMonsterBtn.Name = "loadMonsterBtn";
            this.loadMonsterBtn.Size = new System.Drawing.Size(166, 46);
            this.loadMonsterBtn.TabIndex = 11;
            this.loadMonsterBtn.Text = "添加怪物";
            this.loadMonsterBtn.UseVisualStyleBackColor = true;
            this.loadMonsterBtn.Click += new System.EventHandler(this.loadMonsterBtn_Click);
            // 
            // comboBoxFun1
            // 
            this.comboBoxFun1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFun1.FormattingEnabled = true;
            this.comboBoxFun1.Items.AddRange(new object[] {
            "绘制图块",
            "删除图块",
            "绘制阻挡",
            "删除阻挡",
            "填充图块",
            "清空图块",
            "设置属性",
            "全部清除"});
            this.comboBoxFun1.Location = new System.Drawing.Point(12, 58);
            this.comboBoxFun1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.comboBoxFun1.Name = "comboBoxFun1";
            this.comboBoxFun1.Size = new System.Drawing.Size(162, 32);
            this.comboBoxFun1.TabIndex = 1;
            this.comboBoxFun1.SelectedIndexChanged += new System.EventHandler(this.comboBoxFun1_SelectedIndexChanged);
            // 
            // comboBoxFun3
            // 
            this.comboBoxFun3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFun3.FormattingEnabled = true;
            this.comboBoxFun3.Items.AddRange(new object[] {
            "显示网格",
            "隐藏网格",
            "显示阻挡",
            "隐藏阻挡",
            "地图原始",
            "地图75%",
            "地图50%",
            "地图25%"});
            this.comboBoxFun3.Location = new System.Drawing.Point(368, 58);
            this.comboBoxFun3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.comboBoxFun3.Name = "comboBoxFun3";
            this.comboBoxFun3.Size = new System.Drawing.Size(162, 32);
            this.comboBoxFun3.TabIndex = 1;
            this.comboBoxFun3.SelectedIndexChanged += new System.EventHandler(this.comboBoxFun3_SelectedIndexChanged);
            // 
            // comboBoxMapLayer
            // 
            this.comboBoxMapLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMapLayer.FormattingEnabled = true;
            this.comboBoxMapLayer.Location = new System.Drawing.Point(190, 58);
            this.comboBoxMapLayer.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.comboBoxMapLayer.Name = "comboBoxMapLayer";
            this.comboBoxMapLayer.Size = new System.Drawing.Size(162, 32);
            this.comboBoxMapLayer.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(392, 30);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 24);
            this.label4.TabIndex = 1;
            this.label4.Text = "额外功能";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 28);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 24);
            this.label3.TabIndex = 1;
            this.label3.Text = "绘制";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(224, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "地图层";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonPreview);
            this.groupBox4.Controls.Add(this.btShowMapTitleDlg);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.pictureBoxNowSelect);
            this.groupBox4.Controls.Add(this.checkedListBoxMapLayerShow);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(14, 6);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox4.Size = new System.Drawing.Size(544, 312);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "信息栏";
            // 
            // buttonPreview
            // 
            this.buttonPreview.Location = new System.Drawing.Point(18, 202);
            this.buttonPreview.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonPreview.Name = "buttonPreview";
            this.buttonPreview.Size = new System.Drawing.Size(140, 46);
            this.buttonPreview.TabIndex = 10;
            this.buttonPreview.Text = "预览图";
            this.buttonPreview.UseVisualStyleBackColor = true;
            this.buttonPreview.Click += new System.EventHandler(this.buttonPreview_Click);
            // 
            // btShowMapTitleDlg
            // 
            this.btShowMapTitleDlg.Location = new System.Drawing.Point(18, 256);
            this.btShowMapTitleDlg.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btShowMapTitleDlg.Name = "btShowMapTitleDlg";
            this.btShowMapTitleDlg.Size = new System.Drawing.Size(140, 46);
            this.btShowMapTitleDlg.TabIndex = 9;
            this.btShowMapTitleDlg.Text = "地图图块";
            this.btShowMapTitleDlg.UseVisualStyleBackColor = true;
            this.btShowMapTitleDlg.Click += new System.EventHandler(this.btShowMapTitleDlg_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 34);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 24);
            this.label6.TabIndex = 3;
            this.label6.Text = "当前图块:";
            // 
            // pictureBoxNowSelect
            // 
            this.pictureBoxNowSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxNowSelect.Location = new System.Drawing.Point(142, 30);
            this.pictureBoxNowSelect.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pictureBoxNowSelect.Name = "pictureBoxNowSelect";
            this.pictureBoxNowSelect.Size = new System.Drawing.Size(126, 126);
            this.pictureBoxNowSelect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxNowSelect.TabIndex = 2;
            this.pictureBoxNowSelect.TabStop = false;
            // 
            // checkedListBoxMapLayerShow
            // 
            this.checkedListBoxMapLayerShow.FormattingEnabled = true;
            this.checkedListBoxMapLayerShow.Location = new System.Drawing.Point(290, 62);
            this.checkedListBoxMapLayerShow.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.checkedListBoxMapLayerShow.Name = "checkedListBoxMapLayerShow";
            this.checkedListBoxMapLayerShow.ScrollAlwaysVisible = true;
            this.checkedListBoxMapLayerShow.Size = new System.Drawing.Size(236, 184);
            this.checkedListBoxMapLayerShow.TabIndex = 0;
            this.checkedListBoxMapLayerShow.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxMapLayerShow_ItemCheck);
            this.checkedListBoxMapLayerShow.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxMapLayerShow_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(320, 26);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 24);
            this.label5.TabIndex = 1;
            this.label5.Text = "显示地图图层";
            // 
            // updateTimer
            // 
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1191);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 28, 0);
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(1570, 37);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btOutputPNGFile,
            this.btOutputMapDataFile,
            this.toolStripSeparator1,
            this.SaveEditorDataToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(132, 35);
            this.toolStripDropDownButton1.Text = "地图数据";
            // 
            // btOutputPNGFile
            // 
            this.btOutputPNGFile.Name = "btOutputPNGFile";
            this.btOutputPNGFile.Size = new System.Drawing.Size(285, 38);
            this.btOutputPNGFile.Text = "导出PNG预览图";
            this.btOutputPNGFile.Click += new System.EventHandler(this.btOutputPNGFile_Click);
            // 
            // btOutputMapDataFile
            // 
            this.btOutputMapDataFile.Name = "btOutputMapDataFile";
            this.btOutputMapDataFile.Size = new System.Drawing.Size(285, 38);
            this.btOutputMapDataFile.Text = "导出地图数据";
            this.btOutputMapDataFile.Click += new System.EventHandler(this.btOutputMapDataFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(282, 6);
            // 
            // SaveEditorDataToolStripMenuItem
            // 
            this.SaveEditorDataToolStripMenuItem.Name = "SaveEditorDataToolStripMenuItem";
            this.SaveEditorDataToolStripMenuItem.Size = new System.Drawing.Size(285, 38);
            this.SaveEditorDataToolStripMenuItem.Text = "保存编辑数据";
            this.SaveEditorDataToolStripMenuItem.Click += new System.EventHandler(this.SaveEditorDataToolStripMenuItem_Click);
            // 
            // animationUpdater_Timer
            // 
            this.animationUpdater_Timer.Interval = 10;
            this.animationUpdater_Timer.Tick += new System.EventHandler(this.animationUpdater_Timer_Tick);
            // 
            // tyoEngineTileMapEditWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1570, 1228);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "tyoEngineTileMapEditWindow";
            this.Text = "新的地图";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.tyoEngineTitleMapEditWindow_FormClosing);
            this.Shown += new System.EventHandler(this.tyoEngineTileMapEditWindow_Shown);
            this.ResizeEnd += new System.EventHandler(this.tyoEngineTitleMapEditWindow_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tyoEngineTitleMapEditWindow_KeyDown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNowSelect)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btMapSharp;
        private System.Windows.Forms.VScrollBar mapVScrollBar;
        private System.Windows.Forms.HScrollBar mapHScrollBar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBoxFun1;
        private System.Windows.Forms.ComboBox comboBoxMapLayer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxFun3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckedListBox checkedListBoxMapLayerShow;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBoxNowSelect;
        private tyoEngineTileMapEditWindow.MapEditPanel panelMap;
        private System.Windows.Forms.Label labelMapMousPos;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem btOutputPNGFile;
        private System.Windows.Forms.ToolStripMenuItem btOutputMapDataFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem SaveEditorDataToolStripMenuItem;
        private System.Windows.Forms.Button btShowMapTitleDlg;
        private System.Windows.Forms.Button buttonPreview;
        private System.Windows.Forms.Button loadMonsterBtn;
        private System.Windows.Forms.Button btAnimation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.HScrollBar hSystemFPSBar;
        private System.Windows.Forms.Button btEditLayer;
        private System.Windows.Forms.Button btTileAttribute;
        private System.Windows.Forms.Timer animationUpdater_Timer;
    }
}