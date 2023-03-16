using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Transform _ballPos;
    [SerializeField] private float _ballSpeed;
    private Vector3 _ballDirection;

    private void Start()
    {
        var randomX = new System.Random().Next(-10, 10);
        var randomY = new System.Random().Next(-10, 10);
        _ballDirection = new Vector3(randomX, randomY, 0).normalized;
    }

    private void Update()
    {
        UpdateBallPos();
    }

    private void UpdateBallPos()
    {
        var currentBallPos = _ballPos.position;
        var newBallPos = currentBallPos + _ballDirection * _ballSpeed;
        _ballPos.position = newBallPos;
    }
}
