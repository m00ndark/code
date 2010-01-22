using System;
using System.Collections.Generic;
using UnpakkDaemon.DataObjects;

namespace UnpakkDaemon.EventArguments
{
	public class RootPathListEventArgs : EventArgs
	{
		public RootPathListEventArgs(IEnumerable<RootPath> rootPaths)
		{
			RootPaths = rootPaths;
		}

		public IEnumerable<RootPath> RootPaths { get; private set; }
	}
}
