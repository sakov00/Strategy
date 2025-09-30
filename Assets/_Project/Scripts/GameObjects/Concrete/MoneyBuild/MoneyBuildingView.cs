using System;
using _Project.Scripts.GameObjects.Abstract;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Concrete.MoneyBuild
{
    [Serializable]
    public class MoneyBuildingView : BuildView
    {
        [SerializeField] private TextMeshPro _moneyUpText;

        public override void Initialize()
        {
        }

        public void MoneyUp(int value)
        {
            _moneyUpText.text = "+" + value;
            _moneyUpText.transform
                .DOLocalMoveY(1, 0.5f)
                .From(0);
        }
    }
}