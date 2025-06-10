using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.TowerDefence
{
    [Serializable]
    public class TowerDefenceModel : BuildModel
    {
        [Header("Attack")] 
        [SerializeField] public float attackRange = 20f;
        [SerializeField] public int damageAmount = 10;
        [SerializeField] public float delayAttack = 1f;
        [SerializeField] public float detectionRadius = 20f;
        [SerializeField] public TypeAttack typeAttack = TypeAttack.Distance;
        [SerializeField] public IDamagable AimCharacter;
    }
}