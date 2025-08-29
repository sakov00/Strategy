using _General.Scripts._GlobalLogic;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Info
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] protected MeshRenderer _healthBarRenderer;
        private MaterialPropertyBlock _matBlock;

        private void OnValidate()
        {
            _healthBarRenderer ??= GetComponent<MeshRenderer>();
        }

        public virtual void Start() 
        {
            _healthBarRenderer.enabled = false;
            _matBlock = new MaterialPropertyBlock();
        }
        
        public void UpdateHealthBar(float currentHealth, float maxHealth) 
        {
            if (currentHealth < maxHealth) 
            {
                _healthBarRenderer.enabled = true;
                HealthBarLookAtCamera();
                ChangeHealthBarValue(currentHealth, maxHealth);
            } 
            else 
            {
                _healthBarRenderer.enabled = false;
            }
        }

        private void ChangeHealthBarValue(float currentHealth, float maxHealth) 
        {
            _healthBarRenderer.GetPropertyBlock(_matBlock);
            _matBlock.SetFloat("_Fill", currentHealth / maxHealth);
            _healthBarRenderer.SetPropertyBlock(_matBlock);
        }

        private void HealthBarLookAtCamera() 
        {
            if (GlobalObjects.CameraController != null) 
            {
                var camXform = GlobalObjects.CameraController.transform;
                var forward = _healthBarRenderer.transform.position - camXform.position;
                forward.Normalize();
                var up = Vector3.Cross(forward, camXform.right);
                _healthBarRenderer.transform.rotation = Quaternion.LookRotation(forward, up);
            }
        }
    }
}