using System;
using System.Collections.Generic;

namespace UnpakkDaemon.EventArguments
{
	public class RootPathListEventArgs : EventArgs
	{
		public RootPathListEventArgs(IEnumerable<string> rootPaths)
		{
			RootPaths = rootPaths;
		}

		public IEnumerable<string> RootPaths { get; private set; }
	}
}
