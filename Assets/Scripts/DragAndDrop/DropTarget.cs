using System;
using System.Collections.Generic;
using UnityEngine;

public class DropTarget : MonoBehaviour
{
  [SerializeField] private Animator animator;

  private List<Action<Droppable>> callbacks = new List<Action<Droppable>>();
  private Droppable _occupant;
  public Droppable occupant
  {
    get => _occupant;
    set
    {
      _occupant = value;
      foreach (Action<Droppable> callback in callbacks)
      {
        callback(_occupant);
      }
    }
  }

  private static readonly int Closest = Animator.StringToHash("closest");
  public void SetClosest(bool closest)
  {
    animator.SetBool(Closest,closest);
  }

  public void AddCallback(Action<Droppable> callback)
  {
    callbacks.Add(callback);
  }
}