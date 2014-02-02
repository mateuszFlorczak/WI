using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace MMCG3
{
	/// <summary>
	/// Summary description for colorChooser.
	/// </summary>
	public class ColorNumberBox : System.Windows.Forms.Form
	{
		#region members
		private System.Windows.Forms.TextBox ColorNumber;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private Form1 mainForm;
		#endregion
	
		#region constructor
		public ColorNumberBox( Form1 f )
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			mainForm = f;

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}
		#endregion

		#region dispose
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.ColorNumber = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ColorNumber
            // 
            this.ColorNumber.Location = new System.Drawing.Point(32, 32);
            this.ColorNumber.Name = "ColorNumber";
            this.ColorNumber.Size = new System.Drawing.Size(56, 20);
            this.ColorNumber.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 64);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Max color number";
            // 
            // ColorNumberBox
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(120, 94);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ColorNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "ColorNumberBox";
            this.Text = "colorChooser";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        #region Events
        private void button1_Click(object sender, System.EventArgs e)
        {
            int colN = Int32.Parse(this.ColorNumber.Text);
            mainForm.QNumber = colN;
            this.DialogResult = DialogResult.OK;
            Close();
        } 
        #endregion

	}
}
