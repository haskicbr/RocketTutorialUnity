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
        RocketEvents.DamageEvent.Invoke(30);
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
        RocketEvents.DamageEvent.Invoke(5);
        break;
            
      case GameTags.Energy:
        RocketEvents.IncreasePowerEvent.Invoke(15);
        Destroy(other.transform.gameObject.GetComponent<BoxCollider>());
        Destroy(other.transform.gameObject);
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