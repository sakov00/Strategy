using System;
using _General.Scripts.UI.Info;
using _Project.Scripts.GameObjects.Abstract.Unit;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Concrete.Player
{
    [Serializable]
    public class PlayerView : UnitView
    {
        [SerializeField] private UniversalBar _loadBar;

        public override void Initialize()
        {
            base.Initialize();
        }

        public void UpdateLoadBar(float currentValue, float maxValue)
        {
            _loadBar.UpdateBar(currentValue, maxValue);
        }
    }
}