using System;
using _Project.Scripts.Enums;
using UnityEngine;

namespace _Project.Scripts.Interfaces.View
{
    public interface IFightView
    {
        public event Action AttackHitEvent;
        public void SetWalking(bool isWalking);
        public void SetAttack(bool isAttacking);
        
        public ProjectileType ProjectileType { get; set; }
        public Transform FirePoint { get; set; }
        public float ProjectileSpeed { get; set; }
    }
}