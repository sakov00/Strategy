using System;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Camera
{
    [Serializable]
    public class CameraFollow
    {
        private Transform cameraTransform;
        private Transform target;
        
        [SerializeField] private Vector3 offset = new (0f, 10f, -10f);
        [SerializeField] private float followSpeed = 5f;

        public bool IsFollowing { get; set; } = true;

        public void Init(Transform cameraTransformParam, Transform targetParam)
        {
            cameraTransform = cameraTransformParam;
            target = targetParam;
        }

        public void UpdateCameraPosition()
        {
            if (target == null) return;
            if (!IsFollowing) return;

            Vector3 desiredPosition = target.position + offset;
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, desiredPosition, followSpeed * Time.deltaTime);
            cameraTransform.LookAt(target);
        }
    }
}