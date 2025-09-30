using System;
using _General.Scripts.Interfaces;
using _Project.Scripts.Enums;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Concrete.BuildingZone
{
    [Serializable]
    [MemoryPackable]
    public partial class BuildingZoneModel : ISavableModel
    {
        [field: SerializeField] public BuildType BuildType { get; private set; }

        public Vector3 SavePosition { get; set; }
        public Quaternion SaveRotation { get; set; }
    }
}