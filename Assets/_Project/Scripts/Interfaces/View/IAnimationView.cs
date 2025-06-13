using System;

namespace _Project.Scripts.Interfaces.View
{
    public interface IAnimationView
    {
        public event Action AttackHitEvent;
        
        public void SetWalking(bool isWalking);
        public void SetAttack(bool isAttacking);
        
    }
}