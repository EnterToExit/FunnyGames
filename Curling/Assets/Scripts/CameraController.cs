using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _stone;
    [SerializeField] private float _sensetivity;
    private Vector3 _mousePos;
    private Vector3 _mousePosNew;

    private void Start()
    {
        _cameraPivot.eulerAngles = new Vector3(30f, 0f, 0f);
    }

    private void Update()
    {
        _mousePos = Input.mousePosition;

        CameraRotation();
        CameraScroll();
        CameraPositionReset();

        _mousePosNew = Input.mousePosition;
    }

    private void CameraRotation()
    {
        var mousePosDelta = _mousePosNew - _mousePos;
        // _cameraPivot.Rotate(new Vector3(mousePosDelta.y, mousePosDelta.x * -1f) * _sensetivity);
        _cameraPivot.eulerAngles += new Vector3(mousePosDelta.y, mousePosDelta.x * -1f) * _sensetivity;
        _cameraPivot.eulerAngles = new Vector3(_cameraPivot.eulerAngles.x, _cameraPivot.eulerAngles.y, 0f);
        if (_cameraPivot.eulerAngles.x > 70f)
        {
            _cameraPivot.eulerAngles = new Vector3(70f, _cameraPivot.eulerAngles.y, _cameraPivot.eulerAngles.z);
        }
        if (_cameraPivot.eulerAngles.x < 10f)
        {
            _cameraPivot.eulerAngles = new Vector3(10f, _cameraPivot.eulerAngles.y, _cameraPivot.eulerAngles.z);
        }
    }

    private void CameraScroll()
    {
        var mouseScrollDelta = Input.mouseScrollDelta;
        var direction = (_cameraPivot.position - _camera.position).normalized;
        _camera.position += direction * mouseScrollDelta.y;
    }

    private void CameraPositionReset()
    {
        transform.position = _stone.position + Vector3.up * 1f;
    }
}