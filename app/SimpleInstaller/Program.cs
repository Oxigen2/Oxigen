using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SimpleInstaller
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Introduction i = new Introduction();
      i.Show();
      Application.Run();
    }
  }
}
