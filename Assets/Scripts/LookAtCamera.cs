using System;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Vector3 _cameraPosition;
    private Camera _camera;

    private void Awake()
    {
        _camera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        _cameraPosition = _camera.transform.position;
        var lookDirection = (transform.position - _cameraPosition).normalized;
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }
}
