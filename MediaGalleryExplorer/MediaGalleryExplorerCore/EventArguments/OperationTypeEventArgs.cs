using System;
using MediaGalleryExplorerCore.Workers;

namespace MediaGalleryExplorerCore.EventArguments
{
	public class OperationTypeEventArgs : EventArgs
	{
		public OperationTypeEventArgs(GalleryWorker.OperationType operationType) : base()
		{
			OperationType = operationType;
		}

		public GalleryWorker.OperationType OperationType { get; set; }
	}
}