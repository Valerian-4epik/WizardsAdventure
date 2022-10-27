using System;
using UnityEngine;

namespace Infrastructure.Logic
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Start() => 
            _mainCamera = Camera.main;

        private void Update()
        {
            Quaternion rotattion = _mainCamera.transform.rotation;
            transform.LookAt(transform.position + rotattion * Vector3.back, rotattion * Vector3.up);
        }
    }
}