using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FunctionFactory.ZZ;
using FunctionFactory.Controls;
using System.Collections;

namespace FunctionFactory.CoreFunctions
{
    public class MedianCut : IFunction
    {
        IntSlider _maxColor = new IntSlider("Liczba kolorów", 16, 64, 256);


        public Bitmap Calculate(Bitmap orginal)
        {
            return MMCG3.MedianCutAlgorithm.MedianCut(orginal, ZZ.ZZ.UpdateBitmap(orginal), _maxColor.Value);
        }

        public IList<IFunctionParamControl> ParamList
        {
            get {return new List<IFunctionParamControl> { _maxColor }; }
        }

        public string EnglishName
        {
            get { return "Median Cut"; }
        }

        public string PolishName
        {
            get { return "kwantyzacja barwy"; }
        }
    }
}
