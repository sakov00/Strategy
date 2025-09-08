using System;
using System.Collections.Generic;
using System.Linq;
using _General.Scripts.Registries;
using _Project.Scripts.Enums;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects._Object.Characters.Character;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Pools
{
    public class CharacterPool
    {
        [Inject] private CharacterFactory _characterFactory;
        [Inject] private ObjectsRegistry _objectsRegistry;

        private Transform _containerTransform;
        private readonly List<MyCharacterController> _availableCharacters = new();

        public void SetContainer(Transform transform)
        {
            _containerTransform = transform;
        }
        
        public List<MyCharacterController> GetAvailableBuilds() => _availableCharacters;

        public MyCharacterController Get(CharacterType characterType, Vector3 position = default, Quaternion rotation = default) 
        {
            var character = _availableCharacters.FirstOrDefault(c => c.CharacterModel.CharacterType == characterType);
            if (character != null)
            {
                _availableCharacters.Remove(character);
                character.transform.position = position;
                character.transform.rotation = rotation;
                character.gameObject.SetActive(true);
                character.Initialize();
            }
            else
            {
                character = _characterFactory.CreateCharacter(characterType, position, rotation);
            }

            character.transform.SetParent(null);
            _objectsRegistry.Register(character);
            return character;
        }
        
        public T Get<T>(Vector3 position = default, Quaternion rotation = default) where T : MyCharacterController
        {
            var character = _availableCharacters.OfType<T>().FirstOrDefault();
            if (character != null)
            {
                _availableCharacters.Remove(character);
                character.transform.position = position;
                character.transform.rotation = rotation;
                character.gameObject.SetActive(true);
            }
            else
            {
                character = _characterFactory.CreateCharacter<T>(position, rotation);
            }

            character.transform.SetParent(null);
            _objectsRegistry.Register(character);
            return character;
        }

        public T Get<T>(CharacterType characterType, Vector3 position = default, Quaternion rotation = default) 
            where T : MyCharacterController
        {
            var character = _availableCharacters.OfType<T>().FirstOrDefault(c => c.CharacterModel.CharacterType == characterType);
            if (character != null)
            {
                _availableCharacters.Remove(character);
                character.transform.position = position;
                character.transform.rotation = rotation;
                character.gameObject.SetActive(true);
            }
            else
            {
                character = _characterFactory.CreateCharacter<T>(characterType, position, rotation);
            }

            character.transform.SetParent(null);
            _objectsRegistry.Register(character);
            return character;
        }

        public void Return<T>(T character) where T : MyCharacterController
        {
            if (!_availableCharacters.Contains(character))
            {
                _availableCharacters.Add(character);
            }

            character.gameObject.SetActive(false);
            character.transform.SetParent(_containerTransform, false);
            _objectsRegistry.Unregister(character);
        }
        
        public void Remove<T>(T build) where T : MyCharacterController
        {
            if (!_availableCharacters.Contains(build))
            {
                _availableCharacters.Remove(build);
            }
        }
    }
}
