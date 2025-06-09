using _Project.Scripts._GlobalLogic;
using _Project.Scripts.GameObjects._General;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Characters.Player
{
    [RequireComponent(typeof(CharacterModel))]
    [RequireComponent(typeof(PlayerMovementSystem))]
    [RequireComponent(typeof(DetectionAim))]
    [RequireComponent(typeof(DamageSystem))]
    [RequireComponent(typeof(HealthBarView))]
    public class PlayerController : MonoBehaviour
    {
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
            var inputVector = new Vector3(
                GlobalObjects.GameData.gameWindow.joystick.Direction.x,
                0,
                GlobalObjects.GameData.gameWindow.joystick.Direction.y);
            
            playerMovementSystem.MoveTo(inputVector);
            detectionAim.DetectAim();
            damageSystem.Attack();
            healthBarView.UpdateView();
        }
    }
}