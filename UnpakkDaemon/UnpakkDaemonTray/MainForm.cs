using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UnpakkDaemon.EventArguments;
using UnpakkDaemon.Service.Client;

namespace UnpakkDaemonTray
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			StatusChangedHandler statusChangedHandler = new StatusChangedHandler();
			statusChangedHandler.ProgressChanged += statusChangedHandler_ProgressChanged;
			statusChangedHandler.SubProgressChanged += statusChangedHandler_SubProgressChanged;
			ObjectPool.StatusServiceHandler = new StatusServiceHandler(statusChangedHandler);
			ObjectPool.StatusServiceHandler.Subscribe();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			ObjectPool.StatusServiceHandler.Unsubscribe();
		}

		private void statusChangedHandler_ProgressChanged(object sender, ProgressEventArgs e)
		{
			progressBarMainProgress.Value = (int) e.Percent;
			labelMainMessage.Text = e.Message;
			labelMainProgress.Text = (e.Current < 0 ? string.Empty : e.Current + "/" + e.Max);
		}

		private void statusChangedHandler_SubProgressChanged(object sender, ProgressEventArgs e)
		{
			progressBarSubProgress.Value = (int) e.Percent;
			labelSubMessage.Text = e.Message;
			labelSubProgress.Text = (e.Current < 0 ? string.Empty : (int) e.Percent + "%");
		}
	}
}
