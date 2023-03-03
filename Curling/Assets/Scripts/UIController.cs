using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Slider = UnityEngine.UI.Slider;

public class UIController : Service, IStart, IUpdate
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _blueScore;
    [SerializeField] private TextMeshProUGUI _redScore;
    [SerializeField] private TextMeshProUGUI _uiRoundNumber;
    [SerializeField] private GameObject _redTeamWin;
    [SerializeField] private GameObject _blueTeamWin;
    private RandomService _randomService;
    private ScoreService _scoreService;
    private int _turnCount;
    private int _roundNumber = 1;
    public static Action OnEndSession;
    
    public void GameStart()
    {
        _randomService = Services.Get<RandomService>();
        _scoreService = Services.Get<ScoreService>();
        ShootingService.OnShoot += DisableSlider;
        Stone.OnStopped += EnableSlider;
    }

    public void GameUpdate(float delta)
    {
        UpdateSlider();
        UpdateScore();
        UpdateRound();
        if (_turnCount == 2)
        {
            _roundNumber++;
            _turnCount = 0;
        }
        if (_roundNumber == 6) EndSession();
    }

    public void AddTurn()
    {
        _turnCount++;
    }

    private void EndSession()
    {
        OnEndSession?.Invoke();
        DisableSlider();
        _blueScore.gameObject.SetActive(false);
        _redScore.gameObject.SetActive(false);
        _uiRoundNumber.gameObject.SetActive(false);
        if (_scoreService.ScoreRed > _scoreService.ScoreBlue)
        {
            _redTeamWin.gameObject.SetActive(true);
        }
        else
        {
            _blueTeamWin.gameObject.SetActive(true);
        }
    }

    public void RestartSession()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void UpdateScore()
    {
        _blueScore.text = _scoreService.ScoreBlue.ToString("0.00");
        _redScore.text = _scoreService.ScoreRed.ToString("0.00");
    }

    private void UpdateRound()
    {
        _uiRoundNumber.text = _roundNumber.ToString("0");
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
