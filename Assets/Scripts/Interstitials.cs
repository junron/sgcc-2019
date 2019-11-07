using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interstitials : MonoBehaviour
{
  private Text loadingText, tip;
  private static string nextScene;
  private int counter;

  private void Start()
  {
    Time.timeScale = 1;
    loadingText = this.transform.Find("LoadingText").GetComponent<Text>();
    tip = this.transform.Find("Tip").GetComponent<Text>();
    InvokeRepeating(nameof(UpdateCounter), 1, 1);
    tip.text = "Hello, world";
  }

  private void UpdateCounter()
  {
    if (counter > 4) SceneManager.LoadScene(nextScene);
    loadingText.text += ".";
    counter++;
  }

  public static void Initiate(string nextScene1)
  {
    nextScene = nextScene1;
    SceneManager.LoadScene("Interstitials");
  }
}