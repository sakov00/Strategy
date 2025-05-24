using _Project.Scripts.GameObjects._General;
using _Project.Scripts.GameObjects.Characters;
using UnityEngine;

namespace _Project.Scripts.GameObjects.TowerDefence
{
    [RequireComponent(typeof(TowerDefenceModel))]
    [RequireComponent(typeof(DetectionAim))]
    [RequireComponent(typeof(DamageSystem))]
    [RequireComponent(typeof(HealthBarView))]
    public class TowerDefenceController : MonoBehaviour
    {
        [SerializeField, HideInInspector] private TowerDefenceModel towerDefenceModel;
        [SerializeField, HideInInspector] private DetectionAim detectionAim;
        [SerializeField, HideInInspector] private DamageSystem damageSystem;
        [SerializeField, HideInInspector] private HealthBarView healthBarView;

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