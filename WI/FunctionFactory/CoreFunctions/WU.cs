using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using FunctionFactory.ZZ;
using FunctionFactory.Controls;
using System.Collections;
using System.Windows.Forms;

namespace FunctionFactory.CoreFunctions
{
    public class WU :IFunction
    {
        IntSlider _quantNumber = new IntSlider("Liczba kolorów", 16, 64, 256);

        public Bitmap Calculate(Bitmap orginal)
        {
            return MMCG3.WuAlgorithm.WuQuantization(orginal, ZZ.ZZ.UpdateBitmap(orginal), _quantNumber.Value);
        }

        public IList<IFunctionParamControl> ParamList
        {
            get { return new List<IFunctionParamControl> { _quantNumber }; }
        }

        public string EnglishName
        {
            get { return "WU"; }
        }

        public string PolishName
        {
            get { return "WU"; }
        }
    }
}
