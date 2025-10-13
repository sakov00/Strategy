using _Project.Scripts.GameObjects.Abstract.Unit;

namespace _Project.Scripts.GameObjects.Concrete.WarriorFriend
{
    public class WarriorFriendView : UnitView
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