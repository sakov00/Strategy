using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces.View;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.GameObjects._Object.Characters.Character
{
    public class CharacterView : ObjectView, IAttackableView
    {
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int IsAttack = Animator.StringToHash("IsAttack");
        
        [SerializeField] private Animator _animator;
        [field:SerializeField] public NavMeshAgent Agent { get; private set; }
        [field:SerializeField] public ProjectileType ProjectileType { get; set; }
        [field:SerializeField] public Transform FirePoint { get; set; }
        [field:SerializeField] public float ProjectileSpeed { get; set; } = 10f;
        
        public event Action AttackHitEvent;
        
        public void SetWalking(bool isWalking)
        {
            if(_animator == null)
                return;
            
            ResetAnimations();
        }
        
        public void SetAttack(bool isAttacking)
        {
            if (_animator == null)
            {
                AttackHitEvent?.Invoke();
                return;
            }
            
            ResetAnimations();
            _animator.SetBool(IsAttack, isAttacking);
        }
        
        public void OnAttackHit()
        {
            AttackHitEvent?.Invoke();
        }

        private void ResetAnimations()
        {
            _animator.SetBool(IsAttack, false);
        }
    }
}