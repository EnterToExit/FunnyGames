using System;
using UnityEngine;

public class Stone : MonoBehaviour
{
    
    private void Awake()
    {
        var shootingService = FindObjectOfType<ShootingService>();
        shootingService._stones.Add(gameObject);
    }
}