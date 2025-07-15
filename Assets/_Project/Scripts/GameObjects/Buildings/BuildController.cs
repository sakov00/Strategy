using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.GameObjects.Characters;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Windows.Presenters;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects
{
    public abstract class BuildController : ObjectController, IBuyController
    {
        public abstract BuildModel BuildModel { get; }
        public abstract BuildView BuildView  { get; }
        public override ObjectModel ObjectModel => BuildModel;
        public override ObjectView ObjectView => BuildView;
        
        private Vector3 originalScale;
        
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            BuildView.UpdateTooltip(BuildModel.CurrentLevel, BuildModel.PriceList[BuildModel.CurrentLevel]);
        }
        
        public virtual async UniTask TryBuy()
        {
            if(originalScale == Vector3.zero)
                originalScale = transform.localScale;
            
            var reducedScale = originalScale * 0.9f;
            
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(reducedScale, 0.25f));
            sequence.Append(transform.DOScale(originalScale, 0.25f));
            await sequence.Play();
            
            if (BuildModel.PriceList[BuildModel.CurrentLevel] > AppData.User.Money)
            {
                Debug.Log("Not enough money");
                return;
            }
            AppData.User.Money -= BuildModel.PriceList[0];
            BuildModel.CurrentLevel++;
        }
    }
}