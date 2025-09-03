using _General.Scripts._GlobalLogic;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Info
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _currentLvlText;
        [SerializeField] private TextMeshPro _costNextLvlText;

        public void UpdateTooltip(int? currentLvl, int? costUpgrade)
        {
            _currentLvlText.text = currentLvl != null ? $"LVL:{currentLvl}" : "LVL:MAX$";
            _costNextLvlText.text = costUpgrade != null ? $"Price:{costUpgrade}$" : "Price:MAX$";
            TooltipLookAtCamera();
        }
        
        private void TooltipLookAtCamera() 
        {
            if (GlobalObjects.CameraController != null) 
            {
                var camXform = GlobalObjects.CameraController.transform;
                var forward = transform.position - camXform.position;
                forward.Normalize();
                var up = Vector3.Cross(forward, camXform.right);
                transform.rotation = Quaternion.LookRotation(forward, up);
            }
        }
    }
}