using _General.Scripts._VContainer;
using _General.Scripts.Extentions;
using _General.Scripts.Registries;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces.Model;
using _Project.Scripts.Interfaces.View;
using _Project.Scripts.Pools;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.ActionSystems
{
    public class DamageSystem
    {
        [Inject] private ProjectilePool _projectilePool;
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
            
            var distance = PositionExtention.GetDistanceBetweenObjects(_transform, _fightObjectModel.AimObject.transform);
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

            var distance = PositionExtention.GetDistanceBetweenObjects(_transform, _fightObjectModel.AimObject.transform);
            if (distance > _fightObjectModel.AttackRange)
            {
                _attackableView.SetWalking(true);
                return;
            }
            
            _fightObjectModel.AimObject.ObjectModel.CurrentHealth -= _fightObjectModel.DamageAmount;
            if (_fightObjectModel.AimObject.ObjectModel.CurrentHealth <= 0)
            {
                _fightObjectModel.AimObject.ReturnToPool();
                //_fightObjectModel.AimObject.ObjectModel.Transform.gameObject.SetActive(false);
            }
        }
        
        private void DistanceAttack()
        {
            if (_attackableView.FirePoint == null || _fightObjectModel.AimObject == null)
                return;

            var projectile = _projectilePool.Get(_attackableView.ProjectileType, _attackableView.FirePoint.position);
            projectile._damage = _fightObjectModel.DamageAmount;
            projectile._ownerWarSide = _fightObjectModel.WarSide;
            projectile.LaunchToPoint(_fightObjectModel.AimObject.transform.position + 
                                     Vector3.up * (_fightObjectModel.AimObject.ObjectModel.HeightObject / 2),
                _attackableView.ProjectileSpeed);
        }
    }
}
