using UnityEngine;

public class ComposeBody : MonoBehaviour
{
  protected void DetachObjects(GameObject[] objects, Vector3 velocyty, float forse = 100f, float radius = 10f)
  {
    for (int objectCounter = 0; objectCounter < objects.Length; objectCounter++)
    {
      var detachObject = objects[objectCounter].transform;
      detachObject.SetParent(null, true);
      var childRigidBody = detachObject.gameObject.AddComponent<Rigidbody>();
      childRigidBody.mass = 0.3f;
      childRigidBody.velocity = velocyty;
      childRigidBody.AddExplosionForce(forse, transform.position, radius);
    }
  }
}