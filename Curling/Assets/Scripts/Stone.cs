using System;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private CameraController _cameraController;
    private float _passedTime;
    private bool _dead;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _cameraController = FindObjectOfType<CameraController>();
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
        
        if (_passedTime > 10f && speed > 0f)
        {
            _dead = true;
            _passedTime = 0f;
            _cameraController.ResetCameraTarget();
        }

        if (!(_passedTime > 1f)) return;
        _dead = true;
        _passedTime = 0f;
        _cameraController.ResetCameraTarget();
    }
}