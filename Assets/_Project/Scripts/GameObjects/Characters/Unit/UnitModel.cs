using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.GameObjects.Characters.Unit
{
    [Serializable]
    public class UnitModel : CharacterModel
    {
        public int currentWaypointIndex;
        public List<Vector3> wayToAim;
        public UnitType unitType;
    }
}