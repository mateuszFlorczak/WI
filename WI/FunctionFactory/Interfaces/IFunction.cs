using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FunctionFactory
{
    public interface IFunction
    {
        Bitmap Calculate(Bitmap orginal);

        IList<IFunctionParamControl> ParamList { get; }

        string EnglishName { get; }

        string PolishName { get; }
    }
}
