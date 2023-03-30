using System;
using System.Collections;
using System.Collections.Generic;
using static System.Linq.Enumerable;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Transform _ballPos;
    [SerializeField] private Rigidbody _ball;
    [SerializeField] private float _ballSpeed;
    private Vector3 _ballDirection;
    private Vector3 _hitNormal;

    private void Start()
    {
        var randomX = new System.Random().Next(-10, 10);
        var randomY = new System.Random().Next(-10, 10);
        _ballDirection = new Vector3(randomX, randomY, 0).normalized;
        _ball.AddForce(_ballDirection * _ballSpeed, ForceMode.Impulse);
        // _ball.AddRelativeForce(_ballDirection * _ballSpeed); 
    }

    private void ChangeDireciton(ContactPoint hit) 
    {
        _ballDirection = Vector3.Reflect(_ballDirection, hit.normal);
    }

    private void OnCollisionEnter(Collision other) {
        var contactPoint = other.contacts[0];
        ChangeDireciton(contactPoint);
        _ball.AddForce(_ballDirection * _ballSpeed, ForceMode.Impulse);
    }
}
