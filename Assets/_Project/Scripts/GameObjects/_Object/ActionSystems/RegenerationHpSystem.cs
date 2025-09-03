using System;
using _General.Scripts._GlobalLogic;
using _Project.Scripts.GameObjects._Object;
using _Project.Scripts.Interfaces.Model;
using _Project.Scripts.Interfaces.View;
using UnityEngine;

namespace _Project.Scripts.GameObjects.ActionSystems
{
    public class RegenerationHpSystem : IDisposable
    {
        private readonly ObjectModel _objectModel;
        private readonly ObjectView _objectView;

        public RegenerationHpSystem(ObjectModel healthModel, ObjectView objectView)
        {
            _objectModel = healthModel;
            _objectView = objectView;
            
            GameTimer.I.OnEverySecond += TryRegenerateHealth;
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
            GameTimer.I.OnEverySecond -= TryRegenerateHealth;
        }
    }
}
