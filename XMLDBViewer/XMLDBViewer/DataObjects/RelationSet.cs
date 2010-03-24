using System;
using System.Collections.Generic;

namespace XMLDBViewer.DataObjects
{
	public class RelationSet
	{
		public RelationSet()
		{
			Name = string.Empty;
			CreateDate = DateTime.Now;
			ModifyDate = CreateDate;
			Relations = new List<Relation>();
			Databases = new List<string>();
		}

		#region Properties

		public string Name { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime ModifyDate { get; set; }
		public List<Relation> Relations { get; private set; }
		public List<string> Databases { get; private set; }

		#endregion

		public RelationSet Clone()
		{
			RelationSet relationSet = new RelationSet()
				{
					Name = Name,
					CreateDate = CreateDate,
					ModifyDate = ModifyDate
				};

			foreach (Relation relation in Relations)
				relationSet.Relations.Add(relation.Clone());

			foreach (string database in Databases)
				relationSet.Databases.Add(database);

			return relationSet;
		}

		public void Adopt(RelationSet relationSet)
		{
			Name = relationSet.Name;
			CreateDate = relationSet.CreateDate;
			ModifyDate = relationSet.ModifyDate;

			Relations.Clear();
			foreach (Relation relation in relationSet.Relations)
				Relations.Add(relation.Clone());

			Databases.Clear();
			foreach (string database in relationSet.Databases)
				Databases.Add(database);
		}
	}
}
