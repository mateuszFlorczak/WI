namespace FunctionFactory.Controls
{
    partial class IntSlider
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.paramName = new System.Windows.Forms.Label();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.paramValueDisplay = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // paramName
            // 
            this.paramName.AutoSize = true;
            this.paramName.Location = new System.Drawing.Point(-3, 0);
            this.paramName.Name = "paramName";
            this.paramName.Size = new System.Drawing.Size(102, 17);
            this.paramName.TabIndex = 0;
            this.paramName.Text = "<ParamName>";
            // 
            // trackBar
            // 
            this.trackBar.Location = new System.Drawing.Point(0, 20);
            this.trackBar.Maximum = 32;
            this.trackBar.Minimum = 2;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(268, 56);
            this.trackBar.TabIndex = 1;
            this.trackBar.Value = 2;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // paramValueDisplay
            // 
            this.paramValueDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.paramValueDisplay.AutoSize = true;
            this.paramValueDisplay.Location = new System.Drawing.Point(167, 0);
            this.paramValueDisplay.Name = "paramValueDisplay";
            this.paramValueDisplay.Size = new System.Drawing.Size(101, 17);
            this.paramValueDisplay.TabIndex = 2;
            this.paramValueDisplay.Text = "<ParamValue>";
            this.paramValueDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IntSlider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.paramValueDisplay);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.paramName);
            this.Name = "IntSlider";
            this.Size = new System.Drawing.Size(268, 75);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label paramName;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Label paramValueDisplay;
    }
}
