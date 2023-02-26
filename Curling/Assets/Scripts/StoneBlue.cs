using System;
using UnityEngine;

public class StoneBlue : MonoBehaviour
{
    private CameraService _cameraService;
    private Ground _ground;
    private float _passedTime;
    private bool _dead;
    private Rigidbody _rigidbody;
    private CountArea _countArea;
    public float StoneScore;

    private void Awake()
    {
        _ground = FindObjectOfType<Ground>();
        _countArea = FindObjectOfType<CountArea>();
        _cameraService = FindObjectOfType<CameraService>();
        var shootingService = FindObjectOfType<ShootingService>();
        shootingService.StonesBlue.Add(gameObject.GetComponent<StoneBlue>());
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        StoneScore = 10f - (transform.position - _countArea.transform.position).magnitude;
        
        var speed = _rigidbody.velocity.magnitude;
        if (speed == 0f && !_dead)
        {
            _passedTime += Time.deltaTime;
        }

        if (!(_passedTime > 1f)) return;
        _dead = true;
        _passedTime = 0f;
        _cameraService.ResetCameraTarget();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject != _ground.gameObject) return;
        _cameraService.ResetCameraTarget();
        Destroy(gameObject);
    }
}