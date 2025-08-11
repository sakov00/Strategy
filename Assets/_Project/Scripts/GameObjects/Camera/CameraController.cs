using UnityEngine;

namespace _Project.Scripts.GameObjects.Camera
{
    public class CameraController : MonoBehaviour
    {
        public CameraFollow _cameraFollow;
        public UnityEngine.Camera _currentCamera;

        private void LateUpdate()
        {
            _cameraFollow.UpdateCameraPosition();
        }
    }
}