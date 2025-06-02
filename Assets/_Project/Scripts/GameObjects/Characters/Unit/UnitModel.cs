using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.GameObjects.Characters.Unit
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitModel : CharacterModel
    {
        public NavMeshAgent agent;
        public Vector3 NoAimPosition;
        
        private void OnValidate()
        {
            agent ??= GetComponent<NavMeshAgent>();
        }
    }
}