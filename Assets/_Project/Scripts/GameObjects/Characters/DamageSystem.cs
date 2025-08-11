using _Project.Scripts._VContainer;
using _Project.Scripts.Enums;
using _Project.Scripts.Extentions;
using _Project.Scripts.Factories;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Interfaces.View;
using _Project.Scripts.Registries;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

namespace _Project.Scripts.GameObjects.Characters
{
    public class DamageSystem
    {
        [Inject] private ProjectileFactory _projectileFactory;
        [Inject] private HealthRegistry _healthRegistry;
        
        private readonly IFightObjectModel _fightObjectModel;
        private readonly IAttackableView _attackableView;
        private readonly Transform _transform;
        
        private float _lastAttackTime = -Mathf.Infinity;
        private Coroutine _attackCoroutine;

        public DamageSystem(IFightObjectModel fightObjectModel, IAttackableView attackableView, Transform transform)
        {
            _fightObjectModel = fightObjectModel;
            _attackableView = attackableView;
            _transform = transform;
            
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
            if (_fightObjectModel.AimCharacter == null || _fightObjectModel.AimCharacter.Equals(null))
                return;
            
            if (Time.time - _lastAttackTime < _fightObjectModel.AllAnimAttackTime)
                return;
            
            var distance = PositionExtention.GetDistanceBetweenObjects(_transform, _fightObjectModel.AimCharacter.Transform);
            if (distance > _fightObjectModel.AttackRange)
            {
                _attackableView.SetWalking(true);
                return;
            }
            
            _lastAttackTime = Time.time;
            _attackableView.SetAttack(true);
        }

        private void MeleeAttack()
        {
            if (_fightObjectModel.AimCharacter == null)
            {
                _attackableView.SetWalking(true);
                return;
            }

            var distance = PositionExtention.GetDistanceBetweenObjects(_transform, _fightObjectModel.AimCharacter.Transform);
            if (distance > _fightObjectModel.AttackRange)
            {
                _attackableView.SetWalking(true);
                return;
            }
            
            _fightObjectModel.AimCharacter.CurrentHealth -= _fightObjectModel.DamageAmount;
            if (_fightObjectModel.AimCharacter.CurrentHealth <= 0)
            {
                if (_fightObjectModel.AimCharacter.WarSide == WarSide.Enemy)
                {
                    _healthRegistry.Unregister(_fightObjectModel.AimCharacter);
                    Object.Destroy(_fightObjectModel.AimCharacter.Transform.gameObject);
                }
                else
                {
                    _fightObjectModel.AimCharacter.Transform.gameObject.SetActive(false);
                }
            }
        }
        
        private void DistanceAttack()
        {
            if (_attackableView.FirePoint == null || _fightObjectModel.AimCharacter == null)
                return;

            var projectile = _projectileFactory.CreateProjectile(_attackableView.ProjectileType, _attackableView.FirePoint.position);
            projectile.damage = _fightObjectModel.DamageAmount;
            projectile.ownerWarSide = _fightObjectModel.WarSide;
            projectile.LaunchToPoint(_fightObjectModel.AimCharacter.Transform.position, _attackableView.ProjectileSpeed);
        }
    }
}
