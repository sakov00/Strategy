using _Project.Scripts.GameObjects._General;
using _Project.Scripts.GameObjects.Characters;
using _Project.Scripts.GameObjects.Characters.Player;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects._Controllers
{
    [RequireComponent(typeof(CharacterModel))]
    [RequireComponent(typeof(PlayerMovementSystem))]
    [RequireComponent(typeof(DetectionAim))]
    [RequireComponent(typeof(DamageSystem))]
    [RequireComponent(typeof(HealthBarView))]
    public class PlayerController : MonoBehaviour
    {
        [Inject] private PlayerInputSystem playerInputSystem;
        [SerializeField, HideInInspector] private CharacterModel characterModel;
        [SerializeField, HideInInspector]  private PlayerMovementSystem playerMovementSystem;
        [SerializeField, HideInInspector] private DetectionAim detectionAim;
        [SerializeField, HideInInspector] private DamageSystem damageSystem;
        [SerializeField, HideInInspector] private HealthBarView healthBarView;

        private void OnValidate()
        {
            characterModel ??= GetComponent<CharacterModel>();
            playerMovementSystem ??= GetComponent<PlayerMovementSystem>();
            detectionAim ??= GetComponent<DetectionAim>();
            damageSystem ??= GetComponent<DamageSystem>();
            healthBarView ??= GetComponent<HealthBarView>();
        }

        private void Update()
        {
            var movementDirection = playerInputSystem.GetInputVector();
            playerMovementSystem.MoveTo(movementDirection);
            detectionAim.DetectAim();
            damageSystem.Attack();
            healthBarView.UpdateView();
        }
    }
}