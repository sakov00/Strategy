using _Project.Scripts.Enums;

namespace _Project.Scripts.Interfaces.Controller
{
    public interface IDamagableController : IPositionedController
    {
        public WarSide WarSide { get; }
        public float MaxHealth { get; }
        public float CurrentHealth { get; set; }
    }
}