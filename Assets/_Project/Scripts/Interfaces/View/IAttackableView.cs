using UnityEngine;

namespace _Project.Scripts.Interfaces.View
{
    public interface IAttackableView : IAnimationView
    {
        public GameObject ProjectilePrefab { get; set; }
        public Transform FirePoint { get; set; }
        public float ProjectileSpeed { get; set; }
    }
}