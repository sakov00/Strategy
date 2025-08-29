using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.GameObjects.MoneyBuild
{
    [Serializable]
    public class MoneyBuildingView : BuildView
    {
        [SerializeField] private TextMeshPro _moneyUpText;

        public void MoneyUp(int value)
        {
            _moneyUpText.text = "+" + value;
            _moneyUpText.transform
                .DOLocalMoveY(1, 0.5f)
                .From(0);
        }
    }
}