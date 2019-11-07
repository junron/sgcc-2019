using System;
using UnityEngine;
using UnityEngine.UI;

public class GameSwitch : MonoBehaviour
{
  public string scene;
  public Image background;
  public Sprite path1, path2;

  private void Start()
  {
    if (scene != "Party") return;
    background.sprite = Variables.AllAttempted() ? path2 : path1;
  }

  private void OnMouseOver()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Interstitials.Initiate(scene);
    }
  }
}