using System;
using UnityEngine;

public class ArrowService : Service, IStart, IUpdate
{
    private Arrow _arrow;
    private DirectionService _directionService;
    private StoneStartPos _startPos;

    public void GameStart()
    {
        _directionService = Services.Get<DirectionService>();
        _startPos = FindObjectOfType<StoneStartPos>();
        _arrow = FindObjectOfType<Arrow>();
        ShootingService.OnShoot += DisableArrow;
        Stone.OnStopped += EnableArrow;
    }

    public void GameUpdate(float delta)
    {
        var y = _directionService.Dick;
        _arrow.transform.eulerAngles = new Vector3(0f, y);
        transform.position = _startPos.transform.position;
    }

    private void EnableArrow()
    {
        _arrow.gameObject.SetActive(true);
    }
    
    private void DisableArrow()
    {
        _arrow.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        ShootingService.OnShoot -= DisableArrow;
        Stone.OnStopped -= EnableArrow;
    }
}