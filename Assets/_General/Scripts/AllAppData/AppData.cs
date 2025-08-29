using VContainer.Unity;

namespace _General.Scripts.AllAppData
{
    public class AppData : IInitializable
    {
        public static LevelData LevelData;
        public static User User;

        public void Initialize()
        {
            LevelData = new LevelData();
            User = new User();
        }
    }
}