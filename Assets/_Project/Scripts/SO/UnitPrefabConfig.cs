using _Project.Scripts.GameObjects.Camera;
using _Project.Scripts.GameObjects.Characters.Player;
using _Project.Scripts.GameObjects.Characters.Unit;
using UnityEngine;

namespace _Project.Scripts.SO
{
    [CreateAssetMenu(fileName = "UnitPrefabConfig", menuName = "SO/Unit Prefab Config")]
    public class UnitPrefabConfig : ScriptableObject
    {
        [Header("Unit Prefabs")]
        public UnitController meleeFriendPrefab;
        public UnitController distanceFriendPrefab;
        public UnitController meleeEnemyPrefab;
        public UnitController distanceEnemyPrefab;
        
        [Header("Player Prefabs")]
        public PlayerController playerPrefab;
        public CameraFollow cameraPrefab;
    }
}