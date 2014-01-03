using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace WI
{
    public partial class Form1 : Form
    {
        List<FileInfo> files = new List<FileInfo>();
        public Form1()
        {
            InitializeComponent();
            this.listView1.View = View.LargeIcon;
            //this.imageList1.ImageSize = new Size(256, 256);
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.LargeImageList.ImageSize = new Size(32, 32);
        }

        private void open_Click(object sender, EventArgs e)
        {
            //backgroundWorker.RunWorkerAsync();
            this.folderBrowserDialog1.ShowDialog();
            try
            {
                this.files.Clear();
                this.listView1.Clear();
                this.imageList1.Images.Clear();
                DirectoryInfo dir = new DirectoryInfo(this.folderBrowserDialog1.SelectedPath);
                this.progressBar1.Maximum = dir.GetFiles().GetLength(0);
                this.progressBar1.Step = 1;
                this.progressBar1.Value = 0;
                this.progressBar1.Show();
                int j = 0;
                foreach (FileInfo file in dir.GetFiles())
                {
                    try
                    {
                        this.imageList1.Images.Add(Image.FromFile(file.FullName));
                        this.files.Add(file);
                        ListViewItem item = new ListViewItem();
                        item.ImageIndex = j;
                        this.listView1.Items.Add(item);
                        j++;
                    }
                    catch
                    {
                        Console.WriteLine("This is not an image file");
                    }
                    ++this.progressBar1.Value;
                }
                this.progressBar1.Hide();
            }
            catch
            {
            }
        }

        private void openImage_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.ShowDialog();
            try
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
            catch (IOException)
            { 
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            

            //PictureBox picture = new PictureBox();
            pictureBox1.Image = this.imageList1.Images[this.listView1.FocusedItem.Index]; //this.listView1.GetItemAt(this.listView1.FocusedItem.Position.X, this.listView1.FocusedItem.Position.Y);
            //picture.Size = new Size(150, 250);
            //picture.Show();
            System.Console.Out.WriteLine(files[this.listView1.FocusedItem.Index].FullName);
            Form2 form = new Form2(files[this.listView1.FocusedItem.Index].FullName);
            form.Show();
            System.Console.Out.WriteLine("dziala");
        }
    }
}
