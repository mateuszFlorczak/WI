using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;

namespace MMCG3
{
    public class PopularityAlgorithm
    {
        public static Bitmap PopularityQuantization(
            Bitmap bmp, 
            RGBColor[,] _colorTable, 
            int quantNumber)
        {
            int[, ,] histogram = 
                new int[
                RGBColor.ColorNumber, 
                RGBColor.ColorNumber, 
                RGBColor.ColorNumber];
            // counting colors in the bitmap
            RGBColor col;

            ArrayList activeColors = new ArrayList();

            for( int i=0;i< _colorTable.GetLength(0); i++)
                for (int j = 0; j < _colorTable.GetLength(1); j++)
                {
                    col = _colorTable[i, j];
                    if (histogram[col.R, col.G, col.B] == 0)
                    {
                        activeColors.Add(new ColorEntry(col));
                    }
                    histogram[col.R, col.G, col.B]++;
                }

            // setting Count properties in elements of activeColors

            foreach (ColorEntry ce in activeColors)
            {
                col = ce.Color;
                ce.Count = histogram[col.R, col.G, col.B];
            }

            activeColors.Sort();

            int cnt;
            if (quantNumber < activeColors.Count)
            {
                cnt = activeColors.Count;
                activeColors.RemoveRange(quantNumber, cnt - quantNumber); 
            }

            RGBColor closestCol;
           for( int i=0;i< _colorTable.GetLength(0); i++)
               for (int j = 0; j < _colorTable.GetLength(1); j++)
               {
                   closestCol = 
                       FindClosestColor(activeColors, _colorTable[i, j]);
                   bmp.SetPixel(i, j, Color.FromArgb(
                                        closestCol.RNonscaled, 
                                        closestCol.GNonscaled, 
                                        closestCol.BNonscaled));

               }

            return bmp;
        }

        private static RGBColor FindClosestColor(
            ArrayList activeColors, 
            RGBColor c)
        {
            ulong dist;
            ulong minDist = Int64.MaxValue;
            RGBColor ic; // iterated color
            RGBColor foundColor = null;
            foreach (ColorEntry ce in activeColors)
            {
                ic = ce.Color;
                dist = 
                    (ic.R - c.R) * (ic.R - c.R) 
                    + (ic.G - c.G) * (ic.G - c.G) 
                    + (ic.B - c.B) * (ic.B - c.B);
                if (dist < minDist)
                {
                    minDist = dist;
                    foundColor = ic;
                }
            }
            return foundColor;
        }

    }


    /// <summary>
    /// class designed to sort colors in order of their popularity
    /// </summary>
    class ColorEntry : IComparable
    {

        RGBColor col;
        int cnt;

        public ColorEntry(RGBColor _col)
        {
            col = _col;
        }

        /// <summary>
        /// How many times the color col occured in quantized bitmap
        /// </summary>

        public int Count { get { return cnt; } set { cnt = value; } }
        public RGBColor Color { get { return col; } }

        public int CompareTo(Object o)
        {
            ColorEntry ce;
            ce = (ColorEntry)o;
            if (this.Count == ce.Count)
                return 0;
            if (this.Count < ce.Count)
                return 1;
            return -1;
        }



    }



}
