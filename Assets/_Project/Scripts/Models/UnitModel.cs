using _Project.Scripts.Enums;
using UnityEngine;

namespace _Project.Scripts.Models
{
    public class UnitModel : CharacterModel
    {
        public float detectionRadius = 10f;
        public WarSide warSide;
    }
}