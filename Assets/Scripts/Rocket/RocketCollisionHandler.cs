using System.Collections.Generic;
using UnityEngine;

public class RocketCollisionHandler : MonoBehaviour
{
  private void OnCollisionEnter(Collision other)
  {
    if (!other.transform.gameObject.CompareTag(GameTags.FRIENDLY))
    {
      RocketEvents.DestroyRocketEvent.Invoke();
    }
  }

  private void FixedUpdate()
  {
    if (transform.childCount == 0)
    {
      RocketEvents.DestroyRocketEvent.Invoke();
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    switch (other.transform.gameObject.tag)
    {
      case GameTags.FRIENDLY:
        Destroy(other.transform.gameObject);
        break;
      case GameTags.TRAJECTORY:
        RocketEvents.DestroyRocketEvent.Invoke();
        break;
    }
  }
}