using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class UIController : Service, IStart, IUpdate
{
    [SerializeField] private Slider _slider;
    private RandomService _randomService;

    public void GameStart()
    {
        _randomService = Services.Get<RandomService>();
    }

    public void GameUpdate(float delta)
    {
        _slider.value = _randomService.ForceMultiplier;
    }
}
