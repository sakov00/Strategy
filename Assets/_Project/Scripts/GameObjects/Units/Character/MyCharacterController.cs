using _Project.Scripts.GameObjects._General;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters
{
    public abstract class MyCharacterController : ObjectController
    {
        protected abstract CharacterModel CharacterModel { get; }
        protected abstract CharacterView CharacterView  { get; }
        public override ObjectModel ObjectModel => CharacterModel;
        public override ObjectView ObjectView => CharacterView;
        
        protected override void Initialize()
        {
            CharacterView?.SetWalking(true);
            base.Initialize();
        }
    }
}