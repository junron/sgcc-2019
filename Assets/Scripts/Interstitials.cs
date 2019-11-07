using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class Interstitials : MonoBehaviour
{
  private Text loadingText, tip;
  private static string nextScene;

  private static string[] stuff =
  {
    "Did you know that by 2050, almost half of the Singaporean population will be above 65?",
    "If you are over 65, you may be eligible for the Silver Support Scheme, where thr government gives monthly payouts of $300 to $750.",
    "If you are born on or before 1949, you can receive benefits under the Pioneer Generation Package!",
    "Seniors who live in a 4-room or smaller flat can use the Lease Buyback Scheme to gain extra income in retirement!",
  };
  private int counter;

  private void Start()
  {
    Time.timeScale = 1;
    loadingText = this.transform.Find("LoadingText").GetComponent<Text>();
    tip = this.transform.Find("Tip").GetComponent<Text>();
    InvokeRepeating(nameof(UpdateCounter), 1, 1);
    tip.text = stuff[UnityEngine.Random.Range(0,3)];
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