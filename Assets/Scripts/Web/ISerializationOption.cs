namespace Web
{
    public interface ISerializationOption
    {
        public string ContentType { get; }
        T Deserialize<T>(string text);
    }
}
