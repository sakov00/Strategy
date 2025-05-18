using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.MoneyBuild
{
    public class MoneyBuildModel : MonoBehaviour, IDamagable
    {
        [field: Header("Health")] 
        [field: SerializeField] public WarSide WarSide { get; set; }
        [field: SerializeField] public float MaxHealth { get; set; } = 500f;
        [field: SerializeField] public float CurrentHealth { get; set; } = 500f;
        public Transform Transform => transform;

        [Header("Money")]
        public int addMoneyValue = 1;

    }
}