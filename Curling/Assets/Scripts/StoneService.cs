using UnityEngine;

public class StoneService : Service, IStart, IUpdate
{
    private Stone _stone;

    public void GameStart()
    {
        _stone = FindObjectOfType<Stone>();
    }

    public void GameUpdate(float delta)
    {
        // _stone.transform.position += Vector3.forward * Time.deltaTime;
    }
}