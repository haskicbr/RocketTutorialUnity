using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FlightTrajectory : MonoBehaviour
{
    [SerializeField] private Rocket _rocket;
    
    private void Start()
    {
        transform.position = GetPositionByRocket();
    }
    
    private void FixedUpdate()
    {
        transform.position = GetPositionByRocket();
    }

    public Vector3 GetPositionByRocket()
    {
        Vector3 currentPosition = transform.position;
        float rocketPositionY = _rocket.transform.position.y;
        return new Vector3(currentPosition.x, rocketPositionY, currentPosition.z);
    }
}
