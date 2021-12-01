using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
  [SerializeField] private GameObject _alien;
  [SerializeField] private GameObject _asteroid;
  [SerializeField] private GameObject _spaceTrash;
  [SerializeField] private GameObject _battery;
  [SerializeField] private Rocket _rocket;
  [SerializeField] private float _differenceRocketPosition = 50f;
  [SerializeField] private float _minSpwanPositionX = -5f;
  [SerializeField] private float _maxSpawnPositionX = 5f;

  private float _lastSpawnPosition = 0f;

  private void Start()
  {
    _lastSpawnPosition = _rocket.transform.position.y;
  }

  private void Update()
  {
    float rocketPositionY = _rocket.transform.position.y;

    if (rocketPositionY > _lastSpawnPosition)
    {
      _lastSpawnPosition = rocketPositionY + _differenceRocketPosition;
      SpawnItems();
    }
  }


  private void SpawnBattery()
  {
    GameObject battery = Instantiate(_battery);
    float spawnXPosition = Random.Range(_minSpwanPositionX, _maxSpawnPositionX);
    battery.transform.position =
      new Vector3(spawnXPosition, _rocket.transform.position.y + 100f + Random.Range(50f, 75f), 0f);
  }

  private void SpawnSpaceTrash()
  {
    GameObject battery = Instantiate(_spaceTrash);
    float spawnXPosition = Random.Range(_minSpwanPositionX, _maxSpawnPositionX);
    battery.transform.position =
      new Vector3(spawnXPosition, _rocket.transform.position.y + 100f + Random.Range(50f, 75f), 0f);
  }


  private void SpawnItems()
  {
    SpawnSpaceTrash();
    SpawnBattery();
  }
}