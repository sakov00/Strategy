using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects._General
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer healthBarRenderer;
        private MaterialPropertyBlock matBlock;
        private IDamagable damagable;
        private Camera mainCamera;

        private void OnValidate() 
        {
            damagable ??= GetComponent<IDamagable>(); 
        }

        private void Start() 
        {
            matBlock = new MaterialPropertyBlock();
            mainCamera = Camera.main;
            damagable ??= GetComponent<IDamagable>(); 
        }

        public void UpdateView() 
        {
            if (damagable.CurrentHealth < damagable.MaxHealth) 
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
            matBlock.SetFloat("_Fill", damagable.CurrentHealth / damagable.MaxHealth);
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