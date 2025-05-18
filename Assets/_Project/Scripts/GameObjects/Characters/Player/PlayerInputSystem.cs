using UnityEngine;
using VContainer.Unity;

namespace _Project.Scripts.GameObjects.Characters.Player
{
    public class PlayerInputSystem : ITickable
    {
        private Vector3 inputVector;
    
        public void Tick()
        {
            inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }

        public Vector3 GetInputVector()
        {
            return inputVector;
        }
    }
}
