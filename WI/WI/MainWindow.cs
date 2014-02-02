using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FunctionFactory;


namespace WI
{
    public partial class MainWindow : Form, MouseMoveListener
    {
        IList<Bitmap> _ImageListOrginal = new List<Bitmap>();
        Bitmap _MainImageOrginal;
        string _MainImageName; //:<
        IList<ImagePreview> _PreviewList = new List<ImagePreview>();

        public MainWindow()
        {
            InitializeComponent();
            ImageListView.LargeImageList = ImageList;
            
            //dummy data
            //var img = Image.FromFile("D:/Downloads/rar.png");
            //var img2 = Image.FromFile("D:/Downloads/Untitled drawing.png");
            //AddImageToImageListView(img, "rar");
            //AddImageToImageListView(img2, "Untitled drawing");


            foreach (var function in Factory.Functions)
                this.FunctionPlace.Controls.Add(IFunctionToControl(function));
        }

        //tworzenie przycisku na penelu po prawej na podstawie interfejsu IFunction
        private Control IFunctionToControl(IFunction function)
        {
            Button button = new Button();
            button.Width = 255;
            button.Height = 25;
            button.Text = String.Format("{0}, {1}", function.PolishName, function.EnglishName);
            button.Click += (s, e) => CreateFormAndShow(function);
            return button;
        }

        //tworzenie okienka do ustawienia parametrów funkcji
        private void CreateFormAndShow(IFunction function)
        {
            if (_MainImageOrginal == null)
                return;
            string oldName = _MainImageName;
            string newName = String.Format("{0}_{1}", oldName, function.PolishName);
            Bitmap bitmap = new Bitmap(_MainImageOrginal);
            IForm form = new EmptyForm();
            foreach (var control in function.ParamList)
                form.AddControl(control.UserControl);
            form.OkButton += (s, e) => AddImageToImageListView(function.Calculate(bitmap), newName);
            form.SetImagePreview(bitmap);
            form.Show();
        }

        private void AddImageToImageListView(Image image, string name)
        {
            Bitmap bitmap = new Bitmap(image);
            ImageList.Images.Add(name, bitmap);
            _ImageListOrginal.Add(bitmap);
            var item = new ListViewItem();
            item.ImageIndex = ImageListView.Items.Count;
            item.Text = name;
            ImageListView.Items.Add(item);
            //SetMainImage(bitmap);
        }

        private void RemoveImageFromListView(int index)
        {
            if (ImageListView.FocusedItem.Index == index)
                ClearMainImage();
            ImageList.Images.RemoveAt(index);
            _ImageListOrginal.RemoveAt(index);
            ImageListView.Items.RemoveAt(index);
        }

        private void RemoveAllImagesFromListView()
        {
            ImageListView.Items.Clear();
            ImageList.Images.Clear();
            _ImageListOrginal.Clear();
            ClearMainImage();
        }

        private void SetMainImage(Bitmap image, string name)
        {
            _MainImageOrginal = image;
            _MainImageName = name;
            MainImage.Image = ImageUtils.Scale(image, MainImage.Size);
        }

        private void ClearMainImage()
        {
            _MainImageOrginal = null;
            _MainImageName = null;
            MainImage.Image = null;
        }


        #region events
        private void ImageListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            int index = e.ItemIndex;
            if (index >= 0 && index < _ImageListOrginal.Count)
                SetMainImage(_ImageListOrginal[index], ImageListView.Items[index].Text);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainWindow_SizeChanged(object sender, EventArgs e)
        {
            if (_MainImageOrginal != null)
                SetMainImage(_MainImageOrginal, _MainImageName);
        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenNewImage.ShowDialog();
            string fileName = OpenNewImage.FileName;
            AddImageToImageListView(Image.FromFile(fileName), fileName);
        }

        private void ImageListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //block duplicate windows?
            int index = ImageListView.FocusedItem.Index;
            ImagePreview preview = new ImagePreview(_ImageListOrginal[index], this);
            _PreviewList.Add(preview);
            preview.Show();
        }

        private void SaveCurrentImage()
        {
            if (ImageListView.FocusedItem == null)
                return;
            int index = ImageListView.FocusedItem.Index;
            SaveImageDialog.ShowDialog();
            _ImageListOrginal[index].Save(SaveImageDialog.FileName);
        }

        #endregion

        public new void OnMouseMove(MouseEventArgs e)
        {
            foreach (var preview in _PreviewList)
                preview.OnMouseMove(e);
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenNewImageFolder.ShowDialog();
            //search pattern?
            foreach (var fileName in Directory.GetFiles(OpenNewImageFolder.SelectedPath))
            {
                try
                {
                    //move to background?
                    AddImageToImageListView(Image.FromFile(fileName), fileName);
                }
                catch { } //not image file, ignore
            }
        }

        private void RemoveAllImages_Click(object sender, EventArgs e)
        {
            RemoveAllImagesFromListView();
        }

        private void RemoveImage_Click(object sender, EventArgs e)
        {
            if (ImageListView.FocusedItem == null)
                return;
            RemoveImageFromListView(ImageListView.FocusedItem.Index);
        }

        private void SaveImage_Click(object sender, EventArgs e)
        {
            SaveCurrentImage();
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCurrentImage();
        }
    }
}
