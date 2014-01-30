using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            ImagePlace.Image = new Bitmap(image, ImagePlace.Size);
        }

        private void CoordsToLabel(Label label, int x, int y)
        {
            Bitmap bitmap = ImagePlace.Image as Bitmap;
            if (x > bitmap.Width || y > bitmap.Height)
                return;
            Color color = bitmap.GetPixel(x, y);
            label.Text = String.Format("x: {0} y: {1} \n r:{2} g:{3} b:{4}", x, y, color.R, color.G, color.B);
        }

        //recive event
        public new void OnMouseMove(MouseEventArgs e)
        {
            CoordsToLabel(CurrentPixelContent, e.X, e.Y);
        }

        private void ImagePlace_MouseMove(object sender, MouseEventArgs e)
        {
            //fire the event to main window
            _Listener.OnMouseMove(e);
        }

        // DOESNT WORk! :<
        private void ImagePreview_MouseClick(object sender, MouseEventArgs e)
        {
            CoordsToLabel(LastPixelContent, e.X, e.Y);
        }

        // DOESNT WORk! :<
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            CoordsToLabel(LastPixelContent, e.X, e.Y);
        }

        // DOESNT WORk! :<
        private void splitContainer1_Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            CoordsToLabel(LastPixelContent, e.X, e.Y);
        }

        
    }
}
