using _Project.Scripts.Enums;

namespace _Project.Scripts.Interfaces.Model
{
    public interface IHealthModel
    {
        public WarSide WarSide { get; }
        public float DelayRegeneration { get; }
        public float RegenerateHealthInSecond { get; }
        public int SecondsWithoutDamage { get; set; }
        public float MaxHealth { get; }
        public float CurrentHealth { get; set; }
    }
}