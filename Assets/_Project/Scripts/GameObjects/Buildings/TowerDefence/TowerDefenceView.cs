using System;
using _Project.Scripts.Interfaces.View;
using UnityEngine;

namespace _Project.Scripts.GameObjects.TowerDefence
{
    [Serializable]
    public class TowerDefenceView : BuildView, IAttackableView
    { 
        [field:SerializeField] public GameObject ProjectilePrefab { get; set; }
        [field:SerializeField] public Transform FirePoint { get; set; }
        [field:SerializeField] public float ProjectileSpeed { get; set; } = 10f;

        public void SetWalking(bool isWalking)
        {
            
        }

        public void SetAttack(bool isAttacking)
        {
            
        }

        public event Action AttackHitEvent;
    }
}