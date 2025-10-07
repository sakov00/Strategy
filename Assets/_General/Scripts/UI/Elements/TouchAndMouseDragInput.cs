using _General.Scripts._GlobalLogic;
using _Project.Scripts.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _General.Scripts.UI.Elements
{
    public class TouchAndMouseDragInput : MonoBehaviour
    {
        [Header("Camera Drag Settings")]
        [SerializeField] private float _dragSpeed = 2f;
        [SerializeField] private float _minDragThreshold = 4f;

        [Header("Raycast Layers")]
        [SerializeField] private LayerMask _groundMask;

        private Vector3 _lastPointerPosition;
        private bool _isDragging = false;
        private bool _clickedOnObject = false;
        private bool _justEnabled;

        private ISelectableUnit _selectedUnit;
        private Camera _camera;

        private void Awake()
        {
            _camera = GlobalObjects.CameraController.CurrentCamera;
        }

        private void OnEnable()
        {
            _justEnabled = true;
        }

        private void Update()
        {
            if (_justEnabled)
            {
                _justEnabled = false;
                _isDragging = false;
                return;
            }

#if ANDROID_MODE
            HandleTouchInput();
#else
            HandleMouseInput();
#endif
        }
        
        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _lastPointerPosition = Input.mousePosition;
                _isDragging = true;

                // Проверяем, попали ли по объекту
                _clickedOnObject = TrySelectUnit(Input.mousePosition);
            }

            if (Input.GetMouseButton(0) && _isDragging && !_clickedOnObject)
            {
                Vector3 delta = Input.mousePosition - _lastPointerPosition;
                _lastPointerPosition = Input.mousePosition;

                if (delta.sqrMagnitude > _minDragThreshold)
                {
                    DragCamera(delta);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
                if (_selectedUnit != null)
                {
                    if (TryGetGroundPoint(Input.mousePosition, out var target))
                    {
                        _selectedUnit.MoveTo(target);
                    }
                    _selectedUnit.Deselect();
                    _selectedUnit = null;
                }
                else
                {
                    TryHandleBuy(Input.mousePosition);
                }

                _clickedOnObject = false;
            }
        }

        private void HandleTouchInput()
        {
            if (Input.touchCount != 1)
            {
                _isDragging = false;
                _clickedOnObject = false;
                return;
            }

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _lastPointerPosition = touch.position;
                _isDragging = true;

                _clickedOnObject = TrySelectUnit(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved && _isDragging && !_clickedOnObject)
            {
                Vector2 delta = touch.position - (Vector2)_lastPointerPosition;
                _lastPointerPosition = touch.position;

                if (delta.sqrMagnitude > _minDragThreshold)
                {
                    DragCamera(delta);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                _isDragging = false;

                if (_selectedUnit != null)
                {
                    if (TryGetGroundPoint(touch.position, out var target))
                    {
                        _selectedUnit.MoveTo(target);
                        _selectedUnit.Deselect();
                        _selectedUnit = null;
                    }
                }
                else
                {
                    TryHandleBuy(touch.position);
                }

                _clickedOnObject = false;
            }
        }

        private void DragCamera(Vector2 delta)
        {
            Vector3 right = _camera.transform.right;
            Vector3 forward = Vector3.Cross(right, Vector3.up);

            Vector3 move = (-right * delta.x - forward * delta.y) * _dragSpeed * Time.deltaTime;
            GlobalObjects.CameraController.transform.position += move;
        }

        private bool TrySelectUnit(Vector2 screenPos)
        {
            var ray = _camera.ScreenPointToRay(screenPos);
            if (!Physics.Raycast(ray, out RaycastHit hit, 1000f)) return false;

            if (hit.collider.TryGetComponent<ISelectableUnit>(out var unit))
            {
                if (_selectedUnit != null)
                    _selectedUnit.Deselect();

                _selectedUnit = unit;
                _selectedUnit.Select();
                return true;
            }

            return false;
        }

        private bool TryGetGroundPoint(Vector2 screenPos, out Vector3 point)
        {
            var ray = _camera.ScreenPointToRay(screenPos);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
            {
                if (((1 << hit.collider.gameObject.layer) & _groundMask) != 0)
                {
                    point = hit.point;
                    return true;
                }
                else
                {
                    point = default;
                    return false;
                }
            }

            point = default;
            return false;
        }

        private void TryHandleBuy(Vector2 screenPos)
        {
            var ray = _camera.ScreenPointToRay(screenPos);
            if (!Physics.Raycast(ray, out RaycastHit hit, 1000f)) return;

            if (hit.collider.TryGetComponent<IBuy>(out var buyable))
            {
                buyable.TryBuy().Forget();
            }
        }
    }
}
