using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.GameObjects.Characters;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Windows.Presenters;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects
{
    public abstract class BuildController : ObjectController, IUpgradableController
    {
        [Inject] protected GameWindowViewModel GameWindowViewModel;
        public abstract BuildModel BuildModel { get; }
        public abstract BuildView BuildView  { get; }
        public override ObjectModel ObjectModel => BuildModel;
        public override ObjectView ObjectView => BuildView;
        
        public virtual void TryUpgrade()
        {
            if (BuildModel.PriceList[BuildModel.CurrentLevel] > GameWindowViewModel.Money.Value)
            {
                Debug.Log("Not enough money");
                return;
            }
            GameWindowViewModel.AddMoney(-BuildModel.PriceList[0]);
            BuildModel.CurrentLevel++;
        }
    }
}