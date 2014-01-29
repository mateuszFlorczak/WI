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
    public class FunctionFactory
    {
        public IList<IFunction> Functions { get; set; }

        public FunctionFactory()
        {
            Functions = new List<IFunction>();
            Functions.Add(new Quantization());
        }
        public IForm GetDefaultForm()
        {
            return new EmptyForm();
        }
    }
}
