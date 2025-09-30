using _Project.Scripts.Enums;
using UnityEngine;

namespace _Project.Scripts.Interfaces.View
{
    public interface IAttackableView : IAnimationView, IHealthView
    {
        public ProjectileType ProjectileType { get; set; }
        public Transform FirePoint { get; set; }
        public float ProjectileSpeed { get; set; }
    }
}