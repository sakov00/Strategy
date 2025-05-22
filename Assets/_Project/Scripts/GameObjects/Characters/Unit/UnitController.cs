using System;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.GameObjects.Characters;
using _Project.Scripts.GameObjects.Characters.Unit;
using UnityEngine;

namespace _Project.Scripts.GameObjects._Controllers
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

        private void Update()
        {
            detectionAim.DetectAim();
            unitMovementSystem.MoveToAim();
            damageSystem.Attack();
            healthBarView.UpdateView();
        }

        private Vector3 CalculateDirection()
        {
            if (characterModel.AimCharacter == null || characterModel.AimCharacter.Equals(null))
                return Vector3.zero;
            
            var inputVector = Vector3.zero;
            Vector3 direction = characterModel.AimCharacter.Transform.position - transform.position;
            float distance = direction.magnitude;
            if (distance > characterModel.AttackRange)
            {
                inputVector = new Vector3(direction.x, 0, direction.z).normalized;
            }
            return inputVector;
        }
    }
}