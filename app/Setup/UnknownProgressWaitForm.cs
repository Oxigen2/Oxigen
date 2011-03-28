using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Setup
{
  public partial class UnknownProgressWaitForm : Form
  {
    protected string _message;

    public UnknownProgressWaitForm(string message)
    {
      InitializeComponent();

      _message = message;

      lblProgress.Text = _message;
    }

    private void UnknownProgressWaitForm_Load(object sender, EventArgs e)
    {

    }
  }
}
