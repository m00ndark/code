namespace MediaGallery.DataObjects.Serialization
{
	public interface ISerializable
	{
		string Serialize();
		string Serialize(bool withPrefix);
		string LoadFromDeserialized(string[] deserialized);
	}
}