using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomService : Service, IStart, IUpdate
{
    [SerializeField] private float _minShootForce;
    [SerializeField] private float _maxShootForce;
    [SerializeField] private float _add;
    private bool _switch;
    public float _shootForce;
    
    public void GameStart()
    {
        _shootForce = _minShootForce;
    }

    public void GameUpdate(float delta)
    {
        StartCoroutine(AddForce());
    }

    IEnumerator AddForce() {
        while (_shootForce < _maxShootForce){
            _shootForce += _add;
        }
        // while (_shootForce > _minShootForce){
        //     _shootForce -= _add;
        // }
        yield return null;
    }
}
