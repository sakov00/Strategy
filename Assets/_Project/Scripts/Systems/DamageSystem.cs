using System;
using _Project.Scripts.Models;
using UnityEngine;
using VContainer.Unity;

namespace _Project.Scripts.Systems
{
    [RequireComponent(typeof(CharacterModel))]
    public class DamageSystem : MonoBehaviour
    {
        private CharacterModel unitModel;
        private float lastAttackTime = -Mathf.Infinity;

        private void OnValidate()
        {
            unitModel ??= GetComponent<CharacterModel>();
        }

        public void Attack()
        {
            if (unitModel.aimCharacter == null)
                return;
            
            if (Time.time - lastAttackTime < unitModel.delayAttack)
                return;

            var distance = Vector3.Distance(transform.position, unitModel.aimCharacter.transform.position);
            if (distance <= unitModel.attackRange)
            {
                lastAttackTime = Time.time;
                unitModel.aimCharacter.currentHealth -= unitModel.damageAmount;
                if (unitModel.aimCharacter.currentHealth <= 0)
                    Destroy(unitModel.aimCharacter.gameObject);
            }
        }
    }
}
