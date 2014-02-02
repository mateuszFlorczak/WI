using System;
using System.Collections.Generic;
using System.Text;

namespace MMCG3
{
    public class RGBColorPrecalcWu
    {
        RGBColor rgbCol;
        double p;

        /// <summary>
        /// number of occurencies of the color in the quantized bitmap
        /// </summary>
        int count;

        /// <summary>
        /// precalculated color; c*P(c)
        /// </summary>
        RGBColor cpc;

        /// <summary>
        /// sum of c^2 * P(c)
        /// </summary>
        double sumCSqPC;

        /// <summary>
        /// c^2*P(c)
        /// </summary>
        double cSqPC;


        /// <summary>
        ///sum of P(ci)over ci in cube(0,c) 
        /// </summary>
        double sumPC;

        /// <summary>
        ///sum of c*P(ci)over ci in cube(0,c) 
        /// </summary>
        RGBColor sumCPC;

        int totalPixels;




        public RGBColorPrecalcWu(
            RGBColor color, 
            int _totalPixels)
        {
            count = 0;

            rgbCol = color;
            this.totalPixels = _totalPixels;
            this.SumCSqPc = 0.0;
            this.SumCPC = new RGBColor(0, 0, 0);
            this.SumPC = 0.0;
        }

        public ulong R { get { return rgbCol.R; } }
        public ulong G { get { return rgbCol.G; } }
        public ulong B { get { return rgbCol.B; } }

        /// <summary>
        /// number of occurrencies of this color in the quantized bitmap
        /// </summary>
        private int Count { get { return count; } }

        /// <summary>
        /// c^2*P(c)
        /// </summary>
        public double CSQPC { get { return this.cSqPC; } }

        /// <summary>
        /// Sum(c^2*P(c)) for c in Cuboid((0,0,0), c)
        /// </summary>
        public double SumCSqPc { get { return this.sumCSqPC; } set { this.sumCSqPC = value; } }



        /// <summary>
        /// c*P(c)
        /// </summary>
        public RGBColor CPC { get { return this.cpc; } }

        public double SumPC { set { sumPC = value; } get { return sumPC; } }
        public RGBColor SumCPC { set { sumCPC = value; } get { return sumCPC; } }
        public double P { get { return p; } }

        /// <summary>
        /// Appropriate RGBColor object
        /// </summary>
        public RGBColor RGBCol { get { return this.rgbCol; } }

        public void Increment()
        {
            count++;
        }

        public void performPrecalculations()
        {
            p = ((double)(count*100*OrderOf(totalPixels)))/totalPixels;
            this.cpc = p*rgbCol;
            this.cSqPC = (this.RGBCol*this.RGBCol)*p;
            
        }

        private int OrderOf(int n)
        {
            int result = 1;
            while (n > 1)
            {
                result *= 10;
                n /= result;
            }
            return result;
        }

    }
}
