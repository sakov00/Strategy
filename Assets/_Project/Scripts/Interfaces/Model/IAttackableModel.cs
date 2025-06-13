using _Project.Scripts.Enums;

namespace _Project.Scripts.Interfaces
{
    public interface IAttackableModel
    {
        public float AttackRange { get; set; }
        public int DamageAmount { get; set; }
        public float AllAnimAttackTime { get; set; }
        public float AnimAttackTime { get; set; }
        public float DetectionRadius { get; set; }
        public TypeAttack TypeAttack { get; set; }
        public IHealthModel AimCharacter { get; set; }
    }
}