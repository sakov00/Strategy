using _Project.Scripts.GameObjects._General;
using UnityEngine;

namespace _Project.Scripts.GameObjects.FriendsBuild
{
    [RequireComponent(typeof(HealthBarView))]
    public class FriendsBuildController : MonoBehaviour
    {
        [SerializeField] private HealthBarView healthBarView;

        private void OnValidate()
        {
            healthBarView ??= GetComponent<HealthBarView>();
        }

        private void FixedUpdate()
        {
            healthBarView.UpdateView();
        }
    }
}