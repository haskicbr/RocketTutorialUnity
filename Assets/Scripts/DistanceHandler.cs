using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceHandler : MonoBehaviour
{
  [SerializeField] private Rocket _rocket;

  private const int RealDistanceToSpace = 100000;
  private const float GameDistanceToSpaceCoefficient = 0.2f;
  private const int SecondEnginesDistanceToDetach = 15000;
  private const int MainEnginesDistanceToDetach = 20000;
  
  private Text _distanceToSpaceText;
  private int _distanceToSpace;
  private int _currentDistance;
  
  // Start is called before the first frame update
  private void Start()
  {
    _distanceToSpace = (int) Math.Round(RealDistanceToSpace * GameDistanceToSpaceCoefficient);
    _distanceToSpaceText = GetComponent<Text>();
    _currentDistance = 0;
  }

  // Update is called once per frame
  private void Update()
  {
    _currentDistance = GetDistanceToSpace();
    _distanceToSpaceText.text = $"Distance to space: {_distanceToSpace - _currentDistance} km";

    if (_currentDistance > MainEnginesDistanceToDetach * GameDistanceToSpaceCoefficient)
    {
      RocketEvents.DisconnectEngineMain.Invoke();
      RocketEvents.DisconnectEngineMain.RemoveAllListeners();  
    }
    
    if (_currentDistance > SecondEnginesDistanceToDetach * GameDistanceToSpaceCoefficient)
    {
      RocketEvents.DisconnectEnginesSecond.Invoke();
      RocketEvents.DisconnectEnginesSecond.RemoveAllListeners();  
    }
  }
  
  private int GetDistanceToSpace()
  {
    return (int) Math.Round(_rocket.transform.position.y);
  }
}