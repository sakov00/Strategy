using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;

namespace _Project.Scripts.GameObjects.Units.Character
{
    public abstract class MyCharacterController : ObjectController
    {
        public CharacterType CharacterType { get; set; }
        public abstract CharacterModel CharacterModel { get; }
        public abstract CharacterView CharacterView  { get; }
        public override ObjectModel ObjectModel => CharacterModel;
        public override ObjectView ObjectView => CharacterView;
        
        protected override void Initialize()
        {
            CharacterView?.SetWalking(true);
            base.Initialize();
        }
    }
}