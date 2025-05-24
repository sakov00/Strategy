using Joystick_Pack.Scripts.Base;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.GameObjects.Characters.Player
{
    public class PlayerInputSystem : ITickable
    {
        [Inject] private Joystick joystick;
        private Vector3 inputVector;
    
        public void Tick()
        {
            inputVector = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
        }

        public Vector3 GetInputVector()
        {
            return inputVector;
        }
    }
}
