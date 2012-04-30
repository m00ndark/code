using System;
using System.Linq;
using System.Windows.Forms;
using MediaGalleryExplorerCore;
using MediaGalleryExplorerCore.DataAccess;
using MediaGalleryExplorerCore.DataObjects;
using MediaGalleryExplorerCore.EventArguments;
using MediaGalleryExplorerCore.Workers;
using MediaGalleryExplorerUI.Controls;

namespace MediaGalleryExplorerUI.Forms
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		#region GUI event handlers

		private void MainForm_Load(object sender, EventArgs e)
		{
			toolStripMenuItemSource.Enabled = false;
			ObjectPool.Initialize();
			RegistryHandler.LoadSettings();
			GalleryWorker.StatusUpdated += GalleryWorker_StatusUpdated;
		}

		private void ToolStripMenuItemGalleryNew_Click(object sender, EventArgs e)
		{
			try
			{
				NewGalleryForm newGalleryForm = new NewGalleryForm();
				if (newGalleryForm.ShowDialog(this) == DialogResult.OK)
				{
					CreateTab(CreateContainer(newGalleryForm.Gallery));
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		private void ToolStripMenuItemGalleryOpen_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog fileDialog = new OpenFileDialog()
				{
					CheckPathExists = true,
					CheckFileExists = false,
					SupportMultiDottedExtensions = true,
					AddExtension = true,
					DefaultExt = "mgdb",
					Filter = "Media Gallery Database (*.mgdb)|*.mgdb",
					Title = "Media Gallery Database"
				};
				if (fileDialog.ShowDialog(this) == DialogResult.OK)
				{
					GalleryContainer galleryContainer = CreateContainer(new Gallery(fileDialog.FileName));
					galleryContainer.Worker.LoadGallery();
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		private void ToolStripMenuItemGalleryClose_Click(object sender, EventArgs e)
		{
			try
			{
				TabPage selectedTab = tabControlGalleries.SelectedTab;
				if (selectedTab != null)
				{
					DisposeTab(selectedTab);
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		private void ToolStripMenuItemGalleryAddSource_Click(object sender, EventArgs e)
		{
			try
			{
				TabPage selectedTab = tabControlGalleries.SelectedTab;
				if (selectedTab != null)
				{
					GalleryContainer galleryContainer = (GalleryContainer) selectedTab.Tag;
					if (galleryContainer != null)
						galleryContainer.Worker.AddSource();
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		private void ToolStripMenuItemSourceScan_Click(object sender, EventArgs e)
		{
			try
			{
				TabPage selectedTab = tabControlGalleries.SelectedTab;
				if (selectedTab != null)
				{
					GalleryContainer galleryContainer = (GalleryContainer) selectedTab.Tag;
					if (galleryContainer != null)
						galleryContainer.ScanSource(false);
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		private void ToolStripMenuItemSourceRescan_Click(object sender, EventArgs e)
		{
			try
			{
				TabPage selectedTab = tabControlGalleries.SelectedTab;
				if (selectedTab != null)
				{
					GalleryContainer galleryContainer = (GalleryContainer) selectedTab.Tag;
					if (galleryContainer != null)
						galleryContainer.ScanSource(true);
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		#endregion

		#region GalleryWorker event handlers

		private void GalleryWorker_StatusUpdated(object sender, StringEventArgs e)
		{
			GalleryContainer_StatusUpdated(sender, e);
		}

		#endregion

		#region GalleryContainer event handlers

		private void GalleryContainer_GalleryOpened(object sender, EventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<EventArgs>(GalleryContainer_GalleryOpened), new object[] { sender, e });
				}
				else
				{
					CreateTab((GalleryContainer) sender);
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		private void GalleryContainer_StatusUpdated(object sender, StringEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<StringEventArgs>(GalleryContainer_StatusUpdated), new object[] { sender, e });
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

		private void GalleryContainer_TreeSelectionChanged(object sender, BooleanEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<BooleanEventArgs>(GalleryContainer_TreeSelectionChanged), new object[] { sender, e });
				}
				else
				{
					toolStripMenuItemSource.Enabled = e.Value;
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		#endregion

		#region Helpers

		private GalleryContainer CreateContainer(Gallery gallery)
		{
			GalleryContainer galleryContainer = new GalleryContainer(gallery, imageList);
			galleryContainer.GalleryOpened += GalleryContainer_GalleryOpened;
			galleryContainer.StatusUpdated += GalleryContainer_StatusUpdated;
			galleryContainer.TreeSelectionChanged += GalleryContainer_TreeSelectionChanged;
			return galleryContainer;
		}

		private void CreateTab(GalleryContainer galleryContainer)
		{
			tabControlGalleries.TabPages.Add(galleryContainer.Gallery.ID, galleryContainer.Gallery.Name);
			tabControlGalleries.TabPages[galleryContainer.Gallery.ID].Tag = galleryContainer;
			tabControlGalleries.TabPages[galleryContainer.Gallery.ID].Controls.Add(galleryContainer);
			HandleTabControlVisibility();
			galleryContainer.Size = tabControlGalleries.TabPages[galleryContainer.Gallery.ID].ClientSize;
		}

		private void DisposeTab(TabPage tab)
		{
			GalleryContainer galleryContainer = (GalleryContainer) tab.Tag;
			if (galleryContainer != null)
			{
				galleryContainer.GalleryOpened -= GalleryContainer_GalleryOpened;
				galleryContainer.StatusUpdated -= GalleryContainer_StatusUpdated;
				galleryContainer.TreeSelectionChanged -= GalleryContainer_TreeSelectionChanged;
			}
			tabControlGalleries.TabPages.Remove(tab);
			HandleTabControlVisibility();
		}

		private void HandleTabControlVisibility()
		{
			tabControlGalleries.Visible = (tabControlGalleries.TabPages.Count > 0);
		}

		#endregion
	}
}
