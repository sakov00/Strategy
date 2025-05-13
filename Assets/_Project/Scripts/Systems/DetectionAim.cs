using _Project.Scripts.Models;
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
                var aimCharacterModel = hit.GetComponent<CharacterModel>();
                if (aimCharacterModel == null || aimCharacterModel == characterModel || aimCharacterModel.warSide == characterModel.warSide) continue;
                
                characterModel.aimCharacter = aimCharacterModel;
                
                Vector3 direction = aimCharacterModel.transform.position - transform.position;
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
