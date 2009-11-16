using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;


namespace UnpakkDaemonService
{
	[RunInstaller(true)]
	public partial class Installer : ServiceInstaller
	{
		public Installer()
		{
			InitializeComponent();
		}
	}
}
