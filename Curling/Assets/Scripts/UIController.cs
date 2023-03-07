using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : Service, IStart, IUpdate
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _blueScore;
    [SerializeField] private TextMeshProUGUI _redScore;
    [SerializeField] private TextMeshProUGUI _uiRoundNumber;
    [SerializeField] private GameObject _redTeamWin;
    [SerializeField] private GameObject _blueTeamWin;
    [SerializeField] private GameObject _stonesBlue;
    [SerializeField] private GameObject _stonesRed;
    private Image[] _stonesBlueImages;
    private Image[] _stonesRedImages;
    private RandomService _randomService;
    private ScoreService _scoreService;

    private readonly Dictionary<Team, int> _stoneCounts =
        Enum.GetValues(typeof(Team)).Cast<Team>().ToDictionary(team => team, _ => 4);

    private int _turnCount;
    private int _roundNumber = 1;
    public static Action OnEndSession;

    public void GameStart()
    {
        _stonesBlueImages = _stonesBlue.GetComponentsInChildren<Image>();
        _stonesRedImages = _stonesRed.GetComponentsInChildren<Image>();
        _randomService = Services.Get<RandomService>();
        _scoreService = Services.Get<ScoreService>();
        ShootingService.OnShoot += DisableSlider;
        Stone.OnStopped += EnableSlider;
    }

    public void GameUpdate(float delta)
    {
        UpdateSliderValue();
        UpdateScoreText();
        UpdateRound();

        if (_roundNumber == 6) EndSession();
    }

    public void AddTurn()
    {
        _turnCount++;
    }

    public void RemoveStone(Team teamToRemove)
    {
        (teamToRemove switch
            {
                Team.Blue => _stonesBlueImages,
                Team.Red => _stonesRedImages,
                _ => throw new ArgumentOutOfRangeException(nameof(teamToRemove), teamToRemove, null)
            })
            [_stoneCounts[teamToRemove]]
            .gameObject
            .SetActive(false);

        _stoneCounts[teamToRemove]--;
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

    private void UpdateScoreText()
    {
        _blueScore.text = _scoreService.ScoreBlue.ToString("0.00");
        _redScore.text = _scoreService.ScoreRed.ToString("0.00");
    }

    private void UpdateRound()
    {
        _uiRoundNumber.text = _roundNumber.ToString("0");
        if (_turnCount != 2) return;
        _roundNumber++;
        _turnCount = 0;
    }

    private void UpdateSliderValue()
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

    private void OnDestroy()
    {
        ShootingService.OnShoot -= DisableSlider;
        Stone.OnStopped -= EnableSlider;
    }
}