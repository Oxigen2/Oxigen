using System;

namespace Setup
{
  internal abstract partial class OxigenLogoForm : OxigenForm
  {
    protected OxigenLogoForm()
    {
      InitializeComponent();
    }

    protected void Form_Shown(object sender, EventArgs e)
    {
      base.Form_Shown(sender, e);
    }
  }
}
