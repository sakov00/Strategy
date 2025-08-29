using _General.Scripts.Registries;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Characters.Unit;
using _Project.Scripts.GameObjects.Projectiles;
using _Project.Scripts.SO;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Factories
{
    public class ProjectileFactory
    {
        [Inject] private IObjectResolver resolver;
        [Inject] private ProjectilePrefabConfig unitPrefabConfig;
        
        public Projectile CreateProjectile(ProjectileType projectileType, Vector3 position, Quaternion rotation = default)
        {
            Projectile projectile = null;
            switch (projectileType)
            {
                case ProjectileType.Arrow:
                    projectile = CreateArrow(position, rotation);
                    break;
                case ProjectileType.BigArrow:
                    projectile = CreateBigArrow(position, rotation);
                    break;
            }
            
            return projectile;
        }

        private Projectile CreateArrow(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(unitPrefabConfig.arrowPrefab, position, rotation);
        }
            
        private Projectile CreateBigArrow(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(unitPrefabConfig.bigArrowPrefab, position, rotation);
        }
    }
}