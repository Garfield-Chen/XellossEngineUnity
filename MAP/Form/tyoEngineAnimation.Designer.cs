namespace tyoEngineEditor
{
    partial class tyoEngineAnimation
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
            this.dgvAnimation = new System.Windows.Forms.DataGridView();
            this.txtNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.animationCol = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelAnimation = new tyoEngineEditor.tyoEngineTitleMapEditWindow.MapEditPanel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.cbPlay = new System.Windows.Forms.CheckBox();
            this.panel = new tyoEngineEditor.tyoEngineTitleMapEditWindow.MapEditPanel();
            this.timerDraw = new System.Windows.Forms.Timer(this.components);
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelOriginal = new tyoEngineEditor.tyoEngineTitleMapEditWindow.MapEditPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnimation)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvAnimation
            // 
            this.dgvAnimation.AllowUserToAddRows = false;
            this.dgvAnimation.AllowUserToDeleteRows = false;
            this.dgvAnimation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvAnimation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAnimation.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvAnimation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAnimation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.txtNameCol,
            this.animationCol});
            this.dgvAnimation.Location = new System.Drawing.Point(12, 9);
            this.dgvAnimation.Name = "dgvAnimation";
            this.dgvAnimation.RowTemplate.Height = 23;
            this.dgvAnimation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAnimation.Size = new System.Drawing.Size(260, 681);
            this.dgvAnimation.TabIndex = 0;
            this.dgvAnimation.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAnimation_CellEndEdit);
            // 
            // txtNameCol
            // 
            this.txtNameCol.HeaderText = "动态图名称";
            this.txtNameCol.Name = "txtNameCol";
            // 
            // animationCol
            // 
            this.animationCol.HeaderText = "方向";
            this.animationCol.Items.AddRange(new object[] {
            "0-无",
            "1-上",
            "2-右上",
            "3-右",
            "4-右下",
            "5-下",
            "6-左下",
            "7-左",
            "8-左上"});
            this.animationCol.Name = "animationCol";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panelAnimation);
            this.groupBox1.Location = new System.Drawing.Point(278, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(316, 657);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "预览";
            // 
            // panelAnimation
            // 
            this.panelAnimation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAnimation.Location = new System.Drawing.Point(3, 17);
            this.panelAnimation.Name = "panelAnimation";
            this.panelAnimation.Size = new System.Drawing.Size(310, 637);
            this.panelAnimation.TabIndex = 0;
            this.panelAnimation.Paint += new System.Windows.Forms.PaintEventHandler(this.panelAnimation_Paint);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // cbPlay
            // 
            this.cbPlay.AutoSize = true;
            this.cbPlay.Location = new System.Drawing.Point(281, 9);
            this.cbPlay.Name = "cbPlay";
            this.cbPlay.Size = new System.Drawing.Size(72, 16);
            this.cbPlay.TabIndex = 2;
            this.cbPlay.Text = "播放动画";
            this.cbPlay.UseVisualStyleBackColor = true;
            this.cbPlay.CheckedChanged += new System.EventHandler(this.cbPlay_CheckedChanged);
            // 
            // panel
            // 
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(472, 649);
            this.panel.TabIndex = 0;
            this.panel.Visible = false;
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            this.panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
            this.panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_MouseUp);
            // 
            // timerDraw
            // 
            this.timerDraw.Enabled = true;
            this.timerDraw.Tick += new System.EventHandler(this.timerDraw_Tick);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(600, 9);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(486, 681);
            this.tabControl.TabIndex = 3;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelOriginal);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(478, 655);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "原图";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panelOriginal
            // 
            this.panelOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOriginal.Location = new System.Drawing.Point(3, 3);
            this.panelOriginal.Name = "panelOriginal";
            this.panelOriginal.Size = new System.Drawing.Size(472, 649);
            this.panelOriginal.TabIndex = 0;
            this.panelOriginal.Paint += new System.Windows.Forms.PaintEventHandler(this.panelOriginal_Paint);
            this.panelOriginal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelOriginal_MouseDown);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(478, 655);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "设置大小";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tyoEngineAnimation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 702);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.cbPlay);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvAnimation);
            this.Name = "tyoEngineAnimation";
            this.Text = "动态图";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnimation)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAnimation;
        private System.Windows.Forms.GroupBox groupBox1;
        private tyoEngineTitleMapEditWindow.MapEditPanel panelAnimation;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.CheckBox cbPlay;
        private tyoEngineTitleMapEditWindow.MapEditPanel panel;
        private System.Windows.Forms.Timer timerDraw;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private tyoEngineTitleMapEditWindow.MapEditPanel panelOriginal;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtNameCol;
        private System.Windows.Forms.DataGridViewComboBoxColumn animationCol;
    }
}