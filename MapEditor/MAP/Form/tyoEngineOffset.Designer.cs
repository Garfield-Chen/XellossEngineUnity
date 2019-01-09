namespace tyoEngineEditor
{
    partial class tyoEngineOffset
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
            this.hScrollBarX = new System.Windows.Forms.HScrollBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.hScrollBarY = new System.Windows.Forms.HScrollBar();
            this.SuspendLayout();
            // 
            // hScrollBarX
            // 
            this.hScrollBarX.Location = new System.Drawing.Point(56, 9);
            this.hScrollBarX.Maximum = 1000;
            this.hScrollBarX.Name = "hScrollBarX";
            this.hScrollBarX.Size = new System.Drawing.Size(229, 47);
            this.hScrollBarX.TabIndex = 1;
            this.hScrollBarX.Value = 500;
            this.hScrollBarX.ValueChanged += new System.EventHandler(this.hScrollBarX_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "X方向：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Y方向：";
            // 
            // hScrollBarY
            // 
            this.hScrollBarY.Location = new System.Drawing.Point(56, 56);
            this.hScrollBarY.Maximum = 1000;
            this.hScrollBarY.Name = "hScrollBarY";
            this.hScrollBarY.Size = new System.Drawing.Size(229, 47);
            this.hScrollBarY.TabIndex = 3;
            this.hScrollBarY.Value = 500;
            this.hScrollBarY.ValueChanged += new System.EventHandler(this.hScrollBarY_ValueChanged);
            // 
            // tyoEngineOffset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 115);
            this.Controls.Add(this.hScrollBarY);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hScrollBarX);
            this.Controls.Add(this.label2);
            this.Name = "tyoEngineOffset";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "偏移量设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.HScrollBar hScrollBarX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.HScrollBar hScrollBarY;
    }
}