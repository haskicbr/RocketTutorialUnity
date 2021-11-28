using System.Collections.Generic;
using UnityEngine;

public class RocketCollisionHandler : MonoBehaviour
{
  private void DestroyRocket()
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

    gameObject.SetActive(false);
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