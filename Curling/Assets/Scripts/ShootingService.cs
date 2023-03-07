using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootingService : Service, IStart, IUpdate
{
    [SerializeField] private GameObject _stone;
    public static Action OnShoot;
    public Team CurrentTeam { get; private set; }
    public List<Stone> Stones;
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

    public Tuple<float, float> CountTeamScores()
    {
        return new Tuple<float, float>(
            Stones.Where(stone => stone.Team == Team.Red).Sum(stone => stone.StoneScore),
            Stones.Where(stone => stone.Team == Team.Blue).Sum(stone => stone.StoneScore)
        );
    }

    private void TakeShoot()
    {
        if (_sessionEnded) return;
        if (!_readyToShoot) return;
        DisableShooting();
        OnShoot?.Invoke();

        Shoot();
        CurrentTeam = CurrentTeam.ResolveNextTeam();
        _cameraService.SetCameraTarget(Stones.Last().gameObject);
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

    private void Shoot()
    {
        var direction = _directionService.Direction;

        var instance = Instantiate(_stone, _stoneStartPos.transform);
        
        instance.GetComponentInChildren<Renderer>(true).material.color = CurrentTeam.TeamColor();

        instance.GetComponent<Rigidbody>()
            .AddForce(direction.normalized * (_shootForce * _randomService.ForceMultiplier), ForceMode.Impulse);
    }
}