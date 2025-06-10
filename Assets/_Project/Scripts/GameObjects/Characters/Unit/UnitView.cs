using System;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.GameObjects.Characters.Player
{
    [Serializable]
    public class UnitView : CharacterView
    {
        [SerializeField] public NavMeshAgent Agent;
    }
}