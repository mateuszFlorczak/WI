using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using FunctionFactory.ZZ;
using FunctionFactory.Controls;

namespace FunctionFactory.CoreFunctions
{
    public class PopularityQuantization : IFunction
    {
        IntSlider _quantNumber = new IntSlider("liczba kolorów", 16, 64, 256);

        public Bitmap Calculate(Bitmap orginal)
        {
            return PopularityQuantization_Raw(orginal, _quantNumber.Value);
        }

        public IList<IFunctionParamControl> ParamList
        {
            get { return new List<IFunctionParamControl> { _quantNumber }; }
        }

        public string EnglishName
        {
            get { return "PopularityQuantization"; }
        }

        public string PolishName
        {
            get { return ""; }
        }

        private Bitmap PopularityQuantization_Raw(Bitmap bitmap, int quantNumber)
        {
            return MMCG3.PopularityAlgorithm.PopularityQuantization(bitmap, ZZ.ZZ.UpdateBitmap(bitmap), quantNumber);
        }
    }
}
