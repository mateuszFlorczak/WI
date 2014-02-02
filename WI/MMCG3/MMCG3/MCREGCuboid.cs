using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace MMCG3
{


    public class MCRGBCuboid: RGBCuboid
    {

        int[,,] colorCube;
        int pixelsInside;
        RGBColor midColor;


        public MCRGBCuboid(RGBColor c1, RGBColor c2, int[, ,] _colorCube)
            :
            base(c1, c2)
        {
            colorCube = _colorCube;
            for(ulong r =redStart; r<redEnd; r++)
                for (ulong g = greenStart; g < greenEnd; g++)
                    for (ulong b = blueStart; b < blueEnd; b++)
                    {
                        pixelsInside += colorCube[r, g, b];
                    }

            FindMidColor();
        }

        public RGBCuboidPair Divide()
        {
            if(dr<2 && dg <2 && db<2)
            {
                throw new AlgException(AlgException.Cause.CuboidTooSmallToDivide);
            }
            ulong i = 0;
            int half = (int)(this.Weight / 2);
            RGBCuboidPair pair = null;
            Dimension md = MaximumDimension();
            switch (md)
            {
                case Dimension.RED:
                    pair = divideRed(1);
                    while (((MCRGBCuboid)pair.C1).Weight < half)
                    {
                        i++;
                        try
                        {
                            pair = divideRed(i);
                        }
                        catch
                        {
                            // cannot divide edge
                            break;
                        }
                    }
                    break;
                case Dimension.GREEN:
                    pair = divideGreen(1);
                    while (((MCRGBCuboid)pair.C1).Weight < half)
                    {
                        i++;
                        try
                        {
                            pair = divideGreen(i);
                        }
                        catch
                        {
                            // cannot divide edge
                            break;
                        }
                    }
                    break;
                case Dimension.BLUE:
                    pair = divideBlue(1);
                    while (((MCRGBCuboid)pair.C1).Weight < half)
                    {
                        i++;
                        try
                        {
                           pair = divideBlue(i);
                        }
                        catch
                        {
                            // cannot divide edge
                            break;
                        }
                    }
                    break;
                default: break;
            }
            return pair;
        }



        public Dimension MaximumDimension()
        {

            if (dr >= dg && dr >= db && dr > 1)
                return Dimension.RED;
            if (dg >= dr && dg >= db && dg > 1)
                return Dimension.GREEN;
            if (db >= dg && db >= dr && db > 1)
                return Dimension.BLUE;
 
            throw new AlgException(AlgException.Cause.CuboidTooSmallToDivide);
        }
        public int Weight { get { return pixelsInside; } }

        protected override RGBCuboidPair divideRed(ulong divisionIndex)
        {
            RGBCuboidPair p = base.divideRed(divisionIndex);
            RGBColor c11 = new RGBColor(p.C1.RedStart, p.C1.GreenStart, p.C1.BlueStart);
            RGBColor c21 = new RGBColor(p.C2.RedStart, p.C2.GreenStart, p.C2.BlueStart);
            RGBColor c12 = new RGBColor(p.C1.RedEnd, p.C1.GreenEnd, p.C1.BlueEnd);
            RGBColor c22 = new RGBColor(p.C2.RedEnd, p.C2.GreenEnd, p.C2.BlueEnd);
            return new RGBCuboidPair(new MCRGBCuboid(c11, c12, colorCube), new MCRGBCuboid(c21, c22, colorCube));
        }


        protected override RGBCuboidPair divideBlue(ulong divisionIndex)
        {
            RGBCuboidPair p = base.divideBlue(divisionIndex);
            RGBColor c11 = new RGBColor(p.C1.RedStart, p.C1.GreenStart, p.C1.BlueStart);
            RGBColor c21 = new RGBColor(p.C2.RedStart, p.C2.GreenStart, p.C2.BlueStart);
            RGBColor c12 = new RGBColor(p.C1.RedEnd, p.C1.GreenEnd, p.C1.BlueEnd);
            RGBColor c22 = new RGBColor(p.C2.RedEnd, p.C2.GreenEnd, p.C2.BlueEnd);
            return new RGBCuboidPair(new MCRGBCuboid(c11, c12, colorCube), new MCRGBCuboid(c21, c22, colorCube));
        }

        protected override RGBCuboidPair divideGreen(ulong divisionIndex)
        {
            RGBCuboidPair p = base.divideGreen(divisionIndex);
            RGBColor c11 = new RGBColor(p.C1.RedStart, p.C1.GreenStart, p.C1.BlueStart);
            RGBColor c21 = new RGBColor(p.C2.RedStart, p.C2.GreenStart, p.C2.BlueStart);
            RGBColor c12 = new RGBColor(p.C1.RedEnd, p.C1.GreenEnd, p.C1.BlueEnd);
            RGBColor c22 = new RGBColor(p.C2.RedEnd, p.C2.GreenEnd, p.C2.BlueEnd);
            return new RGBCuboidPair(new MCRGBCuboid(c11, c12, colorCube), new MCRGBCuboid(c21, c22, colorCube));
        }

        public RGBColor MidColor { get { return midColor; } }


        private void FindMidColor()
        {
            if (pixelsInside == 0)
            {
                midColor = new RGBColor(0UL, 0UL, 0UL);
                return;
            }
            ulong sumr=0UL, sumg=0UL, sumb=0UL;
            for (ulong r = redStart; r < redEnd; r++)
                for (ulong g = greenStart; g < greenEnd; g++)
                    for (ulong b = blueStart; b < blueEnd; b++)
                    {
                        sumr += r * (ulong)colorCube[r, g, b];
                        sumg += g * (ulong)colorCube[r, g, b];
                        sumb += b * (ulong)colorCube[r, g, b];
                    }

            midColor = new RGBColor(
                (ulong)(sumr) / ( (ulong)pixelsInside),
                (ulong)((sumg) / (ulong)pixelsInside),
                (ulong)(sumb) / ((ulong)(pixelsInside)));
        }


    }


}
