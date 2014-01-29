using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FunctionFactory;

namespace WI
{
    public partial class MainWindow : Form
    {
        IList<Image> _ImageListOrginal = new List<Image>();
        Image _MainImageOrginal;

        Factory _FunctionFactory = new Factory();

        public MainWindow()
        {
            InitializeComponent();
            ImageListView.LargeImageList = ImageList;
            
            //dummy data
            var img = Image.FromFile("D:/Downloads/rar.png");
            var img2 = Image.FromFile("D:/Downloads/Untitled drawing.png");
            AddImageToImageListView(img);
            AddImageToImageListView(img2);
            //AddImageToImageListView(img);
            //AddImageToImageListView(img2);


            foreach (var function in _FunctionFactory.Functions)
                this.splitContainer2.Panel1.Controls.Add(IFunctionToControl(function));
        }

        private Control IFunctionToControl(IFunction function)
        {
            Button button = new Button();
            button.Width = 255;
            button.Height = 25;
            button.Text = String.Format("{0}, {1}", function.PolishName, function.EnglishName);
            button.Click += (s, e) => CreateFormAndShow(function);
            return button;
        }

        private void CreateFormAndShow(IFunction function)
        {
            Bitmap bitmap = new Bitmap(_MainImageOrginal);
            IForm form = _FunctionFactory.GetDefaultForm();
            foreach (var control in function.ParamList)
                form.AddControl(control.UserControl);
            form.OkButton += (s, e) => AddImageToImageListView(function.Calculate(bitmap));
            form.ImagePreview = new Bitmap(bitmap);
            form.Show();
        }

        private void AddImageToImageListView(Image image)
        {
            ImageList.Images.Add(image);
            _ImageListOrginal.Add(image);
            var item = new ListViewItem();
            item.ImageIndex = ImageListView.Items.Count;
            ImageListView.Items.Add(item);
            SetMainImage(image);
        }

        private void SetMainImage(Image image)
        {
            _MainImageOrginal = image;
            MainImage.Image = new Bitmap(image, MainImage.Size);
        }


        #region events
        private void ImageListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            SetMainImage(_ImageListOrginal[e.ItemIndex]);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainWindow_SizeChanged(object sender, EventArgs e)
        {
            if (_MainImageOrginal != null)
                SetMainImage(_MainImageOrginal);
        }

        #endregion

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenNewImage.ShowDialog();
            AddImageToImageListView(Image.FromFile(OpenNewImage.FileName));
        }
    }
}
