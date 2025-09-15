using System;
using _General.Scripts._GlobalLogic;
using _General.Scripts._VContainer;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects._Object.ActionSystems
{
    public class RegenerationHpSystem : IDisposable
    {
        [Inject] private GameTimer _gameTimer;
        private readonly ObjectModel _objectModel;
        private readonly ObjectView _objectView;

        public RegenerationHpSystem(ObjectModel healthModel, ObjectView objectView)
        {
            InjectManager.Inject(this);
            _objectModel = healthModel;
            _objectView = objectView;
            
            _gameTimer.OnEverySecond += TryRegenerateHealth;
        }

        private void TryRegenerateHealth()
        {
            if(Mathf.Approximately(_objectModel.CurrentHealth, _objectModel.MaxHealth))
                return;

            if (_objectModel.SecondsWithoutDamage <= _objectModel.DelayRegeneration)
            {
                _objectModel.SecondsWithoutDamage++;
                return;
            }
            
            _objectModel.CurrentHealth += _objectModel.RegenerateHealthInSecond;
            _objectView.UpdateHealthBar(_objectModel.CurrentHealth, _objectModel.MaxHealth);
        }

        public void Dispose()
        {
            _gameTimer.OnEverySecond -= TryRegenerateHealth;
        }
    }
}
