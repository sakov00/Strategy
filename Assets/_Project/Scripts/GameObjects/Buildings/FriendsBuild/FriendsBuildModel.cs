using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Characters.Unit;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.FriendsBuild
{
    public class FriendsBuildModel : MonoBehaviour, IDamagable, IUpgradable
    {
        [field: Header("Upgrades")]
        [field: SerializeField] public int CurrentLevel { get; set; }
        
        [field: Header("Health")] 
        [field: SerializeField] public WarSide WarSide { get; set; }
        [field: SerializeField] public float MaxHealth { get; set; } = 500f;
        [field: SerializeField] public float CurrentHealth { get; set; } = 500f;
        public Transform Transform => transform;
        
        [Header("Units")] 
        [SerializeField] public FriendUnitType unitType;
        [SerializeField] public int countUnits;
        [SerializeField] public Vector3 startPositionUnits;
        [SerializeField] public List<UnitModel> buildUnits = new();
    }
}