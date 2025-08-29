using System.Collections.Generic;
using _Project.Scripts.GameObjects.Camera;
using _Project.Scripts.GameObjects.Characters;
using _Project.Scripts.GameObjects.Units.Character;
using UnityEngine;

namespace _Project.Scripts.SO
{
    [CreateAssetMenu(fileName = "CharacterPrefabConfig", menuName = "SO/Character Prefab Config")]
    public class CharacterPrefabConfig : ScriptableObject
    {
        [Header("Character Prefabs")] 
        public List<MyCharacterController> allCharacterPrefabs = new();
        public CameraFollow cameraPrefab;
    }
}