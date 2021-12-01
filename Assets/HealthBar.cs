using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public void СhangeHealth(int health)
    {
        health = health < 0 ? 0 : health;
        GetComponent<Text>().text = $"Health is : {health}";
    }
}
