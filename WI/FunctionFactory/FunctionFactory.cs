using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FunctionFactory.Controls;
using FunctionFactory.CoreFunctions;

namespace FunctionFactory
{
    public class Factory
    {
        public IList<IFunction> Functions { get; set; }

        public Factory()
        {
            Functions = new List<IFunction>();
            Functions.Add(new Quantization());
        }
    }
}
