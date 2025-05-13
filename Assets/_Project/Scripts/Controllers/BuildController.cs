using _Project.Scripts.Views;
using UnityEngine;

namespace _Project.Scripts.Controllers
{
    [RequireComponent(typeof(HealthBarView))]
    public class BuildController : MonoBehaviour
    {
        private HealthBarView healthBarView;

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