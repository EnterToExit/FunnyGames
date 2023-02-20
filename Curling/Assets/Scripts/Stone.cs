using System;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private float _passedTime;
    private CameraController _cameraController;
    private bool _dead;

    private void Awake()
    {
        _cameraController = FindObjectOfType<CameraController>();
        var shootingService = FindObjectOfType<ShootingService>();
        shootingService._stones.Add(gameObject);
    }

    private void Update()
    {
        var speed = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        if (speed == 0f && !_dead)
        {
            _passedTime += Time.deltaTime;
        }

        if (!(_passedTime > 1f)) return;
        _dead = true;
        _passedTime = 0f;
        _cameraController.ResetCameraTarget();
    }
}