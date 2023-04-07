using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform _ball;
    [SerializeField] private float _speed;
    private float prevBallPosX;
    private float newBallPosX;
    
    private void Start()
    {
        
    }

    private void Update()
    {
        prevBallPosX = _ball.position.x;
        var deltaBallPosX = prevBallPosX - newBallPosX;
        transform.position += new Vector3(deltaBallPosX, 0f, 0f);
        newBallPosX = _ball.position.x;
    }
}
