using System;
using _General.Scripts.Interfaces;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects.LevelEnvironment.Terrain
{
    [Serializable]
    [MemoryPackable]
    public partial class TerrainModel : ISavableModel
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
    }
}