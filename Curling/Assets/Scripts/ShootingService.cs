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
    private UIController _uiController;
    private bool _redTurn;
    private bool _readyToShoot;
    private bool _sessionEnded;
    public static Action OnShoot;
    public List<Stone> StonesRed;
    public List<Stone> StonesBlue;

    public void GameStart()
    {
        _gameSettings = Services.Get<GameSettings>();
        _directionService = Services.Get<DirectionService>();
        _cameraService = Services.Get<CameraService>();
        _randomService = Services.Get<RandomService>();
        _uiController = Services.Get<UIController>();
        _stoneStartPos = FindObjectOfType<StoneStartPos>();
        _shootForce = _gameSettings.ShootForce;
        Stone.OnStopped += EnableShooting;
        Stone.OnStopped += EndTurn;
        UIController.OnEndSession += EndSession;
        _readyToShoot = true;
    }

    public void GameUpdate(float delta)
    {
        if (!Input.GetMouseButtonDown(0)) return;
        TakeShoot();
    }

    private void TakeShoot()
    {
        if (_sessionEnded) return;
        if (!_readyToShoot) return;
        DisableShooting();
        OnShoot?.Invoke();
        if (_redTurn)
        {
            Shoot(_redStone);
            _uiController.RemoveRedStone();
            _redTurn = !_redTurn;
            _cameraService.SetCameraTarget(StonesRed.Last().gameObject);
        }
        else
        {
            Shoot(_blueStone);
            _uiController.RemoveBlueStone();
            _redTurn = !_redTurn;
            _cameraService.SetCameraTarget(StonesBlue.Last().gameObject);
        }
    }

    private void EndTurn()
    {
        _uiController.AddTurn();
    }

    private void EndSession()
    {
        _sessionEnded = true;
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