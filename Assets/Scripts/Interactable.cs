using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
  [SerializeField] private GameObject interactText;
  [SerializeField] private GameObject target;
  [SerializeField] private float detectRadius;
  public Action callback;
  public bool inhibitInteraction;
  private Action detectCallback;
  private bool inRange;
  private Transform transform1;
  private bool isInteractTextNotNull;

  private void Start()
  {
    isInteractTextNotNull = interactText!=null;
    transform1 = target.GetComponent<Transform>();
  }

  private void FixedUpdate()
  {
    if (inhibitInteraction) return;
    float distance = Mathf.Abs(Vector3.Distance(transform1.position, this.transform.position));
    inRange = distance < detectRadius;
    if(isInteractTextNotNull) interactText.SetActive(inRange);
    if (inRange) detectCallback?.Invoke();
    if (!inRange || !Input.GetKeyDown(KeyCode.Space)) return;
    callback?.Invoke();
    Destroy(this);
  }

  public void SetOnDetect(Action callback)
  {
    detectCallback = callback;
  }
}