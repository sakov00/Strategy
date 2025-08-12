using _Project.Scripts._GlobalData.AppDataProvider;
using VContainer.Unity;

namespace _Project.Scripts._GlobalData
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