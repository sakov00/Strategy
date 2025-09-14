using System;
using UnityEngine;

namespace _General.Scripts.CameraLogic
{
    [Serializable]
    public class CameraFollow
    {
        private Transform _cameraTransform;
        private Transform _target;
        
        [SerializeField] private Vector3 _offset = new (0f, 20f, -12f);
        [SerializeField] private float _followSpeed = 5f;

        public bool IsFollowing { get; set; } = true;

        public void Init(Transform cameraTransformParam, Transform targetParam)
        {
            _cameraTransform = cameraTransformParam;
            _target = targetParam;
        }

        public void UpdateCameraPosition()
        {
            if (_target == null) return;
            if (!IsFollowing) return;

            Vector3 desiredPosition = _target.position + _offset;
            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, desiredPosition, _followSpeed * Time.deltaTime);
            _cameraTransform.LookAt(_target);
        }
    }
}