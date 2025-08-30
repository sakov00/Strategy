using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.Pools;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Units.Character
{
    public abstract class MyCharacterController : ObjectController
    {
        [field:SerializeField] public CharacterType CharacterType { get; set; }
        
        [Inject] private CharacterPool _characterPool;
        
        public abstract CharacterModel CharacterModel { get; }
        public abstract CharacterView CharacterView  { get; }
        public override ObjectModel ObjectModel => CharacterModel;
        public override ObjectView ObjectView => CharacterView;
        
        protected override void Initialize()
        {
            CharacterView?.SetWalking(true);
            base.Initialize();
        }
        
        public override void ReturnToPool()
        {
            _characterPool.Return(this);
        }
    }
}