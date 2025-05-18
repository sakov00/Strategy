using _Project.Scripts._GlobalLogic;
using UnityEngine;

namespace _Project.Scripts.GameObjects.MoneyBuild
{
    public class MoneyBuildSystem : MonoBehaviour
    {
        [SerializeField] private MoneyBuildModel moneyBuildModel;

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
            GameData.Instance.levelData.Money += moneyBuildModel.addMoneyValue;
        }

        private void OnDestroy()
        {
            GameTimer.Instance.OnEverySecond -= AddMoneyToPlayer;
        }
    }
}