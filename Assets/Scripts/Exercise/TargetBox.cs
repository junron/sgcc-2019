using System;
using System.Collections.Generic;
using UnityEngine;

public class TargetBox : MonoBehaviour
{
  [SerializeField] private GameObject targetObject;
  public Action<bool> onStateChange;
  private bool overlaps;
  private Vector2[] bounds;

  private void Start()
  {
    Bounds colliderBounds = GetComponent<Collider2D>().bounds;
    bounds =  new Vector2[]{colliderBounds.min,colliderBounds.max};
  }

  private void OnTriggerStay2D(Collider2D other)
  {
    if (other.gameObject != targetObject) return;
    Bounds otherColliderBounds = other.bounds;
    Vector2[] otherBounds = {otherColliderBounds.min,otherColliderBounds.max};
    bool nowOverlaps = CompleteOverlap(bounds, otherBounds);
    if (nowOverlaps == overlaps) return;
    overlaps = nowOverlaps;
    Debug.Log(onStateChange);
    onStateChange(overlaps);
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    OnTriggerStay2D(other);
  }

  private bool CompleteOverlap(IList<Vector2> box, IList<Vector2> target)
  {
    //Variables are [min,max] pairs

    //Target is to left of box
    if (box[0].x > target[0].x) return false;
    //Target is to right of box
    if (box[1].x < target[1].x) return false;
    //Target is below box
    if (box[0].y > target[0].y) return false;
    //Target is above box
    return !(box[1].y < target[1].y);
  }
}