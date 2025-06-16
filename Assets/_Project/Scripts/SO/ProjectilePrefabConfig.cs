using _Project.Scripts.GameObjects.Characters.Player;
using _Project.Scripts.GameObjects.Characters.Unit;
using _Project.Scripts.GameObjects.Projectiles;
using UnityEngine;

namespace _Project.Scripts.SO
{
    [CreateAssetMenu(fileName = "ProjectilePrefabConfig", menuName = "SO/Projectile Prefab Config")]
    public class ProjectilePrefabConfig : ScriptableObject
    {
        [Header("Projectile Prefabs")]
        public Projectile arrowPrefab;
        public Projectile bigArrowPrefab;
    }
}