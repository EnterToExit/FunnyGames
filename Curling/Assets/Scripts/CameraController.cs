using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _sensetivity;
    private Vector3 _mousePos;
    private Vector3 _mousePosNew;

    void Start()
    {
    }

    private void Update()
    {
        // _cameraPivot.rotation = Quaternion.Euler(_cameraPivot.rotation.x, _cameraPivot.rotation.y, _cameraPivot.rotation.z);

        _mousePos = Input.mousePosition;

        var mousePosDelta = _mousePosNew - _mousePos;
        _cameraPivot.eulerAngles += new Vector3(mousePosDelta.y, mousePosDelta.x * -1f) * _sensetivity;


        _mousePosNew = Input.mousePosition;
    }
}