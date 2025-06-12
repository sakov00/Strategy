using System;
using _Project.Scripts.Interfaces.View;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters
{
    [Serializable]
    public class CharacterView : IProjectTileView
    {
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int IsAttack = Animator.StringToHash("IsAttack");
        
        [SerializeField] private Animator animator;
        [field:SerializeField] public GameObject ProjectilePrefab { get; set; }
        [field:SerializeField] public Transform FirePoint { get; set; }
        [field:SerializeField] public float ProjectileSpeed { get; set; } = 10f;
        
        public void SetWalking(bool isWalking)
        {
            ResetAnimations();
            animator.SetBool(IsWalking, isWalking);
        }
        
        public void SetAttack(bool isAttacking)
        {
            ResetAnimations();
            animator.SetBool(IsAttack, isAttacking);
        }

        private void ResetAnimations()
        {
            animator.SetBool(IsWalking, false);
            animator.SetBool(IsAttack, false);
        }
    }
}