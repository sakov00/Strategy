using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.TowerDefence
{
    public class TowerDefenceModel : BuildModel, IFightObject
    {
        [field: Header("Attack")] 
        [field: SerializeField] public float AttackRange { get; set; } = 20f;
        [field: SerializeField] public int DamageAmount { get; set; } = 10;
        [field: SerializeField] public float DelayAttack { get; set; } = 1f;
        [field: SerializeField] public float DetectionRadius { get; set; } = 20f;
        [field: SerializeField] public TypeAttack TypeAttack { get; set; } = TypeAttack.Distance;
        [field: SerializeField] public IDamagable AimCharacter { get; set; }
    }
}