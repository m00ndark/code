using System;

namespace XMLDBViewer.EventArguments
{
	public class AdvancedSearchResultItemEventArgs : EventArgs
	{
		public AdvancedSearchResultItemEventArgs(AdvancedSearchResultItem value) : base()
		{
			Value = value;
		}

		public AdvancedSearchResultItem Value { get; set; }
	}
}
