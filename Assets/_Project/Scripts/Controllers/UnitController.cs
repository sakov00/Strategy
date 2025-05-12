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
    public class UnitController : MonoBehaviour
    {
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
            detectionAim.DetectAim();
            if(characterModel.InputVector.HasValue)
                movementSystem.MoveTo(characterModel.InputVector.Value);
            damageSystem.Attack();
            characterView.UpdateView();
        }
    }
}