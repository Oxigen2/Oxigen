﻿using System.Windows.Forms;
using Setup.ClientLoggers;

namespace Setup
{
    public partial class SetupForm : OxigenForm
    {
        public SetupForm()
        {
            InitializeComponent();
        }

        private void SetupForm_Closing(object sender, FormClosingEventArgs e)
        {
            if (AppDataSingleton.Instance.OneFormClosed)
                return;

            if (AppDataSingleton.Instance.ExitPromptSuppressed)
            {
                Application.Exit();
                return;
            }

            if (AppDataSingleton.Instance.OldOxigenSystemModified)
            {
                Application.Exit();
                return;
            }

            if (MessageBox.Show("Are you sure you want to exit Setup?\r\nYour system has not been modified. Please click OK to exit.", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                AppDataSingleton.Instance.OneFormClosed = true;

                ClientLogger logger = new PersistentClientLogger();
                logger.Log("User_Exiting");

                Application.Exit();
            }
            else
                e.Cancel = true;
        }
    }
}
