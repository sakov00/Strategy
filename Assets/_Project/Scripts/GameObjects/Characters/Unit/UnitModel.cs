using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.GameObjects.Characters.Unit
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitModel : CharacterModel
    {
        public NavMeshAgent agent;
        private void OnValidate()
        {
            agent ??= GetComponent<NavMeshAgent>();
        }
    }
}