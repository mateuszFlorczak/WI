using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace WI
{
    class quantization
    {
        private Image img;
        private Bitmap bmp;

        public quantization(Image img)
        {
            this.img = img;
            this.bmp = new Bitmap(this.img);
            //this.bmp = new Bitmap(img);
            //this.img = (Image)this.bmp;
        }
        public Image getImg()
        {
            return img;
        }
        public void rownomierna(int przedzialow)
        {
            int d;
            List<Color> colors = new List<Color>();
            d = 256 / przedzialow;
            for (int x = 0; x < bmp.Width; ++x)
            {
                for (int y = 0; y < bmp.Height; ++y)
                {
                    Color c = Color.FromArgb(
                        (bmp.GetPixel(x, y).R / d * d + d / 2),
                        (bmp.GetPixel(x, y).G / d * d + d / 2),
                        (bmp.GetPixel(x, y).B / d * d + d / 2));
                    bmp.SetPixel(x, y, c);
                    if (!colors.Contains(c))
                        colors.Add(c);
                }
            }
            System.Console.Out.WriteLine(bmp.GetType());
            foreach (Color t in colors)
                System.Console.Out.WriteLine(t.ToString());
            img = (Image)bmp;
        }
    }
}
