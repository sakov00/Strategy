using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerModel : CharacterModel
    {
        [SerializeField, HideInInspector] public CharacterController characterController;
        
        private void OnValidate()
        {
            characterController ??= GetComponent<CharacterController>();
        }
    }
}