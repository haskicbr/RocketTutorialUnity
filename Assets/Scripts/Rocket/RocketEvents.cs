using UnityEngine.Events;

public struct RocketEvents
{
  public static readonly UnityEvent DestroyRocketEvent = new UnityEvent();
  public static readonly UnityEvent DisconnectEnginesSecond = new UnityEvent();
  public static readonly UnityEvent DisconnectEngineMain = new UnityEvent();
  public static readonly UnityEvent<int> IncreasePowerEvent = new UnityEvent<int>();
  public static readonly UnityEvent<int> DamageEvent = new UnityEvent<int>();
}