﻿using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
  public float speed = 3.0f;

  public bool yFrozen;

  public bool xFrozen;

  // Should actually be called randomize
  public bool fsm;
  private Vector3 currentState = Vector3.up;
  private readonly Vector3[] possibleStates = {Vector3.up, Vector3.down, Vector3.right, Vector3.left};
  private Rigidbody2D rb2d;

  [SerializeField] private Collider2D e;

  // Start is called before the first frame update
  void Start()
  {
    rb2d = GetComponent<Rigidbody2D>();
    rb2d.freezeRotation = true;
    if (!fsm) return;
    Time.timeScale = 0.5f;
    rb2d.AddForce(currentState * 300);
  }

  // Update is called once per frame
  void Update()
  {
    if (fsm) return;
    float horizontal = xFrozen ? 0 : Input.GetAxis("Horizontal");
    float vert = yFrozen ? 0 : Input.GetAxis("Vertical");
    transform.position += new Vector3(horizontal, vert, 0) * Time.deltaTime * speed;
  }

  void FixedUpdate()
  {
    if (!fsm) return;
    var position = transform.position;
    Vector3 closestPoint = e.ClosestPoint(position);
    float distance = Vector3.Distance(closestPoint, position);
    // If it hits something...
    if (!(distance < 1.0)) return;
    Debug.Log("Hit");
    Vector3 newState = currentState;
    while (newState == currentState)
    {
      int choice = Random.Range(0, 4);
      Debug.Log(choice);
      newState = possibleStates[choice];
    }

    currentState = newState;
    rb2d.AddForce(currentState * 300);
  }

  public void FreezeY()
  {
    yFrozen = true;
  }

  public void FreeY(){
    yFrozen = false;
  }

  public void FreezeX()
  {
    xFrozen = true;
  }

  public void FreeX(){
    xFrozen = false;
  }
}