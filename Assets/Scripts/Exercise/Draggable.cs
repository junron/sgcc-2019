using System;
using UnityEngine;

public class Draggable : MonoBehaviour
{
  private Rigidbody2D rb;
  private Vector3 endPos;
  public float z = 10.0f;

  // Start is called before the first frame update
  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  private void OnMouseDrag()
  {
    endPos = ToWorldPoint(Input.mousePosition);
  }

  private void OnMouseUp()
  {
    endPos = Vector3.zero;
  }

  private void FixedUpdate()
  {
    if (endPos == Vector3.zero) return;
    rb.MovePosition(endPos);
  }

  private Vector3 ToWorldPoint(Vector3 screenPt)
  {
    screenPt.z = z;
    return Camera.main.ScreenToWorldPoint(screenPt);
  }
}