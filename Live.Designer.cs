namespace FootDetection
{
    partial class Live
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
            this.imageBox_Before = new Emgu.CV.UI.ImageBox();
            this.imageBox_After = new Emgu.CV.UI.ImageBox();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox_Before)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox_After)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox_Before
            // 
            this.imageBox_Before.Location = new System.Drawing.Point(12, 12);
            this.imageBox_Before.Name = "imageBox_Before";
            this.imageBox_Before.Size = new System.Drawing.Size(400, 300);
            this.imageBox_Before.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBox_Before.TabIndex = 2;
            this.imageBox_Before.TabStop = false;
            // 
            // imageBox_After
            // 
            this.imageBox_After.Location = new System.Drawing.Point(418, 12);
            this.imageBox_After.Name = "imageBox_After";
            this.imageBox_After.Size = new System.Drawing.Size(400, 300);
            this.imageBox_After.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBox_After.TabIndex = 3;
            this.imageBox_After.TabStop = false;
            // 
            // Live
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 330);
            this.Controls.Add(this.imageBox_After);
            this.Controls.Add(this.imageBox_Before);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Live";
            this.Text = "Live Image";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Live_FormClosing);
            this.Load += new System.EventHandler(this.Live_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox_Before)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox_After)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox_Before;
        private Emgu.CV.UI.ImageBox imageBox_After;
    }
}