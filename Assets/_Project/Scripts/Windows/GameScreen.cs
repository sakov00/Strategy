using _Project.Scripts._GlobalLogic;
using TMPro;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.Windows
{
    public class GameScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyText;

        private void Start()
        {
            GameData.Instance.levelData.MoneyReactive
                .Subscribe(value => moneyText.text = $"Money: {value}")
                .AddTo(this);
        }
    }
}