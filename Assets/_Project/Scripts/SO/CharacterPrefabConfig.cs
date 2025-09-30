using System.Collections.Generic;
using _General.Scripts.CameraLogic;
using _Project.Scripts.GameObjects.Abstract.Unit;
using UnityEngine;

namespace _Project.Scripts.SO
{
    [CreateAssetMenu(fileName = "CharacterPrefabConfig", menuName = "SO/Character Prefab Config")]
    public class CharacterPrefabConfig : ScriptableObject
    {
        [Header("Character Prefabs")] 
        public List<UnitController> allCharacterPrefabs = new();
        public CameraFollow cameraPrefab;
    }
}