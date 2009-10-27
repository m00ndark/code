using System;
using System.Drawing;
using System.Windows.Forms;
using MediaGallery.DataObjects;

namespace MediaGallery.Forms.Controls
{
	public class ThumbnailContainer : UserControl
	{
		private static readonly Color _selectedColor = Color.FromArgb(96, 96, 128);
		private static readonly Color _unselectedColor = Color.FromArgb(64, 64, 64);

		private PictureBox _pictureBoxThumbnail;
		private Label _labelFileName;

		public event EventHandler<MouseEventArgs> ThumbnailClicked;
		public event EventHandler<EventArgs> ThumbnailDoubleClicked;
		public event EventHandler<EventArgs> ThumbnailGotFocus;

		public ThumbnailContainer(MediaFile mediaFile)
		{
			InitializeComponent(mediaFile);
			MediaFile = mediaFile;
			GotFocus += ThumbnailContainer_GotFocus;
		}

		public void SetThumbnail(Image image, Point location)
		{
			_pictureBoxThumbnail.Image = image;
			_pictureBoxThumbnail.Location = location;
			_pictureBoxThumbnail.Visible = true;
		}

		public void Select()
		{
			SuspendLayout();
			BackColor = _selectedColor;
			_labelFileName.BackColor = _selectedColor;
			ResumeLayout(true);
		}

		public void Deselect()
		{
			SuspendLayout();
			BackColor = _unselectedColor;
			_labelFileName.BackColor = _unselectedColor;
			ResumeLayout(true);
		}

		#region Properties

		public MediaFile MediaFile { get; private set; }

		#endregion

		#region Initialization

		private void InitializeComponent(MediaFile mediaFile)
		{
			_pictureBoxThumbnail = new PictureBox();
			_labelFileName = new Label();

			SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (_pictureBoxThumbnail)).BeginInit();

			_labelFileName.Name = "_labelFileName";
			_labelFileName.Size = new Size(200, 13);
			_labelFileName.Location = new Point(10, 222);
			_labelFileName.BackColor = _unselectedColor;
			_labelFileName.ForeColor = Color.White;
			_labelFileName.TextAlign = ContentAlignment.TopCenter;
			_labelFileName.Text = mediaFile.Name;
			_labelFileName.MouseClick += Control_MouseClick;
			_labelFileName.DoubleClick += Control_DoubleClick;

			_pictureBoxThumbnail.Name = "_pictureBoxThumbnail";
			_pictureBoxThumbnail.Size = new Size(200, 200);
			_pictureBoxThumbnail.Location = new Point(10, 10);
			_pictureBoxThumbnail.Visible = false;
			_pictureBoxThumbnail.MouseClick += Control_MouseClick;
			_pictureBoxThumbnail.DoubleClick += Control_DoubleClick;

			Name = "ThumbnailContainer";
			Size = new Size(220, 245);
			BackColor = _unselectedColor;
			Controls.Add(_labelFileName);
			Controls.Add(_pictureBoxThumbnail);
			MouseClick += Control_MouseClick;
			DoubleClick += Control_DoubleClick;

			((System.ComponentModel.ISupportInitialize) (_pictureBoxThumbnail)).EndInit();
			ResumeLayout(false);
		}

		#endregion

		#region Event handlers

		private void Control_MouseClick(object sender, MouseEventArgs e)
		{
			Point point = (sender is PictureBox ? _pictureBoxThumbnail.PointToScreen(e.Location)
				: (sender is Label ? _labelFileName.PointToScreen(e.Location)
					: PointToScreen(e.Location)));
			RaiseThumbnailClickedEvent(new MouseEventArgs(e.Button, e.Clicks, point.X, point.Y, e.Delta));
		}

		private void Control_DoubleClick(object sender, EventArgs e)
		{
			RaiseThumbnailDoubleClickedEvent();
		}

		private void ThumbnailContainer_GotFocus(object sender, EventArgs e)
		{
			RaiseThumbnailGotFocusEvent();
		}

		#endregion

		#region Event raisers

		private void RaiseThumbnailClickedEvent(MouseEventArgs e)
		{
			if (ThumbnailClicked != null)
			{
				ThumbnailClicked(this, e);
			}
		}

		private void RaiseThumbnailDoubleClickedEvent()
		{
			if (ThumbnailDoubleClicked != null)
			{
				ThumbnailDoubleClicked(this, new EventArgs());
			}
		}

		private void RaiseThumbnailGotFocusEvent()
		{
			if (ThumbnailGotFocus != null)
			{
				ThumbnailGotFocus(this, new EventArgs());
			}
		}

		#endregion
	}
}
