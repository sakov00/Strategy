using _Project.Scripts._GlobalLogic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.GameObjects.MoneyBuild
{
    public class MoneyBuildingView : MonoBehaviour
    {
        [SerializeField, HideInInspector]  private MoneyBuildModel moneyBuildModel;
        [SerializeField] private TextMeshPro moneyUpText;

        private void OnValidate()
        {
            moneyBuildModel ??= GetComponent<MoneyBuildModel>();
        }

        private void Start()
        {
            moneyBuildModel ??= GetComponent<MoneyBuildModel>();
            GameTimer.Instance.OnEverySecond += MoneyUpActivate;
        }

        private void MoneyUpActivate()
        {
            moneyUpText.text = "+" + moneyBuildModel.addMoneyValue * moneyBuildModel.currentLevel;
            moneyUpText.transform
                .DOLocalMoveY(1, 0.5f)
                .From(0);
        }

        private void OnDestroy()
        {
            GameTimer.Instance.OnEverySecond -= MoneyUpActivate;
        }
    }
}