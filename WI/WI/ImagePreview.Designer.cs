namespace WI
{
    partial class ImagePreview
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ImagePlace = new System.Windows.Forms.PictureBox();
            this.CurrentPixelContent = new System.Windows.Forms.Label();
            this.CurrentPixel = new System.Windows.Forms.Label();
            this.LastPixelContent = new System.Windows.Forms.Label();
            this.LastPixel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlace)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(582, 400);
            this.panel1.TabIndex = 0;
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ImagePlace);
            this.splitContainer1.Panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.splitContainer1_Panel1_MouseClick);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.CurrentPixelContent);
            this.splitContainer1.Panel2.Controls.Add(this.CurrentPixel);
            this.splitContainer1.Panel2.Controls.Add(this.LastPixelContent);
            this.splitContainer1.Panel2.Controls.Add(this.LastPixel);
            this.splitContainer1.Size = new System.Drawing.Size(582, 400);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.TabIndex = 0;
            // 
            // ImagePlace
            // 
            this.ImagePlace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImagePlace.Location = new System.Drawing.Point(0, 0);
            this.ImagePlace.Name = "ImagePlace";
            this.ImagePlace.Size = new System.Drawing.Size(400, 400);
            this.ImagePlace.TabIndex = 0;
            this.ImagePlace.TabStop = false;
            this.ImagePlace.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImagePlace_MouseMove);
            // 
            // CurrentPixelContent
            // 
            this.CurrentPixelContent.AutoSize = true;
            this.CurrentPixelContent.Location = new System.Drawing.Point(4, 78);
            this.CurrentPixelContent.Name = "CurrentPixelContent";
            this.CurrentPixelContent.Size = new System.Drawing.Size(157, 17);
            this.CurrentPixelContent.TabIndex = 3;
            this.CurrentPixelContent.Text = "<Current Pixel Content>";
            // 
            // CurrentPixel
            // 
            this.CurrentPixel.AutoSize = true;
            this.CurrentPixel.Location = new System.Drawing.Point(6, 57);
            this.CurrentPixel.Name = "CurrentPixel";
            this.CurrentPixel.Size = new System.Drawing.Size(88, 17);
            this.CurrentPixel.TabIndex = 2;
            this.CurrentPixel.Text = "Current Pixel";
            // 
            // LastPixelContent
            // 
            this.LastPixelContent.AutoSize = true;
            this.LastPixelContent.Location = new System.Drawing.Point(3, 21);
            this.LastPixelContent.Name = "LastPixelContent";
            this.LastPixelContent.Size = new System.Drawing.Size(129, 17);
            this.LastPixelContent.TabIndex = 1;
            this.LastPixelContent.Text = "<LastPixelContent>";
            // 
            // LastPixel
            // 
            this.LastPixel.AutoSize = true;
            this.LastPixel.Location = new System.Drawing.Point(3, 0);
            this.LastPixel.Name = "LastPixel";
            this.LastPixel.Size = new System.Drawing.Size(68, 17);
            this.LastPixel.TabIndex = 0;
            this.LastPixel.Text = "Last Pixel";
            // 
            // ImagePreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 468);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ImagePreview";
            this.Text = "Form2";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ImagePreview_MouseClick);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlace)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox ImagePlace;
        private System.Windows.Forms.Label CurrentPixelContent;
        private System.Windows.Forms.Label CurrentPixel;
        private System.Windows.Forms.Label LastPixelContent;
        private System.Windows.Forms.Label LastPixel;

    }
}