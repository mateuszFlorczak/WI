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
        Image orginal;

        public EmptyForm()
        {
            InitializeComponent();
            OkButton = delegate { };
        }
        IForm IForm.AddControl(UserControl newControl)
        {
            this.ParameterPlace.Controls.Add(newControl);
            return this;
        }

        void IForm.Show()
        {
            this.ShowDialog();
        }

        public void SetImagePreview(Image image)
        {
            orginal = image;
            this.PreviewImage.Image = new Bitmap(image, this.PreviewImage.Size);
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

        private void EmptyForm_SizeChanged(object sender, EventArgs e)
        {
            this.SetImagePreview(this.orginal);
        }
    }
}
