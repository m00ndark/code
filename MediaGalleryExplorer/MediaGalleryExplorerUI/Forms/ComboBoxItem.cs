namespace MediaGalleryExplorerUI.Forms
{
	public class ComboBoxItem
	{
		public ComboBoxItem(string text)
		{
			Text = text;
			Tag = null;
		}

		public ComboBoxItem(string text, object tag)
		{
			Text = text;
			Tag = tag;
		}

		public string Text { get; set; }
		public object Tag { get; set; }

		public override string ToString()
		{
			return Text;
		}
	}
}
