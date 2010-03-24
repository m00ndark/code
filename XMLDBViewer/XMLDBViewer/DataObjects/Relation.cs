namespace XMLDBViewer.DataObjects
{
	public class Relation
	{
		public Relation(string sourceTable, string sourceColumn, string destinationTable, string destinationColumn)
		{
			SourceTable = sourceTable;
			SourceColumn = sourceColumn;
			DestinationTable = destinationTable;
			DestinationColumn = destinationColumn;
		}

		#region Properties

		public string SourceTable { get; private set; }
		public string SourceColumn { get; private set; }
		public string DestinationTable { get; private set; }
		public string DestinationColumn { get; private set; }

		#endregion

		public override bool Equals(object obj)
		{
			Relation relation = (obj as Relation);
			return (relation != null && relation.SourceTable == SourceTable && relation.SourceColumn == SourceColumn
				&& relation.DestinationTable == DestinationTable && relation.DestinationColumn == DestinationColumn);
		}

		public Relation Clone()
		{
			return new Relation(SourceTable, SourceColumn, DestinationTable, DestinationColumn);
		}
	}
}
