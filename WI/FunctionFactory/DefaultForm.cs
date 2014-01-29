using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunctionFactory
{
    public partial class EmptyForm : Form, IForm
    {
        public EmptyForm()
        {
            InitializeComponent();
            OkButton = delegate { };
        }
        IForm IForm.AddControl(UserControl newControl)
        {
            this.flowLayoutPanel1.Controls.Add(newControl);
            return this;
        }

        void IForm.Show()
        {
            this.ShowDialog();
        }

        Image IForm.ImagePreview
        {
            get
            {
                return this.ImageBox.Image;
            }
            set
            {
                this.ImageBox.Image = value;
            }
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            OkButton.Invoke(this, e);
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public EventHandler OkButton { get; set; }
    }
}
