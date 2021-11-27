using System.Collections.Generic;
using UnityEngine;

public class RocketCollisionHandler : MonoBehaviour
{
  private void DestroyRocket()
  {
    gameObject.SetActive(false);
    Destroy(gameObject);
    RocketEvents.DestroyRocketEvent.RemoveListener(DestroyRocket);
  }

  private void Start()
  {
    RocketEvents.DestroyRocketEvent.AddListener(DestroyRocket);
  }
  
  private void OnCollisionEnter(Collision other)
  {
    if (!other.transform.gameObject.CompareTag(GameTags.FRIENDLY))
    {
      List<GameObject> children = new List<GameObject>();

      foreach (Transform child in transform)
      {
        child.SetParent(null);
        children.Add(child.gameObject);

        var childRigidBody = child.transform.gameObject.AddComponent<Rigidbody>();
        childRigidBody.mass = 0.3f;
        childRigidBody.AddExplosionForce(200f, transform.position, 15.0F);
      }
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
    if (other.transform.gameObject.CompareTag(GameTags.FRIENDLY))
    {
      Destroy(other.transform.gameObject);
    }
  }
}