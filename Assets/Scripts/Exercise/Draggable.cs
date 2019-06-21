using System;
using UnityEngine;

public class Draggable : MonoBehaviour
{
  private Rigidbody2D rb;
  private Vector3 endPos;
  private bool isEnd;
  private Vector3 parent;
  public float z = 10.0f;

  // Start is called before the first frame update
  private void Start()
  {
    parent = this.transform.parent.transform.position;
    rb = GetComponent<Rigidbody2D>();
    isEnd = GetComponent<HingeJoint2D>() == null;
  }

  private void OnMouseDrag()
  {
    endPos = ToWorldPoint(Input.mousePosition);
  }

  private void FixedUpdate()
  {
    if (endPos == Vector3.zero) return;
//    if ((endPos - parent).sqrMagnitude > 17.7*17.7)
//    {
//      Debug.Log((endPos - parent).magnitude);
//      return;
//    }
    rb.MovePosition(endPos);
  }

  private Vector3 ToWorldPoint(Vector3 screenPt)
  {
    screenPt.z = z;
    return Camera.main.ScreenToWorldPoint(screenPt);
  }
}