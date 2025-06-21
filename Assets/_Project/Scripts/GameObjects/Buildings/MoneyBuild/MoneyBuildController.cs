using System;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.MoneyBuild
{
    public class MoneyBuildController : BuildController
    {
        [SerializeField] protected MoneyBuildModel model;
        [SerializeField] protected MoneyBuildingView view;
        public override BuildModel BuildModel => model;
        public override BuildView BuildView => view;
        
        private MoneyController moneyController;

        protected override void Initialize()
        {
            moneyController = new MoneyController(model, view);
            base.Initialize();
        }

        private void OnDestroy()
        {
            moneyController.Dispose();
        }
    }
}