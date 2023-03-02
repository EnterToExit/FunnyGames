using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class UIController : Service, IStart, IUpdate
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _blueScore;
    [SerializeField] private TextMeshProUGUI _redScore;
    private RandomService _randomService;
    private ScoreService _scoreService;

    public void GameStart()
    {
        _randomService = Services.Get<RandomService>();
        _scoreService = Services.Get<ScoreService>();
        ShootingService.OnShoot += DisableSlider;
        StoneBlue.OnStopped += EnableSlider;
        StoneRed.OnStopped += EnableSlider;
    }

    public void GameUpdate(float delta)
    {
        UpdateSlider();
        UpdateScore();
    }

    private void UpdateScore()
    {
        _blueScore.text = _scoreService.ScoreBlue.ToString("0.00");
        _redScore.text = _scoreService.ScoreRed.ToString("0.00");
    }

    private void UpdateSlider()
    {
        _slider.value = _randomService.ForceMultiplier;
    }

    private void EnableSlider()
    {
        _slider.gameObject.SetActive(true);
    }
    
    private void DisableSlider()
    {
        _slider.gameObject.SetActive(false);
    }
}
