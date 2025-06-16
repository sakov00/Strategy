using System;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.Interfaces.View;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters
{
    public class CharacterView : ObjectView, IAttackableView
    {
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int IsAttack = Animator.StringToHash("IsAttack");
        
        [SerializeField] private Animator animator;
        [field:SerializeField] public ProjectileType ProjectileType { get; set; }
        [field:SerializeField] public Transform FirePoint { get; set; }
        [field:SerializeField] public float ProjectileSpeed { get; set; } = 10f;
        
        public event Action AttackHitEvent;
        
        public void SetWalking(bool isWalking)
        {
            if(animator == null)
                return;
            
            ResetAnimations();
        }
        
        public void SetAttack(bool isAttacking)
        {
            if(animator == null)
                return;
            
            ResetAnimations();
            animator.SetBool(IsAttack, isAttacking);
        }
        
        public void OnAttackHit()
        {
            AttackHitEvent?.Invoke();
        }

        private void ResetAnimations()
        {
            animator.SetBool(IsAttack, false);
        }
    }
}