namespace tyoEngineEditor
{
    partial class tyoEngineLayerEdit
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
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.listBoxMapEditor_MapLayer = new System.Windows.Forms.ListBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.propertyGridMapEditor_MapLayer = new System.Windows.Forms.PropertyGrid();
            this.btMAP_DelLayer = new System.Windows.Forms.Button();
            this.btMAP_AddLayer = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.groupBox12.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.listBoxMapEditor_MapLayer);
            this.groupBox12.Location = new System.Drawing.Point(261, 12);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(210, 363);
            this.groupBox12.TabIndex = 9;
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
            this.listBoxMapEditor_MapLayer.SelectedIndexChanged += new System.EventHandler(this.listBoxMapEditor_MapLayer_SelectedIndexChanged);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.propertyGridMapEditor_MapLayer);
            this.groupBox11.Location = new System.Drawing.Point(12, 41);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(243, 334);
            this.groupBox11.TabIndex = 8;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "地图层信息";
            // 
            // propertyGridMapEditor_MapLayer
            // 
            this.propertyGridMapEditor_MapLayer.Location = new System.Drawing.Point(6, 19);
            this.propertyGridMapEditor_MapLayer.Name = "propertyGridMapEditor_MapLayer";
            this.propertyGridMapEditor_MapLayer.Size = new System.Drawing.Size(231, 309);
            this.propertyGridMapEditor_MapLayer.TabIndex = 2;
            this.propertyGridMapEditor_MapLayer.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGridMapEditor_MapLayer_PropertyValueChanged);
            // 
            // btMAP_DelLayer
            // 
            this.btMAP_DelLayer.Location = new System.Drawing.Point(93, 12);
            this.btMAP_DelLayer.Name = "btMAP_DelLayer";
            this.btMAP_DelLayer.Size = new System.Drawing.Size(75, 23);
            this.btMAP_DelLayer.TabIndex = 7;
            this.btMAP_DelLayer.Text = "删除图层";
            this.btMAP_DelLayer.UseVisualStyleBackColor = true;
            this.btMAP_DelLayer.Click += new System.EventHandler(this.btMAP_DelLayer_Click);
            // 
            // btMAP_AddLayer
            // 
            this.btMAP_AddLayer.Location = new System.Drawing.Point(12, 12);
            this.btMAP_AddLayer.Name = "btMAP_AddLayer";
            this.btMAP_AddLayer.Size = new System.Drawing.Size(75, 23);
            this.btMAP_AddLayer.TabIndex = 6;
            this.btMAP_AddLayer.Text = "增加图层";
            this.btMAP_AddLayer.UseVisualStyleBackColor = true;
            this.btMAP_AddLayer.Click += new System.EventHandler(this.btMAP_AddLayer_Click);
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(315, 381);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 10;
            this.btSave.Text = "保存";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(396, 381);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 11;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // tyoEngineLayerEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 415);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.groupBox12);
            this.Controls.Add(this.groupBox11);
            this.Controls.Add(this.btMAP_DelLayer);
            this.Controls.Add(this.btMAP_AddLayer);
            this.Name = "tyoEngineLayerEdit";
            this.Text = "图层编辑";
            this.groupBox12.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.ListBox listBoxMapEditor_MapLayer;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.PropertyGrid propertyGridMapEditor_MapLayer;
        private System.Windows.Forms.Button btMAP_DelLayer;
        private System.Windows.Forms.Button btMAP_AddLayer;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btCancel;
    }
}