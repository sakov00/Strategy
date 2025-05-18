using _Project.Scripts.GameObjects._General;
using _Project.Scripts.GameObjects.Characters;
using _Project.Scripts.GameObjects.MoneyBuild;
using UnityEngine;

namespace _Project.Scripts.GameObjects._Controllers
{
    [RequireComponent(typeof(TowerDefenceModel))]
    [RequireComponent(typeof(DetectionAim))]
    [RequireComponent(typeof(DamageSystem))]
    [RequireComponent(typeof(HealthBarView))]
    public class TowerDefenceController : MonoBehaviour
    {
        private TowerDefenceModel towerDefenceModel;
        private DetectionAim detectionAim;
        private DamageSystem damageSystem;
        private HealthBarView healthBarView;

        private void OnValidate()
        {
            towerDefenceModel ??= GetComponent<TowerDefenceModel>();
            detectionAim ??= GetComponent<DetectionAim>();
            damageSystem ??= GetComponent<DamageSystem>();
            healthBarView ??= GetComponent<HealthBarView>();
        }

        private void Update()
        {            
            detectionAim.DetectAim();
            damageSystem.Attack();
            healthBarView.UpdateView();
        }
    }
}