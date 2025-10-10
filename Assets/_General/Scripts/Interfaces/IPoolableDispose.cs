namespace _General.Scripts.Interfaces
{
    public interface IPoolableDispose
    {
        public void Dispose(bool returnToPool = true, bool clearFromRegistry = true);
    }
}