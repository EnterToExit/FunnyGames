using System;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private CameraService _cameraService;
    private Ground _ground;
    private float _passedTime;
    private bool _dead;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _ground = FindObjectOfType<Ground>();
        _cameraService = FindObjectOfType<CameraService>();
        var shootingService = FindObjectOfType<ShootingService>();
        shootingService._stones.Add(gameObject);
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
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
        if (other.gameObject == _ground.gameObject)
        {
            _cameraService.ResetCameraTarget();
            Destroy(gameObject);
        }
    }
}