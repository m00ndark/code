using System.ComponentModel;
using System.Configuration.Install;

namespace UnpakkDaemonService
{
	[RunInstaller(true)]
	public partial class UnpakkDaemonServiceInstaller : Installer
	{
		public UnpakkDaemonServiceInstaller()
		{
			InitializeComponent();
		}
	}
}
