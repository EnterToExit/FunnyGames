using UnityEngine;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;

public class StoneService : Service, IStart, IUpdate
{
    private float _passedTime;
    private Stone _stone;

    public void GameStart()
    {
        _stone = FindObjectOfType<Stone>();
    }

    public void GameUpdate(float delta)
    {
        _passedTime += Time.deltaTime;
    }
}