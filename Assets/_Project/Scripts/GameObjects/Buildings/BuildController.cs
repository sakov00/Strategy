using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.GameObjects.Characters;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects
{
    public abstract class BuildController : ObjectController, IUpgradableController
    {
        public BuildModel BuildModel { get; protected set; }
        public BuildView BuildView { get; protected set; }

        protected override void Initialize()
        {
            Model = BuildModel;
            View = BuildView;
            
            base.Initialize();
        }

        public virtual void Upgrade()
        {
            BuildModel.CurrentLevel++;
        }
    }
}