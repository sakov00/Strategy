using _Project.Scripts._GlobalLogic;
using Joystick_Pack.Scripts.Base;
using TMPro;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.Windows
{
    public class GameWindow : BaseWindow
    {
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] public Joystick joystick;
        
        private void Start()
        {
            GlobalObjects.GameData.levelData.MoneyReactive
                .Subscribe(value => moneyText.text = $"Money: {value}")
                .AddTo(this);
        }
    }
}