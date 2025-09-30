using _General.Scripts._VContainer;
using _General.Scripts.AllAppData;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract.BaseObject;
using _Project.Scripts.Pools;
using VContainer;

namespace _Project.Scripts.GameObjects.Abstract
{
    public abstract class BuildController : ObjectController
    {
        [Inject] protected AppData AppData;
        [Inject] protected BuildPool BuildPool;
        
        protected abstract BuildModel BuildModel { get; }
        protected abstract BuildView BuildView { get; }
        protected override ObjectModel ObjectModel => BuildModel;
        protected override ObjectView ObjectView => BuildView;

        public BuildType BuildType => BuildModel.BuildType;
        public int BuildPrice => BuildModel.BuildPrice;
        
        public override void Initialize()
        {
            InjectManager.Inject(this);
            base.Initialize();
            ObjectsRegistry.Register(this);
        }
        
        public override void DeleteFromScene(bool realDelete = false)
        {
            if(realDelete)
                ObjectsRegistry.Unregister(this);
            else
                BuildPool.Return(this);
        }
    }
}