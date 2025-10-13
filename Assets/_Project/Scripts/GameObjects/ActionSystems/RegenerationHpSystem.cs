using System;
using _General.Scripts._GlobalLogic;
using _General.Scripts._VContainer;
using _Project.Scripts.GameObjects.Abstract.BaseObject;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.ActionSystems
{
    public class RegenerationHpSystem : IDisposable
    {
        private readonly ObjectModel _objectModel;
        private readonly ObjectView _objectView;
        [Inject] private GameTimer _gameTimer;

        public RegenerationHpSystem(ObjectModel healthModel, ObjectView objectView)
        {
            InjectManager.Inject(this);
            _objectModel = healthModel;
            _objectView = objectView;

            _gameTimer.Subscribe(1f, TryRegenerateHealth);
        }

        public void Dispose()
        {
            _gameTimer.Unsubscribe(TryRegenerateHealth);
        }

        private void TryRegenerateHealth()
        {
            if (Mathf.Approximately(_objectModel.CurrentHealth, _objectModel.MaxHealth))
                return;

            if (_objectModel.SecondsWithoutDamage <= _objectModel.DelayRegeneration)
            {
                _objectModel.SecondsWithoutDamage++;
                return;
            }

            _objectModel.CurrentHealth += _objectModel.RegenerateHealthInSecond;
            _objectView.UpdateHealthBar(_objectModel.CurrentHealth, _objectModel.MaxHealth);
        }
    }
}