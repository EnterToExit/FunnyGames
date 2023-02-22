using UnityEngine;

public class CameraService : Service, IStart, IUpdate
{
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private Transform _camera;
    [SerializeField] private GameObject _stoneStartPos;
    private float _sensitivity;
    private GameSettings _gameSettings;
    private Vector3 _mousePos;
    private Vector3 _mousePosNew;
    private GameObject _cameraTarget;
    private MouseService _mouseService;

    public void GameStart()
    {
        _gameSettings = Services.Get<GameSettings>();
        _mouseService = Services.Get<MouseService>();
        _cameraPivot.eulerAngles = new Vector3(30f, 0f, 0f);
        _sensitivity = _gameSettings.Sensitivity;
        _cameraTarget = _stoneStartPos;
    }

    public void GameUpdate(float delta)
    {
        CameraRotation();
        CameraScroll();
        CameraPositionReset();
    }

    private void CameraRotation()
    {
        var eulerAngles = _cameraPivot.eulerAngles;
        eulerAngles += new Vector3(_mouseService.Mouse.y, _mouseService.Mouse.x * -1f) * _sensitivity;
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
        var position = _camera.position;
        var direction = (_cameraPivot.position - position).normalized;
        position += direction * _mouseService.MouseScroll.y;
        _camera.position = position;
    }

    private void CameraPositionReset()
    {
        _cameraPivot.position = _cameraTarget.gameObject.transform.position + Vector3.up;
    }

    public void SetCameraTarget(GameObject target)
    {
        _cameraTarget = target;
    }

    public void ResetCameraTarget()
    {
        _cameraTarget = _stoneStartPos;
    }
}