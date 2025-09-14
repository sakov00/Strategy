using System;
using System.Collections.Generic;
using _General.Scripts.Interfaces;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects.LevelEnvironment.Terrain
{
    [Serializable]
    [MemoryPackable]
    public partial class TerrainModel : ISavableModel
    {
        public List<Vector3> Vertices { get; set; }
        public List<Vector3> Normals { get; set; }
        public List<Vector2> UVs { get; set; }
        public ushort[] Triangles { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
    }
}