using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace MMCG3
{
    public class PrecalculatedWu
    {
        RGBColorPrecalcWu[,,] precalcs;
        RGBColor minColor = new RGBColor(255,255,255);
        RGBColor maxColor = new RGBColor(0,0,0);


        public PrecalculatedWu(RGBColor[,] colorTable)
        {
            ArrayList activeCalcs = new ArrayList();
            ulong r, g, b;
            precalcs = new RGBColorPrecalcWu[RGBColor.ColorNumber, RGBColor.ColorNumber, RGBColor.ColorNumber];
            int i, j;
            int totPix = colorTable.Length;



            int[] squares = new int[RGBColor.ColorNumber];
            for (i = 0; i < RGBColor.ColorNumber; i++)
            {
                squares[i] = i * i;
            }

            for (r = 0; r < RGBColor.ColorNumber; r++)
                for (g = 0; g < RGBColor.ColorNumber; g++)
                    for (b = 0; b < RGBColor.ColorNumber; b++)
                        precalcs[r, g, b] = new RGBColorPrecalcWu( new RGBColor(r,g,b), totPix);

                /*initialize densities, find min and max color*/
                for (i = 0; i < colorTable.GetLength(0); i++)
                    for (j = 0; j < colorTable.GetLength(1); j++)
                    {
                        r = (ulong)colorTable[i, j].R;
                        g = (ulong)colorTable[i, j].G;
                        b = (ulong)colorTable[i, j].B;

                        if (r > maxColor.R || g > maxColor.G || b > maxColor.B)
                        {
                            maxColor = new RGBColor(r, g, b);
                        }

                        if (r < minColor.R || g < minColor.G || b < minColor.B)
                        {
                            minColor = new RGBColor( r,  g,  b);
                        }

                        if (precalcs[r, g, b] == null)
                        {
                            precalcs[r, g, b] = new RGBColorPrecalcWu( colorTable[i,j], totPix);
                            activeCalcs.Add(precalcs[r, g, b]);
                        }

                        precalcs[r, g, b].Increment();
                    }


            RGBColor black = new RGBColor(0, 0, 0);
            
            /*create remaining precalc objects and calcylate sums cpc and pc*/
            RGBColor linergb;
            double[] area = new double[RGBColor.ColorNumber];
            RGBColor[] areargb = new RGBColor[RGBColor.ColorNumber];
            double line2, line;
            double[] area2 = new double[RGBColor.ColorNumber];

            for (r = 1; r < RGBColor.ColorNumber; r++)
            {
                for (i = 0; i < RGBColor.ColorNumber; i++)
                {
                    area2[i] = 0.0;
                    area[i] = 0;
                    areargb[i] = black;
                }
                for (g = 1; g < RGBColor.ColorNumber; g++)
                    {
                        line2 = 0.0;
                        line = 0;
                        linergb = black;
                        for (b = 1; b < RGBColor.ColorNumber; b++)
                        {
                            if (precalcs[r, g, b] == null)
                            {
                                precalcs[r, g, b] =
                                                        new RGBColorPrecalcWu(
                                                        new RGBColor( r,  g,  b),
                                                        totPix);
                            }

                            precalcs[r, g, b].performPrecalculations();

                            line += precalcs[r, g, b].P;
                            linergb += precalcs[r, g, b].CPC;
                            line2 += precalcs[r, g, b].CSQPC;

                            area[b] += line;
                            areargb[b] += linergb;
                            area2[b] += line2;

                            precalcs[r, g, b].SumPC = precalcs[r - 1, g, b].SumPC + area[b];
                            precalcs[r, g, b].SumCPC = precalcs[r - 1, g, b].SumCPC + areargb[b];
                            precalcs[r, g, b].SumCSqPc = precalcs[r - 1, g, b].SumCSqPc + area2[b];

                        }
                    }
            }
        }

        public RGBColorPrecalcWu Get(ulong r, ulong g, ulong b)
        {
            return precalcs[r, g, b];
        }

        public double P(RGBColor c)
        {
            if (precalcs[c.R, c.G, c.B] != null)
                return precalcs[c.R, c.G, c.B].P;
            else return 0.0;
        }

        public RGBColor MinColor { get { return this.minColor; } }
        public RGBColor MaxColor { get { return this.maxColor; } }

    }
}
