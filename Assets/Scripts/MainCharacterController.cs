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
  private Animator animator;

  private static readonly int forward = Animator.StringToHash("forward");
  private static readonly int backward = Animator.StringToHash("backward");
  private static readonly int left = Animator.StringToHash("left");
  private static readonly int right = Animator.StringToHash("right");
  private int movement = 0;

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

      target.z = position.z;
      switch (GetDirectionOfMovement(Direction(target - position)))
      {
        case 1:
        {
          // Right
          animator.SetBool(backward, false);
          animator.SetBool(forward, false);
          animator.SetBool(left, false);
          animator.SetBool(right, true);
          rb2d.AddForce(Math.Abs(target.x - position.x) * 300 * Vector2.right);
          movement = 1;
          break;
        }
        case 2:
        {
          // Up
          animator.SetBool(backward, true);
          animator.SetBool(forward, false);
          animator.SetBool(left, false);
          animator.SetBool(right, false);
          rb2d.AddForce(Math.Abs(target.y - position.y) * 300 * Vector2.up);
          movement = 2;
          break;
        }
        case 3:
        {
          // left
          animator.SetBool(backward, false);
          animator.SetBool(forward, false);
          animator.SetBool(left, true);
          animator.SetBool(right, false);
          rb2d.AddForce(Math.Abs(target.x - position.x) * 300 * Vector2.left);
          movement = 1;
          break;
        }
        default:
        {
          // Down
          animator.SetBool(backward, false);
          animator.SetBool(forward, true);
          animator.SetBool(left, false);
          animator.SetBool(right, false);
          rb2d.AddForce(Math.Abs(target.y - position.y) * 300 * Vector2.down);
          movement = 2;
          break;
        }
      }

      isTargetNull = false;
    }

    if (isTargetNull) return;
    //    Prevent jiggling when reach destination
    if (movement == 1 && Math.Abs(position.x - target.x) < 0.005)
    {
      transform.position = new Vector2(target.x, position.y);
      reset();
    }else if (movement == 2 && Math.Abs(position.y - target.y) < 0.005)
    {
      transform.position = new Vector2(position.x, target.y);
      reset();
    }
  }

  private void reset()
  {
    rb2d.velocity = Vector2.zero;
    rb2d.angularVelocity = 0;
    animator.SetBool(backward, false);
    animator.SetBool(forward, false);
    animator.SetBool(left, false);
    animator.SetBool(right, false);
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