using UnityEngine;

public class ExpandOnHover : MonoBehaviour
{
  [SerializeField] private Animator controller;
  public bool disabled;
  private void OnMouseEnter()
  {
    if (disabled) return;
    controller.SetTrigger("Mouseover");
  }

  private void OnMouseExit()
  {
    if (disabled) return;
    controller.SetTrigger("Mouseexit");
  }
}
