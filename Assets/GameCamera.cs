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

    RocketEvents.DestroyRocketEvent.AddListener(() =>
    {
      var childRigidBody = gameObject.AddComponent<Rigidbody>();
      childRigidBody.mass = 2f;
      childRigidBody.AddExplosionForce(200f, transform.position, 15.0F);

      var cameraCollider = gameObject.AddComponent<BoxCollider>();
      cameraCollider.size = new Vector3(1f, 1f, 1f);

      _isTrackingRocket = false;
    });
  }

  private void FixedUpdate()
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