using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces.View;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.GameObjects._Object.MainBuild
{
    [Serializable]
    public class MainBuildingView : BuildView, IAttackableView
    {
        [field:SerializeField] public ProjectileType ProjectileType { get; set; }
        [field:SerializeField] public Transform FirePoint { get; set; }
        [field:SerializeField] public float ProjectileSpeed { get; set; } = 10f;

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