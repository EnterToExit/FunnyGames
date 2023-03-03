using UnityEngine;

public class MouseService : Service, IUpdate
{
    private Vector3 _mousePos;
    private Vector3 _mousePosNew;
    public Vector2 MouseScroll;
    public Vector3 Mouse;
    

    public void GameUpdate(float delta)
    {
        _mousePos = Input.mousePosition; //ESSENTIAL
        
        Mouse = _mousePosNew - _mousePos;
        MouseScroll = Input.mouseScrollDelta;
        
        _mousePosNew = Input.mousePosition; //ESSENTIAL
    }
}
