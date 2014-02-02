using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace MMCG3
{
	/// <summary>
	/// Summary description for ColorQuantifier.
	/// </summary>
	public class MedianCutAlgorithm
	{

		public static Bitmap MedianCut(Bitmap bmp, 
                                       RGBColor[,] bmpC, 
                                       int maxColors )
		{

            int[,,] colorCube = 
                new int[
                RGBColor.ColorNumber, 
                RGBColor.ColorNumber, 
                RGBColor.ColorNumber];

			CountColors( bmpC, colorCube );
			ArrayList cuboids = new ArrayList();
            cuboids.Add(
                new MCRGBCuboid(new RGBColor(0,0,0), 
                new RGBColor(
                RGBColor.ColorNumber, 
                RGBColor.ColorNumber,
                RGBColor.ColorNumber),
                colorCube)
                );

            RGBCuboidPair pair;
            MCRGBCuboid c;
			while(cuboids.Count<maxColors)
            {
                try
                {
                    c = FindMaximalCuboid(cuboids);
                }
                catch
                {
                    MessageBox.Show("Cannot divide into more than "+cuboids.Count+" colors.");
                    break;
                }
                pair = c.Divide();
                cuboids.Remove(c);
                cuboids.Add(pair.C1);
                cuboids.Add(pair.C2);
			}

            int i, j;
            MCRGBCuboid cub;
            RGBColor midC;
			for( i=0; i<bmp.Width; i++ )
				for( j=0; j<bmp.Height; j++ )
				{
                    cub = FindCuboid(cuboids, bmpC[i, j].R, bmpC[i, j].G, bmpC[i, j].B); // returns null at 0, 40, 106
                    midC = cub.MidColor;
					bmp.SetPixel( i, j, Color.FromArgb(
                        (int)midC.RNonscaled,
                        (int)midC.GNonscaled,
                        (int)midC.BNonscaled));
				}
            return bmp;
		}


		private static void CountColors( RGBColor[,] bmpC,  int[,,] colorCube  )
		{
			int i,j;
			RGBColor c;
            for (i = 0; i < bmpC.GetLength(0); i++)
                for (j = 0; j < bmpC.GetLength(1); j++)
                {
                    c = bmpC[i, j];
                    (colorCube[c.R, c.G, c.B])++;
                }
		}


        private static MCRGBCuboid FindMaximalCuboid(ArrayList cuboids)
        {
            int maxWeight = Int32.MinValue;
            MCRGBCuboid maxC = null;
            foreach (MCRGBCuboid c in cuboids)
            {
                if (c.Weight > maxWeight
                    && (c.DR > 1
                    || c.DG > 1
                    || c.DB > 1))
                {
                    maxC = c;
                    maxWeight = c.Weight;
                }
            }
            if (maxC == null)
            {
                throw new AlgException(AlgException.Cause.CannotDivideintoMoreColors);
            }
            return maxC;
        }


        private static MCRGBCuboid FindCuboid(ArrayList cuboids, ulong r, ulong g, ulong b)
        {
            foreach (MCRGBCuboid c in cuboids)
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
			s+=b.ToString("X");
			if(g<10) s+="0";
			s+=g.ToString("X");
			if(r<10) s+="0";
			s+=r.ToString("X");
			return Int32.Parse(s, System.Globalization.NumberStyles.HexNumber);
		}
		#endregion

	}
}
