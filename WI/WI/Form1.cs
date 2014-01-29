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
        List<quantization> quantized = new List<quantization>();
        List<int> valid_przedzialy = new List<int>();

        public Form1()
        {
            InitializeComponent();
            this.listView1.View = View.LargeIcon;
            //this.imageList1.ImageSize = new Size(256, 256);
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.LargeImageList.ImageSize = new Size(32, 32);
            for(int i = 1; i < 32; i*=2)
                valid_przedzialy.Add(i);
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
        
        private void add_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                quantized.Add(new quantization(pictureBox1.Image));
                this.imageList1.Images.Add(pictureBox1.Image);
                ListViewItem item = new ListViewItem();
                item.ImageIndex = quantized.Count() - 1;
                this.listView1.Items.Add(item);
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (listView1.FocusedItem != null)
            {
                quantized.RemoveAt(listView1.FocusedItem.Index);
                imageList1.Images.RemoveAt(listView1.FocusedItem.Index);
                listView1.Items.RemoveAt(listView1.FocusedItem.Index);
                listView1.Refresh();
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Form2 form = new Form2(quantized[listView1.FocusedItem.Index].getImg());
            form.Show();
        }

        private void przedzialy_ValueChanged(object sender, EventArgs e)
        {
            if (!valid_przedzialy.Contains(przedzialy.Value))
                if (przedzialy.Value > 32)
                    przedzialy.Value = 32;
                else if (przedzialy.Value > 16 && przedzialy.Value < 32)
                    przedzialy.Value = 16;
                else if (przedzialy.Value > 8 && przedzialy.Value < 16)
                    przedzialy.Value = 8;
                else if (przedzialy.Value > 4 && przedzialy.Value < 8)
                    przedzialy.Value = 4;
                else if (przedzialy.Value == 3)
                    przedzialy.Value = 2;

            przedzial_value.Text = przedzialy.Value.ToString();
        }

        private void kwantyzacja_rownomierna_Click(object sender, EventArgs e)
        {
            if (listView1.FocusedItem != null)
            {
                quantized.Add(new quantization(quantized[listView1.FocusedItem.Index].getImg()));
                quantized.Last().rownomierna(przedzialy.Value);
                Form2 form = new Form2(quantized.Last().getImg());
                form.Show();
                imageList1.Images.Add(quantized.Last().getImg());
                ListViewItem item = new ListViewItem();
                item.ImageIndex = quantized.Count() - 1;
                this.listView1.Items.Add(item);
                listView1.Refresh();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var ff = new FunctionFactory.Factory();
            var form = ff.GetDefaultForm();
            foreach (var p in ff.Functions[0].ParamList)
            {
                form.AddControl(p.UserControl);
            }
            form.OkButton += (s, ee) => ff.Functions[0].Calculate(null);
            form.Show();
        }

        /*private void open_Click(object sender, EventArgs e)
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
        }*/
    }
}
