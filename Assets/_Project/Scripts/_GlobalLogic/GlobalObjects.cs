using LunarConsolePlugin;
using UnityEngine;

namespace _Project.Scripts._GlobalLogic
{
    public class GlobalObjects : MonoBehaviour
    {
        public static GlobalObjects Instance { get; private set; }
        
        public static GameData GameData => Instance.gameData;
        public static IsometricCameraFollow CameraFollow => Instance.cameraFollow;
        public static LunarConsole LunarConsole => Instance.lunarConsole;
        
        [SerializeField] private GameData gameData;
        [SerializeField] private IsometricCameraFollow cameraFollow;
        [SerializeField] private LunarConsole lunarConsole;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}