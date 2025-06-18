using System;
using _Project.Scripts.GameObjects.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    public class CameraController : MonoBehaviour
    {
        public CameraFollow cameraFollow;
        public Camera currentCamera;

        private void LateUpdate()
        {
            cameraFollow.UpdateCameraPosition();
        }
    }
}