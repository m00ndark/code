using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace UnpakkDaemon.DataObjects
{
	public class Record : IXmlSerializable
	{
		public Record()
		{
			SubRecords = new List<SubRecord>();
		}

		public Record(string folder, string sfvName, string rarName, int rarCount, long rarSize)
			: this(RecordStatus.InProgress, folder, sfvName, rarName, rarCount, rarSize) { }

		public Record(RecordStatus status, string folder, string sfvName, string rarName, int rarCount, long rarSize)
			: this(Guid.NewGuid(), DateTime.Now, status, folder, sfvName, rarName, rarCount, rarSize) { }

		public Record(DateTime time, RecordStatus status, string folder, string sfvName, string rarName, int rarCount, long rarSize)
			: this(Guid.NewGuid(), time, status, folder, sfvName, rarName, rarCount, rarSize) { }

		public Record(Guid id, DateTime time, RecordStatus status, string folder, string sfvName, string rarName, int rarCount, long rarSize)
			: this()
		{
			ID = id;
			Time = time;
			Status = status;
			Folder = folder;
			SFVName = sfvName;
			RARName = rarName;
			RARCount = rarCount;
			RARSize = rarSize;
		}

		public Record(XmlReader xmlReader) : this()
		{
			ReadXml(xmlReader);
		}

		public void CopyFrom(Record record)
		{
			// do not copy id - it's the unique key
			Time = record.Time;
			Status = record.Status;
			Folder = record.Folder;
			SFVName = record.SFVName;
			RARName = record.RARName;
			RARCount = record.RARCount;
			RARSize = record.RARSize;
		}

		public Record Succeed()
		{
			Status = RecordStatus.Success;
			Time = DateTime.Now;
			return this;
		}

		public Record Fail()
		{
			Status = RecordStatus.Failure;
			Time = DateTime.Now;
			return this;
		}

		#region Properties

		public Guid ID { get; set; }
		public DateTime Time { get; set; }
		public RecordStatus Status { get; set; }
		public string Folder { get; set; }
		public string SFVName { get; set; }
		public string RARName { get; set; }
		public int RARCount { get; set; }
		public long RARSize { get; set; }
		public IList<SubRecord> SubRecords { get; private set; }

		public RecordStatus SubRecordStatus
		{
			get
			{
				return (SubRecords.Count > 0
					? SubRecords.Select(sr => sr.Status).Aggregate((x, y) => (x == RecordStatus.Failure || y == RecordStatus.Failure ? RecordStatus.Failure
						: (x == RecordStatus.InProgress || y == RecordStatus.InProgress ? RecordStatus.InProgress : RecordStatus.Success)))
					: RecordStatus.Success);
			}
		}

		#endregion

		#region Implementation of IXmlSerializable

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			if (!reader.IsStartElement())
				reader.ReadStartElement("Record");

			while (reader.MoveToNextAttribute())
			{
				switch (reader.Name)
				{
					case "ID":
						ID = new Guid(reader.Value);
						break;
					case "Time":
						Time = DateTime.Parse(reader.Value);
						break;
					case "Status":
						Status = (RecordStatus) Enum.Parse(typeof(RecordStatus), reader.Value);
						break;
					case "Folder":
						Folder = reader.Value;
						break;
					case "SFVName":
						SFVName = reader.Value;
						break;
					case "RARName":
						RARName = reader.Value;
						break;
					case "RARCount":
						RARCount = int.Parse(reader.Value);
						break;
					case "RARSize":
						RARSize = long.Parse(reader.Value);
						break;
				}
			}

			reader.MoveToElement();
			if (reader.IsEmptyElement)
				return;

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Record")
					return;

				if (reader.NodeType == XmlNodeType.Element && reader.Name == "SubRecord")
					SubRecords.Add(new SubRecord(reader));
			}
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Record");
			writer.WriteAttributeString("ID", ID.ToString());
			writer.WriteAttributeString("Time", Time.ToString("yyyy-MM-dd HH:mm:ss.fff"));
			writer.WriteAttributeString("Status", Status.ToString());
			writer.WriteAttributeString("Folder", Folder);
			writer.WriteAttributeString("SFVName", SFVName);
			writer.WriteAttributeString("RARName", RARName);
			writer.WriteAttributeString("RARCount", RARCount.ToString());
			writer.WriteAttributeString("RARSize", RARSize.ToString());
			foreach (SubRecord subRecord in SubRecords)
			{
				subRecord.WriteXml(writer);
			}
			writer.WriteEndElement();
		}

		#endregion
	}
}
