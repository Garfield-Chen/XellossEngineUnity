﻿namespace tyoEngineEditor
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
            this.components = new System.ComponentModel.Container();
            this.mapEditorAniListBox_LoadAniList = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btMapEditor_AddAniFile = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mapEditorAniPictureBox_AniShow = new System.Windows.Forms.PictureBox();
            this.btMapEditor_AddAniToMap = new System.Windows.Forms.Button();
            this.mapEditorAniTimer_PlayTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapEditorAniPictureBox_AniShow)).BeginInit();
            this.SuspendLayout();
            // 
            // mapEditorAniListBox_LoadAniList
            // 
            this.mapEditorAniListBox_LoadAniList.FormattingEnabled = true;
            this.mapEditorAniListBox_LoadAniList.Location = new System.Drawing.Point(6, 22);
            this.mapEditorAniListBox_LoadAniList.Name = "mapEditorAniListBox_LoadAniList";
            this.mapEditorAniListBox_LoadAniList.ScrollAlwaysVisible = true;
            this.mapEditorAniListBox_LoadAniList.Size = new System.Drawing.Size(270, 472);
            this.mapEditorAniListBox_LoadAniList.TabIndex = 0;
            this.mapEditorAniListBox_LoadAniList.SelectedIndexChanged += new System.EventHandler(this.mapEditorAniListBox_LoadAniList_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btMapEditor_AddAniFile);
            this.groupBox1.Controls.Add(this.mapEditorAniListBox_LoadAniList);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 540);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " 动画列表";
            // 
            // btMapEditor_AddAniFile
            // 
            this.btMapEditor_AddAniFile.Location = new System.Drawing.Point(201, 504);
            this.btMapEditor_AddAniFile.Name = "btMapEditor_AddAniFile";
            this.btMapEditor_AddAniFile.Size = new System.Drawing.Size(75, 25);
            this.btMapEditor_AddAniFile.TabIndex = 1;
            this.btMapEditor_AddAniFile.Text = "添加动画";
            this.btMapEditor_AddAniFile.UseVisualStyleBackColor = true;
            this.btMapEditor_AddAniFile.Click += new System.EventHandler(this.btMapEditor_AddAniFile_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.mapEditorAniPictureBox_AniShow);
            this.groupBox2.Location = new System.Drawing.Point(303, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 310);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "动画预览";
            // 
            // mapEditorAniPictureBox_AniShow
            // 
            this.mapEditorAniPictureBox_AniShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mapEditorAniPictureBox_AniShow.Location = new System.Drawing.Point(6, 22);
            this.mapEditorAniPictureBox_AniShow.Name = "mapEditorAniPictureBox_AniShow";
            this.mapEditorAniPictureBox_AniShow.Size = new System.Drawing.Size(256, 277);
            this.mapEditorAniPictureBox_AniShow.TabIndex = 0;
            this.mapEditorAniPictureBox_AniShow.TabStop = false;
            // 
            // btMapEditor_AddAniToMap
            // 
            this.btMapEditor_AddAniToMap.Location = new System.Drawing.Point(455, 512);
            this.btMapEditor_AddAniToMap.Name = "btMapEditor_AddAniToMap";
            this.btMapEditor_AddAniToMap.Size = new System.Drawing.Size(118, 34);
            this.btMapEditor_AddAniToMap.TabIndex = 1;
            this.btMapEditor_AddAniToMap.Text = "添加到地图";
            this.btMapEditor_AddAniToMap.UseVisualStyleBackColor = true;
            this.btMapEditor_AddAniToMap.Click += new System.EventHandler(this.btMapEditor_AddAniToMap_Click);
            // 
            // mapEditorAniTimer_PlayTimer
            // 
            this.mapEditorAniTimer_PlayTimer.Tick += new System.EventHandler(this.mapEditorAniTimer_PlayTimer_Tick);
            // 
            // tyoEngineTileAnimationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 563);
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
        private System.Windows.Forms.Timer mapEditorAniTimer_PlayTimer;
    }
}