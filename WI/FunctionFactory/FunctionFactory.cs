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
            CreateQuantization();
        }
        private IFunction CreateQuantization()
        {
            //getDefaultForm().AddControl(newIntSlider(2, 4, 8, 16, 32)).Show();
            return null;
        }
        public IForm GetDefaultForm()
        {
            return new EmptyForm();
        }
        //private IntSlider newIntSlider(params int[] values)
        //{
        //    return new IntSlider(values);
        //}
    }
}
