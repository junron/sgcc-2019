using System;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
  public float speed = 3.0f;

  private Rigidbody2D rb2d;

  public Vector3 target;
  public bool isTargetNull = true;
  private bool isCameraNotNull;
  private Camera mainCamera;
  private Animator animator;
  public bool inhibit;

  private static readonly int state = Animator.StringToHash("state");
  private int movement;

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
    if (inhibit)
    {
      rb2d.velocity = Vector2.zero;
      animator.SetInteger(state, 0);
      return;
    }
    Vector3 position = this.transform.position;
    if (Input.GetMouseButtonDown(0))
    {
      if (isCameraNotNull)
      {
        Vector3 clickPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        target = position + Vector3.ClampMagnitude(clickPos - position, 10);
      }

      target.z = position.z;
      rb2d.velocity = Vector2.zero;
      switch (GetDirectionOfMovement(Direction(target - position)))
      {
        case 1:
        {
          // Right
          animator.SetInteger(state, 1);
          animator.speed = 1;
          rb2d.AddForce(Math.Abs(target.x - position.x) * 300 * Vector2.right);
          movement = 1;
          break;
        }
        case 2:
        {
          // Up
          animator.SetInteger(state, 2);
          animator.speed = 1;
          rb2d.AddForce(Math.Abs(target.y - position.y) * 300 * Vector2.up);
          movement = 2;
          break;
        }
        case 3:
        {
          // left
          animator.SetInteger(state, 3);
          animator.speed = 1;
          rb2d.AddForce(Math.Abs(target.x - position.x) * 300 * Vector2.left);
          movement = 1;
          break;
        }
        default:
        {
          // Down
          animator.SetInteger(state, 4);
          animator.speed = 1;
          rb2d.AddForce(Math.Abs(target.y - position.y) * 300 * Vector2.down);
          movement = 2;
          break;
        }
      }

      isTargetNull = false;
    }

    if (isTargetNull) return;
    //    Prevent jiggling when reach destination
    if (movement == 1 && Math.Abs(position.x - target.x) < 0.1)
    {
      transform.position = new Vector2(target.x, position.y);
      Reset();
    }
    else if (movement == 2 && Math.Abs(position.y - target.y) < 0.1)
    {
      transform.position = new Vector2(position.x, target.y);
      Reset();
    }
  }

  public void Reset()
  {
    rb2d.velocity = Vector2.zero;
    rb2d.angularVelocity = 0;
    animator.speed = 0;
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.collider.isTrigger) return;
    // Hit wall and stopped moving
    rb2d.velocity = Vector2.zero;
    rb2d.angularVelocity = 0;
    animator.speed = 0;
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