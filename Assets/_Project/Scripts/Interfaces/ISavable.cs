namespace _Project.Scripts.Interfaces
{
    public interface ISavable<T> : IClearData
    {
        T GetJsonData();
        void SetJsonData(T json);
    }
}