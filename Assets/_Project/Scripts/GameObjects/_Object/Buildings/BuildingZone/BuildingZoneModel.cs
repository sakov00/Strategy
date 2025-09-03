using System;
using _General.Scripts.Interfaces;
using _Project.Scripts.Enums;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects._Object.BuildingZone
{
    [Serializable]
    [MemoryPackable]
    public partial class BuildingZoneModel : ISavableModel
    {
        [field:SerializeField] public BuildType BuildType { get; private set; }

        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
    }
}