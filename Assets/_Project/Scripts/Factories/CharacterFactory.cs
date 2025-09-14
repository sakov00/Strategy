using System.Linq;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._Object.Characters.Character;
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
        
        public MyCharacterController CreateCharacter(CharacterType type, Vector3 position = default, Quaternion rotation = default)
        {
            var prefab = _characterPrefabConfig.allCharacterPrefabs
                .FirstOrDefault(p => p.CharacterModel.CharacterType == type);

            return prefab != null ? _resolver.Instantiate(prefab, position, rotation) : null;
        }

        public T CreateCharacter<T>(Vector3 position = default, Quaternion rotation = default) where T : MyCharacterController
        {
            var prefab = _characterPrefabConfig.allCharacterPrefabs
                .FirstOrDefault(p => p is T);

            return prefab != null ? _resolver.Instantiate(prefab as T, position, rotation) : null;
        }

        public T CreateCharacter<T>(CharacterType type, Vector3 position = default, Quaternion rotation = default) where T : MyCharacterController
        {
            var prefab = _characterPrefabConfig.allCharacterPrefabs
                .FirstOrDefault(p => p.CharacterModel.CharacterType == type && p is T);

            if (prefab == null)
            {
                Debug.LogError($"CharacterFactory: Prefab of type {typeof(T)} with CharacterType {type} not found");
                return null;
            }

            return _resolver.Instantiate(prefab as T, position, rotation);
        }
    }
}