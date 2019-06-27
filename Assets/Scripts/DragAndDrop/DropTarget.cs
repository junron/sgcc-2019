using System;
using System.Collections.Generic;
using UnityEngine;

public class DropTarget : MonoBehaviour
{
  [SerializeField] private Animator animator;

  private List<Action<Droppable>> callbacks = new List<Action<Droppable>>();
  public Droppable occupant;

  public void Trigger()
  {
    foreach (Action<Droppable> callback in callbacks)
    {
      callback(occupant);
    }
  }

  private static readonly int Closest = Animator.StringToHash("closest");
  public void SetClosest(bool closest)
  {
    if (animator == null) return;
    animator.SetBool(Closest,closest);
  }

  public void AddCallback(Action<Droppable> callback)
  {
    callbacks.Add(callback);
  }
}