using UnityEngine;

public class ArrowService : Service, IStart, IUpdate
{
    private Arrow _arrow;
    private bool _arrowIsActive;

    public void GameStart()
    {
        _arrow = FindObjectOfType<Arrow>();
        _arrowIsActive = true;
    }

    public void GameUpdate(float delta)
    {
        if (!Input.GetMouseButtonDown(0)) return;
        // ChangeActive();
    }

    private void ChangeActive()
    {
        switch (_arrowIsActive)
        {
            case true:
                _arrow.gameObject.SetActive(false);
                _arrowIsActive = false;
                break;
            case false:
                _arrow.gameObject.SetActive(true);
                _arrowIsActive = true;
                break;
        }
    }
}