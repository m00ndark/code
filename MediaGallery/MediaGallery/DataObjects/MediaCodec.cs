﻿using System;
using MediaGallery.DataAccess;
using MediaGallery.DataObjects.Serialization;

namespace MediaGallery.DataObjects
{
	public class MediaCodec : ISerializable
	{
		#region Enumeration

		public enum CodecType
		{
			Undefined,
			Audio,
			Video,
			AudioVideo
		}

		#endregion

		public MediaCodec(CodecType type, string name)
		{
			Initialize(type, name);
		}

		public MediaCodec()
		{
			Initialize(CodecType.Undefined, string.Empty);
		}

		#region Properties

		public string ID { get; private set; }
		public CodecType Type { get; private set; }
		public string Name { get; private set; }

		#endregion

		#region Serialization

		public virtual string Serialize()
		{
			return Serialize(true);
		}

		public virtual string Serialize(bool withPrefix)
		{
			return ObjectSerializer.Serialize((withPrefix ? this : null), Type.ToString(), Name);
		}

		public virtual string LoadFromDeserialized(string[] deserialized)
		{
			Type = (CodecType) Enum.Parse(typeof(CodecType), deserialized[0]);
			Name = deserialized[1];
			CreateID();
			return null;
		}

		#endregion

		private void Initialize(CodecType type, string name)
		{
			ID = null;
			Type = type;
			Name = name;
			if (!string.IsNullOrEmpty(Name))
				CreateID();
		}

		private void CreateID()
		{
			ID = CryptoServiceHandler.GenerateHash(Type + ":" + Name);
		}

		public override bool Equals(object obj)
		{
			MediaCodec codec = (obj as MediaCodec);
			return (codec != null && codec.Type == Type && codec.Name.Equals(Name, StringComparison.CurrentCultureIgnoreCase));
		}

		public static CodecType TranslateCodecType(TagLib.MediaTypes types)
		{
			switch (types)
			{
				case TagLib.MediaTypes.Audio:
					return CodecType.Audio;
				case TagLib.MediaTypes.Video:
					return CodecType.Video;
				case TagLib.MediaTypes.Audio | TagLib.MediaTypes.Video:
					return CodecType.AudioVideo;
				default:
					return CodecType.Undefined;
			}
		}
	}
}
