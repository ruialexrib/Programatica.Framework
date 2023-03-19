using Newtonsoft.Json;

namespace Programatica.Framework.Core.Adapter
{
    /// <summary>
    /// Implementation of a JSON serializer adapter using the Newtonsoft.Json library
    /// </summary>
    public class JsonSerializerAdapter : IJsonSerializerAdapter
    {
        /// <summary>
        /// Serializes a dynamic object into a JSON string
        /// </summary>
        /// <param name="model">The object to serialize</param>
        /// <returns>A JSON string representation of the object</returns>
        public string Serialize(dynamic model)
        {
            return JsonConvert.SerializeObject(
                model,
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }

        /// <summary>
        /// Deserializes a JSON string into an object of the specified type
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to</typeparam>
        /// <param name="model">The JSON string to deserialize</param>
        /// <returns>An object of the specified type deserialized from the JSON string</returns>
        public T Deserialize<T>(string model)
        {
            return JsonConvert.DeserializeObject<T>(model); ;
        }
    }
}
