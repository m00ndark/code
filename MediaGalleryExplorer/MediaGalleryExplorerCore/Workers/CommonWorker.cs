using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MediaGalleryExplorerCore.DataAccess;
using MediaGalleryExplorerCore.EventArguments;

namespace MediaGalleryExplorerCore.Workers
{
	public class CommonWorker
	{
		#region Providing file system information

		public static IDictionary<string, int> GetAvailableEncryptionAlgorithms()
		{
			return FileSystemHandler.GetEncryptionAlgorithms();
		}

		public static bool PathNameIsValid(string filePath)
		{
			return FileSystemHandler.PathNameIsValid(filePath);
		}

		#endregion

		#region Show messages

		public delegate object EventHandler<in TEventArgs>(Object sender, TEventArgs e) where TEventArgs : EventArgs;

		public static event EventHandler<MessageEventArgs> ShowMessage;

		public static void ShowError(Exception ex)
		{
			ShowError("An exception occurred.\r\n\r\n" + ex);
		}

		public static void ShowError(string message)
		{
			RaiseShowMessageEvent(message, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public static void ShowInformation(string message)
		{
			RaiseShowMessageEvent(message, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public static DialogResult ShowQuestion(string message)
		{
			return RaiseShowMessageEvent(message, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}

		private static DialogResult RaiseShowMessageEvent(string message, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			if (ShowMessage != null)
				return (DialogResult) ShowMessage(null, new MessageEventArgs(message, buttons, icon));

			return DialogResult.None;
		}

		#endregion
	}
}
