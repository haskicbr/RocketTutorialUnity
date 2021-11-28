using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameCamera : MonoBehaviour
{
    [SerializeField] private Rocket _rocket;
    [SerializeField] private float offsetRocketPositionY = 20f;
    
    private void Start()
    {
        transform.position = GetPositionByRocket();
    }
    
    private void FixedUpdate()
    {
        transform.position = GetPositionByRocket();
    }

    private Vector3 GetPositionByRocket()
    {
        Vector3 currentPosition = transform.position;
        float rocketPositionY = _rocket.transform.position.y;
        return new Vector3(currentPosition.x, rocketPositionY + offsetRocketPositionY, currentPosition.z);
    }
}
