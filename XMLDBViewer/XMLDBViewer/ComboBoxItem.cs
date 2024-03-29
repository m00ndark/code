﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLDBViewer
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

		#region Properties

		public string Text { get; private set; }
		public object Tag { get; private set; }

		#endregion

		public override string ToString()
		{
			return Text;
		}
	}
}
