using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreService : Service, IUpdate, IStart
{
    private StoneBlue _stoneBlue;
    private StoneRed _stoneRed;
    private ShootingService _shootingService;

    public float ScoreRed;
    public float ScoreBlue;

    public void GameStart()
    {
        _shootingService = Services.Get<ShootingService>();
        _stoneBlue = FindObjectOfType<StoneBlue>();
    }

    public void GameUpdate(float delta)
    {
        CollectScoreBlue();
        CollectScoreRed();
    }

    private void CollectScoreBlue()
    {
        ScoreBlue = 0f;
        foreach (var stone in _shootingService.StonesBlue)
        {
            ScoreBlue += stone.StoneScore;
        }
    }

    private void CollectScoreRed()
    {
        ScoreRed = _shootingService.StonesRed.Sum(stone => stone.StoneScore);
    }
}