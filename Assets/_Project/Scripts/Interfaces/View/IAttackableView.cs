using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Projectiles;
using UnityEngine;

namespace _Project.Scripts.Interfaces.View
{
    public interface IAttackableView : IAnimationView
    {
        public ProjectileType ProjectileType { get; set; }
        public Transform FirePoint { get; set; }
        public float ProjectileSpeed { get; set; }
    }
}