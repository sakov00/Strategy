using _Project.Scripts.Models;
using _Project.Scripts.Systems;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Controllers
{
    [RequireComponent(typeof(CharacterModel))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(UnitInputSystem))]
    public class UnitController : MonoBehaviour
    {
        [SerializeField] private CharacterModel characterModel;
        [SerializeField] private Movement movement;
        [SerializeField] private UnitInputSystem unitInputSystem;

        private void OnValidate()
        {
            characterModel ??= GetComponent<CharacterModel>();
            movement ??= GetComponent<Movement>();
            unitInputSystem ??= GetComponent<UnitInputSystem>();
        }

        private void Update()
        {
            var movementDirection = unitInputSystem.GetInputVector();
            movement.Move(movementDirection);
        }
    }
}