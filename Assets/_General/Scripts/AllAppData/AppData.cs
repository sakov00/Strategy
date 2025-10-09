using _General.Scripts._VContainer;
using _General.Scripts.Enums;
using VContainer.Unity;

namespace _General.Scripts.AllAppData
{
    public class AppData : IInitializable
    {
        public AppMode AppMode { get; set; }
        public User User { get; private set; }
        public LevelData LevelData { get; set; }
        public LevelEvents LevelEvents { get; private set; }

        public void Initialize()
        {
            User = new User();
            LevelData = new LevelData();
            LevelEvents = new LevelEvents();
            InjectManager.Inject(LevelEvents);
        }
    }
}