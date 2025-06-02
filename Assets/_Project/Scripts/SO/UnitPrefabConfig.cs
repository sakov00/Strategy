using _Project.Scripts.GameObjects.Characters.Unit;
using UnityEngine;

namespace _Project.Scripts.SO
{
    [CreateAssetMenu(fileName = "UnitPrefabConfig", menuName = "SO/Unit Prefab Config")]
    public class UnitPrefabConfig : ScriptableObject
    {
        public UnitModel meleeFriendPrefab;
        public UnitModel distanceFriendPrefab;
        public UnitModel meleeEnemyPrefab;
        public UnitModel distanceEnemyPrefab;
    }
}