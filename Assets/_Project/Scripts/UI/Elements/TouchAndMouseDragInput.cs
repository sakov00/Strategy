using _Project.Scripts._GlobalLogic;
using _Project.Scripts.GameObjects.BuildingZone;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.UI.Elements
{
    public class TouchAndMouseDragInput : MonoBehaviour
    {
        [SerializeField] private float dragSpeed = 0.5f;
        [SerializeField] private Vector3 lastMousePosition;
        [SerializeField] private bool isDragging = false;

        private void Update()
        {
#if EDIT_MODE
            HandleMouseDrag();
#else
            HandleTouchDrag();
#endif
        }

        private void HandleMouseDrag()
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                lastMousePosition = Input.mousePosition;
                isDragging = true;
            }

            if (isDragging)
            {
                Vector3 delta = Input.mousePosition - lastMousePosition;
                lastMousePosition = Input.mousePosition;

                Vector3 right = GlobalObjects.CameraController.transform.right;
                Vector3 forward = Vector3.Cross(right, Vector3.up);

                Vector3 move = (-right * delta.x - forward * delta.y) * dragSpeed * Time.deltaTime;
                GlobalObjects.CameraController.transform.position += move;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                
                var ray = GlobalObjects.CameraController.currentCamera.ScreenPointToRay(Input.mousePosition);
                if (!Physics.Raycast(ray, out RaycastHit hit)) return;
                
                if (hit.collider.TryGetComponent<IUpgradableController>(out var upgradeable))
                {
                    upgradeable.TryUpgrade();
                    return;
                }

                if (hit.collider.TryGetComponent<BuildingZoneController>(out var creatable))
                {
                    creatable.TryCreateBuild();
                }
            }
        }

        private void HandleTouchDrag()
        {
            if (Input.touchCount != 1)
            {
                isDragging = false;
                return;
            }

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastMousePosition = touch.position;
                isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector2 delta = touch.position - (Vector2)lastMousePosition;
                lastMousePosition = touch.position;

                Vector3 right = GlobalObjects.CameraController.transform.right;
                Vector3 forward = Vector3.Cross(right, Vector3.up);

                Vector3 move = (-right * delta.x - forward * delta.y) * dragSpeed * Time.deltaTime;
                GlobalObjects.CameraController.transform.position += move;
            }
        }
    }
}