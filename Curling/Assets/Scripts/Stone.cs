using System;
using UnityEngine;

public enum Team
{
    Red,
    Blue
}

public static class Extensions
{
    public static Color TeamColor(this Team team)
    {
        return team switch
        {
            Team.Blue => Color.blue,
            Team.Red => Color.red,
            _ => Color.white
        };
    }

    public static Team ResolveNextTeam(this Team team)
    {
        return team switch
        {
            Team.Blue => Team.Red,
            Team.Red => Team.Blue,
            _ => team
        };
    }
}

public class Stone : MonoBehaviour
{
    public Team Team { get; private set; }
    public float StoneScore;
    public static Action OnStopped;
    [SerializeField] private Material _stoneMaterial; 
    private CameraService _cameraService;
    private ShootingService _shootingService;
    private Ground _ground;
    private float _passedTime;
    private bool _dead;
    private Rigidbody _rigidbody;
    private CountArea _countArea;
    private UIController _uiController;

    private void Awake()
    {
        _uiController = FindObjectOfType<UIController>();
        _ground = FindObjectOfType<Ground>();
        _countArea = FindObjectOfType<CountArea>();
        _cameraService = FindObjectOfType<CameraService>();
        _shootingService = FindObjectOfType<ShootingService>();
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        Team = _shootingService.CurrentTeam;

        _shootingService.Stones.Add(gameObject.GetComponent<Stone>());
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
        _uiController.RemoveStone(Team);
    }
}