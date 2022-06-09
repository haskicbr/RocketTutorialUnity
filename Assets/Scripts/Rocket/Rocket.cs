using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Rocket : ComposeBody, IDamagable
{
  [SerializeField] private float _thrustForce = 30f;
  [SerializeField] private float _rotationSpeed = 100f;
  [SerializeField] private float _thrustForceHorizontal = 1000f;
  [SerializeField] private float _maxEngineVelocityY = 50f;
  [SerializeField] private float _minEngineVelocityY = 50f;
  [SerializeField] public int Health { get; private set; }


  [SerializeField] private HealthBar _healthBar;
  [SerializeField] private GameObject[] _rocketEngines;
  [SerializeField] private GameObject[] _rocketEnginesMain;

  private Rigidbody _rocketRigidBody;
  private AudioSource _engineSound;

  private void Start()
  {
    _rocketRigidBody = GetComponent<Rigidbody>();
    _engineSound = GetComponent<AudioSource>();
    _healthBar.СhangeHealth(Health);

    RocketEvents.DamageEvent.AddListener(Damage);
    RocketEvents.IncreasePowerEvent.AddListener(IncreasePower);
    
    RocketEvents.DestroyRocketEvent.AddListener(Destroy);
    RocketEvents.DisconnectEnginesSecond.AddListener(() => DetachObjects(_rocketEngines, _rocketRigidBody.velocity));
    RocketEvents.DisconnectEngineMain.AddListener(() => DetachObjects(_rocketEnginesMain, _rocketRigidBody.velocity));
  }

  private void IncreasePower(int powerValue)
  {
    Health += powerValue;
    _healthBar.СhangeHealth(Health);
  }

  private void Damage(int damageValue)
  {
    Health -= damageValue;

    _healthBar.СhangeHealth(Health);

    if (Health <= 0)
    {
      RocketEvents.DestroyRocketEvent.Invoke();
      RocketEvents.DestroyRocketEvent.RemoveAllListeners();
      RocketEvents.DamageEvent.RemoveAllListeners();
    }
  }

  private void FixedUpdate()
  {
    RunEngine();
    var currentVelocity = _rocketRigidBody.velocity;

    if (_rocketRigidBody.velocity.y <= _minEngineVelocityY)
    {
      _rocketRigidBody.velocity = new Vector3(currentVelocity.x, _minEngineVelocityY, currentVelocity.z);
    }

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
      _rocketRigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
  }

  private void RunEngine()
  {
    if (Input.GetKey(KeyCode.Space))
    {
      if (_engineSound.volume < 0.25f)
      {
        _engineSound.volume += 0.05f;
      }

      _rocketRigidBody.AddRelativeForce(Vector3.up * _thrustForce);


      var currentVelocity = _rocketRigidBody.velocity;

      if (_rocketRigidBody.velocity.y >= _maxEngineVelocityY)
      {
        _rocketRigidBody.velocity = new Vector3(currentVelocity.x, _maxEngineVelocityY, currentVelocity.z);
      }
    }
    else
    {
      if (_engineSound.volume > 0f)
      {
        _engineSound.volume -= 0.05f;
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
      //transform.Rotate(changedRotation * rotationSpeed);

      _rocketRigidBody.velocity += new Vector3(axisHorizontal, 0f, 0f);
    }
  }

  private void Destroy()
  {
    List<Transform> children = new List<Transform>();

    for (int childCounter = 0; childCounter < transform.childCount; childCounter++)
    {
      children.Add(transform.GetChild(childCounter));
    }

    foreach (Transform child in children)
    {
      child.SetParent(null, true);
      var childRigidBody = child.gameObject.AddComponent<Rigidbody>();
      childRigidBody.mass = 0.3f;
      childRigidBody.AddExplosionForce(200f, transform.position, 15.0F);
    }

    RocketEvents.DestroyRocketEvent.RemoveListener(Destroy);
    RocketEvents.DisconnectEnginesSecond.RemoveAllListeners();
    RocketEvents.DisconnectEngineMain.RemoveAllListeners();

    _engineSound.Stop();
  }
}