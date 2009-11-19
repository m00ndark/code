using System;
using UnpakkDaemon.EventArguments;

namespace UnpakkDaemon
{
	public interface IStatusProvider
	{
		event EventHandler<ProgressEventArgs> Progress;
		event EventHandler<ProgressEventArgs> SubProgress;
	}
}
