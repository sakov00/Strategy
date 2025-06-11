using UnityEngine;

namespace _Project.Scripts.Interfaces.View
{
    public interface IProjectTileView
    {
        public GameObject ProjectilePrefab { get; set; }
        public Transform FirePoint { get; set; }
        public float ProjectileSpeed { get; set; }
    }
}