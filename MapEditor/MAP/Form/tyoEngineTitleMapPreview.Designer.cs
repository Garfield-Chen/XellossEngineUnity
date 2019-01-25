namespace tyoEngineEditor
{
    partial class tyoEngineTileMapPreview
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
            this.panelPreView = new tyoEngineEditor.tyoEngineTileMapEditWindow.MapEditPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonZoom = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // panelPreView
            // 
            this.panelPreView.AutoScroll = true;
            this.panelPreView.Location = new System.Drawing.Point(0, 45);
            this.panelPreView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelPreView.Name = "panelPreView";
            this.panelPreView.Size = new System.Drawing.Size(1476, 1098);
            this.panelPreView.TabIndex = 0;
            this.panelPreView.Paint += new System.Windows.Forms.PaintEventHandler(this.panelPreView_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(123, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 18);
            this.label2.TabIndex = 11;
            this.label2.Text = "%";
            // 
            // buttonZoom
            // 
            this.buttonZoom.Location = new System.Drawing.Point(142, 6);
            this.buttonZoom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonZoom.Name = "buttonZoom";
            this.buttonZoom.Size = new System.Drawing.Size(72, 34);
            this.buttonZoom.TabIndex = 10;
            this.buttonZoom.Text = "OK";
            this.buttonZoom.UseVisualStyleBackColor = true;
            this.buttonZoom.Click += new System.EventHandler(this.buttonZoom_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 14);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 18);
            this.label3.TabIndex = 9;
            this.label3.Text = "缩放:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(69, 9);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.MaxLength = 2;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(50, 28);
            this.textBox1.TabIndex = 8;
            // 
            // tyoEngineTileMapPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1404, 1021);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonZoom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panelPreView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "tyoEngineTileMapPreview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "预览图";
            this.Shown += new System.EventHandler(this.tyoEngineTileMapPreview_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private tyoEngineTileMapEditWindow.MapEditPanel panelPreView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonZoom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
    }
}