using _Project.Scripts.GameObjects.Abstract.Unit;

namespace _Project.Scripts.GameObjects.Concrete.ArcherFriend
{
    public class ArcherFriendView : UnitView
    {
        public override void SetWalking(bool isWalking)
        {
        }

        public override void SetAttacking(bool isAttacking)
        {
            OnAttackHit();
        }
    }
}