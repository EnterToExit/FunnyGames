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
    public List<GameObject> _stones;
    private int _stoneNumber = -1;

    public void GameStart()
    {
        _stoneStartPos = FindObjectOfType<StoneStartPos>();
        _gameSettings = Services.Get<GameSettings>();
        _shootForce = _gameSettings.shootForce;
        _directionService = FindObjectOfType<DirectionService>();
        _cameraService = FindObjectOfType<CameraService>();
    }

    public void GameUpdate(float delta)
    {
        if (!Input.GetMouseButtonDown(0)) return;
        ShootStone();
    }

    private void ShootStone()
    {
        _stoneNumber++;
        var direction = _directionService.direction;
        Instantiate(_stone, _stoneStartPos.transform).GetComponent<Rigidbody>()
            .AddForce(direction.normalized * _shootForce, ForceMode.Impulse);
        _cameraService.SetCameraTarget(_stones[_stoneNumber]);
    }
}