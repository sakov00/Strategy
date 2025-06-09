using _Project.Scripts.GameObjects.Characters.Player;
using _Project.Scripts.GameObjects.Characters.Unit;
using _Project.Scripts.Windows;
using UnityEngine;

namespace _Project.Scripts.SO
{
    [CreateAssetMenu(fileName = "WindowsConfig", menuName = "SO/Windows Config")]
    public class WindowsConfig : ScriptableObject
    {
        public GameWindow gameWindow;
    }
}