using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
  [SerializeField] private Rocket _rocket;
  [SerializeField] private float offsetRocketPositionY = 20f;
  private bool _isTrackingRocket = true;


  private void Start()
  {
    transform.position = GetPositionByRocket();

    RocketEvents.DestroyRocketEvent.AddListener(AddCameraFallEffect);
  }

  private void AddCameraFallEffect()
  {
    var rigidBody = gameObject.AddComponent<Rigidbody>();
    rigidBody.mass = 2f;
    rigidBody.AddExplosionForce(200f, transform.position, 15.0F);

    var collider = gameObject.AddComponent<BoxCollider>();
    collider.size = new Vector3(1f, 1f, 1f);

    _isTrackingRocket = false;
  }
  
  private void Update()
  {
    if (_isTrackingRocket)
    {
      transform.position = GetPositionByRocket();
    }
  }

  private Vector3 GetPositionByRocket()
  {
    Vector3 currentPosition = transform.position;
    float rocketPositionY = _rocket.transform.position.y;
    return new Vector3(currentPosition.x, rocketPositionY + offsetRocketPositionY, currentPosition.z);
  }
}