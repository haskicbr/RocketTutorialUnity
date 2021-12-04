using System;
using System.Collections.Generic;
using UnityEngine;



public class RocketCollisionHandler : MonoBehaviour
{

  private void OnCollisionEnter(Collision other)
  {
    switch (other.transform.gameObject.tag)
    {
      case GameTags.SpaceTrash:
        RocketEvents.DamageEvent.Invoke(10);
        Destroy(other.transform.gameObject.GetComponent<BoxCollider>());
        break;
    }
  }
  
  private void OnTriggerEnter(Collider other)
  {
    switch (other.transform.gameObject.tag)
    {
      case GameTags.Friendly:
        Destroy(other.transform.gameObject);
        break;
      case GameTags.Trajectory:
        RocketEvents.DamageEvent.Invoke(100);
        break;
    }
  }

  private void OnTriggerStay(Collider other)
  {
    switch (other.transform.gameObject.tag)
    {
      case GameTags.Trajectory:
        RocketEvents.DamageEvent.Invoke(5);
        break;
    }
  }
}