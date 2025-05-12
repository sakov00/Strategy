using _Project.Scripts.Models;
using UnityEngine;

namespace _Project.Scripts.Views
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer healthBarRenderer;
        MaterialPropertyBlock matBlock;
        CharacterModel characterModel;
        Camera mainCamera;

        private void OnValidate() 
        {
            characterModel ??= GetComponent<CharacterModel>(); 
        }

        private void Start() 
        {
            matBlock = new MaterialPropertyBlock();
            mainCamera = Camera.main;
        }

        public void UpdateView() 
        {
            if (characterModel.currentHealth < characterModel.maxHealth) 
            {
                healthBarRenderer.enabled = true;
                HealthBarLookAtCamera();
                UpdateHealthBar();
            } 
            else 
            {
                healthBarRenderer.enabled = false;
            }
        }

        private void UpdateHealthBar() 
        {
            healthBarRenderer.GetPropertyBlock(matBlock);
            matBlock.SetFloat("_Fill", characterModel.currentHealth / characterModel.maxHealth);
            healthBarRenderer.SetPropertyBlock(matBlock);
        }

        private void HealthBarLookAtCamera() 
        {
            if (mainCamera != null) 
            {
                var camXform = mainCamera.transform;
                var forward = healthBarRenderer.transform.position - camXform.position;
                forward.Normalize();
                var up = Vector3.Cross(forward, camXform.right);
                healthBarRenderer.transform.rotation = Quaternion.LookRotation(forward, up);
            }
        }

    }
}