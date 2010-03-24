using System;
using System.Collections;
using System.Windows.Forms;

namespace XMLDBViewer
{
	class ListViewItemComparer : IComparer
	{
		#region Constructors

		public ListViewItemComparer()
		{
			FirstColumn = 0;
			SecondColumn = 0;
			FirstSortOrder = SortOrder.Ascending;
			SecondSortOrder = SortOrder.Ascending;
		}

		public ListViewItemComparer(int firstColumn)
		{
			FirstColumn = firstColumn;
			SecondColumn = 0;
			FirstSortOrder = SortOrder.Ascending;
			SecondSortOrder = SortOrder.Ascending;
		}

		public ListViewItemComparer(int firstColumn, int secondColumn)
		{
			FirstColumn = firstColumn;
			SecondColumn = secondColumn;
			FirstSortOrder = SortOrder.Ascending;
			SecondSortOrder = SortOrder.Ascending;
		}

		public ListViewItemComparer(int firstColumn, SortOrder firstSortOrder)
		{
			FirstColumn = firstColumn;
			SecondColumn = 0;
			FirstSortOrder = firstSortOrder;
			SecondSortOrder = SortOrder.Ascending;
		}

		public ListViewItemComparer(int firstColumn, SortOrder firstSortOrder, SortOrder secondSortOrder)
		{
			FirstColumn = firstColumn;
			SecondColumn = 0;
			FirstSortOrder = firstSortOrder;
			SecondSortOrder = secondSortOrder;
		}

		public ListViewItemComparer(int firstColumn, int secondColumn, SortOrder firstSortOrder)
		{
			FirstColumn = firstColumn;
			SecondColumn = secondColumn;
			FirstSortOrder = firstSortOrder;
			SecondSortOrder = SortOrder.Ascending;
		}

		public ListViewItemComparer(int firstColumn, int secondColumn, SortOrder firstSortOrder, SortOrder secondSortOrder)
		{
			FirstColumn = firstColumn;
			SecondColumn = secondColumn;
			FirstSortOrder = firstSortOrder;
			SecondSortOrder = secondSortOrder;
		}

		public ListViewItemComparer(SortOrder firstSortOrder)
		{
			FirstColumn = 0;
			SecondColumn = 0;
			FirstSortOrder = firstSortOrder;
			SecondSortOrder = SortOrder.Ascending;
		}

		public ListViewItemComparer(SortOrder firstSortOrder, SortOrder secondSortOrder)
		{
			FirstColumn = 0;
			SecondColumn = 0;
			FirstSortOrder = firstSortOrder;
			SecondSortOrder = secondSortOrder;
		}

		#endregion

		#region Properties

		public int FirstColumn { get; private set; }
		public int SecondColumn { get; private set; }
		public SortOrder FirstSortOrder { get; set; }
		public SortOrder SecondSortOrder { get; set; }

		#endregion

		public void SetColumn(int column)
		{
			if (FirstColumn == column)
				FirstSortOrder = (FirstSortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending);
			else
			{
				SecondColumn = FirstColumn;
				FirstColumn = column;
				SecondSortOrder = FirstSortOrder;
				FirstSortOrder = SortOrder.Ascending;
			}
		}

		public int Compare(object x, object y)
		{
			ListViewItem xItem = x as ListViewItem;
			ListViewItem yItem = y as ListViewItem;
			if (xItem == null || yItem == null) return 0;
			int value = (FirstSortOrder == SortOrder.None || xItem.SubItems.Count <= FirstColumn
				|| yItem.SubItems.Count <= FirstColumn ? 0 : (FirstSortOrder == SortOrder.Ascending
				? String.Compare(xItem.SubItems[FirstColumn].Text, yItem.SubItems[FirstColumn].Text)
				: String.Compare(yItem.SubItems[FirstColumn].Text, xItem.SubItems[FirstColumn].Text)));
			return (value != 0 || SecondSortOrder == SortOrder.None || xItem.SubItems.Count <= SecondColumn
				|| yItem.SubItems.Count <= SecondColumn ? value : (SecondSortOrder == SortOrder.Ascending
				? String.Compare(xItem.SubItems[SecondColumn].Text, yItem.SubItems[SecondColumn].Text)
				: String.Compare(yItem.SubItems[SecondColumn].Text, xItem.SubItems[SecondColumn].Text)));
		}
	}
}
