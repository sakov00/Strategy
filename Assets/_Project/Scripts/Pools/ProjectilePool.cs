using System.Collections.Generic;
using System.Linq;
using _General.Scripts.Registries;
using _Project.Scripts.Enums;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects.Projectiles;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Pools
{
    public class ProjectilePool
    {
        [Inject] private ProjectileFactory _projectileFactory;
        [Inject] private ObjectsRegistry _objectsRegistry;
        
        private Transform _containerTransform;
        private readonly List<Projectile> _availableProjectiles = new();

        public void SetContainer(Transform transform)
        {
            _containerTransform = transform;
        }
        
        public Projectile Get(ProjectileType projectileType, Vector3 position = default, Quaternion rotation = default) 
        {
            var character = _availableProjectiles.FirstOrDefault(c => c.ProjectileType == projectileType);
            if (character != null)
            {
                _availableProjectiles.Remove(character);
                character.gameObject.SetActive(true);
            }
            else
            {
                character = _projectileFactory.CreateProjectile(projectileType, position, rotation);
            }

            _objectsRegistry.Register(character);
            return character;
        }
        
        public T Get<T>(Vector3 position = default, Quaternion rotation = default) where T : Projectile
        {
            var projectile = _availableProjectiles.OfType<T>().FirstOrDefault();

            if (projectile != null)
            {
                _availableProjectiles.Remove(projectile);
                projectile.gameObject.SetActive(true);
            }
            else
            {
                projectile = _projectileFactory.CreateProjectile<T>(position, rotation);
            }
            
            _objectsRegistry.Register(projectile);
            return projectile;
        }

        public void Return<T>(T projectile) where T : Projectile
        {
            if (!_availableProjectiles.Contains(projectile))
            {
                _availableProjectiles.Add(projectile);
            }
            
            projectile.gameObject.SetActive(false);
            projectile.transform.SetParent(_containerTransform, false); 
            _objectsRegistry.Unregister(projectile);
        }
    }
}