using _Project.Scripts.GameObjects._General;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters.Unit
{
    [RequireComponent(typeof(CharacterModel))]
    [RequireComponent(typeof(UnitMovementSystem))]
    [RequireComponent(typeof(DetectionAim))]
    [RequireComponent(typeof(DamageSystem))]
    [RequireComponent(typeof(HealthBarView))]
    public class UnitController : MonoBehaviour
    {
        [SerializeField, HideInInspector] private CharacterModel characterModel;
        [SerializeField, HideInInspector] private UnitMovementSystem unitMovementSystem;
        [SerializeField, HideInInspector] private DetectionAim detectionAim;
        [SerializeField, HideInInspector] private DamageSystem damageSystem;
        [SerializeField, HideInInspector] private HealthBarView healthBarView;

        private void OnValidate()
        {
            characterModel ??= GetComponent<CharacterModel>();
            unitMovementSystem ??= GetComponent<UnitMovementSystem>();
            detectionAim ??= GetComponent<DetectionAim>();
            damageSystem ??= GetComponent<DamageSystem>();
            healthBarView ??= GetComponent<HealthBarView>();
        }

        private void FixedUpdate()
        {
            detectionAim.DetectAim();
            unitMovementSystem.MoveToAim();
            damageSystem.Attack();
            healthBarView.UpdateView();
        }
    }
}