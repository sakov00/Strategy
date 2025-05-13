using _Project.Scripts.Models;
using _Project.Scripts.Systems;
using _Project.Scripts.Views;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Controllers
{
    [RequireComponent(typeof(CharacterModel))]
    [RequireComponent(typeof(MovementSystem))]
    [RequireComponent(typeof(DetectionAim))]
    [RequireComponent(typeof(DamageSystem))]
    [RequireComponent(typeof(CharacterView))]
    public class PlayerController : MonoBehaviour
    {
        [Inject] private PlayerInputSystem playerInputSystem;
        private CharacterModel characterModel;
        private MovementSystem movementSystem;
        private DetectionAim detectionAim;
        private DamageSystem damageSystem;
        private CharacterView characterView;

        private void OnValidate()
        {
            characterModel ??= GetComponent<CharacterModel>();
            movementSystem ??= GetComponent<MovementSystem>();
            detectionAim ??= GetComponent<DetectionAim>();
            damageSystem ??= GetComponent<DamageSystem>();
            characterView ??= GetComponent<CharacterView>();
        }

        private void Update()
        {
            var movementDirection = playerInputSystem.GetInputVector();
            movementSystem.MoveTo(movementDirection);
            detectionAim.DetectAim();
            damageSystem.Attack();
            characterView.UpdateView();
        }
    }
}