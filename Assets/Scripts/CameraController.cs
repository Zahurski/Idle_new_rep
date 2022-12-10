using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector3 minValue, maxValue;

    private UIManager _uiManager;
    
    private Vector3 _newZoom;
    private Vector3 _newPosition;
    private Vector3 _dragStartPosition;
    private Vector3 _dragCurrentPosition;
    private Vector3 _clampPosition;
    private bool _moveble;
    public bool Moveble => _moveble;

    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        _newPosition = transform.position;
    }

    private void Update()
    {
        if(_uiManager.CurrentScreen != _uiManager.GameScreen) return;
        
        _moveble = transform.position != _newPosition;
        HandleMouseInput();
        HandleMovementInput();
    }
    
    private void HandleMouseInput()
    {
        //TODO: добавить скролы пальцами
        if (Input.mouseScrollDelta.y >= 0 && camera.orthographicSize >= 8)
        {
            camera.orthographicSize -= 1f;
        }
        
        if (Input.mouseScrollDelta.y <= 0 && camera.orthographicSize <= 11)
        {
            camera.orthographicSize += 1f;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;
            
            if(plane.Raycast(ray, out entry))
            {
                _dragStartPosition = ray.GetPoint(entry);
            }
        }

        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;
            
            if(plane.Raycast(ray, out entry))
            {
                _dragCurrentPosition = ray.GetPoint(entry);

                _newPosition = transform.position + _dragStartPosition - _dragCurrentPosition;
                
                _clampPosition = new Vector3(
                    Mathf.Clamp(_newPosition.x, minValue.x, maxValue.x),
                    Mathf.Clamp(_newPosition.y, minValue.y, maxValue.y),
                    Mathf.Clamp(_newPosition.z, minValue.z, maxValue.z));
            }
        }
    }
    
    private void HandleMovementInput()
    {
        transform.position = Vector3.Lerp(transform.position, _clampPosition, movementSpeed * Time.deltaTime);
    }
}