using _Project.Scripts.Models;
using _Project.Scripts.Systems;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Controllers
{
    [RequireComponent(typeof(CharacterModel))]
    [RequireComponent(typeof(MovementSystem))]
    [RequireComponent(typeof(DetectionAim))]
    [RequireComponent(typeof(DamageSystem))]
    public class PlayerController : MonoBehaviour
    {
        [Inject] private PlayerInputSystem playerInputSystem;
        private CharacterModel characterModel;
        private MovementSystem movementSystem;
        private DetectionAim detectionAim;
        private DamageSystem damageSystem;

        private void OnValidate()
        {
            characterModel ??= GetComponent<CharacterModel>();
            movementSystem ??= GetComponent<MovementSystem>();
            detectionAim ??= GetComponent<DetectionAim>();
            damageSystem ??= GetComponent<DamageSystem>();
        }

        private void Update()
        {
            var movementDirection = playerInputSystem.GetInputVector();
            movementSystem.MoveTo(movementDirection);
            detectionAim.DetectAim();
            damageSystem.Attack();
        }
    }
}