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
    public List<GameObject> Stones;
    private int _stoneNumber = -1;

    public void GameStart()
    {
        _gameSettings = Services.Get<GameSettings>();
        _directionService = Services.Get<DirectionService>();
        _cameraService = Services.Get<CameraService>();
        _stoneStartPos = FindObjectOfType<StoneStartPos>();
        _shootForce = _gameSettings.ShootForce;
    }

    public void GameUpdate(float delta)
    {
        if (!Input.GetMouseButtonDown(0)) return;
        ShootStone();
    }

    private void ShootStone()
    {
        _stoneNumber++;
        var direction = _directionService.Direction;
        Instantiate(_stone, _stoneStartPos.transform).GetComponent<Rigidbody>()
            .AddForce(direction.normalized * _shootForce, ForceMode.Impulse);
        _cameraService.SetCameraTarget(Stones[_stoneNumber]);
    }
}