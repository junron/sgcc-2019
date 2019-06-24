using System;
using UnityEngine;

public class DoorSensor : MonoBehaviour
{
  [SerializeField] private bool isNumber1;
  [SerializeField] private Animator controller;

  private void OnTriggerEnter2D(Collider2D other)
  {
    controller.SetTrigger("Sensor" + (isNumber1 ? "1" : "2"));
  }
}