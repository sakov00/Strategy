using _Project.Scripts.Data;
using LunarConsolePlugin;
using UnityEngine;

namespace _Project.Scripts._GlobalLogic
{
    public class GameData : MonoBehaviour
    {
        public static GameData Instance { get; private set; }
        [SerializeField] private LunarConsole lunarConsole;
        [SerializeField] public LevelData levelData = new();

        private void Awake()
        {
            LunarConsole.Show();
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
    }
}