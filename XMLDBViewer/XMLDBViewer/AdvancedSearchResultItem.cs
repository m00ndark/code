namespace XMLDBViewer
{
	public class AdvancedSearchResultItem
	{
		public AdvancedSearchResultItem(string tableName, string column, string row, string value)
		{
			TableName = tableName;
			Column = column;
			Row = row;
			Value = value;
		}

		public string TableName { get; private set; }

		public string Column { get; private set; }

		public string Row { get; private set; }

		public string Value { get; private set; }
	}
}
