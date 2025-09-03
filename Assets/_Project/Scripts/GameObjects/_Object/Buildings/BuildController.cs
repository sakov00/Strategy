using _General.Scripts.AllAppData; 
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Pools;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects._Object
{
    public abstract class BuildController : ObjectController, IBuy
    {
        [Inject] private BuildPool _buildPool;
        
        public abstract BuildModel BuildModel { get; }
        public abstract BuildView BuildView { get; }
        public override ObjectModel ObjectModel => BuildModel;
        public override ObjectView ObjectView => BuildView;
        
        private Vector3 _originalScale;
        
        public virtual async UniTask TryBuy()
        {
            if (_originalScale == Vector3.zero)
                _originalScale = transform.localScale;
            
            var reducedScale = _originalScale * 0.9f;
            
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(reducedScale, 0.25f));
            sequence.Append(transform.DOScale(_originalScale, 0.25f));
            await sequence.Play();
            int? upgradePrice = null;
            if(BuildModel.CurrentLevel < BuildModel.PriceList.Count)
                upgradePrice = BuildModel.PriceList[BuildModel.CurrentLevel];
            
            if (upgradePrice == null || upgradePrice > AppData.User.Money)
            {
                Debug.Log("Not enough money or max level");
                return;
            }
            
            AppData.User.Money -= upgradePrice.Value;
            BuildModel.CurrentLevel++;
            BuildView.UpdateTooltip(BuildModel.CurrentLevel, upgradePrice);
        }

        public override void ReturnToPool()
        {
            _buildPool.Return(this);
        }
    }
}