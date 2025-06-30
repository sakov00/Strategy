using System;
using _Project.Scripts.Enums;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.GameObjects.Characters.Unit
{
    [Serializable]
    public class UnitModel : CharacterModel
    {
        public Vector3 noAimPosition;
        public UnitType unitType;
    }
}