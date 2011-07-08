using System.Drawing;
using System.Windows.Forms;

namespace Setup
{
    public partial class OxigenForm : Form
    {
        public OxigenForm()
        {
            InitializeComponent();

            pictureBox1.Image = SetupHelper.GetBanner();
        }
    }
}
