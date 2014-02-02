using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.IO;

namespace MMCG3
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		#region set pixel
		[DllImport("gdi32.dll")]
		static extern uint SetPixel(System.IntPtr hdc, int X, int Y, uint crColor);
		#endregion
		
		#region members

		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem mcMenuItem;
		private System.Windows.Forms.MenuItem statMenuItem;
		private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.MenuItem loadMenuItem;
        private IContainer components;


		private Bitmap originalBitmap;
		private Bitmap displayedBitmap;
        private RGBColor[,] bitmapColors;
        private MenuItem popularityMenuItem;

		public Bitmap Bmp{get{return displayedBitmap;} set{displayedBitmap=value;}}
        public RGBColor[,] BitmapColors { get { return this.bitmapColors; } }

        int qNumber;

		#endregion

		#region constructor

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | 
				ControlStyles.AllPaintingInWmPaint, true);
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
				if (components != null) 
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
            this.components = new System.ComponentModel.Container();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.loadMenuItem = new System.Windows.Forms.MenuItem();
            this.popularityMenuItem = new System.Windows.Forms.MenuItem();
            this.mcMenuItem = new System.Windows.Forms.MenuItem();
            this.statMenuItem = new System.Windows.Forms.MenuItem();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.loadMenuItem,
            this.popularityMenuItem,
            this.mcMenuItem,
            this.statMenuItem});
            this.menuItem1.Text = "File";
            // 
            // loadMenuItem
            // 
            this.loadMenuItem.Index = 0;
            this.loadMenuItem.Text = "Load Bitmap";
            this.loadMenuItem.Click += new System.EventHandler(this.loadMenuItem_Click);
            // 
            // popularityMenuItem
            // 
            this.popularityMenuItem.Enabled = false;
            this.popularityMenuItem.Index = 1;
            this.popularityMenuItem.Text = "Popularity";
            this.popularityMenuItem.Click += new System.EventHandler(this.popularityMenuItem_Click);
            // 
            // mcMenuItem
            // 
            this.mcMenuItem.Enabled = false;
            this.mcMenuItem.Index = 2;
            this.mcMenuItem.Text = "Median Cut";
            this.mcMenuItem.Click += new System.EventHandler(this.mcMenuItem_Click);
            // 
            // statMenuItem
            // 
            this.statMenuItem.Enabled = false;
            this.statMenuItem.Index = 3;
            this.statMenuItem.Text = "Statistic";
            this.statMenuItem.Click += new System.EventHandler(this.statMenuItem_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(440, 352);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(440, 329);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Color Quantization";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region entry point
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
		#endregion

		#region events
		private void loadMenuItem_Click(object sender, System.EventArgs e)
		{
			Stream myStream;
			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			openFileDialog1.InitialDirectory = "" ;
            openFileDialog1.Filter = "JPG Files (*.jpg)|*.jpg|Bitmap Files (*.bmp)|*.bmp|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";
			openFileDialog1.FilterIndex = 1 ;
			openFileDialog1.RestoreDirectory = true ;

			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				if((myStream = openFileDialog1.OpenFile())!= null)
				{
					Image tempPattern = Bitmap.FromStream(myStream);
					myStream.Close();
					displayedBitmap = new Bitmap(tempPattern);
					originalBitmap = new Bitmap(tempPattern);
					this.pictureBox.Width = originalBitmap.Width;
					this.pictureBox.Height = originalBitmap.Height;
					this.Width = originalBitmap.Width+7;
					this.Height = originalBitmap.Height+52;
					this.mcMenuItem.Enabled = true;
					this.statMenuItem.Enabled = true;
                    this.popularityMenuItem.Enabled = true;
                    this.UpdateBitmap( this.displayedBitmap );
				}
			}
			pictureBox.Invalidate();
		}

		private void pictureBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if( this.displayedBitmap!=null )
			{
				Graphics g = e.Graphics;
				g.DrawImageUnscaled(	displayedBitmap, 
										0, 
										0, 
										displayedBitmap.Width,
										displayedBitmap.Height ); 
			}
		}

        private void mcMenuItem_Click(object sender, System.EventArgs e)
        {
            ColorNumberBox cc = new ColorNumberBox(this);
            if (cc.ShowDialog() == DialogResult.OK)
            {
                Bmp = MedianCutAlgorithm.MedianCut(Bmp,
                                                    bitmapColors,
                                                    this.qNumber);
                Invalidate(true);
            }
        }

        private void statMenuItem_Click(object sender, System.EventArgs e)
        {
            ColorNumberBox cc = new ColorNumberBox(this);
            if (cc.ShowDialog() == DialogResult.OK)
            {
                Bmp = WuAlgorithm.WuQuantization(Bmp,
                                                    BitmapColors,
                                                    this.qNumber);
                Invalidate(true);
            }

        }

        private void popularityMenuItem_Click(object sender, EventArgs e)
        {
            ColorNumberBox cc = new ColorNumberBox(this);
            if (cc.ShowDialog() == DialogResult.OK)
            {
                Bmp = PopularityAlgorithm.PopularityQuantization(Bmp,
                                                                    BitmapColors,
                                                                    this.qNumber);
                Invalidate(true);
            }
        }

		#endregion

        #region updateBitmap

        public void UpdateBitmap(Bitmap bmp)
		{
            bitmapColors = new RGBColor[bmp.Width, bmp.Height];
            int i, j;
            for (i = 0; i < bmp.Width; i++)
                for (j = 0; j < bmp.Height; j++)
                    bitmapColors[ i, j ] = new RGBColor(bmp.GetPixel( i, j ));
        }

        #endregion

        #region QNumber
        public int QNumber { set { qNumber = value; } }
        #endregion

    }


}
