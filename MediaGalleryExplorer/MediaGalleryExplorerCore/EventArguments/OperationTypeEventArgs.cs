using System;
using MediaGalleryExplorerCore.Workers;

namespace MediaGalleryExplorerCore.EventArguments
{
	public class OperationTypeEventArgs : EventArgs
	{
		public OperationTypeEventArgs(MainWorker.OperationType operationType) : base()
		{
			OperationType = operationType;
		}

		public MainWorker.OperationType OperationType { get; set; }
	}
}