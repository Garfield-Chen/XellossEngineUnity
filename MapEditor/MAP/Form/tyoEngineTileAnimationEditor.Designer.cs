namespace tyoEngineEditor
{
    partial class tyoEngineTileAnimationEditor
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
            this.mapEditorAniListBox_LoadAniList = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btMapEditor_AddAniFile = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mapEditorAniPictureBox_AniShow = new System.Windows.Forms.PictureBox();
            this.btMapEditor_AddAniToMap = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapEditorAniPictureBox_AniShow)).BeginInit();
            this.SuspendLayout();
            // 
            // mapEditorAniListBox_LoadAniList
            // 
            this.mapEditorAniListBox_LoadAniList.FormattingEnabled = true;
            this.mapEditorAniListBox_LoadAniList.ItemHeight = 12;
            this.mapEditorAniListBox_LoadAniList.Location = new System.Drawing.Point(6, 20);
            this.mapEditorAniListBox_LoadAniList.Name = "mapEditorAniListBox_LoadAniList";
            this.mapEditorAniListBox_LoadAniList.ScrollAlwaysVisible = true;
            this.mapEditorAniListBox_LoadAniList.Size = new System.Drawing.Size(270, 436);
            this.mapEditorAniListBox_LoadAniList.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btMapEditor_AddAniFile);
            this.groupBox1.Controls.Add(this.mapEditorAniListBox_LoadAniList);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 498);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " 动画列表";
            // 
            // btMapEditor_AddAniFile
            // 
            this.btMapEditor_AddAniFile.Location = new System.Drawing.Point(201, 465);
            this.btMapEditor_AddAniFile.Name = "btMapEditor_AddAniFile";
            this.btMapEditor_AddAniFile.Size = new System.Drawing.Size(75, 23);
            this.btMapEditor_AddAniFile.TabIndex = 1;
            this.btMapEditor_AddAniFile.Text = "添加动画";
            this.btMapEditor_AddAniFile.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.mapEditorAniPictureBox_AniShow);
            this.groupBox2.Location = new System.Drawing.Point(303, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 286);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "动画预览";
            // 
            // mapEditorAniPictureBox_AniShow
            // 
            this.mapEditorAniPictureBox_AniShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mapEditorAniPictureBox_AniShow.Location = new System.Drawing.Point(6, 20);
            this.mapEditorAniPictureBox_AniShow.Name = "mapEditorAniPictureBox_AniShow";
            this.mapEditorAniPictureBox_AniShow.Size = new System.Drawing.Size(256, 256);
            this.mapEditorAniPictureBox_AniShow.TabIndex = 0;
            this.mapEditorAniPictureBox_AniShow.TabStop = false;
            // 
            // btMapEditor_AddAniToMap
            // 
            this.btMapEditor_AddAniToMap.Location = new System.Drawing.Point(455, 473);
            this.btMapEditor_AddAniToMap.Name = "btMapEditor_AddAniToMap";
            this.btMapEditor_AddAniToMap.Size = new System.Drawing.Size(118, 31);
            this.btMapEditor_AddAniToMap.TabIndex = 1;
            this.btMapEditor_AddAniToMap.Text = "添加到地图";
            this.btMapEditor_AddAniToMap.UseVisualStyleBackColor = true;
            // 
            // tyoEngineTileAnimationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 529);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btMapEditor_AddAniToMap);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "tyoEngineTileAnimationEditor";
            this.Text = "tyoEngineTileAnimationEditor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mapEditorAniPictureBox_AniShow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox mapEditorAniListBox_LoadAniList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btMapEditor_AddAniFile;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox mapEditorAniPictureBox_AniShow;
        private System.Windows.Forms.Button btMapEditor_AddAniToMap;
    }
}