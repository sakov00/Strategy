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
            ObjectsRegistry.Register(this);
            IdsRegistry.Register(this);
            Dispose(false, false);
        }
        
        public override void Dispose(bool returnToPool = true, bool clearFromRegistry = true)
        {
            if(returnToPool) BuildPool.Return(this);
            if (clearFromRegistry)
            {
                ObjectsRegistry.Unregister(this);
                IdsRegistry.Unregister(this);
            }
        }
    }
}