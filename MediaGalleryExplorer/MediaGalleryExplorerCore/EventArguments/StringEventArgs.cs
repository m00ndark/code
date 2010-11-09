using System;

namespace MediaGalleryExplorerCore.EventArguments
{
	public class StringEventArgs : EventArgs
	{
		public StringEventArgs(string value) : base()
		{
			Value = value;
		}

		public string Value { get; set; }
	}
}