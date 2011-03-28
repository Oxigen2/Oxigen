using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibraryTester
{
  public partial class GUIDCreator : Form
  {
    public GUIDCreator()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      guid.Text = System.Guid.NewGuid().ToString();
    }

    private void GUIDCreator_Load(object sender, EventArgs e)
    {

    }
  }
}
