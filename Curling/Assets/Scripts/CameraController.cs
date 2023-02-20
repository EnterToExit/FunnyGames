using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _stone;
    private float _sensitivity;
    private GameSettings _gameSettings;
    private Vector3 _mousePos;
    private Vector3 _mousePosNew;

    private void Start()
    {
        _cameraPivot.eulerAngles = new Vector3(30f, 0f, 0f);
        _gameSettings = FindObjectOfType<GameSettings>();
        _sensitivity = _gameSettings.sensitivity;
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
        var eulerAngles = _cameraPivot.eulerAngles;
        eulerAngles += new Vector3(mousePosDelta.y, mousePosDelta.x * -1f) * _sensitivity;
        eulerAngles = new Vector3(eulerAngles.x, eulerAngles.y, 0f);
        _cameraPivot.eulerAngles = eulerAngles;
        if (_cameraPivot.eulerAngles.x > 70f)
        {
            var angles = _cameraPivot.eulerAngles;
            angles = new Vector3(70f, angles.y, angles.z);
            _cameraPivot.eulerAngles = angles;
        }

        if (!(_cameraPivot.eulerAngles.x < 10f)) return;
        {
            var angles = _cameraPivot.eulerAngles;
            angles = new Vector3(10f, angles.y, angles.z);
            _cameraPivot.eulerAngles = angles;
        }
    }

    private void CameraScroll()
    {
        var mouseScrollDelta = Input.mouseScrollDelta;
        var position = _camera.position;
        var direction = (_cameraPivot.position - position).normalized;
        position += direction * mouseScrollDelta.y;
        _camera.position = position;
    }

    private void CameraPositionReset()
    {
        transform.position = _stone.position + Vector3.up * 1f;
    }
}