using _General.Scripts.AllAppData;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.Interfaces.Controller;
using _Project.Scripts.Pools;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects
{
    public abstract class BuildController : ObjectController, IBuyController
    {
        [field: SerializeField] public BuildType BuildType { get; set; }
        
        [Inject] private BuildPool _buildPool;
        
        public abstract BuildModel BuildModel { get; }
        public abstract BuildView BuildView  { get; }
        public override ObjectModel ObjectModel => BuildModel;
        public override ObjectView ObjectView => BuildView;
        
        private Vector3 _originalScale;
        
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            BuildView.UpdateTooltip(BuildModel.CurrentLevel, BuildModel.PriceList[BuildModel.CurrentLevel]);
        }
        
        public virtual async UniTask TryBuy()
        {
            if(_originalScale == Vector3.zero)
                _originalScale = transform.localScale;
            
            var reducedScale = _originalScale * 0.9f;
            
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(reducedScale, 0.25f));
            sequence.Append(transform.DOScale(_originalScale, 0.25f));
            await sequence.Play();
            
            if (BuildModel.PriceList[BuildModel.CurrentLevel] > AppData.User.Money)
            {
                Debug.Log("Not enough money");
                return;
            }
            AppData.User.Money -= BuildModel.PriceList[0];
            BuildModel.CurrentLevel++;
        }

        public override void ReturnToPool()
        {
            _buildPool.Return(this);
        }
    }
}