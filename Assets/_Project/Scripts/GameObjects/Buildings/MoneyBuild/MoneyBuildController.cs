using System.Runtime.InteropServices;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.Windows.Presenters;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.MoneyBuild
{
    public class MoneyBuildController : BuildController
    {
        [Inject] private GameWindowViewModel gameWindowViewModel;
        [SerializeField] private MoneyBuildModel model;
        [SerializeField] private MoneyBuildingView view;
        
        private MoneyController moneyController;
        
        protected override void Initialize()
        {
            BuildModel = model;
            BuildView = view;
            
            moneyController = new MoneyController(model, view, gameWindowViewModel);
            base.Initialize();
        }
    }
}