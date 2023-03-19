namespace Programatica.Framework.Core.Adapter
{
    /// <summary>
    /// An interface for a JSON serializer adapter, which serializes and deserializes objects to and from JSON format.
    /// </summary>
    public interface IJsonSerializerAdapter 
    {
        /// <summary>
        /// Serializes an object to JSON format.
        /// </summary>
        /// <param name="model">The object to be serialized.</param>
        /// <returns>The JSON representation of the object.</returns>
        string Serialize(dynamic model);

        /// <summary>
        /// Deserializes an object from JSON format.
        /// </summary>
        /// <typeparam name="T">The type of the object to be deserialized.</typeparam>
        /// <param name="model">The JSON representation of the object.</param>
        /// <returns>The deserialized object.</returns>
        T Deserialize<T>(string model);
    }
}
