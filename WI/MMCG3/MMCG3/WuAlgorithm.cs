using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace MMCG3
{
    public class WuAlgorithm
    {
        private static RGBColor[,] colorTable;

        public static Bitmap WuQuantization(Bitmap bmp, RGBColor[,] _colorTable, int quantNumber )
        {
            ArrayList cuboids = new ArrayList();
            colorTable = _colorTable;
            PrecalculatedWu precalc = new PrecalculatedWu( colorTable );
            cuboids.Add(
                new WuRGBCuboid(
                new RGBColor(0, 0, 0), 
                new RGBColor(
                RGBColor.ColorNumber - 1,
                RGBColor.ColorNumber - 1,
                RGBColor.ColorNumber - 1), precalc, null));
            RGBCuboidPair minVarCuboidPair = null;
            WuRGBCuboid maxVarC = null;
            while (cuboids.Count  < quantNumber  )
            {
                try
                {
                   maxVarC = FindMaxVarianceCuboid(cuboids);
                }
                catch (AlgException e)
                {
                    if (e.ExceptionCause == AlgException.Cause.CannotDivideintoMoreColors)
                    {
                        MessageBox.Show("I can only divide into " + cuboids.Count + " colors");
                        break;
                    }
                }
                cuboids.Remove(maxVarC);
                minVarCuboidPair = maxVarC.FindMinimumMSEDivision();
                cuboids.Add(minVarCuboidPair.C1);
                cuboids.Add(minVarCuboidPair.C2);

            }


            int red, green, blue;
            RGBColor m;
            RGBColor c;
            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                {
                    c = colorTable[i,j];
                    m = FindCuboid(cuboids, c.R, c.G, c.B).MidColor;
                    red = (int)(m.RNonscaled) ;
                    green = (int)(m.GNonscaled);
                    blue = (int)(m.BNonscaled);
                    bmp.SetPixel(i,j, Color.FromArgb(red, green, blue));
                }
            return bmp;
        }

        private static WuRGBCuboid FindMaxVarianceCuboid(ArrayList cuboids)
        {
            if(cuboids.Count==0)
            {
                throw new AlgException(AlgException.Cause.CuboidsListEmpty);
            }
            int i;
            WuRGBCuboid result = cuboids[0] as WuRGBCuboid;
            double maxVariance = double.MinValue;  
            for (i = 0; i < cuboids.Count; i++)
            {
                WuRGBCuboid c = cuboids[i] as WuRGBCuboid;
                if ( (c.Variance > maxVariance) 
                    && ( c.DR>1
                    || c.DG>1
                    || c.DB>1) )
                {
                    maxVariance = c.Variance;
                    result = c;
                }
            }

            if (result.DR <= 1 && result.DG <= 1 && result.DB <= 1)
            {
                throw new AlgException(AlgException.Cause.CannotDivideintoMoreColors);
            }

            return result;
        }


        private static WuRGBCuboid FindCuboid(ArrayList cuboids, ulong r, ulong g, ulong b)
        {
            foreach (WuRGBCuboid c in cuboids)
            {
                if (
                    c.RedStart <= r
                    && c.RedEnd >= r
                    && c.GreenStart <= g
                    && c.GreenEnd >= g
                    && c.BlueStart <= b
                    && c.BlueEnd >= b)
                {
                    return c;
                }
            }
            throw new AlgException(AlgException.Cause.CuboidNotFound);
        }

        #region RGB
        public static int RGB(int r, int g, int b)
        {
            String s = "";
            s += b.ToString("X");
            if (g < 10) s += "0";
            s += g.ToString("X");
            if (r < 10) s += "0";
            s += r.ToString("X");
            return Int32.Parse(s, System.Globalization.NumberStyles.HexNumber);
        }
        #endregion
    }
}
