using System;
using _General.Scripts.Interfaces;
using _Project.Scripts.Enums;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Additional.LevelEnvironment.Environment
{
    [Serializable]
    [MemoryPackable]
    public partial class EnvironmentModel : ISavableModel
    {
        [field: SerializeField] public EnvironmentType EnvironmentType { get; set; }
        public Vector3 SavePosition { get; set; }
        public Quaternion SaveRotation { get; set; }
    }
}