using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace FunctionFactory
{
    public interface IForm
    {
        IForm AddControl(UserControl newControl);

        void Show();

        EventHandler OkButton { get; set; }
    }
}
