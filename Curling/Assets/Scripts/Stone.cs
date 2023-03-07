using System;
using UnityEngine;

public enum Team
{
    Red,
    Blue
}

public class Stone : MonoBehaviour
{
    [SerializeField] private Team _team;
    private CameraService _cameraService;
    private ShootingService _shootingService;
    private Ground _ground;
    private float _passedTime;
    private bool _dead;
    private Rigidbody _rigidbody;
    private CountArea _countArea;
    private UIController _uiController;
    public float StoneScore;
    public static Action OnStopped;
    public Cum Cum;

    private void Awake()
    {
        _uiController = FindObjectOfType<UIController>();
        _ground = FindObjectOfType<Ground>();
        _countArea = FindObjectOfType<CountArea>();
        _cameraService = FindObjectOfType<CameraService>();
        _shootingService = FindObjectOfType<ShootingService>();
        _rigidbody = gameObject.GetComponent<Rigidbody>();

        switch (_team)
        {
            case Team.Blue:
                _shootingService.StonesBlue.Add(gameObject.GetComponent<Stone>());
                break;
            case Team.Red:
                _shootingService.StonesRed.Add(gameObject.GetComponent<Stone>());
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Update()
    {
        var distance = 5f - (transform.position - _countArea.transform.position).magnitude;
        StoneScore = Mathf.Clamp(distance, 0f, 5f) * 2f;

        var speed = _rigidbody.velocity.magnitude;
        if (speed == 0f && !_dead)
        {
            _passedTime += Time.deltaTime;
        }

        if (!(_passedTime > 1f)) return;
        _dead = true;
        _passedTime = 0f;
        OnStopped?.Invoke();
        RemoveStoneFromUI();
        _cameraService.ResetCameraTarget();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject != _ground.gameObject) return;
        Destroy(gameObject, 1f);
    }

    private void OnDestroy()
    {
        OnStopped?.Invoke();
        RemoveStoneFromUI();
        _cameraService.ResetCameraTarget();
    }

    private void RemoveStoneFromUI()
    {
        switch (_team)
        {
            case Team.Blue:
                _uiController.RemoveBlueStone();
                break;
            case Team.Red:
                _uiController.RemoveRedStone();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}