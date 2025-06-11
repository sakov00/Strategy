using System;
using _Project.Scripts._GlobalLogic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.GameObjects.MoneyBuild
{
    [Serializable]
    public class MoneyBuildingView : BuildView
    {
        [SerializeField] private TextMeshPro moneyUpText;

        public void MoneyUp(int value)
        {
            moneyUpText.text = "+" + value;
            moneyUpText.transform
                .DOLocalMoveY(1, 0.5f)
                .From(0);
        }
    }
}