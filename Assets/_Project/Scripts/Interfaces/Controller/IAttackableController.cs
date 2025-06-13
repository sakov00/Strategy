using _Project.Scripts.Enums;

namespace _Project.Scripts.Interfaces
{
    public interface IAttackableController
    {
        public float AttackRange { get; set; }
        public int DamageAmount { get; set; }
        public float DelayAttack { get; set; }
        public float DetectionRadius { get; set; }
        public TypeAttack TypeAttack { get; set; }
        public IDamagableController AimCharacter { get; set; }
    }
}