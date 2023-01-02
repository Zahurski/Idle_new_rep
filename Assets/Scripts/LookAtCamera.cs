using UnityEngine;

namespace IdleTycoon
{
    public class LookAtCamera : MonoBehaviour
    {
        private Vector3 cameraPosition;
        private Camera localCamera;

        private void Awake()
        {
            localCamera = FindObjectOfType<Camera>();
        }

        private void Update()
        {
            cameraPosition = localCamera.transform.position;
            var lookDirection = (transform.position - cameraPosition).normalized;
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
}