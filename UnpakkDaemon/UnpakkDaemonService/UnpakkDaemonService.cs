using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using UnpakkDaemon;

namespace UnpakkDaemonService
{
	public partial class UnpakkDaemonService : ServiceBase
	{
		private readonly Engine _engine;

		public UnpakkDaemonService()
		{
			InitializeComponent();
			_engine = new Engine(Application.StartupPath);
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
