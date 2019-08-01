namespace Programatica.Framework.Core.Adapter
{
    public interface IJsonSerializerAdapter : IObject
    {
        string Serialize(dynamic model);
        T Deserialize<T>(string model);
    }
}
