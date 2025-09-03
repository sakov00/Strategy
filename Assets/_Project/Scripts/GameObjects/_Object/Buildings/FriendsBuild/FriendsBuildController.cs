using _General.Scripts.Interfaces;
using _General.Scripts.Json;
using _General.Scripts.Registries;
using _Project.Scripts.GameObjects._Object.Characters.Unit;
using _Project.Scripts.Pools;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects._Object.FriendsBuild
{
    public class FriendsBuildController : BuildController
    {
        [Inject] private CharacterPool _characterPool;
        
        [field:SerializeField] public FriendsBuildModel Model { get; set; }
        [field:SerializeField] public FriendsBuildView View { get; set; }
        public override BuildModel BuildModel => Model;
        public override BuildView BuildView => View;
        
        public override void Initialize()
        {
            base.Initialize();
            ObjectsRegistry.Register(this);
        }

        private void Start()
        {
            if(Model.BuildUnits.Count == 0)
                CreateFriends();
        }
        
        public void CreateFriends()
        {
            foreach (var friendUnit in View._buildUnitPositions)
            {
                var unitController = _characterPool.Get<UnitController>(Model.CharacterType, friendUnit.position);
                Model.BuildUnits.Add(unitController.Model);
            }
        }

        public override ISavableModel GetSavableModel()
        {
            Model.Position = transform.position;
            Model.Rotation = transform.rotation;
            return Model;
        }
        
        public override void SetSavableModel(ISavableModel savableModel)
        {
            if (savableModel is FriendsBuildModel friendsBuildModel)
            {
                Model = friendsBuildModel;
                Initialize();
            }
        }

        public override void ClearData()
        {
            ObjectsRegistry.Unregister(this);
        }
    }
}