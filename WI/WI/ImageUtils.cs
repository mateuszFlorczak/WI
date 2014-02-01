using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace WI
{
    public static class ImageUtils
    {
        /// <summary>
        /// żeby nie było zniekształceń obrazu poprzez rozciąganie w poziomie albo w pionie
        /// </summary>
        /// <param name="bitmap">bitmapa</param>
        /// <param name="size">maksymalny wymiar</param>
        /// <returns>nowa bitmapa przescolowana z zachowaniem wymiarów</returns>
        public static Bitmap Scale(Bitmap bitmap, Size size)
        {
            Size newSize = new Size();
            if (size.Height == 0 || size.Width == 0)
            {
                newSize.Height = newSize.Width = 0;
            }
            else
            {
                decimal orginalX = bitmap.Width;
                decimal orginalY = bitmap.Height;
                decimal dx = orginalX / size.Width;
                decimal dy = orginalY / size.Height;
                decimal d = Math.Max(dx, dy);
                Func<decimal, int> decToInt = dec => (int)Math.Floor(dec);
                newSize.Width = decToInt(orginalX / d);
                newSize.Height = decToInt(orginalY / d);
            }
            return new Bitmap(bitmap, newSize);
        }
    }
}
