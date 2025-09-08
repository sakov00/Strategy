using _Project.Scripts.Enums;
using _Project.Scripts.Pools;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects._Object.Characters.Character
{
    public abstract class MyCharacterController : ObjectController
    {
        [Inject] protected CharacterPool CharacterPool;
        
        public abstract CharacterModel CharacterModel { get; }
        public abstract CharacterView CharacterView { get; }
        public override ObjectModel ObjectModel => CharacterModel;
        public override ObjectView ObjectView => CharacterView;
        
        public override void Initialize()
        {
            CharacterView?.SetWalking(true);
            base.Initialize();
        }
        
        public override void Restore()
        {
            transform.SetParent(null);
            CharacterModel.CurrentHealth = CharacterModel.MaxHealth;
            CharacterModel.NoAimPos = transform.position;
            CharacterPool.Remove(this);
            gameObject.SetActive(true);
        }
    }
}