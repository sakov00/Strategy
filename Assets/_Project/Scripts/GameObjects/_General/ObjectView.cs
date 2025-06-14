using System;
using _Project.Scripts._GlobalLogic;
using UnityEngine;

namespace _Project.Scripts.GameObjects._General
{
    public class ObjectView : MonoBehaviour
    {
        [SerializeField] protected MeshRenderer healthBarRenderer;
        [SerializeField] public Renderer objRenderer;
        
        private MaterialPropertyBlock matBlock;

        public virtual void Initialize() 
        {
            healthBarRenderer.enabled = false;
            matBlock = new MaterialPropertyBlock();
        }

        public void UpdateHealthBar(float currentHealth, float maxHealth) 
        {
            if (currentHealth < maxHealth) 
            {
                healthBarRenderer.enabled = true;
                HealthBarLookAtCamera();
                ChangeHealthBarValue(currentHealth, maxHealth);
            } 
            else 
            {
                healthBarRenderer.enabled = false;
            }
        }

        private void ChangeHealthBarValue(float currentHealth, float maxHealth) 
        {
            healthBarRenderer.GetPropertyBlock(matBlock);
            matBlock.SetFloat("_Fill", currentHealth / maxHealth);
            healthBarRenderer.SetPropertyBlock(matBlock);
        }

        private void HealthBarLookAtCamera() 
        {
            if (GlobalObjects.CameraFollow.camera != null) 
            {
                var camXform = GlobalObjects.CameraFollow.camera.transform;
                var forward = healthBarRenderer.transform.position - camXform.position;
                forward.Normalize();
                var up = Vector3.Cross(forward, camXform.right);
                healthBarRenderer.transform.rotation = Quaternion.LookRotation(forward, up);
            }
        }
    }
}