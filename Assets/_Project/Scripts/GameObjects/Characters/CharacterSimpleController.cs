using _Project.Scripts.GameObjects._General;

namespace _Project.Scripts.GameObjects.Characters
{
    public abstract class CharacterSimpleController : ObjectController
    {
        public CharacterModel CharacterModel { get; protected set; }
        public CharacterView CharacterView { get; protected set; }

        protected override void Initialize()
        {
            Model = CharacterModel;
            View = CharacterView;
            CharacterView?.SetWalking(true);
            base.Initialize();
        }
    }
}