using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MediaGallery.DataObjects;
using MediaGallery.EventArguments;
using MediaGallery.Workers;

namespace MediaGallery.Forms
{
	public partial class SettingsForm : Form
	{
		private readonly SettingsWorker _worker;

		public SettingsForm()
		{
			InitializeComponent();
			CommonWorker.ShowMessage += CommonWorker_ShowMessage;
			_worker = new SettingsWorker();
			_worker.DatabaseLocationUpdated += SettingsWorker_DatabaseLocationUpdated;
			_worker.WorkingDirectoryUpdated += SettingsWorker_WorkingDirectoryUpdated;
			_worker.VideoThumbnailsMakerUpdated += SettingsWorker_VideoThumbnailsMakerUpdated;
			_worker.VideoThumbnailsMakerPresetUpdated += SettingsWorker_VideoThumbnailsMakerPresetUpdated;
			_worker.SourceListUpdated += SettingsWorker_SourceListUpdated;
		}

		#region Worker event handlers

		private object CommonWorker_ShowMessage(object sender, MessageEventArgs e)
		{
			if (InvokeRequired)
				return Invoke(new CommonWorker.EventHandler<MessageEventArgs>(CommonWorker_ShowMessage), new object[] { sender, e });

			return FormUtilities.ShowMessage(this, e.Message, e.Buttons, e.Icon);
		}

		private void SettingsWorker_DatabaseLocationUpdated(object sender, StringEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<StringEventArgs>(SettingsWorker_DatabaseLocationUpdated), new object[] { sender, e });
				}
				else
				{
					textBoxDatabaseLocation.Text = e.Value;
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		private void SettingsWorker_WorkingDirectoryUpdated(object sender, StringEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<StringEventArgs>(SettingsWorker_WorkingDirectoryUpdated), new object[] { sender, e });
				}
				else
				{
					textBoxWorkingDirectory.Text = e.Value;
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		private void SettingsWorker_VideoThumbnailsMakerUpdated(object sender, StringEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<StringEventArgs>(SettingsWorker_VideoThumbnailsMakerUpdated), new object[] { sender, e });
				}
				else
				{
					textBoxVideoThumbnailsMaker.Text = e.Value;
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		private void SettingsWorker_VideoThumbnailsMakerPresetUpdated(object sender, StringEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<StringEventArgs>(SettingsWorker_VideoThumbnailsMakerPresetUpdated), new object[] { sender, e });
				}
				else
				{
					textBoxVideoThumbnailsMakerPreset.Text = e.Value;
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		private void SettingsWorker_SourceListUpdated(object sender, SourceListEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<SourceListEventArgs>(SettingsWorker_SourceListUpdated), new object[] { sender, e });
				}
				else
				{
					listViewSources.BeginUpdate();
					listViewSources.Items.Clear();
					foreach (GallerySource source in e.Sources)
					{
						ListViewItem item = listViewSources.Items.Add(source.Path);
						item.SubItems.Add(source.ImageCount.ToString());
						item.SubItems.Add(source.VideoCount.ToString());
						item.Tag = source;
					}
					listViewSources.EndUpdate();
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		#endregion

		#region GUI event handlers

		private void SettingsForm_Load(object sender, EventArgs e)
		{
			_worker.Initialize();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonBrowseDatabase_Click(object sender, EventArgs e)
		{
			_worker.SetDatabaseLocation();
		}

		private void buttonBrowserWorkingDirectory_Click(object sender, EventArgs e)
		{
			_worker.SetWorkingDirectory();
		}

		private void buttonBrowseVideoThumbnailsMaker_Click(object sender, EventArgs e)
		{
			_worker.SetVideoThumbnailsMaker();
		}

		private void buttonBrowseVideoThumbnailsMakerPreset_Click(object sender, EventArgs e)
		{
			_worker.SetVideoThumbnailsMakerPreset();
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			_worker.AddSource();
		}

		private void buttonRemove_Click(object sender, EventArgs e)
		{
			if (listViewSources.SelectedItems.Count > 0)
			{
				_worker.RemoveSource((GallerySource) listViewSources.SelectedItems[0].Tag);
			}
		}

		#endregion
	}
}
