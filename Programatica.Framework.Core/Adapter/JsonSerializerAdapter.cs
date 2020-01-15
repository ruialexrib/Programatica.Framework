using Newtonsoft.Json;

namespace Programatica.Framework.Core.Adapter
{
    public class JsonSerializerAdapter : IJsonSerializerAdapter
    {
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

        public T Deserialize<T>(string model)
        {
            return JsonConvert.DeserializeObject<T>(model); ;
        }
    }
}
