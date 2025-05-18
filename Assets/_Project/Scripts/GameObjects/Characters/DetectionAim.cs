using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters
{
    [RequireComponent(typeof(IFightObject))]
    public class DetectionAim : MonoBehaviour
    {
        private IFightObject attackableModel;

        private void OnValidate()
        {
            attackableModel ??= GetComponent<IFightObject>();
        }

        public void DetectAim()
        {
            if (attackableModel.AimCharacter != null && !attackableModel.AimCharacter.Equals(null))
                return;
            
            var hitBuffer = Physics.OverlapSphere(transform.position, attackableModel.DetectionRadius);
            foreach (var hit in hitBuffer)
            {
                var damagable = hit.GetComponent<IDamagable>();
                if (damagable == null || ReferenceEquals(damagable, attackableModel) || damagable.WarSide == attackableModel.WarSide) continue;
                
                attackableModel.AimCharacter = damagable;
                break;
            }
        }
    }
}
