using LunarConsolePlugin;
using UnityEngine;

namespace _Project.Scripts._GlobalLogic
{
    public class GlobalObjects : MonoBehaviour
    {
        public static GlobalObjects Instance { get; private set; }
        
        public static CameraController CameraControllerController => Instance.cameraController;
        public static LunarConsole LunarConsole => Instance.lunarConsole;
        
        [SerializeField] private CameraController cameraController;
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