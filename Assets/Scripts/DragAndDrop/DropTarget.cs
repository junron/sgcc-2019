using UnityEngine;

public class DropTarget : MonoBehaviour
{
  public Droppable occupant;
  private SpriteRenderer renderer;
  private bool closest;

  private void Start()
  {
    renderer = GetComponent<SpriteRenderer>();
  }

  public void SetClosest(bool closest)
  {
    this.closest = closest;
    renderer.color = closest ? Color.green : Color.black;
  }
}