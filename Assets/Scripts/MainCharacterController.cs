using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
  public float speed = 3.0f;

  private Rigidbody2D rb2d;

  private Vector3 target;
  private bool isTargetNull = true;
  private bool isCameraNotNull;
  private Camera mainCamera;
  private Vector3 originalPosition;

  // Start is called before the first frame update
  void Start()
  {
    Time.timeScale = 1;
    Variables.player = this.gameObject;
    mainCamera = Camera.main;
    isCameraNotNull = mainCamera != null;
    rb2d = GetComponent<Rigidbody2D>();
    rb2d.freezeRotation = true;
  }

  // Update is called once per frame
  void Update()
  {
    Vector3 position = this.transform.position;
    if (Input.GetMouseButtonDown(0))
    {
      if (isCameraNotNull)
      {
        Vector3 clickPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        target = position + Vector3.ClampMagnitude(clickPos-position,10);
      }

      originalPosition = position;
      target.z = position.z;
      isTargetNull = false;
    }

    if (isTargetNull) return;
    //    Prevent jiggling when reach destination
    if ((target - position).sqrMagnitude < 0.005)
    {
      transform.position = target;
      return;
    }

    //    Completed distance / total distance
    float percentageCompleted = (originalPosition - position).sqrMagnitude / (originalPosition - target).sqrMagnitude;
    Vector3 direction = (target - transform.position).normalized;
    //    Ensure at least 30% speed at all times
    rb2d.velocity = 2f * Mathf.Max(0.3f, 1 - percentageCompleted) * speed * direction;
  }
}