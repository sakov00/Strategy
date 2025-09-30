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
    }
}