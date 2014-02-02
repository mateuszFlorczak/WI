using System;
using System.Collections.Generic;
using System.Text;

namespace MMCG3
{
    public class RGBCuboidPair
    {
        RGBCuboid cub1;
        RGBCuboid cub2;

        public RGBCuboidPair(RGBCuboid cb1, RGBCuboid cb2)
        {
            cub1 = cb1;
            cub2 = cb2;
        }

        public RGBCuboid C1 { get { return this.cub1; } }
        public RGBCuboid C2 { get { return this.cub2; } }
    }
}
