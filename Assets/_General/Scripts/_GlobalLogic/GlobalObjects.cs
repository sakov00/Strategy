using _General.Scripts.CameraLogic;
using UnityEngine;

namespace _General.Scripts._GlobalLogic
{
    public class GlobalObjects : MonoBehaviour
    {
        public static GlobalObjects Instance { get; private set; }
        
        public static CameraController CameraController => Instance.cameraController;
        
        [SerializeField] private CameraController cameraController;
        
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