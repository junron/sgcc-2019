using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
  [SerializeField] private GameObject interactText;
  [SerializeField] private GameObject target;
  [SerializeField] private float detectRadius;
  public Action callback;
  private bool inRange;
  private Transform transform1;

  private void Start()
  {
    transform1 = target.GetComponent<Transform>();
  }

  private void FixedUpdate()
  {
    float distance = Mathf.Abs(Vector3.Distance(transform1.position, this.transform.position));
    inRange = distance < detectRadius;
    interactText.SetActive(inRange);
    if (!inRange || !Input.GetKeyDown(KeyCode.Space)) return;
    callback();
    Destroy(this);
  }
}