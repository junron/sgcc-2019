using System;
using UnityEngine;

public class Trigger : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other)
  {
    Exercise.Complete();
  }
}