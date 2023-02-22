using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseService : Service, IStart, IUpdate
{
    private Vector3 _mousePos;
    private Vector3 _mousePosNew;
    public Vector2 mouseScroll;
    public Vector3 mouse;

    public void GameStart()
    {
        // throw new System.NotImplementedException();
    }

    public void GameUpdate(float delta)
    {
        _mousePos = Input.mousePosition;
        _mousePosNew = Input.mousePosition;
        mouse = _mousePosNew - _mousePos;
        mouseScroll = Input.mouseScrollDelta;
    }
}
