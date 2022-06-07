using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneRunner : MonoBehaviour
{
  [SerializeField] private Button button;

  private void Start()
  {
    button.onClick.AddListener(() => { SceneManager.LoadScene("Level 1"); });
  }
}