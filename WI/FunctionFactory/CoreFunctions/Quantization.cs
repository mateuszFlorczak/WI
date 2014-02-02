using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunctionFactory.Controls;
using FunctionFactory.Interfaces;

namespace FunctionFactory.CoreFunctions
{
    public class Quantization : IFunction
    {
        IntSlider _przedzialow = new IntSlider("Liczba przedziałów", 2, 4, 8, 16, 32);

        public Bitmap Calculate(Bitmap orginal)
        {
            return rownomierna_raw(orginal, _przedzialow.Value);
        }

        public IList<IFunctionParamControl> ParamList
        {
            get { return new IFunctionParamControl[] { _przedzialow }; }
        }

        public string EnglishName
        {
            get { return "Quantization"; }
        }

        public string PolishName
        {
            get { return "Równomierna"; }
        }

        public Bitmap rownomierna_raw(Bitmap bitmap, int przedzialow)
        {
            int d;
            List<Color> colors = new List<Color>();
            d = 256 / przedzialow;
            for (int x = 0; x < bitmap.Width; ++x)
            {
                for (int y = 0; y < bitmap.Height; ++y)
                {
                    Color c = Color.FromArgb(
                        (bitmap.GetPixel(x, y).R / d * d + d / 2),
                        (bitmap.GetPixel(x, y).G / d * d + d / 2),
                        (bitmap.GetPixel(x, y).B / d * d + d / 2));
                    bitmap.SetPixel(x, y, c);
                    if (!colors.Contains(c))
                        colors.Add(c);
                }
            }
            System.Console.Out.WriteLine(bitmap.GetType());
            foreach (Color t in colors)
                System.Console.Out.WriteLine(t.ToString());
            return bitmap;
        }
    }
}
