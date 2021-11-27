using UnityEngine.Events;

public struct RocketEvents
{
  public static readonly UnityEvent DestroyRocketEvent = new UnityEvent();
  public static readonly UnityEvent<float> IncreasePower = new UnityEvent<float>();
}