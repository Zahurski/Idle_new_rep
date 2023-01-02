using UnityEngine;

namespace IdleTycoon.UI.Utils
{
    public class RotationUIComponent : MonoBehaviour
    {
        [SerializeField] private Vector3 rotationSpeed;

        private void Update()
        {
            transform.rotation *= Quaternion.Euler(Time.deltaTime * rotationSpeed);
        }
    }
}