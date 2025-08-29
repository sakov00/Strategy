using _General.Scripts._VContainer;
using _General.Scripts.Extentions;
using _General.Scripts.Registries;
using _Project.Scripts._VContainer;
using _Project.Scripts.Enums;
using _Project.Scripts.Factories;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Interfaces.Model;
using _Project.Scripts.Interfaces.View;
using UnityEditor;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

namespace _Project.Scripts.GameObjects.Characters
{
    public class DamageSystem
    {
        [Inject] private ProjectileFactory _projectileFactory;
        [Inject] private ObjectsRegistry _objectsRegistry;
        
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
            if (_fightObjectModel.AimObject == null || _fightObjectModel.AimObject.Equals(null))
                return;
            
            if (Time.time - _lastAttackTime < _fightObjectModel.AllAnimAttackTime)
                return;
            
            var distance = PositionExtention.GetDistanceBetweenObjects(_transform, _fightObjectModel.AimObject.ObjectModel.Transform);
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
            if (_fightObjectModel.AimObject == null)
            {
                _attackableView.SetWalking(true);
                return;
            }

            var distance = PositionExtention.GetDistanceBetweenObjects(_transform, _fightObjectModel.AimObject.ObjectModel.Transform);
            if (distance > _fightObjectModel.AttackRange)
            {
                _attackableView.SetWalking(true);
                return;
            }
            
            _fightObjectModel.AimObject.ObjectModel.CurrentHealth -= _fightObjectModel.DamageAmount;
            if (_fightObjectModel.AimObject.ObjectModel.CurrentHealth <= 0)
            {
                _objectsRegistry.Unregister(_fightObjectModel.AimObject);
                Object.Destroy(_fightObjectModel.AimObject.ObjectModel.Transform.gameObject);
                //_fightObjectModel.AimObject.ObjectModel.Transform.gameObject.SetActive(false);
            }
        }
        
        private void DistanceAttack()
        {
            if (_attackableView.FirePoint == null || _fightObjectModel.AimObject == null)
                return;

            var projectile = _projectileFactory.CreateProjectile(_attackableView.ProjectileType, _attackableView.FirePoint.position);
            projectile._damage = _fightObjectModel.DamageAmount;
            projectile._ownerWarSide = _fightObjectModel.WarSide;
            projectile.LaunchToPoint(_fightObjectModel.AimObject.ObjectModel.Transform.position + 
                                     Vector3.up * (_fightObjectModel.AimObject.ObjectModel.HeightObject / 2),
                _attackableView.ProjectileSpeed);
        }
    }
}
