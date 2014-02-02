using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Expressions;

namespace WI
{
    public interface MouseMoveListener
    {
        void OnMouseMove(MouseEventArgs e);
    }

    public partial class ImagePreview : Form, MouseMoveListener
    {
        private MouseMoveListener _Listener;

        public ImagePreview(Bitmap image, MouseMoveListener listener)
        {
            InitializeComponent();
            _Listener = listener;
            ImagePlace.Image = ImageUtils.Scale(image, ImagePlace.Size);
        }

        private Func<PictureBox, Rectangle> _imageRect;
        //reflection to get private property
        private Func<PictureBox, Rectangle> ImageRect
        {
            get
            {
                if (_imageRect == null)
                {
                    var pb = Expression.Parameter(typeof(PictureBox));
                    var ir = Expression.Property(pb, "ImageRectangle");
                    var lambda = Expression.Lambda<Func<PictureBox, Rectangle>>(ir, pb);
                    _imageRect = lambda.Compile();
                }
                return _imageRect;
            }
        }

        private void CoordsToLabel(Label label, PictureBox box, int mouseX, int mouseY)
        {
            Bitmap bitmap = ImagePlace.Image as Bitmap;
            var rect = ImageRect(ImagePlace);

            int x = mouseX - rect.X;
            int y = mouseY - rect.Y;

            if (x <= 0 || y <= 0 || x >= bitmap.Width || y >= bitmap.Height)
                return;
            
            Color color = bitmap.GetPixel(x, y);
            label.Text = String.Format("x: {0} y: {1} \n R: {2}\n G: {3}\n B: {4}", x, y, color.R, color.G, color.B);
            box.Image = ImageUtils.CreateBitmap(box.Size, color);
        }

        //recive event
        public new void OnMouseMove(MouseEventArgs e)
        {
            CoordsToLabel(CurrentPixelContent, CurrentPixelColorBox, e.X, e.Y);
        }

        private void ImagePlace_MouseMove(object sender, MouseEventArgs e)
        {
            //fire the event to main window
            _Listener.OnMouseMove(e);
        }

        // DOESNT WORk! :<
        private void ImagePreview_MouseClick(object sender, MouseEventArgs e)
        {
            //CoordsToLabel(LastPixelContent, e.X, e.Y);
        }

        // DOESNT WORk! :<
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            //CoordsToLabel(LastPixelContent, e.X, e.Y);
        }

        // DOESNT WORk! :<
        private void splitContainer1_Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            //CoordsToLabel(LastPixelContent, e.X, e.Y);
        }

        
    }
}
