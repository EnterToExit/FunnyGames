using System;
using System.Collections.Generic;
using UnityEngine;

public class ShootingService : Service, IStart, IUpdate
{
    [SerializeField] private GameObject _stone;
    private StoneStartPos _stoneStartPos;
    private DirectionController _directionController;
    private CameraController _cameraController;
    private GameSettings _gameSettings;
    private Vector3 _direction;
    private float _shootForce;
    private int _stoneNumber = -1;
    private RandomService _randomService;
    public List<GameObject> _stones;

    public void GameStart()
    {
        _randomService = Services.Get<RandomService>();
        _stoneStartPos = FindObjectOfType<StoneStartPos>();
        _gameSettings = FindObjectOfType<GameSettings>();
        _directionController = FindObjectOfType<DirectionController>();
        _cameraController = FindObjectOfType<CameraController>();
        _shootForce = _gameSettings.shootForce;
    }

    public void GameUpdate(float delta)
    {
        if (!Input.GetMouseButtonDown(0)) return;
        ShootStone();
    }

    private void ShootStone()
    {
        _stoneNumber++;
        var direction = _directionController.direction;
        Instantiate(_stone, _stoneStartPos.transform).GetComponent<Rigidbody>()
            .AddForce(direction.normalized * _shootForce, ForceMode.Impulse);
        _cameraController.SetCameraTarget(_stones[_stoneNumber]);
    }
}