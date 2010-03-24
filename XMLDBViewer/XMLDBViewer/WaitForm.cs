using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace XMLDBViewer
{
	public partial class WaitForm : Form
	{
		private readonly Worker _worker;

		public WaitForm(Worker worker, Point centerLocation, string waitMessage)
		{
			InitializeComponent();
			_worker = worker;
			Location = new Point(centerLocation.X - Size.Width / 2, centerLocation.Y - Size.Height / 2);
			WaitMessage = waitMessage;
		}

		#region Properties

		public string WaitMessage
		{
			get { return labelMessage.Text; }
			set { labelMessage.Text = value; }
		}

		#endregion

		#region GUI event handlers

		private void WaitForm_Load(object sender, EventArgs e)
		{
			DateTime showFormTimeout = DateTime.Now.AddMilliseconds(500);
			while (showFormTimeout > DateTime.Now && _worker.WaitOperationRunning)
			{
				Application.DoEvents();
				Thread.Sleep(10);
			}
			if (_worker.WaitOperationRunning)
			{
				Opacity = 1;
				new Thread(WaitThread).Start();
			}
			else
				Close();
		}

		#endregion

		private void WaitThread()
		{
			while (_worker.WaitOperationRunning)
			{
				Application.DoEvents();
				Thread.Sleep(10);
			}

			if (InvokeRequired)
				Invoke(new MethodInvoker(Close));
			else
				Close();
		}
	}
}
