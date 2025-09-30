using System;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract;
using _Project.Scripts.Interfaces.View;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Concrete.TowerDefence
{
    [Serializable]
    public class TowerDefenceView : BuildView, IAttackableView
    {
        [field: SerializeField] public ProjectileType ProjectileType { get; set; }
        [field: SerializeField] public Transform FirePoint { get; set; }
        [field: SerializeField] public float ProjectileSpeed { get; set; } = 10f;
        
        public override void Initialize()
        {
            SetWalking(false);
        }

        public void SetWalking(bool isWalking)
        {
        }

        public void SetAttack(bool isAttacking)
        {
            AttackHitEvent?.Invoke();
        }

        public event Action AttackHitEvent;
    }
}