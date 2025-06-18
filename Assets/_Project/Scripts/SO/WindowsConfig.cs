using _Project.Scripts.GameObjects.Characters.Player;
using _Project.Scripts.GameObjects.Characters.Unit;
using _Project.Scripts.Windows;
using _Project.Scripts.Windows.Presenters;
using UnityEngine;

namespace _Project.Scripts.SO
{
    [CreateAssetMenu(fileName = "WindowsConfig", menuName = "SO/Windows Config")]
    public class WindowsConfig : ScriptableObject
    {
        public GameWindowViewModel gameWindowViewModel;
    }
}