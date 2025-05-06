using System;
using _Project.Scripts.Models;
using UnityEngine;
using VContainer.Unity;

namespace _Project.Scripts.Systems
{
    [RequireComponent(typeof(UnitModel))]
    public class UnitInputSystem : MonoBehaviour
    {
        [SerializeField] UnitModel unitModel;
        
        private Vector3 inputVector;

        private void OnValidate()
        {
            unitModel ??= GetComponent<UnitModel>();
        }

        public void Update()
        {
            inputVector = Vector3.zero;
            var hitBuffer = Physics.OverlapSphere(transform.position, unitModel.detectionRadius);
            foreach (var hit in hitBuffer)
            {
                if (hit == null || !hit.CompareTag("Player")) continue;
                Vector3 direction = (hit.transform.position - transform.position).normalized;
                inputVector = new Vector3(direction.x, 0, direction.z);
                break;
            }
        }

        public Vector3 GetInputVector()
        {
            return inputVector;
        }
    }
}
