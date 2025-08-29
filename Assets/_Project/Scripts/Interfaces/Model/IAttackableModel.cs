using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;

namespace _Project.Scripts.Interfaces.Model
{
    public interface IAttackableModel
    {
        public float AttackRange { get; set; }
        public int DamageAmount { get; set; }
        public float AllAnimAttackTime { get; set; }
        public float AnimAttackTime { get; set; }
        public float DetectionRadius { get; set; }
        public TypeAttack TypeAttack { get; set; }
        public ObjectController AimObject { get; set; }
    }
}