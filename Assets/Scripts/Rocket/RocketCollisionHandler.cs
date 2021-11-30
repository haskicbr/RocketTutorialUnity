using System.Collections.Generic;
using UnityEngine;

public class RocketCollisionHandler : MonoBehaviour
{
  private void OnCollisionEnter(Collision other)
  {
    if (!other.transform.gameObject.CompareTag(GameTags.Friendly))
    {
      //RocketEvents.DestroyRocketEvent.Invoke();
    }
  }

  private void FixedUpdate()
  {
    if (transform.childCount == 0)
    {
      //RocketEvents.DestroyRocketEvent.Invoke();
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
        RocketEvents.DestroyRocketEvent.Invoke();
        break;
    }
  }
}