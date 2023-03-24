using System;
using System.Collections;
using System.Collections.Generic;
using static System.Linq.Enumerable;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Transform _ballPos;
    [SerializeField] private float _ballSpeed;
    private Vector3 _ballDirection;
    private Vector3 _hitNormal;

    private void Start()
    {
        var randomX = new System.Random().Next(-10, 10);
        var randomY = new System.Random().Next(-10, 10);
        _ballDirection = new Vector3(randomX, randomY, 0).normalized;
        // _ballDirection = new Vector3(5f, 0f, 0f).normalized;
    }

    private void FixedUpdate()
    {
        MoveBall();
        if (IsTouching()) ChangeDireciton();
    }

    private void MoveBall()
    {
        var currentBallPos = _ballPos.position;
        var newBallPos = currentBallPos + _ballDirection * _ballSpeed;
        _ballPos.position = newBallPos;
    }

    private bool IsTouching()
    {
        RaycastHit hitInfo;
        Collider[] hitColliders = Physics.OverlapSphere(_ballPos.position, 1f);
        
        if (hitColliders.Count() > 1) 
        {
            Physics.SphereCast(_ballPos.position, 1f, Vector3.right, out hitInfo);
            _hitNormal = hitInfo.normal;
            return true;
        }
        else return false;
    }

    private void ChangeDireciton() 
    {
        _ballDirection = Vector3.Reflect(_ballDirection, _hitNormal);
    }
}
