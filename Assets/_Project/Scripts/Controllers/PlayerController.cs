using _Project.Scripts.Models;
using _Project.Scripts.Systems;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Controllers
{
    [RequireComponent(typeof(CharacterModel))]
    [RequireComponent(typeof(Movement))]
    public class PlayerController : MonoBehaviour
    {
        [Inject] private PlayerInputSystem playerInputSystem;
        [SerializeField] private CharacterModel characterModel;
        [SerializeField] private Movement movement;

        private void OnValidate()
        {
            characterModel ??= GetComponent<CharacterModel>();
            movement ??= GetComponent<Movement>();
        }

        private void Update()
        {
            var movementDirection = playerInputSystem.GetInputVector();
            movement.Move(movementDirection);
        }
    }
}