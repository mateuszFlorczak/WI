using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;
using MMCG3;

namespace FunctionFactory.ZZ
{
    class ZZ
    {
        public static MMCG3.RGBColor[,] UpdateBitmap(Bitmap bmp)
        {
            MMCG3.RGBColor[,] bitmapColors = new MMCG3.RGBColor[bmp.Width, bmp.Height];
            int i, j;
            for (i = 0; i < bmp.Width; i++)
                for (j = 0; j < bmp.Height; j++)
                    bitmapColors[i, j] = new MMCG3.RGBColor(bmp.GetPixel(i, j));
            return bitmapColors;
        }
    }
}
