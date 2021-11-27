using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour
{
  [SerializeField] private float _thrustForce = 30f;
  [SerializeField] private float _rotationSpeed = 100f;

  private Rigidbody _rocketRigidBody;
  private AudioSource _engineSound;

  void Start()
  {
    _rocketRigidBody = GetComponent<Rigidbody>();
    _engineSound = GetComponent<AudioSource>();
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

  private void RunEngune()
  {
    if (Input.GetKey(KeyCode.Space))
    {
      if (!_engineSound.isPlaying)
      {
        _engineSound.Play();
      }

      _rocketRigidBody.AddRelativeForce(Vector3.up * _thrustForce);
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

  private void Update()
  {
    RunEngune();
    Rotate();
  }
}