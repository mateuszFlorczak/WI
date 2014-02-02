using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MMCG3
{
    public class RGBColor
    {
        ulong r;
        ulong g;
        ulong b;

        double rd;
        double gd;
        double bd;

        public const int ColorNumber = 33;
        public const double divisionCoef = 255.0 / ((double)ColorNumber-1.0);

        public RGBColor(Color c)
        {
            r = (ulong)(c.R / divisionCoef);
            g = (ulong)(c.G / divisionCoef);
            b = (ulong)(c.B / divisionCoef);
            rd = c.R / divisionCoef;
            gd = c.G / divisionCoef;
            bd = c.B / divisionCoef;
        }


        public RGBColor(ulong red, ulong green, ulong blue)
        {

            r = red;
            rd = (int)red;
            g = green;
            gd = (int)green;
            b = blue;
            bd = (int)blue;
        }

        public ulong R { get { return r; }  }
        public ulong G { get { return g; } }
        public ulong B { get { return b; } }


        public int RNonscaled { get { return  ((int)(rd * divisionCoef)) % 256; } }
        public int GNonscaled { get { return ((int)(gd * divisionCoef)) % 256; } }
        public int BNonscaled { get { return ((int)(bd * divisionCoef)) % 256; } }

        public static RGBColor operator-(RGBColor c1, RGBColor c2)
        {
            return new RGBColor(c1.R - c2.R, c1.G - c2.G, c1.B - c2.B);
        }

        public static double operator *(RGBColor c1, RGBColor c2)
        {
            return c1.R * c2.R + c1.G * c2.G + c1.B * c2.B;
        }

        public static RGBColor operator /(RGBColor c, double d)
        {
            return new RGBColor((ulong)(((double)c.R) / d), (ulong)(((double)c.G) / d), (ulong)(((double)c.B) / d));
        }

        public static RGBColor operator *(RGBColor c1, double d)
        {
            return new RGBColor((ulong)(c1.R * d), (ulong)(c1.G * d), (ulong)(c1.B * d));
        }

        public static RGBColor operator *(double d, RGBColor c1)
        {
            return new RGBColor((ulong)(c1.R * d), (ulong)(c1.G * d), (ulong)(c1.B * d));
        }

        public static RGBColor operator +( RGBColor c1, RGBColor c2)
        {
            return new RGBColor(c1.R + c2.R , c1.G+c2.G, c1.B+c2.B);
        }

        public static bool operator <(RGBColor c1, RGBColor c2)
        {
            return (c1.R < c2.R) || (c1.G < c2.G) || (c1.B < c2.B);
        }
        public static bool operator >(RGBColor c1, RGBColor c2)
        {
            return (c1.R > c2.R) || (c1.G > c2.G) || (c1.B > c2.B);
        }

    }
}
