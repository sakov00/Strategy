using _Project.Scripts.Interfaces;
using _Project.Scripts.Models;
using _Project.Scripts.Models.Characters;
using UnityEngine;

namespace _Project.Scripts.Systems
{
    [RequireComponent(typeof(CharacterModel))]
    public class DetectionAim : MonoBehaviour
    {
        private CharacterModel characterModel;

        private void OnValidate()
        {
            characterModel ??= GetComponent<CharacterModel>();
        }

        public void DetectAim()
        {
            characterModel.InputVector = Vector3.zero;
            characterModel.aimCharacter = null;
            var hitBuffer = Physics.OverlapSphere(transform.position, characterModel.detectionRadius);
            foreach (var hit in hitBuffer)
            {
                var damagable = hit.GetComponent<IDamagable>();
                if (damagable == null || ReferenceEquals(damagable, characterModel) || damagable.WarSide == characterModel.WarSide) continue;
                
                characterModel.aimCharacter = damagable;
                
                Vector3 direction = damagable.Transform.position - transform.position;
                float distance = direction.magnitude;
                if (distance > characterModel.attackRange)
                {
                    characterModel.InputVector = new Vector3(direction.x, 0, direction.z).normalized;
                }

                break;
            }
        }
    }
}
