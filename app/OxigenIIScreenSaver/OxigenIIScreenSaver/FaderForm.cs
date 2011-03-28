using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OxigenIIAdvertising.ScreenSaver
{
  public partial class FaderForm : Form
  {
    public FaderForm()
    {
      InitializeComponent();

      this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
    }
  }
}
