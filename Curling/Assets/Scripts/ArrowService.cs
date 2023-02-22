using UnityEngine;

public class ArrowService : Service, IStart, IUpdate
{
    private Arrow _arrow;
    private DirectionService _directionService;
    private StoneStartPos _startPos;
    private bool _arrowIsActive;

    public void GameStart()
    {
        _directionService = FindObjectOfType<DirectionService>();
        _startPos = FindObjectOfType<StoneStartPos>();
        _arrow = FindObjectOfType<Arrow>();
        _arrowIsActive = true;
    }

    public void GameUpdate(float delta)
    {
        var y = _directionService._dick;
        _arrow.transform.eulerAngles = new Vector3(0f, y);
        transform.position = _startPos.transform.position;
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