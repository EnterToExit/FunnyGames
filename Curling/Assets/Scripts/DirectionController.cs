using UnityEngine;

public class DirectionController : MonoBehaviour
{
    [SerializeField] private Transform _forward;
    [SerializeField] private Transform _back;
    [SerializeField] private Transform _compas;
    private Vector3 _mousePos;
    private Vector3 _mousePosNew;
    private GameSettings _gameSettings;
    private float _sens;
    public Vector3 direction;

    private void Start()
    {
        _gameSettings = FindObjectOfType<GameSettings>();
        _sens = _gameSettings.sensitivity;
    }

    private void Update()
    {
        _mousePos = Input.mousePosition;
        var mousePosDelta = _mousePosNew - _mousePos;
        _compas.transform.eulerAngles += new Vector3(0f, mousePosDelta.x * -1 * _sens, 0f);
        direction = _back.position - _forward.position;
        _mousePosNew = Input.mousePosition;
    }
}