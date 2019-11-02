using System;
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
  private static readonly int left = Animator.StringToHash("left");
  private static readonly int right = Animator.StringToHash("right");

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
      rb2d.AddForce((target - position) * 300);
      switch (GetDirectionOfMovement(Direction(target - position)))
      {
        case 1:
        {
         // Right
         animator.SetBool(backward, false);
         animator.SetBool(forward, false);
         animator.SetBool(left, false);
         animator.SetBool(right, true);
         break;
        }
        case 2:
        {
          // Up
          animator.SetBool(backward, true);
          animator.SetBool(forward, false);
          animator.SetBool(left, false);
          animator.SetBool(right, false);
          break;
        }
        case 3:
        {
          // left
          animator.SetBool(backward, false);
          animator.SetBool(forward, false);
          animator.SetBool(left, true);
          animator.SetBool(right, false);
          break;
        }
        default:
        {
         // Down
         animator.SetBool(backward, false);
         animator.SetBool(forward, true);
         animator.SetBool(left, false);
         animator.SetBool(right, false);
         break;
        }
      }

      isTargetNull = false;
    }

    if (isTargetNull) return;
    //    Prevent jiggling when reach destination
    if ((target - position).sqrMagnitude < 0.005)
    {
      transform.position = target;
      rb2d.velocity = Vector2.zero;
      rb2d.angularVelocity = 0;
      animator.SetBool(backward, false);
      animator.SetBool(forward, false);
      animator.SetBool(left, false);
      animator.SetBool(right, false);
    }
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.collider.isTrigger) return;
    // Hit wall and stopped moving
    rb2d.velocity = Vector2.zero;
    rb2d.angularVelocity = 0;
    animator.SetBool(backward, false);
    animator.SetBool(forward, false);
    animator.SetBool(left, false);
    animator.SetBool(right, false);
    target = transform.position;
    isTargetNull = true;
  }

  private double Direction(Vector2 vec)
  {
    double val = Mathf.Rad2Deg * Math.Atan2(vec.y, vec.x);
    if (val < 0) val += 360;
    return val;
  }

  private int GetDirectionOfMovement(double angle)
  {
    if (angle < 45 || angle > 315) return 1;
    if (angle > 45 && angle < 135) return 2;
    if (angle > 135 && angle < 225) return 3;
    return 4;
  }
}