using _Project.Scripts._GlobalLogic;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.GameObjects.UI
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] private TextMeshPro currentLvlText;
        [SerializeField] private TextMeshPro costNextLvlText;

        public void UpdateTooltip(int currentLvl, int costUpgrade)
        {
            currentLvlText.text = $"LVL:{currentLvl}";
            costNextLvlText.text = $"Price:{costUpgrade}$";
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