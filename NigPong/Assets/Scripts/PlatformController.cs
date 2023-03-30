using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private Transform _platform;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _ball;
    [SerializeField] private float _speed;

    void Update()
    {
        if (Input.GetKey("left")){
            _platform.position += (Vector3.left * _speed) * Time.deltaTime;
        }
        if (Input.GetKey("right")){
            _platform.position += (Vector3.right * _speed) * Time.deltaTime;
        }
        if (Input.GetKeyDown("up")){
            InstantiateBall();
        }
    }

    private void InstantiateBall() {
        var ball = Instantiate(_ball, _spawnPoint) as GameObject;
    }
}
