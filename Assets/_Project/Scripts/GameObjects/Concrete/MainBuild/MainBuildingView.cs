using System;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract;
using _Project.Scripts.Interfaces.View;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Concrete.MainBuild
{
    [Serializable]
    public class MainBuildingView : BuildView, IFightView
    {
        [field: SerializeField] public ProjectileType ProjectileType { get; set; }
        [field: SerializeField] public Transform FirePoint { get; set; }
        [field: SerializeField] public float ProjectileSpeed { get; set; } = 10f;

        public override void Initialize()
        {
        }

        public void SetWalking(bool isWalking)
        {
        }

        public void SetAttacking(bool isAttacking)
        {
            AttackHitEvent?.Invoke();
        }
        
        public Vector3 GetPosition() => transform.position;
        public Vector3 GetScale() => transform.localScale;

        public event Action AttackHitEvent;
    }
}