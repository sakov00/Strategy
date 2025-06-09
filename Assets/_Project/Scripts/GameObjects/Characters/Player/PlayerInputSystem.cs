using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Windows;
using Joystick_Pack.Scripts.Base;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.GameObjects.Characters.Player
{
    public class PlayerInputSystem : ITickable
    {
        private Vector3 inputVector;
    
        public void Tick()
        {
            inputVector = new Vector3(GlobalObjects.GameData.gameWindow.joystick.Direction.x, 0, GlobalObjects.GameData.gameWindow.joystick.Direction.y);
        }

        public Vector3 GetInputVector()
        {
            return inputVector;
        }
    }
}
