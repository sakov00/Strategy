using _Project.Scripts._GlobalLogic;
using UnityEngine;

namespace _Project.Scripts.GameObjects.MoneyBuild
{
    public class MoneyBuildSystem : MonoBehaviour
    {
        [SerializeField, HideInInspector] private MoneyBuildModel moneyBuildModel;

        private void OnValidate()
        {
            moneyBuildModel ??= GetComponent<MoneyBuildModel>();
        }

        private void Start()
        {
            GameTimer.Instance.OnEverySecond += AddMoneyToPlayer;
        }

        private void AddMoneyToPlayer()
        {
            GlobalObjects.GameData.levelData.Money += moneyBuildModel.addMoneyValue * moneyBuildModel.CurrentLevel;
        }

        private void OnDestroy()
        {
            GameTimer.Instance.OnEverySecond -= AddMoneyToPlayer;
        }
    }
}