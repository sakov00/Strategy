namespace _Project.Scripts.Interfaces
{
    public interface ISavable<T>
    {
        T GetJsonData();
        void SetJsonData(T json);
    }
}