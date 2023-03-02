using System;
using System.Collections;
using UnityEngine;

public class StoneRed : MonoBehaviour
{
    private CameraService _cameraService;
    private Ground _ground;
    private float _passedTime;
    private bool _dead;
    private Rigidbody _rigidbody;
    private CountArea _countArea;
    public float StoneScore;
    public static Action OnStopped;

    private void Awake()
    {
        _ground = FindObjectOfType<Ground>();
        _countArea = FindObjectOfType<CountArea>();
        _cameraService = FindObjectOfType<CameraService>();

        var shootingService = FindObjectOfType<ShootingService>();
        shootingService.StonesRed.Add(gameObject.GetComponent<StoneRed>());

        _rigidbody = gameObject.GetComponent<Rigidbody>();
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
        _cameraService.ResetCameraTarget();
    }
}