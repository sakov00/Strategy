using _Project.Scripts.GameObjects._General;
using UnityEngine;

namespace _Project.Scripts.GameObjects.MoneyBuild
{
    public class MoneyBuildController : BuildController
    {
        [SerializeField] private MoneyBuildModel model;
        [SerializeField] private MoneyBuildingView view;
        
        private MoneyController moneyController;
        
        protected override void Initialize()
        {
            BuildModel = model;
            BuildView = view;
            
            moneyController = new MoneyController(model, view);
            base.Initialize();
        }
    }
}