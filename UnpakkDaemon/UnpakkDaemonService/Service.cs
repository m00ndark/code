using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using UnpakkDaemon;

namespace UnpakkDaemonService
{
	public partial class Service : ServiceBase
	{
		private readonly Engine _engine;

		public Service()
		{
			InitializeComponent();
			_engine = new Engine();
		}

		protected override void OnStart(string[] args)
		{
			if (!_engine.IsRunning)
				new Thread(_engine.Start).Start();
		}

		protected override void OnStop()
		{
			_engine.ShutDown();
		}
	}
}
