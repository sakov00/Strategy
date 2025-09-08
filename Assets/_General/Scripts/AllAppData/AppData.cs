using System;
using VContainer.Unity;

namespace _General.Scripts.AllAppData
{
    public class AppData : IInitializable, IDisposable
    {
        public LevelData LevelData { get; private set; }
        public LevelEvents LevelEvents { get; private set; }
        public User User { get; private set; }

        public void Initialize()
        {
            LevelData = new LevelData();
            LevelEvents = new LevelEvents();
            User = new User();
        }

        public void Dispose()
        {
            LevelData.Dispose();
            LevelEvents.Dispose();
        }
    }
}