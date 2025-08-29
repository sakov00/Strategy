using System.Collections.Generic;
using System.Linq;
using _General.Scripts.Json;
using _Project.Scripts.GameObjects.Characters;
using _Project.Scripts.GameObjects.Characters.Unit;
using _Project.Scripts.SO;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Quaternion = System.Numerics.Quaternion;
using Vector3 = System.Numerics.Vector3;

namespace _Project.Scripts.Factories
{
    public class CharacterFactory
    {
        [Inject] private IObjectResolver _resolver;
        [Inject] private CharacterPrefabConfig _characterPrefabConfig;

        // Универсальный метод для создания юнитов
        public T CreateCharacter<T>(Vector3 position = default, Quaternion rotation = default) where T : MyCharacterController
        {
            MyCharacterController unit = null;
            foreach (var prefab in _characterPrefabConfig.allCharacterPrefabs)
            {
                if (prefab is T tPrefab)
                {
                    unit = _resolver.Instantiate(tPrefab, position, rotation);
                    break;
                }
            }

            return (T)unit;
        }

        // Создание списка юнитов из Json
        public List<MyCharacterController> CreateCharacters(List<UnitJson> unitJsons)
        {
            var units = new List<MyCharacterController>();
            foreach (var json in unitJsons)
            {
                var unit = CreateCharacter<MyCharacterController>(json.unitModel.WayToAim.First(), isFriend: isFriend);
                unit.SetJsonData(json);
                units.Add(unit);
            }
            return units;
        }
    }
}