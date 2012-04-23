using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using MediaGalleryExplorerCore.DataObjects;
using MediaGalleryExplorerCore.DataObjects.Properties;
using MediaGalleryExplorerCore.EventArguments;
using MediaGalleryExplorerCore.Workers;
using MediaGalleryExplorerUI.Forms;

namespace MediaGalleryExplorerUI.Controls
{
	public class GalleryContainer : UserControl
	{
		private SplitContainer _splitContainerVertical;
		private TreeView _treeView;
		private FlowLayoutPanel _flowLayoutPanel;
		private SplitContainer _splitContainerHorizontal;
		private PropertyGrid _propertyGridMediaFile;
		private ContextMenuStrip _contextMenuStripThumbnail;
		private ToolStripMenuItem _toolStripMenuItemThumbnailOpenImage;
		private ToolStripMenuItem _toolStripMenuItemThumbnailOpenPreview;
		private ToolStripMenuItem _toolStripMenuItemThumbnailPlayVideo;
		private  ImageList _imageList;
		private readonly IDictionary<MediaFolder, TreeNode> _folderCollection;
		private readonly IDictionary<MediaFile, ThumbnailContainer> _fileCollection;
		private readonly GalleryWorker _worker;

		public event EventHandler<EventArgs> GalleryOpened;
		public event EventHandler<StringEventArgs> StatusUpdated;
		public event EventHandler<BooleanEventArgs> TreeSelectionChanged;

		public GalleryContainer(Gallery gallery, ImageList imageList)
		{
			_imageList = imageList;
			_folderCollection = new Dictionary<MediaFolder, TreeNode>();
			_fileCollection = new Dictionary<MediaFile, ThumbnailContainer>();
			CommonWorker.ShowMessage += CommonWorker_ShowMessage;
			_worker = new GalleryWorker(gallery);
			_worker.GalleryLoaded += GalleryWorker_GalleryLoaded;
			_worker.StatusUpdated += GalleryWorker_StatusUpdated;
			_worker.TreeNodeAdded += GalleryWorker_TreeNodeAdded;
			_worker.TreeNodeRemoved += GalleryWorker_TreeNodeRemoved;
			_worker.ThumbnailAvailable += GalleryWorker_ThumbnailAvailable;
			_worker.DatabaseOperationCompleted += GalleryWorker_DatabaseOperationCompleted;
			InitializeComponent();
		}

		#region Properties

		public GalleryWorker Worker { get { return _worker; } }
		public Gallery Gallery { get { return Worker.Gallery; } }

		#endregion

		#region Initialization

		private void InitializeComponent()
		{
			_splitContainerVertical = new SplitContainer();
			_splitContainerHorizontal = new SplitContainer();
			_treeView = new TreeView();
			_propertyGridMediaFile = new PropertyGrid();
			_flowLayoutPanel = new FlowLayoutPanel();
			_contextMenuStripThumbnail = new ContextMenuStrip();
			_toolStripMenuItemThumbnailOpenImage = new ToolStripMenuItem();
			_toolStripMenuItemThumbnailOpenPreview = new ToolStripMenuItem();
			_toolStripMenuItemThumbnailPlayVideo = new ToolStripMenuItem();
			//_imageList = new ImageList();

			((System.ComponentModel.ISupportInitialize) (_splitContainerVertical)).BeginInit();
			_splitContainerVertical.Panel1.SuspendLayout();
			_splitContainerVertical.Panel2.SuspendLayout();
			_splitContainerVertical.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (_splitContainerHorizontal)).BeginInit();
			_splitContainerHorizontal.Panel1.SuspendLayout();
			_splitContainerHorizontal.Panel2.SuspendLayout();
			_splitContainerHorizontal.SuspendLayout();
			_contextMenuStripThumbnail.SuspendLayout();
			SuspendLayout();

			// 
			// splitContainerVertical
			// 
			_splitContainerVertical.Dock = DockStyle.Fill;
			_splitContainerVertical.FixedPanel = FixedPanel.Panel1;
			_splitContainerVertical.Location = new Point(0, 0);
			_splitContainerVertical.Margin = new Padding(0);
			_splitContainerVertical.Name = "_splitContainerVertical";
			_splitContainerVertical.Size = new Size(1536, 899);
			_splitContainerVertical.SplitterDistance = 317;
			_splitContainerVertical.TabIndex = 4;
			// 
			// splitContainerVertical.Panel1
			// 
			_splitContainerVertical.Panel1.Controls.Add(_splitContainerHorizontal);
			// 
			// splitContainerVertical.Panel2
			// 
			_splitContainerVertical.Panel2.Controls.Add(_flowLayoutPanel);
			// 
			// splitContainerHorizontal
			// 
			_splitContainerHorizontal.Dock = DockStyle.Fill;
			_splitContainerHorizontal.FixedPanel = FixedPanel.Panel2;
			_splitContainerHorizontal.Location = new Point(0, 0);
			_splitContainerHorizontal.Name = "_splitContainerHorizontal";
			_splitContainerHorizontal.Orientation = Orientation.Horizontal;
			_splitContainerHorizontal.Size = new Size(317, 899);
			_splitContainerHorizontal.SplitterDistance = 717;
			_splitContainerHorizontal.TabIndex = 1;
			// 
			// splitContainerHorizontal.Panel1
			// 
			_splitContainerHorizontal.Panel1.Controls.Add(_treeView);
			// 
			// splitContainerHorizontal.Panel2
			// 
			_splitContainerHorizontal.Panel2.Controls.Add(_propertyGridMediaFile);
			// 
			// treeView
			// 
			_treeView.BackColor = Color.Black;
			_treeView.Dock = DockStyle.Fill;
			_treeView.ForeColor = Color.White;
			_treeView.HideSelection = false;
			_treeView.ImageKey = "folder-16";
			_treeView.ImageList = _imageList;
			_treeView.LineColor = Color.DimGray;
			_treeView.Location = new Point(0, 0);
			_treeView.Name = "_treeView";
			_treeView.SelectedImageKey = "folder-16";
			_treeView.Size = new Size(317, 717);
			_treeView.TabIndex = 0;
			_treeView.Enter += TreeView_Enter;
			_treeView.AfterSelect += TreeView_AfterSelect;
			_treeView.BeforeExpand += TreeView_BeforeExpand;
			// 
			// propertyGridMediaFile
			// 
			_propertyGridMediaFile.BackColor = Color.Black;
			_propertyGridMediaFile.CategoryForeColor = Color.Silver;
			_propertyGridMediaFile.Dock = DockStyle.Fill;
			_propertyGridMediaFile.HelpBackColor = Color.Black;
			_propertyGridMediaFile.HelpVisible = false;
			_propertyGridMediaFile.LineColor = Color.FromArgb(64, 64, 64);
			_propertyGridMediaFile.Location = new Point(0, 0);
			_propertyGridMediaFile.Name = "_propertyGridMediaFile";
			_propertyGridMediaFile.Size = new Size(317, 178);
			_propertyGridMediaFile.TabIndex = 0;
			_propertyGridMediaFile.ToolbarVisible = false;
			_propertyGridMediaFile.ViewBackColor = Color.Black;
			_propertyGridMediaFile.ViewForeColor = Color.White;
			// 
			// flowLayoutPanel
			// 
			_flowLayoutPanel.AutoScroll = true;
			_flowLayoutPanel.BackColor = Color.Black;
			_flowLayoutPanel.Dock = DockStyle.Fill;
			_flowLayoutPanel.Location = new Point(0, 0);
			_flowLayoutPanel.Name = "_flowLayoutPanel";
			_flowLayoutPanel.Size = new Size(1215, 899);
			_flowLayoutPanel.TabIndex = 0;
			// 
			// contextMenuStripThumbnail
			// 
			_contextMenuStripThumbnail.Items.AddRange(new ToolStripItem[] {
            _toolStripMenuItemThumbnailOpenImage,
            _toolStripMenuItemThumbnailOpenPreview,
            _toolStripMenuItemThumbnailPlayVideo});
			_contextMenuStripThumbnail.Name = "_contextMenuStripThumbnail";
			_contextMenuStripThumbnail.Size = new Size(148, 70);
			// 
			// toolStripMenuItemThumbnailOpenImage
			// 
			_toolStripMenuItemThumbnailOpenImage.Name = "_toolStripMenuItemThumbnailOpenImage";
			_toolStripMenuItemThumbnailOpenImage.Size = new Size(147, 22);
			_toolStripMenuItemThumbnailOpenImage.Text = "Open Image";
			_toolStripMenuItemThumbnailOpenImage.Click += ToolStripMenuItemThumbnailOpenImage_Click;
			// 
			// toolStripMenuItemThumbnailOpenPreview
			// 
			_toolStripMenuItemThumbnailOpenPreview.Name = "_toolStripMenuItemThumbnailOpenPreview";
			_toolStripMenuItemThumbnailOpenPreview.Size = new Size(147, 22);
			_toolStripMenuItemThumbnailOpenPreview.Text = "Open Preview";
			_toolStripMenuItemThumbnailOpenPreview.Click += ToolStripMenuItemThumbnailOpenPreview_Click;
			// 
			// toolStripMenuItemThumbnailPlayVideo
			// 
			_toolStripMenuItemThumbnailPlayVideo.Name = "_toolStripMenuItemThumbnailPlayVideo";
			_toolStripMenuItemThumbnailPlayVideo.Size = new Size(147, 22);
			_toolStripMenuItemThumbnailPlayVideo.Text = "Play Video";
			_toolStripMenuItemThumbnailPlayVideo.Click += ToolStripMenuItemThumbnailPlayVideo_Click;

			Controls.Add(_splitContainerVertical);
			Font = new Font("Segoe UI", 9F);
			Name = "GalleryContainer";
			Size = new Size(1536, 899);
			Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
			Load += GalleryContainer_Load;

			_splitContainerVertical.Panel1.ResumeLayout(false);
			_splitContainerVertical.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) (_splitContainerVertical)).EndInit();
			_splitContainerVertical.ResumeLayout(false);
			_splitContainerHorizontal.Panel1.ResumeLayout(false);
			_splitContainerHorizontal.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) (_splitContainerHorizontal)).EndInit();
			_splitContainerHorizontal.ResumeLayout(false);
			_contextMenuStripThumbnail.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		#region GUI event handlers

		private void GalleryContainer_Load(object sender, EventArgs e)
		{
			SetPropertyGridColumnWidth();
		}

		private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			try
			{
				MediaFolder folder = (MediaFolder) e.Node.Tag;
				RemoveAllThumbnails();
				UpdateProperties(folder);
				CreateThumbnails(folder);
				_worker.LoadThumbnails(folder);
				RaiseTreeSelectionChangedEvent(folder.Parent == null);
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(ParentForm, ex);
			}
		}

		private void TreeView_Enter(object sender, EventArgs e)
		{
			if (_treeView.SelectedNode != null)
			{
				MediaFolder folder = (MediaFolder) _treeView.SelectedNode.Tag;
				UpdateProperties(folder);
			}
		}

		private void TreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			_worker.FolderExpanded((MediaFolder) e.Node.Tag);
		}

		private void ToolStripMenuItemThumbnailOpenImage_Click(object sender, EventArgs e)
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

		private void ToolStripMenuItemThumbnailOpenPreview_Click(object sender, EventArgs e)
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

		private void ToolStripMenuItemThumbnailPlayVideo_Click(object sender, EventArgs e)
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

		#region Worker event handlers

		private void GalleryWorker_GalleryLoaded(object sender, EventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<EventArgs>(GalleryWorker_GalleryLoaded), new object[] { sender, e });
				}
				else
				{
					RaiseGalleryOpenedEvent();
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(ParentForm, ex);
			}
		}

		private object CommonWorker_ShowMessage(object sender, MessageEventArgs e)
		{
			if (InvokeRequired)
				return Invoke(new CommonWorker.EventHandler<MessageEventArgs>(CommonWorker_ShowMessage), new object[] { sender, e });

			return FormUtilities.ShowMessage(ParentForm, e.Message, e.Buttons, e.Icon);
		}

		private void GalleryWorker_StatusUpdated(object sender, StringEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<StringEventArgs>(GalleryWorker_StatusUpdated), new object[] { sender, e });
				}
				else
				{
					RaiseStatusUpdatedEvent(e.Value);
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(ParentForm, ex);
			}
		}

		private void GalleryWorker_TreeNodeAdded(object sender, MediaFolderEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<MediaFolderEventArgs>(GalleryWorker_TreeNodeAdded), new object[] { sender, e });
				}
				else
				{
					if (!_folderCollection.ContainsKey(e.Folder))
					{
						if (e.Folder.Parent != null)
						{
							if (_folderCollection.ContainsKey(e.Folder.Parent))
							{
								TreeNode parentNode = _folderCollection[e.Folder.Parent];
								TreeNode onlyChildNode = (parentNode.Nodes.Count == 1 ? parentNode.Nodes[0] : null);
								TreeNode node = parentNode.Nodes.Add(e.Folder.Name + " (" + e.Folder.TotalImageCount + ":" + e.Folder.TotalVideoCount + ")");
								node.Tag = e.Folder;
								if (!e.Folder.IsDummy)
								{
									_folderCollection.Add(e.Folder, node);
								}
								if (onlyChildNode != null && ((MediaFolder) onlyChildNode.Tag).IsDummy)
								{
									parentNode.Nodes.Remove(onlyChildNode);
								}
							}
						}
						else
						{
							TreeNode node = _treeView.Nodes.Add(e.Folder.Source.DisplayPath);
							//TreeNode node = _treeView.Nodes.Add(e.Folder.Name);
							node.Tag = e.Folder;
							_folderCollection.Add(e.Folder, node);
						}
					}
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(ParentForm, ex);
			}
		}

		private void GalleryWorker_TreeNodeRemoved(object sender, MediaFolderEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<MediaFolderEventArgs>(GalleryWorker_TreeNodeRemoved), new object[] { sender, e });
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
						_treeView.Nodes.Remove(_folderCollection[e.Folder]);
						_folderCollection.Remove(e.Folder);
					}
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(ParentForm, ex);
			}
		}

		private void GalleryWorker_ThumbnailAvailable(object sender, MediaFileEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<MediaFileEventArgs>(GalleryWorker_ThumbnailAvailable), new object[] { sender, e });
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
				FormUtilities.ShowError(ParentForm, ex);
			}
		}

		private void GalleryWorker_DatabaseOperationCompleted(object sender, OperationTypeEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<OperationTypeEventArgs>(GalleryWorker_DatabaseOperationCompleted), new object[] { sender, e });
				}
				else
				{
					if (e.OperationType == GalleryWorker.OperationType.LoadGallery || e.OperationType == GalleryWorker.OperationType.ScanSource)
					{
						UpdateTreeNodeFileTypeCounts();
						_treeView.Sort();
					}
					RaiseStatusUpdatedEvent("Ready");
					//EnableControls(true, true);
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(ParentForm, ex);
			}
		}

		#endregion

		#region ThumbnailContainer event handlers

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

		#region Event raisers

		private void RaiseGalleryOpenedEvent()
		{
			if (GalleryOpened != null)
			{
				GalleryOpened(this, new EventArgs());
			}
		}

		private void RaiseStatusUpdatedEvent(string status)
		{
			if (StatusUpdated != null)
			{
				StatusUpdated(this, new StringEventArgs(status));
			}
		}

		private void RaiseTreeSelectionChangedEvent(bool sourceRootFolderSelected)
		{
			if (TreeSelectionChanged != null)
			{
				TreeSelectionChanged(this, new BooleanEventArgs(sourceRootFolderSelected));
			}
		}

		#endregion

		#region Operations

		public void ScanSource(bool reScan)
		{
			if (_treeView.SelectedNode != null)
			{
				MediaFolder folder = (MediaFolder) _treeView.SelectedNode.Tag;
				if (folder.Parent == null)
				{
					RemoveTreeNodes(folder.Source.RootFolder);
					_worker.ScanSource(folder.Source, reScan);
				}
			}
		}

		#endregion

		#region Helpers

		private void UpdateTreeNodeFileTypeCounts()
		{
			foreach (KeyValuePair<MediaFolder, TreeNode> pair in _folderCollection)
			{
				string name = (pair.Key.Parent == null ? pair.Key.Source.DisplayPath : pair.Key.Name);
				pair.Value.Text = name + " (" + pair.Key.TotalImageCount + ":" + pair.Key.TotalVideoCount + ")";
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
				else if (_treeView.Nodes.Contains(node))
					_treeView.Nodes.Remove(node);
				_folderCollection.Remove(folder);
			}
		}

		private void RemoveAllThumbnails()
		{
			foreach (Control control in _flowLayoutPanel.Controls)
			{
				if (control is ThumbnailContainer)
				{
					((ThumbnailContainer) control).ThumbnailClicked -= ThumbnailContainer_Click;
					((ThumbnailContainer) control).ThumbnailDoubleClicked -= ThumbnailContainer_DoubleClick;
					((ThumbnailContainer) control).ThumbnailGotFocus -= ThumbnailContainer_GotFocus;
				}
			}
			_toolStripMenuItemThumbnailOpenImage.Tag = null;
			_toolStripMenuItemThumbnailOpenPreview.Tag = null;
			_toolStripMenuItemThumbnailPlayVideo.Tag = null;
			_flowLayoutPanel.Controls.Clear();
			_fileCollection.Clear();
			DeselectThumbnail();
		}

		private void CreateThumbnails(MediaFolder folder)
		{
			_flowLayoutPanel.SuspendLayout();
			foreach (MediaFile mediaFile in folder.Files)
			{
				ThumbnailContainer thumbnailContainer = new ThumbnailContainer(mediaFile);
				thumbnailContainer.ThumbnailClicked += ThumbnailContainer_Click;
				thumbnailContainer.ThumbnailDoubleClicked += ThumbnailContainer_DoubleClick;
				thumbnailContainer.ThumbnailGotFocus += ThumbnailContainer_GotFocus;
				_fileCollection.Add(mediaFile, thumbnailContainer);
				_flowLayoutPanel.Controls.Add(thumbnailContainer);
			}
			_flowLayoutPanel.ResumeLayout(true);
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
			_propertyGridMediaFile.SelectedObject = null;
		}

		private void ShowThumbnailContextMenu(ThumbnailContainer thumbnailContainer, Point point)
		{
			_toolStripMenuItemThumbnailOpenImage.Visible = (thumbnailContainer.MediaFile is ImageFile);
			_toolStripMenuItemThumbnailOpenImage.Enabled = thumbnailContainer.MediaFile.Exists();
			_toolStripMenuItemThumbnailOpenImage.Tag = thumbnailContainer;
			_toolStripMenuItemThumbnailOpenPreview.Visible = (thumbnailContainer.MediaFile is VideoFile);
			_toolStripMenuItemThumbnailOpenPreview.Tag = thumbnailContainer;
			_toolStripMenuItemThumbnailPlayVideo.Visible = (thumbnailContainer.MediaFile is VideoFile);
			_toolStripMenuItemThumbnailPlayVideo.Enabled = thumbnailContainer.MediaFile.Exists();
			_toolStripMenuItemThumbnailPlayVideo.Tag = thumbnailContainer;
			_contextMenuStripThumbnail.Show(point);
		}

		private void UpdateProperties(FileSystemEntry fileSystemEntry)
		{
			_propertyGridMediaFile.SelectedObject =
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
			_propertyGridMediaFile.ExpandAllGridItems();
		}

		private void SetPropertyGridColumnWidth()
		{
			Type propertyGridType = _propertyGridMediaFile.GetType();
			FieldInfo fieldInfo = propertyGridType.GetField("gridView", BindingFlags.NonPublic | BindingFlags.Instance);
			object gridView = fieldInfo.GetValue(_propertyGridMediaFile);
			Type gridViewType = gridView.GetType();
			MethodInfo methodInfo = gridViewType.GetMethod("MoveSplitterTo", BindingFlags.NonPublic | BindingFlags.Instance);
			methodInfo.Invoke(gridView, new object[] { 100 });
		}

		#endregion
	}
}
