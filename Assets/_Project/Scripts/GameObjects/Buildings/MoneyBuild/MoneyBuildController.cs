using _Project.Scripts.GameObjects._General;
using UnityEngine;

namespace _Project.Scripts.GameObjects._Controllers
{
    [RequireComponent(typeof(HealthBarView))]
    public class MoneyBuildController : MonoBehaviour
    {
        [SerializeField] private HealthBarView healthBarView;

        private void OnValidate()
        {
            healthBarView ??= GetComponent<HealthBarView>();
        }

        private void Update()
        {
            healthBarView.UpdateView();
        }
    }
}