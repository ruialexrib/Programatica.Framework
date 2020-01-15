namespace Programatica.Framework.Core.Adapter
{
    public interface IJsonSerializerAdapter 
    {
        string Serialize(dynamic model);
        T Deserialize<T>(string model);
    }
}
