using System;
using System.Windows.Forms;

namespace ProcessController.Utilities
{
    public static class FormUtilities
    {
        public static void ShowError(Form form, Exception ex)
        {
            ShowError(form, "An exception occurred.\r\n\r\n" + ex);
        }

        public static void ShowError(Form form, string message)
        {
            ShowMessage(form, message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowInformation(Form form, string message)
        {
            ShowMessage(form, message, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult ShowQuestion(Form form, string message)
        {
            return ShowMessage(form, message, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult ShowMessage(Form form, string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(form, message, "Process Controller", buttons, icon);
        }
    }
}