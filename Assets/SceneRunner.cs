using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneRunner : MonoBehaviour
{
  [SerializeField] private Button startButton;
  [SerializeField] private Button restartButton;
  
  private void Start()
  {
    startButton.onClick.AddListener(() => { SceneManager.LoadScene("Level 1"); });
  }
}