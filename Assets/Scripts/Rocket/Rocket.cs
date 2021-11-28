using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Rocket : MonoBehaviour
{
  [SerializeField] private float _thrustForce = 30f;
  [SerializeField] private float _rotationSpeed = 100f;
  [SerializeField] private float _maxEngineVelocityY = 50f;
  [SerializeField] private int _rocketHealth = 100;
  
  
  private Rigidbody _rocketRigidBody;
  private AudioSource _engineSound;
  
  private void Start()
  {
    _rocketRigidBody = GetComponent<Rigidbody>();
    _engineSound = GetComponent<AudioSource>();

    RocketEvents.DestroyRocketEvent.AddListener(Destroy);
  }
  
  private void Update()
  {
    RunEngine();
    Rotate();
  }

  private void changeFreezeRotation(bool freezRotaion)
  {
    if (freezRotaion)
    {
      _rocketRigidBody.freezeRotation = freezRotaion;
    }
    else
    {
      _rocketRigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }
  }

  private void RunEngine()
  {
    if (Input.GetKey(KeyCode.Space))
    {
      if (!_engineSound.isPlaying)
      {
        _engineSound.Play();
      }

      _rocketRigidBody.AddRelativeForce(Vector3.up * _thrustForce);

      if (_rocketRigidBody.velocity.y >= _maxEngineVelocityY)
      {
        var currentVelocity = _rocketRigidBody.velocity;
        _rocketRigidBody.velocity = new Vector3(currentVelocity.x, _maxEngineVelocityY, currentVelocity.z);
      }
    }
    else
    {
      if (_engineSound.isPlaying)
      {
        _engineSound.Stop();
      }
    }
  }

  private void Rotate()
  {
    float axisHorizontal = Input.GetAxis("Horizontal");
    float rotationSpeed = _rotationSpeed * Time.deltaTime;

    if (axisHorizontal != 0)
    {
      var changedRotation = new Vector3(0, 0, -axisHorizontal);
      transform.Rotate(changedRotation * rotationSpeed);
    }

    changeFreezeRotation(axisHorizontal != 0);
  }

  private void Destroy()
  {
    foreach (Transform child in transform)
    {
      child.SetParent(null);
      
      var childRigidBody = child.transform.gameObject.AddComponent<Rigidbody>();
      childRigidBody.mass = 0.3f;
      childRigidBody.AddExplosionForce(200f, transform.position, 15.0F);
    }

    gameObject.SetActive(false);
    RocketEvents.DestroyRocketEvent.RemoveListener(Destroy);
  }
}