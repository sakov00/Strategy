using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.MoneyBuild
{
    public class TowerDefenceModel : MonoBehaviour, IFightObject
    {
        [field: Header("Health")]
        [field: SerializeField] public WarSide WarSide { get; set; } = WarSide.Friend;
        [field: SerializeField] public float MaxHealth { get; set; } = 500f;
        [field: SerializeField] public float CurrentHealth { get; set; } = 500f;
        public Transform Transform => transform;
        
        [field: Header("Attack")] 
        [field: SerializeField] public float AttackRange { get; set; } = 20f;
        [field: SerializeField] public int DamageAmount { get; set; } = 10;
        [field: SerializeField] public float DelayAttack { get; set; } = 1f;
        [field: SerializeField] public float DetectionRadius { get; set; } = 20f;
        [field: SerializeField] public TypeAttack TypeAttack { get; set; } = TypeAttack.Distance;
        [field: SerializeField] public IDamagable AimCharacter { get; set; }
    }
}