using _Project.Scripts.GameObjects._General;
using UnityEngine;

namespace _Project.Scripts.GameObjects.MoneyBuild
{
    [RequireComponent(typeof(HealthBarView))]
    public class MoneyBuildController : BuildController
    {
        [SerializeField] private MoneyBuildModel model;
        [SerializeField] private MoneyBuildingView view;
        
        private MoneyController moneyController;
        
        private void Awake()
        {
            BuildModel = model;
            BuildView = view;
            
            moneyController = new MoneyController(model, view);
        }

        private void FixedUpdate()
        {
            view.UpdateHealthBar(model.currentHealth, model.maxHealth);
        }
    }
}