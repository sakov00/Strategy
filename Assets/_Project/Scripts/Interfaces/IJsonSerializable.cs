using _General.Scripts.Json;

namespace _Project.Scripts.Interfaces
{
    public interface IJsonSerializable
    {
        public ObjectJson GetJsonData();
        public void SetJsonData(ObjectJson json);
    }
}