using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FlightTrajectory : MonoBehaviour
{
    [SerializeField] private Rocket _rocket;
    [SerializeField] private GameObject _leftTrajectoryLine;
    [SerializeField] private GameObject _rightTrajectoryLine;
    
    
    private bool _isTrackingRocket = true;

    private void Start()
    {
        transform.position = GetPositionByRocket();
        
        RocketEvents.DestroyRocketEvent.AddListener(() => {
            _isTrackingRocket = false;
        });
    }
    
    private void Update()
    {
        if (!_isTrackingRocket)
        {
            _leftTrajectoryLine.transform.position += Vector3.left;
            _rightTrajectoryLine.transform.position += Vector3.right;
        }
        else
        {
            transform.position = GetPositionByRocket();
        }
    }


    public Vector3 GetPositionByRocket()
    {
        Vector3 currentPosition = transform.position;
        float rocketPositionY = _rocket.transform.position.y;
        return new Vector3(currentPosition.x, rocketPositionY, currentPosition.z);
    }
}
