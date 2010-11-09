using System;
using System.Windows.Forms;

namespace MediaGalleryExplorerCore.EventArguments
{
	public class MessageEventArgs : EventArgs
	{
		public MessageEventArgs(string message, MessageBoxButtons buttons, MessageBoxIcon icon) : base()
		{
			Message = message;
			Buttons = buttons;
			Icon = icon;
		}

		public string Message { get; private set; }
		public MessageBoxButtons Buttons { get; private set; }
		public MessageBoxIcon Icon { get; private set; }
	}
}
