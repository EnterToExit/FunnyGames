using UnityEngine;

public class DirectionService : Service, IStart, IUpdate
{
    [SerializeField] private Transform _forward;
    [SerializeField] private Transform _back;
    [SerializeField] private Transform _compas;
    private Vector3 _mousePos;
    private Vector3 _mousePosNew;
    private GameSettings _gameSettings;
    private float _sens;
    public Vector3 direction;
    public float _dick;

    public void GameStart()
    {
        _gameSettings = Services.Get<GameSettings>();
        _sens = _gameSettings.sensitivity;
    }

    public void GameUpdate(float delta)
    {
        _mousePos = Input.mousePosition;
        var mousePosDelta = _mousePosNew - _mousePos;
        var eulerAngles = _compas.transform.eulerAngles;
        eulerAngles += new Vector3(0f, mousePosDelta.x * -1 * _sens, 0f);
        _compas.transform.eulerAngles = eulerAngles;
        direction = _back.position - _forward.position;
        _dick = eulerAngles.y;
        _mousePosNew = Input.mousePosition;
    }
}