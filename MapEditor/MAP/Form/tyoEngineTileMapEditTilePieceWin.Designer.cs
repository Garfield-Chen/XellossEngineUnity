namespace tyoEngineEditor
{
    partial class tyoEngineTileMapEditTilePieceWin
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
            this.mapTitlePanel = new tyoEngineEditor.tyoEngineTileMapEditWindow.MapEditPanel();
            this.comboxTitleSelect = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btAddTitle = new System.Windows.Forms.Button();
            this.textTitleName = new System.Windows.Forms.TextBox();
            this.comboBoxTitleScale = new System.Windows.Forms.ComboBox();
            this.comboxTitleDirSelect = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // mapTitlePanel
            // 
            this.mapTitlePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mapTitlePanel.Location = new System.Drawing.Point(8, 42);
            this.mapTitlePanel.Margin = new System.Windows.Forms.Padding(2);
            this.mapTitlePanel.Name = "mapTitlePanel";
            this.mapTitlePanel.Size = new System.Drawing.Size(683, 615);
            this.mapTitlePanel.TabIndex = 0;
            this.mapTitlePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mapTilePanel_Paint);
            this.mapTitlePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mapTitlePanel_MouseDown);
            this.mapTitlePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapTitlePanel_MouseMove);
            this.mapTitlePanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mapTilePanel_MouseUp);
            // 
            // comboxTitleSelect
            // 
            this.comboxTitleSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboxTitleSelect.FormattingEnabled = true;
            this.comboxTitleSelect.Location = new System.Drawing.Point(193, 11);
            this.comboxTitleSelect.Margin = new System.Windows.Forms.Padding(2);
            this.comboxTitleSelect.Name = "comboxTitleSelect";
            this.comboxTitleSelect.Size = new System.Drawing.Size(121, 20);
            this.comboxTitleSelect.TabIndex = 1;
            this.comboxTitleSelect.SelectedIndexChanged += new System.EventHandler(this.comboxTitleSelect_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "图块选择:";
            // 
            // btAddTitle
            // 
            this.btAddTitle.Location = new System.Drawing.Point(325, 9);
            this.btAddTitle.Name = "btAddTitle";
            this.btAddTitle.Size = new System.Drawing.Size(75, 21);
            this.btAddTitle.TabIndex = 3;
            this.btAddTitle.Text = "增加图块";
            this.btAddTitle.UseVisualStyleBackColor = true;
            this.btAddTitle.Click += new System.EventHandler(this.btAddTitle_Click);
            // 
            // textTitleName
            // 
            this.textTitleName.Location = new System.Drawing.Point(406, 11);
            this.textTitleName.Name = "textTitleName";
            this.textTitleName.Size = new System.Drawing.Size(229, 21);
            this.textTitleName.TabIndex = 4;
            // 
            // comboBoxTitleScale
            // 
            this.comboBoxTitleScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTitleScale.FormattingEnabled = true;
            this.comboBoxTitleScale.Items.AddRange(new object[] {
            "100%",
            "75%",
            "50%",
            "25%"});
            this.comboBoxTitleScale.Location = new System.Drawing.Point(637, 12);
            this.comboBoxTitleScale.Name = "comboBoxTitleScale";
            this.comboBoxTitleScale.Size = new System.Drawing.Size(54, 20);
            this.comboBoxTitleScale.TabIndex = 5;
            this.comboBoxTitleScale.SelectedIndexChanged += new System.EventHandler(this.comboBoxTitleScale_SelectedIndexChanged);
            // 
            // comboxTitleDirSelect
            // 
            this.comboxTitleDirSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboxTitleDirSelect.FormattingEnabled = true;
            this.comboxTitleDirSelect.Location = new System.Drawing.Point(68, 11);
            this.comboxTitleDirSelect.Margin = new System.Windows.Forms.Padding(2);
            this.comboxTitleDirSelect.Name = "comboxTitleDirSelect";
            this.comboxTitleDirSelect.Size = new System.Drawing.Size(121, 20);
            this.comboxTitleDirSelect.TabIndex = 6;
            this.comboxTitleDirSelect.SelectedIndexChanged += new System.EventHandler(this.comboxTitleDirSelect_SelectedIndexChanged);
            // 
            // tyoEngineTitleMapEditTitlePieceWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 674);
            this.ControlBox = false;
            this.Controls.Add(this.comboxTitleDirSelect);
            this.Controls.Add(this.comboBoxTitleScale);
            this.Controls.Add(this.textTitleName);
            this.Controls.Add(this.btAddTitle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboxTitleSelect);
            this.Controls.Add(this.mapTitlePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "tyoEngineTitleMapEditTitlePieceWin";
            this.Text = "tyoEngineTitleMapEditTitlePieceWin";
            this.Shown += new System.EventHandler(this.tyoEngineTitleMapEditTilePieceWin_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private tyoEngineEditor.tyoEngineTileMapEditWindow.MapEditPanel mapTitlePanel;
        private System.Windows.Forms.ComboBox comboxTitleSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btAddTitle;
        private System.Windows.Forms.TextBox textTitleName;
        private System.Windows.Forms.ComboBox comboBoxTitleScale;
        private System.Windows.Forms.ComboBox comboxTitleDirSelect;
    }
}