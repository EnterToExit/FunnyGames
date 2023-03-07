using System;

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
        CollectScore();
    }

    private void CollectScore()
    {
        (ScoreRed, ScoreBlue) = _shootingService.CountTeamScores();
    }
}