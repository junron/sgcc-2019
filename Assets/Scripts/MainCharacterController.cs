using System;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
  public float speed = 3.0f;

  public bool yFrozen;

  public bool xFrozen;

  private Rigidbody2D rb2d;

  [SerializeField] private Collider2D e;
  private Vector3 target;
  private bool isTargetNull = true;
  private bool isCameraNotNull;
  private Camera mainCamera;
  private Vector3 originalPosition;

  // Start is called before the first frame update
  void Start()
  {
    mainCamera = Camera.main;
    isCameraNotNull = mainCamera != null;
    rb2d = GetComponent<Rigidbody2D>();
    rb2d.freezeRotation = true;
  }

  // Update is called once per frame
  void Update()
  {
    Time.timeScale = 1;
    Vector3 position = this.transform.position;
    if (Input.GetMouseButtonDown(0))
    {
      if (isCameraNotNull)
      {
        target = mainCamera.ScreenToWorldPoint(Input.mousePosition);
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
    //    Ensure at least 20% speed at all times
    rb2d.velocity = 1.5f * Mathf.Max(0.1f, 1 - percentageCompleted) * speed * direction;
  }

  public void FreezeY()
  {
    yFrozen = true;
  }

  public void FreeY()
  {
    yFrozen = false;
  }

  public void FreezeX()
  {
    xFrozen = true;
  }

  public void FreeX()
  {
    xFrozen = false;
  }
}