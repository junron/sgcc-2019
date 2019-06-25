using System;
using UnityEngine;

public class ExpandOnHover : MonoBehaviour
{
  [SerializeField] private Animator controller;
  private void OnMouseEnter()
  {
    Debug.Log("mousever");
    controller.SetTrigger("Mouseover");
  }

  private void OnMouseExit()
  {
    Debug.Log("exit");
    controller.SetTrigger("Mouseexit");
  }
}
