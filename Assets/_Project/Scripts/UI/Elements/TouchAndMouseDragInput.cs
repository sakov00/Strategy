using _Project.Scripts._GlobalLogic;
using _Project.Scripts.GameObjects.BuildingZone;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Interfaces.Controller;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.UI.Elements
{
    public class TouchAndMouseDragInput : MonoBehaviour
    {
        [SerializeField] private float _dragSpeed = 2f;
        [SerializeField] private Vector3 _lastMousePosition;
        [SerializeField] private bool _isDragging = false;

        private void Update()
        {
#if ANDROID_MODE
            HandleTouchDrag();
#else
            HandleMouseDrag();
#endif
        }

        private void HandleMouseDrag()
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                _lastMousePosition = Input.mousePosition;
                _isDragging = true;
            }

            if (_isDragging)
            {
                Vector3 delta = Input.mousePosition - _lastMousePosition;
                _lastMousePosition = Input.mousePosition;

                Vector3 right = GlobalObjects.CameraController.transform.right;
                Vector3 forward = Vector3.Cross(right, Vector3.up);

                Vector3 move = (-right * delta.x - forward * delta.y) * _dragSpeed * Time.deltaTime;
                GlobalObjects.CameraController.transform.position += move;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
                
                var ray = GlobalObjects.CameraController._currentCamera.ScreenPointToRay(Input.mousePosition);
                if (!Physics.Raycast(ray, out RaycastHit hit)) return;
                
                if (hit.collider.TryGetComponent<IBuyController>(out var upgradeable))
                {
                    upgradeable.TryBuy().Forget();
                }
            }
        }

        private void HandleTouchDrag()
        {
            if (Input.touchCount != 1)
            {
                _isDragging = false;
                return;
            }

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _lastMousePosition = touch.position;
                _isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && _isDragging)
            {
                Vector2 delta = touch.position - (Vector2)_lastMousePosition;
                _lastMousePosition = touch.position;

                Vector3 right = GlobalObjects.CameraController.transform.right;
                Vector3 forward = Vector3.Cross(right, Vector3.up);

                Vector3 move = (-right * delta.x - forward * delta.y) * _dragSpeed * Time.deltaTime;
                GlobalObjects.CameraController.transform.position += move;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                _isDragging = false;

                var ray = GlobalObjects.CameraController._currentCamera.ScreenPointToRay(touch.position);
                if (!Physics.Raycast(ray, out RaycastHit hit)) return;

                if (hit.collider.TryGetComponent<IBuyController>(out var upgradeable))
                {
                    upgradeable.TryBuy().Forget();
                }
            }
        }
    }
}