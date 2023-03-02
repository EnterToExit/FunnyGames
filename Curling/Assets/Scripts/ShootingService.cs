using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootingService : Service, IStart, IUpdate
{
    [SerializeField] private GameObject _blueStone;
    [SerializeField] private GameObject _redStone;
    private float _shootForce;
    private StoneStartPos _stoneStartPos;
    private DirectionService _directionService;
    private CameraService _cameraService;
    private GameSettings _gameSettings;
    private Vector3 _direction;
    private RandomService _randomService;
    private bool _redTurn;
    private bool _readyToShoot;

    public static Action OnShoot;
    public List<StoneBlue> StonesBlue;
    public List<StoneRed> StonesRed;

    public void GameStart()
    {
        _gameSettings = Services.Get<GameSettings>();
        _directionService = Services.Get<DirectionService>();
        _cameraService = Services.Get<CameraService>();
        _randomService = Services.Get<RandomService>();
        _stoneStartPos = FindObjectOfType<StoneStartPos>();
        _shootForce = _gameSettings.ShootForce;
        StoneBlue.OnStopped += EnableShooting;
        StoneRed.OnStopped += EnableShooting;
        _readyToShoot = true;
    }

    public void GameUpdate(float delta)
    {
        // DebugShooting();
        if (!Input.GetMouseButtonDown(0)) return;
        TakeShoot();
    }

    private void TakeShoot()
    {
        if (!_readyToShoot) return;
        DisableShooting();
        OnShoot?.Invoke();
        if (_redTurn)
        {
            Shoot(_redStone);
            _redTurn = !_redTurn;
            _cameraService.SetCameraTarget(StonesRed.Last().gameObject);
        }
        else
        {
            Shoot(_blueStone);
            _redTurn = !_redTurn;
            _cameraService.SetCameraTarget(StonesBlue.Last().gameObject);
        }
    }

    private void EnableShooting()
    {
        _readyToShoot = true;
    }
    
    private void DisableShooting()
    {
        _readyToShoot = false;
    }

    private void Shoot(GameObject stone)
    {
        var direction = _directionService.Direction;
        
        Instantiate(stone, _stoneStartPos.transform).GetComponent<Rigidbody>()
            .AddForce(direction.normalized * (_shootForce * _randomService.ForceMultiplier), ForceMode.Impulse);
    }

    private void DebugShooting()
    {
        var direction = _directionService.Direction;
        if (Input.GetKeyDown("1"))
        {
            Instantiate(_blueStone, _stoneStartPos.transform).GetComponent<Rigidbody>()
                .AddForce(direction.normalized * (_shootForce * 0.5f), ForceMode.Impulse);
        }

        if (Input.GetKeyDown("2"))
        {
            Instantiate(_blueStone, _stoneStartPos.transform).GetComponent<Rigidbody>()
                .AddForce(direction.normalized * (_shootForce * 0.75f), ForceMode.Impulse);
        }

        if (Input.GetKeyDown("3"))
        {
            Instantiate(_blueStone, _stoneStartPos.transform).GetComponent<Rigidbody>()
                .AddForce(direction.normalized * (_shootForce * 0.99f), ForceMode.Impulse);
        }
    }
}