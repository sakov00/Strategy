using System;
using System.Collections;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts._VContainer;
using _Project.Scripts.Enums;
using _Project.Scripts.Extentions;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects.Projectiles;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Interfaces.View;
using _Project.Scripts.Registries;
using _Project.Scripts.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

namespace _Project.Scripts.GameObjects.Characters
{
    public class DamageSystem
    {
        [Inject] private ProjectileFactory projectileFactory;
        [Inject] private HealthRegistry healthRegistry;
        
        private readonly IFightObjectModel fightObjectModel;
        private readonly IAttackableView attackableView;
        private readonly Transform transform;
        
        private float lastAttackTime = -Mathf.Infinity;
        private Coroutine attackCoroutine;

        public DamageSystem(IFightObjectModel fightObjectModel, IAttackableView attackableView, Transform transform)
        {
            this.fightObjectModel = fightObjectModel;
            this.attackableView = attackableView;
            this.transform = transform;
            
            InjectManager.Inject(this);

            if (fightObjectModel.TypeAttack == TypeAttack.Melee)
            {
                attackableView.AttackHitEvent += MeleeAttack;
            }
            if (fightObjectModel.TypeAttack == TypeAttack.Distance)
            {
                attackableView.AttackHitEvent += DistanceAttack;
            }
        }

        public void Attack()
        {
            if (fightObjectModel.AimCharacter == null || fightObjectModel.AimCharacter.Equals(null))
                return;
            
            if (Time.time - lastAttackTime < fightObjectModel.AllAnimAttackTime)
                return;
            
            var distance = PositionExtention.GetDistanceBetweenObjects(transform, fightObjectModel.AimCharacter.Transform);
            if (distance > fightObjectModel.AttackRange)
            {
                attackableView.SetWalking(true);
                return;
            }
            
            lastAttackTime = Time.time;
            attackableView.SetAttack(true);
        }

        private void MeleeAttack()
        {
            if (fightObjectModel.AimCharacter == null)
            {
                attackableView.SetWalking(true);
                return;
            }

            var distance = PositionExtention.GetDistanceBetweenObjects(transform, fightObjectModel.AimCharacter.Transform);
            if (distance > fightObjectModel.AttackRange)
            {
                attackableView.SetWalking(true);
                return;
            }
            
            fightObjectModel.AimCharacter.CurrentHealth -= fightObjectModel.DamageAmount;
            if (fightObjectModel.AimCharacter.CurrentHealth <= 0)
            {
                if (fightObjectModel.AimCharacter.WarSide == WarSide.Enemy)
                {
                    healthRegistry.Unregister(fightObjectModel.AimCharacter);
                    Object.Destroy(fightObjectModel.AimCharacter.Transform.gameObject);
                }
                else
                {
                    fightObjectModel.AimCharacter.Transform.gameObject.SetActive(false);
                }
            }
        }
        
        private void DistanceAttack()
        {
            if (attackableView.FirePoint == null || fightObjectModel.AimCharacter == null)
                return;

            var projectile = projectileFactory.CreateProjectile(attackableView.ProjectileType, attackableView.FirePoint.position);
            projectile.damage = fightObjectModel.DamageAmount;
            projectile.ownerWarSide = fightObjectModel.WarSide;
            projectile.LaunchToPoint(fightObjectModel.AimCharacter.Transform.position, attackableView.ProjectileSpeed);
        }
    }
}
