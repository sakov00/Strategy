using System;
using _General.Scripts.DTO;
using _General.Scripts.Interfaces;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Additional.LevelEnvironment.Terrain
{
    [Serializable]
    [MemoryPackable]
    public partial class TerrainModel : ISavableModel
    {
        public Vector3Scaled[] Vertices { get; set; }
        public Vector3Scaled[] Normals { get; set; }
        public Vector2Scaled[] UVs { get; set; }
        public ushort[] Triangles { get; set; }
        public Vector3 SavePosition { get; set; }
        public Quaternion SaveRotation { get; set; }
        public ISavableModel DeepClone()
        {
            return (TerrainModel)MemberwiseClone();
        }
    }
}