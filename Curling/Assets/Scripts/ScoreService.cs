using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreService : Service, IUpdate, IStart
{
    private ShootingService _shootingService;

    public float ScoreRed;
    public float ScoreBlue;

    public void GameStart()
    {
        _shootingService = Services.Get<ShootingService>();
    }

    public void GameUpdate(float delta)
    {
        CollectScoreBlue();
        CollectScoreRed();
    }

    private void CollectScoreBlue()
    {
        ScoreBlue = _shootingService.StonesBlue.Sum(stone => stone.StoneScore);
    }

    private void CollectScoreRed()
    {
        ScoreRed = _shootingService.StonesRed.Sum(stone => stone.StoneScore);
    }
}