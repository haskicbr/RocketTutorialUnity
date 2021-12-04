using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRuner : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("Level 1");
    }
}
