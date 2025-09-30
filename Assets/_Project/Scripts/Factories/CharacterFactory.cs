using System.Linq;
using _Project.Scripts.GameObjects.Abstract.Unit;
using _Project.Scripts.SO;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Factories
{
    public class CharacterFactory
    {
        [Inject] private IObjectResolver _resolver;
        [Inject] private CharacterPrefabConfig _characterPrefabConfig;
        
        public T CreateCharacter<T>(Vector3 position = default, Quaternion rotation = default) where T : UnitController
        {
            var prefab = _characterPrefabConfig.allCharacterPrefabs
                .FirstOrDefault(p => p is T);

            return prefab != null ? _resolver.Instantiate(prefab as T, position, rotation) : null;
        }
    }
}