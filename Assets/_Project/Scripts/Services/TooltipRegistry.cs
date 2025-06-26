using UniRx;
using UnityEngine;

namespace _Project.Scripts.Services
{
    public class TooltipRegistry
    {
        private readonly ReactiveCollection<GameObject> tooltips = new();

        public void Register(GameObject damageable)
        {
            tooltips.Add(damageable);
        }

        public void Unregister(GameObject damageable)
        {
            tooltips.Remove(damageable);
        }

        public IReactiveCollection<GameObject> GetAll()
        {
            return tooltips;
        }

        public void Clear()
        {
            tooltips.Clear();
        }
    }
}