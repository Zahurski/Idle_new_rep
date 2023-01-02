using UnityEngine;

namespace IdleTycoon
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private float movementSpeed;
        [SerializeField] private Vector3 minValue, maxValue;

        private UIManager uiManager;

        private Vector3 newZoom;
        private Vector3 newPosition;
        private Vector3 dragStartPosition;
        private Vector3 dragCurrentPosition;
        private Vector3 clampPosition;
        private bool movable;

        public bool Movable => movable;

        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
        }

        private void Start()
        {
            newPosition = transform.position;
        }

        private void Update()
        {
            if (uiManager.CurrentScreen != uiManager.GameScreen) return;

            movable = transform.position != newPosition;
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

                if (plane.Raycast(ray, out entry))
                {
                    dragStartPosition = ray.GetPoint(entry);
                }
            }

            if (Input.GetMouseButton(0))
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                float entry;

                if (plane.Raycast(ray, out entry))
                {
                    dragCurrentPosition = ray.GetPoint(entry);

                    newPosition = transform.position + dragStartPosition - dragCurrentPosition;

                    clampPosition = new Vector3(
                        Mathf.Clamp(newPosition.x, minValue.x, maxValue.x),
                        Mathf.Clamp(newPosition.y, minValue.y, maxValue.y),
                        Mathf.Clamp(newPosition.z, minValue.z, maxValue.z));
                }
            }
        }

        private void HandleMovementInput()
        {
            transform.position = Vector3.Lerp(transform.position, clampPosition, movementSpeed * Time.deltaTime);
        }
    }
}