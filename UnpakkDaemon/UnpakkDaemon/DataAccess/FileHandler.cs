﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace UnpakkDaemon.DataAccess
{
	public static class FileHandler
	{
		public static bool FileExists(string filePath)
		{
			return File.Exists(filePath);
		}

		public static void DeleteFile(string filePath)
		{
			File.Delete(filePath);
		}

		public static long FileSize(string filePath)
		{
			return new FileInfo(filePath).Length;
		}

		public static bool DirectoryExists(string path)
		{
			return Directory.Exists(path);
		}

		public static void MakeDirectory(string path)
		{
			if (!DirectoryExists(path))
				Directory.CreateDirectory(path);
		}

		public static long GetTotalFileSize(IEnumerable<string> filePaths)
		{
			return filePaths.Sum(filePath => new FileInfo(filePath).Length);
		}

		public static string[] FileReadLines(string filePath)
		{
			return File.ReadAllLines(filePath);
		}

		public static void FileWriteLine(string filePath, string text)
		{
			using (StreamWriter writer = new StreamWriter(filePath, true))
				writer.WriteLine(text);
		}

		public static void Serialize(string filePath, IXmlSerializable obj)
		{
			XmlSerializer serializer = new XmlSerializer(obj.GetType());
			using (StreamWriter writer = new StreamWriter(filePath))
				serializer.Serialize(writer, obj);
		}

		public static T Deserialize<T>(string filePath) where T : IXmlSerializable
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			using (StreamReader reader = new StreamReader(filePath))
				return (T) serializer.Deserialize(reader);
		}
	}
}
