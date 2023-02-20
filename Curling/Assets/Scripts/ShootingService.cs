using UnityEngine;

public class ShootingService : Service, IStart, IUpdate
{
    [SerializeField] private GameObject _stone;
    private float _shootForce;
    private StoneStartPos _stoneStartPos;
    private DirectionController _directionController;
    private GameSettings _gameSettings;
    private Vector3 _direction;

    public void GameStart()
    {
        _stoneStartPos = FindObjectOfType<StoneStartPos>();
        _gameSettings = FindObjectOfType<GameSettings>();
        _shootForce = _gameSettings.shootForce;
        _shootForce = _gameSettings.shootForce;
        _directionController = FindObjectOfType<DirectionController>();
    }

    public void GameUpdate(float delta)
    {
        // var direction = Vector3.forward;
        // _stone.transform.eulerAngles = direction;
        if (!Input.GetMouseButtonDown(0)) return;
        ShootStone();
    }

    private void ShootStone()
    {
        var direction = _directionController.direction;
        Instantiate(_stone, _stoneStartPos.transform).GetComponent<Rigidbody>()
            .AddForce(direction.normalized * _shootForce, ForceMode.Impulse);
    }
}