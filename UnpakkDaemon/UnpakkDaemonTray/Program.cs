using System;
using System.Windows.Forms;
using UnpakkDaemonTray.Forms;

namespace UnpakkDaemonTray
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
