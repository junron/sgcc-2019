using System;
using UnityEngine;
using UnityEngine.UI;

public class GameSwitch : MonoBehaviour
{
  public string scene;
  public Image background;
  public Sprite path1, path2;
  public GameObject endgame;

  private void Start()
  {
    if (scene != "Party") return;
    background.sprite = Variables.AllAttempted() ? path2 : path1;
    endgame.SetActive(Variables.AllAttempted());
  }

  private void OnMouseOver()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Interstitials.Initiate(scene);
    }
  }
}