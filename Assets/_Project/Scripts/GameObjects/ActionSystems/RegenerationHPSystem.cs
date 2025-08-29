using System;
using _General.Scripts._GlobalLogic;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Interfaces.Model;
using _Project.Scripts.Interfaces.View;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters
{
    public class RegenerationHPSystem : IDisposable
    {
        private readonly IHealthModel _healthModel;
        private readonly IHealthView _healthView;

        public RegenerationHPSystem(IHealthModel healthModel, IHealthView healthView)
        {
            _healthModel = healthModel;
            _healthView = healthView;
            
            GameTimer.I.OnEverySecond += TryRegenerateHealth;
        }

        private void TryRegenerateHealth()
        {
            if(Mathf.Approximately(_healthModel.CurrentHealth, _healthModel.MaxHealth))
                return;

            if (_healthModel.SecondsWithoutDamage <= _healthModel.DelayRegeneration)
            {
                _healthModel.SecondsWithoutDamage++;
                return;
            }
            
            _healthModel.CurrentHealth += _healthModel.RegenerateHealthInSecond;
            _healthView.UpdateHealthBar(_healthModel.CurrentHealth, _healthModel.MaxHealth);
        }

        public void Dispose()
        {
            GameTimer.I.OnEverySecond -= TryRegenerateHealth;
        }
    }
}
