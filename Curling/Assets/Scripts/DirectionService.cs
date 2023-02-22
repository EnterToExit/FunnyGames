using UnityEngine;

public class DirectionService : Service, IStart, IUpdate
{
    [SerializeField] private Transform _forward;
    [SerializeField] private Transform _back;
    [SerializeField] private Transform _compass;
    private Vector3 _mousePos;
    private Vector3 _mousePosNew;
    private GameSettings _gameSettings;
    private float _sensitivity;
    public Vector3 Direction;
    public float Dick;
    private MouseService _mouseService;

    public void GameStart()
    {
        _gameSettings = Services.Get<GameSettings>();
        _mouseService = Services.Get<MouseService>();
        _sensitivity = _gameSettings.Sensitivity;
    }

    public void GameUpdate(float delta)
    {
        SetDirection();
    }

    private void SetDirection()
    {
        var transform1 = _compass.transform;
        var eulerAngles = transform1.eulerAngles;
        eulerAngles += new Vector3(0f, _mouseService.Mouse.x * -1 * _sensitivity, 0f);
        transform1.eulerAngles = eulerAngles;
        Direction = _back.position - _forward.position;
        Dick = eulerAngles.y;
    }
}