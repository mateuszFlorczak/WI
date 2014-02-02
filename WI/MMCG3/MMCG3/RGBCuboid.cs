using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace MMCG3
{

    public class RGBCuboid
    {
        protected ulong redStart;
        protected ulong redEnd;
        protected ulong greenStart;
        protected ulong greenEnd;
        protected ulong blueStart;
        protected ulong blueEnd;

        protected ulong dr;
        protected ulong dg;
        protected ulong db;

        public RGBCuboid(RGBColor c1, RGBColor c2)
        {
            if (c1.R >= c2.R)
            {
                redStart = (ulong)c2.R;
                redEnd = (ulong)c1.R;
            }
            else
            {
                redStart = (ulong)c1.R;
                redEnd = (ulong)c2.R;
            }

            if (c1.G >= c2.G)
            {
                greenStart = (ulong)c2.G;
                greenEnd = (ulong)c1.G;
            }
            else
            {
                greenStart = (ulong)c1.G;
                greenEnd = (ulong)c2.G;
            }

            if (c1.B >= c2.B)
            {
                blueStart = (ulong)c2.B;
                blueEnd = (ulong)c1.B;
            }
            else
            {
                blueStart = (ulong)c1.B;
                blueEnd = (ulong)c2.B;
            }

            dr = redEnd - redStart;
            dg = greenEnd - greenStart;
            db = blueEnd - blueStart;
        }



        public ulong DR { get { return dr; } }
        public ulong DG { get { return dg; } }
        public ulong DB { get { return db; } }

        public ulong RedStart { get { return redStart; } }
        public ulong GreenStart { get { return greenStart; } }
        public ulong BlueStart { get { return blueStart; } }
        public ulong RedEnd { get { return redEnd; } }
        public ulong GreenEnd { get { return greenEnd; } }
        public ulong BlueEnd { get { return blueEnd; } }


        protected virtual RGBCuboidPair divideRed(ulong divisionIndex)
        {
            if (divisionIndex < 1)
            {
                throw new AlgException(AlgException.Cause.InvalidDivisionIndex);
            }
            if (divisionIndex >= (this.redEnd - this.redStart)
                || (this.redEnd - this.redStart) < 2)
            {
                throw new AlgException(AlgException.Cause.CannotDivideRedEdge);
            }
            RGBColor c11 = new RGBColor(redStart, greenStart, blueStart);
            RGBColor c12 = new RGBColor(redStart + divisionIndex, greenEnd, blueEnd);
            RGBColor c21 = new RGBColor(redStart + divisionIndex, greenStart, blueStart);
            RGBColor c22 = new RGBColor(redEnd, greenEnd, blueEnd);
            return new RGBCuboidPair(new RGBCuboid(c11, c12), new RGBCuboid(c21, c22));
        }

        protected virtual RGBCuboidPair divideGreen(ulong divisionIndex)
        {
            if (divisionIndex < 1)
            {
                throw new AlgException(AlgException.Cause.InvalidDivisionIndex);
            }
            if (divisionIndex >= (this.greenEnd - this.greenStart)
                || (this.greenEnd - this.greenStart) < 2)
            {
                throw new AlgException(AlgException.Cause.CannotDivideGreenEdge);
            }
            RGBColor c11 = new RGBColor(redStart, greenStart, blueStart);
            RGBColor c12 = new RGBColor(redEnd, greenStart + divisionIndex, blueEnd);
            RGBColor c21 = new RGBColor(redStart, greenStart + divisionIndex, blueStart);
            RGBColor c22 = new RGBColor(redEnd, greenEnd, blueEnd);
            return new RGBCuboidPair(new RGBCuboid(c11, c12), new RGBCuboid(c21, c22));
        }

        protected virtual RGBCuboidPair divideBlue(ulong divisionIndex)
        {
            if (divisionIndex < 1)
            {
                throw new AlgException(AlgException.Cause.InvalidDivisionIndex);
            }
            if (divisionIndex >= (this.blueEnd - this.blueStart)
                || (this.blueEnd - this.blueStart) < 2)
            {
                throw new AlgException(AlgException.Cause.CannotDivideBlueEdge);
            }
            RGBColor c11 = new RGBColor(redStart, greenStart, blueStart);
            RGBColor c12 = new RGBColor(redEnd, greenEnd, blueStart + divisionIndex);
            RGBColor c21 = new RGBColor(redStart, greenStart, blueStart + divisionIndex);
            RGBColor c22 = new RGBColor(redEnd, greenEnd, blueEnd);
            return new RGBCuboidPair(new RGBCuboid(c11, c12), new RGBCuboid(c21, c22));
        }

    }

    public class WuRGBCuboid : RGBCuboid
    {


        double MSE;

        PrecalculatedWu pre;
        RGBColor midColor;

        public RGBColor sumOfCPC;
        public double sumOfPC;

        public WuRGBCuboid(RGBColor c1, RGBColor c2, PrecalculatedWu _pre, RGBColor parentColor)
            :base(c1,c2)
        {
            pre = _pre;
            try
            {
                midColor = this.M();
            }
            catch
            {
                midColor = parentColor;
            }

            try
            {
                MSE = this.CalculateVariance();
            }
            catch
            {
                MSE = 0.0;
            }

        }


        

        private double CalculateVariance()
        {
            RGBColor sumCPC = this.CalculateSumCPC();
            double s2 = sumCPC * sumCPC;
            double pc = CalculateSumPC();
            double div = s2 / pc;
            double scqpc = CalculateSumCSQPC();
            double result = this.CalculateSumCSQPC() - sumCPC * sumCPC / pc;
            if (pc == 0.0)
            {
                throw new AlgException(AlgException.Cause.ComputationFailed);
            }
            return result; 
        }

        

        
        /// <summary>
        /// Mean square error
        /// </summary>
        /// <param name="omega"></param>
        /// <returns></returns>
        public double Variance{ get{ return MSE; } }



        public RGBColor MidColor { get { return midColor;  } }

        /// <summary>
        /// mean weighted color in the cube
        /// </summary>
        /// <returns></returns>
        private RGBColor M()
        {
            RGBColor cpc = CalculateSumCPC();
            double pc = CalculateSumPC();
            RGBColor result = cpc / pc;
            if (pc == 0.0)
                throw new AlgException(AlgException.Cause.ComputationFailed);
            return result;
        }




        public RGBCuboidPair FindMinimumMSEDivision()
        {
            if (dr <= 1 && dg <= 1 && db <= 1)
            {
                throw new AlgException(AlgException.Cause.CuboidTooSmallToDivide);
            }
            ulong i;
            double maxSumOfVariances = double.MinValue ;
            double tempSum;
            RGBCuboidPair tempPair;
            RGBCuboidPair maxPair= null ;

            for (i = 1; i < redEnd - redStart; i++)
            {
                try
                {
                    tempPair = this.divideRed(i);
                    tempSum = this.SumOfVariancesRev(this, (WuRGBCuboid)tempPair.C1);
                    if (tempSum > maxSumOfVariances)
                    {
                        maxSumOfVariances = tempSum;
                        maxPair = tempPair;
                    }
                }
                catch { }
            }

            for (i = 1; i < greenEnd - greenStart; i++)
            {
                try
                {
                    tempPair = this.divideGreen(i);
                    tempSum = this.SumOfVariancesRev(this,(WuRGBCuboid)tempPair.C1);
                    if (tempSum > maxSumOfVariances)
                    {
                        maxSumOfVariances = tempSum;
                        maxPair = tempPair;
                    }
                }
                catch  { }
            }

            for (i = 1; i < blueEnd - blueStart; i++)
            {
                try
                {
                    tempPair = this.divideBlue(i);
                    tempSum = this.SumOfVariancesRev(this, (WuRGBCuboid)tempPair.C1);
                    if (tempSum > maxSumOfVariances)
                    {
                        maxSumOfVariances = tempSum;
                        maxPair = tempPair;
                    }
                }
                catch  { }
            }
            if (maxPair == null)
            {
                if (dr >= dg && dr >= db)
                {
                    return divideRed((ulong)(dr / 2UL));
                }
                if (dg >= dr && dg >= db)
                {
                    return divideGreen((ulong)(dg / 2UL));
                }
                if (db >= dg && db >= dr)
                {
                    return divideBlue((ulong)(db / 2UL));
                }
            }
            return maxPair;
        }

 
        /*Inclusion exclusion principle, using precalculated data*/
        public RGBColor CalculateSumCPC()
        {
            RGBColorPrecalcWu v0 = pre.Get(redStart, greenStart, blueStart);
            RGBColorPrecalcWu v1 = pre.Get(redStart, greenEnd, blueStart);
            RGBColorPrecalcWu v2 = pre.Get(redStart, greenStart, blueEnd);
            RGBColorPrecalcWu v3 = pre.Get(redStart, greenEnd, blueEnd);
            RGBColorPrecalcWu v4 = pre.Get(redEnd, greenStart, blueStart);
            RGBColorPrecalcWu v5 = pre.Get(redEnd, greenEnd, blueStart);
            RGBColorPrecalcWu v6 = pre.Get(redEnd, greenStart, blueEnd);
            RGBColorPrecalcWu v7 = pre.Get(redEnd, greenEnd, blueEnd);

            return 
                v7.SumCPC 
                - v6.SumCPC 
                - v5.SumCPC 
                + v4.SumCPC 
                - v3.SumCPC
                +v2.SumCPC
                +v1.SumCPC
                -v0.SumCPC;
        }

        /*Inclusion exclusion principle, using precalculated data*/
        public double CalculateSumPC()
        {
            RGBColorPrecalcWu v0 = pre.Get(redStart, greenStart, blueStart);
            RGBColorPrecalcWu v1 = pre.Get(redStart, greenEnd, blueStart);
            RGBColorPrecalcWu v2 = pre.Get(redStart, greenStart, blueEnd);
            RGBColorPrecalcWu v3 = pre.Get(redStart, greenEnd, blueEnd);
            RGBColorPrecalcWu v4 = pre.Get(redEnd, greenStart, blueStart);
            RGBColorPrecalcWu v5 = pre.Get(redEnd, greenEnd, blueStart);
            RGBColorPrecalcWu v6 = pre.Get(redEnd, greenStart, blueEnd);
            RGBColorPrecalcWu v7 = pre.Get(redEnd, greenEnd, blueEnd);

            return
                v7.SumPC
                - v6.SumPC
                - v5.SumPC
                + v4.SumPC
                - v3.SumPC
                + v2.SumPC
                + v1.SumPC
                - v0.SumPC;
        }

        /*Inclusion exclusion principle, using precalculated data*/
        public double CalculateSumCSQPC()
        {
            RGBColorPrecalcWu v0 = pre.Get(redStart, greenStart, blueStart);
            RGBColorPrecalcWu v1 = pre.Get(redStart, greenEnd, blueStart);
            RGBColorPrecalcWu v2 = pre.Get(redStart, greenStart, blueEnd);
            RGBColorPrecalcWu v3 = pre.Get(redStart, greenEnd, blueEnd);
            RGBColorPrecalcWu v4 = pre.Get(redEnd, greenStart, blueStart);
            RGBColorPrecalcWu v5 = pre.Get(redEnd, greenEnd, blueStart);
            RGBColorPrecalcWu v6 = pre.Get(redEnd, greenStart, blueEnd);
            RGBColorPrecalcWu v7 = pre.Get(redEnd, greenEnd, blueEnd);

            return
                v7.SumCSqPc
                - v6.SumCSqPc
                - v5.SumCSqPc
                + v4.SumCSqPc
                - v3.SumCSqPc
                + v2.SumCSqPc
                + v1.SumCSqPc
                - v0.SumCSqPc;
        }

        private double SumOfVariancesRev(
            WuRGBCuboid dividedCuboid, 
            WuRGBCuboid firstFromPair)
        {
            RGBColor CPCFirst = firstFromPair.CalculateSumCPC() ;
            RGBColor CPCDivided = dividedCuboid.CalculateSumCPC();
            double PCFirst = firstFromPair.CalculateSumPC() ;
            double PCDivided = dividedCuboid.CalculateSumPC();
            double result = (CPCFirst * CPCFirst) / PCFirst
                + ((CPCDivided - CPCFirst) * (CPCDivided - CPCFirst))
                    / (PCDivided - PCFirst);
            return result;

         }


        protected override RGBCuboidPair divideRed(ulong divisionIndex)
        {
            RGBCuboidPair p = base.divideRed(divisionIndex);
            RGBColor c11 = new RGBColor(p.C1.RedStart, p.C1.GreenStart, p.C1.BlueStart);
            RGBColor c21 = new RGBColor(p.C2.RedStart, p.C2.GreenStart, p.C2.BlueStart);
            RGBColor c12 = new RGBColor(p.C1.RedEnd, p.C1.GreenEnd, p.C1.BlueEnd);
            RGBColor c22 = new RGBColor(p.C2.RedEnd, p.C2.GreenEnd, p.C2.BlueEnd);
            return new RGBCuboidPair(new WuRGBCuboid(c11, c12, pre, midColor), new WuRGBCuboid(c21, c22, pre, midColor));
        }


        protected override RGBCuboidPair divideBlue(ulong divisionIndex)
        {
            RGBCuboidPair p = base.divideBlue(divisionIndex);
            RGBColor c11 = new RGBColor(p.C1.RedStart, p.C1.GreenStart, p.C1.BlueStart);
            RGBColor c21 = new RGBColor(p.C2.RedStart, p.C2.GreenStart, p.C2.BlueStart);
            RGBColor c12 = new RGBColor(p.C1.RedEnd, p.C1.GreenEnd, p.C1.BlueEnd);
            RGBColor c22 = new RGBColor(p.C2.RedEnd, p.C2.GreenEnd, p.C2.BlueEnd);
            return new RGBCuboidPair(new WuRGBCuboid(c11, c12, pre, midColor), new WuRGBCuboid(c21, c22, pre, midColor));
        }

        protected override RGBCuboidPair divideGreen(ulong divisionIndex)
        {
            RGBCuboidPair p = base.divideGreen(divisionIndex);
            RGBColor c11 = new RGBColor(p.C1.RedStart, p.C1.GreenStart, p.C1.BlueStart);
            RGBColor c21 = new RGBColor(p.C2.RedStart, p.C2.GreenStart, p.C2.BlueStart);
            RGBColor c12 = new RGBColor(p.C1.RedEnd, p.C1.GreenEnd, p.C1.BlueEnd);
            RGBColor c22 = new RGBColor(p.C2.RedEnd, p.C2.GreenEnd, p.C2.BlueEnd);
            return new RGBCuboidPair(new WuRGBCuboid(c11, c12, pre, midColor), new WuRGBCuboid(c21, c22, pre, midColor));
        }

    }
}
