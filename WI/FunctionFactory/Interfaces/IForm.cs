using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;



namespace FunctionFactory
{
    public interface IForm
    {
        IForm AddControl(UserControl newControl);

        void Show();

        Image ImagePreview { get; set; }

        EventHandler OkButton { get; set; }
    }
}
