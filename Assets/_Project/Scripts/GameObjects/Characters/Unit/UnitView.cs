using System;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.GameObjects.Characters.Unit
{
    [Serializable]
    public class UnitView : CharacterView
    {
        [SerializeField] public NavMeshAgent Agent;
    }
}