using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FunctionFactory
{
    /// <summary>
    /// Klasa która implementuje ten interfejs MUSI posiadać konstruktor bez parametrów.
    /// Sluży to automatycznemu zebraniu wszystkich implementacji tego interfejsu
    /// </summary>
    public interface IFunction
    {
        Bitmap Calculate(Bitmap orginal);

        IList<IFunctionParamControl> ParamList { get; }

        string EnglishName { get; }

        string PolishName { get; }
    }
}
