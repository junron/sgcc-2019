using UnityEngine;

public class CameraClamp : MonoBehaviour
{
  private void Update()
  {
    Transform currTransform = this.transform;
    Vector3 position = currTransform.position;
    if (position.x <= -0.2) {
      currTransform.position = new Vector3(-0.2f, position.y, -1);
    } else if (position.x >= 4.3f) {
      currTransform.position = new Vector3(4.3f, position.y, -1);
    }

    // Y axis
    if (position.y <= -3) {
      currTransform.position = new Vector3(position.x, -3f, -1);
    } else if (position.y >= 2.7f) {
      currTransform.position = new Vector3(position.x, 2.7f, -1);
    }
  }
}