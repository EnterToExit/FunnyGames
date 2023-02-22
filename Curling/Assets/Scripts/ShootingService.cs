using System;
using System.Collections.Generic;
using UnityEngine;

public class ShootingService : Service, IStart, IUpdate
{
    [SerializeField] private GameObject _stone;
    private float _shootForce;
    private StoneStartPos _stoneStartPos;
    private DirectionService _directionService;
    private CameraService _cameraService;
    private GameSettings _gameSettings;
    private Vector3 _direction;
    private int _stoneNumber = -1;
    private RandomService _randomService;
    public List<GameObject> Stones;

    public void GameStart()
    {
        _gameSettings = Services.Get<GameSettings>();
        _directionService = Services.Get<DirectionService>();
        _cameraService = Services.Get<CameraService>();
        _randomService = Services.Get<RandomService>();
        _stoneStartPos = FindObjectOfType<StoneStartPos>();
        _shootForce = _gameSettings.ShootForce;
    }

    public void GameUpdate(float delta)
    {
        if (Input.GetMouseButtonDown(0)) ShootStone();
    }

    private void ShootStone()
    {
        _stoneNumber++;
        var direction = _directionService.Direction;
        Instantiate(_stone, _stoneStartPos.transform).GetComponent<Rigidbody>()
            .AddForce(direction.normalized * (_shootForce * _randomService.ForceMultiplier), ForceMode.Impulse);
        _cameraService.SetCameraTarget(Stones[_stoneNumber]);
    }
}