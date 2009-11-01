using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using MediaGallery.DataObjects;
using MediaGallery.DataObjects.Properties;
using MediaGallery.EventArguments;
using MediaGallery.Forms.Controls;
using MediaGallery.Workers;

namespace MediaGallery.Forms
{
	public partial class MainForm : Form
	{
		private readonly IDictionary<MediaFolder, TreeNode> _folderCollection;
		private readonly IDictionary<MediaFile, ThumbnailContainer> _fileCollection;
		private readonly MainWorker _worker;

		public MainForm()
		{
			InitializeComponent();
			CommonWorker.ShowMessage += CommonWorker_ShowMessage;
			_folderCollection = new Dictionary<MediaFolder, TreeNode>();
			_fileCollection = new Dictionary<MediaFile, ThumbnailContainer>();
			_worker = new MainWorker();
			_worker.StatusUpdated += MainWorker_StatusUpdated;
			_worker.SourceListUpdated += MainWorker_SourceListUpdated;
			_worker.TreeNodeAdded += MainWorker_TreeNodeAdded;
			_worker.TreeNodeRemoved += MainWorker_TreeNodeRemoved;
			_worker.ThumbnailAvailable += MainWorker_ThumbnailAvailable;
			_worker.DatabaseOperationCompleted += MainWorker_DatabaseOperationCompleted;
		}

		#region Worker event handlers

		private object CommonWorker_ShowMessage(object sender, MessageEventArgs e)
		{
			if (InvokeRequired)
				return Invoke(new CommonWorker.EventHandler<MessageEventArgs>(CommonWorker_ShowMessage), new object[] { sender, e });

			return FormUtilities.ShowMessage(this, e.Message, e.Buttons, e.Icon);
		}

		private void MainWorker_StatusUpdated(object sender, StringEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<StringEventArgs>(MainWorker_StatusUpdated), new object[] { sender, e });
				}
				else
				{
					toolStripStatusLabelStatus.Text = e.Value;
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		private void MainWorker_SourceListUpdated(object sender, SourceListEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<SourceListEventArgs>(MainWorker_SourceListUpdated), new object[] { sender, e });
				}
				else
				{
					toolStripComboBoxSource.BeginUpdate();
					toolStripComboBoxSource.Items.Clear();
					foreach (GallerySource source in e.Sources)
					{
						toolStripComboBoxSource.Items.Add(new ComboBoxItem(source.Path, source));
					}
					toolStripComboBoxSource.EndUpdate();
					EnableControls(true, true);
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		private void MainWorker_TreeNodeAdded(object sender, MediaFolderEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<MediaFolderEventArgs>(MainWorker_TreeNodeAdded), new object[] { sender, e });
				}
				else
				{
					if (e.Folder.Parent != null)
					{
						if (_folderCollection.ContainsKey(e.Folder.Parent))
						{
							TreeNode parentNode = _folderCollection[e.Folder.Parent];
							TreeNode node = parentNode.Nodes.Add(e.Folder.Name);
							node.Tag = e.Folder;
							_folderCollection.Add(e.Folder, node);
						}
					}
					else
					{
						TreeNode node = treeView.Nodes.Add(e.Folder.Name);
						node.Tag = e.Folder;
						_folderCollection.Add(e.Folder, node);
					}
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		private void MainWorker_TreeNodeRemoved(object sender, MediaFolderEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<MediaFolderEventArgs>(MainWorker_TreeNodeRemoved), new object[] { sender, e });
				}
				else
				{
					if (e.Folder.Parent != null)
					{
						if (_folderCollection.ContainsKey(e.Folder.Parent))
						{
							TreeNode parentNode = _folderCollection[e.Folder.Parent];
							parentNode.Nodes.Remove(_folderCollection[e.Folder]);
							_folderCollection.Remove(e.Folder);
						}
					}
					else
					{
						treeView.Nodes.Remove(_folderCollection[e.Folder]); ;
						_folderCollection.Remove(e.Folder);
					}
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		private void MainWorker_ThumbnailAvailable(object sender, MediaFileEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<MediaFileEventArgs>(MainWorker_ThumbnailAvailable), new object[] { sender, e });
				}
				else
				{
					if (e.File.ThumbnailImage != null && _fileCollection.ContainsKey(e.File))
					{
						ThumbnailContainer thumbnailContainer = _fileCollection[e.File];
						thumbnailContainer.SetThumbnail(e.File.ThumbnailImage, new Point(10 + (200 - e.File.ThumbnailImage.Size.Width) / 2, 10));
					}
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		private void MainWorker_DatabaseOperationCompleted(object sender, OperationTypeEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<OperationTypeEventArgs>(MainWorker_DatabaseOperationCompleted), new object[] { sender, e });
				}
				else
				{
					if (e.OperationType == MainWorker.OperationType.LoadSources || e.OperationType == MainWorker.OperationType.ScanSource)
					{
						UpdateTreeNodeFileTypeCounts();
						treeView.Sort();
					}
					toolStripStatusLabelStatus.Text = "Ready";
					EnableControls(true, true);
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		#endregion

		#region GUI event handlers

		private void MainForm_Load(object sender, EventArgs e)
		{
			try
			{
				treeView.Sort();
				EnableControls(true, false);
				_worker.Initialize();
				_worker.LoadSources();
				SetPropertyGridColumnWidth();
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				EnableControls(false, false);
				_worker.CleanUp();
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		private void toolStripButtonSettings_Click(object sender, EventArgs e)
		{
			SettingsForm settingsForm = new SettingsForm();
			settingsForm.ShowDialog(this);
			_worker.Initialize();
		}

		private void toolStripMenuItemSourceRescan_Click(object sender, EventArgs e)
		{
			try
			{
				StartScan(true);
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		private void toolStripMenuItemSourceUpdate_Click(object sender, EventArgs e)
		{
			try
			{
				StartScan(false);
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		private void toolStripComboBoxSource_SelectedIndexChanged(object sender, EventArgs e)
		{
			EnableControls(true, true);
		}

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			try
			{
				MediaFolder folder = (MediaFolder) e.Node.Tag;
				RemoveAllThumbnails();
				UpdateProperties(folder);
				CreateThumbnails(folder);
				_worker.LoadThumbnails(folder);
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		#region Thumbnail context menu

		private void toolStripMenuItemThumbnailOpenImage_Click(object sender, EventArgs e)
		{
			ToolStripItem toolStripItem = sender as ToolStripItem;
			if (toolStripItem != null)
			{
				ThumbnailContainer thumbnailContainer = toolStripItem.Tag as ThumbnailContainer;
				if (thumbnailContainer != null)
				{
					_worker.OpenMediaFile(thumbnailContainer.MediaFile);
				}
			}
		}

		private void toolStripMenuItemThumbnailOpenPreview_Click(object sender, EventArgs e)
		{
			ToolStripItem toolStripItem = sender as ToolStripItem;
			if (toolStripItem != null)
			{
				ThumbnailContainer thumbnailContainer = toolStripItem.Tag as ThumbnailContainer;
				if (thumbnailContainer != null)
				{
					_worker.OpenMediaFile(thumbnailContainer.MediaFile, true);
				}
			}
		}

		private void toolStripMenuItemThumbnailPlayVideo_Click(object sender, EventArgs e)
		{
			ToolStripItem toolStripItem = sender as ToolStripItem;
			if (toolStripItem != null)
			{
				ThumbnailContainer thumbnailContainer = toolStripItem.Tag as ThumbnailContainer;
				if (thumbnailContainer != null)
				{
					_worker.OpenMediaFile(thumbnailContainer.MediaFile);
				}
			}
		}

		#endregion

		#endregion

		#region Custom event handlers

		private void ThumbnailContainer_Click(object sender, MouseEventArgs e)
		{
			ThumbnailContainer thumbnailContainer = sender as ThumbnailContainer;
			if (thumbnailContainer != null)
			{
				thumbnailContainer.Focus();
				if (e.Button == MouseButtons.Right)
				{
					ShowThumbnailContextMenu(thumbnailContainer, e.Location);
				}
			}
		}

		private void ThumbnailContainer_DoubleClick(object sender, EventArgs e)
		{
			ThumbnailContainer thumbnailContainer = sender as ThumbnailContainer;
			if (thumbnailContainer != null)
			{
				_worker.OpenMediaFile(thumbnailContainer.MediaFile, true);
			}
		}

		private void ThumbnailContainer_GotFocus(object sender, EventArgs e)
		{
			ThumbnailContainer thumbnailContainer = sender as ThumbnailContainer;
			if (thumbnailContainer != null)
			{
				SelectThumbnail(thumbnailContainer);
				UpdateProperties(thumbnailContainer.MediaFile);
			}
		}

		#endregion

		#region Helpers

		private void EnableControls(bool enable, bool enableDatabaseOperations)
		{
			toolStripMenuItemSourceRescan.Enabled = (enable && enableDatabaseOperations && toolStripComboBoxSource.SelectedIndex > -1);
			toolStripMenuItemSourceUpdate.Enabled = (enable && enableDatabaseOperations && toolStripComboBoxSource.SelectedIndex > -1);
		}

		private void StartScan(bool reScan)
		{
			if (toolStripComboBoxSource.SelectedIndex > -1)
			{
				EnableControls(true, false);
				GallerySource source = (GallerySource) ((ComboBoxItem) toolStripComboBoxSource.SelectedItem).Tag;
				RemoveTreeNodes(source.RootFolder);
				_worker.ScanSource(source, reScan);
			}
		}

		private void UpdateTreeNodeFileTypeCounts()
		{
			foreach (KeyValuePair<MediaFolder, TreeNode> pair in _folderCollection)
			{
				pair.Value.Text = pair.Key.Name + " (" + pair.Key.TotalImageCount + "/" + pair.Key.TotalVideoCount + ")";
			}
		}

		private void RemoveTreeNodes(MediaFolder folder)
		{
			if (folder != null && _folderCollection.ContainsKey(folder))
			{
				foreach (MediaFolder subFolder in folder.SubFolders)
				{
					RemoveTreeNodes(subFolder);
				}
				TreeNode node = _folderCollection[folder];
				if (node.Parent != null && node.Parent.Nodes.Contains(node))
					node.Parent.Nodes.Remove(node);
				else if (treeView.Nodes.Contains(node))
					treeView.Nodes.Remove(node);
				_folderCollection.Remove(folder);
			}
		}

		private void RemoveAllThumbnails()
		{
			foreach (Control control in flowLayoutPanel.Controls)
			{
				if (control is ThumbnailContainer)
				{
					((ThumbnailContainer) control).ThumbnailClicked -= ThumbnailContainer_Click;
					((ThumbnailContainer) control).ThumbnailDoubleClicked -= ThumbnailContainer_DoubleClick;
					((ThumbnailContainer) control).ThumbnailGotFocus -= ThumbnailContainer_GotFocus;
				}
			}
			toolStripMenuItemThumbnailOpenImage.Tag = null;
			toolStripMenuItemThumbnailOpenPreview.Tag = null;
			toolStripMenuItemThumbnailPlayVideo.Tag = null;
			flowLayoutPanel.Controls.Clear();
			_fileCollection.Clear();
			DeselectThumbnail();
		}

		private void CreateThumbnails(MediaFolder folder)
		{
			flowLayoutPanel.SuspendLayout();
			foreach (MediaFile mediaFile in folder.Files)
			{
				ThumbnailContainer thumbnailContainer = new ThumbnailContainer(mediaFile);
				thumbnailContainer.ThumbnailClicked += ThumbnailContainer_Click;
				thumbnailContainer.ThumbnailDoubleClicked += ThumbnailContainer_DoubleClick;
				thumbnailContainer.ThumbnailGotFocus += ThumbnailContainer_GotFocus;
				_fileCollection.Add(mediaFile, thumbnailContainer);
				flowLayoutPanel.Controls.Add(thumbnailContainer);
			}
			flowLayoutPanel.ResumeLayout(true);
		}

		private void SelectThumbnail(ThumbnailContainer thumbnailContainer)
		{
			if (_worker.SelectedFile != null && _fileCollection.ContainsKey(_worker.SelectedFile))
			{
				ThumbnailContainer oldThumbnailContainer = _fileCollection[_worker.SelectedFile];
				oldThumbnailContainer.Deselect();
			}
			thumbnailContainer.Select();
			_worker.SelectedFile = thumbnailContainer.MediaFile;
		}

		private void DeselectThumbnail()
		{
			_worker.SelectedFile = null;
			propertyGridMediaFile.SelectedObject = null;
		}

		private void ShowThumbnailContextMenu(ThumbnailContainer thumbnailContainer, Point point)
		{
			toolStripMenuItemThumbnailOpenImage.Visible = (thumbnailContainer.MediaFile is ImageFile);
			toolStripMenuItemThumbnailOpenImage.Enabled = thumbnailContainer.MediaFile.Exists();
			toolStripMenuItemThumbnailOpenImage.Tag = thumbnailContainer;
			toolStripMenuItemThumbnailOpenPreview.Visible = (thumbnailContainer.MediaFile is VideoFile);
			toolStripMenuItemThumbnailOpenPreview.Tag = thumbnailContainer;
			toolStripMenuItemThumbnailPlayVideo.Visible = (thumbnailContainer.MediaFile is VideoFile);
			toolStripMenuItemThumbnailPlayVideo.Enabled = thumbnailContainer.MediaFile.Exists();
			toolStripMenuItemThumbnailPlayVideo.Tag = thumbnailContainer;
			contextMenuStripThumbnail.Show(point);
		}

		private void UpdateProperties(FileSystemEntry fileSystemEntry)
		{
			propertyGridMediaFile.SelectedObject =
				(fileSystemEntry == null ? null
					: (fileSystemEntry is VideoFile ? new VideoFileProperties((VideoFile) fileSystemEntry)
						: (fileSystemEntry is ImageFile ? new ImageFileProperties((ImageFile) fileSystemEntry)
							: (fileSystemEntry is MediaFile ? new MediaFileProperties((MediaFile) fileSystemEntry)
								: (fileSystemEntry is MediaFolder ? new MediaFolderProperties((MediaFolder) fileSystemEntry)
									: (FileSystemEntryProperties) null
								)
							)
						)
					)
				);
			propertyGridMediaFile.ExpandAllGridItems();
		}

		private void SetPropertyGridColumnWidth()
		{
			Type propertyGridType = propertyGridMediaFile.GetType();
			FieldInfo fieldInfo = propertyGridType.GetField("gridView", BindingFlags.NonPublic | BindingFlags.Instance);
			object gridView = fieldInfo.GetValue(propertyGridMediaFile);
			Type gridViewType = gridView.GetType();
			MethodInfo methodInfo = gridViewType.GetMethod("MoveSplitterTo", BindingFlags.NonPublic | BindingFlags.Instance);
			methodInfo.Invoke(gridView, new object[] { 90 });
		}

		#endregion
	}
}
