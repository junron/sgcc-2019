using System;
using UnityEngine;

public class Clickable : MonoBehaviour
{
  public Action callback;
  public bool clicked;

  private void OnMouseDown()
  {
    callback?.Invoke();
    clicked = true;
  }
}