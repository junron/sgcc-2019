using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droppable : MonoBehaviour
{
  private Rigidbody2D rb;

  [SerializeField] private DropTarget[] targets;
  [SerializeField] private float threshold;

  private Vector3[] targetPositions;
  private DropTarget closestTarget;

  // Start is called before the first frame update
  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    targetPositions = new Vector3[targets.Length];
    int i = 0;
    foreach (DropTarget target in targets)
    {
      targetPositions[i] = target.GetComponent<Transform>().position;
      i++;
    }
  }

  // Update is called once per frame
  private void FixedUpdate()
  {
    closestTarget = null;
    foreach (DropTarget t in targets) t.SetClosest(false);
    Vector3 thisPosition = this.transform.position;
    float minDist = float.MaxValue;
    int minIndex = -1;
    for (int i = 0; i < targetPositions.Length; i++)
    {
      float distance = Mathf.Abs(Vector3.Distance(thisPosition, targetPositions[i]));
      if (distance > threshold) continue;
      if (!(distance < minDist)) continue;
      minDist = distance;
      minIndex = i;
    }

    if (minIndex == -1) return;

    DropTarget target = targets[minIndex];
    closestTarget = target;
    target.SetClosest(true);
  }

  private void OnMouseUp()
  {
    if (closestTarget == null) return;
    closestTarget.occupant = this;
    rb.MovePosition(closestTarget.GetComponent<Transform>().position);
  }
}