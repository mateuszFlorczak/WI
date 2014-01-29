using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FunctionFactory.Interfaces;

namespace FunctionFactory.Controls
{
    /// <summary>
    /// Wartość jest zaokrąglana w dół
    /// </summary>
    public partial class IntSlider : UserControl, IFunctionParamControl, IValue<int>
    {
        /// <summary>
        /// lista punktów określająca przedziały posortowana malejąco np 32,16,8,4,2
        /// </summary>
        IList<int> _d;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d">List z przedziałami wartości posortowana rosnąco np 2,4,8,16,32</param>
        public IntSlider(string paramName, params int[] d2)
        {
            IList<int> d = d2;
            InitializeComponent();
            this.trackBar.Maximum = d[d.Count - 1];
            this.trackBar.Minimum = d[0];
            _d = d.Reverse().ToList();
            Value = d[0];
            this.paramName.Text = paramName;
        }
        public int Value {
            get { return this.trackBar.Value; }
            private set
            {
                this.paramValueDisplay.Text = value.ToString();
                this.trackBar.Value = value; 
            }
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            Value = _d.SkipWhile(i => i > Value).First();
        }


        public UserControl GetUserControl
        {
            get { return this; }
        }
    }
}
