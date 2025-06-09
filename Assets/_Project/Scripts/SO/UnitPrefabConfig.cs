using _Project.Scripts.GameObjects.Characters.Player;
using _Project.Scripts.GameObjects.Characters.Unit;
using UnityEngine;

namespace _Project.Scripts.SO
{
    [CreateAssetMenu(fileName = "UnitPrefabConfig", menuName = "SO/Unit Prefab Config")]
    public class UnitPrefabConfig : ScriptableObject
    {
        [Header("Unit Prefabs")]
        public UnitModel meleeFriendPrefab;
        public UnitModel distanceFriendPrefab;
        public UnitModel meleeEnemyPrefab;
        public UnitModel distanceEnemyPrefab;
        
        [Header("Player Prefabs")]
        public PlayerModel playerPrefab;
        public IsometricCameraFollow cameraPrefab;
    }
}