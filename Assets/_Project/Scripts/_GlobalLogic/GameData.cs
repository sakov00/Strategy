using _Project.Scripts.Data;
using UnityEngine;

namespace _Project.Scripts._GlobalLogic
{
    public class GameData : MonoBehaviour
    {
        public static GameData Instance { get; private set; }

        [SerializeField] public LevelData levelData = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
    }
}