using System;
using UnityEngine;

public class Touchable : MonoBehaviour
{
  public GameObject target;
  public Action callback;
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject == target)
    {
      Destroy(this.gameObject);
      callback();
    }
  }
}