using UnityEngine;

public class CameraClamp : MonoBehaviour
{
  private Transform parent;

  public void FreezeMovement()
  {
    Transform transform1 = this.transform;
    parent = transform1.parent;
    transform1.parent = null;
  }

  public void Unfreeze()
  {
    this.transform.parent = parent;
  }

  private void Update()
  {
    Transform currTransform = this.transform;
    Vector3 position = currTransform.position;
    if (position.x <= -0.2)
    {
      currTransform.position = new Vector3(-0.2f, position.y, -1);
    }
    else if (position.x >= 0.7f)
    {
      currTransform.position = new Vector3(0.7f, position.y, -1);
    }

    // Y axis
    if (position.y <= -3)
    {
      currTransform.position = new Vector3(position.x, -3f, -1);
    }
    else if (position.y >= 0.2)
    {
      currTransform.position = new Vector3(position.x, 0.2f, -1);
    }
  }
}