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
  private Animator animator;

  private static readonly int forward = Animator.StringToHash("forward");
  private static readonly int backward = Animator.StringToHash("backward");

  // Start is called before the first frame update
  void Start()
  {
    Time.timeScale = 1;
    Variables.player = this.gameObject;
    animator = GetComponent<Animator>();
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
        target = position + Vector3.ClampMagnitude(clickPos - position, 10);
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
      rb2d.velocity = Vector2.zero;
      animator.SetBool(forward, false);
      animator.SetBool(backward, false);
      return;
    }

    //    Completed distance / total distance
    float percentageCompleted = (originalPosition - position).sqrMagnitude / (originalPosition - target).sqrMagnitude;
    Vector3 direction = (target - transform.position).normalized;
    //    Ensure at least 30% speed at all times
    rb2d.velocity = 0.75f * 2f * Mathf.Max(0.3f, 1 - percentageCompleted) * speed * direction;
    if (rb2d.velocity.y > 0)
    {
      animator.SetBool(backward, true);
      animator.SetBool(forward, false);
    }
    else if (rb2d.velocity.y < 0)
    {
      animator.SetBool(forward, true);
      animator.SetBool(backward, false);
    }
  }
}