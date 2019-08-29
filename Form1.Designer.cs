namespace FootDetection
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.imageBox2 = new Emgu.CV.UI.ImageBox();
            this.imageBox3 = new Emgu.CV.UI.ImageBox();
            this.trackBar_H_Low = new System.Windows.Forms.TrackBar();
            this.trackBar_S_Low = new System.Windows.Forms.TrackBar();
            this.trackBar_V_Low = new System.Windows.Forms.TrackBar();
            this.trackBar_H_Up = new System.Windows.Forms.TrackBar();
            this.trackBar_S_Up = new System.Windows.Forms.TrackBar();
            this.trackBar_V_Up = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_H_Low = new System.Windows.Forms.Label();
            this.label_S_Low = new System.Windows.Forms.Label();
            this.label_V_Low = new System.Windows.Forms.Label();
            this.label_H_Up = new System.Windows.Forms.Label();
            this.label_S_Up = new System.Windows.Forms.Label();
            this.label_V_Up = new System.Windows.Forms.Label();
            this.imageBox4 = new Emgu.CV.UI.ImageBox();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_H_Low)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_S_Low)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_V_Low)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_H_Up)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_S_Up)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_V_Up)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox1
            // 
            this.imageBox1.Location = new System.Drawing.Point(12, 12);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(318, 218);
            this.imageBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // imageBox2
            // 
            this.imageBox2.Location = new System.Drawing.Point(336, 12);
            this.imageBox2.Name = "imageBox2";
            this.imageBox2.Size = new System.Drawing.Size(318, 218);
            this.imageBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBox2.TabIndex = 3;
            this.imageBox2.TabStop = false;
            // 
            // imageBox3
            // 
            this.imageBox3.Location = new System.Drawing.Point(660, 12);
            this.imageBox3.Name = "imageBox3";
            this.imageBox3.Size = new System.Drawing.Size(318, 218);
            this.imageBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBox3.TabIndex = 4;
            this.imageBox3.TabStop = false;
            // 
            // trackBar_H_Low
            // 
            this.trackBar_H_Low.Location = new System.Drawing.Point(12, 277);
            this.trackBar_H_Low.Maximum = 360;
            this.trackBar_H_Low.Name = "trackBar_H_Low";
            this.trackBar_H_Low.Size = new System.Drawing.Size(318, 56);
            this.trackBar_H_Low.TabIndex = 5;
            this.trackBar_H_Low.Value = 111;
            this.trackBar_H_Low.Scroll += new System.EventHandler(this.TrackBar_H_Low_Scroll);
            // 
            // trackBar_S_Low
            // 
            this.trackBar_S_Low.Location = new System.Drawing.Point(12, 339);
            this.trackBar_S_Low.Maximum = 255;
            this.trackBar_S_Low.Name = "trackBar_S_Low";
            this.trackBar_S_Low.Size = new System.Drawing.Size(318, 56);
            this.trackBar_S_Low.TabIndex = 6;
            this.trackBar_S_Low.Value = 64;
            this.trackBar_S_Low.Scroll += new System.EventHandler(this.TrackBar_S_Low_Scroll);
            // 
            // trackBar_V_Low
            // 
            this.trackBar_V_Low.Location = new System.Drawing.Point(12, 401);
            this.trackBar_V_Low.Maximum = 255;
            this.trackBar_V_Low.Name = "trackBar_V_Low";
            this.trackBar_V_Low.Size = new System.Drawing.Size(318, 56);
            this.trackBar_V_Low.TabIndex = 7;
            this.trackBar_V_Low.Scroll += new System.EventHandler(this.TrackBar_V_Low_Scroll);
            // 
            // trackBar_H_Up
            // 
            this.trackBar_H_Up.Location = new System.Drawing.Point(336, 277);
            this.trackBar_H_Up.Maximum = 360;
            this.trackBar_H_Up.Name = "trackBar_H_Up";
            this.trackBar_H_Up.Size = new System.Drawing.Size(318, 56);
            this.trackBar_H_Up.TabIndex = 8;
            this.trackBar_H_Up.Value = 118;
            this.trackBar_H_Up.Scroll += new System.EventHandler(this.TrackBar_H_Up_Scroll);
            // 
            // trackBar_S_Up
            // 
            this.trackBar_S_Up.Location = new System.Drawing.Point(336, 339);
            this.trackBar_S_Up.Maximum = 255;
            this.trackBar_S_Up.Name = "trackBar_S_Up";
            this.trackBar_S_Up.Size = new System.Drawing.Size(318, 56);
            this.trackBar_S_Up.TabIndex = 9;
            this.trackBar_S_Up.Value = 255;
            this.trackBar_S_Up.Scroll += new System.EventHandler(this.TrackBar_S_Up_Scroll);
            // 
            // trackBar_V_Up
            // 
            this.trackBar_V_Up.Location = new System.Drawing.Point(336, 401);
            this.trackBar_V_Up.Maximum = 255;
            this.trackBar_V_Up.Name = "trackBar_V_Up";
            this.trackBar_V_Up.Size = new System.Drawing.Size(318, 56);
            this.trackBar_V_Up.TabIndex = 10;
            this.trackBar_V_Up.Value = 255;
            this.trackBar_V_Up.Scroll += new System.EventHandler(this.TrackBar_V_Up_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 247);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "LowerLimit";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(457, 247);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "UpperLimit";
            // 
            // label_H_Low
            // 
            this.label_H_Low.AutoSize = true;
            this.label_H_Low.Location = new System.Drawing.Point(272, 259);
            this.label_H_Low.Name = "label_H_Low";
            this.label_H_Low.Size = new System.Drawing.Size(41, 15);
            this.label_H_Low.TabIndex = 13;
            this.label_H_Low.Text = "label3";
            // 
            // label_S_Low
            // 
            this.label_S_Low.AutoSize = true;
            this.label_S_Low.Location = new System.Drawing.Point(272, 321);
            this.label_S_Low.Name = "label_S_Low";
            this.label_S_Low.Size = new System.Drawing.Size(41, 15);
            this.label_S_Low.TabIndex = 14;
            this.label_S_Low.Text = "label3";
            // 
            // label_V_Low
            // 
            this.label_V_Low.AutoSize = true;
            this.label_V_Low.Location = new System.Drawing.Point(272, 383);
            this.label_V_Low.Name = "label_V_Low";
            this.label_V_Low.Size = new System.Drawing.Size(41, 15);
            this.label_V_Low.TabIndex = 15;
            this.label_V_Low.Text = "label3";
            // 
            // label_H_Up
            // 
            this.label_H_Up.AutoSize = true;
            this.label_H_Up.Location = new System.Drawing.Point(596, 259);
            this.label_H_Up.Name = "label_H_Up";
            this.label_H_Up.Size = new System.Drawing.Size(41, 15);
            this.label_H_Up.TabIndex = 16;
            this.label_H_Up.Text = "label3";
            // 
            // label_S_Up
            // 
            this.label_S_Up.AutoSize = true;
            this.label_S_Up.Location = new System.Drawing.Point(596, 318);
            this.label_S_Up.Name = "label_S_Up";
            this.label_S_Up.Size = new System.Drawing.Size(41, 15);
            this.label_S_Up.TabIndex = 17;
            this.label_S_Up.Text = "label3";
            // 
            // label_V_Up
            // 
            this.label_V_Up.AutoSize = true;
            this.label_V_Up.Location = new System.Drawing.Point(596, 380);
            this.label_V_Up.Name = "label_V_Up";
            this.label_V_Up.Size = new System.Drawing.Size(41, 15);
            this.label_V_Up.TabIndex = 18;
            this.label_V_Up.Text = "label3";
            // 
            // imageBox4
            // 
            this.imageBox4.Location = new System.Drawing.Point(660, 239);
            this.imageBox4.Name = "imageBox4";
            this.imageBox4.Size = new System.Drawing.Size(318, 218);
            this.imageBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBox4.TabIndex = 19;
            this.imageBox4.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 471);
            this.Controls.Add(this.imageBox4);
            this.Controls.Add(this.label_V_Up);
            this.Controls.Add(this.label_S_Up);
            this.Controls.Add(this.label_H_Up);
            this.Controls.Add(this.label_V_Low);
            this.Controls.Add(this.label_S_Low);
            this.Controls.Add(this.label_H_Low);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar_V_Up);
            this.Controls.Add(this.trackBar_S_Up);
            this.Controls.Add(this.trackBar_H_Up);
            this.Controls.Add(this.trackBar_V_Low);
            this.Controls.Add(this.trackBar_S_Low);
            this.Controls.Add(this.trackBar_H_Low);
            this.Controls.Add(this.imageBox3);
            this.Controls.Add(this.imageBox2);
            this.Controls.Add(this.imageBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_H_Low)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_S_Low)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_V_Low)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_H_Up)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_S_Up)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_V_Up)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox1;
        private Emgu.CV.UI.ImageBox imageBox2;
        private Emgu.CV.UI.ImageBox imageBox3;
        private System.Windows.Forms.TrackBar trackBar_H_Low;
        private System.Windows.Forms.TrackBar trackBar_S_Low;
        private System.Windows.Forms.TrackBar trackBar_V_Low;
        private System.Windows.Forms.TrackBar trackBar_H_Up;
        private System.Windows.Forms.TrackBar trackBar_S_Up;
        private System.Windows.Forms.TrackBar trackBar_V_Up;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_H_Low;
        private System.Windows.Forms.Label label_S_Low;
        private System.Windows.Forms.Label label_V_Low;
        private System.Windows.Forms.Label label_H_Up;
        private System.Windows.Forms.Label label_S_Up;
        private System.Windows.Forms.Label label_V_Up;
        private Emgu.CV.UI.ImageBox imageBox4;
    }
}

