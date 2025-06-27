using _Project.Scripts.Interfaces;
using UniRx;

namespace _Project.Scripts.Registries
{
    public class HealthRegistry
    {
        private readonly ReactiveCollection<IHealthModel> damageables = new();

        public void Register(IHealthModel damageable)
        {
            damageables.Add(damageable);
        }

        public void Unregister(IHealthModel damageable)
        {
            damageables.Remove(damageable);
        }

        public IReactiveCollection<IHealthModel> GetAll()
        {
            return damageables;
        }

        public void Clear()
        {
            damageables.Clear();
        }
    }
}